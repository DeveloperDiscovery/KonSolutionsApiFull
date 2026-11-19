using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_DIRECCION - tipos equivalentes a la BD.</summary>
public class TiposDireccion
{
    public int? COD_TIPO_DIRECCION { get; set; }
    public string DES_TIPO_DIRECCION { get; set; } = string.Empty;
    public bool? FLG_COBRANZA { get; set; }
    public bool? FLG_ENTREGA { get; set; }
}

public class TiposDireccionInsertDto
{
    public int? COD_TIPO_DIRECCION { get; set; }
    [Required] public string DES_TIPO_DIRECCION { get; set; } = string.Empty;
    public bool? FLG_COBRANZA { get; set; }
    public bool? FLG_ENTREGA { get; set; }
}

public class TiposDireccionUpdateDto
{
    public int? COD_TIPO_DIRECCION { get; set; }
    [Required] public string DES_TIPO_DIRECCION { get; set; } = string.Empty;
    public bool? FLG_COBRANZA { get; set; }
    public bool? FLG_ENTREGA { get; set; }
}

public class TiposDireccionSearchDto
{
    public string? Search { get; set; }
}