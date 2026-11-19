using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad TIPOS_DOCUMENTO_OFICIAL - tipos equivalentes a la BD.</summary>
public class TiposDocumentoOficial
{
    public string COD_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public string DES_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public bool? FLG_VISUALIZACION_INTERNA { get; set; }
    public bool? FLG_ENVIAR_DOCUMENTO_CORREO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
    public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
}

public class TiposDocumentoOficialInsertDto
{
    [Required] public string COD_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public bool? FLG_VISUALIZACION_INTERNA { get; set; }
    public bool? FLG_ENVIAR_DOCUMENTO_CORREO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
}

public class TiposDocumentoOficialUpdateDto
{
    [Required] public string COD_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    [Required] public string DES_TIPO_DOCUMENTO_OFICIAL { get; set; } = string.Empty;
    public bool? FLG_VISUALIZACION_INTERNA { get; set; }
    public bool? FLG_ENVIAR_DOCUMENTO_CORREO { get; set; }
    public int? COD_TIPO_MOTIVO { get; set; }
}

public class TiposDocumentoOficialSearchDto
{
    public string? Search { get; set; }
}