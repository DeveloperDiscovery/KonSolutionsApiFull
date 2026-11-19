using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Developer;

/// <summary>Entidad MENSAJES - tipos equivalentes a la BD.</summary>
public class Mensajes
{
    public string COD_KEY_MENSAJE { get; set; } = string.Empty;
    public string DES_MENSAJE { get; set; } = string.Empty;
    public string DES_ESQUEMA { get; set; } = string.Empty;
    public bool? FLG_HABILITADO { get; set; }
}

public class MensajesInsertDto
{
    [Required] public string COD_KEY_MENSAJE { get; set; } = string.Empty;
    [Required] public string DES_MENSAJE { get; set; } = string.Empty;
    [Required] public string DES_ESQUEMA { get; set; } = string.Empty;
    public bool? FLG_HABILITADO { get; set; }
}

public class MensajesUpdateDto
{
    [Required] public string COD_KEY_MENSAJE { get; set; } = string.Empty;
    [Required] public string DES_MENSAJE { get; set; } = string.Empty;
    [Required] public string DES_ESQUEMA { get; set; } = string.Empty;
    public bool? FLG_HABILITADO { get; set; }
}

public class MensajesSearchDto
{
    public string? Search { get; set; }
}