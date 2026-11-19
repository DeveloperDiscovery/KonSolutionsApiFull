using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionSafetyRepository
{
    Task<IEnumerable<ConfiguracionSafety>> ShowallAsync();
    Task<ConfiguracionSafety?> ShowbyidAsync(int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConfiguracionSafety>> SearchAsync(ConfiguracionSafetySearchDto dto);
    Task<WriteResult> InsertAsync(ConfiguracionSafetyInsertDto dto);
    Task<WriteResult> UpdateAsync(ConfiguracionSafetyUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_SECUENCIA);
}

public interface IConfiguracionSafetyService
{
    Task<ApiResponse<IEnumerable<ConfiguracionSafety>>> GetAllAsync();
    Task<ApiResponse<ConfiguracionSafety>> GetByIdAsync(int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConfiguracionSafety>>> SearchAsync(ConfiguracionSafetySearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionSafetyInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionSafetyUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SECUENCIA);
}