using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IFormasPagoRepository
{
    Task<IEnumerable<FormasPago>> ShowallAsync();
    Task<FormasPago?> ShowbyidAsync(int COD_FORMA_PAGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FormasPago>> SearchAsync(FormasPagoSearchDto dto);
    Task<WriteResult> InsertAsync(FormasPagoInsertDto dto);
    Task<WriteResult> UpdateAsync(FormasPagoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_FORMA_PAGO);
}

public interface IFormasPagoService
{
    Task<ApiResponse<IEnumerable<FormasPago>>> GetAllAsync();
    Task<ApiResponse<FormasPago>> GetByIdAsync(int COD_FORMA_PAGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FormasPago>>> SearchAsync(FormasPagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FormasPagoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(FormasPagoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_FORMA_PAGO);
}