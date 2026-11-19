using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

public class AplicacionesOpcionesBotones
{
    public string COD_APLICACION { get; set; } = string.Empty;
    public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string COD_BOTON_OPCION { get; set; } = string.Empty;
    public string DES_BOTON_OPCION { get; set; } = string.Empty;
    public string DES_TOOLTIPTEXT { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string COD_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public bool FLG_GRILLA { get; set; }
    public bool FLG_HEADER { get; set; }
    public bool FLG_DIALOG { get; set; }
    // Verbos HTTP que este botón habilita — usados para autorización por rol,
    // sin depender de qué "tipo" de botón es (Nuevo/Eliminar/proceso/etc.).
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    // Nombre del método del Controller de la API a invocar dinámicamente desde el
    // frontend (ej. "Insert", "Delete", "GetById") — sin schema/tabla por delante,
    // ya que la tabla/ruta se resuelve por otro lado (DES_FORMULARIO de la opción).
    public string? DES_METODO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_FORMULARIO { get; set; } = string.Empty;
    public string DES_OPCION_APLICACION { get; set; } = string.Empty;
    public string DES_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class AplicacionesOpcionesBotonesInsertDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    public string? COD_BOTON_OPCION { get; set; }
    [Required] public string DES_BOTON_OPCION { get; set; } = string.Empty;
    public string? DES_TOOLTIPTEXT { get; set; }
    public string? DES_ALIAS { get; set; }
    // Null en el insert; en el update conserva el valor existente (ver repositorio).
    public string? COD_BOTON_OPCION_ESTANDAR { get; set; }
    public byte[]? IMG_ICONO { get; set; }
    public bool FLG_GRILLA { get; set; }
    public bool FLG_HEADER { get; set; }
    public bool FLG_DIALOG { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AplicacionesOpcionesBotonesUpdateDto
{
    [Required] public string COD_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_OPCION_APLICACION { get; set; } = string.Empty;
    [Required] public string COD_BOTON_OPCION { get; set; } = string.Empty;
    [Required] public string DES_BOTON_OPCION { get; set; } = string.Empty;
    [Required] public string DES_TOOLTIPTEXT { get; set; } = string.Empty;
    [Required] public string DES_ALIAS { get; set; } = string.Empty;
    [Required] public string COD_BOTON_OPCION_ESTANDAR { get; set; } = string.Empty;
    public byte[]? IMG_ICONO { get; set; }
    public bool? FLG_ACCION_POST { get; set; }
    public bool? FLG_ACCION_PUT { get; set; }
    public bool? FLG_ACCION_DELETE { get; set; }
    public bool? FLG_ACCION_GET { get; set; }
    public string? DES_METODO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AplicacionesOpcionesBotonesSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_LOADCOMBOS: Botón
/// estándar + Estados (filtrados por @COD_TIPO_ESTADO) en un solo viaje para
/// AplicacionesOpcionesBotonesDialog.razor. "Método de la API" queda fuera: es reflexión
/// sobre Controllers dependiente de la opción padre, no un SP de catálogo.</summary>
public class AplicacionesOpcionesBotonesLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> BotonesEstandar { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados         { get; set; } = new();
}