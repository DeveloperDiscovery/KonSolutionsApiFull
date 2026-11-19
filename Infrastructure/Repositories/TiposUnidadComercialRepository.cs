using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposUnidadComercialRepository : ITiposUnidadComercialRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposUnidadComercialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposUnidadComercial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUnidadComercial>(SP.SCHEMAComercial.TiposUnidadComercial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposUnidadComercial?> ShowbyidAsync(string COD_TIPO_UNIDAD_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposUnidadComercial>(SP.SCHEMAComercial.TiposUnidadComercial.Showbyid, new { COD_TIPO_UNIDAD_COMERCIAL = COD_TIPO_UNIDAD_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposUnidadComercial.Showcombox);
    }

    public async Task<IEnumerable<TiposUnidadComercial>> SearchAsync(TiposUnidadComercialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposUnidadComercial>(SP.SCHEMAComercial.TiposUnidadComercial.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposUnidadComercialInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UNIDAD_COMERCIAL", dto.COD_TIPO_UNIDAD_COMERCIAL);
        p.Add("DES_TIPO_UNIDAD_COMERCIAL", dto.DES_TIPO_UNIDAD_COMERCIAL);
        p.Add("FLG_MESA", dto.FLG_MESA ?? false);
        p.Add("FLG_STAND", dto.FLG_STAND ?? false);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposUnidadComercial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposUnidadComercial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposUnidadComercial.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposUnidadComercialUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_UNIDAD_COMERCIAL", dto.COD_TIPO_UNIDAD_COMERCIAL);
        p.Add("DES_TIPO_UNIDAD_COMERCIAL", dto.DES_TIPO_UNIDAD_COMERCIAL);
        p.Add("FLG_MESA", dto.FLG_MESA ?? false);
        p.Add("FLG_STAND", dto.FLG_STAND ?? false);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposUnidadComercial.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposUnidadComercial.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposUnidadComercial.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_UNIDAD_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposUnidadComercial.Delete, new { COD_TIPO_UNIDAD_COMERCIAL = COD_TIPO_UNIDAD_COMERCIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposUnidadComercial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposUnidadComercial.Delete, new { COD_TIPO_UNIDAD_COMERCIAL = COD_TIPO_UNIDAD_COMERCIAL })
        };
    }
}

}