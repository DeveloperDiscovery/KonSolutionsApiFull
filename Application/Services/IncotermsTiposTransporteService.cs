using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class IncotermsTiposTransporteService : IIncotermsTiposTransporteService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(IncotermsTiposTransporteService);
    private readonly IIncotermsTiposTransporteRepository _repo;
    private readonly ErrorLogService _log;
    public IncotermsTiposTransporteService(IIncotermsTiposTransporteRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<IncotermsTiposTransporte>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<IncotermsTiposTransporte>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<IncotermsTiposTransporte>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IncotermsTiposTransporte>> GetByIdAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE)
    {
        try { var d = await _repo.ShowbyidAsync(COD_INCOTERM, COD_TIPO_TRANSPORTE);
              return d is null ? ApiResponse<IncotermsTiposTransporte>.Fail("No encontrado.") : ApiResponse<IncotermsTiposTransporte>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<IncotermsTiposTransporte>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<IncotermsTiposTransporte>>> SearchAsync(IncotermsTiposTransporteSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<IncotermsTiposTransporte>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<IncotermsTiposTransporte>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(IncotermsTiposTransporteInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE)
    {
        try { var r = await _repo.DeleteAsync(COD_INCOTERM, COD_TIPO_TRANSPORTE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}