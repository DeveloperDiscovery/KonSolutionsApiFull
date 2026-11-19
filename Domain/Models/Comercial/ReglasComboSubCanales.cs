using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad REGLAS_COMBO_SUB_CANALES - tipos equivalentes a la BD.</summary>
public class ReglasComboSubCanales
{
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_REGLA_COMBOS { get; set; } = string.Empty;
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
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
    public string DES_REGLA_COMBOS { get; set; } = string.Empty;
    public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public int? NUM_UNIDAD_NEGOCIO { get; set; }
}

public class ReglasComboSubCanalesInsertDto
{
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_REGLA_COMBOS { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ReglasComboSubCanalesUpdateDto
{
    public int? NUM_UNIDAD_COMERCIAL { get; set; }
    public int? NUM_CANAL_VENTA { get; set; }
    public int? NUM_SUB_CANAL_VENTA { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_REGLA_COMBOS { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ReglasComboSubCanalesSearchDto
{
    public string? Search { get; set; }
}