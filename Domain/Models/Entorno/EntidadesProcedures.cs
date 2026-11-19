namespace KonSolutionsApi.Domain.Models.Entorno;

/// <summary>Fila de PROC_ENTORNO_ENTIDADES_PROCEDURES_PARAMETERS_SHOWALL: un parámetro del SP
/// consultado. El SP devuelve b.name y c.name SIN alias (ambas columnas se llaman "name" —
/// la del parámetro y la del tipo), así que Dapper no puede mapear por nombre: el repositorio
/// lee por ORDINAL (0=nombre parámetro, 1=system_type_id, 2=nombre tipo), no por convención.</summary>
public class EntidadProcedureParametro
{
    public string ParameterName { get; set; } = string.Empty;
    public int SystemTypeId { get; set; }
    public string TypeName { get; set; } = string.Empty;
}
