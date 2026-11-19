using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PERSONAL_CONTACTOS - tipos equivalentes a la BD.</summary>
public class PersonalContactos
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_CONTACTO { get; set; }
    public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public bool? FLG_EMERGENCIA { get; set; }
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
    public string DES_TIPO_PARENTESCO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class PersonalContactosInsertDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_CONTACTO { get; set; }
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public bool? FLG_EMERGENCIA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalContactosUpdateDto
{
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public decimal? COD_ENTIDAD_CONTACTO { get; set; }
    [Required] public string COD_TIPO_PARENTESCO { get; set; } = string.Empty;
    public bool? FLG_EMERGENCIA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class PersonalContactosSearchDto
{
    public string? Search { get; set; }
}