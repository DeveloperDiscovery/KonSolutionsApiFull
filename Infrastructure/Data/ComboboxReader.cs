using System.Data;
using Dapper;
using KonSolutionsApi.Common;

namespace KonSolutionsApi.Infrastructure.Data;

/// <summary>Lectura de un SP <c>_SHOWCOMBOX</c> hacia <see cref="ComboboxItem"/>.
///
/// POR QUÉ EXISTE: los SP de combo no devuelven "Id"/"Texto" de forma estable. El generador
/// los emite con las columnas propias de la tabla —<c>COD_XXX</c> y <c>DES_XXX</c>— y cada
/// vez que un SP se regenera pierde los alias. Mapear <c>ComboboxItem</c> directamente con
/// Dapper deja entonces el combo con todos los textos en blanco, y el síntoma aparece lejos
/// del cambio que lo causó: nadie relaciona "regeneré el SP" con "el combo salió vacío".
///
/// La convención de la base es el nombre de la columna, así que se lee SIEMPRE por
/// convención: el código es la primera columna <c>COD_*</c> y la descripción la primera
/// <c>DES_*</c>. Los alias genéricos <c>Id</c>/<c>Texto</c> NO se reconocen a propósito — un
/// SP que todavía los use devuelve el combo vacío hasta que se lo migre a columnas reales,
/// en vez de convivir en silencio con el patrón viejo.</summary>
public static class ComboboxReader
{
    public static async Task<IEnumerable<ComboboxItem>> LeerAsync(
        IDbConnection conn, string storedProcedure, object? parametros = null)
    {
        using var reader = await conn.ExecuteReaderAsync(
            storedProcedure, parametros, commandType: CommandType.StoredProcedure);

        var ordId = OrdinalPorPrefijo(reader, "COD_");
        var ordTexto = OrdinalPorPrefijo(reader, "DES_");

        return Leer(reader, ordId, ordTexto);
    }

    /// <summary>Igual que <see cref="LeerAsync"/>, pero con el nombre de la columna clave
    /// explícito en vez de "la primera COD_*"/"la primera DES_*".
    ///
    /// POR QUÉ EXISTE: la convención de prefijo asume que la clave del combo se llama
    /// COD_algo. No siempre es así — LOTES_CONTROL, por ejemplo, se identifica por
    /// NUM_LOTE_CONTROL, que no empieza con "COD_". El lector genérico entonces tomaba la
    /// primera COD_* que encontraba —una columna cualquiera de la fila, no la clave— y el
    /// combo mandaba ese valor como si fuera el identificador. El síntoma no aparece al
    /// cargar el combo (la columna fantasma también tiene datos): aparece recién al grabar,
    /// como una violación de FOREIGN KEY contra un valor que "no existe" porque nunca fue
    /// la clave real.</summary>
    public static async Task<IEnumerable<ComboboxItem>> LeerAsync(
        IDbConnection conn, string storedProcedure, string columnaId, string columnaTexto, object? parametros = null)
    {
        using var reader = await conn.ExecuteReaderAsync(
            storedProcedure, parametros, commandType: CommandType.StoredProcedure);

        var ordId = OrdinalExacto(reader, columnaId);
        var ordTexto = OrdinalExacto(reader, columnaTexto);

        return Leer(reader, ordId, ordTexto);
    }

    private static List<ComboboxItem> Leer(IDataReader reader, int ordId, int ordTexto)
    {
        var lista = new List<ComboboxItem>();
        while (reader.Read())
        {
            lista.Add(new ComboboxItem
            {
                Id = Texto(reader, ordId),
                Texto = Texto(reader, ordTexto)
            });
        }
        return lista;
    }

    /// <summary>Busca la PRIMERA columna que empiece con el prefijo. Se toma la primera y no
    /// la última a propósito: el generador lista las columnas de la tabla base antes que las
    /// de los JOIN, así que la primera es la propia. No reconoce alias "Id"/"Texto" — un SP
    /// que solo tenga esos devuelve el combo vacío (a propósito, ver comentario de la clase).</summary>
    private static int OrdinalPorPrefijo(IDataReader reader, string prefijo)
    {
        for (var i = 0; i < reader.FieldCount; i++)
            if (reader.GetName(i).StartsWith(prefijo, StringComparison.OrdinalIgnoreCase))
                return i;

        return -1;
    }

    private static int OrdinalExacto(IDataReader reader, string nombre)
    {
        for (var i = 0; i < reader.FieldCount; i++)
            if (string.Equals(reader.GetName(i), nombre, StringComparison.OrdinalIgnoreCase))
                return i;
        return -1;
    }

    /// <summary>Convierte a texto sin asumir el tipo: hay combos cuyo código es numérico
    /// (COD_ESTADO, COD_TIPO_MOTIVO), y pedirle GetString a un smallint lanzaría.</summary>
    private static string Texto(IDataReader reader, int ordinal)
    {
        if (ordinal < 0 || reader.IsDBNull(ordinal)) return string.Empty;
        return Convert.ToString(reader.GetValue(ordinal)) ?? string.Empty;
    }
}
