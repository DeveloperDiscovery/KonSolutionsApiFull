using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

public class BancoColores
{
    public int    COD_BANCO_COLOR   { get; set; }
    public string DES_BANCO_COLOR   { get; set; } = string.Empty;
    public string DES_CODIGO_COLOR  { get; set; } = string.Empty;
}

/// <summary>COD_BANCO_COLOR = 0 en un alta nueva (la BD lo asigna vía IDENTITY);
/// con un valor &gt; 0 el SP actualiza el registro existente (upsert).</summary>
public class BancoColoresSaveDto
{
    public int    COD_BANCO_COLOR  { get; set; }
    [Required] public string DES_BANCO_COLOR  { get; set; } = string.Empty;
    [Required] public string DES_CODIGO_COLOR { get; set; } = string.Empty;
}

public class BancoColoresSearchDto
{
    public string? Search { get; set; }
}
