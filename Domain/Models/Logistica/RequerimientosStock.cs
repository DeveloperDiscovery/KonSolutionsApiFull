using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad REQUERIMIENTOS_STOCK - tipos equivalentes a la BD.</summary>
public class RequerimientosStock
{
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_REQUERIMIENTO_STOCK { get; set; }
    public string COD_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO_REQUIRIENTE { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_REQUIRIENTE { get; set; }

    // El requiriente "original" es quién pidió antes de cualquier reasignación. El SP los graba
    // desde siempre; faltaban acá, así que las dos columnas quedaban en NULL.
    public string? COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL { get; set; }
    public decimal? COD_ENTIDAD_REQUIRIENTE_ORIGINAL { get; set; }

    // El requerimiento del que sale éste. Faltaba en el modelo de la API —sólo estaba en el
    // del cliente—, así que el SHOWBYID lo devolvía y la serialización lo descartaba: la
    // pantalla recibía null y el campo salía vacío por más que el dato estuviera en la base.
    public int? NUM_REQUERIMIENTO_STOCK_ORIGINAL { get; set; }

    // Descripción de ese requerimiento de origen (el LEFT JOIN "e" del SHOWBYID). Sólo lectura:
    // la arma el SELECT, lo que se graba sigue siendo el número.
    public string? DES_REQUERIMIENTO_STOCK_ORIGINAL { get; set; }

    public DateTime? FEC_ATENCION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public int? NUM_PROYECTO { get; set; }
    public string? DES_PROYECTO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class RequerimientosStockInsertDto
{
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string COD_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO_REQUIRIENTE { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_REQUIRIENTE { get; set; }

    // El requiriente "original" es quién pidió antes de cualquier reasignación. El SP los graba
    // desde siempre; faltaban acá, así que las dos columnas quedaban en NULL.
    public string? COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL { get; set; }
    public decimal? COD_ENTIDAD_REQUIRIENTE_ORIGINAL { get; set; }

    // El SP de alta también exige el número original, igual que el de modificación —no es
    // un campo del formulario, así que cuando viene en null el repositorio usa el número
    // que acaba de asignarse (ver InsertAsync).
    public int? NUM_REQUERIMIENTO_STOCK_ORIGINAL { get; set; }
    public DateTime? FEC_ATENCION { get; set; }
    // Nulable y sin [Required]: DES_OBSERVACION es NULL en la base, la tabla nunca lo exigió.
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    /// <summary>Proyecto al que pertenece (opcional).</summary>
    public int? NUM_PROYECTO { get; set; }
}

public class RequerimientosStockUpdateDto
{
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string COD_TIPO_REQUERIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO_REQUIRIENTE { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_REQUIRIENTE { get; set; }

    // El requiriente "original" es quién pidió antes de cualquier reasignación. El SP los graba
    // desde siempre; faltaban acá, así que las dos columnas quedaban en NULL.
    public string? COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL { get; set; }
    public decimal? COD_ENTIDAD_REQUIRIENTE_ORIGINAL { get; set; }

    // El SP de UPDATE exige además el número original, como parte de la misma clave anterior
    // que los dos campos de arriba. La pantalla no lo manda porque el correlativo no se edita,
    // así que cuando viene en null el repositorio usa el número actual (ver UpdateAsync).
    public int? NUM_REQUERIMIENTO_STOCK_ORIGINAL { get; set; }
    public DateTime? FEC_ATENCION { get; set; }
    // Nulable y sin [Required]: DES_OBSERVACION es NULL en la base, la tabla nunca lo exigió.
    public string? DES_OBSERVACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    /// <summary>Proyecto al que pertenece (opcional).</summary>
    public int? NUM_PROYECTO { get; set; }
}

public class RequerimientosStockSearchDto
{
    public string? Search { get; set; }

    /// <summary>Filtro por Tipo de Requerimiento. Viaja también en el listado sin búsqueda:
    /// el mismo DTO sirve para el SHOWALL y para el SEARCH, así los dos caminos filtran con
    /// el mismo criterio en vez de comportarse distinto sin que nadie se dé cuenta.
    /// Vacío o null = sin filtro.</summary>
    public string? COD_TIPO_REQUERIMIENTO { get; set; }
}

/// <summary>Resultado de PROC_LOGISTICA_REQUERIMIENTOS_STOCK_LOADCOMBOS: Tipos de requerimiento,
/// Centros de costo, Entidades, Lotes y Productos (los cinco combos independientes que carga
/// RequerimientoDocumento.razor) en un solo viaje. Productos viaja como lista completa —no como
/// ComboboxItem— porque el detalle necesita más datos del maestro (unidad de medida, etc.) al
/// elegir el producto.</summary>
public class RequerimientosStockLoadCombosResult
{
    // Tipos viaja COMPLETO y no como ComboboxItem, por el mismo motivo que Productos: la
    // pantalla necesita el COD_TIPO_ESTADO de cada tipo para armar el combo de Estado —el
    // tipo de estado del requerimiento sale de su Tipo de Requerimiento, no de la tabla.
    public List<TiposRequerimiento> Tipos { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Centros   { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Entidades { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Lotes     { get; set; } = new();
    public List<KonSolutionsApi.Domain.Models.Estandar.Productos> Productos { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Almacenes { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Procesos  { get; set; } = new();
}