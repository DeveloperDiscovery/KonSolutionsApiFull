using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ENTIDADES - tipos equivalentes a la BD.</summary>
public class Entidades
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? COD_TIPO_PERSONA { get; set; }
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    /// <summary>Solo aplica cuando COD_TIPO_PERSONA es Persona Jurídica — ver el switch
    /// del diálogo (mismo criterio con el que se habilita/deshabilita el campo).</summary>
    public string? DES_PREFIJO_USUARIO_COMERCIAL { get; set; }
    /// <summary>Solo aplica cuando COD_TIPO_PERSONA es Persona Natural — el diálogo
    /// deshabilita este campo (y lo limpia) para Persona Jurídica.</summary>
    public decimal? COD_ENTIDAD_DEPENDENCIA { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public bool? FLG_DOMICILIADO { get; set; }
    public string COD_PAIS { get; set; } = string.Empty;
    public string NUM_DUNS { get; set; } = string.Empty;
    public string COD_USUARIO_RELACIONADO { get; set; } = string.Empty;
    public byte[]? IMG_LOGO { get; set; }
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
    public string DES_PAIS { get; set; } = string.Empty;
    public string DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public string DES_CORREO_INSTITUCIONAL { get; set; } = string.Empty;
    public string DES_USUARIO { get; set; } = string.Empty;
    public string COD_USUARIO { get; set; } = string.Empty;
    /// <summary>Nombre completo de la entidad de dependencia (COD_ENTIDAD_DEPENDENCIA) —
    /// el SP SHOWALL/SHOWBYID/SEARCH ya fue corregido para alias-ear esta columna del join
    /// contra sí misma con sufijo _DEPENDENCIA (evita la colisión con las columnas propias
    /// de la entidad que causaba el bug de "graba mal la info").</summary>
    public string DES_ENTIDAD_DEPENDENCIA { get; set; } = string.Empty;
}

/// <summary>PROC_ESTANDAR_ENTIDADES_SHOWIMAGES — experimento: logos de todas las entidades
/// en un endpoint aparte del SHOWALL principal, para no cargar imágenes en cada fila de
/// una grilla que en realidad solo necesita texto.</summary>
public class EntidadImagen
{
    public decimal? COD_ENTIDAD { get; set; }
    public byte[]? IMG_LOGO { get; set; }
}

public class EntidadesInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? COD_TIPO_PERSONA { get; set; }
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string? DES_PREFIJO_USUARIO_COMERCIAL { get; set; }
    public decimal? COD_ENTIDAD_DEPENDENCIA { get; set; }
    [Required] public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public bool? FLG_DOMICILIADO { get; set; }
    public string? COD_PAIS { get; set; }
    public string? NUM_DUNS { get; set; }
    public string? COD_USUARIO_RELACIONADO { get; set; }
    public byte[]? IMG_LOGO { get; set; }
    /// <summary>Opcional — si viene, el SP INSERT (upsert) además asigna atómicamente esta
    /// entidad al tipo indicado en ESTANDAR.ENTIDADES_TIPOS_ENTIDADES (solo si aún no está
    /// asignada). Usado por pantallas reutilizadas de Entidades que fijan un tipo, ej.
    /// "CLI" para Clientes — ver DES_PARAMETRO en SEGURIDAD.APLICACIONES_OPCIONES.</summary>
    public string? COD_TIPO_ENTIDAD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesSearchDto
{
    public string? Search { get; set; }
    /// <summary>Opcional — filtra por ESTANDAR.ENTIDADES_TIPOS_ENTIDADES (SP _MODULESEARCH)
    /// en vez del SEARCH genérico. Usado por pantallas reutilizadas (ej. "CLI" = Clientes).</summary>
    public string? CodTipoEntidad { get; set; }
    /// <summary>Opcional — filtro multi-selección de chips (ej. 'CLI_PRV_'), enviado tal cual
    /// a PROC_ESTANDAR_ENTIDADES_SEARCH en @DES_TIPOS_ENTIDAD.</summary>
    public string? DesTiposEntidad { get; set; }
}

/// <summary>Resultado de PROC_ESTANDAR_ENTIDADES_LOADCOMBOS: tipos de persona, tipos de
/// documento de identidad, países, estados (filtrados por @COD_TIPO_ESTADO), tipos de medio
/// de contacto y tipos de dirección — en un solo viaje para el diálogo de Entidades.</summary>
public class EntidadesLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposPersonas          { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposDocumentoIdentidad { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Paises                  { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados                 { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposMedioContacto      { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposDireccion          { get; set; } = new();
}