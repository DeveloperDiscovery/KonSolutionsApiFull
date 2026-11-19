using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPosRepository : ITiposPosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPos>(SP.SCHEMAComercial.TiposPos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPos?> ShowbyidAsync(string COD_TIPO_POS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPos>(SP.SCHEMAComercial.TiposPos.Showbyid, new { COD_TIPO_POS = COD_TIPO_POS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposPos.Showcombox);
    }

    public async Task<IEnumerable<TiposPos>> SearchAsync(TiposPosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPos>(SP.SCHEMAComercial.TiposPos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_POS", dto.COD_TIPO_POS);
        p.Add("DES_TIPO_POS", dto.DES_TIPO_POS);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("FLG_VENTA_MENUDEO", dto.FLG_VENTA_MENUDEO ?? false);
        p.Add("FLG_PEDIDO_VENTA_RESTAURANTE", dto.FLG_PEDIDO_VENTA_RESTAURANTE ?? false);
        p.Add("FLG_PEDIDO_VENTA_POR_MAYOR", dto.FLG_PEDIDO_VENTA_POR_MAYOR ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposPosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_POS", dto.COD_TIPO_POS);
        p.Add("DES_TIPO_POS", dto.DES_TIPO_POS);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("FLG_VENTA_MENUDEO", dto.FLG_VENTA_MENUDEO ?? false);
        p.Add("FLG_PEDIDO_VENTA_RESTAURANTE", dto.FLG_PEDIDO_VENTA_RESTAURANTE ?? false);
        p.Add("FLG_PEDIDO_VENTA_POR_MAYOR", dto.FLG_PEDIDO_VENTA_POR_MAYOR ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_POS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposPos.Delete, new { COD_TIPO_POS = COD_TIPO_POS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposPos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposPos.Delete, new { COD_TIPO_POS = COD_TIPO_POS })
        };
    }
}

}