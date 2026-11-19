using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService : IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService);
    private readonly IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService(IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>> GetByIdAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ENTIDAD, NUM_PURCHASE_ORDER, NUM_SECUENCIA);
              return d is null ? ApiResponse<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA)
    {
        try { var r = await _repo.DeleteAsync(COD_ENTIDAD, NUM_PURCHASE_ORDER, NUM_SECUENCIA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}