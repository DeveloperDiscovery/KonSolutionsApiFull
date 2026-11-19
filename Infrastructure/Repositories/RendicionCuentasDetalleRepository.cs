using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RendicionCuentasDetalleRepository : IRendicionCuentasDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public RendicionCuentasDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RendicionCuentasDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RendicionCuentasDetalle>(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RendicionCuentasDetalle?> ShowbyidAsync(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RendicionCuentasDetalle>(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Showbyid, new { NUM_RENDICION_CUENTAS = NUM_RENDICION_CUENTAS, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.RendicionCuentasDetalle.Showcombox, "NUM_RENDICION_CUENTAS", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<RendicionCuentasDetalle>> SearchAsync(RendicionCuentasDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RendicionCuentasDetalle>(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RendicionCuentasDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_CONCEPTO_FINANCIERO", dto.COD_TIPO_CONCEPTO_FINANCIERO);
        p.Add("COD_CONCEPTO_FINANCIERO", dto.COD_CONCEPTO_FINANCIERO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.RendicionCuentasDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RendicionCuentasDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_CONCEPTO_FINANCIERO", dto.COD_TIPO_CONCEPTO_FINANCIERO);
        p.Add("COD_CONCEPTO_FINANCIERO", dto.COD_CONCEPTO_FINANCIERO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.RendicionCuentasDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_RENDICION_CUENTAS, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Delete, new { NUM_RENDICION_CUENTAS = NUM_RENDICION_CUENTAS, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.RendicionCuentasDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RendicionCuentasDetalle.Delete, new { NUM_RENDICION_CUENTAS = NUM_RENDICION_CUENTAS, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}