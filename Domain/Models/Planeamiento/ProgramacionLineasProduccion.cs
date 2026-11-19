using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad PROGRAMACION_LINEAS_PRODUCCION - tipos equivalentes a la BD.</summary>
public class ProgramacionLineasProduccion
{
    public DateTime? FEC_PROGRAMACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
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
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
}

public class ProgramacionLineasProduccionInsertDto
{
    public DateTime? FEC_PROGRAMACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionLineasProduccionUpdateDto
{
    public DateTime? FEC_PROGRAMACION { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionLineasProduccionSearchDto
{
    public string? Search { get; set; }
}