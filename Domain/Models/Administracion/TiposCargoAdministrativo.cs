using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Administracion;

/// <summary>Entidad TIPOS_CARGO_ADMINISTRATIVO - tipos equivalentes a la BD.</summary>
public class TiposCargoAdministrativo
{
    public int? COD_TIPO_CARGO { get; set; }
    public string DES_TIPO_CARGO { get; set; } = string.Empty;
    public bool? FLG_GERENCIA { get; set; }
    public bool? FLG_SUB_GERENCIA { get; set; }
    public bool? FLG_JEFATURA { get; set; }
    public bool? FLG_SUB_JEFATURA { get; set; }
    public bool? FLG_SUPERVISOR { get; set; }
    public bool? FLG_ANALISTA { get; set; }
    public bool? FLG_ASISTENTE { get; set; }
    public bool? FLG_OPERARIO { get; set; }
}

public class TiposCargoAdministrativoInsertDto
{
    public int? COD_TIPO_CARGO { get; set; }
    [Required] public string DES_TIPO_CARGO { get; set; } = string.Empty;
    public bool? FLG_GERENCIA { get; set; }
    public bool? FLG_SUB_GERENCIA { get; set; }
    public bool? FLG_JEFATURA { get; set; }
    public bool? FLG_SUB_JEFATURA { get; set; }
    public bool? FLG_SUPERVISOR { get; set; }
    public bool? FLG_ANALISTA { get; set; }
    public bool? FLG_ASISTENTE { get; set; }
    public bool? FLG_OPERARIO { get; set; }
}

public class TiposCargoAdministrativoUpdateDto
{
    public int? COD_TIPO_CARGO { get; set; }
    [Required] public string DES_TIPO_CARGO { get; set; } = string.Empty;
    public bool? FLG_GERENCIA { get; set; }
    public bool? FLG_SUB_GERENCIA { get; set; }
    public bool? FLG_JEFATURA { get; set; }
    public bool? FLG_SUB_JEFATURA { get; set; }
    public bool? FLG_SUPERVISOR { get; set; }
    public bool? FLG_ANALISTA { get; set; }
    public bool? FLG_ASISTENTE { get; set; }
    public bool? FLG_OPERARIO { get; set; }
}

public class TiposCargoAdministrativoSearchDto
{
    public string? Search { get; set; }
}
