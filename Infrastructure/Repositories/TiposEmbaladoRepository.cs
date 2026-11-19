using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposEmbaladoRepository : ITiposEmbaladoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposEmbaladoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposEmbalado>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEmbalado>(SP.SCHEMALogistica.TiposEmbalado.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposEmbalado?> ShowbyidAsync(string COD_TIPO_EMBALADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposEmbalado>(SP.SCHEMALogistica.TiposEmbalado.Showbyid, new { COD_TIPO_EMBALADO = COD_TIPO_EMBALADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposEmbalado.Showcombox);
    }

    public async Task<IEnumerable<TiposEmbalado>> SearchAsync(TiposEmbaladoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposEmbalado>(SP.SCHEMALogistica.TiposEmbalado.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposEmbaladoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EMBALADO", dto.COD_TIPO_EMBALADO);
        p.Add("DES_TIPO_EMBALADO", dto.DES_TIPO_EMBALADO);
        p.Add("FLG_PREPACK", dto.FLG_PREPACK ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposEmbalado.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposEmbalado.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposEmbalado.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposEmbaladoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_EMBALADO", dto.COD_TIPO_EMBALADO);
        p.Add("DES_TIPO_EMBALADO", dto.DES_TIPO_EMBALADO);
        p.Add("FLG_PREPACK", dto.FLG_PREPACK ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposEmbalado.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposEmbalado.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposEmbalado.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_EMBALADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposEmbalado.Delete, new { COD_TIPO_EMBALADO = COD_TIPO_EMBALADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposEmbalado.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposEmbalado.Delete, new { COD_TIPO_EMBALADO = COD_TIPO_EMBALADO })
        };
    }
}

}