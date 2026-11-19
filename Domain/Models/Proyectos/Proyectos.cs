using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS (cabecera) - tipos equivalentes a la BD.
/// Entrega de alcance "solo cabecera": los 14 detalles del módulo (Fases, Tareas,
/// Sub_Tareas, Gastos, Hitos, Riesgos, Roles, Archivos, Memoria, Recurso_Humano_Asignado,
/// Impacto_Riesgo, Probabilidad_Riesgo, Planes_Mitigacion_Riesgos, Unidades_Medida_Tareas,
/// Tipos_Entidad) quedan fuera de esta entrega y se agregan a pedido.</summary>
public class Proyectos
{
    public int? NUM_PROYECTO { get; set; }
    public string DES_PROYECTO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public decimal? COD_ENTIDAD_ESPONSOR { get; set; }
    public string COD_TIPO_PROYECTO { get; set; } = string.Empty;
    public string COD_MONEDA_VALORIZACION { get; set; } = string.Empty;
    public decimal? VAL_PRESUPUESTO_PROYECTO { get; set; }
    public decimal? VAL_COSTO_PROYECTO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Vienen resueltas por los JOIN del procedimiento: solo se muestran.
    // Nota: el SP solo hace JOIN a ENTIDADES para COD_ENTIDAD_RESPONSABLE (alias "b");
    // COD_ENTIDAD_ESPONSOR no trae descripción — no se adivinó una solución cliente para
    // esto porque no se pidió; avisar si hace falta mostrar el nombre del sponsor.
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_PREFIJO_USUARIO_COMERCIAL { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_TIPO_PROYECTO { get; set; } = string.Empty;
    /// <summary>Nombre de la entidad sponsor (la trae el procedimiento desde el script 68).</summary>
    public string DES_ESPONSOR { get; set; } = string.Empty;
    public string? DES_CARGO_RESPONSABLE { get; set; }
    public string? DES_AREA_RESPONSABLE { get; set; }
    public string? DES_CARGO_ESPONSOR { get; set; }
    public string? DES_AREA_ESPONSOR { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class ProyectosInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    [Required] public string DES_PROYECTO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public decimal? COD_ENTIDAD_ESPONSOR { get; set; }
    public string? COD_TIPO_PROYECTO { get; set; }
    public string? COD_MONEDA_VALORIZACION { get; set; }
    public decimal? VAL_PRESUPUESTO_PROYECTO { get; set; }
    public decimal? VAL_COSTO_PROYECTO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ProyectosUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    [Required] public string DES_PROYECTO { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMADO { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADO { get; set; }
    public DateTime? FEC_INICIO_EJECUTADO { get; set; }
    public DateTime? FEC_FINAL_EJECUTADO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public decimal? COD_ENTIDAD_ESPONSOR { get; set; }
    public string? COD_TIPO_PROYECTO { get; set; }
    public string? COD_MONEDA_VALORIZACION { get; set; }
    public decimal? VAL_PRESUPUESTO_PROYECTO { get; set; }
    public decimal? VAL_COSTO_PROYECTO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ProyectosSearchDto
{
    public string? Search { get; set; }
}
