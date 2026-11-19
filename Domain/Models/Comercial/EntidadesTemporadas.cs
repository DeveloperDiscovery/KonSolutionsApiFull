using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad ENTIDADES_TEMPORADAS - tipos equivalentes a la BD.</summary>
public class EntidadesTemporadas
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_TEMPORADA { get; set; }
    public string DES_TEMPORADA { get; set; } = string.Empty;
    public int? NUM_ANO { get; set; }
    public int? NUM_MES { get; set; }
    public int? MES_FINAL { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
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
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
}

public class EntidadesTemporadasInsertDto
{
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_TEMPORADA { get; set; }
    [Required] public string DES_TEMPORADA { get; set; } = string.Empty;
    public int? NUM_ANO { get; set; }
    public int? NUM_MES { get; set; }
    public int? MES_FINAL { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesTemporadasSearchDto
{
    [Required] public decimal COD_ENTIDAD { get; set; }
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_COMERCIAL_ENTIDADES_TEMPORADAS_LOADCOMBOS: años y meses (los 2
/// combos independientes de EntidadesTemporadasDialog.razor) en un solo viaje.</summary>
public class EntidadesTemporadasLoadCombosResult
{
    public List<KonSolutionsApi.Common.ComboboxItem> Anos  { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> Meses { get; set; } = new();
}