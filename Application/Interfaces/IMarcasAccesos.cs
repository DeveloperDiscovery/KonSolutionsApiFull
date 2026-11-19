using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMarcasAccesosRepository
{
    Task<IEnumerable<MarcasAccesos>> ShowallAsync();
    Task<MarcasAccesos?> ShowbyidAsync(int NUM_READ_TAG);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<MarcasAccesos>> SearchAsync(MarcasAccesosSearchDto dto);
    Task<WriteResult> InsertAsync(MarcasAccesosInsertDto dto);
    Task<WriteResult> UpdateAsync(MarcasAccesosUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_READ_TAG);
}

public interface IMarcasAccesosService
{
    Task<ApiResponse<IEnumerable<MarcasAccesos>>> GetAllAsync();
    Task<ApiResponse<MarcasAccesos>> GetByIdAsync(int NUM_READ_TAG);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<MarcasAccesos>>> SearchAsync(MarcasAccesosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(MarcasAccesosInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(MarcasAccesosUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_READ_TAG);
}