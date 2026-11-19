using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad DISENOS_VERSION_SECTOR_COMPONENTE_PRODUCTO - tipos equivalentes a la BD.</summary>
public class DisenosVersionSectorComponenteProducto
{
    public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    public string COD_SECTOR_PRODUCTO { get; set; } = string.Empty;
    public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO_COMPONENTE { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_CONSUMO { get; set; }
    public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_CONSUMO_SECUNDARIO { get; set; }
    public string COD_UNIDAD_MEDIDA_CAPACIDAD { get; set; } = string.Empty;
    public decimal? CAN_CAPACIDAD { get; set; }
    public string COD_TIPO_MERMA { get; set; } = string.Empty;
    public decimal? TAS_MERMA { get; set; }
    public decimal? CAN_MERMA { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? PRE_TOTAL { get; set; }
    public string DES_FORMULA { get; set; } = string.Empty;
    public string DES_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_MERMA { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class DisenosVersionSectorComponenteProductoInsertDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_SECTOR_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO_COMPONENTE { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_CONSUMO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_CONSUMO_SECUNDARIO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_CAPACIDAD { get; set; } = string.Empty;
    public decimal? CAN_CAPACIDAD { get; set; }
    [Required] public string COD_TIPO_MERMA { get; set; } = string.Empty;
    public decimal? TAS_MERMA { get; set; }
    public decimal? CAN_MERMA { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? PRE_TOTAL { get; set; }
    [Required] public string DES_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosVersionSectorComponenteProductoUpdateDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public int? NUM_VERSION { get; set; }
    [Required] public string COD_SECTOR_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_COMPONENTE_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO_COMPONENTE { get; set; } = string.Empty;
    [Required] public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public decimal? CAN_CONSUMO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; } = string.Empty;
    public decimal? CAN_CONSUMO_SECUNDARIO { get; set; }
    [Required] public string COD_UNIDAD_MEDIDA_CAPACIDAD { get; set; } = string.Empty;
    public decimal? CAN_CAPACIDAD { get; set; }
    [Required] public string COD_TIPO_MERMA { get; set; } = string.Empty;
    public decimal? TAS_MERMA { get; set; }
    public decimal? CAN_MERMA { get; set; }
    [Required] public string COD_MONEDA { get; set; } = string.Empty;
    public decimal? PRE_UNITARIO { get; set; }
    public decimal? PRE_TOTAL { get; set; }
    [Required] public string DES_FORMULA { get; set; } = string.Empty;
    [Required] public string DES_MASCARA_FORMULA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosVersionSectorComponenteProductoSearchDto
{
    public string? Search { get; set; }
}