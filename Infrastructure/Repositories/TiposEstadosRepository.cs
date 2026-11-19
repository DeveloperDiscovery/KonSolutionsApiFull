using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposEstadosRepository : ITiposEstadosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposEstadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposEstados>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEstados>(SP.SCHEMAEstandar.TiposEstados.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposEstados?> ShowbyidAsync(int COD_TIPO_ESTADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposEstados>(SP.SCHEMAEstandar.TiposEstados.Showbyid, new { COD_TIPO_ESTADO = COD_TIPO_ESTADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposEstados.Showcombox);
    }

    public async Task<IEnumerable<TiposEstados>> SearchAsync(TiposEstadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEstados>(SP.SCHEMAEstandar.TiposEstados.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposEstadosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("DES_TIPO_ESTADO", dto.DES_TIPO_ESTADO);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposEstados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposEstados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposEstados.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposEstadosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("DES_TIPO_ESTADO", dto.DES_TIPO_ESTADO);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposEstados.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposEstados.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposEstados.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_ESTADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposEstados.Delete, new { COD_TIPO_ESTADO = COD_TIPO_ESTADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposEstados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposEstados.Delete, new { COD_TIPO_ESTADO = COD_TIPO_ESTADO })
        };
    }
}

}