using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionEmisionElectronicaRepository
{
    /// <summary>La fila única, con las columnas cifradas. Null si se borró.</summary>
    Task<ConfiguracionEmisionElectronicaRegistro?> ShowbyidAsync();
    /// <summary>Graba. En los campos cifrados y en los del certificado, null significa "no cambiar lo que hay".</summary>
    Task<WriteResult> UpdateAsync(ConfiguracionEmisionElectronicaRegistro nuevo, string codUsuario, string codEstacion);
}

public interface IConfiguracionEmisionElectronicaService
{
    /// <summary>Lo que ve la pantalla: sin secretos.</summary>
    Task<ApiResponse<ConfiguracionEmisionElectronica>> GetAsync();
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionEmisionElectronicaUpdateDto dto, string codUsuario, string codEstacion);
    /// <summary>Las credenciales descifradas, para el cliente de SUNAT. No tiene endpoint: sólo se usa dentro de la API.</summary>
    Task<ApiResponse<CredencialesEmisionElectronica>> ObtenerCredencialesAsync();
    /// <summary>Un resumen para el validador previo.</summary>
    Task<EmisionConfigEstado> EstadoAsync();
}
