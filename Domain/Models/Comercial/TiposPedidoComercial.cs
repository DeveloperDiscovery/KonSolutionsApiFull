using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad TIPOS_PEDIDO_COMERCIAL - tipos equivalentes a la BD.</summary>
public class TiposPedidoComercial
{
    public int? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    public string DES_TIPO_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_TIPO_ENTIDAD_VENDEDORA { get; set; } = string.Empty;
    public bool? FLG_PEDIDO_FORMAL { get; set; }
    public bool? FLG_BIENES { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO_INTERNO { get; set; }

    // Solo lectura (vienen del JOIN en SHOWALL/SEARCH/SHOWBYID).
    public string? DES_TIPO_ENTIDAD { get; set; }
    public string? DES_TIPO_ENTIDAD_VENDEDORA { get; set; }
    public string? DES_TIPO_COMPROBANTE_PAGO { get; set; }
}

public class TiposPedidoComercialInsertDto
{
    public int? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    [Required] public string DES_TIPO_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_VENDEDORA { get; set; } = string.Empty;
    public bool? FLG_PEDIDO_FORMAL { get; set; }
    public bool? FLG_BIENES { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO_INTERNO { get; set; }
}

public class TiposPedidoComercialUpdateDto
{
    public int? COD_TIPO_PEDIDO_COMERCIAL { get; set; }
    [Required] public string DES_TIPO_PEDIDO_COMERCIAL { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    [Required] public string COD_TIPO_ENTIDAD_VENDEDORA { get; set; } = string.Empty;
    public bool? FLG_PEDIDO_FORMAL { get; set; }
    public bool? FLG_BIENES { get; set; }
    public string? COD_TIPO_COMPROBANTE_PAGO_INTERNO { get; set; }
}

public class TiposPedidoComercialSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_COMERCIAL_TIPOS_PEDIDO_COMERCIAL_LOADCOMBOS: tipos de entidad y
/// tipos de comprobante de pago (los 2 combos independientes de TiposPedidoComercialDialog.razor)
/// en un solo viaje.</summary>
public class TiposPedidoComercialLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> TiposEntidad         { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> TiposComprobantePago { get; set; } = new();
}