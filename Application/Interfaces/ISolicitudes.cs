using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Solicitudes;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesRepository
{
    Task<IEnumerable<Solicitudes>> ShowallAsync();
    Task<Solicitudes?> ShowbyidAsync(int NUM_SOLICITUD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<Solicitudes>> SearchAsync(SolicitudesSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(SolicitudesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD);
}

public interface ISolicitudesService
{
    Task<ApiResponse<IEnumerable<Solicitudes>>> GetAllAsync();
    Task<ApiResponse<Solicitudes>> GetByIdAsync(int NUM_SOLICITUD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<Solicitudes>>> SearchAsync(SolicitudesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(SolicitudesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD);
}