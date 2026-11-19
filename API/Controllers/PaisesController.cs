using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/paises")]
[ExigePermiso("ESTANDAR.PAISES")]
public class PaisesController : BaseController
{
    private readonly IPaisesService       _service;
    private readonly IDbConnectionFactory _db;
    private readonly FlagDownloadProgress _progress;

    public PaisesController(IPaisesService service, IDbConnectionFactory db, FlagDownloadProgress progress)
    {
        _service  = service;
        _db       = db;
        _progress = progress;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_PAIS}")]
    public async Task<IActionResult> GetById(string COD_PAIS)
    { var r = await _service.GetByIdAsync(COD_PAIS); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string? search = null)
    { var r = await _service.GetComboboxAsync(search); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] PaisesSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("flag-download-status")]
    public IActionResult FlagDownloadStatus()
        => Ok(ApiResponse<object>.Ok(new
        {
            _progress.Total,
            _progress.Processed,
            _progress.IsRunning,
            _progress.Percentage
        }));

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PaisesInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PaisesUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_PAIS}")]
    public async Task<IActionResult> Delete(string COD_PAIS)
    { var r = await _service.DeleteAsync(COD_PAIS); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromBody] PaisesUploadDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.JsonArray))
            return BadRequest(ApiResponse<object>.Fail("JSON requerido."));
        var r = await _service.UploadJsonAsync(dto.JsonArray);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpPost("download-from-snippet")]
    public async Task<IActionResult> DownloadFromSnippet([FromBody] SnippetBotonDto dto,
        [FromServices] IHttpClientFactory httpClientFactory)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var snippet = await conn.QueryFirstOrDefaultAsync<SnippetUrlResult>(
                SP.SCHEMAExterno.Snippets.SearchBoton,
                new { dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.COD_BOTON_OPCION },
                commandType: CommandType.StoredProcedure);
            if (snippet is null || string.IsNullOrWhiteSpace(snippet.URL_SNIPPET))
                return BadRequest(ApiResponse<object>.Fail("No se encontró snippet para el botón indicado."));

            var client = httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(snippet.DES_TOKEN))
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {snippet.DES_TOKEN}");
            var json = await client.GetStringAsync(snippet.URL_SNIPPET);

            var r = await _service.UploadJsonAsync(json);
            if (!r.Success) return BadRequest(r);

            _ = Task.Run(() => DescargarBanderasAsync(httpClientFactory));

            return Ok(r);
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }

    private async Task DescargarBanderasAsync(IHttpClientFactory httpClientFactory)
    {
        if (_progress.IsRunning) return;
        try
        {
            _progress.IsRunning = true;
            _progress.Processed = 0;
            _progress.Total     = 0;

            using var conn = _db.CreateConnection();
            var paises = (await conn.QueryAsync<PaisBanderaUrlDto>(
                "PROC_ESTANDAR_PAISES_BANDERAS_PENDIENTES",
                commandType: System.Data.CommandType.StoredProcedure)).ToList();

            _progress.Total = paises.Count;

            var http = httpClientFactory.CreateClient();
            http.Timeout = TimeSpan.FromSeconds(30);

            foreach (var p in paises)
            {
                try
                {
                    byte[]? png = null, svg = null;
                    if (!string.IsNullOrEmpty(p.URL_BANDERA_PNG))
                        png = await http.GetByteArrayAsync(p.URL_BANDERA_PNG);
                    if (!string.IsNullOrEmpty(p.URL_BANDERA_SVG))
                        svg = await http.GetByteArrayAsync(p.URL_BANDERA_SVG);

                    if (png is not null || svg is not null)
                        await conn.ExecuteAsync(
                            "PROC_ESTANDAR_PAISES_IMAGE",
                            new { p.COD_PAIS, IMG_ICONO = png, IMG_ICONO_SVG = svg },
                            commandType: CommandType.StoredProcedure);
                }
                catch { }
                finally { _progress.Processed++; }
            }
        }
        catch { }
        finally { _progress.IsRunning = false; }
    }
}

public class PaisBanderaUrlDto
{
    public string  COD_PAIS        { get; set; } = string.Empty;
    public string? URL_BANDERA_PNG { get; set; }
    public string? URL_BANDERA_SVG { get; set; }
}

public class PaisesUploadDto
{
    public string JsonArray { get; set; } = string.Empty;
}

public class SnippetBotonDto
{
    public string COD_APLICACION        { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_BOTON_OPCION      { get; set; } = string.Empty;
}

public class SnippetUrlResult
{
    public string? URL_SNIPPET { get; set; }
    public string? DES_TOKEN   { get; set; }
}
