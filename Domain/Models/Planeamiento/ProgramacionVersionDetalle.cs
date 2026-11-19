using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Planeamiento;

/// <summary>Entidad PROGRAMACION_VERSION_DETALLE - tipos equivalentes a la BD.</summary>
public class ProgramacionVersionDetalle
{
    public int? NUM_PROGRAMA { get; set; }
    public int? NUM_VERSION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_PROCESO { get; set; } = string.Empty;
    public string COD_SUB_PROCESO { get; set; } = string.Empty;
    public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string COD_ACTIVO_MAQUINA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PROGRAMADA { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public DateTime? FEC_PROGRAMADA_INICIO { get; set; }
    public DateTime? FEC_PROGRAMADA_FINAL { get; set; }
    public DateTime? FEC_REAL_INICIO { get; set; }
    public DateTime? FEC_REAL_FINAL { get; set; }
    public string DES_OBSERVACION { get; set; } = string.Empty;
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
    public string DES_LINEA_PRODUCCION { get; set; } = string.Empty;
    public string DES_VERSION { get; set; } = string.Empty;
    public string DES_ALIAS { get; set; } = string.Empty;
    public string DES_EQUIPO_PRODUCCION_PRINCIPAL { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_ITEM { get; set; } = string.Empty;
    public string DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD { get; set; } = string.Empty;
    public string DES_SUB_PROCESO { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
    public string DES_TIPO_PROGRAMACION { get; set; } = string.Empty;
    public string COD_ACTIVO { get; set; } = string.Empty;
}

public class ProgramacionVersionDetalleInsertDto
{
    public int? NUM_PROGRAMA { get; set; }
    public int? NUM_VERSION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO_MAQUINA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PROGRAMADA { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public DateTime? FEC_PROGRAMADA_INICIO { get; set; }
    public DateTime? FEC_PROGRAMADA_FINAL { get; set; }
    public DateTime? FEC_REAL_INICIO { get; set; }
    public DateTime? FEC_REAL_FINAL { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionVersionDetalleUpdateDto
{
    public int? NUM_PROGRAMA { get; set; }
    public int? NUM_VERSION { get; set; }
    public int? NUM_SECUENCIA { get; set; }
    [Required] public string COD_TIPO_PROGRAMACION { get; set; } = string.Empty;
    [Required] public string COD_SUCURSAL { get; set; } = string.Empty;
    [Required] public string COD_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_SUB_PROCESO { get; set; } = string.Empty;
    [Required] public string COD_LINEA_PRODUCCION { get; set; } = string.Empty;
    [Required] public string COD_ACTIVO_MAQUINA { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRODUCCION { get; set; }
    public int? NUM_ORDEN_PRODUCCION_ITEM { get; set; }
    public int? NUM_ORDEN_PRODUCCION_UNIDAD { get; set; }
    public decimal? CAN_PROGRAMADA { get; set; }
    public decimal? CAN_PRODUCIDA { get; set; }
    public DateTime? FEC_PROGRAMADA_INICIO { get; set; }
    public DateTime? FEC_PROGRAMADA_FINAL { get; set; }
    public DateTime? FEC_REAL_INICIO { get; set; }
    public DateTime? FEC_REAL_FINAL { get; set; }
    [Required] public string DES_OBSERVACION { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class ProgramacionVersionDetalleSearchDto
{
    public string? Search { get; set; }
}