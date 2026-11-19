using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad CONTENEDORES_PRODUCCION - tipos equivalentes a la BD.</summary>
public class ContenedoresProduccion
{
    public int? NUM_CONTENEDOR_PRODUCCION { get; set; }
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_SUB_PROCESO { get; set; } = string.Empty;
}

public class ContenedoresProduccionInsertDto
{
    public int? NUM_CONTENEDOR_PRODUCCION { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ContenedoresProduccionUpdateDto
{
    public int? NUM_CONTENEDOR_PRODUCCION { get; set; }
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_PRODUCTO { get; set; } = string.Empty;
    public decimal? CAN_PRODUCTO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ContenedoresProduccionSearchDto
{
    public string? Search { get; set; }
}