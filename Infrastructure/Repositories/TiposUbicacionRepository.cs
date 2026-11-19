using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposUbicacionRepository : ITiposUbicacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposUbicacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposUbicacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUbicacion>(
            SP.SCHEMALogistica.TiposUbicacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposUbicacion?> ShowbyidAsync(string COD_TIPO_UBICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposUbicacion>(
            SP.SCHEMALogistica.TiposUbicacion.Showbyid,
            new { COD_TIPO_UBICACION = COD_TIPO_UBICACION }, commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá, en el repositorio.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposUbicacion.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(TiposUbicacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UBICACION", dto.COD_TIPO_UBICACION);
        p.Add("DES_TIPO_UBICACION", dto.DES_TIPO_UBICACION);
        // Explícito como binario: sin el DbType, Dapper manda un byte[] nulo como string
        // y el SP falla al asignarlo a varbinary.
        p.Add("IMG_IMAGEN", dto.IMG_IMAGEN, DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.TiposUbicacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposUbicacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposUbicacion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_UBICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.TiposUbicacion.Delete,
            new { COD_TIPO_UBICACION = COD_TIPO_UBICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposUbicacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposUbicacion.Delete, new { COD_TIPO_UBICACION = COD_TIPO_UBICACION })
        };
    }
}
}
