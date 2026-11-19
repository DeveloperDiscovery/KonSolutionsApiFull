using System.ComponentModel.DataAnnotations;

namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.ESTRUCTURA_RFID - tipos equivalentes a la BD.</summary>
public class EstructuraRfid
{
    public string COD_ESTRUCTURA_RFID { get; set; } = string.Empty;
    public string? DES_ESTRUCTURA_RFID { get; set; }
    public int? VAL_LONGITUD { get; set; }
    public int? NUM_ORDEN { get; set; }
}

public class EstructuraRfidInsertDto
{
    [Required] public string COD_ESTRUCTURA_RFID { get; set; } = string.Empty;
    public string? DES_ESTRUCTURA_RFID { get; set; }
    public int? VAL_LONGITUD { get; set; }
    public int? NUM_ORDEN { get; set; }
}

public class EstructuraRfidUpdateDto
{
    [Required] public string COD_ESTRUCTURA_RFID { get; set; } = string.Empty;
    public string? DES_ESTRUCTURA_RFID { get; set; }
    public int? VAL_LONGITUD { get; set; }
    public int? NUM_ORDEN { get; set; }
}

public class EstructuraRfidSearchDto
{
    public string? Search { get; set; }
}
