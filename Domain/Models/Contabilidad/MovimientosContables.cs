using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad MOVIMIENTOS_CONTABLES - tipos equivalentes a la BD.</summary>
public class MovimientosContables
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public DateTime? FEC_ASIENTO_CONTABLE { get; set; }
    public string DES_GLOSA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_COMENTARIO_CIERRE { get; set; } = string.Empty;
}

public class MovimientosContablesInsertDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public DateTime? FEC_ASIENTO_CONTABLE { get; set; }
    [Required] public string DES_GLOSA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MovimientosContablesUpdateDto
{
    public int? NUM_PERIODO_CONTABLE { get; set; }
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public int? NUM_ASIENTO_CONTABLE { get; set; }
    public DateTime? FEC_ASIENTO_CONTABLE { get; set; }
    [Required] public string DES_GLOSA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MovimientosContablesSearchDto
{
    public string? Search { get; set; }
}