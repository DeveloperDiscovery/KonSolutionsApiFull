using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad TURNOS_TRABAJO - tipos equivalentes a la BD.</summary>
public class TurnosTrabajo
{
    public string COD_TURNO { get; set; } = string.Empty;
    public string DES_TURNO { get; set; } = string.Empty;
    public DateTime? HOR_INICIO { get; set; }
    public DateTime? HOR_FINAL { get; set; }
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

public class TurnosTrabajoInsertDto
{
    [Required] public string COD_TURNO { get; set; } = string.Empty;
    [Required] public string DES_TURNO { get; set; } = string.Empty;
    public DateTime? HOR_INICIO { get; set; }
    public DateTime? HOR_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TurnosTrabajoUpdateDto
{
    [Required] public string COD_TURNO { get; set; } = string.Empty;
    [Required] public string DES_TURNO { get; set; } = string.Empty;
    public DateTime? HOR_INICIO { get; set; }
    public DateTime? HOR_FINAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class TurnosTrabajoSearchDto
{
    public string? Search { get; set; }
}