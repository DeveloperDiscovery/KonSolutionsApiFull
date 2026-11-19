using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuariosAplicacionesOpcionesRepository
{
    Task<IEnumerable<RolesUsuariosAplicacionesOpciones>> ShowallAsync();
    Task<IEnumerable<RolesUsuariosAplicacionesOpciones>> ShowByRolAplicacionAsync(string codRol, string codAplicacion);
    Task<RolesUsuariosAplicacionesOpciones?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<RolesUsuariosAplicacionesOpciones>> SearchAsync(RolesUsuariosAplicacionesOpcionesSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuariosAplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RolesUsuariosAplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<IEnumerable<NodoOpcion>> TreeviewAsignadasAsync(string codRolUsuario);
    Task<IEnumerable<NodoOpcion>> TreeviewDisponiblesAsync(string codRolUsuario);
}

public interface IRolesUsuariosAplicacionesOpcionesService
{
    Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>> GetAllAsync();
    Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>> GetByRolAplicacionAsync(string codRol, string codAplicacion);
    Task<ApiResponse<RolesUsuariosAplicacionesOpciones>> GetByIdAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<RolesUsuariosAplicacionesOpciones>>> SearchAsync(RolesUsuariosAplicacionesOpcionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuariosAplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RolesUsuariosAplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_APLICACION, string COD_OPCION_APLICACION);
    Task<ApiResponse<IEnumerable<NodoOpcion>>> TreeviewAsignadasAsync(string codRolUsuario);
    Task<ApiResponse<IEnumerable<NodoOpcion>>> TreeviewDisponiblesAsync(string codRolUsuario);
}