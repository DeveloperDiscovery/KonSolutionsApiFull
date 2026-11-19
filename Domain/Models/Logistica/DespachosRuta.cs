using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad DESPACHOS_RUTA - tipos equivalentes a la BD.</summary>
public class DespachosRuta
{
    public int? NUM_DESPACHO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public decimal? VAL_LATITUD_ORIGEN { get; set; }
    public decimal? VAL_LONGITUD_ORIGEN { get; set; }
    public decimal? VAL_LATITUD_DESTINO { get; set; }
    public decimal? VAL_LONGITUD_DESTINO { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DESPACHO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class DespachosRutaInsertDto
{
    public int? NUM_DESPACHO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public decimal? VAL_LATITUD_ORIGEN { get; set; }
    public decimal? VAL_LONGITUD_ORIGEN { get; set; }
    public decimal? VAL_LATITUD_DESTINO { get; set; }
    public decimal? VAL_LONGITUD_DESTINO { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DespachosRutaUpdateDto
{
    public int? NUM_DESPACHO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public decimal? VAL_LATITUD_ORIGEN { get; set; }
    public decimal? VAL_LONGITUD_ORIGEN { get; set; }
    public decimal? VAL_LATITUD_DESTINO { get; set; }
    public decimal? VAL_LONGITUD_DESTINO { get; set; }
    public DateTime? FEC_ENTREGA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DespachosRutaSearchDto
{
    public string? Search { get; set; }
}