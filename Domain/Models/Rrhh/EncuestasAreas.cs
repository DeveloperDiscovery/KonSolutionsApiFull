using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad ENCUESTAS_AREAS - tipos equivalentes a la BD.</summary>
public class EncuestasAreas
{
    public int? NUM_ENCUESTA { get; set; }
    public int? COD_AREA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public string DES_ECUENSTA { get; set; } = string.Empty;
}

public class EncuestasAreasInsertDto
{
    public int? NUM_ENCUESTA { get; set; }
    public int? COD_AREA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasAreasUpdateDto
{
    public int? NUM_ENCUESTA { get; set; }
    public int? COD_AREA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EncuestasAreasSearchDto
{
    public string? Search { get; set; }
}