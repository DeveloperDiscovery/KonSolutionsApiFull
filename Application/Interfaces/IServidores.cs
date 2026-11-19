using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IServidoresRepository
{
    Task<IEnumerable<Servidores>> ShowallAsync();
    Task<Servidores?> ShowbyidAsync(string COD_KEY_SERVIDOR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Servidores>> SearchAsync(ServidoresSearchDto dto);
    Task<WriteResult> InsertAsync(ServidoresInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_KEY_SERVIDOR);
}

public interface IServidoresService
{
    Task<ApiResponse<IEnumerable<Servidores>>> GetAllAsync();
    Task<ApiResponse<Servidores>> GetByIdAsync(string COD_KEY_SERVIDOR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Servidores>>> SearchAsync(ServidoresSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ServidoresInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_KEY_SERVIDOR);
}
