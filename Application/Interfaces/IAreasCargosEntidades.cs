using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;

namespace KonSolutionsApi.Application.Interfaces;

public interface IAreasCargosEntidadesRepository
{
    Task<IEnumerable<AreasCargosEntidades>> ShowallAsync(int codArea, int codCargo);
    /// <summary>Personas del centro de costo, alcanzadas por su área.</summary>
    Task<IEnumerable<ComboboxItem>> ShowcomboxPorCentroCostoAsync(string codCentroCosto);
    Task<AreasCargosEntidades?> ShowbyidAsync(int codArea, int codCargo, decimal codEntidad);
    Task<IEnumerable<AreasCargosEntidades>> SearchAsync(AreasCargosEntidadesSearchDto dto);
    Task<WriteResult> InsertAsync(AreasCargosEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int codArea, int codCargo, decimal codEntidad);
    // Árbol completo Área > Cargo > Entidad asignada (sin parámetros).
    Task<IEnumerable<AreasCargosEntidadesNodo>> GetTreeviewAsync();
    // Personas activas aún no asignadas a ningún área/cargo, para el diálogo "Asignar".
    Task<IEnumerable<EntidadDisponible>> GetEntidadesDisponiblesAsync();
    // Personas asignadas al Área (y niveles jerárquicos) enlazada a una Etiqueta de Búsqueda
    // (PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES, resuelve el Área internamente vía
    // ESTANDAR.ETIQUETAS_BUSQUEDA_AREAS).
    Task<IEnumerable<AreaCargoEntidadPorOpcion>> GetPorEtiquetaAsync(string codEtiquetaBusqueda);
}

public interface IAreasCargosEntidadesService
{
    Task<ApiResponse<IEnumerable<AreasCargosEntidades>>> GetAllAsync(int codArea, int codCargo);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxPorCentroCostoAsync(string codCentroCosto);
    Task<ApiResponse<AreasCargosEntidades>> GetByIdAsync(int codArea, int codCargo, decimal codEntidad);
    Task<ApiResponse<IEnumerable<AreasCargosEntidades>>> SearchAsync(AreasCargosEntidadesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(AreasCargosEntidadesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int codArea, int codCargo, decimal codEntidad);
    Task<ApiResponse<IEnumerable<AreasCargosEntidadesNodo>>> GetTreeviewAsync();
    Task<ApiResponse<IEnumerable<EntidadDisponible>>> GetEntidadesDisponiblesAsync();
    Task<ApiResponse<IEnumerable<AreaCargoEntidadPorOpcion>>> GetPorEtiquetaAsync(string codEtiquetaBusqueda);
}
