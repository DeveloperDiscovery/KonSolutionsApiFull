using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Safety;

/// <summary>Entidad ACCESOS_REGISTRO - tipos equivalentes a la BD.</summary>
public class AccesosRegistro
{
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_ZONIFICACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public DateTime? FEC_ACCESO { get; set; }
    public string DES_INCIDENCIA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_ZONIFICACION { get; set; } = string.Empty;
}

public class AccesosRegistroInsertDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ZONIFICACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public DateTime? FEC_ACCESO { get; set; }
    [Required] public string DES_INCIDENCIA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AccesosRegistroUpdateDto
{
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_ZONIFICACION { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
    public DateTime? FEC_ACCESO { get; set; }
    [Required] public string DES_INCIDENCIA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AccesosRegistroSearchDto
{
    public string? Search { get; set; }
}