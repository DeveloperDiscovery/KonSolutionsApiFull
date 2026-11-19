using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad COMERCIAL.ENTIDADES_INFORMACION_COMERCIAL — las condiciones comerciales
/// pactadas con una entidad para un tipo de operación puntual: moneda, condición de pago y
/// forma de pago. PK compuesta (COD_ENTIDAD, COD_CLASE_OPERACION, COD_TIPO_OPERACION), o sea
/// que una misma entidad puede tener condiciones distintas por tipo de operación.</summary>
public class EntidadesInformacionComercial
{
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string? COD_MONEDA { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }

    // Descripciones que resuelven los LEFT JOIN de los SP: la grilla muestra texto, no códigos.
    public string? DES_MONEDA { get; set; }
    public string? DES_CONDICION_PAGO { get; set; }
    public string? DES_FORMA_PAGO { get; set; }
    public string? DES_CLASE_OPERACION { get; set; }
    public string? DES_TIPO_OPERACION { get; set; }
    public string? DES_COMERCIAL { get; set; }
    public string? DES_MATERNO { get; set; }
    public string? DES_NOMBRE { get; set; }
    public string? DES_NOMBRE_COMPLETO { get; set; }
    public string? DES_NOMBRE2 { get; set; }
    public string? DES_PATERNO { get; set; }
    public string? DES_PREFIJO_USUARIO_COMERCIAL { get; set; }
}

public class EntidadesInformacionComercialInsertDto
{
    [Required] public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string? COD_MONEDA { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
}

public class EntidadesInformacionComercialUpdateDto
{
    [Required] public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string? COD_MONEDA { get; set; }
    public int? COD_CONDICION_PAGO { get; set; }
    public int? COD_FORMA_PAGO { get; set; }
}

public class EntidadesInformacionComercialSearchDto
{
    [Required] public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string? Search { get; set; }
}
