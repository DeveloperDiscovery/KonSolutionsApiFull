using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad CARGOS_FUNCIONES - tipos equivalentes a la BD.</summary>
public class CargosFunciones
{
    public int? COD_CARGO { get; set; }
    public string COD_FUNCION { get; set; } = string.Empty;
    public string DES_FUNCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
}

public class CargosFuncionesInsertDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string COD_FUNCION { get; set; } = string.Empty;
    [Required] public string DES_FUNCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosFuncionesUpdateDto
{
    public int? COD_CARGO { get; set; }
    [Required] public string COD_FUNCION { get; set; } = string.Empty;
    [Required] public string DES_FUNCION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CargosFuncionesSearchDto
{
    public string? Search { get; set; }
}