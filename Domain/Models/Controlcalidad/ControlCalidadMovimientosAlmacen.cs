using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Controlcalidad;

/// <summary>Entidad CONTROL_CALIDAD_MOVIMIENTOS_ALMACEN - tipos equivalentes a la BD.</summary>
public class ControlCalidadMovimientosAlmacen
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_APROBADOR { get; set; }
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_AUTORIZACION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PATH_FILE { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_NO { get; set; } = string.Empty;
    public string DES_QUERY_EXECUTE_SI { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class ControlCalidadMovimientosAlmacenInsertDto
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_APROBADOR { get; set; }
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ControlCalidadMovimientosAlmacenUpdateDto
{
    public int? NUM_CONTROL_CALIDAD { get; set; }
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_EVALUADOR { get; set; }
    public decimal? COD_ENTIDAD_APROBADOR { get; set; }
    public int? NUM_AUTORIZACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ControlCalidadMovimientosAlmacenSearchDto
{
    public string? Search { get; set; }
}