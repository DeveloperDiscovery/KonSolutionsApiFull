using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RendicionCuentasRepository : IRendicionCuentasRepository
{
    private readonly IDbConnectionFactory _db;
    public RendicionCuentasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RendicionCuentas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RendicionCuentas>(SP.SCHEMAFinanzas.RendicionCuentas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RendicionCuentas?> ShowbyidAsync(int NUM_RENDICION_CUENTAS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RendicionCuentas>(SP.SCHEMAFinanzas.RendicionCuentas.Showbyid, new { NUM_RENDICION_CUENTAS = NUM_RENDICION_CUENTAS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.RendicionCuentas.Showcombox, "NUM_RENDICION_CUENTAS", "DES_RENDICION_CUENTAS");
    }

    public async Task<IEnumerable<RendicionCuentas>> SearchAsync(RendicionCuentasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RendicionCuentas>(SP.SCHEMAFinanzas.RendicionCuentas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RendicionCuentasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("DES_RENDICION_CUENTAS", dto.DES_RENDICION_CUENTAS);
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("COD_ENTIDAD_RENDIDORA", dto.COD_ENTIDAD_RENDIDORA);
        p.Add("FEC_RENDICION_CUENTAS", dto.FEC_RENDICION_CUENTAS);
        p.Add("NUM_OPERACION_RENDICION", dto.NUM_OPERACION_RENDICION);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("VAL_RENDICION", dto.VAL_RENDICION);
        p.Add("VAL_RENDIDO", dto.VAL_RENDIDO);
        p.Add("VAL_PENDIENTE", dto.VAL_PENDIENTE);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.RendicionCuentas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.RendicionCuentas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RendicionCuentas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RendicionCuentasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("DES_RENDICION_CUENTAS", dto.DES_RENDICION_CUENTAS);
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("COD_ENTIDAD_RENDIDORA", dto.COD_ENTIDAD_RENDIDORA);
        p.Add("FEC_RENDICION_CUENTAS", dto.FEC_RENDICION_CUENTAS);
        p.Add("NUM_OPERACION_RENDICION", dto.NUM_OPERACION_RENDICION);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("VAL_RENDICION", dto.VAL_RENDICION);
        p.Add("VAL_RENDIDO", dto.VAL_RENDIDO);
        p.Add("VAL_PENDIENTE", dto.VAL_PENDIENTE);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.RendicionCuentas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.RendicionCuentas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RendicionCuentas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_RENDICION_CUENTAS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.RendicionCuentas.Delete, new { NUM_RENDICION_CUENTAS = NUM_RENDICION_CUENTAS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.RendicionCuentas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RendicionCuentas.Delete, new { NUM_RENDICION_CUENTAS = NUM_RENDICION_CUENTAS })
        };
    }
}

}