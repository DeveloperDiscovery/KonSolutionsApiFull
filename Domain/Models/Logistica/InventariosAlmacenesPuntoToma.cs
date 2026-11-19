using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_PUNTO_TOMA - tipos equivalentes a la BD.
/// Clave compuesta NUM_INVENTARIO + COD_ALMACEN + NUM_PUNTO_TOMA_INVENTARIO (un "punto de
/// conteo" dentro del almacén de un inventario puntual).
///
/// COLISIÓN DE NOMBRES: SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH devuelven DOS columnas llamadas
/// "DES_OBSERVACION" en el mismo resultset — la propia del punto de toma (tabla a) y la del
/// inventario/almacén padre (join d, LOGISTICA.INVENTARIOS_ALMACENES). Dapper no distingue
/// columnas repetidas por nombre al mapear a un POCO: ambas terminan escribiendo la misma
/// propiedad y la última leída (la del join) gana en silencio, perdiendo la propia.
/// Se resuelve igual que <see cref="KonSolutionsApi.Infrastructure.Data.ComboboxReader"/> ya
/// resuelve la ambigüedad de nombres de columna en este proyecto: leyendo por ORDINAL en vez
/// de por nombre. El repositorio mapea manualmente el IDataReader (ver
/// InventariosAlmacenesPuntoTomaRepository.MapRow) tomando la PRIMERA columna DES_OBSERVACION
/// (columna propia de la tabla base, listada primero por el generador) para DES_OBSERVACION, y
/// la ÚLTIMA (la del JOIN al inventario padre) para DES_OBSERVACION_INVENTARIO.</summary>
public class InventariosAlmacenesPuntoToma
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public bool? FLG_READER_RFID { get; set; }
    public bool? FLG_READER_CODEBAR { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_FORECOLOR { get; set; }
    public string? DES_COMERCIAL { get; set; }
    public string? DES_MATERNO { get; set; }
    public string? DES_NOMBRE { get; set; }
    public string? DES_NOMBRE_COMPLETO { get; set; }
    public string? DES_NOMBRE2 { get; set; }
    public string? DES_PATERNO { get; set; }
    public string? DES_PREFIJO_USUARIO_COMERCIAL { get; set; }
    /// <summary>DES_OBSERVACION del INVENTARIOS_ALMACENES padre (join d) — ver comentario de
    /// colisión de nombres arriba.</summary>
    public string? DES_OBSERVACION_INVENTARIO { get; set; }
}

public class InventariosAlmacenesPuntoTomaInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public bool? FLG_READER_RFID { get; set; }
    public bool? FLG_READER_CODEBAR { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaUpdateDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_RESPONSABLE { get; set; }
    public DateTime? FEC_INICIO_PROGRAMADA { get; set; }
    public DateTime? FEC_FINAL_PROGRAMADA { get; set; }
    public DateTime? FEC_INICIO_EFECTUADA { get; set; }
    public DateTime? FEC_FINAL_EFECTUADA { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public bool? FLG_READER_RFID { get; set; }
    public bool? FLG_READER_CODEBAR { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public string? Search { get; set; }
}
