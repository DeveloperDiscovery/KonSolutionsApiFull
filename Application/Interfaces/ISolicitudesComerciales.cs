using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesComercialesRepository
{
    Task<IEnumerable<SolicitudesComerciales>> ShowallAsync();
    Task<SolicitudesComerciales?> ShowbyidAsync(int numSolicitudComercial);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal? codEntidadCliente);
    Task<IEnumerable<SolicitudesComerciales>> SearchAsync(SolicitudesComercialesSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numSolicitudComercial, string codUsuario, string codEstacion);
    Task<WriteResult> AsignarResponsableAsync(SolicitudesComercialesAsignarResponsableDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> ReasignaAsesorAsync(SolicitudesComercialesReasignaAsesorDto dto, string codUsuario, string codEstacion);
    Task<SolicitudesComercialesLoadCombosResult> LoadcombosAsync();
}

public interface ISolicitudesComercialesService
{
    Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> GetAllAsync();
    Task<ApiResponse<SolicitudesComerciales>> GetByIdAsync(int numSolicitudComercial);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal? codEntidadCliente);
    Task<ApiResponse<IEnumerable<SolicitudesComerciales>>> SearchAsync(SolicitudesComercialesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesComercialesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numSolicitudComercial, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> AsignarResponsableAsync(SolicitudesComercialesAsignarResponsableDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> ReasignaAsesorAsync(SolicitudesComercialesReasignaAsesorDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<SolicitudesComercialesLoadCombosResult>> GetLoadcombosAsync();
}
