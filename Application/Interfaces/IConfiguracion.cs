using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;

namespace KonSolutionsApi.Application.Interfaces;

public interface IConfiguracionRepository
{
    Task<IEnumerable<Configuracion>> ShowallAsync();
    Task<Configuracion?> ShowbyidAsync(int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Configuracion>> SearchAsync(ConfiguracionSearchDto dto);
    Task<WriteResult> InsertAsync(ConfiguracionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ConfiguracionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SECUENCIA);
}

public interface IConfiguracionService
{
    Task<ApiResponse<IEnumerable<Configuracion>>> GetAllAsync();
    Task<ApiResponse<Configuracion>> GetByIdAsync(int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Configuracion>>> SearchAsync(ConfiguracionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ConfiguracionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SECUENCIA);
}