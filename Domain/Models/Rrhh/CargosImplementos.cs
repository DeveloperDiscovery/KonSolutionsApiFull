using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad CARGOS_IMPLEMENTOS - tipos equivalentes a la BD.</summary>
public class CargosImplementos
{
    public int? COD_CARGO { get; set; }
    public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public int? NUM_MESES_REPOSICION { get; set; }
    public bool? FLG_CONSIDERAR_FECHA_ENTREGA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class CargosImplementosInsertDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public int? NUM_MESES_REPOSICION { get; set; }
    public bool? FLG_CONSIDERAR_FECHA_ENTREGA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosImplementosUpdateDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_IMPLEMENTO_LABORAL { get; set; } = string.Empty;
    public int? NUM_MESES_REPOSICION { get; set; }
    public bool? FLG_CONSIDERAR_FECHA_ENTREGA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosImplementosSearchDto
{
    public string? Search { get; set; }
}