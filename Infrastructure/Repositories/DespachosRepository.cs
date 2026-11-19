using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DespachosRepository : IDespachosRepository
{
    private readonly IDbConnectionFactory _db;
    public DespachosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Despachos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Despachos>(SP.SCHEMALogistica.Despachos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Despachos?> ShowbyidAsync(int NUM_DESPACHO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Despachos>(SP.SCHEMALogistica.Despachos.Showbyid, new { NUM_DESPACHO = NUM_DESPACHO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.Despachos.Showcombox, "NUM_DESPACHO", "DES_DESPACHO");
    }

    public async Task<IEnumerable<Despachos>> SearchAsync(DespachosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Despachos>(SP.SCHEMALogistica.Despachos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DespachosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DESPACHO", dto.NUM_DESPACHO);
        p.Add("DES_DESPACHO", dto.DES_DESPACHO);
        p.Add("FEC_DESPACHO", dto.FEC_DESPACHO);
        p.Add("COD_ENTIDAD_TRANSPORTISTA", dto.COD_ENTIDAD_TRANSPORTISTA);
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Despachos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Despachos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Despachos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DespachosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DESPACHO", dto.NUM_DESPACHO);
        p.Add("DES_DESPACHO", dto.DES_DESPACHO);
        p.Add("FEC_DESPACHO", dto.FEC_DESPACHO);
        p.Add("COD_ENTIDAD_TRANSPORTISTA", dto.COD_ENTIDAD_TRANSPORTISTA);
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Despachos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Despachos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Despachos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_DESPACHO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Despachos.Delete, new { NUM_DESPACHO = NUM_DESPACHO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Despachos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Despachos.Delete, new { NUM_DESPACHO = NUM_DESPACHO })
        };
    }
}

}