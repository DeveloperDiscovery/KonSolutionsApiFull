using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UnidadesMedidaEquivalenciasRepository : IUnidadesMedidaEquivalenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public UnidadesMedidaEquivalenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UnidadesMedidaEquivalencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesMedidaEquivalencias>(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UnidadesMedidaEquivalencias?> ShowbyidAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UnidadesMedidaEquivalencias>(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Showbyid, new { COD_UNIDAD_MEDIDA = COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE = COD_UNIDAD_MEDIDA_EQUIVALENTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Showcombox);
    }

    public async Task<IEnumerable<UnidadesMedidaEquivalencias>> SearchAsync(UnidadesMedidaEquivalenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesMedidaEquivalencias>(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<UnidadesMedidaEquivalencias>> GetByTipoAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesMedidaEquivalencias>(
            SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.ShowByTipo,
            new { COD_TIPO_UNIDAD_MEDIDA },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> UpsertAsync(UnidadesMedidaEquivalenciasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA",            dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_EQUIVALENTE", dto.COD_UNIDAD_MEDIDA_EQUIVALENTE);
        p.Add("VAL_EQUIVALENTE",               dto.VAL_EQUIVALENTE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Upsert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Upsert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Upsert, p)
        };
    }
}

    public async Task<WriteResult> InsertAsync(UnidadesMedidaEquivalenciasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_EQUIVALENTE", dto.COD_UNIDAD_MEDIDA_EQUIVALENTE);
        p.Add("VAL_EQUIVALENTE", dto.VAL_EQUIVALENTE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UnidadesMedidaEquivalenciasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_EQUIVALENTE", dto.COD_UNIDAD_MEDIDA_EQUIVALENTE);
        p.Add("VAL_EQUIVALENTE", dto.VAL_EQUIVALENTE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_UNIDAD_MEDIDA, string COD_UNIDAD_MEDIDA_EQUIVALENTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Delete, new { COD_UNIDAD_MEDIDA = COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE = COD_UNIDAD_MEDIDA_EQUIVALENTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.Delete, new { COD_UNIDAD_MEDIDA = COD_UNIDAD_MEDIDA, COD_UNIDAD_MEDIDA_EQUIVALENTE = COD_UNIDAD_MEDIDA_EQUIVALENTE })
        };
    }
}

    public async Task<WriteResult> CargarEstandarAsync(string COD_TIPO_UNIDAD_MEDIDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.CargarEstandar,
            new { COD_TIPO_UNIDAD_MEDIDA },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 0 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.CargarEstandar,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.UnidadesMedidaEquivalencias.CargarEstandar, new { COD_TIPO_UNIDAD_MEDIDA })
        };
    }
}

}