using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesDetalleEspecificacionesTecnicasRepository : IPedidosComercialesDetalleEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesDetalleEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesDetalleEspecificacionesTecnicas>(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComercialesDetalleEspecificacionesTecnicas?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComercialesDetalleEspecificacionesTecnicas>(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<PedidosComercialesDetalleEspecificacionesTecnicas>> SearchAsync(PedidosComercialesDetalleEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComercialesDetalleEspecificacionesTecnicas>(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesDetalleEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FLG_SI", dto.FLG_SI ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PedidosComercialesDetalleEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FLG_SI", dto.FLG_SI ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.PedidosComercialesDetalleEspecificacionesTecnicas.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA })
        };
    }
}

}