using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoRepository
{
    Task<IEnumerable<ComprobantesPago>> ShowallAsync();
    Task<ComprobantesPago?> ShowbyidAsync(decimal NUM_REGISTRO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPago>> SearchAsync(ComprobantesPagoSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ComprobantesPagoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO);
}

public interface IComprobantesPagoService
{
    Task<ApiResponse<IEnumerable<ComprobantesPago>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPago>> GetByIdAsync(decimal NUM_REGISTRO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPago>>> SearchAsync(ComprobantesPagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComprobantesPagoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO);
}