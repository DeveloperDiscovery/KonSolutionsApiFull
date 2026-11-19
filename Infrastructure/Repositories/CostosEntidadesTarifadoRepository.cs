using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CostosEntidadesTarifadoRepository : ICostosEntidadesTarifadoRepository
{
    private readonly IDbConnectionFactory _db;
    public CostosEntidadesTarifadoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CostosEntidadesTarifado>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosEntidadesTarifado>(SP.SCHEMACostos.CostosEntidadesTarifado.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CostosEntidadesTarifado?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CostosEntidadesTarifado>(SP.SCHEMACostos.CostosEntidadesTarifado.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_PROCESO = COD_PROCESO, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CostosEntidadesTarifado.Showcombox);
    }

    public async Task<IEnumerable<CostosEntidadesTarifado>> SearchAsync(CostosEntidadesTarifadoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CostosEntidadesTarifado>(SP.SCHEMACostos.CostosEntidadesTarifado.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CostosEntidadesTarifadoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("VAL_TARIFADO", dto.VAL_TARIFADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosEntidadesTarifado.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosEntidadesTarifado.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosEntidadesTarifado.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CostosEntidadesTarifadoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("VAL_TARIFADO", dto.VAL_TARIFADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosEntidadesTarifado.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosEntidadesTarifado.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosEntidadesTarifado.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_PROCESO, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CostosEntidadesTarifado.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_PROCESO = COD_PROCESO, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CostosEntidadesTarifado.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CostosEntidadesTarifado.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_PROCESO = COD_PROCESO, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}