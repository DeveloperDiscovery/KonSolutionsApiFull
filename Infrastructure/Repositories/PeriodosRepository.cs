using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PeriodosRepository : IPeriodosRepository
{
    private readonly IDbConnectionFactory _db;
    public PeriodosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Periodos>> ShowallAsync(string codTipoPeriodo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Periodos>(SP.SCHEMAEstandar.Periodos.Showall, new { COD_TIPO_PERIODO = codTipoPeriodo }, commandType: CommandType.StoredProcedure);
    }

    public async Task<Periodos?> ShowbyidAsync(string codTipoPeriodo, int numPeriodo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Periodos>(SP.SCHEMAEstandar.Periodos.Showbyid,
            new { COD_TIPO_PERIODO = codTipoPeriodo, NUM_PERIODO = numPeriodo }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codTipoPeriodo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComboboxItem>(SP.SCHEMAEstandar.Periodos.Showcombox, new { COD_TIPO_PERIODO = codTipoPeriodo }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Periodos>> SearchAsync(PeriodosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Periodos>(SP.SCHEMAEstandar.Periodos.Search,
            new { COD_TIPO_PERIODO = dto.CodTipoPeriodo, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PeriodosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        // @NUM_PERIODO lo exige el SP pero no lo usa: la tabla lo genera por IDENTITY
        // (el propio SP lo confirma devolviendo SCOPE_IDENTITY() al final).
        p.Add("NUM_PERIODO", 0);
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("COD_PERIODO", dto.COD_PERIODO);
        p.Add("DES_PERIODO", dto.DES_PERIODO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("NUM_ANO", dto.NUM_ANO);
        p.Add("NUM_SEMESTRE", dto.NUM_SEMESTRE);
        p.Add("NUM_QUARTER", dto.NUM_QUARTER);
        p.Add("NUM_MES", dto.NUM_MES);
        p.Add("NUM_SEMANA", dto.NUM_SEMANA);
        p.Add("NUM_DIA", dto.NUM_DIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            // El SP termina con "SELECT NUM_PERIODO = TRIM(CAST(SCOPE_IDENTITY() AS NVARCHAR))",
            // que no calza con las columnas de WriteResult (Estado/Mensaje/AffectedRows):
            // se lee como escalar y se arma el WriteResult a mano.
            var numPeriodoGenerado = await conn.QueryFirstOrDefaultAsync<string>(SP.SCHEMAEstandar.Periodos.Insert, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = numPeriodoGenerado ?? string.Empty };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.Periodos.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Periodos.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(PeriodosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("NUM_PERIODO", dto.NUM_PERIODO);
        p.Add("COD_PERIODO", dto.COD_PERIODO);
        p.Add("DES_PERIODO", dto.DES_PERIODO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("NUM_ANO", dto.NUM_ANO);
        p.Add("NUM_SEMESTRE", dto.NUM_SEMESTRE);
        p.Add("NUM_QUARTER", dto.NUM_QUARTER);
        p.Add("NUM_MES", dto.NUM_MES);
        p.Add("NUM_SEMANA", dto.NUM_SEMANA);
        p.Add("NUM_DIA", dto.NUM_DIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Periodos.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.Periodos.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Periodos.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codTipoPeriodo, int numPeriodo)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERIODO", codTipoPeriodo);
        p.Add("NUM_PERIODO", numPeriodo);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Periodos.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.Periodos.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Periodos.Delete, p)
            };
        }
    }

}
