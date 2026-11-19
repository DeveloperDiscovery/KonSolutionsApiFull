using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposUnidadNegocioRepository : ITiposUnidadNegocioRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposUnidadNegocioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposUnidadNegocio>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUnidadNegocio>(SP.SCHEMAComercial.TiposUnidadNegocio.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposUnidadNegocio?> ShowbyidAsync(string COD_TIPO_UNIDAD_NEGOCIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposUnidadNegocio>(SP.SCHEMAComercial.TiposUnidadNegocio.Showbyid, new { COD_TIPO_UNIDAD_NEGOCIO = COD_TIPO_UNIDAD_NEGOCIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposUnidadNegocio.Showcombox);
    }

    public async Task<IEnumerable<TiposUnidadNegocio>> SearchAsync(TiposUnidadNegocioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUnidadNegocio>(SP.SCHEMAComercial.TiposUnidadNegocio.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposUnidadNegocioInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UNIDAD_NEGOCIO", dto.COD_TIPO_UNIDAD_NEGOCIO);
        p.Add("DES_TIPO_UNIDAD_NEGOCIO", dto.DES_TIPO_UNIDAD_NEGOCIO);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposUnidadNegocio.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposUnidadNegocio.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposUnidadNegocio.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposUnidadNegocioUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UNIDAD_NEGOCIO", dto.COD_TIPO_UNIDAD_NEGOCIO);
        p.Add("DES_TIPO_UNIDAD_NEGOCIO", dto.DES_TIPO_UNIDAD_NEGOCIO);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposUnidadNegocio.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposUnidadNegocio.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposUnidadNegocio.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_UNIDAD_NEGOCIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposUnidadNegocio.Delete, new { COD_TIPO_UNIDAD_NEGOCIO = COD_TIPO_UNIDAD_NEGOCIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposUnidadNegocio.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposUnidadNegocio.Delete, new { COD_TIPO_UNIDAD_NEGOCIO = COD_TIPO_UNIDAD_NEGOCIO })
        };
    }
}

}