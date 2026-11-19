using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class ConfiguracionGeneralService : IConfiguracionGeneralService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase    = nameof(ConfiguracionGeneralService);
    private readonly IConfiguracionGeneralRepository _repo;
    private readonly ErrorLogService _log;
    public ConfiguracionGeneralService(IConfiguracionGeneralRepository repo, ErrorLogService log)
    { _repo = repo; _log = log; }

    public async Task<ApiResponse<IEnumerable<ConfiguracionGeneral>>> GetAllAsync()
    {
        try { var d = (await _repo.ShowallAsync()).ToList();
              return ApiResponse<IEnumerable<ConfiguracionGeneral>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetAllAsync));
              return ApiResponse<IEnumerable<ConfiguracionGeneral>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ConfiguracionGeneralPublicDto>> GetPublicAsync()
    {
        try
        {
            var d = (await _repo.ShowallAsync()).FirstOrDefault();
            return d is null
                ? ApiResponse<ConfiguracionGeneralPublicDto>.Fail("No configurado.")
                : ApiResponse<ConfiguracionGeneralPublicDto>.Ok(ConfiguracionGeneralPublicDto.De(d));
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetPublicAsync));
              return ApiResponse<ConfiguracionGeneralPublicDto>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ConfiguracionGeneral>> GetByIdAsync(string DES_NOMBRE_EMPRESA)
    {
        try { var d = await _repo.ShowbyidAsync(DES_NOMBRE_EMPRESA);
              return d is null ? ApiResponse<ConfiguracionGeneral>.Fail("No encontrado.")
                               : ApiResponse<ConfiguracionGeneral>.Ok(d); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetByIdAsync));
              return ApiResponse<ConfiguracionGeneral>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync()
    {
        try { return ApiResponse<IEnumerable<ComboboxItem>>.Ok(await _repo.ShowcomboxAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetComboboxAsync));
              return ApiResponse<IEnumerable<ComboboxItem>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<ConfiguracionGeneral>>> SearchAsync(ConfiguracionGeneralSearchDto dto)
    {
        try { var d = (await _repo.SearchAsync(dto)).ToList();
              return ApiResponse<IEnumerable<ConfiguracionGeneral>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SearchAsync));
              return ApiResponse<IEnumerable<ConfiguracionGeneral>>.Fail(ex.Message); }
    }

    /// <summary>Guarda la configuración, con la clave SOL incluida, en una sola llamada a PROC_ESTANDAR_CONFIGURACION_GENERAL_INSERT: la clave nueva (tipo COD_PASSWORD) se cifra en SQL y se guarda en ENC_PASSWORD_VCDP
    /// en la misma transacción; vacía no toca la que ya está; con FLG_QUITAR_PASSWORD_VCDP se borra. No hace falta ninguna variable de entorno.</summary>
    public async Task<ApiResponse<WriteResult>> SaveAsync(ConfiguracionGeneralSaveDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var r = await _repo.SaveAsync(dto, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Configuración guardada.");
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(SaveAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    /// <summary>El usuario y la clave SOL en claro (los descifra el procedimiento), para validar comprobantes en SUNAT (no sale por ningún punto de la API). Error = por qué no se puede (sin usuario o sin clave).</summary>
    public async Task<(string? Usuario, string? Clave, string? Error)> CredencialesVcdpAsync()
    {
        try
        {
            var (usuario, clave, _) = await _repo.CredencialesVcdpAsync();
            if (string.IsNullOrWhiteSpace(usuario)) return (null, null, "Falta el usuario SOL en Configuración general, pestaña Compras.");
            if (string.IsNullOrEmpty(clave)) return (usuario, null, "Falta la clave SOL en Configuración general, pestaña Compras.");
            return (usuario, clave, null);
        }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CredencialesVcdpAsync)); return (null, null, "No se pudo leer la clave SOL: " + ex.Message); }
    }

    public async Task<ApiResponse<WriteResult>> DeleteAsync(string DES_NOMBRE_EMPRESA)
    {
        try { var r = await _repo.DeleteAsync(DES_NOMBRE_EMPRESA);
              return ApiResponse<WriteResult>.FromWriteResult(r, "Registro eliminado."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(DeleteAsync));
              return ApiResponse<WriteResult>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<ConfiguracionGeneralLoadCombosResult>> GetLoadcombosAsync()
    {
        try { return ApiResponse<ConfiguracionGeneralLoadCombosResult>.Ok(await _repo.LoadcombosAsync()); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(GetLoadcombosAsync));
              return ApiResponse<ConfiguracionGeneralLoadCombosResult>.Fail(ex.Message); }
    }
}
