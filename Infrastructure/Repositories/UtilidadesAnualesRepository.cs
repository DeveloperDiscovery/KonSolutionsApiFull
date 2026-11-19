using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UtilidadesAnualesRepository : IUtilidadesAnualesRepository
{
    private readonly IDbConnectionFactory _db;
    public UtilidadesAnualesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UtilidadesAnuales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UtilidadesAnuales>(SP.SCHEMANomina.UtilidadesAnuales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UtilidadesAnuales?> ShowbyidAsync(int NUM_ANO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UtilidadesAnuales>(SP.SCHEMANomina.UtilidadesAnuales.Showbyid, new { NUM_ANO = NUM_ANO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.UtilidadesAnuales.Showcombox);
    }

    public async Task<IEnumerable<UtilidadesAnuales>> SearchAsync(UtilidadesAnualesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UtilidadesAnuales>(SP.SCHEMANomina.UtilidadesAnuales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UtilidadesAnualesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ANO", dto.NUM_ANO);
        p.Add("VAL_UTILIDAD", dto.VAL_UTILIDAD);
        p.Add("TAS_CALCULO_ASISTENCIA", dto.TAS_CALCULO_ASISTENCIA);
        p.Add("TAS_CALCULO_REMUNERACION", dto.TAS_CALCULO_REMUNERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.UtilidadesAnuales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.UtilidadesAnuales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.UtilidadesAnuales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UtilidadesAnualesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ANO", dto.NUM_ANO);
        p.Add("VAL_UTILIDAD", dto.VAL_UTILIDAD);
        p.Add("TAS_CALCULO_ASISTENCIA", dto.TAS_CALCULO_ASISTENCIA);
        p.Add("TAS_CALCULO_REMUNERACION", dto.TAS_CALCULO_REMUNERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.UtilidadesAnuales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.UtilidadesAnuales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.UtilidadesAnuales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ANO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.UtilidadesAnuales.Delete, new { NUM_ANO = NUM_ANO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.UtilidadesAnuales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.UtilidadesAnuales.Delete, new { NUM_ANO = NUM_ANO })
        };
    }
}

}