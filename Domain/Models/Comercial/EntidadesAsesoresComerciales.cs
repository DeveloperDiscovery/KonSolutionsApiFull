using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad COMERCIAL.ENTIDADES_ASESORES_COMERCIALES — tabla puente: qué asesores
/// comerciales (personas, ESTANDAR.ENTIDADES) están asignados a un cliente (también una
/// ESTANDAR.ENTIDADES). PK compuesta (COD_ENTIDAD_CLIENTE, COD_ENTIDAD_ASESOR_COMERCIAL).
/// Solo aplica al Catálogo de Clientes (COD_TIPO_ENTIDAD = 'CLI').</summary>
public class EntidadesAsesoresComerciales
{
    public decimal? COD_ENTIDAD_CLIENTE { get; set; }
    public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string? DES_COMERCIAL { get; set; }
    public string? DES_MATERNO { get; set; }
    public string? DES_NOMBRE { get; set; }
    public string? DES_NOMBRE_COMPLETO { get; set; }
    public string? DES_NOMBRE2 { get; set; }
    public string? DES_PATERNO { get; set; }
    public string? DES_PREFIJO_USUARIO_COMERCIAL { get; set; }
}

public class EntidadesAsesoresComercialesInsertDto
{
    [Required] public decimal? COD_ENTIDAD_CLIENTE { get; set; }
    [Required] public decimal? COD_ENTIDAD_ASESOR_COMERCIAL { get; set; }
}

public class EntidadesAsesoresComercialesSearchDto
{
    [Required] public decimal? COD_ENTIDAD_CLIENTE { get; set; }
    public string? Search { get; set; }
}
