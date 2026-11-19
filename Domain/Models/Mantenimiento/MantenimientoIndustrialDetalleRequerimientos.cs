using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Mantenimiento;

/// <summary>Entidad MANTENIMIENTO_INDUSTRIAL_DETALLE_REQUERIMIENTOS - tipos equivalentes a la BD.</summary>
public class MantenimientoIndustrialDetalleRequerimientos
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_REQUERIDO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleRequerimientosInsertDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_REQUERIDO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleRequerimientosUpdateDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_REQUERIDO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleRequerimientosSearchDto
{
    public string? Search { get; set; }
}