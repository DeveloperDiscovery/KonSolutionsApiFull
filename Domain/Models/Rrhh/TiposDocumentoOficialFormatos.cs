using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_DOCUMENTO_OFICIAL_FORMATOS - tipos equivalentes a la BD.</summary>
public class TiposDocumentoOficialFormatos
{
    public int? NUM_FORMATO_DOCUMENTO { get; set; }
    public string DES_FORMATO_DOCUMENTO { get; set; } = string.Empty;
    public string COD_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
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
    public string DES_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
}

public class TiposDocumentoOficialFormatosInsertDto
{
    public int? NUM_FORMATO_DOCUMENTO { get; set; }
    [Required] public string DES_FORMATO_DOCUMENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposDocumentoOficialFormatosUpdateDto
{
    public int? NUM_FORMATO_DOCUMENTO { get; set; }
    [Required] public string DES_FORMATO_DOCUMENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposDocumentoOficialFormatosSearchDto
{
    public string? Search { get; set; }
}