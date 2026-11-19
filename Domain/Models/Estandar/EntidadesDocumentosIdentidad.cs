using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ENTIDADES_DOCUMENTOS_IDENTIDAD - tipos equivalentes a la BD.</summary>
public class EntidadesDocumentosIdentidad
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public DateTime? FEC_CADUCIDAD { get; set; }
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
    public string DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
}

public class EntidadesDocumentosIdentidadInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public DateTime? FEC_CADUCIDAD { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesDocumentosIdentidadUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public DateTime? FEC_CADUCIDAD { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesDocumentosIdentidadSearchDto
{
    public decimal COD_ENTIDAD { get; set; }
    public string? Search { get; set; }
}