using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>LOGISTICA.MOVIMIENTOS_ALMACEN_TRANSPORTE
///
/// Transportes asignados a un movimiento. Es la unica de las cinco tablas hijas que tiene datos propios —acarreo y observacion—, por eso lleva UPDATE.</summary>
public class MovimientosAlmacenTransporte
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    public string COD_TRANSPORTE { get; set; } = string.Empty;
    public bool? FLG_ACARREO { get; set; }
    /// <summary>ANULABLE. El proyecto tiene activado el modelo de nulabilidad de C#, y ASP.NET
    /// Core trata toda propiedad de referencia NO anulable como obligatoria aunque no lleve
    /// [Required]. Como string, agregar un transporte sin observación —que es lo normal—
    /// moría en validación con "The DES_OBSERVACION field is required", sin llegar al SP.
    ///
    /// La columna admite NULL en la tabla: quien no tiene nada que observar, no observa.</summary>
    public string? DES_OBSERVACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_TRANSPORTE { get; set; } = string.Empty;
    public string DES_PLACA { get; set; } = string.Empty;
    public string DES_ENTIDAD { get; set; } = string.Empty;
}

public class MovimientosAlmacenTransporteInsertDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    [Required] public string COD_TRANSPORTE { get; set; } = string.Empty;
    public bool? FLG_ACARREO { get; set; }
    /// <summary>ANULABLE. El proyecto tiene activado el modelo de nulabilidad de C#, y ASP.NET
    /// Core trata toda propiedad de referencia NO anulable como obligatoria aunque no lleve
    /// [Required]. Como string, agregar un transporte sin observación —que es lo normal—
    /// moría en validación con "The DES_OBSERVACION field is required", sin llegar al SP.
    ///
    /// La columna admite NULL en la tabla: quien no tiene nada que observar, no observa.</summary>
    public string? DES_OBSERVACION { get; set; }
}

public class MovimientosAlmacenTransporteUpdateDto
{
    public decimal? NUM_MOVIMIENTO_ALMACEN { get; set; }
    [Required] public string COD_TRANSPORTE { get; set; } = string.Empty;
    public bool? FLG_ACARREO { get; set; }
    /// <summary>ANULABLE. El proyecto tiene activado el modelo de nulabilidad de C#, y ASP.NET
    /// Core trata toda propiedad de referencia NO anulable como obligatoria aunque no lleve
    /// [Required]. Como string, agregar un transporte sin observación —que es lo normal—
    /// moría en validación con "The DES_OBSERVACION field is required", sin llegar al SP.
    ///
    /// La columna admite NULL en la tabla: quien no tiene nada que observar, no observa.</summary>
    public string? DES_OBSERVACION { get; set; }
}

public class MovimientosAlmacenTransporteSearchDto
{
    public string? Search { get; set; }
}
