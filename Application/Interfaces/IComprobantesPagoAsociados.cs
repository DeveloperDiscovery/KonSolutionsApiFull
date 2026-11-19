using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoAsociadosRepository
{
    Task<IEnumerable<ComprobantesPagoAsociados>> ShowallAsync();
    Task<ComprobantesPagoAsociados?> ShowbyidAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ASOCIADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoAsociados>> SearchAsync(ComprobantesPagoAsociadosSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoAsociadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ASOCIADO);
}

public interface IComprobantesPagoAsociadosService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoAsociados>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoAsociados>> GetByIdAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ASOCIADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoAsociados>>> SearchAsync(ComprobantesPagoAsociadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoAsociadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ASOCIADO);
}