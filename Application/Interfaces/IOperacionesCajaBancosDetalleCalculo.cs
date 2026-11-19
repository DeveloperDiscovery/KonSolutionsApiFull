using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;

namespace KonSolutionsApi.Application.Interfaces;

public interface IOperacionesCajaBancosDetalleCalculoRepository
{
    Task<IEnumerable<OperacionesCajaBancosDetalleCalculo>> ShowallAsync();
    Task<OperacionesCajaBancosDetalleCalculo?> ShowbyidAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<OperacionesCajaBancosDetalleCalculo>> SearchAsync(OperacionesCajaBancosDetalleCalculoSearchDto dto);
    Task<WriteResult> InsertAsync(OperacionesCajaBancosDetalleCalculoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(OperacionesCajaBancosDetalleCalculoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO);
}

public interface IOperacionesCajaBancosDetalleCalculoService
{
    Task<ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>> GetAllAsync();
    Task<ApiResponse<OperacionesCajaBancosDetalleCalculo>> GetByIdAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<OperacionesCajaBancosDetalleCalculo>>> SearchAsync(OperacionesCajaBancosDetalleCalculoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(OperacionesCajaBancosDetalleCalculoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(OperacionesCajaBancosDetalleCalculoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal NUM_OPERACION, int NUM_SECUENCIA, string COD_TIPO_VALOR_CALCULO);
}