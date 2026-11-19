using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionContableRepository
{
    Task<IEnumerable<ConfiguracionContable>> ShowallAsync();
    Task<ConfiguracionContable?> ShowbyidAsync(int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConfiguracionContable>> SearchAsync(ConfiguracionContableSearchDto dto);
    Task<WriteResult> InsertAsync(ConfiguracionContableInsertDto dto);
    Task<WriteResult> UpdateAsync(ConfiguracionContableUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_SECUENCIA);
}

public interface IConfiguracionContableService
{
    Task<ApiResponse<IEnumerable<ConfiguracionContable>>> GetAllAsync();
    Task<ApiResponse<ConfiguracionContable>> GetByIdAsync(int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConfiguracionContable>>> SearchAsync(ConfiguracionContableSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionContableInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionContableUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SECUENCIA);
}