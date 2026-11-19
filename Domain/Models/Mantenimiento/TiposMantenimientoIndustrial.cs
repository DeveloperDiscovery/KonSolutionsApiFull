using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Mantenimiento;

/// <summary>Entidad TIPOS_MANTENIMIENTO_INDUSTRIAL - tipos equivalentes a la BD.</summary>
public class TiposMantenimientoIndustrial
{
    public string COD_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    public string DES_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public string DES_TIPO_PROGRAMACION { get; set; } = string.Empty;
}

public class TiposMantenimientoIndustrialInsertDto
{
    [Required] public string COD_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
}

public class TiposMantenimientoIndustrialUpdateDto
{
    [Required] public string COD_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_MANTENIMIENTO_INDUSTRIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
}

public class TiposMantenimientoIndustrialSearchDto
{
    public string? Search { get; set; }
}