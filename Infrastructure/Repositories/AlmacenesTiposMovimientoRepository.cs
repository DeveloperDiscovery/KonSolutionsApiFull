using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AlmacenesTiposMovimientoRepository : IAlmacenesTiposMovimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public AlmacenesTiposMovimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AlmacenesTiposMovimiento>> ShowallAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AlmacenesTiposMovimiento>(
            SP.SCHEMALogistica.AlmacenesTiposMovimiento.Showall,
            new { COD_ALMACEN, COD_CLASE_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<AlmacenesTiposMovimiento?> ShowbyidAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AlmacenesTiposMovimiento>(
            SP.SCHEMALogistica.AlmacenesTiposMovimiento.Showbyid,
            new { COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los SP _SHOWCOMBOX devuelven COD_*/DES_*, no Id/Texto: la traducción por
    /// convención la hace ComboboxReader, igual que en el resto de los repositorios.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.AlmacenesTiposMovimiento.Showcombox, new { COD_ALMACEN, COD_CLASE_MOVIMIENTO });
    }

    /// <summary>OJO: el SP _SEARCH de esta tabla tiene el filtro mal armado —el WHERE combina
    /// las claves con OR sobre el texto buscado—, así que devuelve filas de otros almacenes.
    /// Se deja disponible por completitud, pero la pantalla de asignación NO lo usa.</summary>
    public async Task<IEnumerable<AlmacenesTiposMovimiento>> SearchAsync(AlmacenesTiposMovimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AlmacenesTiposMovimiento>(
            SP.SCHEMALogistica.AlmacenesTiposMovimiento.Search,
            new { COD_ALMACEN = dto.COD_ALMACEN, COD_CLASE_MOVIMIENTO = dto.COD_CLASE_MOVIMIENTO, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.AlmacenesTiposMovimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.AlmacenesTiposMovimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.AlmacenesTiposMovimiento.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.AlmacenesTiposMovimiento.Delete,
            new { COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.AlmacenesTiposMovimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.AlmacenesTiposMovimiento.Delete, new { COD_ALMACEN, COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO })
        };
    }
}
}
