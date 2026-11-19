using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOrdenesProduccionUnidadesPedidosComercialesRepository
{
    Task<IEnumerable<OrdenesProduccionUnidadesPedidosComerciales>> ShowallAsync();
    Task<OrdenesProduccionUnidadesPedidosComerciales?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<OrdenesProduccionUnidadesPedidosComerciales>> SearchAsync(OrdenesProduccionUnidadesPedidosComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(OrdenesProduccionUnidadesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OrdenesProduccionUnidadesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}

public interface IOrdenesProduccionUnidadesPedidosComercialesService
{
    Task<ApiResponse<IEnumerable<OrdenesProduccionUnidadesPedidosComerciales>>> GetAllAsync();
    Task<ApiResponse<OrdenesProduccionUnidadesPedidosComerciales>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<OrdenesProduccionUnidadesPedidosComerciales>>> SearchAsync(OrdenesProduccionUnidadesPedidosComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OrdenesProduccionUnidadesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OrdenesProduccionUnidadesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}