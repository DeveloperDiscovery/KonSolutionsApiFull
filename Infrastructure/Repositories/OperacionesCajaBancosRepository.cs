using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OperacionesCajaBancosRepository : IOperacionesCajaBancosRepository
{
    private readonly IDbConnectionFactory _db;
    public OperacionesCajaBancosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OperacionesCajaBancos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OperacionesCajaBancos>(SP.SCHEMAFinanzas.OperacionesCajaBancos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<OperacionesCajaBancos?> ShowbyidAsync(decimal NUM_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OperacionesCajaBancos>(SP.SCHEMAFinanzas.OperacionesCajaBancos.Showbyid, new { NUM_OPERACION = NUM_OPERACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.OperacionesCajaBancos.Showcombox, "NUM_OPERACION", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<OperacionesCajaBancos>> SearchAsync(OperacionesCajaBancosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OperacionesCajaBancos>(SP.SCHEMAFinanzas.OperacionesCajaBancos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OperacionesCajaBancosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_OPERACION", dto.NUM_OPERACION);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("FEC_OPERACION", dto.FEC_OPERACION);
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("NUM_CUENTA_BANCARIA", dto.NUM_CUENTA_BANCARIA);
        p.Add("NUM_OPERACION_BANCARIA", dto.NUM_OPERACION_BANCARIA);
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("VAL_OPERACION", dto.VAL_OPERACION);
        p.Add("COD_BANCO_DESTINO", dto.COD_BANCO_DESTINO);
        p.Add("NUM_CUENTA_BANCARIA_DESTINO", dto.NUM_CUENTA_BANCARIA_DESTINO);
        p.Add("COD_CAJA_DESTINO", dto.COD_CAJA_DESTINO);
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(OperacionesCajaBancosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_OPERACION", dto.NUM_OPERACION);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("FEC_OPERACION", dto.FEC_OPERACION);
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("NUM_CUENTA_BANCARIA", dto.NUM_CUENTA_BANCARIA);
        p.Add("NUM_OPERACION_BANCARIA", dto.NUM_OPERACION_BANCARIA);
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("VAL_OPERACION", dto.VAL_OPERACION);
        p.Add("COD_BANCO_DESTINO", dto.COD_BANCO_DESTINO);
        p.Add("NUM_CUENTA_BANCARIA_DESTINO", dto.NUM_CUENTA_BANCARIA_DESTINO);
        p.Add("COD_CAJA_DESTINO", dto.COD_CAJA_DESTINO);
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_OPERACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.OperacionesCajaBancos.Delete, new { NUM_OPERACION = NUM_OPERACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.OperacionesCajaBancos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.OperacionesCajaBancos.Delete, new { NUM_OPERACION = NUM_OPERACION })
        };
    }
}

}