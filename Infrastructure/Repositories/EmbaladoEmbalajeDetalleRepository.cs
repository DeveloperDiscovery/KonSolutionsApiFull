using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EmbaladoEmbalajeDetalleRepository : IEmbaladoEmbalajeDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public EmbaladoEmbalajeDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EmbaladoEmbalajeDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EmbaladoEmbalajeDetalle>(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EmbaladoEmbalajeDetalle?> ShowbyidAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EmbaladoEmbalajeDetalle>(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Showbyid, new { NUM_EMBALADO = NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE = NUM_SECUENCIA_EMBALAJE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Showcombox);
    }

    public async Task<IEnumerable<EmbaladoEmbalajeDetalle>> SearchAsync(EmbaladoEmbalajeDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EmbaladoEmbalajeDetalle>(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EmbaladoEmbalajeDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMBALADO", dto.NUM_EMBALADO);
        p.Add("NUM_SECUENCIA_EMBALAJE", dto.NUM_SECUENCIA_EMBALAJE);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO", dto.NUM_SECUENCIA_PEDIDO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_PRODUCTO_UNICO", dto.NUM_PRODUCTO_UNICO);
        p.Add("CAN_EMBALADO", dto.CAN_EMBALADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EmbaladoEmbalajeDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMBALADO", dto.NUM_EMBALADO);
        p.Add("NUM_SECUENCIA_EMBALAJE", dto.NUM_SECUENCIA_EMBALAJE);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO", dto.NUM_SECUENCIA_PEDIDO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_PRODUCTO_UNICO", dto.NUM_PRODUCTO_UNICO);
        p.Add("CAN_EMBALADO", dto.CAN_EMBALADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_EMBALADO, int NUM_SECUENCIA_EMBALAJE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Delete, new { NUM_EMBALADO = NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE = NUM_SECUENCIA_EMBALAJE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.EmbaladoEmbalajeDetalle.Delete, new { NUM_EMBALADO = NUM_EMBALADO, NUM_SECUENCIA_EMBALAJE = NUM_SECUENCIA_EMBALAJE })
        };
    }
}

}