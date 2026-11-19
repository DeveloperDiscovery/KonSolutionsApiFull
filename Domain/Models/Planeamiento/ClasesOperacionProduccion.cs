using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad CLASES_OPERACION_PRODUCCION - tipos equivalentes a la BD.</summary>
public class ClasesOperacionProduccion
{
    public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesOperacionProduccionInsertDto
{
    [Required] public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesOperacionProduccionUpdateDto
{
    [Required] public string COD_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_CLASE_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class ClasesOperacionProduccionSearchDto
{
    public string? Search { get; set; }
}