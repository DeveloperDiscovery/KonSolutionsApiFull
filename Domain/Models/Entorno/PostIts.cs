using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad ENTORNO.POSTITS - notas adhesivas personales del usuario.</summary>
public class PostIts
{
    public long NUM_POSTIT { get; set; }
    public string COD_USUARIO { get; set; } = string.Empty;
    public string DES_TITULO { get; set; } = string.Empty;
    public string DES_POSTIT { get; set; } = string.Empty;
    public int POS_LEFT { get; set; }
    public int POS_TOP { get; set; }
}

public class PostItsInsertDto
{
    [Required] public long NUM_POSTIT { get; set; }
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    public string DES_TITULO { get; set; } = string.Empty;
    public string DES_POSTIT { get; set; } = string.Empty;
    public int POS_LEFT { get; set; }
    public int POS_TOP { get; set; }
}

public class PostItsSearchDto
{
    public string? Search { get; set; }
}
