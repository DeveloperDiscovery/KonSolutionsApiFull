using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad MANUALES_FUNCIONES - tipos equivalentes a la BD.</summary>
public class ManualesFunciones
{
    public int? NUM_MANUAL_FUNCIONES { get; set; }
    public string DES_MANUAL_FUNCIONES { get; set; } = string.Empty;
    public string DES_TEXTO { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CARGO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class ManualesFuncionesInsertDto
{
    public int? NUM_MANUAL_FUNCIONES { get; set; }
    [Required] public string DES_MANUAL_FUNCIONES { get; set; } = string.Empty;
    [Required] public string DES_TEXTO { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ManualesFuncionesUpdateDto
{
    public int? NUM_MANUAL_FUNCIONES { get; set; }
    [Required] public string DES_MANUAL_FUNCIONES { get; set; } = string.Empty;
    [Required] public string DES_TEXTO { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ManualesFuncionesSearchDto
{
    public string? Search { get; set; }
}