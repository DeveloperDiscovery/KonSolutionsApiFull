using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoDetalleRepository
{
    Task<IEnumerable<ComprobantesPagoDetalle>> ShowallAsync();
    Task<ComprobantesPagoDetalle?> ShowbyidAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoDetalle>> SearchAsync(ComprobantesPagoDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ComprobantesPagoDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
}

public interface IComprobantesPagoDetalleService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoDetalle>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoDetalle>> GetByIdAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoDetalle>>> SearchAsync(ComprobantesPagoDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ComprobantesPagoDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA);
}