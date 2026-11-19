using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad USUARIOS_SUCURSALES_CALENDARIO - tipos equivalentes a la BD.</summary>
public class UsuariosSucursalesCalendario
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public DateTime? FEC_INICIAL { get; set; }
    public DateTime? FEC_FINAL { get; set; }
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
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
}

public class UsuariosSucursalesCalendarioInsertDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public DateTime? FEC_INICIAL { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosSucursalesCalendarioUpdateDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public DateTime? FEC_INICIAL { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosSucursalesCalendarioSearchDto
{
    public string? Search { get; set; }
}