using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

/// <summary>Mapa de trazabilidad: un nodo, sus vecinos y su entidad. Sólo lectura.</summary>
public class TrazabilidadService : ITrazabilidadService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TrazabilidadService);
    // Los tipos que entienden los procedimientos; cualquier otro se rechaza acá, con un
    // mensaje claro, en vez de devolver un mapa vacío sin explicación.
    private static readonly HashSet<string> Tipos = new(StringComparer.OrdinalIgnoreCase) { "REQ", "RES", "PC", "MOV", "ALM" };
    // Desde qué se puede partir: requerimiento, reserva, proforma, orden de compra o movimiento.
    private static readonly HashSet<string> TiposBusqueda = new(StringComparer.OrdinalIgnoreCase) { "REQ", "RES", "PRF", "OC", "MOV" };
    private readonly ITrazabilidadRepository _repo;
    private readonly ErrorLogService _log;
    public TrazabilidadService(ITrazabilidadRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private static string? Validar(string? tipo, string? clave)
    {
        if (string.IsNullOrWhiteSpace(tipo) || !Tipos.Contains(tipo)) return $"Tipo de nodo no válido: '{tipo}'.";
        if (string.IsNullOrWhiteSpace(clave)) return "Falta la clave del nodo.";
        return null;
    }

    public async Task<ApiResponse<TrazabilidadNodo>> NodoAsync(string? tipo, string? clave)
    {
        if (Validar(tipo, clave) is { } error) return ApiResponse<TrazabilidadNodo>.Fail(error);
        try { var d = await _repo.NodoAsync(tipo!.ToUpperInvariant(), clave!.Trim());
              return d is null ? ApiResponse<TrazabilidadNodo>.Fail("No se encontró el documento.") : ApiResponse<TrazabilidadNodo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(NodoAsync));
              return ApiResponse<TrazabilidadNodo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TrazabilidadNodo>>> VecinosAsync(string? tipo, string? clave, string? sentido)
    {
        if (Validar(tipo, clave) is { } error) return ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail(error);
        if (!string.IsNullOrWhiteSpace(sentido) && sentido is not ("A" or "D"))
            return ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail($"Sentido no válido: '{sentido}'. Usa A (atrás) o D (adelante).");
        try { var d = (await _repo.VecinosAsync(tipo!.ToUpperInvariant(), clave!.Trim(), sentido)).ToList();
              return ApiResponse<IEnumerable<TrazabilidadNodo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(VecinosAsync));
              return ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TrazabilidadNodo>>> BuscarAsync(string? tipoBusqueda, string? numero)
    {
        if (string.IsNullOrWhiteSpace(tipoBusqueda) || !TiposBusqueda.Contains(tipoBusqueda))
            return ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail($"Tipo de búsqueda no válido: '{tipoBusqueda}'.");
        if (string.IsNullOrWhiteSpace(numero) || !decimal.TryParse(numero.Trim(), out var n) || n <= 0)
            return ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail("Escribe un número de documento válido.");
        try { var d = (await _repo.BuscarAsync(tipoBusqueda.ToUpperInvariant(), numero.Trim())).ToList();
              return d.Count == 0
                  ? ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail("No se encontró ningún documento con ese número.")
                  : ApiResponse<IEnumerable<TrazabilidadNodo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(BuscarAsync));
              return ApiResponse<IEnumerable<TrazabilidadNodo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TrazabilidadFranja>>> FranjasAsync()
    {
        try { var d = (await _repo.FranjasAsync()).ToList();
              return ApiResponse<IEnumerable<TrazabilidadFranja>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(FranjasAsync));
              return ApiResponse<IEnumerable<TrazabilidadFranja>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TrazabilidadEntidad>> EntidadAsync(string? tipo, string? clave)
    {
        if (Validar(tipo, clave) is { } error) return ApiResponse<TrazabilidadEntidad>.Fail(error);
        try { return ApiResponse<TrazabilidadEntidad>.Ok(await _repo.EntidadAsync(tipo!.ToUpperInvariant(), clave!.Trim())); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EntidadAsync));
              return ApiResponse<TrazabilidadEntidad>.Fail(ex.Message); }
    }
}
