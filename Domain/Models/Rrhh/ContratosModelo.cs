using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad CONTRATOS_MODELO - tipos equivalentes a la BD.</summary>
public class ContratosModelo
{
    public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string DES_CONTRATO_MODELO { get; set; } = string.Empty;
    public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
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
    public string DES_REGIMEN_LABORAL { get; set; } = string.Empty;
    public string DES_SUB_TIPO { get; set; } = string.Empty;
    public string DES_TIPO_CONTRATO { get; set; } = string.Empty;
    public string DES_USO_CONTRACTUAL { get; set; } = string.Empty;
}

public class ContratosModeloInsertDto
{
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string DES_CONTRATO_MODELO { get; set; } = string.Empty;
    [Required] public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ContratosModeloUpdateDto
{
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string DES_CONTRATO_MODELO { get; set; } = string.Empty;
    [Required] public string COD_REGIMEN_LABORAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ContratosModeloSearchDto
{
    public string? Search { get; set; }
}