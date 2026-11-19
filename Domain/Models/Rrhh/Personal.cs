using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL - tipos equivalentes a la BD.</summary>
public class Personal
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string COD_PERSONAL { get; set; } = string.Empty;
    public DateTime? FEC_NACIMIENTO { get; set; }
    public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    public string COD_SEXO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public string COD_ESTADO_CIVIL { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_CESE { get; set; }
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public string COD_AFP { get; set; } = string.Empty;
    public int? COD_GRADO_CONFIDENCIABILIDAD { get; set; }
    public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string COD_MONEDA_REMUNERACION { get; set; } = string.Empty;
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
    public string DES_SEXO { get; set; } = string.Empty;
    public string DES_AFP { get; set; } = string.Empty;
    public string DES_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_ESTADO_CIVIL { get; set; } = string.Empty;
    public string DES_GRADO_CONFIDENCIABILIDAD { get; set; } = string.Empty;
    public string DES_AREA { get; set; } = string.Empty;
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_MONEDA { get; set; } = string.Empty;
    public string DES_TIPO_PERSONAL { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_MONEDA { get; set; } = string.Empty;
}

public class PersonalInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_PERSONAL { get; set; } = string.Empty;
    public DateTime? FEC_NACIMIENTO { get; set; }
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string COD_SEXO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    [Required] public string COD_ESTADO_CIVIL { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_CESE { get; set; }
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_AFP { get; set; } = string.Empty;
    public int? COD_GRADO_CONFIDENCIABILIDAD { get; set; }
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_MONEDA_REMUNERACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_PERSONAL { get; set; } = string.Empty;
    public DateTime? FEC_NACIMIENTO { get; set; }
    [Required] public string COD_TIPO_PERSONAL { get; set; } = string.Empty;
    [Required] public string COD_SEXO { get; set; } = string.Empty;
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    [Required] public string COD_ESTADO_CIVIL { get; set; } = string.Empty;
    public DateTime? FEC_INGRESO { get; set; }
    public DateTime? FEC_CESE { get; set; }
    [Required] public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    [Required] public string COD_AFP { get; set; } = string.Empty;
    public int? COD_GRADO_CONFIDENCIABILIDAD { get; set; }
    [Required] public string COD_CATEGORIA_OPERACION_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_MONEDA_REMUNERACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalSearchDto
{
    public string? Search { get; set; }
}