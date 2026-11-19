using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CostosCentroCostosProcesosRepository : ICostosCentroCostosProcesosRepository
{
    private readonly IDbConnectionFactory _db;
    public CostosCentroCostosProcesosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CostosCentroCostosProcesos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosCentroCostosProcesos>(SP.SCHEMACostos.CostosCentroCostosProcesos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CostosCentroCostosProcesos?> ShowbyidAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CostosCentroCostosProcesos>(SP.SCHEMACostos.CostosCentroCostosProcesos.Showbyid, new { COD_CENTRO_COSTO = COD_CENTRO_COSTO, COD_PROCESO = COD_PROCESO, COD_SUB_PROCESO = COD_SUB_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CostosCentroCostosProcesos.Showcombox);
    }

    public async Task<IEnumerable<CostosCentroCostosProcesos>> SearchAsync(CostosCentroCostosProcesosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosCentroCostosProcesos>(SP.SCHEMACostos.CostosCentroCostosProcesos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CostosCentroCostosProcesosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("VAL_COSTO_DISTRIBUIDO", dto.VAL_COSTO_DISTRIBUIDO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosCentroCostosProcesos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosCentroCostosProcesos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosCentroCostosProcesos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CostosCentroCostosProcesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("VAL_COSTO_DISTRIBUIDO", dto.VAL_COSTO_DISTRIBUIDO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosCentroCostosProcesos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosCentroCostosProcesos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosCentroCostosProcesos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CENTRO_COSTO, string COD_PROCESO, string COD_SUB_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosCentroCostosProcesos.Delete, new { COD_CENTRO_COSTO = COD_CENTRO_COSTO, COD_PROCESO = COD_PROCESO, COD_SUB_PROCESO = COD_SUB_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosCentroCostosProcesos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosCentroCostosProcesos.Delete, new { COD_CENTRO_COSTO = COD_CENTRO_COSTO, COD_PROCESO = COD_PROCESO, COD_SUB_PROCESO = COD_SUB_PROCESO })
        };
    }
}

}