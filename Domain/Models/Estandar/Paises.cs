using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

public class Paises
{
    public string  COD_PAIS          { get; set; } = string.Empty;
    public string  DES_PAIS          { get; set; } = string.Empty;
    public string? COD_PAIS_3        { get; set; }
    public string? DES_PAIS_INGLES   { get; set; }
    public string? DES_PAIS_NATIVO   { get; set; }
    public string? DES_REGION        { get; set; }
    public string? DES_SUBREGION     { get; set; }
    public string? DES_CAPITAL       { get; set; }
    public string? DES_BANDERA_EMOJI { get; set; }
    public string? URL_BANDERA_PNG   { get; set; }
    public string? URL_BANDERA_SVG   { get; set; }
    public byte[]? IMG_ICONO         { get; set; }
    public byte[]? IMG_ICONO_SVG     { get; set; }
    public long?   NUM_POBLACION     { get; set; }
}

public class PaisesInsertDto
{
    [Required] public string  COD_PAIS          { get; set; } = string.Empty;
    [Required] public string  DES_PAIS          { get; set; } = string.Empty;
    public string? COD_PAIS_3        { get; set; }
    public string? DES_PAIS_INGLES   { get; set; }
    public string? DES_PAIS_NATIVO   { get; set; }
    public string? DES_REGION        { get; set; }
    public string? DES_SUBREGION     { get; set; }
    public string? DES_CAPITAL       { get; set; }
    public string? DES_BANDERA_EMOJI { get; set; }
    public string? URL_BANDERA_PNG   { get; set; }
    public string? URL_BANDERA_SVG   { get; set; }
    public byte[]? IMG_ICONO         { get; set; }
    public byte[]? IMG_ICONO_SVG     { get; set; }
    public long?   NUM_POBLACION     { get; set; }
}

public class PaisesUpdateDto
{
    [Required] public string  COD_PAIS          { get; set; } = string.Empty;
    [Required] public string  DES_PAIS          { get; set; } = string.Empty;
    public string? COD_PAIS_3        { get; set; }
    public string? DES_PAIS_INGLES   { get; set; }
    public string? DES_PAIS_NATIVO   { get; set; }
    public string? DES_REGION        { get; set; }
    public string? DES_SUBREGION     { get; set; }
    public string? DES_CAPITAL       { get; set; }
    public string? DES_BANDERA_EMOJI { get; set; }
    public string? URL_BANDERA_PNG   { get; set; }
    public string? URL_BANDERA_SVG   { get; set; }
    public byte[]? IMG_ICONO         { get; set; }
    public byte[]? IMG_ICONO_SVG     { get; set; }
    public long?   NUM_POBLACION     { get; set; }
}

public class PaisesSearchDto
{
    public string? Search { get; set; }
}