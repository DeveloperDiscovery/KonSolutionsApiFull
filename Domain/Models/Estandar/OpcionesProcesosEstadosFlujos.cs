namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>
/// Fila devuelta por PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_TREELIST — una por cada
/// combinación Botón × Estado de dependencia (LEFT JOIN a OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA).
/// El árbol se arma en el cliente agrupando por COD_BOTON_OPCION: el botón es el nodo padre y
/// cada COD_ESTADO_DEPENDENCIA no nulo es uno de sus hijos (estado previo requerido). Cuando
/// COD_ESTADO_DEPENDENCIA es NULL, el botón no tiene restricción y se ejecuta desde cualquier estado.
/// </summary>
public class OpcionesProcesosEstadosFlujosTreeList
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public string? DES_BOTON_OPCION { get; set; }
    public short? COD_ESTADO { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public short? COD_ESTADO_DEPENDENCIA { get; set; }
    public string? DES_ESTADO_DEPENDENCIA { get; set; }
    public string? DES_BACKCOLOR_DEPENDENCIA { get; set; }
    public string? DES_FORECOLOR_DEPENDENCIA { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

/// <summary>ESTANDAR.OPCIONES_PROCESOS_ESTADOS_FLUJOS — un flujo por botón dentro del Tipo de
/// Estado, con el Estado destino al que pasa el registro al ejecutarse. A diferencia de
/// OPCIONES_PROCESOS_ESTADOS, esta tabla sí tiene SP _UPDATE separado del _INSERT.</summary>
public class OpcionesProcesosEstadosFlujos
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public short? COD_ESTADO { get; set; }
    public byte? NUM_ORDEN_FLUJO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL vía JOIN a APLICACIONES_OPCIONES_BOTONES ──
    public string? DES_BOTON_OPCION { get; set; }
    public string? DES_ALIAS { get; set; }
    public string? IconoBase64 { get; set; }
}

public class OpcionesProcesosEstadosFlujosInsertDto
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public short? COD_ESTADO { get; set; }
    public byte? NUM_ORDEN_FLUJO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

public class OpcionesProcesosEstadosFlujosUpdateDto
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public short? COD_ESTADO { get; set; }
    public byte? NUM_ORDEN_FLUJO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}
