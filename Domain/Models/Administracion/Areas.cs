using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Administracion;

/// <summary>Entidad AREAS - tipos equivalentes a la BD.</summary>
public class Areas
{
    public int? COD_AREA { get; set; }
    public string DES_AREA { get; set; } = string.Empty;
    public int? COD_AREA_PADRE { get; set; }
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_CENTRO_COSTO { get; set; } = string.Empty;
    public string DES_BACKCOLOR { get; set; } = string.Empty;
    public string DES_ESTADO { get; set; } = string.Empty;
    public string DES_FORECOLOR { get; set; } = string.Empty;
    public string DES_DIRECCION { get; set; } = string.Empty;
    public string DES_LINK_PATH_FACTURACION { get; set; } = string.Empty;
    public string DES_SUCURSAL { get; set; } = string.Empty;
    public string DES_TOKEN_FACTURACION { get; set; } = string.Empty;
}

public class AreasInsertDto
{
    public int? COD_AREA { get; set; }
    [Required] public string DES_AREA { get; set; } = string.Empty;
    public int? COD_AREA_PADRE { get; set; }
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AreasUpdateDto
{
    public int? COD_AREA { get; set; }
    [Required] public string DES_AREA { get; set; } = string.Empty;
    public int? COD_AREA_PADRE { get; set; }
    public string COD_SUCURSAL { get; set; } = string.Empty;
    public string COD_CENTRO_COSTO { get; set; } = string.Empty;
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int? COD_TIPO_ESTADO { get; set; }
    public int? COD_ESTADO { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class AreasSearchDto
{
    public string? Search { get; set; }
}

/// <summary>Resultado de PROC_ADMINISTRACION_AREAS_LOADCOMBOS: sucursales, centros de costo
/// y estados (filtrados por @COD_TIPO_ESTADO) en un solo viaje para el diálogo de Áreas.</summary>
public class AreasLoadCombosResult
{
    public int CodTipoEstado { get; set; }
    public List<KonSolutionsApi.Common.ComboboxItem> Sucursales    { get; set; } = new();
    public List<KonSolutionsApi.Common.ComboboxItem> CentrosCosto { get; set; } = new();
    public List<KonSolutionsApi.Common.EstadoItem>   Estados      { get; set; } = new();
}