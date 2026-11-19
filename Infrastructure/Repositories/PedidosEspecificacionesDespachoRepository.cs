using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosEspecificacionesDespachoRepository : IPedidosEspecificacionesDespachoRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosEspecificacionesDespachoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosEspecificacionesDespacho>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosEspecificacionesDespacho>(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosEspecificacionesDespacho?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosEspecificacionesDespacho>(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Showcombox);
    }

    public async Task<IEnumerable<PedidosEspecificacionesDespacho>> SearchAsync(PedidosEspecificacionesDespachoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosEspecificacionesDespacho>(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("CAN_EMBALAJE", dto.CAN_EMBALAJE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosEspecificacionesDespacho.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}