using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUbigeosRepository
{
    Task<IEnumerable<Ubigeos>> ShowallAsync();
    Task<Ubigeos?> ShowbyidAsync(string COD_UBIGEO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null);
    Task<IEnumerable<Ubigeos>> SearchAsync(UbigeosSearchDto dto);
    Task<WriteResult> InsertAsync(UbigeosInsertDto dto);
    Task<WriteResult> UpdateAsync(UbigeosUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_UBIGEO);
    Task<WriteResult> UploadJsonAsync(string jsonArray);
}

public interface IUbigeosService
{
    Task<ApiResponse<IEnumerable<Ubigeos>>> GetAllAsync();
    Task<ApiResponse<Ubigeos>> GetByIdAsync(string COD_UBIGEO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string? search = null);
    Task<ApiResponse<IEnumerable<Ubigeos>>> SearchAsync(UbigeosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(UbigeosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(UbigeosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_UBIGEO);
    Task<ApiResponse<WriteResult>> UploadJsonAsync(string jsonArray);
}