using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad USUARIOS_AUTORIZADORES - tipos equivalentes a la BD.</summary>
public class UsuariosAutorizadores
{
    public string COD_TIPO_AUTORIZACION { get; set; } = string.Empty;
    public string COD_USUARIO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRIORIDAD { get; set; }
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
    public string DES_TIPO_AUTORIZACION { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
}

public class UsuariosAutorizadoresInsertDto
{
    [Required] public string COD_TIPO_AUTORIZACION { get; set; } = string.Empty;
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRIORIDAD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAutorizadoresUpdateDto
{
    [Required] public string COD_TIPO_AUTORIZACION { get; set; } = string.Empty;
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRIORIDAD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAutorizadoresSearchDto
{
    public string? Search { get; set; }
}