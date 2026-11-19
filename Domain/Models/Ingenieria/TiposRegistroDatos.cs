using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad INGENIERIA.TIPOS_REGISTRO_DATOS - catálogo simple usado por
/// GRUPOS_FAMILIAS_SUB_FAMILIAS_ESPECIFICACION_TECNICA (COD_TIPO_REGISTRO_DATOS).</summary>
public class TiposRegistroDatos
{
    public string COD_TIPO_REGISTRO_DATOS { get; set; } = string.Empty;
    public string? DES_TIPO_REGISTRO_DATOS { get; set; }
}

public class TiposRegistroDatosInsertDto
{
    [Required] public string COD_TIPO_REGISTRO_DATOS { get; set; } = string.Empty;
    public string? DES_TIPO_REGISTRO_DATOS { get; set; }
}

public class TiposRegistroDatosSearchDto
{
    public string? Search { get; set; }
}
