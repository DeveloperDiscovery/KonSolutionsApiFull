using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;

namespace KonSolutionsApi.Application.Interfaces;

// ───────────────────────── Equipos ─────────────────────────

public interface IRfidEquiposRepository
{
    Task<IEnumerable<RfidEquipo>> ShowallAsync();
    Task<RfidEquipo?> ShowbyidAsync(string COD_PRODUCTO);
    Task<IEnumerable<RfidEquipoCombo>> ShowcomboxAsync();
    /// <summary>Los productos que pueden ser la estación donde corre el agente: los del grupo y, si se indican, la familia y la sub familia.</summary>
    Task<IEnumerable<ComboboxItem>> EstacionesComboAsync(string? codGrupoProducto, string? codFamiliaProducto, string? codSubFamiliaProducto);
}

public interface IRfidEquiposService
{
    Task<ApiResponse<IEnumerable<RfidEquipo>>> GetAllAsync();
    Task<ApiResponse<RfidEquipo>> GetByIdAsync(string COD_PRODUCTO);
    Task<ApiResponse<IEnumerable<RfidEquipoCombo>>> ComboAsync();
}

/// <summary>Lo que necesita la pestaña RFID de Configuración general para elegir la estación y el archivo de la plantilla.</summary>
public interface IRfidConfiguracionService
{
    Task<ApiResponse<IEnumerable<ComboboxItem>>> EstacionesAsync(string? codGrupoProducto, string? codFamiliaProducto, string? codSubFamiliaProducto);
    /// <summary>Los archivos que hay en la carpeta de una ruta de ENTORNO.PATHFILES (solo los nombres, sin subcarpetas).</summary>
    Task<ApiResponse<IEnumerable<ComboboxItem>>> ArchivosPlantillaAsync(string codKeyPathfile);
}

// ───────────────────────── Emisiones y etiquetas ─────────────────────────

public interface IRfidEmisionesRepository
{
    Task<IEnumerable<RfidEmision>> ShowallAsync();
    Task<RfidEmision?> ShowbyidAsync(int NUM_EMISION);
    Task<IEnumerable<RfidEmision>> SearchAsync(RfidEmisionSearchDto dto);
    Task<IEnumerable<RfidEmisionDetalle>> DetalleAsync(int NUM_EMISION);
    Task<IEnumerable<RfidEtiqueta>> EtiquetasAsync(int NUM_EMISION, int? NUM_ITEM, string? COD_ESTADO_ETIQUETA, int? TOP);
    Task<IEnumerable<RfidEtiqueta>> EtiquetasSearchAsync(RfidEtiquetaSearchDto dto);

