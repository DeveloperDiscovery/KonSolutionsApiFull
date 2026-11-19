using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposParentescoRepository
{
    Task<IEnumerable<TiposParentesco>> ShowallAsync();
    Task<TiposParentesco?> ShowbyidAsync(string COD_TIPO_PARENTESCO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposParentesco>> SearchAsync(TiposParentescoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposParentescoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposParentescoUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_PARENTESCO);
}

public interface ITiposParentescoService
{
    Task<ApiResponse<IEnumerable<TiposParentesco>>> GetAllAsync();
    Task<ApiResponse<TiposParentesco>> GetByIdAsync(string COD_TIPO_PARENTESCO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposParentesco>>> SearchAsync(TiposParentescoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposParentescoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposParentescoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_PARENTESCO);
}