using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesComercialesMemoriasRepository
{
    Task<IEnumerable<SolicitudesComercialesMemorias>> ShowallAsync(int numSolicitudComercial);
    Task<SolicitudesComercialesMemorias?> ShowbyidAsync(int numSolicitudComercial, short numSecuencia);
    Task<IEnumerable<SolicitudesComercialesMemorias>> SearchAsync(SolicitudesComercialesMemoriasSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesComercialesMemoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numSolicitudComercial, short numSecuencia);
}

public interface ISolicitudesComercialesMemoriasService
{
    Task<ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>> GetAllAsync(int numSolicitudComercial);
    Task<ApiResponse<SolicitudesComercialesMemorias>> GetByIdAsync(int numSolicitudComercial, short numSecuencia);
    Task<ApiResponse<IEnumerable<SolicitudesComercialesMemorias>>> SearchAsync(SolicitudesComercialesMemoriasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesComercialesMemoriasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numSolicitudComercial, short numSecuencia);
}
