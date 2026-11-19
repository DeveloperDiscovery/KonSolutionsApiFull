namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Guia de compra (ingreso comercial del mismo proveedor y almacen) que se puede asociar a una
/// devolucion de compra, o que ya esta asociada (CAN_DEVUELTA se llena solo en la lista de asociadas).</summary>
public class MovimientoDevolucionGuia
{
    public int NUM_MOVIMIENTO_ALMACEN_ORIGEN { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO { get; set; }
    public string? NUM_COMPROBANTE_PAGO { get; set; }
    public string DES_GUIA { get; set; } = string.Empty;
    public DateTime? FEC_MOVIMENTO_ALMACEN { get; set; }
    public string? COD_TIPO_MOVIMIENTO { get; set; }
    public int CAN_LINEAS { get; set; }
    public decimal CAN_RECIBIDA { get; set; }
    public decimal CAN_SALDO { get; set; }
    public decimal CAN_DEVUELTA { get; set; }
    public bool FLG_ASOCIADA { get; set; }
}

/// <summary>Item de una guia de compra: recibido, ya devuelto en otras devoluciones, saldo y lo que esta
/// devolucion lleva (CAN_DEVOLVER, 0 si aun no se asigna).</summary>
public class MovimientoDevolucionLinea
{
    public int NUM_MOVIMIENTO_ALMACEN_ORIGEN { get; set; }
    public int NUM_SECUENCIA_ORIGEN { get; set; }
    public string DES_GUIA { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string? DES_PRODUCTO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? NUM_LOTE_CONTROL { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public decimal CAN_RECIBIDA { get; set; }
    public decimal CAN_YA_DEVUELTA { get; set; }
    public decimal CAN_SALDO { get; set; }
    public int? NUM_SECUENCIA_DEVOLUCION { get; set; }
    public decimal CAN_DEVOLVER { get; set; }
    /// <summary>Ubicación de distribución del almacén: de ahí sale la devolución.</summary>
    public string? COD_UBICACION_DISTRIBUCION { get; set; }
    /// <summary>Stock disponible del producto/lote en esa ubicación (más lo que esta devolución ya tomó).</summary>
    public decimal CAN_STOCK_DISTRIBUCION { get; set; }
    /// <summary>Lo máximo que se puede devolver: el menor entre el saldo de la guía y el stock en distribución.</summary>
    public decimal CAN_MAXIMA { get; set; }
}

public class MovimientoDevolucionItemDto
{
    public int NUM_MOVIMIENTO_ALMACEN_ORIGEN { get; set; }
    public int NUM_SECUENCIA_ORIGEN { get; set; }
    /// <summary>Cantidad a devolver; 0 quita el item de la devolucion.</summary>
    public decimal CAN_DEVOLVER { get; set; }
}

public class MovimientoDevolucionAdicionarDto
{
    public List<MovimientoDevolucionItemDto> Items { get; set; } = new();
}
