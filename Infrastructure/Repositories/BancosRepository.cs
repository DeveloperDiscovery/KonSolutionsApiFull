using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class BancosRepository : IBancosRepository
{
    private readonly IDbConnectionFactory _db;
    public BancosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Bancos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Bancos>(SP.SCHEMAFinanzas.Bancos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Bancos?> ShowbyidAsync(string COD_BANCO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Bancos>(SP.SCHEMAFinanzas.Bancos.Showbyid, new { COD_BANCO = COD_BANCO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.Bancos.Showcombox);
    }

    public async Task<IEnumerable<Bancos>> SearchAsync(BancosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Bancos>(SP.SCHEMAFinanzas.Bancos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(BancosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("DES_BANCO", dto.DES_BANCO);
        p.Add("COD_BANCO_SUNAT", dto.COD_BANCO_SUNAT);
        p.Add("COD_PAIS", dto.COD_PAIS);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.Bancos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.Bancos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.Bancos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(BancosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("DES_BANCO", dto.DES_BANCO);
        p.Add("COD_BANCO_SUNAT", dto.COD_BANCO_SUNAT);
        p.Add("COD_PAIS", dto.COD_PAIS);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.Bancos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.Bancos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.Bancos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_BANCO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.Bancos.Delete, new { COD_BANCO = COD_BANCO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.Bancos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.Bancos.Delete, new { COD_BANCO = COD_BANCO })
        };
    }
}

}