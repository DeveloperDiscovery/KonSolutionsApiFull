using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Mantenimiento;

/// <summary>Entidad MANTENIMIENTO_INDUSTRIAL_DETALLE_ENTIDADES - tipos equivalentes a la BD.</summary>
public class MantenimientoIndustrialDetalleEntidades
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleEntidadesInsertDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleEntidadesUpdateDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleEntidadesSearchDto
{
    public string? Search { get; set; }
}