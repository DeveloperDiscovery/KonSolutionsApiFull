using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionRrhhNominaRepository
{
    Task<IEnumerable<ConfiguracionRrhhNomina>> ShowallAsync();
    Task<ConfiguracionRrhhNomina?> ShowbyidAsync(int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ConfiguracionRrhhNomina>> SearchAsync(ConfiguracionRrhhNominaSearchDto dto);
    Task<WriteResult> InsertAsync(ConfiguracionRrhhNominaInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ConfiguracionRrhhNominaUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SECUENCIA);
}

public interface IConfiguracionRrhhNominaService
{
    Task<ApiResponse<IEnumerable<ConfiguracionRrhhNomina>>> GetAllAsync();
    Task<ApiResponse<ConfiguracionRrhhNomina>> GetByIdAsync(int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ConfiguracionRrhhNomina>>> SearchAsync(ConfiguracionRrhhNominaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionRrhhNominaInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionRrhhNominaUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SECUENCIA);
}