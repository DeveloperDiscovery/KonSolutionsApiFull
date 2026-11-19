using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IRolesUsuarioAsientosContablesRepository
{
    Task<IEnumerable<RolesUsuarioAsientosContables>> ShowallAsync(string COD_ROL_USUARIO);
    /// <summary>Asientos que el rol todavía no tiene, ya sin los anulados: el SP _DISPONIBLES
    /// excluye el alias de estado 'INA'.</summary>
    Task<IEnumerable<RolesUsuarioAsientosContables>> DisponiblesAsync(string COD_ROL_USUARIO);
    Task<RolesUsuarioAsientosContables?> ShowbyidAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ROL_USUARIO);
    Task<IEnumerable<RolesUsuarioAsientosContables>> SearchAsync(RolesUsuarioAsientosContablesSearchDto dto);
    Task<WriteResult> InsertAsync(RolesUsuarioAsientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE);
}

public interface IRolesUsuarioAsientosContablesService
{
    Task<ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>> GetAllAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>> GetDisponiblesAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<RolesUsuarioAsientosContables>> GetByIdAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_ROL_USUARIO);
    Task<ApiResponse<IEnumerable<RolesUsuarioAsientosContables>>> SearchAsync(RolesUsuarioAsientosContablesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RolesUsuarioAsientosContablesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_ROL_USUARIO, string COD_ASIENTO_CONTABLE);

    Task<ApiResponse<RolesUsuarioAsientosContablesLoteResult>> AsignarAsync(RolesUsuarioAsientosContablesLoteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RolesUsuarioAsientosContablesLoteResult>> DesasignarAsync(RolesUsuarioAsientosContablesLoteDto dto);
}
