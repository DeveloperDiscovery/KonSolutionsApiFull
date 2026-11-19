using System.Data;
using System.Globalization;
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
[Route("api/v1/estandar/monedas-tipo-cambio")]
[ExigePermiso("ESTANDAR.MONEDAS_TIPO_CAMBIO")]
public class MonedasTipoCambioController : BaseController
{
    private readonly IMonedasTipoCambioService _service;
    private readonly ILogger<MonedasTipoCambioController> _log;
    private readonly IDbConnectionFactory _db;
    public MonedasTipoCambioController(IMonedasTipoCambioService service, ILogger<MonedasTipoCambioController> log, IDbConnectionFactory db)
    { _service = service; _log = log; _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? COD_MONEDA)
    {
        _log.LogInformation("[TC] GetAll COD_MONEDA={CodMoneda}", COD_MONEDA);
        var r = await _service.GetAllAsync(COD_MONEDA);
        _log.LogInformation("[TC] GetAll Success={Ok} Count={N} Msg={Msg}", r.Success, r.Total, r.Message);
        return r.Success ? Ok(r) : BadRequest(r);
    }

    [HttpGet("{COD_MONEDA}/{FEC_TIPO_CAMBIO}")]
    public async Task<IActionResult> GetById(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO)
    { var r = await _service.GetByIdAsync(COD_MONEDA, FEC_TIPO_CAMBIO); return r.Success ? Ok(r) : NotFound(r); }

    /// <summary>Tipo de cambio a usar para una moneda en una fecha, resuelto contra la
    /// pivot configurada — 1 si codMoneda ES la pivot. fecTipoCambio es opcional, por
    /// defecto hoy.</summary>
    [HttpGet("obtener")]
    public async Task<IActionResult> Obtener([FromQuery] string codMoneda, [FromQuery] DateTime? fecTipoCambio)
    { var r = await _service.ObtenerAsync(codMoneda, fecTipoCambio); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] MonedasTipoCambioSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] MonedasTipoCambioInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] MonedasTipoCambioUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_MONEDA}/{FEC_TIPO_CAMBIO}")]
    public async Task<IActionResult> Delete(string COD_MONEDA, DateTime FEC_TIPO_CAMBIO)
    { var r = await _service.DeleteAsync(COD_MONEDA, FEC_TIPO_CAMBIO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>
    /// Descarga el tipo de cambio del día desde la URL registrada en Externo → Snippets
    /// para este botón (ej. https://www.sunat.gob.pe/a/txt/tipoCambio.txt), en formato
    /// "dd/MM/yyyy|compra|venta|", y lo guarda como TC SUNAT (siempre USD) del día
    /// correspondiente, sin pisar los valores Comercial/Paralelo que ya existieran.
    /// </summary>
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
            // Algunos servidores (ej. SUNAT) rechazan peticiones sin un User-Agent tipo
            // navegador, devolviendo 404/403 en vez del contenido esperado.
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36");
            if (!string.IsNullOrEmpty(snippet.DES_TOKEN))
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {snippet.DES_TOKEN}");
            var texto = await client.GetStringAsync(snippet.URL_SNIPPET);

            var linea = texto.Split('\n', '\r').FirstOrDefault(l => !string.IsNullOrWhiteSpace(l));
            if (string.IsNullOrWhiteSpace(linea))
                return BadRequest(ApiResponse<object>.Fail("El snippet no devolvió datos de tipo de cambio."));

            var partes = linea.Split('|');
            if (partes.Length < 3)
                return BadRequest(ApiResponse<object>.Fail($"Formato inesperado del snippet: \"{linea}\""));

            if (!DateTime.TryParseExact(partes[0].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
                return BadRequest(ApiResponse<object>.Fail($"No se pudo interpretar la fecha: \"{partes[0]}\""));
            if (!decimal.TryParse(partes[1].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var compra))
                return BadRequest(ApiResponse<object>.Fail($"No se pudo interpretar el valor de compra: \"{partes[1]}\""));
            if (!decimal.TryParse(partes[2].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var venta))
                return BadRequest(ApiResponse<object>.Fail($"No se pudo interpretar el valor de venta: \"{partes[2]}\""));

            var r = await _service.GuardarSunatAsync(fecha, compra, venta, GetCodUsuario(), GetEstacion());
            return r.Success ? Ok(r) : BadRequest(r);
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }
}
