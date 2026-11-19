using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Aprobaciones;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAprobacionesRepository
{
    Task<IEnumerable<Aprobaciones>> ShowallAsync();
    Task<Aprobaciones?> ShowbyidAsync(int NUM_APROBACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Aprobaciones>> SearchAsync(AprobacionesSearchDto dto);
    Task<WriteResult> InsertAsync(AprobacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(AprobacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_APROBACION);
}

public interface IAprobacionesService
{
    Task<ApiResponse<IEnumerable<Aprobaciones>>> GetAllAsync();
    Task<ApiResponse<Aprobaciones>> GetByIdAsync(int NUM_APROBACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Aprobaciones>>> SearchAsync(AprobacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AprobacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(AprobacionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_APROBACION);
}