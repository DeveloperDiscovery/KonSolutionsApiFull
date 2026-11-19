using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Services;

public class GruposNetworkingAsesoresComerialesService : IGruposNetworkingAsesoresComerialesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(GruposNetworkingAsesoresComerialesService);
    private readonly IGruposNetworkingAsesoresComerialesRepository _repo;
    private readonly ErrorLogService _log;
    public GruposNetworkingAsesoresComerialesService(IGruposNetworkingAsesoresComerialesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<GruposNetworkingAsesoresComeriales>> GetByIdAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL)
    {
        try { var d = await _repo.ShowbyidAsync(NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL);
              return d is null ? ApiResponse<GruposNetworkingAsesoresComeriales>.Fail("No encontrado.") : ApiResponse<GruposNetworkingAsesoresComeriales>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<GruposNetworkingAsesoresComeriales>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>> SearchAsync(GruposNetworkingAsesoresComerialesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<GruposNetworkingAsesoresComeriales>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(GruposNetworkingAsesoresComerialesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro guardado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> UpdateAsync(GruposNetworkingAsesoresComerialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.UpdateAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro modificado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_GRUPO_NETWORKING, decimal COD_ENTIDAD_ASESOR_COMERCIAL)
    {
        try { var r = await _repo.DeleteAsync(NUM_GRUPO_NETWORKING, COD_ENTIDAD_ASESOR_COMERCIAL);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

}