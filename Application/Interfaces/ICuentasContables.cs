using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICuentasContablesRepository
{
    Task<IEnumerable<CuentasContables>> ShowallAsync();
    Task<CuentasContables?> ShowbyidAsync(string COD_CUENTA_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CuentasContables>> SearchAsync(CuentasContablesSearchDto dto);
    Task<WriteResult> InsertAsync(CuentasContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(CuentasContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CUENTA_CONTABLE);
}

public interface ICuentasContablesService
{
    Task<ApiResponse<IEnumerable<CuentasContables>>> GetAllAsync();
    Task<ApiResponse<CuentasContables>> GetByIdAsync(string COD_CUENTA_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CuentasContables>>> SearchAsync(CuentasContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CuentasContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(CuentasContablesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CUENTA_CONTABLE);
}