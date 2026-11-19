using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_CONTRATO_BENEFICIOS_LABORALES - tipos equivalentes a la BD.</summary>
public class TiposContratoBeneficiosLaborales
{
    public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public string DES_SUB_TIPO { get; set; } = string.Empty;
    public string DES_TIPO_CONTRATO { get; set; } = string.Empty;
    public string DES_USO_CONTRACTUAL { get; set; } = string.Empty;
}

public class TiposContratoBeneficiosLaboralesInsertDto
{
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    [Required] public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposContratoBeneficiosLaboralesUpdateDto
{
    [Required] public string COD_TIPO_CONTRATO { get; set; } = string.Empty;
    [Required] public string COD_BENEFICIO_LABORAL { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposContratoBeneficiosLaboralesSearchDto
{
    public string? Search { get; set; }
}