using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad MOVIMIENTOS_ALMACEN - tipos equivalentes a la BD.</summary>
public class MovimientosAlmacen
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public DateTime? FEC_MOVIMENTO_ALMACEN { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string NUM_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_PROYECTO { get; set; }
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public int? NUM_REQUERIMIENTO_STOCK_ATENDIDO { get; set; }
    public int? NUM_RESERVA_STOCK_ATENDIDO { get; set; }
    public string COD_ALMACEN_SINCRONIZADO { get; set; } = string.Empty;
    public decimal? NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO { get; set; }
    // Estas cuatro columnas estaban mal y por eso el alta y la modificacion de la cabecera
    // fallaban: el SP pide COD_CLASE_OPERACION, COD_TIPO_OPERACION, MUM_PEDIDO_COMERCIAL y
    // NUM_REGISTRO, y el repositorio le mandaba en cambio MUM_PEDIDO_COMERCIAL_SINCRONIZADO
    // y NUM_REGISTRO_SINCRONIZADO, que no son parametros del procedimiento. El modelo venia
    // de una version anterior de la tabla.
    //
    // Van ocultas en los cuatro modos segun la matriz, pero tienen que viajar igual o se
    // pierden al modificar. MUM_ con M es como esta escrito en la tabla.
    //
    // NUM_REGISTRO no se declara aca: ya estaba mas abajo, entre las columnas que devuelven
    // los SELECT. Declararla dos veces no compila.
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public int? MUM_PEDIDO_COMERCIAL { get; set; }
    public decimal? COD_ENTIDAD_TRANSPORTISTA { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    /// <summary>Dirección y ubigeo de ORIGEN: los de la sucursal del almacén del movimiento.
    /// Los agrega el SHOWBYID; no son columnas de la tabla, así que sólo vienen al leer.</summary>
    public string? DES_DIRECCION_ORIGEN { get; set; }
    public string? DES_UBIGEO_ORIGEN { get; set; }

    /// <summary>La dirección de destino la RESUELVE el SHOWBYID: sucursal del almacén
    /// contrario cuando el movimiento es transferencia entre sucursales o una salida
    /// comercial, y dirección de la entidad en el resto de los casos. La pantalla la muestra,
    /// no la calcula.</summary>
    public string? DES_DIRECCION_DESTINO { get; set; }
    public string COD_UBIGEO_DESTINO { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    /// <summary>Anulables: la tabla los admite NULL y una fila vieja puede no tenerlos.</summary>
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_RESERVA_STOCK { get; set; } = string.Empty;
    public string DES_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string DES_UBIGEO { get; set; } = string.Empty;
    public string DES_ABREVIADA { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_INTERIOR { get; set; } = string.Empty;
    public string DES_KILOMETRO { get; set; } = string.Empty;
    public string DES_LOTE { get; set; } = string.Empty;
    public string DES_MANZANA { get; set; } = string.Empty;
    public string DES_NOMBRE_VIA { get; set; } = string.Empty;
    public string DES_NOMBRE_ZONA { get; set; } = string.Empty;
    public string DES_NUMERACION { get; set; } = string.Empty;
    public string DES_REFERENCIA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;

    /// <summary>Del tipo de movimiento, vía el SHOWBYID: con qué rótulo se pide la entidad en
    /// ESTE documento. Un movimiento de compra dice "Proveedor", uno de venta "Cliente".</summary>
    public string? COD_TIPO_ENTIDAD_ASOCIADA { get; set; }
    public string? DES_TIPO_ENTIDAD { get; set; }
    public string DES_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public int? NUM_RESERVA_STOCK { get; set; }
    public string COD_UBIGEO { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? NUM_PEDIDO_COMERCIAL { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
}

/// <summary>Los ocho campos que antes iban con [Required] ya no lo llevan.
///
/// Ninguno es obligatorio de verdad: las ocho columnas admiten NULL en la tabla, y cuales
/// se piden depende del TIPO DE MOVIMIENTO. Una transferencia no lleva comprobante de pago;
/// un ingreso por compra no lleva almacen destino; el lote solo aparece si el tipo lo
/// consigna. La pantalla ya sabe eso - lo resuelve con las banderas del tipo - y la API no
/// tiene por que volver a decidirlo con menos informacion.
///
/// Con [Required], grabar la cabecera de cualquier movimiento que no usara los ocho
/// devolvia 400 antes de tocar la base, y el mensaje hablaba de campos que la pantalla ni
/// siquiera muestra.
///
/// Es el mismo caso que ya habiamos corregido en ProductosUpdateDto.</summary>
public class MovimientosAlmacenInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public DateTime? FEC_MOVIMENTO_ALMACEN { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string? COD_CENTRO_COSTO { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK_ATENDIDO { get; set; }
    public int? NUM_RESERVA_STOCK_ATENDIDO { get; set; }
    public string? COD_ALMACEN_SINCRONIZADO { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO { get; set; }
    public string? COD_CLASE_OPERACION { get; set; }
    public string? COD_TIPO_OPERACION { get; set; }
    public int? MUM_PEDIDO_COMERCIAL { get; set; }
    public decimal? NUM_REGISTRO { get; set; }
    public decimal? COD_ENTIDAD_TRANSPORTISTA { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    public string? DES_DIRECCION_DESTINO { get; set; }
    public string? COD_UBIGEO_DESTINO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    /// <summary>ANULABLES, y no por gusto: el modelo de nulabilidad de C# está activado, y
    /// ASP.NET Core trata toda propiedad de referencia NO anulable como obligatoria aunque no
    /// tenga [Required]. Como string tampoco las manda —las pone el controlador desde el
    /// token, y el repositorio usa ésas y no las del DTO—, la validación rechazaba el pedido
    /// antes de llegar a la base con "The COD_USUARIO_REGISTRO field is required".
    ///
    /// Un campo que el cliente no debe mandar no puede ser obligatorio para el cliente.</summary>
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class MovimientosAlmacenUpdateDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public DateTime? FEC_MOVIMENTO_ALMACEN { get; set; }
    public DateTime? FEC_RECEPCION { get; set; }
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string? COD_CENTRO_COSTO { get; set; }
    public int? NUM_PROYECTO { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK_ATENDIDO { get; set; }
    public int? NUM_RESERVA_STOCK_ATENDIDO { get; set; }
    public string? COD_ALMACEN_SINCRONIZADO { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO { get; set; }
    public string? COD_CLASE_OPERACION { get; set; }
    public string? COD_TIPO_OPERACION { get; set; }
    public int? MUM_PEDIDO_COMERCIAL { get; set; }
    public decimal? NUM_REGISTRO { get; set; }
    public decimal? COD_ENTIDAD_TRANSPORTISTA { get; set; }
    public int? NUM_SECUENCIA_DIRECCION { get; set; }
    public string? DES_DIRECCION_DESTINO { get; set; }
    public string? COD_UBIGEO_DESTINO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    /// <summary>ANULABLES, y no por gusto: el modelo de nulabilidad de C# está activado, y
    /// ASP.NET Core trata toda propiedad de referencia NO anulable como obligatoria aunque no
    /// tenga [Required]. Como string tampoco las manda —las pone el controlador desde el
    /// token, y el repositorio usa ésas y no las del DTO—, la validación rechazaba el pedido
    /// antes de llegar a la base con "The COD_USUARIO_REGISTRO field is required".
    ///
    /// Un campo que el cliente no debe mandar no puede ser obligatorio para el cliente.</summary>
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class MovimientosAlmacenSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Los ocho combos de la cabecera en un solo viaje.
///
/// EL ORDEN DE LAS PROPIEDADES REFLEJA EL ORDEN DE LOS RESULTADOS DEL SP, y eso es un
/// contrato: el repositorio los lee en secuencia con QueryMultiple. Si se cambia el orden
/// en PROC_LOGISTICA_MOVIMIENTOS_ALMACEN_LOAD_COMBOS hay que cambiarlo aca tambien, y el
/// sintoma seria una pantalla con los combos cruzados, no un error.</summary>
public class MovimientosAlmacenLoadCombosResult
{
    public int                CodTipoEstado          { get; set; }
    public List<ComboboxItem> Almacenes              { get; set; } = new();
    public List<ComboboxItem> TiposMovimiento        { get; set; } = new();
    public List<ComboboxItem> Entidades              { get; set; } = new();
    public List<ComboboxItem> TiposComprobantePago   { get; set; } = new();
    public List<ComboboxItem> CentrosCosto           { get; set; } = new();
    public List<ComboboxItem> LotesControl           { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida         { get; set; } = new();
    public List<EstadoItem>   Estados                { get; set; } = new();

    /// <summary>La sucursal de cada almacén de <see cref="Almacenes"/>. NO es un conjunto aparte del
    /// SP: sale de las mismas filas del primer conjunto (PROC_LOGISTICA_ALMACENES_SHOWCOMBOX ya
    /// trae COD_SUCURSAL y DES_SUCURSAL), así que no altera el orden de los resultados. La pantalla
    /// la usa para ofrecer, en una transferencia, solo los almacenes que el alcance del tipo permite.</summary>
    public List<AlmacenSucursalItem> AlmacenesSucursal { get; set; } = new();
}

/// <summary>A qué sucursal pertenece un almacén. COD_SUCURSAL es opcional en LOGISTICA.ALMACENES:
/// un almacén sin sucursal llega con CodSucursal en null.</summary>
public class AlmacenSucursalItem
{
    public string  CodAlmacen  { get; set; } = string.Empty;
    public string? CodSucursal { get; set; }
    public string? DesSucursal { get; set; }
}

/// <summary>Lo que pide el boton "Agregar al detalle".
///
/// DES_SECUENCIAS acepta "1,3,7" para bajar solo esas lineas. En null baja TODAS las del
/// documento de origen que todavia tengan cantidad pendiente.</summary>
/// <summary>Lo que devuelve el SP del número al imprimir.
///
/// FLG_ASIGNADO_AHORA distingue el número recién consumido de uno que ya estaba: sirve para
/// que la pantalla avise "se asignó la guía T001-0000453" sólo la primera vez, y no en cada
/// reimpresión.
///
/// NUM_COMPROBANTE_PAGO en null NO es un error: quiere decir que ese movimiento no lleva
/// guía con correlativo, que es el caso de la mayoría.</summary>
public class MovimientosAlmacenNumeroImpresionResult
{
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public bool FLG_ASIGNADO_AHORA { get; set; }
}

public class MovimientosAlmacenAgregarDto
{
    public int? NUM_RESERVA_STOCK       { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public int? NUM_PEDIDO_COMERCIAL    { get; set; }
    public string? DES_SECUENCIAS       { get; set; }
}

/// <summary>Lo que devuelven los tres procedimientos de "Agregar al detalle": cuantas
/// lineas entraron y un mensaje listo para mostrar. Cero no es un error — significa que ya
/// estaba todo agregado o que no queda nada pendiente.</summary>
public class MovimientosAlmacenAgregarResult
{
    public int    CAN_LINEAS_AGREGADAS { get; set; }
    public string DES_MENSAJE          { get; set; } = string.Empty;
}
