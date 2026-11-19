using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CONTRATOS - tipos equivalentes a la BD.</summary>
public class PersonalContratos
{
    public int? NUM_CONTRATO { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
    public string DES_SUB_TIPO { get; set; } = string.Empty;
    public string DES_TIPO_CONTRATO { get; set; } = string.Empty;
    public string DES_USO_CONTRACTUAL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalContratosInsertDto
{
    public int? NUM_CONTRATO { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalContratosUpdateDto
{
    public int? NUM_CONTRATO { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_VENCIMIENTO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalContratosSearchDto
{
    public string? Search { get; set; }
}