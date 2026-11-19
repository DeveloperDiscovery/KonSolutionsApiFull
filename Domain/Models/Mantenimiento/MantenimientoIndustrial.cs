using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Mantenimiento;

/// <summary>Entidad MANTENIMIENTO_INDUSTRIAL - tipos equivalentes a la BD.</summary>
public class MantenimientoIndustrial
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    public string DES_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    public string COD_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMACION { get; set; }
    public DateTime? FEC_FIN_PROGRAMACION { get; set; }
    public DateTime? FEC_INICIO_EJECUTADA { get; set; }
    public DateTime? FEC_FIN_EJECUTADA { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
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
    public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public string DES_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
}

public class MantenimientoIndustrialInsertDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    [Required] public string DES_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMACION { get; set; }
    public DateTime? FEC_FIN_PROGRAMACION { get; set; }
    public DateTime? FEC_INICIO_EJECUTADA { get; set; }
    public DateTime? FEC_FIN_EJECUTADA { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialUpdateDto
{
    public int? NUM_MANTENIMIENTO_INDUSTRIAL { get; set; }
    [Required] public string DES_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    public DateTime? FEC_INICIO_PROGRAMACION { get; set; }
    public DateTime? FEC_FIN_PROGRAMACION { get; set; }
    public DateTime? FEC_INICIO_EJECUTADA { get; set; }
    public DateTime? FEC_FIN_EJECUTADA { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MantenimientoIndustrialSearchDto
{
    public string? Search { get; set; }
}