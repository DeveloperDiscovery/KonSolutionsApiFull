using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISaldosContablesRepository
{
    Task<IEnumerable<SaldosContables>> ShowallAsync();
    Task<SaldosContables?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SaldosContables>> SearchAsync(SaldosContablesSearchDto dto);
    Task<WriteResult> InsertAsync(SaldosContablesInsertDto dto);
    Task<WriteResult> UpdateAsync(SaldosContablesUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA);
}

public interface ISaldosContablesService
{
    Task<ApiResponse<IEnumerable<SaldosContables>>> GetAllAsync();
    Task<ApiResponse<SaldosContables>> GetByIdAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SaldosContables>>> SearchAsync(SaldosContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SaldosContablesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(SaldosContablesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_CUENTA_CONTABLE, string COD_MONEDA);
}