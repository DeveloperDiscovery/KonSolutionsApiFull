using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCuentaContableRepository : ITiposCuentaContableRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCuentaContableRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCuentaContable>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCuentaContable>(SP.SCHEMAContabilidad.TiposCuentaContable.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCuentaContable?> ShowbyidAsync(string COD_TIPO_CUENTA_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCuentaContable>(SP.SCHEMAContabilidad.TiposCuentaContable.Showbyid, new { COD_TIPO_CUENTA_CONTABLE = COD_TIPO_CUENTA_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.TiposCuentaContable.Showcombox);
    }

    public async Task<IEnumerable<TiposCuentaContable>> SearchAsync(TiposCuentaContableSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCuentaContable>(SP.SCHEMAContabilidad.TiposCuentaContable.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCuentaContableInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CUENTA_CONTABLE", dto.COD_TIPO_CUENTA_CONTABLE);
        p.Add("DES_TIPO_CUENTA_CONTABLE", dto.DES_TIPO_CUENTA_CONTABLE);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("FLG_AFECTA_BALANCE", dto.FLG_AFECTA_BALANCE);
        p.Add("FLG_AFECTA_RESULTADO", dto.FLG_AFECTA_RESULTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.TiposCuentaContable.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.TiposCuentaContable.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.TiposCuentaContable.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCuentaContableUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CUENTA_CONTABLE", dto.COD_TIPO_CUENTA_CONTABLE);
        p.Add("DES_TIPO_CUENTA_CONTABLE", dto.DES_TIPO_CUENTA_CONTABLE);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("FLG_AFECTA_BALANCE", dto.FLG_AFECTA_BALANCE);
        p.Add("FLG_AFECTA_RESULTADO", dto.FLG_AFECTA_RESULTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.TiposCuentaContable.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.TiposCuentaContable.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.TiposCuentaContable.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CUENTA_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.TiposCuentaContable.Delete, new { COD_TIPO_CUENTA_CONTABLE = COD_TIPO_CUENTA_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.TiposCuentaContable.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.TiposCuentaContable.Delete, new { COD_TIPO_CUENTA_CONTABLE = COD_TIPO_CUENTA_CONTABLE })
        };
    }
}

}