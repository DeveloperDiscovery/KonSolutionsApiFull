using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Mantenimiento;

/// <summary>Entidad MANTENIMIENTO_INDUSTRIAL_DETALLE_ESPECIFICACIONES_TECNICAS - tipos equivalentes a la BD.</summary>
public class MantenimientoIndustrialDetalleEspecificacionesTecnicas
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
    public string DES_ITEM_TECNICO { get; set; } = string.Empty;
    public string DES_ITEM_TECNICO_SINGULAR { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleEspecificacionesTecnicasInsertDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
}

public class MantenimientoIndustrialDetalleEspecificacionesTecnicasUpdateDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public int? COD_ITEM_TECNICO { get; set; }
}

public class MantenimientoIndustrialDetalleEspecificacionesTecnicasSearchDto
{
    public string? Search { get; set; }
}