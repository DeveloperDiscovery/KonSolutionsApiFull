using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITurnosTrabajoRepository
{
    Task<IEnumerable<TurnosTrabajo>> ShowallAsync();
    Task<TurnosTrabajo?> ShowbyidAsync(string COD_TURNO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TurnosTrabajo>> SearchAsync(TurnosTrabajoSearchDto dto);
    Task<WriteResult> InsertAsync(TurnosTrabajoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(TurnosTrabajoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TURNO);
}

public interface ITurnosTrabajoService
{
    Task<ApiResponse<IEnumerable<TurnosTrabajo>>> GetAllAsync();
    Task<ApiResponse<TurnosTrabajo>> GetByIdAsync(string COD_TURNO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TurnosTrabajo>>> SearchAsync(TurnosTrabajoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TurnosTrabajoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(TurnosTrabajoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TURNO);
}