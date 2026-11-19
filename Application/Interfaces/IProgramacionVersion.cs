using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionVersionRepository
{
    Task<IEnumerable<ProgramacionVersion>> ShowallAsync();
    Task<ProgramacionVersion?> ShowbyidAsync(int NUM_PROGRAMA, int NUM_VERSION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionVersion>> SearchAsync(ProgramacionVersionSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionVersionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionVersionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_PROGRAMA, int NUM_VERSION);
}

public interface IProgramacionVersionService
{
    Task<ApiResponse<IEnumerable<ProgramacionVersion>>> GetAllAsync();
    Task<ApiResponse<ProgramacionVersion>> GetByIdAsync(int NUM_PROGRAMA, int NUM_VERSION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionVersion>>> SearchAsync(ProgramacionVersionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionVersionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionVersionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_PROGRAMA, int NUM_VERSION);
}