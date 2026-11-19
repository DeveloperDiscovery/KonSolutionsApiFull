using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_PERIODO - tipos equivalentes a la BD.</summary>
public class TiposPeriodo
{
    public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public bool? FLG_QUINQUENAL { get; set; }
    public bool? FLG_BIANUAL { get; set; }
    public bool? FLG_ANUAL { get; set; }
    public bool? FLG_SEMESTRAL { get; set; }
    public bool? FLG_TRIMESTRAL { get; set; }
    public bool? FLG_MENSUAL { get; set; }
    public bool? FLG_QUINCENAL { get; set; }
    public bool? FLG_SEMANAL { get; set; }
    public bool? FLG_DIARIO { get; set; }
    public bool? FLG_MODULO_CONTABLE { get; set; }
    public bool? FLG_MODULO_COSTOS { get; set; }
    public bool? FLG_MODULO_RRHH_NOMINA { get; set; }
    public bool? FLG_MODULO_COMERCIAL { get; set; }
    public bool? FLG_MODULO_NETWORKING { get; set; }
    public bool? FLG_MODULO_PLANEAMIENTO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string DES_TIPO_ESTADO { get; set; } = string.Empty;
}

public class TiposPeriodoInsertDto
{
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    [Required] public string DES_PREFIJO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public bool? FLG_QUINQUENAL { get; set; }
    public bool? FLG_BIANUAL { get; set; }
    public bool? FLG_ANUAL { get; set; }
    public bool? FLG_SEMESTRAL { get; set; }
    public bool? FLG_TRIMESTRAL { get; set; }
    public bool? FLG_MENSUAL { get; set; }
    public bool? FLG_QUINCENAL { get; set; }
    public bool? FLG_SEMANAL { get; set; }
    public bool? FLG_DIARIO { get; set; }
    public bool? FLG_MODULO_CONTABLE { get; set; }
    public bool? FLG_MODULO_COSTOS { get; set; }
    public bool? FLG_MODULO_RRHH_NOMINA { get; set; }
    public bool? FLG_MODULO_COMERCIAL { get; set; }
    public bool? FLG_MODULO_NETWORKING { get; set; }
    public bool? FLG_MODULO_PLANEAMIENTO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposPeriodoUpdateDto
{
    [Required] public string COD_TIPO_PERIODO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_PERIODO { get; set; } = string.Empty;
    [Required] public string DES_PREFIJO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public bool? FLG_QUINQUENAL { get; set; }
    public bool? FLG_BIANUAL { get; set; }
    public bool? FLG_ANUAL { get; set; }
    public bool? FLG_SEMESTRAL { get; set; }
    public bool? FLG_TRIMESTRAL { get; set; }
    public bool? FLG_MENSUAL { get; set; }
    public bool? FLG_QUINCENAL { get; set; }
    public bool? FLG_SEMANAL { get; set; }
    public bool? FLG_DIARIO { get; set; }
    public bool? FLG_MODULO_CONTABLE { get; set; }
    public bool? FLG_MODULO_COSTOS { get; set; }
    public bool? FLG_MODULO_RRHH_NOMINA { get; set; }
    public bool? FLG_MODULO_COMERCIAL { get; set; }
    public bool? FLG_MODULO_NETWORKING { get; set; }
    public bool? FLG_MODULO_PLANEAMIENTO { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposPeriodoSearchDto
{
    public string? Search { get; set; }
}
