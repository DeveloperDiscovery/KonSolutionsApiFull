using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad RUTAS_ARCHIVOS - tipos equivalentes a la BD.</summary>
public class RutasArchivos
{
    public int? NUM_RUTA_ARCHIVO { get; set; }
    public string DES_RUTA_ARCHIVO { get; set; } = string.Empty;
    public string KEY_RUTA_ARCHIVO { get; set; } = string.Empty;
}

public class RutasArchivosInsertDto
{
    public int? NUM_RUTA_ARCHIVO { get; set; }
    [Required] public string DES_RUTA_ARCHIVO { get; set; } = string.Empty;
    [Required] public string KEY_RUTA_ARCHIVO { get; set; } = string.Empty;
}

public class RutasArchivosUpdateDto
{
    public int? NUM_RUTA_ARCHIVO { get; set; }
    [Required] public string DES_RUTA_ARCHIVO { get; set; } = string.Empty;
    [Required] public string KEY_RUTA_ARCHIVO { get; set; } = string.Empty;
}

public class RutasArchivosSearchDto
{
    public string? Search { get; set; }
}