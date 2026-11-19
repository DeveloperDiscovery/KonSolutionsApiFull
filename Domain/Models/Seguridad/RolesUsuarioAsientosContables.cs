using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>Entidad SEGURIDAD.ROLES_USUARIO_ASIENTOS_CONTABLES — qué asientos contables puede
/// usar un rol. Hija de SEGURIDAD.ROLES_USUARIO. PK: COD_ROL_USUARIO + COD_ASIENTO_CONTABLE.
///
/// El SP _DISPONIBLES ya excluye los asientos anulados (alias de estado 'INA'): la pantalla no
/// vuelve a filtrar por estado, para no discrepar con la base si mañana cambia el criterio.</summary>
public class RolesUsuarioAsientosContables
{
    public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // Solo lectura (JOIN con ROLES_USUARIO y CONTABILIDAD.ASIENTOS_CONTABLES).
    public string? DES_ROL_USUARIO { get; set; }
    public string? DES_ASIENTO_CONTABLE { get; set; }
}

public class RolesUsuarioAsientosContablesInsertDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    [Required] public string COD_ASIENTO_CONTABLE { get; set; } = string.Empty;
}

public class RolesUsuarioAsientosContablesLoteDto
{
    [Required] public string COD_ROL_USUARIO { get; set; } = string.Empty;
    public List<string> Asientos { get; set; } = new();
}

public class RolesUsuarioAsientosContablesSearchDto
{
    public string? COD_ROL_USUARIO { get; set; }
    public string? Search { get; set; }
}

public class RolesUsuarioAsientosContablesLoteResult
{
    public int Procesados { get; set; }
    public List<string> Fallidos { get; set; } = new();
    public string? DesError { get; set; }
}
