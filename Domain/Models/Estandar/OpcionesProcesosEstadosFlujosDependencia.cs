namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>ESTANDAR.OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA. Clave compuesta
/// COD_APLICACION + COD_OPCION_APLICACION + COD_TIPO_ESTADO + COD_BOTON_OPCION + COD_ESTADO —
/// los estados previos requeridos para que un flujo (botón) pueda ejecutarse. Tabla de
/// asignación pura: el SP _INSERT hace upsert, no hay SP _UPDATE.</summary>
public class OpcionesProcesosEstadosFlujosDependencia
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public short COD_ESTADO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL vía JOIN a ESTANDAR.ESTADOS ──
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }
}

public class OpcionesProcesosEstadosFlujosDependenciaInsertDto
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public short COD_TIPO_ESTADO { get; set; }
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public short COD_ESTADO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
}

/// <summary>Resultado de PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA_VALIDA —
/// valida si el estado actual del registro (COD_ESTADO_ACTUAL) habilita ejecutar un botón,
/// según sus estados previos requeridos. OJO: si el botón no tiene NINGUNA dependencia
/// configurada, el SP devuelve FLG_VALIDO=0 igual (el INNER JOIN no encuentra filas) — no
/// es "sin restricción = válido siempre", hay que llamarlo solo para botones que sí tienen
/// dependencias (ver _dependenciasPorBoton en OpcionesProcesosEstados.razor).</summary>
public class OpcionesProcesosEstadosFlujosDependenciaValidaResult
{
    public bool FLG_VALIDO { get; set; }
    public string? DES_MENSAJE { get; set; }
}
