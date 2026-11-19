using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UnidadesMedidaRepository : IUnidadesMedidaRepository
{
    private readonly IDbConnectionFactory _db;
    public UnidadesMedidaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UnidadesMedida>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesMedida>(SP.SCHEMAEstandar.UnidadesMedida.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UnidadesMedida?> ShowbyidAsync(string COD_UNIDAD_MEDIDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UnidadesMedida>(SP.SCHEMAEstandar.UnidadesMedida.Showbyid, new { COD_UNIDAD_MEDIDA = COD_UNIDAD_MEDIDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.UnidadesMedida.Showcombox);
    }

    public async Task<IEnumerable<UnidadesMedida>> SearchAsync(UnidadesMedidaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesMedida>(SP.SCHEMAEstandar.UnidadesMedida.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UnidadesMedidaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("DES_UNIDAD_MEDIDA", dto.DES_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SUNAT", dto.COD_UNIDAD_MEDIDA_SUNAT);
        p.Add("COD_SIMBOLO", dto.COD_SIMBOLO);
        p.Add("COD_TIPO_UNIDAD_MEDIDA", dto.COD_TIPO_UNIDAD_MEDIDA);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.UnidadesMedida.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedida.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedida.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UnidadesMedidaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("DES_UNIDAD_MEDIDA", dto.DES_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SUNAT", dto.COD_UNIDAD_MEDIDA_SUNAT);
        p.Add("COD_SIMBOLO", dto.COD_SIMBOLO);
        p.Add("COD_TIPO_UNIDAD_MEDIDA", dto.COD_TIPO_UNIDAD_MEDIDA);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.UnidadesMedida.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedida.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedida.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_UNIDAD_MEDIDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.UnidadesMedida.Delete, new { COD_UNIDAD_MEDIDA = COD_UNIDAD_MEDIDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedida.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedida.Delete, new { COD_UNIDAD_MEDIDA = COD_UNIDAD_MEDIDA })
        };
    }
}

    public async Task<WriteResult> UploadJsonAsync(string jsonArray)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.UnidadesMedida.UploadJson,
            new { JSON_ARRAY = jsonArray },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedida.UploadJson,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedida.UploadJson, new { JSON_ARRAY = jsonArray })
        };
    }
}

}