using System.ComponentModel.DataAnnotations;

namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.ESTRUCTURA_PRODUCTO - tipos equivalentes a la BD.
/// Clave COD_GRUPO_PRODUCTO (un registro por Grupo, ya no por Grupo+Familia).</summary>
public class EstructuraProducto
{
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO { get; set; }
    public int? LNG_CORRELATIVO { get; set; }

    // ── Solo lectura: traído por SEARCH/SHOWALL/SHOWBYID/SHOWCOMBOX vía JOIN ──
    public string? DES_GRUPO_PRODUCTO { get; set; }
}

public class EstructuraProductoInsertDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO { get; set; }
    public int? LNG_CORRELATIVO { get; set; }
}

public class EstructuraProductoUpdateDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_INCLUIR_ALIAS_GRUPO_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_ALIAS_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_SUB_FAMILIA_PRODUCTO { get; set; }
    public bool? FLG_INCLUIR_ALIAS_SUB_FAMILIA_PRODUCTO { get; set; }
    public int? LNG_CORRELATIVO { get; set; }
}

public class EstructuraProductoSearchDto
{
    public string? Search { get; set; }
}
