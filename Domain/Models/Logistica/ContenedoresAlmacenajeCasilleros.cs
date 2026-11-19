using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.CONTENEDORES_ALMACENAJE_CASILLEROS — cada celda de la grilla
/// de un contenedor. Hija de LOGISTICA.CONTENEDORES_ALMACENAJE.
/// PK compuesta: COD_CONTENEDOR + COD_CASILLERO.
///
/// Cada casillero apunta a una UBICACIÓN (COD_ALMACEN + COD_UBICACION_ALMACEN): esa es la
/// dirección a la que apuntan stocks y movimientos. El casillero es el mueble; la ubicación
/// es la dirección.</summary>
public class ContenedoresAlmacenajeCasilleros
{
    public string COD_CONTENEDOR { get; set; } = string.Empty;
    public string COD_CASILLERO { get; set; } = string.Empty;
    public string? DES_CASILLERO { get; set; }
    /// <summary>Nivel (altura) dentro del contenedor.</summary>
    public int? NUM_FILA { get; set; }
    /// <summary>Posición a lo largo del contenedor. En vista de planta es la única que se ve:
    /// las filas se apilan sobre la misma franja.</summary>
    public int? NUM_COLUMNA { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // Solo lectura (JOINs).
    public string? DES_CONTENDOR { get; set; }   // sic: así se llama la columna en la BD
    public string? DES_ALMACEN { get; set; }
    public string? DES_ABREVIADA { get; set; }
    public string? DES_UBICACION_ALMACEN { get; set; }
    /// <summary>Polígono de la UBICACIÓN a la que apunta el casillero. Es la única de las
    /// tres columnas homónimas que conserva el nombre simple en el SP, porque la tabla base
    /// no tiene DES_POLIGONO propio.</summary>
    public string? DES_POLIGONO { get; set; }

    /// <summary>Polígono del CONTENEDOR: el rectángulo del mueble sobre el plano. Es el que
    /// sirve para derivar la geometría del casillero.</summary>
    public string? DES_POLIGONO_CONTENEDORES_ALMACENAJE { get; set; }

    /// <summary>Contorno del ALMACÉN.</summary>
    public string? DES_POLIGONO_ALMACENES { get; set; }
}

/// <summary>El SP _INSERT hace upsert y genera COD_CASILLERO cuando llega vacío
/// (MAX + FNC_ObtenerSiguienteCorrelativo por contenedor).</summary>
public class ContenedoresAlmacenajeCasillerosInsertDto
{
    [Required] public string COD_CONTENEDOR { get; set; } = string.Empty;
    public string? COD_CASILLERO { get; set; }
    [Required] public string DES_CASILLERO { get; set; } = string.Empty;
    public int? NUM_FILA { get; set; }
    public int? NUM_COLUMNA { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? COD_UBICACION_ALMACEN { get; set; }
}

/// <summary>Parámetros del generador (_CREATETALL): borra los casilleros del contenedor y
/// los vuelve a crear como una matriz de NUM_FILAS × NUM_COLUMNAS, creando de paso una
/// ubicación por casillero.</summary>
public class ContenedoresAlmacenajeCasillerosGenerarDto
{
    [Required] public string COD_CONTENEDOR { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public int NUM_FILAS { get; set; }
    public int NUM_COLUMNAS { get; set; }
}

public class ContenedoresAlmacenajeCasillerosSearchDto
{
    public string? COD_CONTENEDOR { get; set; }
    public string? Search { get; set; }
}
