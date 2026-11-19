using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuarioAsientosContablesService : IRolesUsuarioAsientosContablesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuarioAsientosContablesService);
    private readonly IRolesUsuarioAsientosContablesRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuarioAsientosContablesService(IRolesUsuarioAsientosContablesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>> GetAllAsync(string COD_ROL_USUARIO)
    {
        try { var d = (await _repo.ShowallAsync(COD_ROL_USUARIO)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>> GetDisponiblesAsync(string COD_ROL_USUARIO)
    {
        try { var d = (await _repo.DisponiblesAsync(COD_ROL_USUARIO)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDisponiblesAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuarioAsientosContables>> GetByIdAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_ASIENTO_CONTABLE);
              return d is null ? ApiResponse<RolesUsuarioAsientosContables>.Fail("No encontrado.") : ApiResponse<RolesUsuarioAsientosContables>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuarioAsientosContables>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_ROL_USUARIO)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>> SearchAsync(RolesUsuarioAsientosContablesSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAsientosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Asiento contable asignado al rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE)
    {
        try { var r = await _repo.DeleteAsync(COD_ROL_USUARIO, COD_ASIENTO_CONTABLE);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Asiento contable quitado del rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuarioAsientosContablesLoteResult>> AsignarAsync(RolesUsuarioAsientosContablesLoteDto dto, string codUsuario, string codEstacion)
    {
        var res = new RolesUsuarioAsientosContablesLoteResult();
        foreach (var asi in dto.Asientos.Distinct())
        {
            try
            {
                await _repo.InsertAsync(new RolesUsuarioAsientosContablesInsertDto
                {
                    COD_ROL_USUARIO = dto.COD_ROL_USUARIO,
                    COD_ASIENTO_CONTABLE = asi
                }, codUsuario, codEstacion);
                res.Procesados++;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(AsignarAsync));
                res.Fallidos.Add(asi);
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioAsientosContablesLoteResult>.Ok(res);
    }

    public async Task<ApiResponse<RolesUsuarioAsientosContablesLoteResult>> DesasignarAsync(RolesUsuarioAsientosContablesLoteDto dto)
    {
        var res = new RolesUsuarioAsientosContablesLoteResult();
        foreach (var asi in dto.Asientos.Distinct())
        {
            try { await _repo.DeleteAsync(dto.COD_ROL_USUARIO, asi); res.Procesados++; }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(DesasignarAsync));
                res.Fallidos.Add(asi);
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioAsientosContablesLoteResult>.Ok(res);
    }
}
