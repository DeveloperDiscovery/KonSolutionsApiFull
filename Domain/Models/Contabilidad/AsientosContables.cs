using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad ASIENTOS_CONTABLES - tipos equivalentes a la BD.</summary>
public class AsientosContables
{
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string DES_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public bool? FLG_ASIENTO_APERTURA { get; set; }
    public bool? FLG_ASIENTO_CIERRE { get; set; }
    public string COD_LIBRO_CONTABLE_SUNAT { get; set; } = string.Empty;
    public bool? FLG_APERTURA { get; set; }
    public bool? FLG_MENSUAL { get; set; }
    public bool? FLG_CIERRE { get; set; }
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
}

public class AsientosContablesInsertDto
{
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    [Required] public string DES_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public bool? FLG_ASIENTO_APERTURA { get; set; }
    public bool? FLG_ASIENTO_CIERRE { get; set; }
    [Required] public string COD_LIBRO_CONTABLE_SUNAT { get; set; } = string.Empty;
    public bool? FLG_APERTURA { get; set; }
    public bool? FLG_MENSUAL { get; set; }
    public bool? FLG_CIERRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AsientosContablesUpdateDto
{
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    [Required] public string DES_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public bool? FLG_ASIENTO_APERTURA { get; set; }
    public bool? FLG_ASIENTO_CIERRE { get; set; }
    [Required] public string COD_LIBRO_CONTABLE_SUNAT { get; set; } = string.Empty;
    public bool? FLG_APERTURA { get; set; }
    public bool? FLG_MENSUAL { get; set; }
    public bool? FLG_CIERRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AsientosContablesSearchDto
{
    public string? Search { get; set; }
}