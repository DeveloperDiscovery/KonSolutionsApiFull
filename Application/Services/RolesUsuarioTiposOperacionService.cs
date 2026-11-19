using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Services;

public class RolesUsuarioTiposOperacionService : IRolesUsuarioTiposOperacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(RolesUsuarioTiposOperacionService);
    private readonly IRolesUsuarioTiposOperacionRepository _repo;
    private readonly ErrorLogService _log;
    public RolesUsuarioTiposOperacionService(IRolesUsuarioTiposOperacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>> GetAllAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION)
    {
        try { var d = (await _repo.ShowallAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>> GetDisponiblesAsync(string COD_ROL_USUARIO)
    {
        try { var d = (await _repo.DisponiblesAsync(COD_ROL_USUARIO)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetDisponiblesAsync));
              return ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<RolesUsuarioTiposOperacion>> GetByIdAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        try { var d = await _repo.ShowbyidAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION);
              return d is null ? ApiResponse<RolesUsuarioTiposOperacion>.Fail("No encontrado.") : ApiResponse<RolesUsuarioTiposOperacion>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<RolesUsuarioTiposOperacion>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION)
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION)); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>> SearchAsync(RolesUsuarioTiposOperacionSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<RolesUsuarioTiposOperacion>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        try { var r = await _repo.InsertAsync(dto, codUsuario, codEstacion);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de operación asignado al rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(InsertAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        try { var r = await _repo.DeleteAsync(COD_ROL_USUARIO, COD_CLASE_OPERACION, COD_TIPO_OPERACION);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Tipo de operación quitado del rol."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    /// <summary>Lote de pares clase+tipo; un fallo no corta el resto.</summary>
    public async Task<ApiResponse<RolesUsuarioTiposOperacionLoteResult>> AsignarAsync(RolesUsuarioTiposOperacionLoteDto dto, string codUsuario, string codEstacion)
    {
        var res = new RolesUsuarioTiposOperacionLoteResult();
        foreach (var t in Unicos(dto))
        {
            try
            {
                await _repo.InsertAsync(new RolesUsuarioTiposOperacionInsertDto
                {
                    COD_ROL_USUARIO = dto.COD_ROL_USUARIO,
                    COD_CLASE_OPERACION = t.COD_CLASE_OPERACION,
                    COD_TIPO_OPERACION = t.COD_TIPO_OPERACION
                }, codUsuario, codEstacion);
                res.Procesados++;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(AsignarAsync));
                res.Fallidos.Add($"{t.COD_CLASE_OPERACION}/{t.COD_TIPO_OPERACION}");
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioTiposOperacionLoteResult>.Ok(res);
    }

    public async Task<ApiResponse<RolesUsuarioTiposOperacionLoteResult>> DesasignarAsync(RolesUsuarioTiposOperacionLoteDto dto)
    {
        var res = new RolesUsuarioTiposOperacionLoteResult();
        foreach (var t in Unicos(dto))
        {
            try { await _repo.DeleteAsync(dto.COD_ROL_USUARIO, t.COD_CLASE_OPERACION, t.COD_TIPO_OPERACION); res.Procesados++; }
            catch (Exception ex)
            {
                _log.LogError(ex, Programa, Clase, nameof(DesasignarAsync));
                res.Fallidos.Add($"{t.COD_CLASE_OPERACION}/{t.COD_TIPO_OPERACION}");
                res.DesError ??= ex.Message;
            }
        }
        return ApiResponse<RolesUsuarioTiposOperacionLoteResult>.Ok(res);
    }

    private static IEnumerable<RolesUsuarioTiposOperacionClave> Unicos(RolesUsuarioTiposOperacionLoteDto dto)
        => dto.Tipos.GroupBy(t => $"{t.COD_CLASE_OPERACION}|{t.COD_TIPO_OPERACION}").Select(g => g.First());
}
