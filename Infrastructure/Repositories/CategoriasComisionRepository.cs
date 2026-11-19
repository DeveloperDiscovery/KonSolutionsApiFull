using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Networking;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CategoriasComisionRepository : ICategoriasComisionRepository
{
    private readonly IDbConnectionFactory _db;
    public CategoriasComisionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CategoriasComision>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasComision>(SP.SCHEMANetworking.CategoriasComision.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CategoriasComision?> ShowbyidAsync(string COD_CATEGORIA_COMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CategoriasComision>(SP.SCHEMANetworking.CategoriasComision.Showbyid, new { COD_CATEGORIA_COMISION = COD_CATEGORIA_COMISION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANetworking.CategoriasComision.Showcombox);
    }

    public async Task<IEnumerable<CategoriasComision>> SearchAsync(CategoriasComisionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasComision>(SP.SCHEMANetworking.CategoriasComision.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CategoriasComisionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_COMISION", dto.COD_CATEGORIA_COMISION);
        p.Add("DES_CATEGORIA_COMISION", dto.DES_CATEGORIA_COMISION);
        p.Add("TAS_COMISION", dto.TAS_COMISION);
        p.Add("TAS_COMISON_REFERENCIADA", dto.TAS_COMISON_REFERENCIADA);
        p.Add("COD_TIPO_CATEGORIA_COMISION", dto.COD_TIPO_CATEGORIA_COMISION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.CategoriasComision.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.CategoriasComision.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.CategoriasComision.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CategoriasComisionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_COMISION", dto.COD_CATEGORIA_COMISION);
        p.Add("DES_CATEGORIA_COMISION", dto.DES_CATEGORIA_COMISION);
        p.Add("TAS_COMISION", dto.TAS_COMISION);
        p.Add("TAS_COMISON_REFERENCIADA", dto.TAS_COMISON_REFERENCIADA);
        p.Add("COD_TIPO_CATEGORIA_COMISION", dto.COD_TIPO_CATEGORIA_COMISION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.CategoriasComision.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.CategoriasComision.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.CategoriasComision.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CATEGORIA_COMISION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANetworking.CategoriasComision.Delete, new { COD_CATEGORIA_COMISION = COD_CATEGORIA_COMISION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANetworking.CategoriasComision.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANetworking.CategoriasComision.Delete, new { COD_CATEGORIA_COMISION = COD_CATEGORIA_COMISION })
        };
    }
}

}