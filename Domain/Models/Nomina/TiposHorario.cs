using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad TIPOS_HORARIO - tipos equivalentes a la BD.</summary>
public class TiposHorario
{
    public string COD_TIPO_HORARIO { get; set; } = string.Empty;
    public string DES_TIPO_HORARIO { get; set; } = string.Empty;
    public bool? FLG_SEMANAL { get; set; }
    public bool? FLG_QUINCENAL { get; set; }
    public bool? FLG_MENSUAL { get; set; }
}

public class TiposHorarioInsertDto
{
    [Required] public string COD_TIPO_HORARIO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_HORARIO { get; set; } = string.Empty;
    public bool? FLG_SEMANAL { get; set; }
    public bool? FLG_QUINCENAL { get; set; }
    public bool? FLG_MENSUAL { get; set; }
}

public class TiposHorarioUpdateDto
{
    [Required] public string COD_TIPO_HORARIO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_HORARIO { get; set; } = string.Empty;
    public bool? FLG_SEMANAL { get; set; }
    public bool? FLG_QUINCENAL { get; set; }
    public bool? FLG_MENSUAL { get; set; }
}

public class TiposHorarioSearchDto
{
    public string? Search { get; set; }
}