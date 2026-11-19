using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAnosRepository
{
    Task<IEnumerable<Anos>> ShowallAsync();
    Task<Anos?> ShowbyidAsync(int NUM_ANO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(AnosInsertDto dto);
    Task<WriteResult> DeleteAsync(int NUM_ANO);
}

public interface IAnosService
{
    Task<ApiResponse<IEnumerable<Anos>>> GetAllAsync();
    Task<ApiResponse<Anos>> GetByIdAsync(int NUM_ANO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(AnosInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_ANO);
}
