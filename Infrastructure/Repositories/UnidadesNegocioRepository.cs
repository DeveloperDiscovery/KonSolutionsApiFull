using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UnidadesNegocioRepository : IUnidadesNegocioRepository
{
    private readonly IDbConnectionFactory _db;
    public UnidadesNegocioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UnidadesNegocio>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesNegocio>(SP.SCHEMAComercial.UnidadesNegocio.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UnidadesNegocio?> ShowbyidAsync(int NUM_UNIDAD_NEGOCIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UnidadesNegocio>(SP.SCHEMAComercial.UnidadesNegocio.Showbyid, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.UnidadesNegocio.Showcombox, "NUM_UNIDAD_NEGOCIO", "DES_UNIDAD_NEGOCIO");
    }

    public async Task<IEnumerable<UnidadesNegocio>> SearchAsync(UnidadesNegocioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesNegocio>(SP.SCHEMAComercial.UnidadesNegocio.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UnidadesNegocioInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("DES_UNIDAD_NEGOCIO", dto.DES_UNIDAD_NEGOCIO);
        p.Add("COD_TIPO_UNIDAD_NEGOCIO", dto.COD_TIPO_UNIDAD_NEGOCIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.UnidadesNegocio.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.UnidadesNegocio.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.UnidadesNegocio.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UnidadesNegocioUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_NEGOCIO", dto.NUM_UNIDAD_NEGOCIO);
        p.Add("DES_UNIDAD_NEGOCIO", dto.DES_UNIDAD_NEGOCIO);
        p.Add("COD_TIPO_UNIDAD_NEGOCIO", dto.COD_TIPO_UNIDAD_NEGOCIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.UnidadesNegocio.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.UnidadesNegocio.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.UnidadesNegocio.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_UNIDAD_NEGOCIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.UnidadesNegocio.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.UnidadesNegocio.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.UnidadesNegocio.Delete, new { NUM_UNIDAD_NEGOCIO = NUM_UNIDAD_NEGOCIO })
        };
    }
}

}