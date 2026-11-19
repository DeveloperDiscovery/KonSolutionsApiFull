using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad CARGOS_ADMINISTRATIVOS_COMPETENCIAS - tipos equivalentes a la BD.</summary>
public class CargosAdministrativosCompetencias
{
    public int? COD_CARGO { get; set; }
    public string COD_COMPETENCIA { get; set; } = string.Empty;
    public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_COMPETENCIA { get; set; } = string.Empty;
}

public class CargosAdministrativosCompetenciasInsertDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosAdministrativosCompetenciasUpdateDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string COD_COMPETENCIA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_COMPETENCIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosAdministrativosCompetenciasSearchDto
{
    public string? Search { get; set; }
}