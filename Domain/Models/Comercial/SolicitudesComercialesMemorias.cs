using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Comercial;

/// <summary>Entidad SOLICITUDES_COMERCIALES_MEMORIAS - bitácora de anotaciones libres por
/// solicitud comercial ("Ayuda Memoria"). PK: NUM_SOLICITUD_COMERCIAL + NUM_SECUENCIA.</summary>
public class SolicitudesComercialesMemorias
{
    public int NUM_SOLICITUD_COMERCIAL { get; set; }
    public short NUM_SECUENCIA { get; set; }
    public string? DES_MEMORIA { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class SolicitudesComercialesMemoriasInsertDto
{
    [Required] public int NUM_SOLICITUD_COMERCIAL { get; set; }
    [Required] public short NUM_SECUENCIA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
    // COD_USUARIO_REGISTRO/COD_ESTACION_REGISTRO NO van acá — el controller los toma de la
    // sesión (GetCodUsuario()/GetEstacion()) y se los pasa al Service aparte, nunca del
    // body. Si estuvieran acá como string no-nullable (con Nullable enable en el .csproj),
    // ASP.NET Core los trata como implícitamente [Required] aunque el WebApp no los mande.
}

public class SolicitudesComercialesMemoriasSearchDto
{
    [Required] public int NUM_SOLICITUD_COMERCIAL { get; set; }
    public string? Search { get; set; }
}
