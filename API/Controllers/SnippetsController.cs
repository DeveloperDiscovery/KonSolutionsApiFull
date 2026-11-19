using System.Data;
using System.Net.Http.Json;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[ExigePermiso("EXTERNO.SNIPPETS")]
[Route("api/v1/externo/snippets")]
public class SnippetsController : BaseController
{
    private readonly IDbConnectionFactory _db;
    private readonly ErrorLogService _errorLog;
    public SnippetsController(IDbConnectionFactory db, ErrorLogService errorLog)
    { _db = db; _errorLog = errorLog; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(
                "PROC_EXTERNO_SNIPPETS_SHOWALL",
                commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<object>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(GetAll));
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q = "")
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(
                "PROC_EXTERNO_SNIPPETS_SEARCH",
                new { SEARCH = q },
                commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<object>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(Search), $"q={q}");
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryAsync(
                "PROC_EXTERNO_SNIPPETS_SHOWCOMBOX",
                commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<object>.Ok(data));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(GetCombobox));
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    [HttpGet("{codSnippet:int}")]
    public async Task<IActionResult> GetById(int codSnippet)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var data = await conn.QueryFirstOrDefaultAsync(
                "PROC_EXTERNO_SNIPPETS_SHOWBYID",
                new { COD_SNIPPET = codSnippet },
                commandType: CommandType.StoredProcedure);
            return data is not null
                ? Ok(ApiResponse<object>.Ok(data))
                : NotFound(ApiResponse<object>.Fail("Snippet no encontrado."));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(GetById), $"COD_SNIPPET={codSnippet}");
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SnippetSaveDto dto)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var p = new DynamicParameters();
            p.Add("COD_SNIPPET",           dto.COD_SNIPPET);
            p.Add("DES_SNIPPET",           dto.DES_SNIPPET);
            p.Add("URL_SNIPPET",           dto.URL_SNIPPET);
            p.Add("DES_TOKEN",             dto.DES_TOKEN,             System.Data.DbType.String);
            p.Add("FLG_FREE_PAY",          dto.FLG_FREE_PAY);
            p.Add("COD_APLICACION",        dto.COD_APLICACION,        System.Data.DbType.String);
            p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION, System.Data.DbType.String);
            p.Add("COD_BOTON_OPCION",      dto.COD_BOTON_OPCION,      System.Data.DbType.String);
            p.Add("COD_TIPO_ESTADO",       dto.COD_TIPO_ESTADO);
            p.Add("COD_ESTADO",            dto.COD_ESTADO);
            p.Add("COD_USUARIO_REGISTRO",  GetCodUsuario());
            p.Add("COD_ESTACION_REGISTRO", GetEstacion());
            var result = await conn.ExecuteScalarAsync<string>(
                "PROC_EXTERNO_SNIPPETS_INSERT", p,
                commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<object>.Ok((object?)result ?? new object(), message: "Guardado correctamente."));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(Save), $"COD_SNIPPET={dto.COD_SNIPPET}");
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    [HttpDelete("{codSnippet:int}")]
    public async Task<IActionResult> Delete(int codSnippet)
    {
        try
        {
            using var conn = _db.CreateConnection();
            await conn.ExecuteAsync(
                "PROC_EXTERNO_SNIPPETS_DELETE",
                new { COD_SNIPPET = codSnippet },
                commandType: CommandType.StoredProcedure);
            return Ok(ApiResponse<object>.Ok("Eliminado correctamente."));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(Delete), $"COD_SNIPPET={codSnippet}");
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    // Nombre convencional (no case-sensitive, basta con que lo contenga) que debe llevar
    // la DES_SNIPPET del registro de traducción en Externo → Snippets. Así el endpoint se
    // resuelve solo, por nombre — nadie tiene que acordarse ni hardcodear un COD_SNIPPET.
    private const string NombreSnippetTraductor = "TRADUCTOR";

    // Cacheado en memoria (vive todo el proceso) tras la primera resolución exitosa, para
    // no consultar la BD en cada traducción — igual que ObtenerDirectorio() en
    // ErrorLogService. Si renombras o recreas el snippet, reinicia la API para que lo
    // vuelva a resolver.
    private static int? _codSnippetTraductorCache;

    /// <summary>
    /// Traducción genérica de texto, reusable desde cualquier módulo (no solo Cubso Sunat):
    /// resuelve el snippet de traducción POR NOMBRE (ver <see cref="NombreSnippetTraductor"/>),
    /// no por un COD_SNIPPET hardcodeado — así nadie necesita anotar ni mantener un ID.
    /// Contrato de LibreTranslate: POST {q, source, target, format} → {translatedText}.
    /// La api_key (si el snippet tiene DES_TOKEN) va en el body, no como Bearer header —
    /// es como LibreTranslate espera la credencial, a diferencia de los demás snippets.
    /// </summary>
    [HttpPost("traducir")]
    public async Task<IActionResult> Traducir(
        [FromBody] TraducirDto dto,
        [FromServices] IHttpClientFactory httpClientFactory)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Texto))
                return Ok(ApiResponse<string>.Ok(""));

            using var conn = _db.CreateConnection();

            if (_codSnippetTraductorCache is null)
            {
                var candidatos = await conn.QueryAsync<SnippetBusquedaResult>(
                    "PROC_EXTERNO_SNIPPETS_SHOWALL", commandType: CommandType.StoredProcedure);
                var encontrado = candidatos.FirstOrDefault(s =>
                    (s.DES_SNIPPET ?? "").Contains(NombreSnippetTraductor, StringComparison.OrdinalIgnoreCase));
                if (encontrado is null)
                    return BadRequest(ApiResponse<string>.Fail(
                        $"No hay ningún Snippet en Externo → Snippets cuya descripción contenga \"{NombreSnippetTraductor}\". " +
                        "Crea uno (ej. \"Traductor ES-EN\") con la URL de tu servicio LibreTranslate."));
                _codSnippetTraductorCache = encontrado.COD_SNIPPET;
            }

            var snippet = await conn.QueryFirstOrDefaultAsync<SnippetUrlResult>(
                "PROC_EXTERNO_SNIPPETS_SHOWBYID",
                new { COD_SNIPPET = _codSnippetTraductorCache },
                commandType: CommandType.StoredProcedure);

            if (snippet is null || string.IsNullOrWhiteSpace(snippet.URL_SNIPPET))
                return BadRequest(ApiResponse<string>.Fail("No se encontró el snippet de traducción configurado."));

            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(20);

            var body = new Dictionary<string, string?>
            {
                ["q"]      = dto.Texto,
                ["source"] = string.IsNullOrWhiteSpace(dto.Origen)  ? "es" : dto.Origen,
                ["target"] = string.IsNullOrWhiteSpace(dto.Destino) ? "en" : dto.Destino,
                ["format"] = "text"
            };
            if (!string.IsNullOrEmpty(snippet.DES_TOKEN))
                body["api_key"] = snippet.DES_TOKEN;

            var resp = await client.PostAsJsonAsync(snippet.URL_SNIPPET, body);
            if (!resp.IsSuccessStatusCode)
                return BadRequest(ApiResponse<string>.Fail($"El servicio de traducción respondió {(int)resp.StatusCode}."));

            var resultado = await resp.Content.ReadFromJsonAsync<LibreTranslateResult>();
            return Ok(ApiResponse<string>.Ok(resultado?.TranslatedText ?? ""));
        }
        catch (Exception ex)
        {
            _errorLog.LogError(ex, "KonSolutionsApi", nameof(SnippetsController), nameof(Traducir));
            return BadRequest(ApiResponse<string>.Fail(ex.Message));
        }
    }
}

public class TraducirDto
{
    public string  Texto      { get; set; } = string.Empty;
    public string? Origen     { get; set; } // ej. "es" — default "es" si no se manda
    public string? Destino    { get; set; } // ej. "en" — default "en" si no se manda
}

public class LibreTranslateResult
{
    public string? TranslatedText { get; set; }
}

public class SnippetBusquedaResult
{
    public int     COD_SNIPPET { get; set; }
    public string? DES_SNIPPET { get; set; }
}

public class SnippetSaveDto
{
    public int?    COD_SNIPPET            { get; set; }
    public string  DES_SNIPPET            { get; set; } = string.Empty;
    public string  URL_SNIPPET            { get; set; } = string.Empty;
    public string? DES_TOKEN              { get; set; }
    public bool    FLG_FREE_PAY           { get; set; }
    public string? COD_APLICACION         { get; set; }
    public string? COD_OPCION_APLICACION  { get; set; }
    public string? COD_BOTON_OPCION       { get; set; }
    public int?    COD_TIPO_ESTADO        { get; set; }
    public int?    COD_ESTADO             { get; set; }
}
