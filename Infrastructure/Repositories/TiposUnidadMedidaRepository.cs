using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposUnidadMedidaRepository : ITiposUnidadMedidaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposUnidadMedidaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposUnidadMedida>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUnidadMedida>(SP.SCHEMAEstandar.TiposUnidadMedida.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposUnidadMedida?> ShowbyidAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposUnidadMedida>(SP.SCHEMAEstandar.TiposUnidadMedida.Showbyid, new { COD_TIPO_UNIDAD_MEDIDA = COD_TIPO_UNIDAD_MEDIDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposUnidadMedida.Showcombox);
    }

    public async Task<IEnumerable<TiposUnidadMedida>> SearchAsync(TiposUnidadMedidaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUnidadMedida>(SP.SCHEMAEstandar.TiposUnidadMedida.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposUnidadMedidaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UNIDAD_MEDIDA", dto.COD_TIPO_UNIDAD_MEDIDA);
        p.Add("DES_TIPO_UNIDAD_MEDIDA", dto.DES_TIPO_UNIDAD_MEDIDA);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposUnidadMedida.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposUnidadMedida.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposUnidadMedida.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposUnidadMedidaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UNIDAD_MEDIDA", dto.COD_TIPO_UNIDAD_MEDIDA);
        p.Add("DES_TIPO_UNIDAD_MEDIDA", dto.DES_TIPO_UNIDAD_MEDIDA);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposUnidadMedida.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposUnidadMedida.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposUnidadMedida.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposUnidadMedida.Delete, new { COD_TIPO_UNIDAD_MEDIDA = COD_TIPO_UNIDAD_MEDIDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposUnidadMedida.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposUnidadMedida.Delete, new { COD_TIPO_UNIDAD_MEDIDA = COD_TIPO_UNIDAD_MEDIDA })
        };
    }
}

    public async Task<WriteResult> UploadJsonAsync(string jsonArray)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.TiposUnidadMedida.UploadJson,
            new { JSON_ARRAY = jsonArray },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposUnidadMedida.UploadJson,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposUnidadMedida.UploadJson, new { JSON_ARRAY = jsonArray })
        };
    }
}

}