using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Contabilidad;

/// <summary>Entidad CUENTAS_CONTABLES - tipos equivalentes a la BD.</summary>
public class CuentasContables
{
    public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public string COD_CUENTA_CONTABLE_PADRE { get; set; } = string.Empty;
    public string COD_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public bool? FLG_GENERA_ASIENTO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public string COD_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CUENTA_NATURALEZA { get; set; } = string.Empty;
    public string DES_CUENTA_NATURALEZA_ALTERNATIVA { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_TIPO_CUENTA_CONTABLE { get; set; } = string.Empty;
}

public class CuentasContablesInsertDto
{
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    // Nullable: las cuentas de nivel 1 (raíz) no tienen padre — la ESTRUCTURA DE TABLA
    // marca la columna NULL. Antes estaba [Required], lo que habría bloqueado con 400 a
    // cualquier alta de cuenta raíz.
    public string? COD_CUENTA_CONTABLE_PADRE { get; set; }
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public bool? FLG_GENERA_ASIENTO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public string? COD_TIPO_CUENTA_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CuentasContablesUpdateDto
{
    [Required] public string COD_CUENTA_CONTABLE { get; set; } = string.Empty;
    [Required] public string DES_CUENTA_CONTABLE { get; set; } = string.Empty;
    public int? NUM_NIVEL { get; set; }
    public string? COD_CUENTA_CONTABLE_PADRE { get; set; }
    public string? COD_CUENTA_NATURALEZA { get; set; }
    public bool? FLG_GENERA_ASIENTO { get; set; }
    public bool? FLG_CENTRO_COSTO { get; set; }
    public string? COD_TIPO_CUENTA_CONTABLE { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class CuentasContablesSearchDto
{
    public string? Search { get; set; }
}