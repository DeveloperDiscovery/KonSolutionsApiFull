using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoCronogramaCancelacionRepository
{
    Task<IEnumerable<ComprobantesPagoCronogramaCancelacion>> ShowallAsync();
    Task<ComprobantesPagoCronogramaCancelacion?> ShowbyidAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoCronogramaCancelacion>> SearchAsync(ComprobantesPagoCronogramaCancelacionSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoCronogramaCancelacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ComprobantesPagoCronogramaCancelacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA);
}

public interface IComprobantesPagoCronogramaCancelacionService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoCronogramaCancelacion>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoCronogramaCancelacion>> GetByIdAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoCronogramaCancelacion>>> SearchAsync(ComprobantesPagoCronogramaCancelacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoCronogramaCancelacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComprobantesPagoCronogramaCancelacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO, int NUM_SECUENCIA);
}