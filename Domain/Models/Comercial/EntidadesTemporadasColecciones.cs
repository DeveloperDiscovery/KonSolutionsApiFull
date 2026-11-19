using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad COMERCIAL.ENTIDADES_TEMPORADAS_COLECCIONES — Colecciones de una
/// Temporada de una Entidad. NUM_COLECCION NO es identity (el usuario lo ingresa, el
/// INSERT lo pasa explícito en la lista de columnas). Hijo de COMERCIAL.ENTIDADES_TEMPORADAS.</summary>
public class EntidadesTemporadasColecciones
{
    public decimal COD_ENTIDAD { get; set; }
    public int NUM_TEMPORADA { get; set; }
    public int NUM_COLECCION { get; set; }
    public string? DES_COLECCION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    // Join a COMERCIAL.ENTIDADES_TEMPORADAS
    public string? DES_TEMPORADA { get; set; }
    // Join a ESTANDAR.ESTADOS
    public string? DES_BACKCOLOR { get; set; }
    public string? DES_ESTADO { get; set; }
    public string? DES_FORECOLOR { get; set; }
}

public class EntidadesTemporadasColeccionesInsertDto
{
    [Required] public decimal COD_ENTIDAD { get; set; }
    [Required] public int NUM_TEMPORADA { get; set; }
    // Null en un alta nueva — el SP lo calcula como MAX(NUM_COLECCION)+1, ignorando lo recibido.
    public int? NUM_COLECCION { get; set; }
    [Required] public string DES_COLECCION { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class EntidadesTemporadasColeccionesSearchDto
{
    [Required] public decimal COD_ENTIDAD { get; set; }
    [Required] public int NUM_TEMPORADA { get; set; }
    public string? Search { get; set; }
}
