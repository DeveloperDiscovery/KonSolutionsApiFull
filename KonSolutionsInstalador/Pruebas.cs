using Microsoft.Data.SqlClient;

namespace KonSolutionsInstalador;

public static class Pruebas
{
    /// <summary>Conecta a SQL y comprueba que la base es la del sistema (tiene la configuracion general y los usuarios).</summary>
    public static async Task<(bool Ok, string Mensaje)> SqlAsync(string conexion)
    {
        try
        {
            await using var c = new SqlConnection(conexion);
            await c.OpenAsync();
            await using var cmd = c.CreateCommand();
            cmd.CommandText = @"SELECT (SELECT COUNT(*) FROM sys.tables WHERE name = N'CONFIGURACION_GENERAL'), (SELECT COUNT(*) FROM sys.tables WHERE name = N'USUARIOS' AND SCHEMA_NAME(schema_id) = N'SEGURIDAD'),
                                       (SELECT COUNT(*) FROM sys.procedures WHERE name = N'PROC_ESTANDAR_CONFIGURACION_GENERAL_INSERT')";
            await using var r = await cmd.ExecuteReaderAsync();
            await r.ReadAsync();
            if (r.GetInt32(0) == 0 || r.GetInt32(1) == 0) return (false, $"Conecta a {c.DataSource} / {c.Database}, pero esa base no parece la del sistema (faltan ESTANDAR.CONFIGURACION_GENERAL o SEGURIDAD.USUARIOS): revisa la base de la cadena.");
            if (r.GetInt32(2) == 0) return (false, "Conecta, pero falta PROC_ESTANDAR_CONFIGURACION_GENERAL_INSERT: corre los scripts SQL del sistema.");
            return (true, $"Conecta a {c.DataSource} / {c.Database} y es la base del sistema.");
        }
        catch (Exception ex) { return (false, "No conecta a SQL Server: " + ex.Message); }
    }
}
