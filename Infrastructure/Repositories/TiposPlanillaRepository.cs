using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPlanillaRepository : ITiposPlanillaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPlanillaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPlanilla>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPlanilla>(SP.SCHEMANomina.TiposPlanilla.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPlanilla?> ShowbyidAsync(string COD_TIPO_PLANILLA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPlanilla>(SP.SCHEMANomina.TiposPlanilla.Showbyid, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.TiposPlanilla.Showcombox);
    }

    public async Task<IEnumerable<TiposPlanilla>> SearchAsync(TiposPlanillaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPlanilla>(SP.SCHEMANomina.TiposPlanilla.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPlanillaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("DES_TIPO_PLANILLA", dto.DES_TIPO_PLANILLA);
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanilla.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanilla.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanilla.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposPlanillaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("DES_TIPO_PLANILLA", dto.DES_TIPO_PLANILLA);
        p.Add("COD_TIPO_PERSONAL", dto.COD_TIPO_PERSONAL);
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanilla.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanilla.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanilla.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanilla.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanilla.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanilla.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA })
        };
    }
}

}