using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FlujosProcesoEtapasDocumentacionRepository : IFlujosProcesoEtapasDocumentacionRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosProcesoEtapasDocumentacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosProcesoEtapasDocumentacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoEtapasDocumentacion>(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosProcesoEtapasDocumentacion?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosProcesoEtapasDocumentacion>(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Showbyid, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Showcombox);
    }

    public async Task<IEnumerable<FlujosProcesoEtapasDocumentacion>> SearchAsync(FlujosProcesoEtapasDocumentacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoEtapasDocumentacion>(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosProcesoEtapasDocumentacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_ETAPA", dto.NUM_ETAPA);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_PATH_FILE", dto.DES_PATH_FILE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FlujosProcesoEtapasDocumentacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_ETAPA", dto.NUM_ETAPA);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_PATH_FILE", dto.DES_PATH_FILE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapasDocumentacion.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}