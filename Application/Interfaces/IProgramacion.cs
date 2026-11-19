using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionRepository
{
    Task<IEnumerable<Programacion>> ShowallAsync();
    Task<Programacion?> ShowbyidAsync(int NUM_PROGRAMA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Programacion>> SearchAsync(ProgramacionSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROGRAMA);
}

public interface IProgramacionService
{
    Task<ApiResponse<IEnumerable<Programacion>>> GetAllAsync();
    Task<ApiResponse<Programacion>> GetByIdAsync(int NUM_PROGRAMA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Programacion>>> SearchAsync(ProgramacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROGRAMA);
}