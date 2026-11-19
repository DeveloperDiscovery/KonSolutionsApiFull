using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Administracion;

/// <summary>Entidad CARGOS - tipos equivalentes a la BD.</summary>
public class Cargos
{
    public int? COD_CARGO { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
    public int? COD_TIPO_CARGO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
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
    public string DES_TIPO_CARGO { get; set; } = string.Empty;
}

public class CargosInsertDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string DES_CARGO { get; set; } = string.Empty;
    public int? COD_TIPO_CARGO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosUpdateDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string DES_CARGO { get; set; } = string.Empty;
    public int? COD_TIPO_CARGO { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosSearchDto
{
    public string? Search { get; set; }
}