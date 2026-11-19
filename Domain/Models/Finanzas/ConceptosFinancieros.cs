using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad CONCEPTOS_FINANCIEROS - tipos equivalentes a la BD.</summary>
public class ConceptosFinancieros
{
    public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string COD_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string DES_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public string COD_PRODUCTO_GASTO { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class ConceptosFinancierosInsertDto
{
    [Required] public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string COD_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string DES_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO_GASTO { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConceptosFinancierosUpdateDto
{
    [Required] public string COD_TIPO_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string COD_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string DES_CONCEPTO_FINANCIERO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO_GASTO { get; set; } = string.Empty;
    public decimal? NUM_REGISTRO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ConceptosFinancierosSearchDto
{
    public string? Search { get; set; }
}