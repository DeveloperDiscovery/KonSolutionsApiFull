using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ContenedoresAlmacenajeCasillerosRepository : IContenedoresAlmacenajeCasillerosRepository
{
    private readonly IDbConnectionFactory _db;
    public ContenedoresAlmacenajeCasillerosRepository(IDbConnectionFactory db) => _db = db;

    // Todos los SP de lectura piden @COD_CONTENEDOR: sin padre no hay listado posible.
    public async Task<IEnumerable<ContenedoresAlmacenajeCasilleros>> ShowallAsync(string COD_CONTENEDOR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContenedoresAlmacenajeCasilleros>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Showall,
            new { COD_CONTENEDOR = COD_CONTENEDOR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<ContenedoresAlmacenajeCasilleros?> ShowbyidAsync(string COD_CONTENEDOR, string COD_CASILLERO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ContenedoresAlmacenajeCasilleros>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Showbyid,
            new { COD_CONTENEDOR = COD_CONTENEDOR, COD_CASILLERO = COD_CASILLERO },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los SP _SHOWCOMBOX devuelven COD_*/DES_*, no Id/Texto: la traducción por
    /// convención la hace ComboboxReader, igual que en el resto de los repositorios.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_CONTENEDOR)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Showcombox, new { COD_CONTENEDOR = COD_CONTENEDOR });
    }

    public async Task<IEnumerable<ContenedoresAlmacenajeCasilleros>> SearchAsync(ContenedoresAlmacenajeCasillerosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContenedoresAlmacenajeCasilleros>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Search,
            new { COD_CONTENEDOR = dto.COD_CONTENEDOR, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Deja COD_CONTENEDOR y SEARCH sin mandar: el SP los trata como "sin filtro" por
    /// su valor por defecto ('') y busca únicamente por la ubicación exacta.</summary>
    public async Task<ContenedoresAlmacenajeCasilleros?> BuscarPorUbicacionAsync(string codAlmacen, string codUbicacionAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ContenedoresAlmacenajeCasilleros>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Search,
            new { COD_ALMACEN = codAlmacen, COD_UBICACION_ALMACEN = codUbicacionAlmacen },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ContenedoresAlmacenajeCasillerosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CONTENEDOR", dto.COD_CONTENEDOR);
        // Vacío al crear: el SP saca el siguiente correlativo dentro del contenedor.
        p.Add("COD_CASILLERO", dto.COD_CASILLERO ?? string.Empty);
        p.Add("DES_CASILLERO", dto.DES_CASILLERO);
        p.Add("NUM_FILA", dto.NUM_FILA);
        p.Add("NUM_COLUMNA", dto.NUM_COLUMNA);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CONTENEDOR, string COD_CASILLERO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Delete,
            new { COD_CONTENEDOR = COD_CONTENEDOR, COD_CASILLERO = COD_CASILLERO },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.Delete, new { COD_CONTENEDOR = COD_CONTENEDOR, COD_CASILLERO = COD_CASILLERO })
        };
    }
}

    /// <summary>Regenera la matriz completa de casilleros. Es destructivo por diseño: el SP
    /// borra los casilleros del contenedor y las ubicaciones que él mismo había creado.
    /// Timeout amplio porque crea FILAS × COLUMNAS ubicaciones, una por una.</summary>
    public async Task<WriteResult> GenerarAsync(ContenedoresAlmacenajeCasillerosGenerarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CONTENEDOR", dto.COD_CONTENEDOR);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("NUM_FILAS", dto.NUM_FILAS);
        p.Add("NUM_COLUMNAS", dto.NUM_COLUMNAS);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.CreateTall, p,
            commandType: CommandType.StoredProcedure, commandTimeout: 180);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = dto.NUM_FILAS * dto.NUM_COLUMNAS };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.CreateTall,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ContenedoresAlmacenajeCasilleros.CreateTall, p)
        };
    }
}
}
