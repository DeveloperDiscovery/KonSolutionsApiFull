using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad LOTES_CONTROL_DETALLE - tipos equivalentes a la BD.</summary>
public class LotesControlDetalle
{
    public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? VAL_TOTAL { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
    public string DES_LOTE_CONTROL { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
}

public class LotesControlDetalleInsertDto
{
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? VAL_TOTAL { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
}

public class LotesControlDetalleUpdateDto
{
    [Required] public string NUM_LOTE_CONTROL { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? VAL_TOTAL { get; set; }
    public decimal? CAN_PRODUCTO { get; set; }
}

public class LotesControlDetalleSearchDto
{
    public string? Search { get; set; }
}