using Dapper;

namespace KonSolutionsApi.Common;

/// <summary>Reconstruye la sentencia EXEC + parámetros (solo para mostrar en el detalle
/// técnico del error, nunca para ejecutar) a partir de los DynamicParameters ya armados
/// para la llamada a Dapper que falló.</summary>
public static class SqlSentenceHelper
{
    /// <summary>Acepta tanto DynamicParameters (Insert/Update, que arman los parámetros con
    /// p.Add(...)) como un objeto anónimo inline (Delete, que suele pasar "new { COD_X = x }"
    /// directo a Dapper) — Dapper ya sabe leer las propiedades de un objeto anónimo con este
    /// mismo constructor de DynamicParameters, así que no hace falta duplicar lógica.</summary>
    public static string Build(string storedProcedure, object parametros)
    {
        var dp = parametros as DynamicParameters ?? new DynamicParameters(parametros);
        var nombres = dp.ParameterNames.ToList();
        var anchoMax = nombres.Count > 0 ? nombres.Max(n => n.Length) : 0;

        var lineas = nombres.Select((nombre, i) =>
        {
            var valor = dp.Get<object?>(nombre);
            var secreto = Secreto(nombre);   // una clave nunca sale en el detalle técnico del error
            var literal = valor switch
            {
                null => "NULL",
                not null when secreto => "N'********'",
                string s => $"N'{s.Replace("'", "''")}'",
                bool b => b ? "1" : "0",
                DateTime dt => $"'{dt:yyyy-MM-dd HH:mm:ss}'",
                _ => valor.ToString()
            };
            var coma = i < nombres.Count - 1 ? "," : "";
            return $"\t\t@{nombre.PadRight(anchoMax)}\t= {literal}{coma}";
        });

        return $"EXEC\t[{storedProcedure}]\n{string.Join("\n", lineas)}";
    }

    private static bool Secreto(string nombre) =>
        nombre.Contains("PASSWORD", StringComparison.OrdinalIgnoreCase) || nombre.Contains("CLAVE", StringComparison.OrdinalIgnoreCase) || nombre.Contains("SECRET", StringComparison.OrdinalIgnoreCase);
}
