using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITareasRepository
{
    Task<IEnumerable<Tareas>> ShowallAsync();
    Task<Tareas?> ShowbyidAsync(int COD_TAREA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Tareas>> SearchAsync(TareasSearchDto dto);
    Task<WriteResult> InsertAsync(TareasInsertDto dto);
    Task<WriteResult> UpdateAsync(TareasUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TAREA);
}

public interface ITareasService
{
    Task<ApiResponse<IEnumerable<Tareas>>> GetAllAsync();
    Task<ApiResponse<Tareas>> GetByIdAsync(int COD_TAREA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Tareas>>> SearchAsync(TareasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TareasInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TareasUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TAREA);
}