    /// <summary>Crea la emisión y sus etiquetas pendientes. En Codigo queda el número de la emisión.</summary>
    Task<WriteResult> InsertAsync(RfidEmisionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(RfidEmisionUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> AnularAsync(int NUM_EMISION, RfidAnularDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_EMISION);

    /// <summary>Un chip entrega su TID. Si sale bien, <paramref name="enrolado"/> trae el código y el EPC que hay que grabar.</summary>
    Task<(WriteResult Resultado, RfidEnrolado? Enrolado)> EnrolarAsync(int NUM_EMISION, RfidEnrolarDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> GrabadaAsync(RfidGrabadaDto dto, string codUsuario);
    Task<(WriteResult Resultado, RfidEtiquetaAnulada? Anulada)> AnularEtiquetaAsync(int NUM_EMISION, int NUM_ITEM, int NUM_ETIQUETA, RfidEtiquetaAnularDto dto, string codUsuario, string codEstacion);

    Task<IEnumerable<RfidOrigenCompra>> OrigenComprasAsync(RfidOrigenCompraSearchDto dto);
    Task<IEnumerable<RfidOrigenCompraLinea>> OrigenComprasLineasAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<RfidOrigenStock>> OrigenStockAsync(RfidOrigenStockSearchDto dto);
}

public interface IRfidImpresionRepository
{
    Task<IEnumerable<RfidTrabajoImpresion>> SearchAsync(RfidTrabajoImpresionSearchDto dto);
    Task<RfidTrabajoImpresion?> ShowbyidAsync(int NUM_TRABAJO);
    Task<(WriteResult Resultado, RfidTrabajoCreado? Creado)> SolicitarAsync(RfidImprimirDto dto, string codUsuario, string codEstacion);
    Task<(WriteResult Resultado, RfidTrabajoEstado? Estado)> CancelarAsync(int NUM_TRABAJO, string codUsuario);
    /// <summary>El siguiente trabajo de la impresora (ya en proceso), o nulo si no hay.</summary>
    Task<(WriteResult Resultado, RfidTrabajoImpresion? Trabajo)> ReclamarAsync(RfidTrabajoReclamarDto dto, string codUsuario);
    Task<(WriteResult Resultado, RfidTrabajoEstado? Estado)> ProgresoAsync(int NUM_TRABAJO, RfidTrabajoProgresoDto dto, string codUsuario);
    Task<WriteResult> TerminarAsync(int NUM_TRABAJO, RfidTrabajoTerminarDto dto, string codUsuario);
}

public interface IRfidImpresionService
{
    Task<ApiResponse<IEnumerable<RfidTrabajoImpresion>>> SearchAsync(RfidTrabajoImpresionSearchDto dto);
    Task<ApiResponse<RfidTrabajoImpresion>> GetByIdAsync(int NUM_TRABAJO);
    Task<ApiResponse<RfidTrabajoCreado>> SolicitarAsync(RfidImprimirDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RfidTrabajoEstado>> CancelarAsync(int NUM_TRABAJO, string codUsuario);
    Task<ApiResponse<RfidTrabajoImpresion?>> ReclamarAsync(RfidTrabajoReclamarDto dto, string codUsuario);
    Task<ApiResponse<RfidTrabajoEstado>> ProgresoAsync(int NUM_TRABAJO, RfidTrabajoProgresoDto dto, string codUsuario);
    Task<ApiResponse<WriteResult>> TerminarAsync(int NUM_TRABAJO, RfidTrabajoTerminarDto dto, string codUsuario);
}

public interface IRfidEmisionesService
{
    Task<ApiResponse<IEnumerable<RfidEmision>>> GetAllAsync();
    Task<ApiResponse<RfidEmision>> GetByIdAsync(int NUM_EMISION);
    Task<ApiResponse<IEnumerable<RfidEmision>>> SearchAsync(RfidEmisionSearchDto dto);
    Task<ApiResponse<IEnumerable<RfidEmisionDetalle>>> DetalleAsync(int NUM_EMISION);
    Task<ApiResponse<IEnumerable<RfidEtiqueta>>> EtiquetasAsync(int NUM_EMISION, int? NUM_ITEM, string? COD_ESTADO_ETIQUETA, int? TOP);
    Task<ApiResponse<IEnumerable<RfidEtiqueta>>> EtiquetasSearchAsync(RfidEtiquetaSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(RfidEmisionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(RfidEmisionUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> AnularAsync(int NUM_EMISION, RfidAnularDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_EMISION);
    Task<ApiResponse<RfidEnrolado>> EnrolarAsync(int NUM_EMISION, RfidEnrolarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> GrabadaAsync(RfidGrabadaDto dto, string codUsuario);
    Task<ApiResponse<RfidEtiquetaAnulada>> AnularEtiquetaAsync(int NUM_EMISION, int NUM_ITEM, int NUM_ETIQUETA, RfidEtiquetaAnularDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<RfidOrigenCompra>>> OrigenComprasAsync(RfidOrigenCompraSearchDto dto);
    Task<ApiResponse<IEnumerable<RfidOrigenCompraLinea>>> OrigenComprasLineasAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<RfidOrigenStock>>> OrigenStockAsync(RfidOrigenStockSearchDto dto);
}
