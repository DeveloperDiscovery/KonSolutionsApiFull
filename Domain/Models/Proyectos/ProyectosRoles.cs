using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Proyectos;

/// <summary>Entidad PROYECTOS_ROLES - tipos equivalentes a la BD.</summary>
public class ProyectosRoles
{
    public string COD_ROL_TAREA { get; set; } = string.Empty;
    public string DES_ROL_TAREA { get; set; } = string.Empty;
    public string? COD_CATEGORIA_ROL { get; set; }
    public string? DES_CATEGORIA_ROL { get; set; }
    public string? COD_ROL_PADRE { get; set; }
    public string? DES_ROL_PADRE { get; set; }
}

public class ProyectosRolesInsertDto
{
    [Required] public string COD_ROL_TAREA { get; set; } = string.Empty;
    [Required] public string DES_ROL_TAREA { get; set; } = string.Empty;
    public string? COD_CATEGORIA_ROL { get; set; }
    public string? COD_ROL_PADRE { get; set; }
}

public class ProyectosRolesUpdateDto
{
    [Required] public string COD_ROL_TAREA { get; set; } = string.Empty;
    [Required] public string DES_ROL_TAREA { get; set; } = string.Empty;
    public string? COD_CATEGORIA_ROL { get; set; }
    public string? COD_ROL_PADRE { get; set; }
}

public class ProyectosRolesSearchDto
{
    public string? Search { get; set; }
}