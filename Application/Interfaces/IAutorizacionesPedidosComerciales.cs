using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAutorizacionesPedidosComercialesRepository
{
    Task<IEnumerable<AutorizacionesPedidosComerciales>> ShowallAsync();
    Task<AutorizacionesPedidosComerciales?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<AutorizacionesPedidosComerciales>> SearchAsync(AutorizacionesPedidosComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(AutorizacionesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AutorizacionesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA);
}

public interface IAutorizacionesPedidosComercialesService
{
    Task<ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>> GetAllAsync();
    Task<ApiResponse<AutorizacionesPedidosComerciales>> GetByIdAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<AutorizacionesPedidosComerciales>>> SearchAsync(AutorizacionesPedidosComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AutorizacionesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AutorizacionesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA);
}