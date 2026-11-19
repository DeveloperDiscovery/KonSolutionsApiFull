using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Sistema;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SistemaHerramientasRepository : ISistemaHerramientasRepository
{
    private readonly IDbConnectionFactory _db;
    public SistemaHerramientasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<List<SpEncontrado>> GetSpsPorPrefijoAsync(string prefijo)
    {
        using var conn = _db.CreateConnection();
        var sql = """
            SELECT p.name AS Nombre, m.definition AS Definicion
            FROM   sys.procedures p
            JOIN   sys.sql_modules m ON m.object_id = p.object_id
            WHERE  p.name LIKE @prefijo + '%'
            ORDER BY p.name
            """;
        var rows = await conn.QueryAsync<SpEncontrado>(sql, new { prefijo });
        return rows.ToList();
    }

    /// <summary>Todas las tablas de usuario como "ESQUEMA.TABLA".
    ///
    /// Alimenta el combo de DES_OBJETO en el alta de opciones de menú: ese campo es la tabla
    /// contra la que el guardián resuelve el permiso, y hasta ahora no había forma de cargarlo
    /// desde la pantalla —nacía en NULL y la opción quedaba sin permisos posibles—.
    ///
    /// Sale de sys.tables y no de DEVELOPER.TABLAS_CODIGOS porque esa última sólo tiene las
    /// tablas configuradas para generar códigos, que son una fracción. Va por SQL directo, igual
    /// que el resto de este repositorio: es metadato del motor, no hay SP que lo envuelva.</summary>
    public async Task<List<string>> GetTablasAsync()
    {
        using var conn = _db.CreateConnection();
        var sql = """
            SELECT   s.name + '.' + t.name
            FROM     sys.tables t
            JOIN     sys.schemas s ON s.schema_id = t.schema_id
            WHERE    t.is_ms_shipped = 0
            ORDER BY s.name, t.name
            """;
        var rows = await conn.QueryAsync<string>(sql);
        return rows.ToList();
    }

    public async Task<List<ColumnaTabla>> GetEstructuraTablaAsync(string tabla)
    {
        // Acepta tanto "TABLA" como "SCHEMA.TABLA" (ej. "INGENIERIA.ESPECIFICACIONES_TECNICAS") —
        // si viene con schema, se separa para filtrar por TABLE_SCHEMA además de TABLE_NAME.
        string? schema = null;
        var nombreTabla = tabla;
        var puntoIdx = tabla.IndexOf('.');
        if (puntoIdx > 0)
        {
            schema = tabla[..puntoIdx];
            nombreTabla = tabla[(puntoIdx + 1)..];
        }

        using var conn = _db.CreateConnection();
        var sql = """
            SELECT TABLE_SCHEMA                                    AS SchemaName,
                   COLUMN_NAME                                    AS NombreColumna,
                   DATA_TYPE                                      AS TipoDato,
                   CHARACTER_MAXIMUM_LENGTH                       AS MaxLength,
                   NUMERIC_PRECISION                              AS Precision,
                   NUMERIC_SCALE                                  AS Scale,
                   CASE IS_NULLABLE WHEN 'YES' THEN 1 ELSE 0 END  AS EsNullable,
                   COLUMN_DEFAULT                                 AS ValorPorDefecto,
                   ORDINAL_POSITION                               AS OrdinalPosition
            FROM   INFORMATION_SCHEMA.COLUMNS
            WHERE  TABLE_NAME = @nombreTabla
              AND  (@schema IS NULL OR TABLE_SCHEMA = @schema)
            ORDER BY ORDINAL_POSITION
            """;
        var rows = await conn.QueryAsync<ColumnaTabla>(sql, new { nombreTabla, schema });
        return rows.ToList();
    }
}
