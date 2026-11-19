using System.Data;
using System.Text.Json;
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
[Route("api/v1/estandar/unidades-medida")]
[ExigePermiso("ESTANDAR.UNIDADES_MEDIDA")]
public class UnidadesMedidaController : BaseController
{
    private readonly IUnidadesMedidaService    _service;
    private readonly ITiposUnidadMedidaService _tiposService;
    private readonly IDbConnectionFactory      _db;
    private readonly IWebHostEnvironment       _env;

    public UnidadesMedidaController(
        IUnidadesMedidaService    service,
        ITiposUnidadMedidaService tiposService,
        IDbConnectionFactory      db,
        IWebHostEnvironment       env)
    {
        _service      = service;
        _tiposService = tiposService;
        _db           = db;
        _env          = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> GetById(string COD_UNIDAD_MEDIDA)
    { var r = await _service.GetByIdAsync(COD_UNIDAD_MEDIDA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] UnidadesMedidaSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UnidadesMedidaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut("{COD_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> Update(string COD_UNIDAD_MEDIDA, [FromBody] UnidadesMedidaInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(COD_UNIDAD_MEDIDA, dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_UNIDAD_MEDIDA}")]
    public async Task<IActionResult> Delete(string COD_UNIDAD_MEDIDA)
    { var r = await _service.DeleteAsync(COD_UNIDAD_MEDIDA); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>
    /// Descarga tipos de magnitud + unidades de medida desde una URL registrada en Snippets.
    /// El JSON del snippet debe tener la forma: { "tipos": [...], "unidades": [...] }
    /// La URL del snippet se registra en el módulo Externo → Snippets del sistema.
    /// </summary>
    [HttpPost("download-from-snippet")]
    public async Task<IActionResult> DownloadFromSnippet(
        [FromBody] SnippetBotonDto dto,
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
                return BadRequest(ApiResponse<object>.Fail("No se encontró snippet registrado para este botón. Registre la URL en Externo → Snippets."));

            string json;
            if (snippet.URL_SNIPPET.StartsWith('/'))
            {
                var basePath = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
                var filePath = Path.Combine(basePath,
                    snippet.URL_SNIPPET.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (!System.IO.File.Exists(filePath))
                    return BadRequest(ApiResponse<object>.Fail($"Archivo local no encontrado: {filePath}"));
                json = await System.IO.File.ReadAllTextAsync(filePath);
            }
            else
            {
                var client = httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(30);
                if (!string.IsNullOrEmpty(snippet.DES_TOKEN))
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {snippet.DES_TOKEN}");
                json = await client.GetStringAsync(snippet.URL_SNIPPET);
            }

            using var doc    = JsonDocument.Parse(json);
            var tiposJson    = doc.RootElement.TryGetProperty("tipos",    out var t) ? t.GetRawText() : "[]";
            var unidadesJson = doc.RootElement.TryGetProperty("unidades", out var u) ? u.GetRawText() : "[]";

            var rTipos    = await _tiposService.UploadJsonAsync(tiposJson);
            var rUnidades = await _service.UploadJsonAsync(unidadesJson);

            if (!rTipos.Success)    return BadRequest(rTipos);
            if (!rUnidades.Success) return BadRequest(rUnidades);

            return Ok(ApiResponse<object>.Ok(new
            {
                TiposCargados    = rTipos.Data?.AffectedRows    ?? 0,
                UnidadesCargadas = rUnidades.Data?.AffectedRows ?? 0
            }, $"Carga completada: {rTipos.Data?.AffectedRows ?? 0} tipos · {rUnidades.Data?.AffectedRows ?? 0} unidades."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }
}
