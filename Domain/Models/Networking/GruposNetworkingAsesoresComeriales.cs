using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad GRUPOS_NETWORKING_ASESORES_COMERIALES - tipos equivalentes a la BD.</summary>
public class GruposNetworkingAsesoresComeriales
{
    public int? NUM_GRUPO_NETWORKING { get; set; }
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_ALIAS_NETWORKING { get; set; } = string.Empty;
    public string DES_GRUPO_NETWORKING { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class GruposNetworkingAsesoresComerialesInsertDto
{
    public int? NUM_GRUPO_NETWORKING { get; set; }
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposNetworkingAsesoresComerialesUpdateDto
{
    public int? NUM_GRUPO_NETWORKING { get; set; }
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_SALIDA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposNetworkingAsesoresComerialesSearchDto
{
    public string? Search { get; set; }
}