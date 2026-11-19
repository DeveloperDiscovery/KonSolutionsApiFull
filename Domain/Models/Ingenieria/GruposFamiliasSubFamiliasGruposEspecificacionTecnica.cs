using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>
/// Entidad INGENIERIA.GRUPOS_FAMILIAS_SUB_FAMILIAS_GRUPOS_ESPECIFICACION_TECNICA - catálogo
/// simple de Grupos de Especificación Técnica (código + descripción). Tabla de solo 5
/// columnas, sin auditoría de actualización ni relación a Grupo/Familia/Sub Familia de
/// Producto (esos campos pertenecían a otro módulo, no a este). COD_GRUPO_ESPECIFICACION_TECNICA
/// es IDENTITY — se genera solo, no lo asigna el usuario.
/// </summary>
public class GruposFamiliasSubFamiliasGruposEspecificacionTecnica
{
    public int COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class GruposFamiliasSubFamiliasGruposEspecificacionTecnicaInsertDto
{
    // 0 en alta nueva (IDENTITY la genera el SP); en edición, el código existente para que
    // el SP encuentre la fila en su intento de UPDATE.
    public int COD_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public string? DES_GRUPO_ESPECIFICACION_TECNICA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposFamiliasSubFamiliasGruposEspecificacionTecnicaSearchDto
{
    public string? Search { get; set; }
}
