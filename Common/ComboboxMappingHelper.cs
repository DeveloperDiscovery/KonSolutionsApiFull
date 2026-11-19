using System.Collections.Generic;

namespace KonSolutionsApi.Common;

/// <summary>
/// Mapeo tolerante para SPs *_SHOWCOMBOX: distintos SPs de la BD están en distinto estado
/// de la migración a alias "Id"/"Texto" (algunos ya la tienen, otros todavía devuelven las
/// columnas crudas de la entidad, ej. COD_TIPO_ENTIDAD/DES_TIPO_ENTIDAD) — este helper
/// intenta primero Id/Texto y, si no existen en el resultado, cae al nombre de columna
/// crudo indicado. Así el repositorio no se rompe sin importar en qué SP se aplicó ya el
/// fix de alias y en cuál todavía no.
/// </summary>
public static class ComboboxMappingHelper
{
    public static ComboboxItem Map(IDictionary<string, object> row, string codColumn, string desColumn)
    {
        if (row.TryGetValue("Id", out var idAlias) && idAlias is not null)
        {
            row.TryGetValue("Texto", out var textoAlias);
            return new ComboboxItem
            {
                Id    = idAlias.ToString() ?? "",
                Texto = textoAlias?.ToString() ?? idAlias.ToString() ?? ""
            };
        }

        row.TryGetValue(codColumn, out var idRaw);
        row.TryGetValue(desColumn, out var desRaw);
        return new ComboboxItem
        {
            Id    = idRaw?.ToString() ?? "",
            Texto = desRaw?.ToString() ?? idRaw?.ToString() ?? ""
        };
    }
}
