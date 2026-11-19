using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad RESERVAS_STOCK - tipos equivalentes a la BD.</summary>
public class ReservasStock
{
    public int? NUM_RESERVA_STOCK { get; set; }
    public string DES_RESERVA_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_RESERVA_STOCK { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    public string COD_TIPO_RESERVA { get; set; } = string.Empty;
    // Nueva en la tabla (SCRIPdb_v28): cuándo se atendió la reserva. Nulable — nace vacía,
    // la pone el proceso que la atiende, no el alta.
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
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_REQUERIMIENTO_STOCK { get; set; } = string.Empty;
    public string DES_TIPO_RESERVA { get; set; } = string.Empty;
}

public class ReservasStockInsertDto
{
    public int? NUM_RESERVA_STOCK { get; set; }
    [Required] public string DES_RESERVA_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_RESERVA_STOCK { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string COD_TIPO_RESERVA { get; set; } = string.Empty;
    // Nulable y sin [Required]: nace vacía, la pone el proceso que atiende la reserva.
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

public class ReservasStockUpdateDto
{
    public int? NUM_RESERVA_STOCK { get; set; }
    [Required] public string DES_RESERVA_STOCK { get; set; } = string.Empty;
    public DateTime? FEC_RESERVA_STOCK { get; set; }
    public int? NUM_REQUERIMIENTO_STOCK { get; set; }
    [Required] public string COD_TIPO_RESERVA { get; set; } = string.Empty;
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

public class ReservasStockSearchDto
{
    public string? Search { get; set; }

    /// <summary>Filtro por Tipo de Reserva. Viaja también en el listado sin búsqueda: el mismo
    /// DTO sirve para el SHOWALL y para el SEARCH, así los dos caminos filtran con el mismo
    /// criterio en vez de comportarse distinto sin que nadie se dé cuenta.
    /// Vacío o null = sin filtro.</summary>
    public string? COD_TIPO_RESERVA { get; set; }

    /// <summary>Filtro por producto. La reserva no tiene producto —lo tiene su detalle—, así que
    /// el SP resuelve con un EXISTS: "las reservas que tengan al menos una línea de este
    /// producto". Lo usa el clic en la celda Reservada del requerimiento.</summary>
    public string? COD_PRODUCTO { get; set; }
}