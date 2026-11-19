using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

public class TransportesEspecificacionesTecnicasService : ITransportesEspecificacionesTecnicasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(TransportesEspecificacionesTecnicasService);
    private readonly ITransportesEspecificacionesTecnicasRepository _repo;
    private readonly ErrorLogService _log;
    public TransportesEspecificacionesTecnicasService(ITransportesEspecificacionesTecnicasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>> GetAllAsync(string COD_TRANSPORTE)
    {
        try { var d = (await _repo.ShowallAsync(COD_TRANSPORTE)).ToList();
              return ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TransportesEspecificacionesTecnicas>> GetByIdAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA)
    {
        try { var d = await _repo.ShowbyidAsync(COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA);
              return d is null ? ApiResponse<TransportesEspecificacionesTecnicas>.Fail("No encontrado.") : ApiResponse<TransportesEspecificacionesTecnicas>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TransportesEspecificacionesTecnicas>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_TRANSPORTE)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_TRANSPORTE)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>> SearchAsync(TransportesEspecificacionesTecnicasSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TransportesEspecificacionesTecnicas>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TransportesEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(TransportesEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA)
    {
        try { var r = await _repo.DeleteAsync(COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}