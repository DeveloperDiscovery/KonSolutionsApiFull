using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class DisenosEstructuraService : IDisenosEstructuraService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(DisenosEstructuraService);
    private readonly IDisenosEstructuraRepository _repo;
    private readonly ErrorLogService _log;
    public DisenosEstructuraService(IDisenosEstructuraRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    // 524xx (52400 a 52462) y 526xx (52620 a 52629, script 130) son validaciones de los scripts 107, 112 y 130 (version bloqueada, ciclo, mas de 10 niveles...): se devuelven como mensaje.
    private static bool EsValidacion(SqlException ex) => ex.Number is (>= 52400 and <= 52462) or (>= 52620 and <= 52629) or (>= 52730 and <= 52739);

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<DisenosEstructuraNodo>>> EstructuraAsync(string codDiseno, int numVersion, int maxNivel)
    {
        try { var d = (await _repo.EstructuraAsync(codDiseno, numVersion, maxNivel)).ToList(); return ApiResponse<IEnumerable<DisenosEstructuraNodo>>.Ok(d, total: d.Count); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<IEnumerable<DisenosEstructuraNodo>>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(EstructuraAsync)); return ApiResponse<IEnumerable<DisenosEstructuraNodo>>.Fail(ex.Message); }
    }

    public Task<ApiResponse<bool>> SectorGuardarAsync(DisenosEstructuraSectorDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.SectorGuardarAsync(dto, u, e); return true; }, nameof(SectorGuardarAsync));
    public Task<ApiResponse<bool>> SectorEliminarAsync(string codDiseno, int numVersion, string codSector) =>
        Ejecutar(async () => { await _repo.SectorEliminarAsync(codDiseno, numVersion, codSector); return true; }, nameof(SectorEliminarAsync));
    public Task<ApiResponse<bool>> ComponenteAgregarAsync(DisenosEstructuraComponenteDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.ComponenteAgregarAsync(dto, u, e); return true; }, nameof(ComponenteAgregarAsync));
    public Task<ApiResponse<bool>> ComponenteQuitarAsync(DisenosEstructuraComponenteDto dto) =>
        Ejecutar(async () => { await _repo.ComponenteQuitarAsync(dto); return true; }, nameof(ComponenteQuitarAsync));
    public Task<ApiResponse<bool>> ComponenteCambiarAsync(DisenosEstructuraComponenteCambiarDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.ComponenteCambiarAsync(dto, u, e); return true; }, nameof(ComponenteCambiarAsync));
    public Task<ApiResponse<DisenosEstructuraInsumoResultado>> InsumoGuardarAsync(DisenosEstructuraInsumoDto dto, string u, string e) =>
        Ejecutar(async () =>
        {
            dto.COD_TIPO_MATERIAL = string.IsNullOrWhiteSpace(dto.COD_TIPO_MATERIAL) ? null : dto.COD_TIPO_MATERIAL.Trim();
            if (dto.COD_TIPO_MATERIAL is { Length: > 2 }) throw new ArgumentException("El tipo de material no es valido.");
            var n = await _repo.InsumoGuardarAsync(dto, u, e);
            // si la linea tiene formula, su CAN_TOTAL se recalcula con el consumo / merma nuevos (si no se puede calcular el total queda vacio; no impide guardar el insumo)
            try { await _repo.FormulaCalcularAsync(new DisenosEstructuraFormulaCalcularDto { COD_DISENO = dto.COD_DISENO, NUM_VERSION = dto.NUM_VERSION, COD_SECTOR_PRODUCTO = dto.COD_SECTOR_PRODUCTO,
                COD_COMPONENTE_PRODUCTO = dto.COD_COMPONENTE_PRODUCTO, NUM_SECUENCIA = n, COD_PRODUCTO_COMPONENTE = dto.COD_PRODUCTO_COMPONENTE }, u, e); }
            catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsumoGuardarAsync)); }
            return new DisenosEstructuraInsumoResultado { NUM_SECUENCIA = n };
        }, nameof(InsumoGuardarAsync));
    public Task<ApiResponse<bool>> InsumoEliminarAsync(DisenosEstructuraInsumoClaveDto dto) =>
        Ejecutar(async () => { await _repo.InsumoEliminarAsync(dto); return true; }, nameof(InsumoEliminarAsync));
    public Task<ApiResponse<bool>> BloquearAsync(string codDiseno, int numVersion, string u) =>
        Ejecutar(async () => { await _repo.BloquearAsync(codDiseno, numVersion, u); return true; }, nameof(BloquearAsync));
    public Task<ApiResponse<bool>> DesbloquearAsync(string codDiseno, int numVersion, string u) =>
        Ejecutar(async () => { await _repo.DesbloquearAsync(codDiseno, numVersion, u); return true; }, nameof(DesbloquearAsync));
    public Task<ApiResponse<DisenosVersionCopiarResultado>> CopiarVersionAsync(DisenosVersionCopiarDto dto, string u, string e) =>
        Ejecutar(() => _repo.CopiarVersionAsync(dto, u, e), nameof(CopiarVersionAsync));
    public Task<ApiResponse<DisenosSegmentoCopiarResultado>> CopiarSegmentoAsync(DisenosSegmentoCopiarDto dto, string u, string e) =>
        Ejecutar(() => _repo.CopiarSegmentoAsync(dto, u, e), nameof(CopiarSegmentoAsync));

    // ---------- ruta de produccion de la version (script 112) ----------
    public Task<ApiResponse<DisenosRutaCatalogos>> RutaCatalogosAsync() =>
        Ejecutar(() => _repo.RutaCatalogosAsync(), nameof(RutaCatalogosAsync));
    public Task<ApiResponse<DisenosRutaResultado>> RutaCrearAsync(DisenosRutaCrearDto dto, string u, string e) =>
        Ejecutar(async () => new DisenosRutaResultado { COD_RUTA_PRODUCCION = await _repo.RutaCrearAsync(dto, u, e) }, nameof(RutaCrearAsync));
    public Task<ApiResponse<bool>> RutaAsignarAsync(DisenosRutaAsignarDto dto, string u, string e) =>
        Ejecutar(async () => { await _repo.RutaAsignarAsync(dto, u, e); return true; }, nameof(RutaAsignarAsync));
    public Task<ApiResponse<DisenosRutaResultado>> RutaCopiarAsync(DisenosRutaCopiarDto dto, string u, string e) =>
        Ejecutar(() => _repo.RutaCopiarAsync(dto, u, e), nameof(RutaCopiarAsync));

    // ---------- formulas de consumo en las lineas (script 147; errores 52730 a 52739) ----------
    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    private static void ValidarLinea(DisenosEstructuraLineaClaveDto l)
    {
        if (string.IsNullOrWhiteSpace(l.COD_DISENO) || l.NUM_VERSION <= 0 || string.IsNullOrWhiteSpace(l.COD_SECTOR_PRODUCTO) || string.IsNullOrWhiteSpace(l.COD_COMPONENTE_PRODUCTO)
            || l.NUM_SECUENCIA <= 0 || string.IsNullOrWhiteSpace(l.COD_PRODUCTO_COMPONENTE))
            throw new ArgumentException("Indica el diseno, la version, el sector, el componente, la secuencia y el producto de la linea.");
    }
    public Task<ApiResponse<IEnumerable<DisenosEstructuraFormulaDisponible>>> FormulaDisponiblesAsync(DisenosEstructuraLineaClaveDto l) =>
        Ejecutar(async () => { ValidarLinea(l); return (IEnumerable<DisenosEstructuraFormulaDisponible>)(await _repo.FormulaDisponiblesAsync(l)).ToList(); }, nameof(FormulaDisponiblesAsync));
    public Task<ApiResponse<DisenosEstructuraFormulaLinea>> FormulaLineaAsync(DisenosEstructuraLineaClaveDto l) =>
        Ejecutar(async () => { ValidarLinea(l); return await _repo.FormulaLineaAsync(l); }, nameof(FormulaLineaAsync));
    public Task<ApiResponse<DisenosEstructuraFormulaResultado>> FormulaAsignarAsync(DisenosEstructuraFormulaAsignarDto dto, string u, string e) =>
        Ejecutar(async () =>
        {
            ValidarLinea(dto);
            if (dto.NUM_FORMULACION <= 0) throw new ArgumentException("Elige la formula a asignar.");
            return await _repo.FormulaAsignarAsync(dto, u, e);
        }, nameof(FormulaAsignarAsync));
    public Task<ApiResponse<bool>> FormulaQuitarAsync(DisenosEstructuraLineaClaveDto l, string u, string e) =>
        Ejecutar(async () => { ValidarLinea(l); await _repo.FormulaQuitarAsync(l, u, e); return true; }, nameof(FormulaQuitarAsync));
    public Task<ApiResponse<DisenosEstructuraFormulaResultado>> FormulaCalcularAsync(DisenosEstructuraFormulaCalcularDto dto, string u, string e) =>
        Ejecutar(async () => { ValidarLinea(dto); return await _repo.FormulaCalcularAsync(dto, u, e); }, nameof(FormulaCalcularAsync));
    public Task<ApiResponse<IEnumerable<DisenosEstructuraFormulaVersionFila>>> FormulaCalcularVersionAsync(string codDiseno, int numVersion, bool guardar, string u, string e) =>
        Ejecutar(async () =>
        {
            if (Nulo(codDiseno) is null || numVersion <= 0) throw new ArgumentException("Indica el diseno y la version.");
            return (IEnumerable<DisenosEstructuraFormulaVersionFila>)(await _repo.FormulaCalcularVersionAsync(codDiseno, numVersion, guardar, u, e)).ToList();
        }, nameof(FormulaCalcularVersionAsync));
    private const int MaxFichasLinea = 200;
    // las fichas como las espera la base (el mismo JSON del constructor); las constantes y las especificaciones llevan su valor en DES_FORMULA
    private static string FichasJson(List<KonSolutionsApi.Domain.Models.Formulacion.FormulaFichaDto>? fichas)
    {
        if (fichas is null || fichas.Count == 0) throw new ArgumentException("La formula necesita al menos una ficha.");
        if (fichas.Count > MaxFichasLinea) throw new ArgumentException($"Una formula admite como maximo {MaxFichasLinea} fichas.");
        var lista = new List<object>();
        foreach (var f in fichas)
        {
            var comp = Nulo(f.COD_COMPONENTE_FORMULACION)?.ToUpperInvariant() ?? throw new ArgumentException("Falta el componente de una ficha.");
            var elem = Nulo(f.COD_ELEMENTO_FORMULACION) ?? "";
            var esp = Nulo(f.COD_ESPECIFICACION_TECNICA);
            if (comp.Length > 5 || elem.Length > 5) throw new ArgumentException("El codigo del componente o del elemento de una ficha es demasiado largo.");
            if (esp is null && elem.Length == 0) throw new ArgumentException("Falta el elemento de una ficha.");
            lista.Add(new { COD_COMPONENTE_FORMULACION = comp, COD_ELEMENTO_FORMULACION = elem.Length == 0 ? null : elem, DES_FORMULA = Nulo(f.DES_FORMULA), COD_ESPECIFICACION_TECNICA = esp });
        }
        return System.Text.Json.JsonSerializer.Serialize(lista);
    }

    public Task<ApiResponse<DisenosEstructuraFormulaResultado>> FormulaGuardarAsync(DisenosEstructuraFormulaGuardarDto dto, string u, string e) =>
        Ejecutar(async () => { ValidarLinea(dto); return await _repo.FormulaGuardarAsync(dto, FichasJson(dto.Fichas), u, e); }, nameof(FormulaGuardarAsync));

    public Task<ApiResponse<IEnumerable<DisenosEstructuraFormulaDiagrama>>> FormulaDiagramaAsync(string codDiseno, int numVersion) =>
        Ejecutar(async () =>
        {
            if (Nulo(codDiseno) is null || numVersion <= 0) throw new ArgumentException("Indica el diseno y la version.");
            return (IEnumerable<DisenosEstructuraFormulaDiagrama>)(await _repo.FormulaDiagramaAsync(codDiseno, numVersion)).ToList();
        }, nameof(FormulaDiagramaAsync));

    public Task<ApiResponse<IEnumerable<DisenosEstructuraIndicador>>> IndicadoresAsync(string codDiseno, int numVersion) =>
        Ejecutar(async () =>
        {
            if (Nulo(codDiseno) is null || numVersion <= 0) throw new ArgumentException("Indica el diseno y la version.");
            return (IEnumerable<DisenosEstructuraIndicador>)(await _repo.IndicadoresAsync(codDiseno, numVersion)).ToList();
        }, nameof(IndicadoresAsync));

    public Task<ApiResponse<IEnumerable<DisenosEstructuraTipoMaterial>>> TiposMaterialAsync() =>
        Ejecutar(async () => (IEnumerable<DisenosEstructuraTipoMaterial>)(await _repo.TiposMaterialAsync()).ToList(), nameof(TiposMaterialAsync));
}
