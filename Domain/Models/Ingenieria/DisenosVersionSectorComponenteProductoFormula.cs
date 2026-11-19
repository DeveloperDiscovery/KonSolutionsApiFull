using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO_FORMULA - tipos equivalentes a la BD.</summary>
public class DisenosVersionSectorComponenteProductoFormula
{
    public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string COD_SECTOR_PRODUCTO { get; set; } = string.Empty;
    public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO_COMPONENTE { get; set; } = string.Empty;
    public int? NUM_SECUENCIA_FORMULA { get; set; }
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    public string DES_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public string DES_ELEMENTO_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_FORMULA { get; set; } = string.Empty;
    public string DES_MASCARA_FORMULA { get; set; } = string.Empty;
    public string DES_TIPO_ELEMENTO_FORMULA { get; set; } = string.Empty;
    public string DES_TIPO_MASCARA_FORMULA { get; set; } = string.Empty;
}

public class DisenosVersionSectorComponenteProductoFormulaInsertDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_SECTOR_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO_COMPONENTE { get; set; } = string.Empty;
    public int? NUM_SECUENCIA_FORMULA { get; set; }
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_ELEMENTO_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_ELEMENTO_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosVersionSectorComponenteProductoFormulaUpdateDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_SECTOR_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO_COMPONENTE { get; set; } = string.Empty;
    public int? NUM_SECUENCIA_FORMULA { get; set; }
    public int? COD_CLASE_ELEMENTO_FORMULA { get; set; }
    public int? COD_TIPO_ELEMENTO_FORMULA { get; set; }
    [Required] public string DES_ELEMENTO_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_ELEMENTO_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosVersionSectorComponenteProductoFormulaSearchDto
{
    public string? Search { get; set; }
}