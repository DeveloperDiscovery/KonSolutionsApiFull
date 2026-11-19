using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.ALMACENES_TIPOS_MOVIMIENTO — qué tipos de movimiento están
/// habilitados en un almacén. Hija de LOGISTICA.ALMACENES.
/// PK compuesta: COD_ALMACEN + COD_CLASE_MOVIMIENTO + COD_TIPO_MOVIMIENTO.
///
/// Es una tabla de asignación pura: no tiene datos propios más allá de la auditoría, así que
/// la pantalla no es un CRUD sino un asignar/desasignar.</summary>
public class AlmacenesTiposMovimiento
{
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // Solo lectura (JOIN con LOGISTICA.TIPOS_MOVIMIENTO).
    public string? DES_TIPO_MOVIMIENTO { get; set; }
    public string? DES_OBSERVACION { get; set; }
}

/// <summary>El SP _INSERT hace upsert por la PK completa. Usuario y estación salen del token.</summary>
public class AlmacenesTiposMovimientoInsertDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
}

/// <summary>Se conserva del scaffold original: el SP INSERT hace upsert, así que Modificar
/// y Crear son el mismo POST y este DTO no se usa. Queda por compatibilidad.</summary>
public class AlmacenesTiposMovimientoUpdateDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
}

/// <summary>Asignación o baja EN BLOQUE de varios tipos a la vez.
///
/// Va como una sola llamada y no como N: marcar veinte tipos y disparar veinte peticiones
/// deja la pantalla a merced de que alguna falle por el medio, con el almacén a medio asignar
/// y sin forma de saber cuáles entraron.</summary>
public class AlmacenesTiposMovimientoLoteDto
{
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    [Required] public string COD_CLASE_MOVIMIENTO { get; set; } = string.Empty;
    public List<string> Tipos { get; set; } = new();
}

public class AlmacenesTiposMovimientoSearchDto
{
    public string? COD_ALMACEN { get; set; }
    public string? COD_CLASE_MOVIMIENTO { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de una operación en bloque: cuántos se procesaron y cuáles fallaron.
/// Se informa el detalle porque en un lote parcial el usuario necesita saber exactamente qué
/// quedó afuera — y por qué, si el motivo es una referencia que impide la baja.</summary>
public class AlmacenesTiposMovimientoLoteResult
{
    public int Procesados { get; set; }
    public List<string> Fallidos { get; set; } = new();
    public string? DesError { get; set; }
}
