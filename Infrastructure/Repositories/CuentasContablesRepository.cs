using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CuentasContablesRepository : ICuentasContablesRepository
{
    private readonly IDbConnectionFactory _db;
    public CuentasContablesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CuentasContables>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CuentasContables>(SP.SCHEMAContabilidad.CuentasContables.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CuentasContables?> ShowbyidAsync(string COD_CUENTA_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CuentasContables>(SP.SCHEMAContabilidad.CuentasContables.Showbyid, new { COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.CuentasContables.Showcombox);
    }

    public async Task<IEnumerable<CuentasContables>> SearchAsync(CuentasContablesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CuentasContables>(SP.SCHEMAContabilidad.CuentasContables.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CuentasContablesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("DES_CUENTA_CONTABLE", dto.DES_CUENTA_CONTABLE);
        p.Add("NUM_NIVEL", dto.NUM_NIVEL);
        p.Add("COD_CUENTA_CONTABLE_PADRE", dto.COD_CUENTA_CONTABLE_PADRE);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("FLG_GENERA_ASIENTO", dto.FLG_GENERA_ASIENTO ?? false);
        p.Add("FLG_CENTRO_COSTO", dto.FLG_CENTRO_COSTO ?? false);
        p.Add("COD_TIPO_CUENTA_CONTABLE", dto.COD_TIPO_CUENTA_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CuentasContables.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.CuentasContables.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CuentasContables.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CuentasContablesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("DES_CUENTA_CONTABLE", dto.DES_CUENTA_CONTABLE);
        p.Add("NUM_NIVEL", dto.NUM_NIVEL);
        p.Add("COD_CUENTA_CONTABLE_PADRE", dto.COD_CUENTA_CONTABLE_PADRE);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("FLG_GENERA_ASIENTO", dto.FLG_GENERA_ASIENTO ?? false);
        p.Add("FLG_CENTRO_COSTO", dto.FLG_CENTRO_COSTO ?? false);
        p.Add("COD_TIPO_CUENTA_CONTABLE", dto.COD_TIPO_CUENTA_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CuentasContables.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.CuentasContables.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CuentasContables.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CUENTA_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CuentasContables.Delete, new { COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.CuentasContables.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CuentasContables.Delete, new { COD_CUENTA_CONTABLE = COD_CUENTA_CONTABLE })
        };
    }
}

}