using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class PerfilesUsuario
{
    public string COD_PERFIL_USUARIO { get; set; } = string.Empty;
    public string DES_PERFIL_USUARIO { get; set; } = string.Empty;
    public string COD_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    public bool? FLG_DEVELOPER { get; set; }
    public bool? FLG_ADMINISTRADOR { get; set; }
    public bool? FLG_SEGURIDAD { get; set; }
    public bool? FLG_AUDITORIA { get; set; }
    public bool? FLG_COMERCIAL { get; set; }
    public bool? FLG_DEPENDENCIA_COMERCIAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARACTERES_ESPECIALES { get; set; } = string.Empty;
    public string DES_CARACTERES_LETRAS_MAYUSCULAS { get; set; } = string.Empty;
    public string DES_CARACTERES_LETRAS_MINUSCULAS { get; set; } = string.Empty;
    public string DES_CARACTERES_NUMERICOS { get; set; } = string.Empty;
    public string DES_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class PerfilesUsuarioInsertDto
{
    [Required] public string COD_PERFIL_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_PERFIL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    public bool? FLG_DEVELOPER { get; set; }
    public bool? FLG_ADMINISTRADOR { get; set; }
    public bool? FLG_SEGURIDAD { get; set; }
    public bool? FLG_AUDITORIA { get; set; }
    public bool? FLG_COMERCIAL { get; set; }
    public bool? FLG_DEPENDENCIA_COMERCIAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class PerfilesUsuarioUpdateDto
{
    [Required] public string COD_PERFIL_USUARIO { get; set; } = string.Empty;
    [Required] public string DES_PERFIL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_PASSWORD_COMPLEJIDAD { get; set; } = string.Empty;
    public bool? FLG_DEVELOPER { get; set; }
    public bool? FLG_ADMINISTRADOR { get; set; }
    public bool? FLG_SEGURIDAD { get; set; }
    public bool? FLG_AUDITORIA { get; set; }
    public bool? FLG_COMERCIAL { get; set; }
    public bool? FLG_DEPENDENCIA_COMERCIAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
}

public class PerfilesUsuarioSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_SEGURIDAD_PERFILES_USUARIO_LOADCOMBOS: complejidades de
/// contraseña + estados (filtrados por @COD_TIPO_ESTADO) en un solo viaje para
/// PerfilDialog.razor.</summary>
public class PerfilesUsuarioLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> ComplejidadesPassword { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados                { get; set; } = new();
}