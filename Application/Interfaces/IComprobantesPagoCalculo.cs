using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoCalculoRepository
{
    Task<IEnumerable<ComprobantesPagoCalculo>> ShowallAsync();
    Task<ComprobantesPagoCalculo?> ShowbyidAsync(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoCalculo>> SearchAsync(ComprobantesPagoCalculoSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoCalculoInsertDto dto);
    Task<WriteResult> UpdateAsync(ComprobantesPagoCalculoUpdateDto dto);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO);
}

public interface IComprobantesPagoCalculoService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoCalculo>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoCalculo>> GetByIdAsync(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoCalculo>>> SearchAsync(ComprobantesPagoCalculoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoCalculoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComprobantesPagoCalculoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, string COD_TIPO_VALOR_CALCULO);
}