using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Services;

public class TiposContratoBeneficiosLaboralesService : ITiposContratoBeneficiosLaboralesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TiposContratoBeneficiosLaboralesService);
    private readonly ITiposContratoBeneficiosLaboralesRepository _repo;
    private readonly ErrorLogService _log;
    public TiposContratoBeneficiosLaboralesService(ITiposContratoBeneficiosLaboralesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposContratoBeneficiosLaborales>> GetByIdAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL);
              return d is null ? ApiResponse<TiposContratoBeneficiosLaborales>.Fail("No encontrado.") : ApiResponse<TiposContratoBeneficiosLaborales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposContratoBeneficiosLaborales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>> SearchAsync(TiposContratoBeneficiosLaboralesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposContratoBeneficiosLaborales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposContratoBeneficiosLaboralesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL)
    {
        try { var r = await _repo.DeleteAsync(COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}