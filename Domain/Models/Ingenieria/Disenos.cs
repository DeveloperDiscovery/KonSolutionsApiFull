using System.ComponentModel.DataAnnotations;
using KonSolutionsApi.Common;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad DISENOS - tipos equivalentes a la BD.</summary>
public class Disenos
{
    public string COD_DISENO { get; set; } = string.Empty;
    public string DES_DISENO { get; set; } = string.Empty;
    public string COD_TIPO_DISENO { get; set; } = string.Empty;
    public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string COD_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public int? NUM_SOLICITUD_COMERICAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_COMERCIAL { get; set; } = string.Empty;
    public string DES_MATERNO { get; set; } = string.Empty;
    public string DES_NOMBRE { get; set; } = string.Empty;
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string DES_NOMBRE2 { get; set; } = string.Empty;
    public string DES_PATERNO { get; set; } = string.Empty;
    public string DES_TIPO_DISENO { get; set; } = string.Empty;
    public string DES_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    // Alias del estado (ESTANDAR.ESTADOS): 'ANU' deja el diseño en solo lectura.
    public string? COD_ESTADO_ALIAS { get; set; }
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_UNIDAD_MEDIDA { get; set; } = string.Empty;
    public string DES_TEMPORADA { get; set; } = string.Empty;
    public string DES_COLECCION { get; set; } = string.Empty;
    public string DES_GRUPO_PRODUCTO { get; set; } = string.Empty;
    public string DES_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string DES_OBSERVACIONES { get; set; } = string.Empty;
    public string DES_SOLICITUD_COMERCIAL { get; set; } = string.Empty;

    // Entidad responsable de la solicitud comercial vinculada. El SP la trae con
    // LEFT JOIN ESTANDAR.ENTIDADES j ON i.COD_ENTIDAD_RESPONSABLE = j.COD_ENTIDAD.
    public string DES_ENTIDAD_ASIGNADA { get; set; } = string.Empty;
}

public class DisenosInsertDto
{
    // Sin [Required]: al crear se genera vía correlativo (FNC_ObtenerSiguienteCorrelativo)
    // dentro del propio SP INSERT — este valor viaja vacío/ignorado al crear.
    public string COD_DISENO { get; set; } = string.Empty;
    [Required] public string DES_DISENO { get; set; } = string.Empty;
    [Required] public string COD_TIPO_DISENO { get; set; } = string.Empty;
    [Required] public string COD_GRUPO_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    [Required] public string COD_SUB_FAMILIA_PRODUCTO { get; set; } = string.Empty;
    public string? COD_UNIDAD_MEDIDA { get; set; }
    public string? COD_UNIDAD_MEDIDA_SECUNDARIA { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public int? NUM_TEMPORADA { get; set; }
    public int? NUM_COLECCION { get; set; }
    public int? NUM_SOLICITUD_COMERICAL { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class DisenosSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_INGENIERIA_DISENOS_LOADCOMBOS — un solo roundtrip para los
/// combos del diálogo en vez de llamadas separadas a Tipos de Diseño, Grupos de Producto,
/// Unidad de Medida y Entidades cliente.</summary>
public class DisenosLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<ComboboxItem> TiposDiseno { get; set; } = new();
    public List<ComboboxItem> GruposProducto { get; set; } = new();
    public List<ComboboxItem> UnidadesMedida { get; set; } = new();
    public List<ComboboxItem> Clientes { get; set; } = new();
    public List<EstadoItem> Estados { get; set; } = new();
}
