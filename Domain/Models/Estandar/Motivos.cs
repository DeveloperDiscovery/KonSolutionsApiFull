using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad MOTIVOS - tipos equivalentes a la BD.</summary>
public class Motivos
{
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public string DES_MOTIVO { get; set; } = string.Empty;
    public bool? FLG_DEFAULT { get; set; }
    public bool? FLG_EXPLICAR { get; set; }
    public bool? FLG_AUTOMATICO { get; set; }
    public string COD_MOTIVO_SUNAT { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
}

public class MotivosInsertDto
{
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public bool EsNuevo { get; set; } // true = crear (recalcula COD_MOTIVO); false = editar (respeta COD_MOTIVO, incluso 0)
    [Required] public string DES_MOTIVO { get; set; } = string.Empty;
    public bool? FLG_DEFAULT { get; set; }
    public bool? FLG_EXPLICAR { get; set; }
    public bool? FLG_AUTOMATICO { get; set; }
    /// <summary>Código del motivo en el catálogo de SUNAT (por ejemplo, el catálogo 20 de motivos de traslado
    /// de la guía de remisión). Dos dígitos o vacío: vacío se graba como NULL.</summary>
    [RegularExpression(@"^\d{2}$", ErrorMessage = "El código SUNAT son dos dígitos (por ejemplo, 01).")]
    public string? COD_MOTIVO_SUNAT { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MotivosUpdateDto
{
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    [Required] public string DES_MOTIVO { get; set; } = string.Empty;
    public bool? FLG_DEFAULT { get; set; }
    public bool? FLG_EXPLICAR { get; set; }
    public bool? FLG_AUTOMATICO { get; set; }
    /// <summary>Código del motivo en el catálogo de SUNAT (por ejemplo, el catálogo 20 de motivos de traslado
    /// de la guía de remisión). Dos dígitos o vacío: vacío se graba como NULL.</summary>
    [RegularExpression(@"^\d{2}$", ErrorMessage = "El código SUNAT son dos dígitos (por ejemplo, 01).")]
    public string? COD_MOTIVO_SUNAT { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public byte[]? IMG_PICTURE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class MotivosSearchDto
{
    public string? Search { get; set; }
}