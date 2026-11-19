using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_GASTOS_ADICIONALES: gastos adicionales presupuestados, a una fase o al proyecto principal (COD_FASE nulo).</summary>
public class ProyectosGastosAdicionales
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public string? DES_FASE { get; set; }
    public string DES_GASTO { get; set; } = string.Empty;
    public decimal? VAL_PRESUPUESTADO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class ProyectosGastosAdicionalesInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    [Required] public string DES_GASTO { get; set; } = string.Empty;
    [Required] public decimal? VAL_PRESUPUESTADO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class ProyectosGastosAdicionalesUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    [Required] public string DES_GASTO { get; set; } = string.Empty;
    [Required] public decimal? VAL_PRESUPUESTADO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

public class ProyectosGastosAdicionalesSearchDto
{
    public string? Search { get; set; }
}
