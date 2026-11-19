using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad EMBALADO - tipos equivalentes a la BD.</summary>
public class Embalado
{
    public int? NUM_EMBALADO { get; set; }
    public string DES_EMBALADO { get; set; } = string.Empty;
    public DateTime? FEC_EMBALADO { get; set; }
    public DateTime? FEC_DESPACHO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO_EMBALADO { get; set; } = string.Empty;
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
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
    public string DES_DIRECCION_DESTINO { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_TIPO_EMBALADO { get; set; } = string.Empty;
}

public class EmbaladoInsertDto
{
    public int? NUM_EMBALADO { get; set; }
    [Required] public string DES_EMBALADO { get; set; } = string.Empty;
    public DateTime? FEC_EMBALADO { get; set; }
    public DateTime? FEC_DESPACHO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_EMBALADO { get; set; } = string.Empty;
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EmbaladoUpdateDto
{
    public int? NUM_EMBALADO { get; set; }
    [Required] public string DES_EMBALADO { get; set; } = string.Empty;
    public DateTime? FEC_EMBALADO { get; set; }
    public DateTime? FEC_DESPACHO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_EMBALADO { get; set; } = string.Empty;
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EmbaladoSearchDto
{
    public string? Search { get; set; }
}