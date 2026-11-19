namespace KonSolutionsApi.Domain.Models.Developer;

public class ColumnaMetadata
{
    public string  NombreColumna { get; set; } = "";
    public string? TipoDato      { get; set; }
    public bool    EsNullable    { get; set; }
    public int?    MaxLength     { get; set; }
}
