using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>COMERCIAL.ENTIDADES_UNIDADES_COMERCIALES — unidades comerciales de una entidad
/// (pestaña "Unidades Comerciales" del diálogo de Entidades, después de Medios de Contacto).</summary>
public class EntidadesUnidadesComerciales
{
    public decimal? COD_ENTIDAD { get; set; }
    public byte? COD_UNIDAD_COMERCIAL { get; set; }
    public string? DES_UNIDAD_COMERCIAL { get; set; }
    public string? COD_PAIS { get; set; }
    public string? DES_DIRECCION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class EntidadesUnidadesComercialesInsertDto
{
    [Required] public decimal COD_ENTIDAD { get; set; }
    [Required] public byte COD_UNIDAD_COMERCIAL { get; set; }
    public string? DES_UNIDAD_COMERCIAL { get; set; }
    public string? COD_PAIS { get; set; }
    public string? DES_DIRECCION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesUnidadesComercialesUpdateDto : EntidadesUnidadesComercialesInsertDto { }

public class EntidadesUnidadesComercialesSearchDto
{
    [Required] public decimal CodEntidad { get; set; }
    public string? Search { get; set; }
}
