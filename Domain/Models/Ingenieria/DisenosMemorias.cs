using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Ingenieria;

/// <summary>Entidad DISENOS_MEMORIAS - bitácora de anotaciones libres por diseño
/// ("Ayuda Memoria"). PK: COD_DISENO + NUM_SECUENCIA. Mismo patrón que
/// COMERCIAL.SOLICITUDES_COMERCIALES_MEMORIAS.</summary>
public class DisenosMemorias
{
    public string COD_DISENO { get; set; } = string.Empty;
    public short NUM_SECUENCIA { get; set; }
    public string? DES_MEMORIA { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class DisenosMemoriasInsertDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public short NUM_SECUENCIA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
    // COD_USUARIO_REGISTRO/COD_ESTACION_REGISTRO NO van acá: el controller los toma de la
    // sesión y se los pasa al Service aparte. Como string no-nullable (con Nullable enable)
    // ASP.NET Core los trataría como implícitamente [Required] en el body.
}

public class DisenosMemoriasSearchDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    public string? Search { get; set; }
}

/// <summary>Entidad DISENOS_VERSIONES_MEMORIAS - igual que DisenosMemorias pero acotada a
/// una versión concreta. PK: COD_DISENO + NUM_VERSION + NUM_SECUENCIA.</summary>
public class DisenosVersionesMemorias
{
    public string COD_DISENO { get; set; } = string.Empty;
    public short NUM_VERSION { get; set; }
    public short NUM_SECUENCIA { get; set; }
    public string? DES_MEMORIA { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

public class DisenosVersionesMemoriasInsertDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public short NUM_VERSION { get; set; }
    [Required] public short NUM_SECUENCIA { get; set; }
    [Required] public string DES_MEMORIA { get; set; } = string.Empty;
}

public class DisenosVersionesMemoriasSearchDto
{
    [Required] public string COD_DISENO { get; set; } = string.Empty;
    [Required] public short NUM_VERSION { get; set; }
    public string? Search { get; set; }
}
