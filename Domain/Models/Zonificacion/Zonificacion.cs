using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Zonificacion;

/// <summary>Entidad ZONIFICACION.ZONIFICACION — hija de ESTANDAR.SUCURSALES y jerárquica
/// (COD_ZONIFICACION_PADRE apunta a otra zonificación de la misma sucursal).
/// PK compuesta: COD_SUCURSAL + COD_ZONIFICACION.
/// IMG_PLANO + VAL_ANCHO_METROS/VAL_LARGO_METROS son la base de la vista geométrica:
/// el plano del nivel y su escala real en metros.</summary>
public class Zonificacion
{
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_ZONIFICACION { get; set; } = string.Empty;
    public string? DES_ZONIFICACION { get; set; }
    public string? COD_ZONIFICACION_PADRE { get; set; }
    public string? COD_TIPO_ZONIFICACION { get; set; }
    public int? NUM_NIVEL { get; set; }
    public int? NUM_NIVEL_ORDEN { get; set; }
    public int? VAL_ANCHO_METROS { get; set; }
    public int? VAL_LARGO_METROS { get; set; }
    public bool? FLG_MOSTRAR_DESCENDENTE { get; set; }
    public byte[]? IMG_PLANO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura (JOINs). Ojo: la columna del SP es DES_DIRECCION (una sola C intermedia);
    // el andamiaje la tenía como DES_DIRECCION y por eso llegaba siempre vacía.
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? DES_DIRECCION { get; set; }
    public string? DES_LINK_PATH_FACTURACION { get; set; }
    public string? DES_SUCURSAL { get; set; }
    public string? DES_TOKEN_FACTURACION { get; set; }
    public string? DES_TIPO_ZONIFICACION { get; set; }
}

/// <summary>El SP _INSERT hace upsert. Al crear, COD_ZONIFICACION va vacío: el SP arma el
/// código como COD_ZONIFICACION_PADRE + correlativo de 3 dígitos. Por eso no lleva [Required]
/// —con &lt;Nullable&gt;enable&lt;/Nullable&gt; el binder lo exigiría y devolvería 400 al crear.
/// Usuario y estación tampoco van en el DTO: salen del token en el controller.</summary>
public class ZonificacionInsertDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public string? COD_ZONIFICACION { get; set; }
    [Required] public string DES_ZONIFICACION { get; set; } = string.Empty;
    public string? COD_ZONIFICACION_PADRE { get; set; }
    public string? COD_TIPO_ZONIFICACION { get; set; }
    public int? NUM_NIVEL { get; set; }
    public int? NUM_NIVEL_ORDEN { get; set; }
    public int? VAL_ANCHO_METROS { get; set; }
    public int? VAL_LARGO_METROS { get; set; }
    public bool? FLG_MOSTRAR_DESCENDENTE { get; set; }
    public byte[]? IMG_PLANO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class ZonificacionSearchDto
{
    public string? COD_SUCURSAL { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_ZONIFICACION_ZONIFICACION_LOADCOMBOS: tipos de zonificación +
/// zonificaciones de la sucursal (para el combo "Zona padre") + estados (filtrados por
/// @COD_TIPO_ESTADO) en un solo viaje para ZonificacionDialog.razor.</summary>
public class ZonificacionLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposZonificacion { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Zonificaciones    { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados           { get; set; } = new();
}
