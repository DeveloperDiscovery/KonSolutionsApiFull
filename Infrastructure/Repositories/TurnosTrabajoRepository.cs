using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TurnosTrabajoRepository : ITurnosTrabajoRepository
{
    private readonly IDbConnectionFactory _db;
    public TurnosTrabajoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TurnosTrabajo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TurnosTrabajo>(SP.SCHEMAEstandar.TurnosTrabajo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TurnosTrabajo?> ShowbyidAsync(string COD_TURNO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TurnosTrabajo>(SP.SCHEMAEstandar.TurnosTrabajo.Showbyid, new { COD_TURNO = COD_TURNO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TurnosTrabajo.Showcombox);
    }

    public async Task<IEnumerable<TurnosTrabajo>> SearchAsync(TurnosTrabajoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TurnosTrabajo>(SP.SCHEMAEstandar.TurnosTrabajo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TurnosTrabajoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TURNO", dto.COD_TURNO);
        p.Add("DES_TURNO", dto.DES_TURNO);
        p.Add("HOR_INICIO", dto.HOR_INICIO);
        p.Add("HOR_FINAL", dto.HOR_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TurnosTrabajo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TurnosTrabajo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TurnosTrabajo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TurnosTrabajoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TURNO", dto.COD_TURNO);
        p.Add("DES_TURNO", dto.DES_TURNO);
        p.Add("HOR_INICIO", dto.HOR_INICIO);
        p.Add("HOR_FINAL", dto.HOR_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TurnosTrabajo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TurnosTrabajo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TurnosTrabajo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TURNO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TurnosTrabajo.Delete, new { COD_TURNO = COD_TURNO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TurnosTrabajo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TurnosTrabajo.Delete, new { COD_TURNO = COD_TURNO })
        };
    }
}

}