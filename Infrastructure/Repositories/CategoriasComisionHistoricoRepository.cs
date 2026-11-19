using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CategoriasComisionHistoricoRepository : ICategoriasComisionHistoricoRepository
{
    private readonly IDbConnectionFactory _db;
    public CategoriasComisionHistoricoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CategoriasComisionHistorico>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasComisionHistorico>(SP.SCHEMANetworking.CategoriasComisionHistorico.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CategoriasComisionHistorico?> ShowbyidAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CategoriasComisionHistorico>(SP.SCHEMANetworking.CategoriasComisionHistorico.Showbyid, new { COD_CATEGORIA_COMISION = COD_CATEGORIA_COMISION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.CategoriasComisionHistorico.Showcombox);
    }

    public async Task<IEnumerable<CategoriasComisionHistorico>> SearchAsync(CategoriasComisionHistoricoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasComisionHistorico>(SP.SCHEMANetworking.CategoriasComisionHistorico.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CategoriasComisionHistoricoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_COMISION", dto.COD_CATEGORIA_COMISION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("TAS_COMISION", dto.TAS_COMISION);
        p.Add("TAS_COMISON_REFERENCIADA", dto.TAS_COMISON_REFERENCIADA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.CategoriasComisionHistorico.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.CategoriasComisionHistorico.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.CategoriasComisionHistorico.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CategoriasComisionHistoricoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_COMISION", dto.COD_CATEGORIA_COMISION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("TAS_COMISION", dto.TAS_COMISION);
        p.Add("TAS_COMISON_REFERENCIADA", dto.TAS_COMISON_REFERENCIADA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.CategoriasComisionHistorico.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.CategoriasComisionHistorico.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.CategoriasComisionHistorico.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CATEGORIA_COMISION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.CategoriasComisionHistorico.Delete, new { COD_CATEGORIA_COMISION = COD_CATEGORIA_COMISION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.CategoriasComisionHistorico.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.CategoriasComisionHistorico.Delete, new { COD_CATEGORIA_COMISION = COD_CATEGORIA_COMISION, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}