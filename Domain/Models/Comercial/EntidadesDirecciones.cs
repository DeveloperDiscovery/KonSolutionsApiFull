using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_DIRECCIONES - tipos equivalentes a la BD.</summary>
public class EntidadesDirecciones
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_TIPO_DIRECCION { get; set; }
    public string? COD_UBIGEO { get; set; }
    public string? COD_TIPO_ZONA { get; set; }
    public string? DES_NOMBRE_ZONA { get; set; }
    public string? COD_TIPO_VIA { get; set; }
    public string? DES_NOMBRE_VIA { get; set; }
    public string? DES_NUMERACION { get; set; }
    public string? DES_KILOMETRO { get; set; }
    public string? DES_MANZANA { get; set; }
    public string? DES_LOTE { get; set; }
    public string? DES_INTERIOR { get; set; }
    public string? DES_DIRECCION { get; set; }
    public string? DES_REFERENCIA { get; set; }
    public decimal? VAL_LATITUD { get; set; }
    public decimal? VAL_LONGITUD { get; set; }
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
    public string DES_TIPO_VIA { get; set; } = string.Empty;
    public string DES_TIPO_ZONA { get; set; } = string.Empty;
    public string DES_UBIGEO { get; set; } = string.Empty;
}

public class EntidadesDireccionesInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_TIPO_DIRECCION { get; set; }
    [Required] public string COD_UBIGEO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ZONA { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE_ZONA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_VIA { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE_VIA { get; set; } = string.Empty;
    [Required] public string DES_NUMERACION { get; set; } = string.Empty;
    // NULL en BD — direcciones sin kilómetro/manzana/lote/interior son válidas
    // (no toda dirección es una urbanización con manzana/lote, ni toda vía es una carretera
    // con kilómetro), así que no pueden ser [Required] como los demás.
    public string? DES_KILOMETRO { get; set; }
    public string? DES_MANZANA { get; set; }
    public string? DES_LOTE { get; set; }
    public string? DES_INTERIOR { get; set; }
    [Required] public string DES_DIRECCION { get; set; } = string.Empty;
    public string? DES_REFERENCIA { get; set; }
    public decimal? VAL_LATITUD { get; set; }
    public decimal? VAL_LONGITUD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesDireccionesUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_TIPO_DIRECCION { get; set; }
    [Required] public string COD_UBIGEO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ZONA { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE_ZONA { get; set; } = string.Empty;
    [Required] public string COD_TIPO_VIA { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE_VIA { get; set; } = string.Empty;
    [Required] public string DES_NUMERACION { get; set; } = string.Empty;
    public string? DES_KILOMETRO { get; set; }
    public string? DES_MANZANA { get; set; }
    public string? DES_LOTE { get; set; }
    public string? DES_INTERIOR { get; set; }
    [Required] public string DES_DIRECCION { get; set; } = string.Empty;
    public string? DES_REFERENCIA { get; set; }
    public decimal? VAL_LATITUD { get; set; }
    public decimal? VAL_LONGITUD { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesDireccionesSearchDto
{
    public decimal COD_ENTIDAD { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_COMERCIAL_ENTIDADES_DIRECCIONES_LOADCOMBOS: TiposDireccion,
/// TiposZona (Rrhh), TiposVia y Estado (filtrado por @COD_TIPO_ESTADO) en un solo viaje
/// para EntidadesDireccionesDialog.razor.</summary>
public class EntidadesDireccionesLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> TiposDireccion { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposZona      { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposVia       { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados        { get; set; } = new();
}
