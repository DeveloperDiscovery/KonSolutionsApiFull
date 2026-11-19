using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class AplicacionesOpciones
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION_PADRE { get; set; } = string.Empty;
    public string DES_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_FORMULARIO { get; set; } = string.Empty;
    /// <summary>Parámetro fijo genérico de la opción (ej. "CLI" para bloquear el Tipo de
    /// Entidad en una pantalla de Entidades reutilizada) — sobrevive a mover la opción de
    /// carpeta porque vive en la misma fila (PROC_SEGURIDAD_OPCIONES_MOVE la arrastra
    /// explícita en el INSERT final junto con el resto de columnas).</summary>
    public string? DES_PARAMETRO { get; set; }
    public bool? FLG_EXIGE_PASSWORD { get; set; }
    public bool? FLG_TECLADO_VIRTUAL { get; set; }
    /// <summary>Permite abrir la MISMA opción en más de una pestaña del workspace a la vez
    /// (ej. dos Transacciones de Almacén abiertas en paralelo). Sin esto, volver a hacer
    /// clic en la opción salta a la pestaña que ya estaba abierta.</summary>
    public bool? FLG_MULTI_LOAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR        { get; set; } = string.Empty;
    public string DES_FORECOLOR        { get; set; } = string.Empty;
    public string DES_ESTADO           { get; set; } = string.Empty;
    public string DES_BACKCOLOR_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR_ESTADO { get; set; } = string.Empty;
    public string DES_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_APLICACION { get; set; } = string.Empty;
    public string? DES_OBJETO { get; set; }
}

public class AplicacionesOpcionesInsertDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
               public string COD_OPCION_APLICACION { get; set; } = string.Empty;
               /// <summary>Vacío (o null) = opción de primer nivel de la aplicación: no tiene padre.</summary>
               public string? COD_OPCION_APLICACION_PADRE { get; set; } = string.Empty;
    [Required] public string DES_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
               /// <summary>Una carpeta no tiene formulario: vacío (o null) es válido.</summary>
               public string? DES_FORMULARIO { get; set; } = string.Empty;
    /// <summary>Parámetro fijo genérico de la opción (ej. "CLI" para bloquear el Tipo de
    /// Entidad en una pantalla de Entidades reutilizada) — sobrevive a mover la opción de
    /// carpeta porque vive en la misma fila (PROC_SEGURIDAD_OPCIONES_MOVE la arrastra
    /// explícita en el INSERT final junto con el resto de columnas).</summary>
    public string? DES_PARAMETRO { get; set; }
    public bool? FLG_EXIGE_PASSWORD { get; set; }
    public bool? FLG_TECLADO_VIRTUAL { get; set; }
    /// <summary>Permite abrir la MISMA opción en más de una pestaña del workspace a la vez
    /// (ej. dos Transacciones de Almacén abiertas en paralelo). Sin esto, volver a hacer
    /// clic en la opción salta a la pestaña que ya estaba abierta.</summary>
    public bool? FLG_MULTI_LOAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string  DES_BACKCOLOR { get; set; } = "";
    public string  DES_FORECOLOR { get; set; } = "";
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    /// <summary>Tabla de negocio que administra esta opción (LOGISTICA.ALMACENES).
    /// Es de lo que depende el permiso de la API, así que TIENE que viajar en el guardado:
    /// si no llega, la modificación la deja en NULL y el rol pierde el permiso sin que nadie
    /// lo note.</summary>
    public string? DES_OBJETO { get; set; }

}

public class AplicacionesOpcionesUpdateDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
               /// <summary>Vacío (o null) = opción de primer nivel de la aplicación: no tiene padre. Antes era [Required] y
               /// una opción de primer nivel (que se puede crear con padre vacío) no se podía volver a guardar:
               /// «The COD_OPCION_APLICACION_PADRE field is required».</summary>
               public string? COD_OPCION_APLICACION_PADRE { get; set; } = string.Empty;
    [Required] public string DES_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPCION_APLICACION { get; set; } = string.Empty;
               /// <summary>Una carpeta no tiene formulario: vacío (o null) es válido.</summary>
               public string? DES_FORMULARIO { get; set; } = string.Empty;
    /// <summary>Parámetro fijo genérico de la opción (ej. "CLI" para bloquear el Tipo de
    /// Entidad en una pantalla de Entidades reutilizada) — sobrevive a mover la opción de
    /// carpeta porque vive en la misma fila (PROC_SEGURIDAD_OPCIONES_MOVE la arrastra
    /// explícita en el INSERT final junto con el resto de columnas).</summary>
    public string? DES_PARAMETRO { get; set; }
    public bool? FLG_EXIGE_PASSWORD { get; set; }
    public bool? FLG_TECLADO_VIRTUAL { get; set; }
    /// <summary>Permite abrir la MISMA opción en más de una pestaña del workspace a la vez
    /// (ej. dos Transacciones de Almacén abiertas en paralelo). Sin esto, volver a hacer
    /// clic en la opción salta a la pestaña que ya estaba abierta.</summary>
    public bool? FLG_MULTI_LOAD { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public string  DES_BACKCOLOR { get; set; } = "";
    public string  DES_FORECOLOR { get; set; } = "";
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    /// <summary>Tabla de negocio que administra esta opción (LOGISTICA.ALMACENES).
    /// Es de lo que depende el permiso de la API, así que TIENE que viajar en el guardado:
    /// si no llega, la modificación la deja en NULL y el rol pierde el permiso sin que nadie
    /// lo note.</summary>
    public string? DES_OBJETO { get; set; }

}

public class AplicacionesOpcionesSearchDto
{
    public string? Search { get; set; }
}

public class AplicacionesOpcionesMoveDto
{
    [System.ComponentModel.DataAnnotations.Required] public string COD_APLICACION { get; set; } = string.Empty;
    [System.ComponentModel.DataAnnotations.Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    [System.ComponentModel.DataAnnotations.Required] public string COD_APLICACION_DESTINO { get; set; } = string.Empty;
    [System.ComponentModel.DataAnnotations.Required] public string COD_OPCION_APLICACION_DESTINO { get; set; } = string.Empty;
}

/// <summary>Resultado de PROC_SEGURIDAD_APLICACIONES_OPCIONES_LOADCOMBOS: Tipo de opción +
/// Aplicación + Estados (filtrados por @COD_TIPO_ESTADO) en un solo viaje para
/// AplicacionesOpcionesDialog.razor. "Objeto (tabla que administra)" queda fuera: es
/// metadata de esquema (ApiRoutes.Sistema.Tablas), no un SP de catálogo.</summary>
public class AplicacionesOpcionesLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposOpciones { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Aplicaciones  { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados       { get; set; } = new();
}