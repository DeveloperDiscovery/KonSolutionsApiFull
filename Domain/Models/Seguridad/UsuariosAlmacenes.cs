using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad USUARIOS_ALMACENES - tipos equivalentes a la BD.</summary>
public class UsuariosAlmacenes
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
    public string COD_USUARIO_ACTUALIZACION { get; set; } = string.Empty;
    public string COD_ESTACION_ACTUALIZACION { get; set; } = string.Empty;
    public DateTime? FEC_ACTUALIZACION { get; set; }
    public string DES_ABREVIADA { get; set; } = string.Empty;
    public string DES_ALMACEN { get; set; } = string.Empty;
}

public class UsuariosAlmacenesInsertDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAlmacenesUpdateDto
{
    [Required] public string COD_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class UsuariosAlmacenesSearchDto
{
    public string? Search { get; set; }
}