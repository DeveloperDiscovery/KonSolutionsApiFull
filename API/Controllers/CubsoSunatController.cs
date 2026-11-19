using System.Data;
using System.Net.Http.Json;
using System.Text.Json;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sunat;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/sunat/cubso-sunat")]
[ExigePermiso("SUNAT.CUBSO_SUNAT")]
public class CubsoSunatController : BaseController
{
    private readonly ICubsoSunatService  _service;
    private readonly IDbConnectionFactory _db;
    private readonly IServiceScopeFactory _scopeFactory;

    public CubsoSunatController(ICubsoSunatService service, IDbConnectionFactory db, IServiceScopeFactory scopeFactory)
    {
        _service      = service;
        _db           = db;
        _scopeFactory = scopeFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("{COD_CUBSO}")]
    public async Task<IActionResult> GetById(string COD_CUBSO)
    { var r = await _service.GetByIdAsync(COD_CUBSO); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] CubsoSunatSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    // POST = crear, PUT = modificar. El SP _INSERT ya no hace upsert.
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CubsoSunatInsertDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.InsertAsync(dto);
      return r.Success ? StatusCode(201, r) : BadRequest(r); }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CubsoSunatUpdateDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.UpdateAsync(dto);
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{COD_CUBSO}")]
    public async Task<IActionResult> Delete(string COD_CUBSO)
    { var r = await _service.DeleteAsync(COD_CUBSO); return r.Success ? Ok(r) : BadRequest(r); }

    /// <summary>
    /// Descarga el catálogo UNSPSC (base del catálogo CUBSO de SUNAT) desde una URL
    /// registrada en Externo → Snippets (mismo patrón que Países/Ubigeos/Unidades de
    /// Medida). Shape confirmado por Pedro contra el API real de UNGM: cada elemento
    /// del arreglo raíz trae "Id" (código propio), "ParentId" (código del nodo padre
    /// en la jerarquía UNSPSC), "UNSPSCode" (el código UNSPSC de 8 dígitos) y "Title"
    /// (descripción en inglés) — mapeados a COD_CUBSO / COD_CUBSO_PARENT / COD_UNSPS / DES_CUBSO.
    ///
    /// IMPORTANTE sobre la traducción: traducir miles de descripciones distintas puede
    /// tardar mucho más que el timeout del HttpClient del navegador (100s por defecto).
    /// Por eso este endpoint responde de inmediato apenas sube los códigos SIN traducir,
    /// y dispara la traducción como una tarea de FONDO (fire-and-forget, con su propio
    /// scope de DI vía IServiceScopeFactory porque el request original ya habrá
    /// terminado). La traducción se va guardando en DES_CUBSO_ES por lotes a medida que
    /// avanza — el usuario puede seguir el progreso simplemente refrescando la grilla.
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

            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(60);
            if (!string.IsNullOrEmpty(snippet.DES_TOKEN))
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {snippet.DES_TOKEN}");
            var json = await client.GetStringAsync(snippet.URL_SNIPPET);

            var (filas, totalEncontrados) = Aplanar(json);
            if (totalEncontrados == 0)
                return BadRequest(ApiResponse<object>.Fail(
                    "El snippet respondió, pero no se encontró ningún elemento con \"Id\" en el JSON " +
                    "(ni en la raíz ni dentro de una propiedad \"value\"). Revisa si el shape de la respuesta cambió."));

            var flatJson = JsonSerializer.Serialize(filas);
            var r = await _service.UploadJsonAsync(flatJson);
            if (!r.Success) return BadRequest(r);

            var traductor = await ResolverSnippetTraductorAsync(conn);
            var mensaje = $"Carga completada: {r.Data?.AffectedRows ?? 0} de {totalEncontrados} códigos CUBSO.";
            if (traductor is null || string.IsNullOrWhiteSpace(traductor.URL_SNIPPET))
            {
                mensaje += " (No se tradujo: no hay ningún Snippet con \"TRADUCTOR\" en su descripción.)";
            }
            else
            {
                mensaje += " La traducción al español se está generando en segundo plano " +
                           "(puede tardar varios minutos); refresca la grilla para ver el avance.";
                DispararTraduccionEnFondo(filas, traductor);
            }

            return Ok(ApiResponse<object>.Ok(new
            {
                CodigosEncontrados = totalEncontrados,
                CodigosCargados    = r.Data?.AffectedRows ?? 0
            }, mensaje));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Fire-and-forget: crea su propio scope de DI (el del request HTTP original ya
    /// habrá terminado y sido liberado cuando esto corra) y va traduciendo por lotes,
    /// guardando el avance en DES_CUBSO_ES cada LOTE filas para no perder el progreso
    /// si algo falla a mitad de camino.
    /// </summary>
    private void DispararTraduccionEnFondo(List<CubsoFlatRow> filas, SnippetUrlResult traductor)
    {
        const int LOTE = 300;
        _ = Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            var service = scope.ServiceProvider.GetRequiredService<ICubsoSunatService>();
            var log = scope.ServiceProvider.GetRequiredService<ErrorLogService>();
            const string Clase = nameof(CubsoSunatController);

            try
            {
                var textosUnicos = filas
                    .Where(f => !string.IsNullOrWhiteSpace(f.DES_CUBSO))
                    .Select(f => f.DES_CUBSO!)
                    .Distinct()
                    .ToList();

                log.LogTrace(Clase, "TraduccionCubso.Inicio",
                    $"URL={traductor.URL_SNIPPET} | TextosUnicos={textosUnicos.Count} | Filas={filas.Count}");

                var client = httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(20);
                var semaforo = new SemaphoreSlim(5);
                var traducciones = new Dictionary<string, string>();
                var fallas = new List<string>();

                var tareas = textosUnicos.Select(async texto =>
                {
                    await semaforo.WaitAsync();
                    try
                    {
                        var body = new Dictionary<string, string?>
                        {
                            ["q"] = texto, ["source"] = "en", ["target"] = "es", ["format"] = "text"
                        };
                        if (!string.IsNullOrEmpty(traductor.DES_TOKEN)) body["api_key"] = traductor.DES_TOKEN;

                        var resp = await client.PostAsJsonAsync(traductor.URL_SNIPPET, body);
                        if (!resp.IsSuccessStatusCode)
                        {
                            var cuerpo = await resp.Content.ReadAsStringAsync();
                            lock (fallas)
                            {
                                if (fallas.Count < 5)
                                    fallas.Add($"HTTP {(int)resp.StatusCode} traduciendo \"{texto}\": {cuerpo}");
                            }
                            return;
                        }
                        var resultado = await resp.Content.ReadFromJsonAsync<LibreTranslateResult>();
                        if (!string.IsNullOrWhiteSpace(resultado?.TranslatedText))
                            lock (traducciones) { traducciones[texto] = resultado!.TranslatedText!; }
                        else
                            lock (fallas) { if (fallas.Count < 5) fallas.Add($"Respuesta sin translatedText para \"{texto}\"."); }
                    }
                    catch (Exception ex)
                    {
                        lock (fallas) { if (fallas.Count < 5) fallas.Add($"Excepción traduciendo \"{texto}\": {ex.Message}"); }
                    }
                    finally { semaforo.Release(); }
                });
                await Task.WhenAll(tareas);

                if (fallas.Count > 0)
                    log.LogTrace(Clase, "TraduccionCubso.Fallas", string.Join(" || ", fallas));

                int filasActualizadas = 0;
                for (int i = 0; i < filas.Count; i += LOTE)
                {
                    var lote = filas.Skip(i).Take(LOTE)
                        .Where(f => f.DES_CUBSO is not null && traducciones.ContainsKey(f.DES_CUBSO))
                        .Select(f => f with { DES_CUBSO_ES = traducciones[f.DES_CUBSO!] })
                        .ToList();
                    if (lote.Count == 0) continue;

                    try
                    {
                        await service.UploadJsonAsync(JsonSerializer.Serialize(lote));
                        filasActualizadas += lote.Count;
                    }
                    catch (Exception ex)
                    {
                        log.LogError(ex, "CubsoSunat", Clase, "TraduccionCubso.UploadLote", $"Lote desde índice {i}, {lote.Count} filas.");
                    }
                }

                log.LogTrace(Clase, "TraduccionCubso.Fin",
                    $"Traducidos: {traducciones.Count}/{textosUnicos.Count} textos únicos | Filas guardadas: {filasActualizadas}/{filas.Count}.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "CubsoSunat", Clase, "TraduccionCubso", "Fallo general de la tarea de traducción en fondo.");
            }
        });
    }

    private const string NombreSnippetTraductor = "TRADUCTOR";
    private static int? _codSnippetTraductorCache;

    private static async Task<SnippetUrlResult?> ResolverSnippetTraductorAsync(IDbConnection conn)
    {
        if (_codSnippetTraductorCache is null)
        {
            var candidatos = await conn.QueryAsync<SnippetBusquedaResult>(
                "PROC_EXTERNO_SNIPPETS_SHOWALL", commandType: CommandType.StoredProcedure);
            var encontrado = candidatos.FirstOrDefault(s =>
                (s.DES_SNIPPET ?? "").Contains(NombreSnippetTraductor, StringComparison.OrdinalIgnoreCase));
            if (encontrado is null) return null;
            _codSnippetTraductorCache = encontrado.COD_SNIPPET;
        }
        return await conn.QueryFirstOrDefaultAsync<SnippetUrlResult>(
            "PROC_EXTERNO_SNIPPETS_SHOWBYID",
            new { COD_SNIPPET = _codSnippetTraductorCache },
            commandType: CommandType.StoredProcedure);
    }

    private static (List<CubsoFlatRow> filas, int total) Aplanar(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var raiz = doc.RootElement;

        // El API de UNGM devuelve un sobre OData: { "@odata.context": "...", "value": [...] }
        // — no un arreglo raíz directo.
        JsonElement arreglo;
        if (raiz.ValueKind == JsonValueKind.Array)
            arreglo = raiz;
        else if (raiz.ValueKind == JsonValueKind.Object && raiz.TryGetProperty("value", out var valueEl) && valueEl.ValueKind == JsonValueKind.Array)
            arreglo = valueEl;
        else
            return (new List<CubsoFlatRow>(), 0);

        var filas = new List<CubsoFlatRow>();
        var vistos = new HashSet<string>();

        foreach (var item in arreglo.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object) continue;
            // "Id" con mayúscula inicial, numérico (no string) — confirmado contra el
            // API real: { "Id": 100, "ParentId": null, "UNSPSCode": "A", "Title": "..." }
            if (!item.TryGetProperty("Id", out var idEl)) continue;

            var codigo = ComoTexto(idEl);
            if (string.IsNullOrWhiteSpace(codigo) || !vistos.Add(codigo)) continue;

            var descripcion = item.TryGetProperty("Title", out var tEl) ? ComoTexto(tEl) : null;
            var parent      = item.TryGetProperty("ParentId", out var pEl) ? ComoTexto(pEl) : null;
            var unspsc      = item.TryGetProperty("UNSPSCode", out var uEl) ? ComoTexto(uEl) : null;

            filas.Add(new CubsoFlatRow(codigo.Trim(), descripcion?.Trim(), null, parent?.Trim(), unspsc?.Trim()));
        }

        return (filas, filas.Count);
    }

    private static string? ComoTexto(JsonElement el) => el.ValueKind switch
    {
        JsonValueKind.String => el.GetString(),
        JsonValueKind.Number => el.GetRawText(),
        JsonValueKind.Null   => null,
        _ => null
    };

    private record CubsoFlatRow(string COD_CUBSO, string? DES_CUBSO, string? DES_CUBSO_ES, string? COD_CUBSO_PARENT, string? COD_UNSPS);
}

file class LibreTranslateResult
{
    public string? TranslatedText { get; set; }
}

file class SnippetBusquedaResult
{
    public int     COD_SNIPPET { get; set; }
    public string? DES_SNIPPET { get; set; }
}
