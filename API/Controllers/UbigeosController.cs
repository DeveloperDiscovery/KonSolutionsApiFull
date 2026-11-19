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
[Route("api/v1/estandar/ubigeos")]
[ExigePermiso("ESTANDAR.UBIGEOS")]
public class UbigeosController : BaseController
{
    private readonly IUbigeosService    _service;
    private readonly IDbConnectionFactory _db;
    public UbigeosController(IUbigeosService service, IDbConnectionFactory db)
    { _service = service; _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_UBIGEO}")]
    public async Task<IActionResult> GetById(string COD_UBIGEO)
    { var r = await _service.GetByIdAsync(COD_UBIGEO); return r.Success ? Ok(r) : NotFound(r); }

    // Antes [AllowAnonymous]: sin datos sensibles (catálogo de ubigeos de Perú),
    // pero ningún consumidor del Web lo llama antes de autenticar (todos son
    // dialogs ya logueados) — se retira la excepción innecesaria.
    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox([FromQuery] string? search = null)
    { var r = await _service.GetComboboxAsync(search); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UbigeosSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UbigeosInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UbigeosUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_UBIGEO}")]
    public async Task<IActionResult> Delete(string COD_UBIGEO)
    { var r = await _service.DeleteAsync(COD_UBIGEO); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost("upload")]
    public async Task<IActionResult> UBIGEOSUpload([FromBody] UbigeosUploadDto dto)
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

            var flatJson = AplanarUbigeos(json);
            var r = await _service.UploadJsonAsync(flatJson);
            return r.Success ? Ok(r) : BadRequest(r);
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }

    private static string AplanarUbigeos(string json)
    {
        using var doc = System.Text.Json.JsonDocument.Parse(json);
        var filas = new List<UbigeoFlatRow>();
        var deptosSeen  = new HashSet<string>();
        var provSeen    = new HashSet<string>();
        var distSeen    = new HashSet<string>();

        foreach (var depto in doc.RootElement.EnumerateObject())
        {
            var deptoNombre = depto.Name;
            foreach (var prov in depto.Value.EnumerateObject())
            {
                var provNombre = prov.Name;
                foreach (var dist in prov.Value.EnumerateObject())
                {
                    var distNombre = dist.Name;
                    if (!dist.Value.TryGetProperty("inei", out var ubigeoEl)) continue;
                    var ubigeo = ubigeoEl.GetString() ?? "";
                    if (ubigeo.Length != 6) continue;

                    var codDepto = ubigeo[..2] + "0000";
                    var codProv  = ubigeo[..4] + "00";

                    if (deptosSeen.Add(codDepto))
                        filas.Add(new UbigeoFlatRow(codDepto, deptoNombre, true, false, false));
                    if (provSeen.Add(codProv))
                        filas.Add(new UbigeoFlatRow(codProv, provNombre, false, true, false));
                    if (distSeen.Add(ubigeo))
                        filas.Add(new UbigeoFlatRow(ubigeo, distNombre, false, false, true));
                }
            }
        }

        return System.Text.Json.JsonSerializer.Serialize(filas);
    }

    private record UbigeoFlatRow(
        string COD_UBIGEO,
        string DES_UBIGEO,
        bool   FLG_DEPARTAMENTO,
        bool   FLG_PROVINCIA,
        bool   FLG_DISTRITO);
}

public class UbigeosUploadDto
{
    public string JsonArray { get; set; } = string.Empty;
}