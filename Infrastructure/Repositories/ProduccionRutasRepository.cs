using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProduccionRutasRepository : IProduccionRutasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProduccionRutasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProduccionRutas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProduccionRutas>(SP.SCHEMAIngenieria.ProduccionRutas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProduccionRutas?> ShowbyidAsync(int COD_RUTA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProduccionRutas>(SP.SCHEMAIngenieria.ProduccionRutas.Showbyid, new { COD_RUTA_PRODUCCION = COD_RUTA_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.ProduccionRutas.Showcombox);
    }

    public async Task<IEnumerable<ProduccionRutas>> SearchAsync(ProduccionRutasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProduccionRutas>(SP.SCHEMAIngenieria.ProduccionRutas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProduccionRutasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("DES_RUTA_PRODUCCION", dto.DES_RUTA_PRODUCCION);
        p.Add("COD_TIPO_RUTA_PRODUCCION", dto.COD_TIPO_RUTA_PRODUCCION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ProduccionRutas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ProduccionRutas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ProduccionRutas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_RUTA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ProduccionRutas.Delete, new { COD_RUTA_PRODUCCION = COD_RUTA_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ProduccionRutas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ProduccionRutas.Delete, new { COD_RUTA_PRODUCCION = COD_RUTA_PRODUCCION })
        };
    }
}

}