using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Safety;

/// <summary>Entidad ACTIVOS_TAG - tipos equivalentes a la BD.</summary>
public class ActivosTag
{
    public string COD_ACTIVO_TAG { get; set; } = string.Empty;
    public string COD_ACTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ACTIVO { get; set; } = string.Empty;
    public string DES_MARCA { get; set; } = string.Empty;
    public string DES_MODELO { get; set; } = string.Empty;
    public string DES_SERIE { get; set; } = string.Empty;
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_MOTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD { get; set; }
}

public class ActivosTagInsertDto
{
    [Required] public string COD_ACTIVO_TAG { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosTagUpdateDto
{
    [Required] public string COD_ACTIVO_TAG { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO { get; set; } = string.Empty;
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public int? COD_TIPO_MOTIVO { get; set; }
    public int? COD_MOTIVO { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ActivosTagSearchDto
{
    public string? Search { get; set; }
}