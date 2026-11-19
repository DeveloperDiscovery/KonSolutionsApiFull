using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProgramacionVacacionesRepository
{
    Task<IEnumerable<ProgramacionVacaciones>> ShowallAsync();
    Task<ProgramacionVacaciones?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProgramacionVacaciones>> SearchAsync(ProgramacionVacacionesSearchDto dto);
    Task<WriteResult> InsertAsync(ProgramacionVacacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProgramacionVacacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}

public interface IProgramacionVacacionesService
{
    Task<ApiResponse<IEnumerable<ProgramacionVacaciones>>> GetAllAsync();
    Task<ApiResponse<ProgramacionVacaciones>> GetByIdAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProgramacionVacaciones>>> SearchAsync(ProgramacionVacacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProgramacionVacacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProgramacionVacacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, int NUM_SECUENCIA);
}