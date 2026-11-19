using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad DOCUMENTOS_OFICIALES_PERSONAL - tipos equivalentes a la BD.</summary>
public class DocumentosOficialesPersonal
{
    public int? NUM_DOCUMENTO_OFICIAL { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string DES_COMENTARIOS { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public string DES_FORMATO_DOCUMENTO { get; set; } = string.Empty;
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

public class DocumentosOficialesPersonalInsertDto
{
    public int? NUM_DOCUMENTO_OFICIAL { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string DES_COMENTARIOS { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DocumentosOficialesPersonalUpdateDto
{
    public int? NUM_DOCUMENTO_OFICIAL { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string DES_COMENTARIOS { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DocumentosOficialesPersonalSearchDto
{
    public string? Search { get; set; }
}