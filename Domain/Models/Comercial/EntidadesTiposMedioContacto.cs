using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_TIPOS_MEDIO_CONTACTO - tipos equivalentes a la BD.
/// Clave: COD_ENTIDAD + NUM_SECUENCIA (auto-generado por el SP INSERT, ver
/// PROC_COMERCIAL_ENTIDADES_TIPOS_MEDIO_CONTACTO_INSERT) — permite varias filas
/// del mismo COD_TIPO_MEDIO_CONTACTO para una misma entidad.</summary>
public class EntidadesTiposMedioContacto
{
    public decimal? COD_ENTIDAD { get; set; }
    public short? NUM_SECUENCIA { get; set; }
    public int? COD_TIPO_MEDIO_CONTACTO { get; set; }
    public string DAT_TIPO_MEDIO_CONTACTO { get; set; } = string.Empty;
    public string? COD_TIPO_USO { get; set; }
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
    public string DES_TIPO_MEDIO_CONTACTO { get; set; } = string.Empty;
    public string DES_TIPO_USO { get; set; } = string.Empty;
}

/// <summary>DTO de guardado. PROC_..._INSERT es upsert: si NUM_SECUENCIA viene en 0/null
/// se trata como alta (el SP calcula el siguiente correlativo por COD_ENTIDAD); si viene
/// con un valor existente, actualiza esa fila.</summary>
public class EntidadesTiposMedioContactoInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public short? NUM_SECUENCIA { get; set; }
    public int? COD_TIPO_MEDIO_CONTACTO { get; set; }
    [Required] public string DAT_TIPO_MEDIO_CONTACTO { get; set; } = string.Empty;
    public string? COD_TIPO_USO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesTiposMedioContactoSearchDto
{
    public decimal COD_ENTIDAD { get; set; }
    public string? Search { get; set; }
}
