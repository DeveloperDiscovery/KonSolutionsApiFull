using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Services;

public class EntidadesInformacionComercialService : IEntidadesInformacionComercialService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(EntidadesInformacionComercialService);
    private readonly IEntidadesInformacionComercialRepository _repo;
    private readonly ErrorLogService _log;
    public EntidadesInformacionComercialService(IEntidadesInformacionComercialRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<EntidadesInformacionComercial>>> GetAllAsync(decimal codEntidad, string codClaseOperacion)
    {
        try { var d = (await _repo.ShowallAsync(codEntidad, codClaseOperacion)).ToList();
              return ApiResponse<IEnumerable<EntidadesInformacionComercial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<EntidadesInformacionComercial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<EntidadesInformacionComercial>> GetByIdAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion)
    {
        try { var d = await _repo.ShowbyidAsync(codEntidad, codClaseOperacion, codTipoOperacion);
              return d is null ? ApiResponse<EntidadesInformacionComercial>.Fail("No encontrado.") : ApiResponse<EntidadesInformacionComercial>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<EntidadesInformacionComercial>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<EntidadesInformacionComercial>>> SearchAsync(EntidadesInformacionComercialSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<EntidadesInformacionComercial>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<EntidadesInformacionComercial>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(EntidadesInformacionComercialInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro creado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesInformacionComercialUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion)
    {
        try { var r = await _repo.DeleteAsync(codEntidad, codClaseOperacion, codTipoOperacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }
}
