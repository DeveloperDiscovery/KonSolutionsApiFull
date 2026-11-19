using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EmbaladoEmbalajesRepository : IEmbaladoEmbalajesRepository
{
    private readonly IDbConnectionFactory _db;
    public EmbaladoEmbalajesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EmbaladoEmbalajes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EmbaladoEmbalajes>(SP.SCHEMALogistica.EmbaladoEmbalajes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EmbaladoEmbalajes?> ShowbyidAsync(decimal NUM_EMBALAJE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EmbaladoEmbalajes>(SP.SCHEMALogistica.EmbaladoEmbalajes.Showbyid, new { NUM_EMBALAJE = NUM_EMBALAJE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.EmbaladoEmbalajes.Showcombox, "NUM_EMBALAJE", "DES_DETALLADA");
    }

    public async Task<IEnumerable<EmbaladoEmbalajes>> SearchAsync(EmbaladoEmbalajesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EmbaladoEmbalajes>(SP.SCHEMALogistica.EmbaladoEmbalajes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EmbaladoEmbalajesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMBALAJE", dto.NUM_EMBALAJE);
        p.Add("NUM_EMBALADO", dto.NUM_EMBALADO);
        p.Add("COD_PRODUCTO_EMBALAJE", dto.COD_PRODUCTO_EMBALAJE);
        p.Add("COD_TIPO_EMBALAJE", dto.COD_TIPO_EMBALAJE);
        p.Add("NUM_EMBALAJE_PADRE", dto.NUM_EMBALAJE_PADRE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.EmbaladoEmbalajes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.EmbaladoEmbalajes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.EmbaladoEmbalajes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EmbaladoEmbalajesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMBALAJE", dto.NUM_EMBALAJE);
        p.Add("NUM_EMBALADO", dto.NUM_EMBALADO);
        p.Add("COD_PRODUCTO_EMBALAJE", dto.COD_PRODUCTO_EMBALAJE);
        p.Add("COD_TIPO_EMBALAJE", dto.COD_TIPO_EMBALAJE);
        p.Add("NUM_EMBALAJE_PADRE", dto.NUM_EMBALAJE_PADRE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.EmbaladoEmbalajes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.EmbaladoEmbalajes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.EmbaladoEmbalajes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_EMBALAJE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.EmbaladoEmbalajes.Delete, new { NUM_EMBALAJE = NUM_EMBALAJE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.EmbaladoEmbalajes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.EmbaladoEmbalajes.Delete, new { NUM_EMBALAJE = NUM_EMBALAJE })
        };
    }
}

}