using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionPedidosComercialesRepository
{
    Task<IEnumerable<ConfiguracionPedidosComerciales>> ShowallAsync();
    Task<ConfiguracionPedidosComerciales?> ShowbyidAsync(string COD_TIPO_ENTIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConfiguracionPedidosComerciales>> SearchAsync(ConfiguracionPedidosComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(ConfiguracionPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ConfiguracionPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ENTIDAD);
}

public interface IConfiguracionPedidosComercialesService
{
    Task<ApiResponse<IEnumerable<ConfiguracionPedidosComerciales>>> GetAllAsync();
    Task<ApiResponse<ConfiguracionPedidosComerciales>> GetByIdAsync(string COD_TIPO_ENTIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConfiguracionPedidosComerciales>>> SearchAsync(ConfiguracionPedidosComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ENTIDAD);
}