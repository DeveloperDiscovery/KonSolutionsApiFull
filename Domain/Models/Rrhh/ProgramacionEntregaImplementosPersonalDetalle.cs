using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PROGRAMACION_ENTREGA_IMPLEMENTOS_PERSONAL_DETALLE - tipos equivalentes a la BD.</summary>
public class ProgramacionEntregaImplementosPersonalDetalle
{
    public int? NUM_ENTREGA_IMPLEMENTOS { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    public DateTime? FEC_ENTREGA { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DETALLADA { get; set; } = string.Empty;
    public string DES_PRODUCTO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
}

public class ProgramacionEntregaImplementosPersonalDetalleInsertDto
{
    public int? NUM_ENTREGA_IMPLEMENTOS { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    public DateTime? FEC_ENTREGA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionEntregaImplementosPersonalDetalleUpdateDto
{
    public int? NUM_ENTREGA_IMPLEMENTOS { get; set; }
    public decimal? COD_ENTIDAD_PERSONAL { get; set; }
    [Required] public string COD_PRODUCTO_IMPLEMENTO { get; set; } = string.Empty;
    public DateTime? FEC_ENTREGA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionEntregaImplementosPersonalDetalleSearchDto
{
    public string? Search { get; set; }
}