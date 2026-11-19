using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCategoriaComisionRepository : ITiposCategoriaComisionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCategoriaComisionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCategoriaComision>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCategoriaComision>(SP.SCHEMANetworking.TiposCategoriaComision.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCategoriaComision?> ShowbyidAsync(string COD_TIPO_CATEGORIA_COMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCategoriaComision>(SP.SCHEMANetworking.TiposCategoriaComision.Showbyid, new { COD_TIPO_CATEGORIA_COMISION = COD_TIPO_CATEGORIA_COMISION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.TiposCategoriaComision.Showcombox);
    }

    public async Task<IEnumerable<TiposCategoriaComision>> SearchAsync(TiposCategoriaComisionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCategoriaComision>(SP.SCHEMANetworking.TiposCategoriaComision.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCategoriaComisionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CATEGORIA_COMISION", dto.COD_TIPO_CATEGORIA_COMISION);
        p.Add("DES_TIPO_COMISION", dto.DES_TIPO_COMISION);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.TiposCategoriaComision.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.TiposCategoriaComision.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.TiposCategoriaComision.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCategoriaComisionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CATEGORIA_COMISION", dto.COD_TIPO_CATEGORIA_COMISION);
        p.Add("DES_TIPO_COMISION", dto.DES_TIPO_COMISION);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.TiposCategoriaComision.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.TiposCategoriaComision.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.TiposCategoriaComision.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CATEGORIA_COMISION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.TiposCategoriaComision.Delete, new { COD_TIPO_CATEGORIA_COMISION = COD_TIPO_CATEGORIA_COMISION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.TiposCategoriaComision.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.TiposCategoriaComision.Delete, new { COD_TIPO_CATEGORIA_COMISION = COD_TIPO_CATEGORIA_COMISION })
        };
    }
}

}