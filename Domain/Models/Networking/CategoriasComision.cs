using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad CATEGORIAS_COMISION - tipos equivalentes a la BD.</summary>
public class CategoriasComision
{
    public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    public string DES_CATEGORIA_COMISION { get; set; } = string.Empty;
    public decimal? TAS_COMISION { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
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
    public string DES_TIPO_COMISION { get; set; } = string.Empty;
}

public class CategoriasComisionInsertDto
{
    [Required] public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_COMISION { get; set; } = string.Empty;
    public decimal? TAS_COMISION { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    [Required] public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CategoriasComisionUpdateDto
{
    [Required] public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_COMISION { get; set; } = string.Empty;
    public decimal? TAS_COMISION { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    [Required] public string COD_TIPO_CATEGORIA_COMISION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CategoriasComisionSearchDto
{
    public string? Search { get; set; }
}