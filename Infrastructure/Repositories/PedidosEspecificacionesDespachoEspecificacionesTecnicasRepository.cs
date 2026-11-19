using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosEspecificacionesDespachoEspecificacionesTecnicasRepository : IPedidosEspecificacionesDespachoEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosEspecificacionesDespachoEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosEspecificacionesDespachoEspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosEspecificacionesDespachoEspecificacionesTecnicas>(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosEspecificacionesDespachoEspecificacionesTecnicas?> ShowbyidAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosEspecificacionesDespachoEspecificacionesTecnicas>(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Showbyid, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<PedidosEspecificacionesDespachoEspecificacionesTecnicas>> SearchAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosEspecificacionesDespachoEspecificacionesTecnicas>(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FLG_HEADER", dto.FLG_HEADER ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PedidosEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FLG_HEADER", dto.FLG_HEADER ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PEDIDO_COMERCIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosEspecificacionesDespachoEspecificacionesTecnicas.Delete, new { NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA })
        };
    }
}

}