using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

public class EstructuraEspecificacionTecnicaService : IEstructuraEspecificacionTecnicaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EstructuraEspecificacionTecnicaService);
    private readonly IEstructuraEspecificacionTecnicaRepository _repo;
    private readonly ErrorLogService _log;
    public EstructuraEspecificacionTecnicaService(IEstructuraEspecificacionTecnicaRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EstructuraEspecificacionTecnica>> GetByIdAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<EstructuraEspecificacionTecnica>.Fail("No encontrado.") : ApiResponse<EstructuraEspecificacionTecnica>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EstructuraEspecificacionTecnica>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>> SearchAsync(EstructuraEspecificacionTecnicaSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EstructuraEspecificacionTecnica>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EstructuraEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}