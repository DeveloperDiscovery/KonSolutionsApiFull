using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IComprobantesPagoAplicadosRepository
{
    Task<IEnumerable<ComprobantesPagoAplicados>> ShowallAsync();
    Task<ComprobantesPagoAplicados?> ShowbyidAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ComprobantesPagoAplicados>> SearchAsync(ComprobantesPagoAplicadosSearchDto dto);
    Task<WriteResult> InsertAsync(ComprobantesPagoAplicadosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO);
}

public interface IComprobantesPagoAplicadosService
{
    Task<ApiResponse<IEnumerable<ComprobantesPagoAplicados>>> GetAllAsync();
    Task<ApiResponse<ComprobantesPagoAplicados>> GetByIdAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ComprobantesPagoAplicados>>> SearchAsync(ComprobantesPagoAplicadosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ComprobantesPagoAplicadosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA, decimal NUM_REGISTRO_ASOCIADO);
}