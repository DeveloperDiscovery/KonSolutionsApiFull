using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposRegistroDatosRepository : ITiposRegistroDatosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposRegistroDatosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposRegistroDatos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposRegistroDatos>(SP.SCHEMAIngenieria.TiposRegistroDatos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposRegistroDatos?> ShowbyidAsync(string COD_TIPO_REGISTRO_DATOS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposRegistroDatos>(SP.SCHEMAIngenieria.TiposRegistroDatos.Showbyid, new { COD_TIPO_REGISTRO_DATOS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.TiposRegistroDatos.Showcombox);
    }

    public async Task<IEnumerable<TiposRegistroDatos>> SearchAsync(TiposRegistroDatosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposRegistroDatos>(SP.SCHEMAIngenieria.TiposRegistroDatos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposRegistroDatosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_REGISTRO_DATOS", dto.COD_TIPO_REGISTRO_DATOS);
        p.Add("DES_TIPO_REGISTRO_DATOS", dto.DES_TIPO_REGISTRO_DATOS);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposRegistroDatos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposRegistroDatos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposRegistroDatos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_REGISTRO_DATOS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.TiposRegistroDatos.Delete, new { COD_TIPO_REGISTRO_DATOS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.TiposRegistroDatos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.TiposRegistroDatos.Delete, new { COD_TIPO_REGISTRO_DATOS })
        };
    }
}
}
