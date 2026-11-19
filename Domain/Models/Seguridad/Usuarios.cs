using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad USUARIOS - tipos equivalentes a la BD.</summary>
public class Usuarios
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
    public string COD_PERFIL_USUARIO { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string NUM_TELEFONO { get; set; } = string.Empty;
    public string COD_PASSWORD { get; set; } = string.Empty;
    public bool? FLG_VALIDA_DIRECTORIO_ACTIVO { get; set; }
    public bool? FLG_LOGIN_CREDENCIALES { get; set; }
    public bool? FLG_LOGIN_FACE { get; set; }
    public bool? FLG_LOGIN_HUELLA_DACTILAR { get; set; }
    public int? NUM_MAXIMO_INTENTOS { get; set; }
    public int? NUM_INTENTOS { get; set; }
    public int? NUM_DIAS_PASSWORD_RESET { get; set; }
    public DateTime? FEC_ULTIMA_PASSWORD_CHANGE { get; set; }
    public byte[]? IMG_FOTO { get; set; }
    public byte[]? IMG_FIRMA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public bool? FLG_REGISTRO_FACIAL { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_PERFIL_USUARIO { get; set; } = string.Empty;
}

public class UsuariosInsertDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_PERFIL_USUARIO { get; set; } = string.Empty;
    public string? DES_CORREO_INSTITUCIONAL { get; set; }
    public string? NUM_TELEFONO { get; set; }
    public string? COD_PASSWORD { get; set; }
    public bool? FLG_VALIDA_DIRECTORIO_ACTIVO { get; set; }
    public bool? FLG_LOGIN_CREDENCIALES { get; set; }
    public bool? FLG_LOGIN_FACE { get; set; }
    public bool? FLG_LOGIN_HUELLA_DACTILAR { get; set; }
    public int? NUM_MAXIMO_INTENTOS { get; set; }
    public int? NUM_INTENTOS { get; set; }
    public int? NUM_DIAS_PASSWORD_RESET { get; set; }
    public byte[]? IMG_FOTO { get; set; }
    public byte[]? IMG_FIRMA { get; set; }
    public bool? FLG_REGISTRO_FACIAL { get; set; }
    /// <summary>Entidad (ESTANDAR.ENTIDADES) que se vincula a este usuario — el SP INSERT
    /// hace UPDATE ESTANDAR.ENTIDADES SET COD_USUARIO_RELACIONADO = @COD_USUARIO ahí. Se
    /// llena al elegir una sugerencia (Usuario Comercial / Usuario de Dependencia Comercial);
    /// si viene null, ese UPDATE simplemente no afecta ninguna fila.</summary>
    public decimal? COD_ENTIDAD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosUpdateDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_PERFIL_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    [Required] public string NUM_TELEFONO { get; set; } = string.Empty;
    [Required] public string COD_PASSWORD { get; set; } = string.Empty;
    public bool? FLG_VALIDA_DIRECTORIO_ACTIVO { get; set; }
    public bool? FLG_LOGIN_CREDENCIALES { get; set; }
    public bool? FLG_LOGIN_FACE { get; set; }
    public bool? FLG_LOGIN_HUELLA_DACTILAR { get; set; }
    public int? NUM_MAXIMO_INTENTOS { get; set; }
    public int? NUM_INTENTOS { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosSearchDto
{
    public string? Search { get; set; }
}

public class UsuariosEstadosDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    public int? COD_ESTADO { get; set; }
}

public class UsuariosPasswordDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_PASSWORD { get; set; } = string.Empty;
    public string? COD_ROL_USUARIO_ACTIVO { get; set; }
}

/// <summary>DTO para el reset de password (PROC_SEGURIDAD_USUARIOS_PASSWORDRESET).</summary>
public class UsuariosPasswordResetDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
}

public class UsuariosResetintentosDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
}

public class UsuariosValidaloginDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_PASSWORD { get; set; } = string.Empty;
}

/// <summary>Resultado de PROC_SEGURIDAD_USUARIOS_LOADCOMBOS: Perfil + Dominios de correo +
/// Estados (filtrados por @COD_TIPO_ESTADO) en un solo viaje para UsuarioDialog.razor.</summary>
public class UsuariosLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> Perfiles         { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> DominiosEmpresa  { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados          { get; set; } = new();
}
