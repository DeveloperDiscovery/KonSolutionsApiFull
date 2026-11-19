using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Finanzas;

/// <summary>Entidad CAJAS - tipos equivalentes a la BD.</summary>
public class Cajas
{
    public string COD_CAJA { get; set; } = string.Empty;
    public string DES_CAJA { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public int? NUM_SERIE { get; set; }
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
}

public class CajasInsertDto
{
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    [Required] public string DES_CAJA { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public int? NUM_SERIE { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CajasUpdateDto
{
    [Required] public string COD_CAJA { get; set; } = string.Empty;
    [Required] public string DES_CAJA { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public int? NUM_SERIE { get; set; }
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CajasSearchDto
{
    public string? Search { get; set; }
}