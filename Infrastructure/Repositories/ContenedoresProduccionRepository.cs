using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ContenedoresProduccionRepository : IContenedoresProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public ContenedoresProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ContenedoresProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContenedoresProduccion>(SP.SCHEMAProduccion.ContenedoresProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ContenedoresProduccion?> ShowbyidAsync(int NUM_CONTENEDOR_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ContenedoresProduccion>(SP.SCHEMAProduccion.ContenedoresProduccion.Showbyid, new { NUM_CONTENEDOR_PRODUCCION = NUM_CONTENEDOR_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.ContenedoresProduccion.Showcombox);
    }

    public async Task<IEnumerable<ContenedoresProduccion>> SearchAsync(ContenedoresProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContenedoresProduccion>(SP.SCHEMAProduccion.ContenedoresProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ContenedoresProduccionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTENEDOR_PRODUCCION", dto.NUM_CONTENEDOR_PRODUCCION);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ContenedoresProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ContenedoresProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ContenedoresProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ContenedoresProduccionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTENEDOR_PRODUCCION", dto.NUM_CONTENEDOR_PRODUCCION);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ContenedoresProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ContenedoresProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ContenedoresProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CONTENEDOR_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ContenedoresProduccion.Delete, new { NUM_CONTENEDOR_PRODUCCION = NUM_CONTENEDOR_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ContenedoresProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ContenedoresProduccion.Delete, new { NUM_CONTENEDOR_PRODUCCION = NUM_CONTENEDOR_PRODUCCION })
        };
    }
}

}