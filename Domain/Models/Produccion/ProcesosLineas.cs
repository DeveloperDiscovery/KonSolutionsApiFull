using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Produccion;

/// <summary>Entidad PROCESOS_LINEAS - tipos equivalentes a la BD.</summary>
public class ProcesosLineas
{
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_MAQUINA { get; set; }
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
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string DES_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
}

public class ProcesosLineasInsertDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_MAQUINA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProcesosLineasUpdateDto
{
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_TIPO_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    public bool? FLG_MANUAL { get; set; }
    public bool? FLG_MAQUINA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProcesosLineasSearchDto
{
    public string? Search { get; set; }
}