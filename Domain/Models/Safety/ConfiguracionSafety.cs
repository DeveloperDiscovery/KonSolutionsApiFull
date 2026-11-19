using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Safety;

/// <summary>Entidad CONFIGURACION_SAFETY - tipos equivalentes a la BD.</summary>
public class ConfiguracionSafety
{
    public int? NUM_SECUENCIA { get; set; }
    public int? POS_EPC { get; set; }
    public int? POS_ANTENA { get; set; }
    public int? POS_RSSI { get; set; }
    public int? POS_DATE_TIME { get; set; }
    public int? POS_ID_READER { get; set; }
}

public class ConfiguracionSafetyInsertDto
{
    public int? NUM_SECUENCIA { get; set; }
    public int? POS_EPC { get; set; }
    public int? POS_ANTENA { get; set; }
    public int? POS_RSSI { get; set; }
    public int? POS_DATE_TIME { get; set; }
    public int? POS_ID_READER { get; set; }
}

public class ConfiguracionSafetyUpdateDto
{
    public int? NUM_SECUENCIA { get; set; }
    public int? POS_EPC { get; set; }
    public int? POS_ANTENA { get; set; }
    public int? POS_RSSI { get; set; }
    public int? POS_DATE_TIME { get; set; }
    public int? POS_ID_READER { get; set; }
}

public class ConfiguracionSafetySearchDto
{
    public string? Search { get; set; }
}