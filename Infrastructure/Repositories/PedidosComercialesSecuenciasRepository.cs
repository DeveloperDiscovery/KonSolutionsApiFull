using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesSecuenciasRepository : IPedidosComercialesSecuenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesSecuenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComercialesSecuencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesSecuencias>(SP.SCHEMAComercial.PedidosComercialesSecuencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComercialesSecuencias?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComercialesSecuencias>(SP.SCHEMAComercial.PedidosComercialesSecuencias.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.PedidosComercialesSecuencias.Showcombox);
    }

    public async Task<IEnumerable<PedidosComercialesSecuencias>> SearchAsync(PedidosComercialesSecuenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesSecuencias>(SP.SCHEMAComercial.PedidosComercialesSecuencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesSecuenciasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("CAN_SOLICITADA", dto.CAN_SOLICITADA);
        p.Add("CAN_TOLERANCIA_POSITIVA", dto.CAN_TOLERANCIA_POSITIVA);
        p.Add("CAN_TOLERANCIA_NEGATIVA", dto.CAN_TOLERANCIA_NEGATIVA);
        p.Add("TAS_TOLERANCIA_POSITIVA", dto.TAS_TOLERANCIA_POSITIVA);
        p.Add("TAS_TOLERANCIA_NEGATIVA", dto.TAS_TOLERANCIA_NEGATIVA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesSecuencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesSecuencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesSecuencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PedidosComercialesSecuenciasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("CAN_SOLICITADA", dto.CAN_SOLICITADA);
        p.Add("CAN_TOLERANCIA_POSITIVA", dto.CAN_TOLERANCIA_POSITIVA);
        p.Add("CAN_TOLERANCIA_NEGATIVA", dto.CAN_TOLERANCIA_NEGATIVA);
        p.Add("TAS_TOLERANCIA_POSITIVA", dto.TAS_TOLERANCIA_POSITIVA);
        p.Add("TAS_TOLERANCIA_NEGATIVA", dto.TAS_TOLERANCIA_NEGATIVA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesSecuencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesSecuencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesSecuencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComercialesSecuencias.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosComercialesSecuencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComercialesSecuencias.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}