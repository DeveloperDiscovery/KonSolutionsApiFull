using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad SUB_FAMILIAS_PRODUCTO - tipos equivalentes a la BD.</summary>
public class SubFamiliasProducto
{
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string? COD_CUBSO { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO_ALIAS { get; set; }
    public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_PARTIDA_ARANCELARIA { get; set; } = string.Empty;
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    /// <summary>Unidad en la que el almacén despacha los productos de esta sub familia.</summary>
    public string? COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN { get; set; }
    /// <summary>Cómo se leen los productos de la sub familia (ENTORNO.TIPOS_LECTURA: manual, código de barras, RFID).
    /// tinyint en la base.</summary>
    public byte? COD_TIPO_LECTURA { get; set; }
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
    public string DES_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string? DES_CUBSO { get; set; }
    public string? DES_CUBSO_ES { get; set; }
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? DES_UNIDAD_MEDIDA { get; set; }
    public string? DES_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public string? DES_UNIDAD_MEDIDA_DESPACHO_ALMACEN { get; set; }
    public string? DES_TIPO_LECTURA { get; set; }
}

public class SubFamiliasProductoInsertDto
{
    // Igual que Grupos/Familias: para "Nuevo" el SP recalcula el código real vía
    // FNC_ObtenerSiguienteCorrelativo si el ALIAS aún no existe para ese Grupo+Familia.
    // Sin [Required] en el código: al crear se manda "" y [Required] rechaza vacío.
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string? COD_CUBSO { get; set; }
    [Required] public string COD_SUB_FAMILIA_PRODUCTO_ALIAS { get; set; } = string.Empty;
    // string?: campos opcionales — con Nullable habilitado, un "string" no anulable se
    // trata como obligatorio implícito si el JSON llega en null (combo sin elegir).
    public string? COD_TIPO_PRODUCTO { get; set; }
    public string? COD_PARTIDA_ARANCELARIA { get; set; }
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public string? COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN { get; set; }
    public byte? COD_TIPO_LECTURA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubFamiliasProductoUpdateDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_PARTIDA_ARANCELARIA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class SubFamiliasProductoSearchDto
{
    public string? Search { get; set; }
}

public class SubFamiliasProductoLoadCombosResult
{
    public int                CodTipoEstado     { get; set; }
    public List<ComboboxItem> TiposProducto     { get; set; } = new();
    public List<EstadoItem>   Estados           { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida    { get; set; } = new();
    public List<ComboboxItem> TiposValorCalculo { get; set; } = new();
    /// <summary>Tipos de lectura (ENTORNO.TIPOS_LECTURA). No viene en el LOADCOMBOS de este módulo: se pide aparte
    /// con el SHOWCOMBOX de Tipos de Lectura, como Unidad de Medida y Tipo de Valor de Cálculo.</summary>
    public List<ComboboxItem> TiposLectura      { get; set; } = new();
    /// <summary>Cubso (SUNAT) — antes se traía con una llamada HTTP aparte a
    /// ApiRoutes.Sunat.CubsoSunatCombobox; ahora viene en el mismo LOADCOMBOS.</summary>
    public List<ComboboxItem> Cubso             { get; set; } = new();
}