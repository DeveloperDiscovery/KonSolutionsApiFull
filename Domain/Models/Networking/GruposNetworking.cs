using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad GRUPOS_NETWORKING - tipos equivalentes a la BD.</summary>
public class GruposNetworking
{
    public int? NUM_GRUPO_NETWORKING { get; set; }
    public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    public string DES_ALIAS_NETWORKING { get; set; } = string.Empty;
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
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
}

public class GruposNetworkingInsertDto
{
    public int? NUM_GRUPO_NETWORKING { get; set; }
    [Required] public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    [Required] public string DES_ALIAS_NETWORKING { get; set; } = string.Empty;
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposNetworkingUpdateDto
{
    public int? NUM_GRUPO_NETWORKING { get; set; }
    [Required] public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    [Required] public string DES_ALIAS_NETWORKING { get; set; } = string.Empty;
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposNetworkingSearchDto
{
    public string? Search { get; set; }
}