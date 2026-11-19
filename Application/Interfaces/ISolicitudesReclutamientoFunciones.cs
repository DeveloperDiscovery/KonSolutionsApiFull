using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;

namespace KonSolutionsApi.Application.Interfaces;

public interface ISolicitudesReclutamientoFuncionesRepository
{
    Task<IEnumerable<SolicitudesReclutamientoFunciones>> ShowallAsync();
    Task<SolicitudesReclutamientoFunciones?> ShowbyidAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<SolicitudesReclutamientoFunciones>> SearchAsync(SolicitudesReclutamientoFuncionesSearchDto dto);
    Task<WriteResult> InsertAsync(SolicitudesReclutamientoFuncionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION);
}

public interface ISolicitudesReclutamientoFuncionesService
{
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>> GetAllAsync();
    Task<ApiResponse<SolicitudesReclutamientoFunciones>> GetByIdAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<SolicitudesReclutamientoFunciones>>> SearchAsync(SolicitudesReclutamientoFuncionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(SolicitudesReclutamientoFuncionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_SOLICITUD_RECLUTAMIENTO, string COD_FUNCION);
}