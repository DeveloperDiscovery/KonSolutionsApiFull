using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPaisesRepository
{
    Task<IEnumerable<Paises>> ShowallAsync();
    Task<Paises?> ShowbyidAsync(string COD_PAIS);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null);
    Task<IEnumerable<Paises>> SearchAsync(PaisesSearchDto dto);
    Task<WriteResult> InsertAsync(PaisesInsertDto dto);
    Task<WriteResult> UpdateAsync(PaisesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_PAIS);
    Task<WriteResult> UploadJsonAsync(string jsonArray);
}

public interface IPaisesService
{
    Task<ApiResponse<IEnumerable<Paises>>> GetAllAsync();
    Task<ApiResponse<Paises>> GetByIdAsync(string COD_PAIS);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string? search = null);
    Task<ApiResponse<IEnumerable<Paises>>> SearchAsync(PaisesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PaisesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PaisesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PAIS);
    Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray);
}