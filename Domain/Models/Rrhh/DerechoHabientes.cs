using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad DERECHO_HABIENTES - tipos equivalentes a la BD.</summary>
public class DerechoHabientes
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_DERECHO_HABIENTE { get; set; }
    public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public DateTime? FEC_NACIMIENTO { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public string COD_SEXO { get; set; } = string.Empty;
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
    public string DES_SEXO { get; set; } = string.Empty;
    public string DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    public string DES_TIPO_PARENTESCO { get; set; } = string.Empty;
}

public class DerechoHabientesInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_DERECHO_HABIENTE { get; set; }
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    [Required] public string DES_PATERNO { get; set; } = string.Empty;
    [Required] public string DES_MATERNO { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE2 { get; set; } = string.Empty;
    public DateTime? FEC_NACIMIENTO { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_SEXO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DerechoHabientesUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_DERECHO_HABIENTE { get; set; }
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    [Required] public string DES_PATERNO { get; set; } = string.Empty;
    [Required] public string DES_MATERNO { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE { get; set; } = string.Empty;
    [Required] public string DES_NOMBRE2 { get; set; } = string.Empty;
    public DateTime? FEC_NACIMIENTO { get; set; }
    public int? COD_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    [Required] public string NUM_DOCUMENTO_IDENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_SEXO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DerechoHabientesSearchDto
{
    public string? Search { get; set; }
}