using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad TIPOS_MOVIMIENTO - tipos equivalentes a la BD.</summary>
public class TiposMovimiento
{
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string DES_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD_ASOCIADA { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_TRANSFERENCIA { get; set; }
    public bool? FLG_COMERCIAL { get; set; }
    public bool? FLG_DEVOLUCION { get; set; }
    public bool? FLG_ANULACION { get; set; }
    public bool? FLG_CONSIGNACION { get; set; }
    public bool? FLG_AJUSTE_INVENTARIO { get; set; }
    public bool? FLG_COMPROBANTE_RELACIONADO { get; set; }
    public bool? FLG_VALOR_INVENTARIO { get; set; }
    public bool? FLG_CONSIGNA_PROYECTO { get; set; }
    public bool? FLG_CONSIGNA_CENTRO_COSTO { get; set; }
    public bool? FLG_CONSIGNA_LOTE_CONTROL { get; set; }
    public bool? FLG_DESTINO_AUTOMATICO { get; set; }
    public bool? FLG_REQUIERE_CONFIRMACION_RECEPCION { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_REQUERIMIENTO { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    /// <summary>Alcance de la transferencia. Sólo tienen sentido con FLG_TRANSFERENCIA en
    /// true: dicen SI la transferencia cruza sucursales de la empresa, si es entre almacenes
    /// de una misma sucursal, o entre ubicaciones de un mismo almacén. En un tipo que no es de
    /// transferencia no significan nada.</summary>
    public bool? FLG_TRANSFERENCIA_SUCURSALES_EMPRESA { get; set; }
    public bool? FLG_TRANSFERENCIA_ALMACENES_SUCURSAL { get; set; }
    /// <summary>Tercer alcance: entre ubicaciones de un MISMO almacén (traslados de un rack a
    /// otro). El almacén destino es el mismo del movimiento.</summary>
    public bool? FLG_TRANSFERENCIA_UBICACIONES_ALMACEN { get; set; }
    public string COD_TIPO_MOVIMIENTO_SINCRONIZADO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    // Tipo de estado que el movimiento aplica al documento relacionado (ESTANDAR.TIPOS_ESTADOS).
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_MOTIVO_DESTINO { get; set; }
    public int? COD_MOTIVO_DEFECTO { get; set; }
    /// <summary>Icono del tipo de movimiento. Se guarda como imagen en la base (IMG_PICTURE),
    /// igual que el de Tipos de Merma o Tipos de Zonificacion.</summary>
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
    // Descripción de COD_TIPO_ESTADO_RELACIONADO (JOIN a ESTANDAR.TIPOS_ESTADOS).
    public string? DES_TIPO_ESTADO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    /// <summary>Lectura de etiquetas (RFID / codigo de barras) en este tipo de movimiento:
    /// N = no usa lectura | O = optativa | R = obligatoria (la revision exige las lecturas).</summary>
    public string? COD_REQUISITO_LECTURA { get; set; }
}

public class TiposMovimientoInsertDto
{
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    // Sin [Required]: PROC_LOGISTICA_TIPOS_MOVIMIENTO_INSERT lo genera automáticamente
    // (correlativo de 5 dígitos por Clase de Movimiento, vía FNC_ObtenerSiguienteCorrelativo)
    // cuando llega vacío — [Required] rechazaría el string vacío del alta nueva.
    public string? COD_TIPO_MOVIMIENTO { get; set; }
    // Sin [Required]: solo Estado es obligatorio en este módulo (pedido explícito) —
    // la columna además es NULL en la tabla real.
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public string? COD_TIPO_ENTIDAD_ASOCIADA { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_TRANSFERENCIA { get; set; }
    public bool? FLG_COMERCIAL { get; set; }
    public bool? FLG_DEVOLUCION { get; set; }
    public bool? FLG_ANULACION { get; set; }
    public bool? FLG_CONSIGNACION { get; set; }
    public bool? FLG_AJUSTE_INVENTARIO { get; set; }
    public bool? FLG_COMPROBANTE_RELACIONADO { get; set; }
    public bool? FLG_VALOR_INVENTARIO { get; set; }
    public bool? FLG_CONSIGNA_PROYECTO { get; set; }
    public bool? FLG_CONSIGNA_CENTRO_COSTO { get; set; }
    public bool? FLG_CONSIGNA_LOTE_CONTROL { get; set; }
    public bool? FLG_DESTINO_AUTOMATICO { get; set; }
    public bool? FLG_REQUIERE_CONFIRMACION_RECEPCION { get; set; }
    public bool? FLG_CONTROL_CALIDAD { get; set; }
    public bool? FLG_REQUERIMIENTO { get; set; }
    public bool? FLG_PRODUCCION { get; set; }
    /// <summary>Alcance de la transferencia. Sólo tienen sentido con FLG_TRANSFERENCIA en
    /// true: dicen SI la transferencia cruza sucursales de la empresa, si es entre almacenes
    /// de una misma sucursal, o entre ubicaciones de un mismo almacén. En un tipo que no es de
    /// transferencia no significan nada.</summary>
    public bool? FLG_TRANSFERENCIA_SUCURSALES_EMPRESA { get; set; }
    public bool? FLG_TRANSFERENCIA_ALMACENES_SUCURSAL { get; set; }
    /// <summary>Tercer alcance: entre ubicaciones de un MISMO almacén (traslados de un rack a
    /// otro). El almacén destino es el mismo del movimiento.</summary>
    public bool? FLG_TRANSFERENCIA_UBICACIONES_ALMACEN { get; set; }
    public string? COD_TIPO_MOVIMIENTO_SINCRONIZADO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_MOTIVO_DESTINO { get; set; }
    public int? COD_MOTIVO_DEFECTO { get; set; }
    /// <summary>Icono del tipo de movimiento. Se guarda como imagen en la base (IMG_PICTURE),
    /// igual que el de Tipos de Merma o Tipos de Zonificacion.</summary>
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    /// <summary>N / O / R. En el alta, vacio es N; al modificar, vacio deja el que estaba.</summary>
    [RegularExpression("^[NOR]?$", ErrorMessage = "El requisito de lectura es N (no usa), O (optativa) o R (obligatoria).")]
    public string? COD_REQUISITO_LECTURA { get; set; }
}

public class TiposMovimientoSearchDto
{
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_LOGISTICA_TIPOS_MOVIMIENTO_LOADCOMBOS: TiposEntidades,
/// TiposMotivos, TiposEstados y Estado (filtrado por @COD_TIPO_ESTADO) en un solo viaje
/// para TiposMovimientoDialog.razor. CargarMotivos() (cascada real por
/// COD_TIPO_MOTIVO_DESTINO) NO forma parte de este bundle.</summary>
public class TiposMovimientoLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposEntidades { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposMotivos   { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposEstados   { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados        { get; set; } = new();
}