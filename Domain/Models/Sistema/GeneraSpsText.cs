using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Sistema;

public class SpEncontrado
{
    public string Nombre { get; set; } = string.Empty;
    public string? Definicion { get; set; }
}

public class ColumnaTabla
{
    public string SchemaName { get; set; } = string.Empty;
    public string NombreColumna { get; set; } = string.Empty;
    public string TipoDato { get; set; } = string.Empty;
    public int? MaxLength { get; set; }
    public int? Precision { get; set; }
    public int? Scale { get; set; }
    public bool EsNullable { get; set; }
    public string? ValorPorDefecto { get; set; }
    public int OrdinalPosition { get; set; }
}

public class GenerarSpsTextRequest
{
    [Required] public string PrefijoSp { get; set; } = string.Empty;
    public string? NombreTabla { get; set; }
}

public class GenerarSpsTextResult
{
    public string RutaArchivo { get; set; } = string.Empty;
    public int CantidadSps { get; set; }
    public bool TablaEncontrada { get; set; }
}
