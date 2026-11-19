using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesMovimientoRepository : IClasesMovimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesMovimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesMovimiento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesMovimiento>(SP.SCHEMALogistica.ClasesMovimiento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesMovimiento?> ShowbyidAsync(string COD_CLASE_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesMovimiento>(SP.SCHEMALogistica.ClasesMovimiento.Showbyid, new { COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los SP _SHOWCOMBOX devuelven COD_*/DES_*, no Id/Texto: la traducción por
    /// convención la hace ComboboxReader, igual que en el resto de los repositorios.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.ClasesMovimiento.Showcombox);
    }

    public async Task<IEnumerable<ClasesMovimiento>> SearchAsync(ClasesMovimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesMovimiento>(SP.SCHEMALogistica.ClasesMovimiento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesMovimientoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("DES_CLASE_MOVIMIENTO", dto.DES_CLASE_MOVIMIENTO);
        p.Add("FLG_ALMACENES", dto.FLG_ALMACENES);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ClasesMovimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ClasesMovimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ClasesMovimiento.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesMovimientoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("DES_CLASE_MOVIMIENTO", dto.DES_CLASE_MOVIMIENTO);
        p.Add("FLG_ALMACENES", dto.FLG_ALMACENES);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ClasesMovimiento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ClasesMovimiento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ClasesMovimiento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.ClasesMovimiento.Delete, new { COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ClasesMovimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ClasesMovimiento.Delete, new { COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO })
        };
    }
}

}