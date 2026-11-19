using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPlanillaPeriodosRepository : ITiposPlanillaPeriodosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPlanillaPeriodosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPlanillaPeriodos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPlanillaPeriodos>(SP.SCHEMANomina.TiposPlanillaPeriodos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPlanillaPeriodos?> ShowbyidAsync(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPlanillaPeriodos>(SP.SCHEMANomina.TiposPlanillaPeriodos.Showbyid, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_TIPO_PERIODO = COD_TIPO_PERIODO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.TiposPlanillaPeriodos.Showcombox);
    }

    public async Task<IEnumerable<TiposPlanillaPeriodos>> SearchAsync(TiposPlanillaPeriodosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPlanillaPeriodos>(SP.SCHEMANomina.TiposPlanillaPeriodos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPlanillaPeriodosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PLANILLA", dto.COD_TIPO_PLANILLA);
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanillaPeriodos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanillaPeriodos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanillaPeriodos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PLANILLA, string COD_TIPO_PERIODO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposPlanillaPeriodos.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_TIPO_PERIODO = COD_TIPO_PERIODO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposPlanillaPeriodos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposPlanillaPeriodos.Delete, new { COD_TIPO_PLANILLA = COD_TIPO_PLANILLA, COD_TIPO_PERIODO = COD_TIPO_PERIODO })
        };
    }
}

}