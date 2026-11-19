using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOperacionesCajaBancosDetalleRepository
{
    Task<IEnumerable<OperacionesCajaBancosDetalle>> ShowallAsync();
    Task<OperacionesCajaBancosDetalle?> ShowbyidAsync(decimal NUM_OPERACION, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<OperacionesCajaBancosDetalle>> SearchAsync(OperacionesCajaBancosDetalleSearchDto dto);
    Task<WriteResult> InsertAsync(OperacionesCajaBancosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OperacionesCajaBancosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA);
}

public interface IOperacionesCajaBancosDetalleService
{
    Task<ApiResponse<IEnumerable<OperacionesCajaBancosDetalle>>> GetAllAsync();
    Task<ApiResponse<OperacionesCajaBancosDetalle>> GetByIdAsync(decimal NUM_OPERACION, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<OperacionesCajaBancosDetalle>>> SearchAsync(OperacionesCajaBancosDetalleSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OperacionesCajaBancosDetalleInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OperacionesCajaBancosDetalleUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA);
}