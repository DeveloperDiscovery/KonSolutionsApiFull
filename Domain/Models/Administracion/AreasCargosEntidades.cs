using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Administracion;

/// <summary>Entidad ADMINISTRACION.AREAS_CARGOS_ENTIDADES — tabla puente: qué entidades
/// (personas) están asignadas a un cargo dentro de un área. PK compuesta
/// (COD_AREA, COD_CARGO, COD_ENTIDAD).</summary>
public class AreasCargosEntidades
{
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }
}

public class AreasCargosEntidadesInsertDto
{
    [Required] public int? COD_AREA { get; set; }
    [Required] public int? COD_CARGO { get; set; }
    [Required] public decimal? COD_ENTIDAD { get; set; }
}

public class AreasCargosEntidadesSearchDto
{
    [Required] public int? COD_AREA { get; set; }
    [Required] public int? COD_CARGO { get; set; }
    public string? Search { get; set; }
}

/// <summary>Fila plana del árbol Área &gt; Cargo &gt; Entidad asignada
/// (PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES_TREEVIEW, sin parámetros — trae TODO el
/// árbol de una vez). COD_TIPO: 'A' = Área, 'C' = Cargo, 'P' = Entidad/Persona asignada.</summary>
public class AreasCargosEntidadesNodo
{
    public int COD_NODO { get; set; }
    public string DES_NODO { get; set; } = string.Empty;
    public int? COD_NODO_PADRE { get; set; }
    public int? COD_AREA { get; set; }
    public int? COD_CARGO { get; set; }
    public decimal? COD_ENTIDAD { get; set; }
    public string COD_TIPO { get; set; } = string.Empty;
}

/// <summary>Fila de PROC_ESTANDAR_ENTIDADES_ENTIDADES_DISPONIBLES — personas activas que
/// todavía no están asignadas a ningún área/cargo, candidatas para el diálogo "Asignar".</summary>
public class EntidadDisponible
{
    public decimal COD_ENTIDAD { get; set; }
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public string? DES_TIPO_DOCUMENTO_IDENTIDAD { get; set; }
    public string? NUM_DOCUMENTO_IDENTIDAD { get; set; }
}

/// <summary>Fila de PROC_ADMINISTRACION_AREAS_CARGOS_ENTIDADES (@COD_APLICACION,
/// @COD_OPCION_APLICACION) — personas asignadas a algún cargo del Área enlazada a esa
/// opción de menú vía ESTANDAR.APLICACIONES_OPCIONES_AREAS.</summary>
public class AreaCargoEntidadPorOpcion
{
    public decimal COD_ENTIDAD { get; set; }
    public string DES_NOMBRE_COMPLETO { get; set; } = string.Empty;
    public int? COD_CARGO { get; set; }
    public string? DES_CARGO { get; set; }
    public int? COD_AREA { get; set; }
    public string? DES_AREA { get; set; }
    public int? COD_AREA_PADRE { get; set; }
}
