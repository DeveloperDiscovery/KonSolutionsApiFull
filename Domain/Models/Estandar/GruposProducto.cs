using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad GRUPOS_PRODUCTO - tipos equivalentes a la BD.</summary>
public class GruposProducto
{
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string DES_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_BIEN { get; set; }
    public string? COD_CUBSO { get; set; }
    public string? COD_GRUPO_PRODUCTO_ALIAS { get; set; }
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
    public string? DES_CUBSO { get; set; }
    public string? DES_CUBSO_ES { get; set; }
    public string? DES_TIPO_VALOR_CALCULO { get; set; }
    public string? DES_UNIDAD_MEDIDA { get; set; }
}

public class GruposProductoInsertDto
{
    // Para "Nuevo" el SP ignora este valor si el ALIAS aún no existe (genera el
    // código real vía FNC_ObtenerSiguienteCorrelativo) — igual se manda (puede ir
    // vacío) porque el parámetro del SP no admite NULL. Para editar sí debe ser el
    // código real del registro. Sin [Required]: al crear se manda "" y [Required]
    // rechaza string vacío por defecto, lo que rompía el ModelState.IsValid del Insert.
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_BIEN { get; set; }
    public string? COD_CUBSO { get; set; }
    [Required] public string COD_GRUPO_PRODUCTO_ALIAS { get; set; } = string.Empty;
    // string? (no solo sin [Required]): con Nullable habilitado, ASP.NET Core trata un
    // "string" no anulable como obligatorio IMPLÍCITAMENTE si el JSON no lo trae o lo
    // trae en null. Este campo es opcional, así que debe ser string?, no alcanza con
    // solo quitar [Required].
    public string? COD_PARTIDA_ARANCELARIA { get; set; }
    public string? COD_TIPO_VALOR_CALCULO { get; set; }
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposProductoUpdateDto
{
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string DES_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public bool? FLG_BIEN { get; set; }
    [Required] public string COD_PARTIDA_ARANCELARIA { get; set; } = string.Empty;
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class GruposProductoSearchDto
{
    public string? Search { get; set; }
}

public class GruposProductoLoadCombosResult
{
    public int                CodTipoEstado     { get; set; }
    public List<EstadoItem>   Estados           { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida    { get; set; } = new();
    public List<ComboboxItem> TiposValorCalculo { get; set; } = new();
}