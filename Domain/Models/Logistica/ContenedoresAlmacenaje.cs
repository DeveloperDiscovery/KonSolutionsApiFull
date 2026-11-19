using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.CONTENEDORES_ALMACENAJE — el mueble físico que ocupa un
/// espacio del almacén (rack, góndola, jaula…). PK: COD_CONTENEDOR.
///
/// Lleva la geometría (DES_POLIGONO) y la grilla (NUM_FILAS × NUM_COLUMNAS) desde las que
/// se derivan sus casilleros: el polígono se dibuja UNA vez sobre el plano del nivel y la
/// posición de cada casillero se calcula, no se captura.
///
/// OJO con DES_CONTENDOR: así se llama la columna en la BD (le falta una "O"). El nombre
/// del modelo la respeta a propósito para que Dapper mapee sin alias.</summary>
public class ContenedoresAlmacenaje
{
    public string COD_CONTENEDOR { get; set; } = string.Empty;
    public string? DES_CONTENDOR { get; set; }
    public string? COD_TIPO_UBICACION { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_ZONIFICACION { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? DES_POLIGONO { get; set; }
    public int? NUM_FILAS { get; set; }
    public int? NUM_COLUMNAS { get; set; }
    public int? VAL_ALTO_NIVEL_CENTIMETROS { get; set; }
    public int? VAL_LARGO_NIVEL_CENTIMETROS { get; set; }
    public int? VAL_PROFUNDIDAD_NIVEL_CENTIMETROS { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Solo lectura (JOINs).
    public string? DES_TIPO_UBICACION { get; set; }
    public string? DES_ALMACEN { get; set; }
    public string? DES_ABREVIADA { get; set; }
    public string? DES_SUCURSAL { get; set; }
    public string? DES_DIRECCION { get; set; }
    public string? DES_ZONIFICACION { get; set; }
    /// <summary>Contorno del ALMACÉN al que pertenece, ya con su nombre propio en el SP
    /// (c.[DES_POLIGONO] AS [DES_POLIGONO_ALMACENES]). Antes venía con el mismo nombre que el
    /// del contenedor y lo pisaba; el sufijo con la tabla de origen es lo que evita que dos
    /// columnas homónimas compitan por la misma propiedad.</summary>
    public string? DES_POLIGONO_ALMACENES { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_FORECOLOR { get; set; }

    /// <summary>Marca el contenedor como NODO DE DISTRIBUCIÓN: la ubicación a la que van
    /// por defecto los ingresos por compra y las devoluciones de venta.
    ///
    /// Anulable, como la columna: una base con contenedores anteriores al campo los tiene
    /// en NULL, y NULL significa "no es nodo" igual que false. Declararlo no anulable haría
    /// fallar la lectura de esas filas.</summary>
    public bool? FLG_NODO_DISTRIBUCION { get; set; }

}

/// <summary>El SP _INSERT hace upsert: al Crear, COD_CONTENEDOR va vacío y el SP genera el
/// correlativo; con valor, actualiza. Por eso NO lleva [Required] ni string no anulable
/// —con &lt;Nullable&gt;enable&lt;/Nullable&gt; el binder lo exigiría y devolvería 400 al crear.
/// Usuario y estación salen del token, no del cuerpo.</summary>
public class ContenedoresAlmacenajeInsertDto
{
    public string? COD_CONTENEDOR { get; set; }
    [Required] public string DES_CONTENDOR { get; set; } = string.Empty;
    public string? COD_TIPO_UBICACION { get; set; }
    public string? COD_SUCURSAL { get; set; }
    public string? COD_ZONIFICACION { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? DES_POLIGONO { get; set; }
    public int? NUM_FILAS { get; set; }
    public int? NUM_COLUMNAS { get; set; }
    public int? VAL_ALTO_NIVEL_CENTIMETROS { get; set; }
    public int? VAL_LARGO_NIVEL_CENTIMETROS { get; set; }
    public int? VAL_PROFUNDIDAD_NIVEL_CENTIMETROS { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }

    /// <summary>Marca el contenedor como NODO DE DISTRIBUCIÓN: la ubicación a la que van
    /// por defecto los ingresos por compra y las devoluciones de venta.
    ///
    /// Anulable, como la columna: una base con contenedores anteriores al campo los tiene
    /// en NULL, y NULL significa "no es nodo" igual que false. Declararlo no anulable haría
    /// fallar la lectura de esas filas.</summary>
    public bool? FLG_NODO_DISTRIBUCION { get; set; }

}

public class ContenedoresAlmacenajeSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_LOADCOMBOS: Tipos de ubicación,
/// Sucursales y Almacenes (los tres combos independientes que carga
/// ContenedoresAlmacenajeDialog.razor) en un solo viaje.</summary>
public class ContenedoresAlmacenajeLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> TiposUbicacion { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Sucursales     { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Almacenes      { get; set; } = new();
}
