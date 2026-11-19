using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ENTIDADES_TIPOS_ENTIDADES - tipos equivalentes a la BD.</summary>
public class EntidadesTiposEntidades
{
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
}

public class EntidadesTiposEntidadesInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesTiposEntidadesUpdateDto
{
    public decimal? COD_ENTIDAD { get; set; }
    [Required] public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesTiposEntidadesSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Fila de PROC_ESTANDAR_ENTIDADES_TIPOS_ENTIDADES_SHOWWSELECT — todos los tipos de
/// entidad existentes, con FLG_SELECT indicando si ya está asignado a la entidad consultada.</summary>
public class EntidadesTiposEntidadesShowselectRow
{
    public bool FLG_SELECT { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO_ENTIDAD { get; set; } = string.Empty;
    public string DES_TIPO_ENTIDAD { get; set; } = string.Empty;
}