using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CostosProcesoRepository : ICostosProcesoRepository
{
    private readonly IDbConnectionFactory _db;
    public CostosProcesoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CostosProceso>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosProceso>(SP.SCHEMACostos.CostosProceso.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CostosProceso?> ShowbyidAsync(int NUM_PERIODO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CostosProceso>(SP.SCHEMACostos.CostosProceso.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CostosProceso.Showcombox, "NUM_PERIODO_CONTABLE", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<CostosProceso>> SearchAsync(CostosProcesoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosProceso>(SP.SCHEMACostos.CostosProceso.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CostosProcesoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProceso.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProceso.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProceso.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CostosProcesoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProceso.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProceso.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProceso.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosProceso.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosProceso.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosProceso.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE })
        };
    }
}

}