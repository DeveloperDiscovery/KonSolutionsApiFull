using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Entidad TIPOS_FLUJOS_PROCESO - tipos equivalentes a la BD.</summary>
public class TiposFlujosProceso
{
    public string COD_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public string DES_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public bool? FLG_AVISA_SOLO_RESPONSABLE { get; set; }
    public int? NUM_MINUTOS_AVISO_PREVIO_INICIO { get; set; }
    public int? NUM_MINUTOS_AVISO_PREVIO_FINAL { get; set; }
    public bool? FLG_ALARMA_PREVIA { get; set; }
    public int? NUN_REPETICIONES_PREVIA { get; set; }
    public int? NUM_SEGUNDOS_ALARMA_PREVIO { get; set; }
    public int? NUM_MINUTOS_AVISO_POSTERIOR_INICIO { get; set; }
    public int? NUM_MINUTOS_AVISO_POSTERIOR_FINAL { get; set; }
    public bool? FLG_ALARMA_POSTERIOR { get; set; }
    public int? NUM_SEGUNDOS_ALARMA_POSTERIOR { get; set; }
    public int? NUN_REPETICIONES_POSTERIOR { get; set; }
}

public class TiposFlujosProcesoInsertDto
{
    [Required] public string COD_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public bool? FLG_AVISA_SOLO_RESPONSABLE { get; set; }
    public int? NUM_MINUTOS_AVISO_PREVIO_INICIO { get; set; }
    public int? NUM_MINUTOS_AVISO_PREVIO_FINAL { get; set; }
    public bool? FLG_ALARMA_PREVIA { get; set; }
    public int? NUN_REPETICIONES_PREVIA { get; set; }
    public int? NUM_SEGUNDOS_ALARMA_PREVIO { get; set; }
    public int? NUM_MINUTOS_AVISO_POSTERIOR_INICIO { get; set; }
    public int? NUM_MINUTOS_AVISO_POSTERIOR_FINAL { get; set; }
    public bool? FLG_ALARMA_POSTERIOR { get; set; }
    public int? NUM_SEGUNDOS_ALARMA_POSTERIOR { get; set; }
    public int? NUN_REPETICIONES_POSTERIOR { get; set; }
}

public class TiposFlujosProcesoUpdateDto
{
    [Required] public string COD_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_FLUJO_PROCESO { get; set; } = string.Empty;
    public bool? FLG_AVISA_SOLO_RESPONSABLE { get; set; }
    public int? NUM_MINUTOS_AVISO_PREVIO_INICIO { get; set; }
    public int? NUM_MINUTOS_AVISO_PREVIO_FINAL { get; set; }
    public bool? FLG_ALARMA_PREVIA { get; set; }
    public int? NUN_REPETICIONES_PREVIA { get; set; }
    public int? NUM_SEGUNDOS_ALARMA_PREVIO { get; set; }
    public int? NUM_MINUTOS_AVISO_POSTERIOR_INICIO { get; set; }
    public int? NUM_MINUTOS_AVISO_POSTERIOR_FINAL { get; set; }
    public bool? FLG_ALARMA_POSTERIOR { get; set; }
    public int? NUM_SEGUNDOS_ALARMA_POSTERIOR { get; set; }
    public int? NUN_REPETICIONES_POSTERIOR { get; set; }
}

public class TiposFlujosProcesoSearchDto
{
    public string? Search { get; set; }
}