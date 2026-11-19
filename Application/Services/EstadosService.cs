using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class EstadosService : IEstadosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EstadosService);
    private readonly IEstadosRepository _repo;
    private readonly ErrorLogService _log;
    public EstadosService(IEstadosRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<Estados>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<Estados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<Estados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Estados>>> GetByTipoAsync(int codTipoEstado)
    {
        try { var d = (await _repo.ShowByTipoAsync(codTipoEstado)).ToList();
              return ApiResponse<IEnumerable<Estados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByTipoAsync));
              return ApiResponse<IEnumerable<Estados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> ImportFromEstandarAsync(int codTipoEstado, int codEstadoEstandar, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.ImportFromEstandarAsync(codTipoEstado, codEstadoEstandar, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Estado importado desde estándar."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ImportFromEstandarAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<Estados>> GetByIdAsync(int COD_TIPO_ESTADO, int COD_ESTADO)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_ESTADO, COD_ESTADO);
              return d is null ? ApiResponse<Estados>.Fail("No encontrado.") : ApiResponse<Estados>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<Estados>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<Estados>>> SearchAsync(EstadosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<Estados>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<Estados>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EstadosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_ESTADO, int COD_ESTADO)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_ESTADO, COD_ESTADO);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}