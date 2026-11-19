using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOrdenProduccionItemsRepository
{
    Task<IEnumerable<OrdenProduccionItems>> ShowallAsync();
    Task<OrdenProduccionItems?> ShowbyidAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComboboxItem>> ShowcomboxPorOrdenAsync(string codProceso, int numOrdenProduccion);
    Task<IEnumerable<OrdenProduccionItems>> SearchAsync(OrdenProduccionItemsSearchDto dto);
    Task<WriteResult> InsertAsync(OrdenProduccionItemsInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OrdenProduccionItemsUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}

public interface IOrdenProduccionItemsService
{
    Task<ApiResponse<IEnumerable<OrdenProduccionItems>>> GetAllAsync();
    Task<ApiResponse<OrdenProduccionItems>> GetByIdAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxPorOrdenAsync(string codProceso, int numOrdenProduccion);
    Task<ApiResponse<IEnumerable<OrdenProduccionItems>>> SearchAsync(OrdenProduccionItemsSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OrdenProduccionItemsInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OrdenProduccionItemsUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PROCESO, int NUM_ORDEN_PRODUCCION, int NUM_ORDEN_PRODUCCION_ITEM);
}