using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_MATERIALES: el plan de materiales del proyecto. Lo consumido sale de los movimientos de almacén del proyecto.</summary>
public class ProyectosMateriales
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string? DES_PRODUCTO { get; set; }
    public decimal? CAN_PROGRAMADA { get; set; }
    public decimal? VAL_COSTO_UNITARIO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class ProyectosMaterialesInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PROGRAMADA { get; set; }
    public decimal? VAL_COSTO_UNITARIO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class ProyectosMaterialesUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PROGRAMADA { get; set; }
    public decimal? VAL_COSTO_UNITARIO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class ProyectosMaterialesSearchDto
{
    public string? Search { get; set; }
}
