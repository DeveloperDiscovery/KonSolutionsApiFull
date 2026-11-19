using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad SUCURSALES - tipos equivalentes a la BD.</summary>
public class Sucursales
{
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string COD_UBIGEO { get; set; } = string.Empty;
    public string COD_TIPO_SUCURSAL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string COD_TIPO_POS { get; set; } = string.Empty;
    public int? COD_TIPO_DIRECCION { get; set; }
    public decimal? VAL_LATITUD { get; set; }
    public decimal? VAL_LONGITUD { get; set; }
    public string COD_SUCURSAL_SUNAT { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_DIRECCION { get; set; } = string.Empty;
    public string DES_TIPO_POS { get; set; } = string.Empty;
    public string DES_UBIGEO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class SucursalesInsertDto
{
    /// <summary>No se digita: PROC_ESTANDAR_SUCURSALES_INSERT asigna el siguiente correlativo y
    /// descarta lo que llegue acá.</summary>
    public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string DES_SUCURSAL { get; set; } = string.Empty;
    [Required] public string DES_DIRECCION { get; set; } = string.Empty;
    [Required] public string COD_UBIGEO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_SUCURSAL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_POS { get; set; } = string.Empty;
    public int? COD_TIPO_DIRECCION { get; set; }
    public decimal? VAL_LATITUD { get; set; }
    public decimal? VAL_LONGITUD { get; set; }
    /// <summary>Código del establecimiento anexo en SUNAT (cuatro dígitos; 0000 es el domicilio fiscal).
    /// Vacío se graba como NULL.</summary>
    [RegularExpression(@"^\d{4}$", ErrorMessage = "El código SUNAT del establecimiento son cuatro dígitos (por ejemplo, 0001).")]
    public string? COD_SUCURSAL_SUNAT { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SucursalesUpdateDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string DES_SUCURSAL { get; set; } = string.Empty;
    [Required] public string DES_DIRECCION { get; set; } = string.Empty;
    [Required] public string COD_UBIGEO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_SUCURSAL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_PROPIETARIA { get; set; }
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_POS { get; set; } = string.Empty;
    public int? COD_TIPO_DIRECCION { get; set; }
    public decimal? VAL_LATITUD { get; set; }
    public decimal? VAL_LONGITUD { get; set; }
    /// <summary>Código del establecimiento anexo en SUNAT (cuatro dígitos; 0000 es el domicilio fiscal).
    /// Vacío se graba como NULL.</summary>
    [RegularExpression(@"^\d{4}$", ErrorMessage = "El código SUNAT del establecimiento son cuatro dígitos (por ejemplo, 0001).")]
    public string? COD_SUCURSAL_SUNAT { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SucursalesSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_ESTANDAR_SUCURSALES_LOADCOMBOS — trae en un solo
/// viaje a la BD los 3 combos que necesita el diálogo (Ubigeo ya filtrado a
/// distritos, Entidades activas, Tipos de POS).</summary>
public class SucursalesLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> Ubigeos        { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Entidades      { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposPos       { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposDireccion { get; set; } = new();
}