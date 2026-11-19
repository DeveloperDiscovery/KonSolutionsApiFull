using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionGeneralRepository
{
    Task<IEnumerable<ConfiguracionGeneral>> ShowallAsync(string? desNombreEmpresa = null);
    Task<ConfiguracionGeneral?> ShowbyidAsync(string DES_NOMBRE_EMPRESA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConfiguracionGeneral>> SearchAsync(ConfiguracionGeneralSearchDto dto);
    Task<WriteResult> SaveAsync(ConfiguracionGeneralSaveDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> InsertAsync(ConfiguracionGeneralInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string DES_NOMBRE_EMPRESA);
    Task<ConfiguracionGeneralLoadCombosResult> LoadcombosAsync();
    Task<(string? Usuario, string? Clave, string? Ruc)> CredencialesVcdpAsync();
}

public interface IConfiguracionGeneralService
{
    Task<ApiResponse<IEnumerable<ConfiguracionGeneral>>> GetAllAsync();
    Task<ApiResponse<ConfiguracionGeneralPublicDto>> GetPublicAsync();
    Task<ApiResponse<ConfiguracionGeneral>> GetByIdAsync(string DES_NOMBRE_EMPRESA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConfiguracionGeneral>>> SearchAsync(ConfiguracionGeneralSearchDto dto);
    Task<ApiResponse<WriteResult>> SaveAsync(ConfiguracionGeneralSaveDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string DES_NOMBRE_EMPRESA);
    Task<ApiResponse<ConfiguracionGeneralLoadCombosResult>> GetLoadcombosAsync();
    /// <summary>Usuario y clave SOL en claro, solo para uso interno (validar comprobantes en SUNAT).</summary>
    Task<(string? Usuario, string? Clave, string? Error)> CredencialesVcdpAsync();
}

/// <summary>Combos independientes que la pantalla "Configuración General" necesita al abrir:
/// Monedas, Tipos de Configuración, Grupos de Producto y Tipos de Valor de Cálculo — antes
/// llamadas separadas, ahora 1 sola vía PROC_ESTANDAR_CONFIGURACION_GENERAL_LOADCOMBOS.</summary>
public class ConfiguracionGeneralLoadCombosResult
{
    public List<ComboboxItem> Monedas { get; set; } = new();
    public List<ComboboxItem> TiposConfiguracion { get; set; } = new();
    public List<ComboboxItem> GruposProducto { get; set; } = new();
    public List<ComboboxItem> TiposValorCalculo { get; set; } = new();
    public List<ComboboxItem> ClasesOperacion { get; set; } = new();
    public List<ComboboxItem> ClasesMovimiento { get; set; } = new();
}
