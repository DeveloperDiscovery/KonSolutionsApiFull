using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad GRUPOS_FAMILIAS_SUB_FAMILIAS_REGLAS_CODIFICACION - tipos equivalentes a la BD.</summary>
public class GruposFamiliasSubFamiliasReglasCodificacion
{
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string DES_PREFIJO { get; set; } = string.Empty;
    public string DES_SINGULAR { get; set; } = string.Empty;
    public string DES_SUFIJO { get; set; } = string.Empty;
    public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public int? COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
}

public class GruposFamiliasSubFamiliasReglasCodificacionInsertDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public int? COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
}

public class GruposFamiliasSubFamiliasReglasCodificacionUpdateDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_ESPECIFICACION_TECNICA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public int? COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
}

public class GruposFamiliasSubFamiliasReglasCodificacionSearchDto
{
    public string? Search { get; set; }
}