using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstadosEstandarRepository : IEstadosEstandarRepository
{
    private readonly IDbConnectionFactory _db;
    public EstadosEstandarRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EstadosEstandar>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstadosEstandar>(SP.SCHEMAEstandar.EstadosEstandar.Showall, new { COD_INDICE = (int?)null }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EstadosEstandar?> ShowbyidAsync(int COD_INDICE, int COD_ESTADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EstadosEstandar>(SP.SCHEMAEstandar.EstadosEstandar.Showbyid, new { COD_INDICE = COD_INDICE, COD_ESTADO = COD_ESTADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EstadosEstandar.Showcombox);
    }

    public async Task<IEnumerable<EstadosEstandar>> SearchAsync(EstadosEstandarSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstadosEstandar>(SP.SCHEMAEstandar.EstadosEstandar.Search, new { COD_INDICE = (int?)null, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EstadosEstandarInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INDICE", dto.COD_INDICE);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("DES_ESTADO", dto.DES_ESTADO);
        p.Add("COD_ESTADO_ALIAS", dto.COD_ESTADO_ALIAS);
        p.Add("DES_BACKCOLOR", dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR", dto.DES_FORECOLOR);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstadosEstandar.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EstadosEstandar.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstadosEstandar.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EstadosEstandarUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INDICE", dto.COD_INDICE);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("DES_ESTADO", dto.DES_ESTADO);
        p.Add("COD_ESTADO_ALIAS", dto.COD_ESTADO_ALIAS);
        p.Add("DES_BACKCOLOR", dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR", dto.DES_FORECOLOR);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstadosEstandar.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EstadosEstandar.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstadosEstandar.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_INDICE, int COD_ESTADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EstadosEstandar.Delete, new { COD_INDICE = COD_INDICE, COD_ESTADO = COD_ESTADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EstadosEstandar.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EstadosEstandar.Delete, new { COD_INDICE = COD_INDICE, COD_ESTADO = COD_ESTADO })
        };
    }
}

}