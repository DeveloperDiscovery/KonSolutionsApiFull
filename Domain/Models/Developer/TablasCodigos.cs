using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Developer;

/// <summary>Entidad TABLAS_CODIGOS - tipos equivalentes a la BD.</summary>
public class TablasCodigos
{
    public string NAME_SCHEMA { get; set; } = string.Empty;
    public string NAME_TABLA { get; set; } = string.Empty;
    public string FIELD_CORRELATIVO { get; set; } = string.Empty;
    public string FIELD_PARENT { get; set; } = string.Empty;
    public string FIELD_WHERE { get; set; } = string.Empty;
    public bool? IS_NUMERIC { get; set; }
    public int? FRACCION_SEGMENTO { get; set; }
    public int? LONGITUD_MAXIMA { get; set; }
}

public class TablasCodigosInsertDto
{
    [Required] public string NAME_SCHEMA { get; set; } = string.Empty;
    [Required] public string NAME_TABLA { get; set; } = string.Empty;
    [Required] public string FIELD_CORRELATIVO { get; set; } = string.Empty;
    [Required] public string FIELD_PARENT { get; set; } = string.Empty;
    [Required] public string FIELD_WHERE { get; set; } = string.Empty;
    public bool? IS_NUMERIC { get; set; }
    public int? FRACCION_SEGMENTO { get; set; }
    public int? LONGITUD_MAXIMA { get; set; }
}

public class TablasCodigosUpdateDto
{
    [Required] public string NAME_SCHEMA { get; set; } = string.Empty;
    [Required] public string NAME_TABLA { get; set; } = string.Empty;
    [Required] public string FIELD_CORRELATIVO { get; set; } = string.Empty;
    [Required] public string FIELD_PARENT { get; set; } = string.Empty;
    [Required] public string FIELD_WHERE { get; set; } = string.Empty;
    public bool? IS_NUMERIC { get; set; }
    public int? FRACCION_SEGMENTO { get; set; }
    public int? LONGITUD_MAXIMA { get; set; }
}

public class TablasCodigosSearchDto
{
    public string? Search { get; set; }
}