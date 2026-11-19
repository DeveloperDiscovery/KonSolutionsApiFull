using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad TIPOS_DOCUMENTO_COMPROMISO.</summary>
public class TiposDocumentoCompromiso
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string COD_TIPO_DOCUMENTO_COMPROMISO { get; set; } = string.Empty;
    public string DES_TIPO_DOCUMENTO_COMPROMISO { get; set; } = string.Empty;
    /// <summary>C = compromiso, A = aceptacion.</summary>
    public string COD_CLASE_COMPROMISO { get; set; } = "C";
    public string? DES_CLASE_COMPROMISO { get; set; }
    public string? DES_DESCRIPCION { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int NUM_DOCUMENTOS { get; set; }
}

public class TiposDocumentoCompromisoInsertDto
{
    [Required] public string COD_TIPO_DOCUMENTO_COMPROMISO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_DOCUMENTO_COMPROMISO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_COMPROMISO { get; set; } = "C";
    public string? DES_DESCRIPCION { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposDocumentoCompromisoUpdateDto
{
    [Required] public string COD_TIPO_DOCUMENTO_COMPROMISO { get; set; } = string.Empty;
    [Required] public string DES_TIPO_DOCUMENTO_COMPROMISO { get; set; } = string.Empty;
    [Required] public string COD_CLASE_COMPROMISO { get; set; } = "C";
    public string? DES_DESCRIPCION { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class TiposDocumentoCompromisoSearchDto
{
    public string? Search { get; set; }
}
