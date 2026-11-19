using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMesesRepository
{
    Task<IEnumerable<Meses>> ShowallAsync();
    Task<Meses?> ShowbyidAsync(int NUM_MES);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(MesesInsertDto dto);
    Task<WriteResult> DeleteAsync(int NUM_MES);
}

public interface IMesesService
{
    Task<ApiResponse<IEnumerable<Meses>>> GetAllAsync();
    Task<ApiResponse<Meses>> GetByIdAsync(int NUM_MES);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(MesesInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_MES);
}
