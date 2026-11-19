using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Administracion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCargoAdministrativoRepository : ITiposCargoAdministrativoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCargoAdministrativoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCargoAdministrativo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCargoAdministrativo>(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCargoAdministrativo?> ShowbyidAsync(int COD_TIPO_CARGO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCargoAdministrativo>(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Showbyid, new { COD_TIPO_CARGO = COD_TIPO_CARGO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAAdministracion.TiposCargoAdministrativo.Showcombox);
    }

    public async Task<IEnumerable<TiposCargoAdministrativo>> SearchAsync(TiposCargoAdministrativoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCargoAdministrativo>(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCargoAdministrativoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CARGO", dto.COD_TIPO_CARGO);
        p.Add("DES_TIPO_CARGO", dto.DES_TIPO_CARGO);
        p.Add("FLG_GERENCIA", dto.FLG_GERENCIA ?? false);
        p.Add("FLG_SUB_GERENCIA", dto.FLG_SUB_GERENCIA ?? false);
        p.Add("FLG_JEFATURA", dto.FLG_JEFATURA ?? false);
        p.Add("FLG_SUB_JEFATURA", dto.FLG_SUB_JEFATURA ?? false);
        p.Add("FLG_SUPERVISOR", dto.FLG_SUPERVISOR ?? false);
        p.Add("FLG_ANALISTA", dto.FLG_ANALISTA ?? false);
        p.Add("FLG_ASISTENTE", dto.FLG_ASISTENTE ?? false);
        p.Add("FLG_OPERARIO", dto.FLG_OPERARIO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAdministracion.TiposCargoAdministrativo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCargoAdministrativoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CARGO", dto.COD_TIPO_CARGO);
        p.Add("DES_TIPO_CARGO", dto.DES_TIPO_CARGO);
        p.Add("FLG_GERENCIA", dto.FLG_GERENCIA ?? false);
        p.Add("FLG_SUB_GERENCIA", dto.FLG_SUB_GERENCIA ?? false);
        p.Add("FLG_JEFATURA", dto.FLG_JEFATURA ?? false);
        p.Add("FLG_SUB_JEFATURA", dto.FLG_SUB_JEFATURA ?? false);
        p.Add("FLG_SUPERVISOR", dto.FLG_SUPERVISOR ?? false);
        p.Add("FLG_ANALISTA", dto.FLG_ANALISTA ?? false);
        p.Add("FLG_ASISTENTE", dto.FLG_ASISTENTE ?? false);
        p.Add("FLG_OPERARIO", dto.FLG_OPERARIO ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAdministracion.TiposCargoAdministrativo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_CARGO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Delete, new { COD_TIPO_CARGO = COD_TIPO_CARGO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAdministracion.TiposCargoAdministrativo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAdministracion.TiposCargoAdministrativo.Delete, new { COD_TIPO_CARGO = COD_TIPO_CARGO })
        };
    }
}

}