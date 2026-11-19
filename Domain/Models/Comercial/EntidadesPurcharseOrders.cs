using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_PURCHARSE_ORDERS - tipos equivalentes a la BD.</summary>
public class EntidadesPurcharseOrders
{
    public decimal? COD_ENTIDAD { get; set; }
    public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    public string COD_DISENO { get; set; } = string.Empty;
    public string DES_DISENO_ENTIDAD { get; set; } = string.Empty;
    public DateTime? FEC_DISENO_ENTIDAD { get; set; }
    public bool? FLG_ITERAR_EMBALAJE { get; set; }
    public bool? FLG_PURCHASE_ORDER_GENERICA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public string DES_DISENO_ENTIDAD { get; set; } = string.Empty;
    public DateTime? FEC_DISENO_ENTIDAD { get; set; }
    public bool? FLG_ITERAR_EMBALAJE { get; set; }
    public bool? FLG_PURCHASE_ORDER_GENERICA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string NUM_PURCHASE_ORDER { get; set; } = string.Empty;
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public string DES_DISENO_ENTIDAD { get; set; } = string.Empty;
    public DateTime? FEC_DISENO_ENTIDAD { get; set; }
    public bool? FLG_ITERAR_EMBALAJE { get; set; }
    public bool? FLG_PURCHASE_ORDER_GENERICA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesPurcharseOrdersSearchDto
{
    public string? Search { get; set; }
}