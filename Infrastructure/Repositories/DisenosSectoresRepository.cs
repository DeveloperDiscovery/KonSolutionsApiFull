using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosSectoresRepository : IDisenosSectoresRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosSectoresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosSectores>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosSectores>(SP.SCHEMAIngenieria.DisenosSectores.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosSectores?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosSectores>(SP.SCHEMAIngenieria.DisenosSectores.Showbyid, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.DisenosSectores.Showcombox);
    }

    public async Task<IEnumerable<DisenosSectores>> SearchAsync(DisenosSectoresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosSectores>(SP.SCHEMAIngenieria.DisenosSectores.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosSectoresInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("DES_SECTOR_PRODUCTO", dto.DES_SECTOR_PRODUCTO);
        p.Add("FLG_TOTAL_PRODUCTO", dto.FLG_TOTAL_PRODUCTO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosSectores.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosSectores.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosSectores.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DisenosSectoresUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("DES_SECTOR_PRODUCTO", dto.DES_SECTOR_PRODUCTO);
        p.Add("FLG_TOTAL_PRODUCTO", dto.FLG_TOTAL_PRODUCTO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosSectores.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosSectores.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosSectores.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosSectores.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosSectores.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosSectores.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO })
        };
    }
}

}