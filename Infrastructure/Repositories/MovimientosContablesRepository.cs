using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MovimientosContablesRepository : IMovimientosContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MovimientosContables>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosContables>(SP.SCHEMAContabilidad.MovimientosContables.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosContables?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosContables>(SP.SCHEMAContabilidad.MovimientosContables.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE = NUM_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.MovimientosContables.Showcombox, "NUM_PERIODO_CONTABLE", "DES_GLOSA_CONTABLE");
    }

    public async Task<IEnumerable<MovimientosContables>> SearchAsync(MovimientosContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosContables>(SP.SCHEMAContabilidad.MovimientosContables.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("FEC_ASIENTO_CONTABLE", dto.FEC_ASIENTO_CONTABLE);
        p.Add("DES_GLOSA_CONTABLE", dto.DES_GLOSA_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.MovimientosContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.MovimientosContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.MovimientosContables.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MovimientosContablesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("FEC_ASIENTO_CONTABLE", dto.FEC_ASIENTO_CONTABLE);
        p.Add("DES_GLOSA_CONTABLE", dto.DES_GLOSA_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.MovimientosContables.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.MovimientosContables.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.MovimientosContables.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.MovimientosContables.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE = NUM_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.MovimientosContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.MovimientosContables.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE = NUM_ASIENTO_CONTABLE })
        };
    }
}

}