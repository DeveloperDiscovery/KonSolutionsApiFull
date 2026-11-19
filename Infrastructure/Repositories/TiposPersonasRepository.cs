using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPersonasRepository : ITiposPersonasRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPersonasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPersonas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPersonas>(SP.SCHEMAEstandar.TiposPersonas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPersonas?> ShowbyidAsync(byte COD_TIPO_PERSONA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPersonas>(SP.SCHEMAEstandar.TiposPersonas.Showbyid, new { COD_TIPO_PERSONA }, commandType: CommandType.StoredProcedure);
    }

    // PROC_ESTANDAR_TIPOS_PERSONAS_SHOWCOMBOX no aliasea columnas a Id/Texto (selecciona
    // COD_TIPO_PERSONA/DES_TIPO_PERSONA tal cual) — se mapea a mano.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposPersonas.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(TiposPersonasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERSONA", dto.COD_TIPO_PERSONA);
        p.Add("DES_TIPO_PERSONA", dto.DES_TIPO_PERSONA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposPersonas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposPersonas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposPersonas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(byte COD_TIPO_PERSONA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposPersonas.Delete, new { COD_TIPO_PERSONA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposPersonas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposPersonas.Delete, new { COD_TIPO_PERSONA })
        };
    }
}
}
