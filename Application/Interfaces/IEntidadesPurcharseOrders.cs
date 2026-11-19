using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesPurcharseOrdersRepository
{
    Task<IEnumerable<EntidadesPurcharseOrders>> ShowallAsync();
    Task<EntidadesPurcharseOrders?> ShowbyidAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<EntidadesPurcharseOrders>> SearchAsync(EntidadesPurcharseOrdersSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesPurcharseOrdersInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(EntidadesPurcharseOrdersUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER);
}

public interface IEntidadesPurcharseOrdersService
{
    Task<ApiResponse<IEnumerable<EntidadesPurcharseOrders>>> GetAllAsync();
    Task<ApiResponse<EntidadesPurcharseOrders>> GetByIdAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<EntidadesPurcharseOrders>>> SearchAsync(EntidadesPurcharseOrdersSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesPurcharseOrdersInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(EntidadesPurcharseOrdersUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER);
}