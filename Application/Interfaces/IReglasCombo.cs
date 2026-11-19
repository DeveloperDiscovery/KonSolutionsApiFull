using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IReglasComboRepository
{
    Task<IEnumerable<ReglasCombo>> ShowallAsync();
    Task<ReglasCombo?> ShowbyidAsync(string COD_REGLA_COMBOS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ReglasCombo>> SearchAsync(ReglasComboSearchDto dto);
    Task<WriteResult> InsertAsync(ReglasComboInsertDto dto);
    Task<WriteResult> UpdateAsync(ReglasComboUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_REGLA_COMBOS);
}

public interface IReglasComboService
{
    Task<ApiResponse<IEnumerable<ReglasCombo>>> GetAllAsync();
    Task<ApiResponse<ReglasCombo>> GetByIdAsync(string COD_REGLA_COMBOS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ReglasCombo>>> SearchAsync(ReglasComboSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ReglasComboInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ReglasComboUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_REGLA_COMBOS);
}