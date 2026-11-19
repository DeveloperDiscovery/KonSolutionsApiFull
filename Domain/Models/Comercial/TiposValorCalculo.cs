using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad COMERCIAL.TIPOS_VALOR_CALCULO. Solo COD_TIPO_VALOR_CALCULO es NOT
/// NULL (PK); el resto admite null en la tabla, por lo que no son obligatorios en el DTO.</summary>
public class TiposValorCalculo
{
    public string  COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_TIPO_IMPUESTO      { get; set; }
    public bool?   FLG_POSITIVO           { get; set; }
    public bool?   FLG_IMPUESTO           { get; set; }
    public bool?   FLG_CALCULA            { get; set; }
    public bool?   FLG_REGIMEN_TRIBUTARIO { get; set; }
    /// <summary>El concepto suma los anticipos asociados a la factura (ej. ANTICIPOS).</summary>
    public bool?   FLG_ANTICIPO           { get; set; }
    public string? DES_ETIQUETA_XML       { get; set; }
    public string? DES_TIPO_IMPUESTO      { get; set; }
}

public class TiposValorCalculoInsertDto
{
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_TIPO_IMPUESTO      { get; set; }
    public bool?   FLG_POSITIVO           { get; set; }
    public bool?   FLG_IMPUESTO           { get; set; }
    public bool?   FLG_CALCULA            { get; set; }
    public bool?   FLG_REGIMEN_TRIBUTARIO { get; set; }
    /// <summary>El concepto suma los anticipos asociados a la factura (ej. ANTICIPOS).</summary>
    public bool?   FLG_ANTICIPO           { get; set; }
    public string? DES_ETIQUETA_XML       { get; set; }
}

public class TiposValorCalculoUpdateDto
{
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_TIPO_IMPUESTO      { get; set; }
    public bool?   FLG_POSITIVO           { get; set; }
    public bool?   FLG_IMPUESTO           { get; set; }
    public bool?   FLG_CALCULA            { get; set; }
    public bool?   FLG_REGIMEN_TRIBUTARIO { get; set; }
    /// <summary>El concepto suma los anticipos asociados a la factura (ej. ANTICIPOS).</summary>
    public bool?   FLG_ANTICIPO           { get; set; }
    public string? DES_ETIQUETA_XML       { get; set; }
}

public class TiposValorCalculoSearchDto
{
    public string? Search { get; set; }
}