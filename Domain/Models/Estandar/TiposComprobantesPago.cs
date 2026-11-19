using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TIPOS_COMPROBANTES_PAGO - tipos equivalentes a la BD.</summary>
public class TiposComprobantesPago
{
    public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    public string COD_PREFIJO { get; set; } = string.Empty;

    /// <summary>El prefijo que va en el NÚMERO IMPRESO del comprobante, el que exige SUNAT
    /// ("T" en T001-0000453). COD_PREFIJO es el interno del sistema, que puede no coincidir.
    /// Para armar el número manda éste; si está vacío se cae al interno.</summary>
    public string? COD_PREFIJO_SUNAT { get; set; }
    public int? COD_COMPROBANTE_PAGO_SUNAT { get; set; }
    public bool? FLG_EMISION { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_PREFIJO_COMPROBANTE_ORIGEN { get; set; }
    public bool? FLG_DOCUMENTO_APLICABLE { get; set; }
    public bool? FLG_CONTROL_INTERNO { get; set; }

    /// <summary>Longitudes con que se arma el número del comprobante.
    ///
    /// VAL_LONGITUD_SERIE cuenta SÓLO LOS DÍGITOS de la serie, sin el prefijo: con prefijo "T"
    /// y longitud 3, la serie completa es "T001" y mide cuatro. VAL_LONGITUD_NUMERO es la
    /// cantidad de dígitos del correlativo.
    ///
    /// En nulo no se normaliza nada y el campo se comporta como antes.</summary>
    public int? VAL_LONGITUD_SERIE { get; set; }
    public int? VAL_LONGITUD_NUMERO { get; set; }
}

public class TiposComprobantesPagoInsertDto
{
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string COD_PREFIJO { get; set; } = string.Empty;

    /// <summary>El prefijo que va en el NÚMERO IMPRESO del comprobante, el que exige SUNAT
    /// ("T" en T001-0000453). COD_PREFIJO es el interno del sistema, que puede no coincidir.
    /// Para armar el número manda éste; si está vacío se cae al interno.</summary>
    public string? COD_PREFIJO_SUNAT { get; set; }
    public int? COD_COMPROBANTE_PAGO_SUNAT { get; set; }
    public bool? FLG_EMISION { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_PREFIJO_COMPROBANTE_ORIGEN { get; set; }
    public bool? FLG_DOCUMENTO_APLICABLE { get; set; }
    public bool? FLG_CONTROL_INTERNO { get; set; }

    /// <summary>Longitudes con que se arma el número del comprobante.
    ///
    /// VAL_LONGITUD_SERIE cuenta SÓLO LOS DÍGITOS de la serie, sin el prefijo: con prefijo "T"
    /// y longitud 3, la serie completa es "T001" y mide cuatro. VAL_LONGITUD_NUMERO es la
    /// cantidad de dígitos del correlativo.
    ///
    /// En nulo no se normaliza nada y el campo se comporta como antes.</summary>
    public int? VAL_LONGITUD_SERIE { get; set; }
    public int? VAL_LONGITUD_NUMERO { get; set; }
}

public class TiposComprobantesPagoUpdateDto
{
    [Required] public string COD_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_COMPROBANTE_PAGO { get; set; } = string.Empty;
    [Required] public string COD_PREFIJO { get; set; } = string.Empty;

    /// <summary>El prefijo que va en el NÚMERO IMPRESO del comprobante, el que exige SUNAT
    /// ("T" en T001-0000453). COD_PREFIJO es el interno del sistema, que puede no coincidir.
    /// Para armar el número manda éste; si está vacío se cae al interno.</summary>
    public string? COD_PREFIJO_SUNAT { get; set; }
    public int? COD_COMPROBANTE_PAGO_SUNAT { get; set; }
    public bool? FLG_EMISION { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public bool? FLG_ANTICIPO { get; set; }
    public bool? FLG_PREFIJO_COMPROBANTE_ORIGEN { get; set; }
    public bool? FLG_DOCUMENTO_APLICABLE { get; set; }
    public bool? FLG_CONTROL_INTERNO { get; set; }

    /// <summary>Longitudes con que se arma el número del comprobante.
    ///
    /// VAL_LONGITUD_SERIE cuenta SÓLO LOS DÍGITOS de la serie, sin el prefijo: con prefijo "T"
    /// y longitud 3, la serie completa es "T001" y mide cuatro. VAL_LONGITUD_NUMERO es la
    /// cantidad de dígitos del correlativo.
    ///
    /// En nulo no se normaliza nada y el campo se comporta como antes.</summary>
    public int? VAL_LONGITUD_SERIE { get; set; }
    public int? VAL_LONGITUD_NUMERO { get; set; }
}

public class TiposComprobantesPagoSearchDto
{
    public string? Search { get; set; }
}