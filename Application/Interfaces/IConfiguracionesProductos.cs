using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionesProductosRepository
{
    Task<IEnumerable<ConfiguracionesProductos>> ShowallAsync(string codTipoConfiguracion);
    Task<ConfiguracionesProductos?> ShowbyidAsync(string codTipoConfiguracion, short numSecuencia);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codTipoConfiguracion);
    Task<IEnumerable<ConfiguracionesProductos>> SearchAsync(string codTipoConfiguracion, string? search);
    Task<WriteResult> InsertAsync(ConfiguracionesProductosInsertDto dto);
    Task<WriteResult> UpdateAsync(ConfiguracionesProductosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string codTipoConfiguracion, short numSecuencia);
}

public interface IConfiguracionesProductosService
{
    Task<ApiResponse<IEnumerable<ConfiguracionesProductos>>> GetAllAsync(string codTipoConfiguracion);
    Task<ApiResponse<ConfiguracionesProductos>> GetByIdAsync(string codTipoConfiguracion, short numSecuencia);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codTipoConfiguracion);
    Task<ApiResponse<IEnumerable<ConfiguracionesProductos>>> SearchAsync(string codTipoConfiguracion, string? search);
    Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionesProductosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionesProductosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoConfiguracion, short numSecuencia);
}
