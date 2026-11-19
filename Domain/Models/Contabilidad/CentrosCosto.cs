using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad CENTROS_COSTO - tipos equivalentes a la BD.</summary>
public class CentrosCosto
{
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string COD_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public string COD_CENTRO_COSTO_PADRE { get; set; } = string.Empty;
    public bool? FLG_GENERA_ASIENTO { get; set; }
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
    public string DES_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
}

public class CentrosCostoInsertDto
{
    // Sin [Required]: al crear (INSERT) viaja vacío/null porque el código lo genera el SP;
    // al actualizar/eliminar viaja con el valor real ya asignado (mismo endpoint upsert).
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public string COD_CENTRO_COSTO_PADRE { get; set; } = string.Empty;
    public bool? FLG_GENERA_ASIENTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CentrosCostoUpdateDto
{
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public string COD_CENTRO_COSTO_PADRE { get; set; } = string.Empty;
    public bool? FLG_GENERA_ASIENTO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CentrosCostoSearchDto
{
    public string? Search { get; set; }
}