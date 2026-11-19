using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_OPERACION_CALCULO - tipos equivalentes a la BD.</summary>
public class TiposOperacionCalculo
{
    public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_CALCULO_INVERSO { get; set; }
    public bool? FLG_MOSTRAR_COMPROBANTE { get; set; }
    public bool? FLG_VALOR_MOSTRAR { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public string? COD_CUENTA_CONTABLE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public string DES_CUENTA_NATURALEZA_ALTERNATIVA { get; set; } = string.Empty;
    public string DES_TIPO_OPERACION { get; set; } = string.Empty;
    public string DES_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    // Provienen del JOIN con COMERCIAL.TIPOS_VALOR_CALCULO — solo lectura.
    public bool? FLG_CALCULA { get; set; }
    public bool? FLG_IMPUESTO { get; set; }
    public bool? FLG_REGIMEN_TRIBUTARIO { get; set; }
}

public class TiposOperacionCalculoInsertDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_CALCULO_INVERSO { get; set; }
    public bool? FLG_MOSTRAR_COMPROBANTE { get; set; }
    public bool? FLG_VALOR_MOSTRAR { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public string? COD_CUENTA_CONTABLE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposOperacionCalculoUpdateDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_VALOR_CALCULO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public bool? FLG_CALCULO_INVERSO { get; set; }
    public bool? FLG_MOSTRAR_COMPROBANTE { get; set; }
    public bool? FLG_VALOR_MOSTRAR { get; set; }
    public bool? FLG_POSITIVO { get; set; }
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public string? COD_CUENTA_CONTABLE { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TiposOperacionCalculoCopyDto
{
    [Required] public string COD_CLASE_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION { get; set; } = string.Empty;
    [Required] public string COD_CLASE_OPERACION_DESTINO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_OPERACION_DESTINO { get; set; } = string.Empty;
}

public class TiposOperacionCalculoSearchDto
{
    public string? Search { get; set; }
}
