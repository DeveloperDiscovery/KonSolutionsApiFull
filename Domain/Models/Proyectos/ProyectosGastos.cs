using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_GASTOS - tipos equivalentes a la BD.</summary>
public class ProyectosGastos
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTE { get; set; }
    public decimal? VAL_GASTO_ASIGNADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
}

public class ProyectosGastosInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTE { get; set; }
    public decimal? VAL_GASTO_ASIGNADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosGastosUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public decimal? NUM_REGISTRO_COMPROBANTE { get; set; }
    public decimal? VAL_GASTO_ASIGNADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProyectosGastosSearchDto
{
    public string? Search { get; set; }
}