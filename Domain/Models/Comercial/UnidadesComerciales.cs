using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad UNIDADES_COMERCIALES - tipos equivalentes a la BD.</summary>
public class UnidadesComerciales
{
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public string DES_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public string COD_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_ZONIFICACION { get; set; } = string.Empty;
    public int? NUM_POSICION_SUPERIOR { get; set; }
    public int? NUM_POSICION_IZQUIERDA { get; set; }
    public int? NUM_LONGITUD_LARGO { get; set; }
    public int? NUM_LONGITUD_ANCHO { get; set; }
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
    public string DES_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    public string DES_ZONIFICACION { get; set; } = string.Empty;
}

public class UnidadesComercialesInsertDto
{
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    [Required] public string DES_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ZONIFICACION { get; set; } = string.Empty;
    public int? NUM_POSICION_SUPERIOR { get; set; }
    public int? NUM_POSICION_IZQUIERDA { get; set; }
    public int? NUM_LONGITUD_LARGO { get; set; }
    public int? NUM_LONGITUD_ANCHO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UnidadesComercialesUpdateDto
{
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    [Required] public string DES_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_UNIDAD_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ZONIFICACION { get; set; } = string.Empty;
    public int? NUM_POSICION_SUPERIOR { get; set; }
    public int? NUM_POSICION_IZQUIERDA { get; set; }
    public int? NUM_LONGITUD_LARGO { get; set; }
    public int? NUM_LONGITUD_ANCHO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UnidadesComercialesSearchDto
{
    public string? Search { get; set; }
}