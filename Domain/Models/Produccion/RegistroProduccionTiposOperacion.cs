using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad REGISTRO_PRODUCCION_TIPOS_OPERACION - tipos equivalentes a la BD.</summary>
public class RegistroProduccionTiposOperacion
{
    public int? NUM_REGISTRO_PRODUCCION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public decimal? CAN_LEAD_TIME { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
}

public class RegistroProduccionTiposOperacionInsertDto
{
    public int? NUM_REGISTRO_PRODUCCION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public decimal? CAN_LEAD_TIME { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RegistroProduccionTiposOperacionUpdateDto
{
    public int? NUM_REGISTRO_PRODUCCION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public decimal? CAN_LEAD_TIME { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public decimal? CAN_PRODUCIDA_SECUNDARIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class RegistroProduccionTiposOperacionSearchDto
{
    public string? Search { get; set; }
}