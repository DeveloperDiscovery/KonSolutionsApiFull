using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Entidad INVENTARIOS_ALMACENES_PUNTO_TOMA_PERSONAL - tipos equivalentes a la BD.
/// Clave compuesta NUM_INVENTARIO + COD_ALMACEN + NUM_PUNTO_TOMA_INVENTARIO +
/// COD_ENTIDAD_ASIGNADA (persona asignada a un punto de conteo). Tabla de asignación pura:
/// SOLO Insert/Delete, NO tiene _UPDATE — el SP _INSERT es en realidad un UPSERT explícito
/// (intenta UPDATE de auditoría primero, y si @@ROWCOUNT=0 hace INSERT; es intencional, "la
/// fila es el dato, no hay nada que modificar" según el propio comentario del SP: reasignar
/// algo ya asignado es inofensivo). Por eso este módulo se expone solo como "Asignar"/"Quitar"
/// (mismo criterio que el patrón Asignar/Desasignar de RolesAlmacenes en otras partes del
/// proyecto), sin UpdateAsync ni modo Edición.
///
/// El SP no trae el nombre de la entidad asignada (solo su código) — se resuelve del lado
/// cliente cruzando con el combobox general de Entidades (ver comentario en
/// InventariosAlmacenesPuntoTomaPersonalGrid.razor).</summary>
public class InventariosAlmacenesPuntoTomaPersonal
{
    public int? NUM_INVENTARIO { get; set; }
    public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
    public DateTime? FEC_REGISTRO { get; set; }

    // ── Solo lectura: traído por SHOWALL/SHOWBYID/SHOWCOMBOX/SEARCH vía JOIN ──
    /// <summary>DES_OBSERVACION del INVENTARIOS_ALMACENES_PUNTO_TOMA padre (join b).</summary>
    public string? DES_OBSERVACION { get; set; }
}

public class InventariosAlmacenesPuntoTomaPersonalInsertDto
{
    public int? NUM_INVENTARIO { get; set; }
    [Required] public string COD_ALMACEN { get; set; } = string.Empty;
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public decimal? COD_ENTIDAD_ASIGNADA { get; set; }
    public string COD_USUARIO_REGISTRO { get; set; } = string.Empty;
    public string COD_ESTACION_REGISTRO { get; set; } = string.Empty;
}

public class InventariosAlmacenesPuntoTomaPersonalSearchDto
{
    public int? NUM_INVENTARIO { get; set; }
    public string? COD_ALMACEN { get; set; }
    public byte? NUM_PUNTO_TOMA_INVENTARIO { get; set; }
    public string? Search { get; set; }
}
