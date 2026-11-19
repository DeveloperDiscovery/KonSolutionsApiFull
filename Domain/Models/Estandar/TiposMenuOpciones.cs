using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

public class TiposMenuOpciones
{
    public string COD_TIPO_MENU_OPCIONES { get; set; } = string.Empty;
    public string? DES_TIPO_MENU_OPCIONES { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposMenuOpcionesInsertDto
{
    [Required] public string COD_TIPO_MENU_OPCIONES { get; set; } = string.Empty;
    public string? DES_TIPO_MENU_OPCIONES { get; set; }
    public byte[]? IMG_ICONO { get; set; }
}

public class TiposMenuOpcionesSearchDto
{
    public string? Search { get; set; }
}
