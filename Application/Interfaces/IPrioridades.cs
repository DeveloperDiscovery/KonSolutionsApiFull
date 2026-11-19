using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Solicitudes;

namespace KonSolutionsApi.Application.Interfaces;

public interface IPrioridadesRepository
{
    Task<IEnumerable<Prioridades>> ShowallAsync();
    Task<Prioridades?> ShowbyidAsync(string COD_PRIORIDAD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Prioridades>> SearchAsync(PrioridadesSearchDto dto);
    Task<WriteResult> InsertAsync(PrioridadesInsertDto dto);
    Task<WriteResult> UpdateAsync(PrioridadesUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_PRIORIDAD);
}

public interface IPrioridadesService
{
    Task<ApiResponse<IEnumerable<Prioridades>>> GetAllAsync();
    Task<ApiResponse<Prioridades>> GetByIdAsync(string COD_PRIORIDAD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Prioridades>>> SearchAsync(PrioridadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(PrioridadesInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(PrioridadesUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRIORIDAD);
}