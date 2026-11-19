using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad PRODUCCION_RUTAS - tipos equivalentes a la BD.</summary>
public class ProduccionRutas
{
    public int? COD_RUTA_PRODUCCION { get; set; }
    public string DES_RUTA_PRODUCCION { get; set; } = string.Empty;
    public string COD_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
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
    public string DES_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
}

public class ProduccionRutasInsertDto
{
    public int? COD_RUTA_PRODUCCION { get; set; }
    [Required] public string DES_RUTA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProduccionRutasUpdateDto
{
    public int? COD_RUTA_PRODUCCION { get; set; }
    [Required] public string DES_RUTA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_RUTA_PRODUCCION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProduccionRutasSearchDto
{
    public string? Search { get; set; }
}