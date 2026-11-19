using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProcesosContablesRepository : IProcesosContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public ProcesosContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProcesosContables>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosContables>(SP.SCHEMAContabilidad.ProcesosContables.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProcesosContables?> ShowbyidAsync(int NUM_PERIODO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProcesosContables>(SP.SCHEMAContabilidad.ProcesosContables.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.ProcesosContables.Showcombox, "NUM_PERIODO_CONTABLE", "DES_COMENTARIO_CIERRE");
    }

    public async Task<IEnumerable<ProcesosContables>> SearchAsync(ProcesosContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosContables>(SP.SCHEMAContabilidad.ProcesosContables.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProcesosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("DES_COMENTARIO_CIERRE", dto.DES_COMENTARIO_CIERRE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.ProcesosContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.ProcesosContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.ProcesosContables.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProcesosContablesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("DES_COMENTARIO_CIERRE", dto.DES_COMENTARIO_CIERRE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.ProcesosContables.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.ProcesosContables.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.ProcesosContables.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.ProcesosContables.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.ProcesosContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.ProcesosContables.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE })
        };
    }
}

}