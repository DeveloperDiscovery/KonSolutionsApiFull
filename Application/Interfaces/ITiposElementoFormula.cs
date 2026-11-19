using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposElementoFormulaRepository
{
    Task<IEnumerable<TiposElementoFormula>> ShowallAsync();
    Task<TiposElementoFormula?> ShowbyidAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposElementoFormula>> SearchAsync(TiposElementoFormulaSearchDto dto);
    Task<WriteResult> InsertAsync(TiposElementoFormulaInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposElementoFormulaUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA);
}

public interface ITiposElementoFormulaService
{
    Task<ApiResponse<IEnumerable<TiposElementoFormula>>> GetAllAsync();
    Task<ApiResponse<TiposElementoFormula>> GetByIdAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposElementoFormula>>> SearchAsync(TiposElementoFormulaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposElementoFormulaInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposElementoFormulaUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_CLASE_ELEMENTO_FORMULA, int COD_TIPO_ELEMENTO_FORMULA);
}