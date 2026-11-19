using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EmbaladoRepository : IEmbaladoRepository
{
    private readonly IDbConnectionFactory _db;
    public EmbaladoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Embalado>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Embalado>(SP.SCHEMALogistica.Embalado.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Embalado?> ShowbyidAsync(int NUM_EMBALADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Embalado>(SP.SCHEMALogistica.Embalado.Showbyid, new { NUM_EMBALADO = NUM_EMBALADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.Embalado.Showcombox, "NUM_EMBALADO", "DES_EMBALADO");
    }

    public async Task<IEnumerable<Embalado>> SearchAsync(EmbaladoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Embalado>(SP.SCHEMALogistica.Embalado.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EmbaladoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMBALADO", dto.NUM_EMBALADO);
        p.Add("DES_EMBALADO", dto.DES_EMBALADO);
        p.Add("FEC_EMBALADO", dto.FEC_EMBALADO);
        p.Add("FEC_DESPACHO", dto.FEC_DESPACHO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_EMBALADO", dto.COD_TIPO_EMBALADO);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Embalado.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Embalado.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Embalado.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EmbaladoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMBALADO", dto.NUM_EMBALADO);
        p.Add("DES_EMBALADO", dto.DES_EMBALADO);
        p.Add("FEC_EMBALADO", dto.FEC_EMBALADO);
        p.Add("FEC_DESPACHO", dto.FEC_DESPACHO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_EMBALADO", dto.COD_TIPO_EMBALADO);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Embalado.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Embalado.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Embalado.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_EMBALADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Embalado.Delete, new { NUM_EMBALADO = NUM_EMBALADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Embalado.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Embalado.Delete, new { NUM_EMBALADO = NUM_EMBALADO })
        };
    }
}

}