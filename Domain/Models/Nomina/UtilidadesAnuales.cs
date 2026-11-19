using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Nomina;

/// <summary>Entidad UTILIDADES_ANUALES - tipos equivalentes a la BD.</summary>
public class UtilidadesAnuales
{
    public int? NUM_ANO { get; set; }
    public decimal? VAL_UTILIDAD { get; set; }
    public decimal? TAS_CALCULO_ASISTENCIA { get; set; }
    public decimal? TAS_CALCULO_REMUNERACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class UtilidadesAnualesInsertDto
{
    public int? NUM_ANO { get; set; }
    public decimal? VAL_UTILIDAD { get; set; }
    public decimal? TAS_CALCULO_ASISTENCIA { get; set; }
    public decimal? TAS_CALCULO_REMUNERACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UtilidadesAnualesUpdateDto
{
    public int? NUM_ANO { get; set; }
    public decimal? VAL_UTILIDAD { get; set; }
    public decimal? TAS_CALCULO_ASISTENCIA { get; set; }
    public decimal? TAS_CALCULO_REMUNERACION { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UtilidadesAnualesSearchDto
{
    public string? Search { get; set; }
}