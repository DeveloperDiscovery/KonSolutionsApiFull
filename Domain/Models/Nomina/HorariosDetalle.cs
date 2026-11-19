using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad HORARIOS_DETALLE - tipos equivalentes a la BD.</summary>
public class HorariosDetalle
{
    public string COD_HORARIO { get; set; } = string.Empty;
    public int? NUM_DIA_SEMANA { get; set; }
    public DateTime? HOR_INGRESO { get; set; }
    public DateTime? HOR_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_HORARIO { get; set; } = string.Empty;
}

public class HorariosDetalleInsertDto
{
    [Required] public string COD_HORARIO { get; set; } = string.Empty;
    public int? NUM_DIA_SEMANA { get; set; }
    public DateTime? HOR_INGRESO { get; set; }
    public DateTime? HOR_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class HorariosDetalleUpdateDto
{
    [Required] public string COD_HORARIO { get; set; } = string.Empty;
    public int? NUM_DIA_SEMANA { get; set; }
    public DateTime? HOR_INGRESO { get; set; }
    public DateTime? HOR_SALIDA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class HorariosDetalleSearchDto
{
    public string? Search { get; set; }
}