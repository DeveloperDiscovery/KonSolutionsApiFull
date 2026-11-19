using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosRepository
{
    Task<IEnumerable<Disenos>> ShowallAsync();
    Task<Disenos?> ShowbyidAsync(string COD_DISENO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Disenos>> SearchAsync(DisenosSearchDto dto);
    Task<WriteResult> InsertAsync(DisenosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_DISENO);
    Task<DisenosLoadCombosResult> LoadcombosAsync();
}

public interface IDisenosService
{
    Task<ApiResponse<IEnumerable<Disenos>>> GetAllAsync();
    Task<ApiResponse<Disenos>> GetByIdAsync(string COD_DISENO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Disenos>>> SearchAsync(DisenosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(DisenosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_DISENO);
    Task<ApiResponse<DisenosLoadCombosResult>> GetLoadcombosAsync();
}