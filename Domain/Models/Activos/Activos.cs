using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Activos;

/// <summary>Entidad ACTIVOS - tipos equivalentes a la BD.</summary>
public class Activos
{
    public string COD_ACTIVO { get; set; } = string.Empty;
    public string DES_ACTIVO { get; set; } = string.Empty;
    public string COD_GRUPO_ACTIVO { get; set; } = string.Empty;
    public string COD_FAMILIA_ACTIVO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_ACTIVO { get; set; } = string.Empty;
    public string COD_TIPO_ACTIVO { get; set; } = string.Empty;
    public string COD_TAG { get; set; } = string.Empty;
    public string DES_MARCA { get; set; } = string.Empty;
    public string DES_MODELO { get; set; } = string.Empty;
    public string DES_SERIE { get; set; } = string.Empty;
    public int? CAN_ANOS_VIDA_UTIL { get; set; }
    public bool? FLG_ASIGNACION_ENTIDAD { get; set; }
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_ZONIFICACION { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
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
    public string DES_MOTIVO { get; set; } = string.Empty;
    public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_ACTIVO { get; set; } = string.Empty;
    public string DES_ZONIFICACION { get; set; } = string.Empty;
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
}

public class ActivosInsertDto
{
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    [Required] public string DES_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_TAG { get; set; } = string.Empty;
    [Required] public string DES_MARCA { get; set; } = string.Empty;
    [Required] public string DES_MODELO { get; set; } = string.Empty;
    [Required] public string DES_SERIE { get; set; } = string.Empty;
    public int? CAN_ANOS_VIDA_UTIL { get; set; }
    public bool? FLG_ASIGNACION_ENTIDAD { get; set; }
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ZONIFICACION { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosUpdateDto
{
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    [Required] public string DES_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ACTIVO { get; set; } = string.Empty;
    [Required] public string COD_TAG { get; set; } = string.Empty;
    [Required] public string DES_MARCA { get; set; } = string.Empty;
    [Required] public string DES_MODELO { get; set; } = string.Empty;
    [Required] public string DES_SERIE { get; set; } = string.Empty;
    public int? CAN_ANOS_VIDA_UTIL { get; set; }
    public bool? FLG_ASIGNACION_ENTIDAD { get; set; }
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ZONIFICACION { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosSearchDto
{
    public string? Search { get; set; }
}