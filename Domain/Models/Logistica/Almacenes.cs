using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad ALMACENES - tipos equivalentes a la BD.</summary>
public class Almacenes
{
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;
    public string DES_ABREVIADA { get; set; } = string.Empty;
    public string COD_TIPO_ALMACEN { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string? COD_ZONIFICACION { get; set; }
    public bool? FLG_STOCK_NEGATIVO { get; set; }
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    // Polígono del almacén sobre el plano del nivel (ZONIFICACION.IMG_PLANO).
    // Es texto libre (nvarchar(max)): la app guarda acá la lista de vértices.
    public string? DES_POLIGONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_SUB_PROCESO { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string DES_TIPO_ALMACEN { get; set; } = string.Empty;
    public string? DES_ZONIFICACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string? DES_AREA { get; set; }
}

// Solo DES_ALMACEN es obligatorio. El resto de columnas admite NULL en la tabla, y
// COD_ALMACEN lo genera el propio SP (MAX + FNC_ObtenerSiguienteCorrelativo) — al Crear
// llega vacío desde la UI. Ojo: con <Nullable>enable</Nullable>, un string no-nullable es
// implícitamente [Required] para el model binder aunque no lleve el atributo; por eso los
// opcionales van declarados como string? y no como string.
public class AlmacenesInsertDto
{
    public string? COD_ALMACEN { get; set; }
    [Required] public string DES_ALMACEN { get; set; } = string.Empty;
    public string? DES_ABREVIADA { get; set; }
    public string? COD_TIPO_ALMACEN { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_ZONIFICACION { get; set; }
    public bool? FLG_STOCK_NEGATIVO { get; set; }
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    public string? COD_PROCESO { get; set; }
    public string? COD_SUB_PROCESO { get; set; }
    public int? COD_AREA { get; set; }
    public string? DES_POLIGONO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
}

public class AlmacenesSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_LOGISTICA_ALMACENES_LOADCOMBOS: Tipos de almacén, Sucursales y
/// Procesos (los tres combos independientes que carga AlmacenesDialog.razor) en un solo viaje.</summary>
public class AlmacenesLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> TiposAlmacen { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Sucursales   { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Procesos     { get; set; } = new();
}