using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad FAMILIAS_PRODUCTO - tipos equivalentes a la BD.</summary>
public class FamiliasProducto
{
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_BIEN { get; set; }
    public string? COD_CUBSO { get; set; }
    public string? COD_FAMILIA_PRODUCTO_ALIAS { get; set; }
    public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_PARTIDA_ARANCELARIA { get; set; } = string.Empty;
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
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
    public string DES_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string DES_TIPO_PRODUCTO { get; set; } = string.Empty;
    public string? DES_CUBSO { get; set; }
    public string? DES_CUBSO_ES { get; set; }
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? DES_UNIDAD_MEDIDA { get; set; }
}

public class FamiliasProductoInsertDto
{
    // Igual que en Grupos: para "Nuevo" el SP recalcula el código real vía
    // FNC_ObtenerSiguienteCorrelativo si el ALIAS aún no existe para ese Grupo. Sin
    // [Required] en el código: al crear se manda "" y [Required] rechaza vacío.
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_BIEN { get; set; }
    public string? COD_CUBSO { get; set; }
    [Required] public string COD_FAMILIA_PRODUCTO_ALIAS { get; set; } = string.Empty;
    // string?: campos opcionales — con Nullable habilitado, un "string" no anulable se
    // trata como obligatorio implícito si el JSON llega en null (combo sin elegir).
    public string? COD_TIPO_PRODUCTO { get; set; }
    public string? COD_PARTIDA_ARANCELARIA { get; set; }
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FamiliasProductoUpdateDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_BIEN { get; set; }
    [Required] public string COD_TIPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_PARTIDA_ARANCELARIA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class FamiliasProductoSearchDto
{
    public string? Search { get; set; }
}

public class FamiliasProductoLoadCombosResult
{
    public int                CodTipoEstado     { get; set; }
    public List<ComboboxItem> TiposProducto     { get; set; } = new();
    public List<EstadoItem>   Estados           { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida    { get; set; } = new();
    public List<ComboboxItem> TiposValorCalculo { get; set; } = new();
    /// <summary>Cubso (SUNAT) — antes se traía con una llamada HTTP aparte a
    /// ApiRoutes.Sunat.CubsoSunatCombobox; ahora viene en el mismo LOADCOMBOS.</summary>
    public List<ComboboxItem> Cubso             { get; set; } = new();
}