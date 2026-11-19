using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad EMBALADO_EMBALAJES - tipos equivalentes a la BD.</summary>
public class EmbaladoEmbalajes
{
    public decimal? NUM_EMBALAJE { get; set; }
    public int? NUM_EMBALADO { get; set; }
    public string COD_PRODUCTO_EMBALAJE { get; set; } = string.Empty;
    public string COD_TIPO_EMBALAJE { get; set; } = string.Empty;
    public decimal? NUM_EMBALAJE_PADRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_EMBALADO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_EMBALAJE { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class EmbaladoEmbalajesInsertDto
{
    public decimal? NUM_EMBALAJE { get; set; }
    public int? NUM_EMBALADO { get; set; }
    [Required] public string COD_PRODUCTO_EMBALAJE { get; set; } = string.Empty;
    [Required] public string COD_TIPO_EMBALAJE { get; set; } = string.Empty;
    public decimal? NUM_EMBALAJE_PADRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EmbaladoEmbalajesUpdateDto
{
    public decimal? NUM_EMBALAJE { get; set; }
    public int? NUM_EMBALADO { get; set; }
    [Required] public string COD_PRODUCTO_EMBALAJE { get; set; } = string.Empty;
    [Required] public string COD_TIPO_EMBALAJE { get; set; } = string.Empty;
    public decimal? NUM_EMBALAJE_PADRE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EmbaladoEmbalajesSearchDto
{
    public string? Search { get; set; }
}