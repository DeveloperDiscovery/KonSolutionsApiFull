using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

// ── Maestro ──────────────────────────────────────────────────────────────────
public class FlujosAprobacionService : IFlujosAprobacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosAprobacionService);
    private readonly IFlujosAprobacionRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosAprobacionService(IFlujosAprobacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosAprobacion>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<FlujosAprobacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosAprobacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosAprobacion>> GetByIdAsync(short NUM_FLUJO_APROBACION)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_FLUJO_APROBACION);
              return d is null ? ApiResponse<FlujosAprobacion>.Fail("No encontrado.") : ApiResponse<FlujosAprobacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosAprobacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosAprobacion>>> SearchAsync(FlujosAprobacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosAprobacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosAprobacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosAprobacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FlujosAprobacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(short NUM_FLUJO_APROBACION)
    {
        try { var r = await _repo.DeleteAsync(NUM_FLUJO_APROBACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}


// ── Detalle: Área / Cargo ────────────────────────────────────────────────────
public class FlujosAprobacionAreaCargoService : IFlujosAprobacionAreaCargoService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosAprobacionAreaCargoService);
    private readonly IFlujosAprobacionAreaCargoRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosAprobacionAreaCargoService(IFlujosAprobacionAreaCargoRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>> GetAllAsync(short numFlujo, int? codArea = null)
    {
        try { var d = (await _repo.ShowallAsync(numFlujo, codArea)).ToList();
              return ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosAprobacionAreaCargo>> GetByIdAsync(short numFlujo, int codArea, int codCargo)
    {
        try { var d = await _repo.ShowbyidAsync(numFlujo, codArea, codCargo);
              return d is null ? ApiResponse<FlujosAprobacionAreaCargo>.Fail("No encontrado.") : ApiResponse<FlujosAprobacionAreaCargo>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosAprobacionAreaCargo>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>> SearchAsync(FlujosAprobacionAreaCargoSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>.Fail(ex.Message); }
    }

    /// <summary>El monto máximo debe superar al mínimo. Se valida acá además de en la
    /// pantalla: el diálogo no es el único camino a este servicio, y una regla de negocio que
    /// solo vive en la UI se puede saltear.</summary>
    private static string? ValidarMontos(decimal? minimo, decimal? maximo)
    {
        if (minimo is null || maximo is null) return null;   // sin tope: nada que comparar
        return maximo <= minimo
            ? "El monto máximo debe ser mayor que el monto mínimo."
            : null;
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosAprobacionAreaCargoInsertDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = ValidarMontos(dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(FlujosAprobacionAreaCargoUpdateDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var error = ValidarMontos(dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Registro actualizado.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(short numFlujo, int codArea, int codCargo)
    {
        try { var r = await _repo.DeleteAsync(numFlujo, codArea, codCargo);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> CopyAsync(FlujosAprobacionAreaCargoCopyDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            if (dto.NUM_FLUJO_APROBACION == dto.NUM_FLUJO_APROBACION_DESTINO)
                return ApiResponse<WriteResult>.Fail("El flujo de destino debe ser distinto al de origen.");

            var r = await _repo.CopyAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Niveles copiados.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CopyAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}


// ── Detalle: Tipos de Operación ──────────────────────────────────────────────
public class FlujosAprobacionTiposOperacionService : IFlujosAprobacionTiposOperacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FlujosAprobacionTiposOperacionService);
    private readonly IFlujosAprobacionTiposOperacionRepository _repo;
    private readonly ErrorLogService _log;
    public FlujosAprobacionTiposOperacionService(IFlujosAprobacionTiposOperacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>> GetAllAsync(short numFlujo, string? codClaseOperacion = null)
    {
        try { var d = (await _repo.ShowallAsync(numFlujo, codClaseOperacion)).ToList();
              return ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FlujosAprobacionTiposOperacion>> GetByIdAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion)
    {
        try { var d = await _repo.ShowbyidAsync(numFlujo, codClaseOperacion, codTipoOperacion);
              return d is null ? ApiResponse<FlujosAprobacionTiposOperacion>.Fail("No encontrado.") : ApiResponse<FlujosAprobacionTiposOperacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<FlujosAprobacionTiposOperacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>> SearchAsync(FlujosAprobacionTiposOperacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(FlujosAprobacionTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion)
    {
        try { var r = await _repo.DeleteAsync(numFlujo, codClaseOperacion, codTipoOperacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
