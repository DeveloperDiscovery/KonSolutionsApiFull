using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposMedioContactoRepository : ITiposMedioContactoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposMedioContactoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposMedioContacto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMedioContacto>(SP.SCHEMAComercial.TiposMedioContacto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposMedioContacto?> ShowbyidAsync(int COD_TIPO_MEDIO_CONTACTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposMedioContacto>(SP.SCHEMAComercial.TiposMedioContacto.Showbyid, new { COD_TIPO_MEDIO_CONTACTO = COD_TIPO_MEDIO_CONTACTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposMedioContacto.Showcombox);
    }

    public async Task<IEnumerable<TiposMedioContacto>> SearchAsync(TiposMedioContactoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMedioContacto>(SP.SCHEMAComercial.TiposMedioContacto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposMedioContactoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MEDIO_CONTACTO", dto.COD_TIPO_MEDIO_CONTACTO);
        p.Add("DES_TIPO_MEDIO_CONTACTO", dto.DES_TIPO_MEDIO_CONTACTO);
        p.Add("FLG_CORREO", dto.FLG_CORREO ?? false);
        p.Add("FLG_TELEFONO", dto.FLG_TELEFONO ?? false);
        p.Add("FLG_WEB", dto.FLG_WEB ?? false);
        p.Add("FLG_RED_SOCIAL", dto.FLG_RED_SOCIAL ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposMedioContacto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposMedioContacto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposMedioContacto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposMedioContactoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MEDIO_CONTACTO", dto.COD_TIPO_MEDIO_CONTACTO);
        p.Add("DES_TIPO_MEDIO_CONTACTO", dto.DES_TIPO_MEDIO_CONTACTO);
        p.Add("FLG_CORREO", dto.FLG_CORREO ?? false);
        p.Add("FLG_TELEFONO", dto.FLG_TELEFONO ?? false);
        p.Add("FLG_WEB", dto.FLG_WEB ?? false);
        p.Add("FLG_RED_SOCIAL", dto.FLG_RED_SOCIAL ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposMedioContacto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposMedioContacto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposMedioContacto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_MEDIO_CONTACTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposMedioContacto.Delete, new { COD_TIPO_MEDIO_CONTACTO = COD_TIPO_MEDIO_CONTACTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposMedioContacto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposMedioContacto.Delete, new { COD_TIPO_MEDIO_CONTACTO = COD_TIPO_MEDIO_CONTACTO })
        };
    }
}

}