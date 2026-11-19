using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCuentasBancariasRepository : ITiposCuentasBancariasRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCuentasBancariasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCuentasBancarias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCuentasBancarias>(SP.SCHEMAFinanzas.TiposCuentasBancarias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCuentasBancarias?> ShowbyidAsync(string COD_TIPO_CUENTA_BANCARIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCuentasBancarias>(SP.SCHEMAFinanzas.TiposCuentasBancarias.Showbyid, new { COD_TIPO_CUENTA_BANCARIA = COD_TIPO_CUENTA_BANCARIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.TiposCuentasBancarias.Showcombox);
    }

    public async Task<IEnumerable<TiposCuentasBancarias>> SearchAsync(TiposCuentasBancariasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCuentasBancarias>(SP.SCHEMAFinanzas.TiposCuentasBancarias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCuentasBancariasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CUENTA_BANCARIA", dto.COD_TIPO_CUENTA_BANCARIA);
        p.Add("DES_TIPO_CUENTA_BANCARIA", dto.DES_TIPO_CUENTA_BANCARIA);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.TiposCuentasBancarias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.TiposCuentasBancarias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.TiposCuentasBancarias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCuentasBancariasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CUENTA_BANCARIA", dto.COD_TIPO_CUENTA_BANCARIA);
        p.Add("DES_TIPO_CUENTA_BANCARIA", dto.DES_TIPO_CUENTA_BANCARIA);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.TiposCuentasBancarias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.TiposCuentasBancarias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.TiposCuentasBancarias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CUENTA_BANCARIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.TiposCuentasBancarias.Delete, new { COD_TIPO_CUENTA_BANCARIA = COD_TIPO_CUENTA_BANCARIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.TiposCuentasBancarias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.TiposCuentasBancarias.Delete, new { COD_TIPO_CUENTA_BANCARIA = COD_TIPO_CUENTA_BANCARIA })
        };
    }
}

}