using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Networking;

/// <summary>Entidad CATEGORIAS_COMISION_HISTORICO - tipos equivalentes a la BD.</summary>
public class CategoriasComisionHistorico
{
    public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public decimal? TAS_COMISION { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CATEGORIA_COMISION { get; set; } = string.Empty;
}

public class CategoriasComisionHistoricoInsertDto
{
    [Required] public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public decimal? TAS_COMISION { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CategoriasComisionHistoricoUpdateDto
{
    [Required] public string COD_CATEGORIA_COMISION { get; set; } = string.Empty;
    public int? NUM_SECUENCIA { get; set; }
    public DateTime? FEC_INICIO { get; set; }
    public DateTime? FEC_FINAL { get; set; }
    public decimal? TAS_COMISION { get; set; }
    public decimal? TAS_COMISON_REFERENCIADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CategoriasComisionHistoricoSearchDto
{
    public string? Search { get; set; }
}