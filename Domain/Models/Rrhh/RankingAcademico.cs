using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad RANKING_ACADEMICO - tipos equivalentes a la BD.</summary>
public class RankingAcademico
{
    public string COD_RANKING_ACADEMICO { get; set; } = string.Empty;
    public string DES_RANKING_ACADEMICO { get; set; } = string.Empty;
}

public class RankingAcademicoInsertDto
{
    [Required] public string COD_RANKING_ACADEMICO { get; set; } = string.Empty;
    [Required] public string DES_RANKING_ACADEMICO { get; set; } = string.Empty;
}

public class RankingAcademicoUpdateDto
{
    [Required] public string COD_RANKING_ACADEMICO { get; set; } = string.Empty;
    [Required] public string DES_RANKING_ACADEMICO { get; set; } = string.Empty;
}

public class RankingAcademicoSearchDto
{
    public string? Search { get; set; }
}