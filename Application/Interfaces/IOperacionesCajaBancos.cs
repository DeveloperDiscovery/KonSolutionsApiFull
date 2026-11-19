using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOperacionesCajaBancosRepository
{
    Task<IEnumerable<OperacionesCajaBancos>> ShowallAsync();
    Task<OperacionesCajaBancos?> ShowbyidAsync(decimal NUM_OPERACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<OperacionesCajaBancos>> SearchAsync(OperacionesCajaBancosSearchDto dto);
    Task<WriteResult> InsertAsync(OperacionesCajaBancosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OperacionesCajaBancosUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_OPERACION);
}

public interface IOperacionesCajaBancosService
{
    Task<ApiResponse<IEnumerable<OperacionesCajaBancos>>> GetAllAsync();
    Task<ApiResponse<OperacionesCajaBancos>> GetByIdAsync(decimal NUM_OPERACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<OperacionesCajaBancos>>> SearchAsync(OperacionesCajaBancosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OperacionesCajaBancosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OperacionesCajaBancosUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_OPERACION);
}