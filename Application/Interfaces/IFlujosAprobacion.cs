using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

// ── Maestro ──────────────────────────────────────────────────────────────────
public interface IFlujosAprobacionRepository
{
    Task<IEnumerable<FlujosAprobacion>> ShowallAsync();
    Task<FlujosAprobacion?> ShowbyidAsync(short NUM_FLUJO_APROBACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<FlujosAprobacion>> SearchAsync(FlujosAprobacionSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosAprobacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosAprobacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(short NUM_FLUJO_APROBACION);
}

public interface IFlujosAprobacionService
{
    Task<ApiResponse<IEnumerable<FlujosAprobacion>>> GetAllAsync();
    Task<ApiResponse<FlujosAprobacion>> GetByIdAsync(short NUM_FLUJO_APROBACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<FlujosAprobacion>>> SearchAsync(FlujosAprobacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosAprobacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosAprobacionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(short NUM_FLUJO_APROBACION);
}


// ── Detalle: Área / Cargo ────────────────────────────────────────────────────
public interface IFlujosAprobacionAreaCargoRepository
{
    /// <summary>codArea opcional: null = todas las áreas del flujo (la grilla editable las
    /// lista todas y elige el área dentro de cada fila).</summary>
    Task<IEnumerable<FlujosAprobacionAreaCargo>> ShowallAsync(short numFlujo, int? codArea = null);
    Task<FlujosAprobacionAreaCargo?> ShowbyidAsync(short numFlujo, int codArea, int codCargo);
    Task<IEnumerable<FlujosAprobacionAreaCargo>> SearchAsync(FlujosAprobacionAreaCargoSearchDto dto);
    Task<WriteResult> InsertAsync(FlujosAprobacionAreaCargoInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(FlujosAprobacionAreaCargoUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(short numFlujo, int codArea, int codCargo);
    Task<WriteResult> CopyAsync(FlujosAprobacionAreaCargoCopyDto dto, string codUsuario, string codEstacion);
}

public interface IFlujosAprobacionAreaCargoService
{
    Task<ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>> GetAllAsync(short numFlujo, int? codArea = null);
    Task<ApiResponse<FlujosAprobacionAreaCargo>> GetByIdAsync(short numFlujo, int codArea, int codCargo);
    Task<ApiResponse<IEnumerable<FlujosAprobacionAreaCargo>>> SearchAsync(FlujosAprobacionAreaCargoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosAprobacionAreaCargoInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(FlujosAprobacionAreaCargoUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(short numFlujo, int codArea, int codCargo);
    Task<ApiResponse<WriteResult>> CopyAsync(FlujosAprobacionAreaCargoCopyDto dto, string codUsuario, string codEstacion);
}


// ── Detalle: Tipos de Operación ──────────────────────────────────────────────
public interface IFlujosAprobacionTiposOperacionRepository
{
    /// <summary>codClaseOperacion opcional: null = todas las clases del flujo.</summary>
    Task<IEnumerable<FlujosAprobacionTiposOperacion>> ShowallAsync(short numFlujo, string? codClaseOperacion = null);
    Task<FlujosAprobacionTiposOperacion?> ShowbyidAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion);
    Task<IEnumerable<FlujosAprobacionTiposOperacion>> SearchAsync(FlujosAprobacionTiposOperacionSearchDto dto);
    /// <summary>Upsert: volver a asignar algo ya asignado es inofensivo (no hay _UPDATE).</summary>
    Task<WriteResult> InsertAsync(FlujosAprobacionTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion);
}

public interface IFlujosAprobacionTiposOperacionService
{
    Task<ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>> GetAllAsync(short numFlujo, string? codClaseOperacion = null);
    Task<ApiResponse<FlujosAprobacionTiposOperacion>> GetByIdAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion);
    Task<ApiResponse<IEnumerable<FlujosAprobacionTiposOperacion>>> SearchAsync(FlujosAprobacionTiposOperacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(FlujosAprobacionTiposOperacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion);
}
