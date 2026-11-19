using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad CLASES_OPERACION - tipos equivalentes a la BD.</summary>
public class ClasesOperacion
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string DES_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD_RELACIONADA { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_CAMBIO { get; set; }
    public bool? FLG_FINANZAS { get; set; }
    public bool? FLG_RENDICION { get; set; }
    public bool? FLG_PERMUTA { get; set; }
    public bool? FLG_BANCO { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_REGIMEN_ESPECIAL { get; set; }
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_MOTIVO_RELACIONADO { get; set; }
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
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string DES_TIPO_ESTADO { get; set; } = string.Empty;
    public string DES_TIPO_MOTIVO { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
}

public class ClasesOperacionInsertDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string DES_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_RELACIONADA { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_CAMBIO { get; set; }
    public bool? FLG_FINANZAS { get; set; }
    public bool? FLG_RENDICION { get; set; }
    public bool? FLG_PERMUTA { get; set; }
    public bool? FLG_BANCO { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_REGIMEN_ESPECIAL { get; set; }
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_MOTIVO_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ClasesOperacionUpdateDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string DES_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_RELACIONADA { get; set; } = string.Empty;
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_CAMBIO { get; set; }
    public bool? FLG_FINANZAS { get; set; }
    public bool? FLG_RENDICION { get; set; }
    public bool? FLG_PERMUTA { get; set; }
    public bool? FLG_BANCO { get; set; }
    public bool? FLG_CAJA { get; set; }
    public bool? FLG_REGIMEN_ESPECIAL { get; set; }
    public int? COD_TIPO_ESTADO_RELACIONADO { get; set; }
    public int? COD_TIPO_MOTIVO_RELACIONADO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ClasesOperacionSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_ESTANDAR_CLASES_OPERACION_LOADCOMBOS (SP ya existente en BD,
/// antes huérfano): TiposEntidades, TiposEstados, TiposMotivos y Estado (filtrado por
/// @COD_TIPO_ESTADO_CLASES_OPERACION) en un solo viaje para ClaseOperacionDialog.razor.</summary>
public class ClasesOperacionLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposEntidades { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposEstados   { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposMotivos   { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados        { get; set; } = new();
}