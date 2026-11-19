using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICondicionesPagoRepository
{
    Task<IEnumerable<CondicionesPago>> ShowallAsync();
    Task<CondicionesPago?> ShowbyidAsync(int COD_CONDICION_PAGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CondicionesPago>> SearchAsync(CondicionesPagoSearchDto dto);
    Task<WriteResult> InsertAsync(CondicionesPagoInsertDto dto);
    Task<WriteResult> UpdateAsync(CondicionesPagoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_CONDICION_PAGO);
}

public interface ICondicionesPagoService
{
    Task<ApiResponse<IEnumerable<CondicionesPago>>> GetAllAsync();
    Task<ApiResponse<CondicionesPago>> GetByIdAsync(int COD_CONDICION_PAGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CondicionesPago>>> SearchAsync(CondicionesPagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CondicionesPagoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CondicionesPagoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CONDICION_PAGO);
}