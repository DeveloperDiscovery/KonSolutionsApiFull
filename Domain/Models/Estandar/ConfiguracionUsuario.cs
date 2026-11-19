using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.CONFIGURACION_USUARIO — preferencias propias de cada usuario.</summary>
public class ConfiguracionUsuario
{
    public string   COD_USUARIO                    { get; set; } = "";
    public int?     TAS_ZOOM_WEB                   { get; set; }
    public int?     NUM_MINUTOS_LOCKOUT_SCREEN     { get; set; }
    public byte[]?  IMG_FONDO_WORKSPACE            { get; set; }
    public bool?    FLG_NOTIFICACION_ACTIVA        { get; set; }
    public bool?    FLG_USUARIO_SURDO              { get; set; }
    public bool?    FLG_TECLADO_VIRTUAL            { get; set; }
    public int?     NUM_ROWS_PAGE                  { get; set; }
    public string?  COD_TIPO_MENU_OPCIONES         { get; set; }
    public string   COD_USUARIO_REGISTRO           { get; set; } = "";
    public string   COD_ESTACION_REGISTRO          { get; set; } = "";
    public DateTime? FEC_REGISTRO                  { get; set; }
    public string   COD_USUARIO_ACTUALIZACION      { get; set; } = "";
    public string   COD_ESTACION_ACTUALIZACION     { get; set; } = "";
    public DateTime? FEC_ACTUALIZACION             { get; set; }
}

/// <summary>DTO upsert — campos editables (sin auditoría, COD_USUARIO se toma de la sesión).</summary>
public class ConfiguracionUsuarioSaveDto
{
    [Required] public string COD_USUARIO           { get; set; } = "";
    public int?     TAS_ZOOM_WEB                   { get; set; }
    public int?     NUM_MINUTOS_LOCKOUT_SCREEN     { get; set; }
    public byte[]?  IMG_FONDO_WORKSPACE            { get; set; }
    public bool?    FLG_NOTIFICACION_ACTIVA        { get; set; }
    public bool?    FLG_USUARIO_SURDO              { get; set; }
    public bool?    FLG_TECLADO_VIRTUAL            { get; set; }
    public int?     NUM_ROWS_PAGE                  { get; set; }
    public string?  COD_TIPO_MENU_OPCIONES         { get; set; }
}

public class ConfiguracionUsuarioSearchDto { public string? Search { get; set; } }
