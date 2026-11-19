using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesPersonalRepository
{
    Task<IEnumerable<SolicitudesPersonal>> ShowallAsync();
    Task<SolicitudesPersonal?> ShowbyidAsync(int NUM_SOLICITUD_PERSONAL);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesPersonal>> SearchAsync(SolicitudesPersonalSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_PERSONAL);
}

public interface ISolicitudesPersonalService
{
    Task<ApiResponse<IEnumerable<SolicitudesPersonal>>> GetAllAsync();
    Task<ApiResponse<SolicitudesPersonal>> GetByIdAsync(int NUM_SOLICITUD_PERSONAL);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesPersonal>>> SearchAsync(SolicitudesPersonalSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesPersonalInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesPersonalUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_PERSONAL);
}