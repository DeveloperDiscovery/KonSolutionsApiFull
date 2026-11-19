using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class ContenedoresAlmacenajeCasillerosService : IContenedoresAlmacenajeCasillerosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ContenedoresAlmacenajeCasillerosService);
    private readonly IContenedoresAlmacenajeCasillerosRepository _repo;
    private readonly ErrorLogService _log;
    public ContenedoresAlmacenajeCasillerosService(IContenedoresAlmacenajeCasillerosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>> GetAllAsync(string COD_CONTENEDOR)
    {
        try { var d = (await _repo.ShowallAsync(COD_CONTENEDOR)).ToList();
              return ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ContenedoresAlmacenajeCasilleros>> GetByIdAsync(string COD_CONTENEDOR, string COD_CASILLERO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_CONTENEDOR, COD_CASILLERO);
              return d is null ? ApiResponse<ContenedoresAlmacenajeCasilleros>.Fail("No encontrado.") : ApiResponse<ContenedoresAlmacenajeCasilleros>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ContenedoresAlmacenajeCasilleros>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_CONTENEDOR)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_CONTENEDOR)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>> SearchAsync(ContenedoresAlmacenajeCasillerosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ContenedoresAlmacenajeCasilleros>> BuscarPorUbicacionAsync(string codAlmacen, string codUbicacionAlmacen)
    {
        try { var d = await _repo.BuscarPorUbicacionAsync(codAlmacen, codUbicacionAlmacen);
              return d is null ? ApiResponse<ContenedoresAlmacenajeCasilleros>.Fail("No encontrado.") : ApiResponse<ContenedoresAlmacenajeCasilleros>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(BuscarPorUbicacionAsync));
              return ApiResponse<ContenedoresAlmacenajeCasilleros>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(ContenedoresAlmacenajeCasillerosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CONTENEDOR, string COD_CASILLERO)
    {
        try { var r = await _repo.DeleteAsync(COD_CONTENEDOR, COD_CASILLERO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> GenerarAsync(ContenedoresAlmacenajeCasillerosGenerarDto dto, string codUsuario, string codEstacion)
    {
        // Validación acá y no solo en la UI: el generador borra antes de crear, y una matriz
        // en cero dejaría el contenedor sin casilleros sin decir por qué.
        if (dto.NUM_FILAS <= 0 || dto.NUM_COLUMNAS <= 0)
            return ApiResponse<WriteResult>.Fail("Filas y columnas deben ser mayores a cero.");

        try { var r = await _repo.GenerarAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Casilleros generados."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GenerarAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
