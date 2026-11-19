using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposComprobantesPagoRepository
{
    Task<IEnumerable<TiposComprobantesPago>> ShowallAsync();
    Task<TiposComprobantesPago?> ShowbyidAsync(string COD_TIPO_COMPROBANTE_PAGO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposComprobantesPago>> SearchAsync(TiposComprobantesPagoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposComprobantesPagoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposComprobantesPagoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_COMPROBANTE_PAGO);
}

public interface ITiposComprobantesPagoService
{
    Task<ApiResponse<IEnumerable<TiposComprobantesPago>>> GetAllAsync();
    Task<ApiResponse<TiposComprobantesPago>> GetByIdAsync(string COD_TIPO_COMPROBANTE_PAGO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposComprobantesPago>>> SearchAsync(TiposComprobantesPagoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposComprobantesPagoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposComprobantesPagoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_COMPROBANTE_PAGO);
}