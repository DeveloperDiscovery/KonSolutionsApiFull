using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Sunat;

namespace KonSolutionsApi.Application.Interfaces;

public interface ICubsoSunatRepository
{
    Task<IEnumerable<CubsoSunat>> ShowallAsync();
    Task<CubsoSunat?> ShowbyidAsync(string COD_CUBSO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<CubsoSunat>> SearchAsync(CubsoSunatSearchDto dto);
    Task<WriteResult> InsertAsync(CubsoSunatInsertDto dto);
    Task<WriteResult> UpdateAsync(CubsoSunatUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_CUBSO);
    Task<WriteResult> UploadJsonAsync(string jsonArray);
}

public interface ICubsoSunatService
{
    Task<ApiResponse<IEnumerable<CubsoSunat>>> GetAllAsync();
    Task<ApiResponse<CubsoSunat>> GetByIdAsync(string COD_CUBSO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<CubsoSunat>>> SearchAsync(CubsoSunatSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(CubsoSunatInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(CubsoSunatUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CUBSO);
    Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray);
}