using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_ROLES_CATEGORIAS (categorias de los roles de proyecto).</summary>
public class ProyectosRolesCategorias
{
    // Auditoria (la muestra PanelAuditoria en el dialogo)
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string COD_CATEGORIA_ROL { get; set; } = string.Empty;
    public string DES_CATEGORIA_ROL { get; set; } = string.Empty;
    public string? DES_DESCRIPCION { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
    public int NUM_ROLES { get; set; }
}

public class ProyectosRolesCategoriasInsertDto
{
    [Required] public string COD_CATEGORIA_ROL { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_ROL { get; set; } = string.Empty;
    public string? DES_DESCRIPCION { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class ProyectosRolesCategoriasUpdateDto
{
    [Required] public string COD_CATEGORIA_ROL { get; set; } = string.Empty;
    [Required] public string DES_CATEGORIA_ROL { get; set; } = string.Empty;
    public string? DES_DESCRIPCION { get; set; }
    public int? NUM_ORDEN_PRESENTACION { get; set; }
}

public class ProyectosRolesCategoriasSearchDto
{
    public string? Search { get; set; }
}
