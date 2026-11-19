using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FlujosProcesoMemoriaRepository : IFlujosProcesoMemoriaRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosProcesoMemoriaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosProcesoMemoria>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoMemoria>(SP.SCHEMAEntorno.FlujosProcesoMemoria.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosProcesoMemoria?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosProcesoMemoria>(SP.SCHEMAEntorno.FlujosProcesoMemoria.Showbyid, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.FlujosProcesoMemoria.Showcombox);
    }

    public async Task<IEnumerable<FlujosProcesoMemoria>> SearchAsync(FlujosProcesoMemoriaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoMemoria>(SP.SCHEMAEntorno.FlujosProcesoMemoria.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosProcesoMemoriaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoMemoria.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoMemoria.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoMemoria.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FlujosProcesoMemoriaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoMemoria.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoMemoria.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoMemoria.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoMemoria.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoMemoria.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoMemoria.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}