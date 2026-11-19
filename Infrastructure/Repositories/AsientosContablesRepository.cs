using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AsientosContablesRepository : IAsientosContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public AsientosContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AsientosContables>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AsientosContables>(SP.SCHEMAContabilidad.AsientosContables.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AsientosContables?> ShowbyidAsync(string COD_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AsientosContables>(SP.SCHEMAContabilidad.AsientosContables.Showbyid, new { COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.AsientosContables.Showcombox);
    }

    public async Task<IEnumerable<AsientosContables>> SearchAsync(AsientosContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AsientosContables>(SP.SCHEMAContabilidad.AsientosContables.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AsientosContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("DES_ASIENTO_CONTABLE", dto.DES_ASIENTO_CONTABLE);
        p.Add("FLG_ASIENTO_APERTURA", dto.FLG_ASIENTO_APERTURA ?? false);
        p.Add("FLG_ASIENTO_CIERRE", dto.FLG_ASIENTO_CIERRE ?? false);
        p.Add("COD_LIBRO_CONTABLE_SUNAT", dto.COD_LIBRO_CONTABLE_SUNAT);
        p.Add("FLG_APERTURA", dto.FLG_APERTURA ?? false);
        p.Add("FLG_MENSUAL", dto.FLG_MENSUAL ?? false);
        p.Add("FLG_CIERRE", dto.FLG_CIERRE ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.AsientosContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.AsientosContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.AsientosContables.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AsientosContablesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("DES_ASIENTO_CONTABLE", dto.DES_ASIENTO_CONTABLE);
        p.Add("FLG_ASIENTO_APERTURA", dto.FLG_ASIENTO_APERTURA ?? false);
        p.Add("FLG_ASIENTO_CIERRE", dto.FLG_ASIENTO_CIERRE ?? false);
        p.Add("COD_LIBRO_CONTABLE_SUNAT", dto.COD_LIBRO_CONTABLE_SUNAT);
        p.Add("FLG_APERTURA", dto.FLG_APERTURA ?? false);
        p.Add("FLG_MENSUAL", dto.FLG_MENSUAL ?? false);
        p.Add("FLG_CIERRE", dto.FLG_CIERRE ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.AsientosContables.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.AsientosContables.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.AsientosContables.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.AsientosContables.Delete, new { COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.AsientosContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.AsientosContables.Delete, new { COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE })
        };
    }
}

}