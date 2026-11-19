using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IGuiasRemisionSeriesRepository
{
    Task<IEnumerable<GuiaRemisionSerie>> ShowallAsync();
    Task<GuiaRemisionSerie?> ShowbyidAsync(string COD_SERIE);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<WriteResult> InsertAsync(GuiaRemisionSerieInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(GuiaRemisionSerieUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_SERIE);
}

public interface IGuiasRemisionSeriesService
{
    Task<ApiResponse<IEnumerable<GuiaRemisionSerie>>> GetAllAsync();
    Task<ApiResponse<GuiaRemisionSerie>> GetByIdAsync(string COD_SERIE);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<WriteResult>> InsertAsync(GuiaRemisionSerieInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(GuiaRemisionSerieUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_SERIE);
}

public interface IGuiasRemisionRepository
{
    Task<IEnumerable<GuiaRemision>> ShowallAsync(GuiaRemisionFiltroDto filtro);
    Task<GuiaRemision?> ShowbyidAsync(int NUM_GUIA_REMISION);
    /// <summary>La guía vigente del movimiento; si no hay, la última (anulada). Null si nunca tuvo.</summary>
    Task<GuiaRemision?> ShowbymovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<IEnumerable<GuiaRemisionEnvio>> EnviosAsync(int NUM_GUIA_REMISION);
    Task<GuiaRemisionArchivo?> XmlAsync(int NUM_GUIA_REMISION);
    Task<GuiaRemisionArchivo?> CdrAsync(int NUM_GUIA_REMISION);

    // Las operaciones de la emisión. Los SP rechazan lo que no corresponde (estado equivocado, movimiento con guía vigente,
    // sin serie...) con un THROW de la serie 505xx: el SqlException sube tal cual y lo traduce el servicio.
    Task<GuiaRemision> CrearAsync(GuiaRemisionCrearDto dto, string codUsuario, string codEstacion);
    Task RegistrarXmlAsync(GuiaRemisionXmlDto dto, string codUsuario, string codEstacion);
    Task RegistrarEnvioAsync(GuiaRemisionEnvioDto dto, string codUsuario, string codEstacion);
    Task RegistrarRespuestaAsync(GuiaRemisionRespuestaDto dto, string codUsuario, string codEstacion);
    Task<GuiaRemision> ReintentarAsync(GuiaRemisionReintentarDto dto, string codUsuario, string codEstacion);
    /// <summary>Las opciones de la emisión de la guía (M1/L, transbordo, documento relacionado). Siempre trae una fila si la guía existe; null si no existe.</summary>
    Task<GuiaRemisionOpciones?> OpcionesAsync(int NUM_GUIA_REMISION);
    /// <summary>Reemplaza las opciones. Sólo en una guía PENDIENTE (el SP lo exige).</summary>
    Task GuardarOpcionesAsync(int NUM_GUIA_REMISION, GuiaRemisionOpcionesDto dto, string codUsuario, string codEstacion);

    /// <summary>Las filas de PROC_LOGISTICA_REPORTE_MOVIMIENTOS para un movimiento, tal como salen: por nombre de columna.
    /// Se leen sin un modelo fijo a propósito: el SP es del usuario y puede cambiar; el validador avisa qué columna falta.</summary>
    Task<IReadOnlyList<IReadOnlyDictionary<string, object?>>> ReporteMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    /// <summary>Las series con las que se puede emitir la guía de ese movimiento.</summary>
    Task<IReadOnlyList<string>> SeriesParaMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    /// <summary>El mensaje de bloqueo si el movimiento tiene una guía vigente ENVIADA o ACEPTADA (ya no se modifica); null si se puede modificar.</summary>
    Task<string?> BloqueoMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    /// <summary>Los registros de los catálogos que usan las salidas y a los que les falta algo que la guía necesita.</summary>
    Task<IEnumerable<GuiaRemisionCatalogoPendiente>> CatalogosPendientesAsync();
}

public interface IGuiasRemisionService
{
    Task<ApiResponse<IEnumerable<GuiaRemision>>> GetAllAsync(GuiaRemisionFiltroDto filtro);
    Task<ApiResponse<GuiaRemision>> GetByIdAsync(int NUM_GUIA_REMISION);
    Task<ApiResponse<GuiaRemision>> GetByMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<IEnumerable<GuiaRemisionEnvio>>> GetEnviosAsync(int NUM_GUIA_REMISION);
    Task<ApiResponse<GuiaRemisionArchivo>> GetXmlAsync(int NUM_GUIA_REMISION);
    Task<ApiResponse<GuiaRemisionArchivo>> GetCdrAsync(int NUM_GUIA_REMISION);
    /// <summary>Lo mismo que GetEnvios/GetXml/GetCdr pero pidiendo la guía por su movimiento (la vigente; si no hay, la última).
    /// Es lo que usa la pestaña "Guía de remisión" del movimiento: quien ve el movimiento no tiene por qué poder recorrer todas las guías.</summary>
    Task<ApiResponse<IEnumerable<GuiaRemisionEnvio>>> GetEnviosPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<GuiaRemisionArchivo>> GetXmlPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    Task<ApiResponse<GuiaRemisionArchivo>> GetCdrPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    /// <summary>El validador previo: revisa si el movimiento está en condiciones de emitir su guía. <paramref name="trasladoVehiculoM1L"/>:
    /// se revisa como traslado en vehículos M1 o L (SUNAT no exige vehículo ni conductor), que es una opción de la emisión.</summary>
    Task<ApiResponse<GuiaValidacionResultado>> ValidarAsync(decimal NUM_MOVIMIENTO_ALMACEN, bool trasladoVehiculoM1L = false);
    /// <summary>Si el movimiento ya no se puede modificar porque su guía está ENVIADA o ACEPTADA.</summary>
    Task<ApiResponse<GuiaRemisionBloqueo>> GetBloqueoPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
    /// <summary>El chequeo de catálogos: qué le falta a lo que usan las salidas para poder emitir sus guías.</summary>
    Task<ApiResponse<IEnumerable<GuiaRemisionCatalogoPendiente>>> GetCatalogosPendientesAsync();

    Task<ApiResponse<GuiaRemision>> CrearAsync(GuiaRemisionCrearDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> RegistrarXmlAsync(GuiaRemisionXmlDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> RegistrarEnvioAsync(GuiaRemisionEnvioDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> RegistrarRespuestaAsync(GuiaRemisionRespuestaDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<GuiaRemision>> ReintentarAsync(GuiaRemisionReintentarDto dto, string codUsuario, string codEstacion);
}

/// <summary>La emisión de la guía de remisión electrónica: validar, crear, armar y firmar el XML, mandarlo a SUNAT y anotar su respuesta.
/// Una respuesta de SUNAT (aceptada, rechazada o en proceso) es un resultado y vuelve con Success = true; Success = false es no haber podido
/// emitir (movimiento sin condiciones, sin conexión con SUNAT, etc.) y el mensaje dice cómo quedó la guía.</summary>
public interface IGuiasRemisionEmisionService
{
    /// <summary>Crea la guía del movimiento y la emite. Si el movimiento ya tiene una guía PENDIENTE, sigue con ella (mismo número); si está ENVIADA, consulta.</summary>
    Task<ApiResponse<GuiaRemisionEmisionResultado>> EmitirAsync(GuiaRemisionEmitirDto dto, string codUsuario, string codEstacion, CancellationToken ct = default);
    /// <summary>Pregunta a SUNAT por una guía ENVIADA y anota la respuesta.</summary>
    Task<ApiResponse<GuiaRemisionEmisionResultado>> ConsultarAsync(int NUM_GUIA_REMISION, string codUsuario, string codEstacion, CancellationToken ct = default);
    Task<ApiResponse<GuiaRemisionEmisionResultado>> ConsultarPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN, string codUsuario, string codEstacion, CancellationToken ct = default);
    /// <summary>Vuelve a emitir una guía RECHAZADA, con el mismo número y los datos del movimiento como están ahora. Las opciones nuevas son opcionales.</summary>
    Task<ApiResponse<GuiaRemisionEmisionResultado>> ReintentarAsync(int NUM_GUIA_REMISION, GuiaRemisionOpcionesDto? opciones, string codUsuario, string codEstacion, CancellationToken ct = default);
    Task<ApiResponse<GuiaRemisionEmisionResultado>> ReintentarPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN, GuiaRemisionOpcionesDto? opciones, string codUsuario, string codEstacion, CancellationToken ct = default);
    /// <summary>Las opciones de emisión guardadas de la guía (M1/L, transbordo, documento relacionado).</summary>
    Task<ApiResponse<GuiaRemisionOpciones>> GetOpcionesAsync(int NUM_GUIA_REMISION);
    Task<ApiResponse<GuiaRemisionOpciones>> GetOpcionesPorMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN);
}
