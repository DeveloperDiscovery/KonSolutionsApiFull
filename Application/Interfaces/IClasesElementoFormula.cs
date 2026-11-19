using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IClasesElementoFormulaRepository
{
    Task<IEnumerable<ClasesElementoFormula>> ShowallAsync();
    Task<ClasesElementoFormula?> ShowbyidAsync(int COD_CLASE_ELEMENTO_FORMULA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ClasesElementoFormula>> SearchAsync(ClasesElementoFormulaSearchDto dto);
    Task<WriteResult> InsertAsync(ClasesElementoFormulaInsertDto dto);
    Task<WriteResult> UpdateAsync(ClasesElementoFormulaUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA);
}

public interface IClasesElementoFormulaService
{
    Task<ApiResponse<IEnumerable<ClasesElementoFormula>>> GetAllAsync();
    Task<ApiResponse<ClasesElementoFormula>> GetByIdAsync(int COD_CLASE_ELEMENTO_FORMULA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ClasesElementoFormula>>> SearchAsync(ClasesElementoFormulaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ClasesElementoFormulaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ClasesElementoFormulaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA);
}