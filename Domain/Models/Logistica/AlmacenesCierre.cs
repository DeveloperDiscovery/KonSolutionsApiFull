using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOGISTICA.ALMACENES_CIERRE — un cierre de UN almacén para un periodo (FEC_DESDE a FEC_HASTA, ambos
/// inclusive). Mientras FLG_CERRADO sea verdadero, el almacén no admite movimientos, ajustes ni transferencias con fecha
/// dentro del periodo (ni operaciones hechas HOY si hoy cae dentro). Reabrir no borra: deja FLG_CERRADO en falso con
/// quién, cuándo y por qué. PK: NUM_CIERRE_ALMACEN (IDENTITY).</summary>
public class AlmacenesCierre
{
    public int NUM_CIERRE_ALMACEN { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string? DES_ALMACEN { get; set; }
    public DateTime FEC_DESDE { get; set; }
    public DateTime FEC_HASTA { get; set; }
    /// <summary>Días que abarca el cierre (calculado en el SP).</summary>
    public int NUM_DIAS { get; set; }
    public bool FLG_CERRADO { get; set; }
    /// <summary>CERRADO o REABIERTO (calculado en el SP a partir de FLG_CERRADO).</summary>
    public string? DES_ESTADO { get; set; }
    /// <summary>Cierre vigente cuyo periodo incluye el día de hoy.</summary>
    public bool FLG_EN_CURSO { get; set; }
    public string? DES_OBSERVACION { get; set; }
    public string? COD_USUARIO_REAPERTURA { get; set; }
    public string? COD_ESTACION_REAPERTURA { get; set; }
    public DateTime? FEC_REAPERTURA { get; set; }
    public string? DES_MOTIVO_REAPERTURA { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>Filtros del SP _SEARCH. Todos opcionales: el texto busca en almacén, observación, motivo, usuarios, estado y
/// fechas; FLG_CERRADO = true solo vigentes, false solo reabiertos; FEC_REFERENCIA = los cierres que alcanzan ese día.</summary>
public class AlmacenesCierreSearchDto
{
    public string? SEARCH { get; set; }
    public string? COD_ALMACEN { get; set; }
    public bool? FLG_CERRADO { get; set; }
    public DateTime? FEC_REFERENCIA { get; set; }
}

/// <summary>Cierra UNO O VARIOS almacenes con el mismo periodo, todo o nada (un cierre por almacén).</summary>
public class AlmacenesCierreInsertDto
{
    [Required, MinLength(1)] public List<string> COD_ALMACENES { get; set; } = new();
    [Required] public DateTime FEC_DESDE { get; set; }
    [Required] public DateTime FEC_HASTA { get; set; }
    [StringLength(1000)] public string? DES_OBSERVACION { get; set; }
}

/// <summary>Cambia el periodo o la observación de un cierre VIGENTE. El almacén no se cambia: se reabre y se cierra otro.</summary>
public class AlmacenesCierreUpdateDto
{
    [Required] public int NUM_CIERRE_ALMACEN { get; set; }
    [Required] public DateTime FEC_DESDE { get; set; }
    [Required] public DateTime FEC_HASTA { get; set; }
    [StringLength(1000)] public string? DES_OBSERVACION { get; set; }
}

/// <summary>El motivo es obligatorio: queda registrado junto con quién y cuándo reabrió.</summary>
public class AlmacenesCierreReabrirDto
{
    [Required, StringLength(1000, MinimumLength = 1)] public string DES_MOTIVO_REAPERTURA { get; set; } = string.Empty;
}

/// <summary>¿Este almacén admite operar en esta fecha (y hoy)? Si no, DES_MENSAJE dice por qué.</summary>
public class AlmacenesCierreConsulta
{
    public string COD_ALMACEN { get; set; } = string.Empty;
    public DateTime? FEC_MOVIMENTO { get; set; }
    public bool FLG_CERRADO { get; set; }
    public string? DES_MENSAJE { get; set; }
}

/// <summary>Una fila que devuelve el SP _INSERT por cada cierre creado.</summary>
public class AlmacenesCierreCreado
{
    public int NUM_CIERRE_ALMACEN { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
}
