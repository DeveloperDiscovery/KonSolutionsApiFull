using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Mantenimiento;

/// <summary>Entidad MANTENIMIENTO_INDUSTRIAL_DETALLE - tipos equivalentes a la BD.</summary>
public class MantenimientoIndustrialDetalle
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_ACTIVO_MAQUINA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMACION { get; set; }
    public DateTime? FEC_FIN_PROGRAMACION { get; set; }
    public DateTime? FEC_INICIO_EJECUTADA { get; set; }
    public DateTime? FEC_FIN_EJECUTADA { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleInsertDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO_MAQUINA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMACION { get; set; }
    public DateTime? FEC_FIN_PROGRAMACION { get; set; }
    public DateTime? FEC_INICIO_EJECUTADA { get; set; }
    public DateTime? FEC_FIN_EJECUTADA { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleUpdateDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO_MAQUINA { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMACION { get; set; }
    public DateTime? FEC_FIN_PROGRAMACION { get; set; }
    public DateTime? FEC_INICIO_EJECUTADA { get; set; }
    public DateTime? FEC_FIN_EJECUTADA { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialDetalleSearchDto
{
    public string? Search { get; set; }
}