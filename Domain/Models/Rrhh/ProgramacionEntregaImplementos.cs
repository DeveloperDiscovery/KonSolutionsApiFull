using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Rrhh;

/// <summary>Entidad PROGRAMACION_ENTREGA_IMPLEMENTOS - tipos equivalentes a la BD.</summary>
public class ProgramacionEntregaImplementos
{
    public int? NUM_ENTREGA_IMPLEMENTOS { get; set; }
    public string DES_ENTREGA_IMPLEMENTOS { get; set; } = string.Empty;
    public DateTime? FEC_ENTREGA_PROGRAMADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class ProgramacionEntregaImplementosInsertDto
{
    public int? NUM_ENTREGA_IMPLEMENTOS { get; set; }
    [Required] public string DES_ENTREGA_IMPLEMENTOS { get; set; } = string.Empty;
    public DateTime? FEC_ENTREGA_PROGRAMADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionEntregaImplementosUpdateDto
{
    public int? NUM_ENTREGA_IMPLEMENTOS { get; set; }
    [Required] public string DES_ENTREGA_IMPLEMENTOS { get; set; } = string.Empty;
    public DateTime? FEC_ENTREGA_PROGRAMADA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionEntregaImplementosSearchDto
{
    public string? Search { get; set; }
}