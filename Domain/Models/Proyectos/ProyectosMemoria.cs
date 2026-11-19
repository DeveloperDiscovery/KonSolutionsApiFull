using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_MEMORIA - tipos equivalentes a la BD.</summary>
public class ProyectosMemoria
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    public string DES_MEMORIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
    public string DES_PROYECTO { get; set; } = string.Empty;
}

public class ProyectosMemoriaInsertDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    /// <summary>Evento del proyecto al que se liga la memoria (opcional).</summary>
    public int? NUM_SECUENCIA_EVENTO { get; set; }
}

public class ProyectosMemoriaUpdateDto
{
    public int? NUM_PROYECTO { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public int? COD_FASE { get; set; }
    public int? COD_TAREA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public int? NUM_SECUENCIA_EVENTO { get; set; }
}

public class ProyectosMemoriaSearchDto
{
    public string? Search { get; set; }
}