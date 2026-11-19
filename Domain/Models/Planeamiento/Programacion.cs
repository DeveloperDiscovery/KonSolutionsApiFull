using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad PROGRAMACION - tipos equivalentes a la BD.</summary>
public class Programacion
{
    public int? NUM_PROGRAMA { get; set; }
    public string DES_PROGRAMA { get; set; } = string.Empty;
    public string DES_OBSERVACION { get; set; } = string.Empty;
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
}

public class ProgramacionInsertDto
{
    public int? NUM_PROGRAMA { get; set; }
    [Required] public string DES_PROGRAMA { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionUpdateDto
{
    public int? NUM_PROGRAMA { get; set; }
    [Required] public string DES_PROGRAMA { get; set; } = string.Empty;
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionSearchDto
{
    public string? Search { get; set; }
}