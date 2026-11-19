using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoCronogramaRepository
{
    Task<IEnumerable<ComprobantesPagoCronograma>> ShowallAsync();
    Task<ComprobantesPagoCronograma?> ShowbyidAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoCronograma>> SearchAsync(ComprobantesPagoCronogramaSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoCronogramaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ComprobantesPagoCronogramaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO);
}

public interface IComprobantesPagoCronogramaService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoCronograma>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoCronograma>> GetByIdAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoCronograma>>> SearchAsync(ComprobantesPagoCronogramaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoCronogramaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComprobantesPagoCronogramaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, DateTime FEC_VENCIMIENTO);
}