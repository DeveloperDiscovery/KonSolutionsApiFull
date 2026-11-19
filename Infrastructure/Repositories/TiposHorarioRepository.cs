using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposHorarioRepository : ITiposHorarioRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposHorarioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposHorario>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposHorario>(SP.SCHEMANomina.TiposHorario.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposHorario?> ShowbyidAsync(string COD_TIPO_HORARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposHorario>(SP.SCHEMANomina.TiposHorario.Showbyid, new { COD_TIPO_HORARIO = COD_TIPO_HORARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.TiposHorario.Showcombox);
    }

    public async Task<IEnumerable<TiposHorario>> SearchAsync(TiposHorarioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposHorario>(SP.SCHEMANomina.TiposHorario.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposHorarioInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_HORARIO", dto.COD_TIPO_HORARIO);
        p.Add("DES_TIPO_HORARIO", dto.DES_TIPO_HORARIO);
        p.Add("FLG_SEMANAL", dto.FLG_SEMANAL ?? false);
        p.Add("FLG_QUINCENAL", dto.FLG_QUINCENAL ?? false);
        p.Add("FLG_MENSUAL", dto.FLG_MENSUAL ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposHorario.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposHorario.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposHorario.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposHorarioUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_HORARIO", dto.COD_TIPO_HORARIO);
        p.Add("DES_TIPO_HORARIO", dto.DES_TIPO_HORARIO);
        p.Add("FLG_SEMANAL", dto.FLG_SEMANAL ?? false);
        p.Add("FLG_QUINCENAL", dto.FLG_QUINCENAL ?? false);
        p.Add("FLG_MENSUAL", dto.FLG_MENSUAL ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposHorario.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposHorario.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposHorario.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_HORARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposHorario.Delete, new { COD_TIPO_HORARIO = COD_TIPO_HORARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposHorario.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposHorario.Delete, new { COD_TIPO_HORARIO = COD_TIPO_HORARIO })
        };
    }
}

}