namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Los dos resultados de PROC_MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DETALLE_DISPONIBLES
/// en un solo objeto: qué se cubre por línea y de dónde saldría el stock.</summary>
public class MovimientoAlmacenRequerimientoCobertura
{
    public List<MovimientoAlmacenRequerimientoCoberturaLinea> Lineas { get; set; } = new();
    public List<MovimientoAlmacenRequerimientoCoberturaStock> Stock  { get; set; } = new();
}

/// <summary>Resultado 1: una fila por línea del requerimiento. El SP devuelve más columnas
/// (almacén, lote, orden de producción...); acá sólo se declaran las que muestra la
/// pantalla de cobertura, y Dapper ignora el resto.</summary>
public class MovimientoAlmacenRequerimientoCoberturaLinea
{
    public int      NUM_REQUERIMIENTO_STOCK      { get; set; }
    public int      NUM_SECUENCIA                { get; set; }
    public string?  COD_ALMACEN                  { get; set; }
    public string?  DES_ALMACEN                  { get; set; }
    public string?  COD_PRODUCTO                 { get; set; }
    public string?  DES_PRODUCTO                 { get; set; }
    public decimal  CAN_PRODUCTO                 { get; set; }
    public string?  COD_UNIDAD_MEDIDA            { get; set; }
    public string?  DES_UNIDAD_MEDIDA            { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA      { get; set; }
    public string?  COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public string?  DES_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal  CAN_ATENDER                  { get; set; }
    public decimal  CAN_PENDIENTE_ATENDER        { get; set; }

    /// <summary>POR REVISAR (sin stock seleccionado) · PARCIAL · COMPLETO. La resuelve el SP.</summary>
    public string?  DES_COBERTURA                { get; set; }
}

/// <summary>Resultado 2: el stock que se despacharía por línea, con la reserva de origen.
/// FLG_RESERVA distingue lo que sale de una reserva de lo que sale de stock libre.
/// NUM_ORDEN_SECUENCIA conserva el orden en que el SP fue consumiendo el stock.</summary>
public class MovimientoAlmacenRequerimientoCoberturaStock
{
    public int      NUM_SECUENCIA                { get; set; }
    public string?  COD_ALMACEN                  { get; set; }
    public string?  COD_UBICACION_ALMACEN        { get; set; }
    public string?  DES_UBICACION_ALMACEN        { get; set; }
    public string?  NUM_LOTE_CONTROL             { get; set; }
    public string?  DES_LOTE_CONTROL             { get; set; }
    public string?  COD_PROCESO                  { get; set; }
    public string?  DES_PROCESO                  { get; set; }
    public int?     NUM_ORDEN_PRODUCCION         { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_ITEM    { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_UNIDAD  { get; set; }
    public bool     FLG_RESERVA                  { get; set; }
    public decimal  CAN_PRODUCTO                 { get; set; }
    public decimal? CAN_PRODUCTO_SECUNDARIA      { get; set; }
    public int?     NUM_RESERVA_STOCK            { get; set; }
    public int?     NUM_SECUENCIA_RESERVA        { get; set; }
    public int      NUM_ORDEN_SECUENCIA          { get; set; }
}
