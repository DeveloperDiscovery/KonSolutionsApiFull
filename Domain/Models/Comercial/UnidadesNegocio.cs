using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad UNIDADES_NEGOCIO - tipos equivalentes a la BD.</summary>
public class UnidadesNegocio
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    public string DES_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public string COD_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
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
    public string DES_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
}

public class UnidadesNegocioInsertDto
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    [Required] public string DES_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UnidadesNegocioUpdateDto
{
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
    [Required] public string DES_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_UNIDAD_NEGOCIO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UnidadesNegocioSearchDto
{
    public string? Search { get; set; }
}