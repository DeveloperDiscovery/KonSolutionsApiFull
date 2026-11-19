using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class TiposSolicitudComercialAreasCargosService : ITiposSolicitudComercialAreasCargosService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(TiposSolicitudComercialAreasCargosService);

    private readonly ITiposSolicitudComercialAreasCargosRepository _repo;
    private readonly ErrorLogService _log;

    public TiposSolicitudComercialAreasCargosService(ITiposSolicitudComercialAreasCargosRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>> GetAllAsync(string codTipoSolicitudComercial, int codArea)
    {
        try { var d = (await _repo.ShowallAsync(codTipoSolicitudComercial, codArea)).ToList();
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposSolicitudComercialAreasCargos>> GetByIdAsync(string codTipoSolicitudComercial, int codArea, int codCargo)
    {
        try { var d = await _repo.ShowbyidAsync(codTipoSolicitudComercial, codArea, codCargo);
              return d is null ? ApiResponse<TiposSolicitudComercialAreasCargos>.Fail("No encontrado.") : ApiResponse<TiposSolicitudComercialAreasCargos>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<TiposSolicitudComercialAreasCargos>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>> SearchAsync(TiposSolicitudComercialAreasCargosSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<TiposSolicitudComercialAreasCargos>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudComercialAreasCargosInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Cargo asignado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoSolicitudComercial, int codArea, int codCargo)
    {
        try { var r = await _repo.DeleteAsync(codTipoSolicitudComercial, codArea, codCargo);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Cargo quitado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<TiposSolicitudComercialAreasCargosLoadCombosResult>> LoadcombosAsync()
    {
        try { return ApiResponse<TiposSolicitudComercialAreasCargosLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(LoadcombosAsync));
              return ApiResponse<TiposSolicitudComercialAreasCargosLoadCombosResult>.Fail(ex.Message); }
    }
}
