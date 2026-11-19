using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPathFilesRepository
{
    Task<IEnumerable<PathFiles>> ShowallAsync();
    Task<PathFiles?> ShowbyidAsync(string COD_KEY_PATHFILE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<PathFiles>> SearchAsync(PathFilesSearchDto dto);
    Task<WriteResult> InsertAsync(PathFilesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_KEY_PATHFILE);
}

public interface IPathFilesService
{
    Task<ApiResponse<IEnumerable<PathFiles>>> GetAllAsync();
    Task<ApiResponse<PathFiles>> GetByIdAsync(string COD_KEY_PATHFILE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<PathFiles>>> SearchAsync(PathFilesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PathFilesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_KEY_PATHFILE);
}
