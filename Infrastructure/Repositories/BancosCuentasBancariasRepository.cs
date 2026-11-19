using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class BancosCuentasBancariasRepository : IBancosCuentasBancariasRepository
{
    private readonly IDbConnectionFactory _db;
    public BancosCuentasBancariasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<BancosCuentasBancarias>> ShowallAsync(string codBanco)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BancosCuentasBancarias>(SP.SCHEMAFinanzas.BancosCuentasBancarias.Showall, new { COD_BANCO = codBanco }, commandType: CommandType.StoredProcedure);
    }

    public async Task<BancosCuentasBancarias?> ShowbyidAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<BancosCuentasBancarias>(SP.SCHEMAFinanzas.BancosCuentasBancarias.Showbyid, new { COD_BANCO = COD_BANCO, NUM_CUENTA_BANCARIA = NUM_CUENTA_BANCARIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.BancosCuentasBancarias.Showcombox);
    }

    public async Task<IEnumerable<BancosCuentasBancarias>> SearchAsync(BancosCuentasBancariasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BancosCuentasBancarias>(SP.SCHEMAFinanzas.BancosCuentasBancarias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(BancosCuentasBancariasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("NUM_CUENTA_BANCARIA", dto.NUM_CUENTA_BANCARIA);
        p.Add("NUM_CUENTA_BANCARIA_CCI", dto.NUM_CUENTA_BANCARIA_CCI);
        p.Add("COD_TIPO_CUENTA_BANCARIA", dto.COD_TIPO_CUENTA_BANCARIA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("VAL_SALDO_INICIAL", dto.VAL_SALDO_INICIAL);
        p.Add("VAL_MOVIMIENTOS_INGRESO", dto.VAL_MOVIMIENTOS_INGRESO);
        p.Add("VAL_MOVIMIENTOS_SALIDA", dto.VAL_MOVIMIENTOS_SALIDA);
        p.Add("VAL_SALDO_FINAL", dto.VAL_SALDO_FINAL);
        p.Add("FLG_PRINCIPAL", dto.FLG_PRINCIPAL);
        p.Add("FLG_HABILITADA", dto.FLG_HABILITADA);
        p.Add("FLG_CUENTA_EMPRESA", dto.FLG_CUENTA_EMPRESA);
        p.Add("FLG_COBROS", dto.FLG_COBROS);
        p.Add("FLG_PAGOS", dto.FLG_PAGOS);
        p.Add("FLG_RESTRINGIDA", dto.FLG_RESTRINGIDA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.BancosCuentasBancarias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.BancosCuentasBancarias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.BancosCuentasBancarias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(BancosCuentasBancariasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("NUM_CUENTA_BANCARIA", dto.NUM_CUENTA_BANCARIA);
        p.Add("NUM_CUENTA_BANCARIA_CCI", dto.NUM_CUENTA_BANCARIA_CCI);
        p.Add("COD_TIPO_CUENTA_BANCARIA", dto.COD_TIPO_CUENTA_BANCARIA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("VAL_SALDO_INICIAL", dto.VAL_SALDO_INICIAL);
        p.Add("VAL_MOVIMIENTOS_INGRESO", dto.VAL_MOVIMIENTOS_INGRESO);
        p.Add("VAL_MOVIMIENTOS_SALIDA", dto.VAL_MOVIMIENTOS_SALIDA);
        p.Add("VAL_SALDO_FINAL", dto.VAL_SALDO_FINAL);
        p.Add("FLG_PRINCIPAL", dto.FLG_PRINCIPAL);
        p.Add("FLG_HABILITADA", dto.FLG_HABILITADA);
        p.Add("FLG_CUENTA_EMPRESA", dto.FLG_CUENTA_EMPRESA);
        p.Add("FLG_COBROS", dto.FLG_COBROS);
        p.Add("FLG_PAGOS", dto.FLG_PAGOS);
        p.Add("FLG_RESTRINGIDA", dto.FLG_RESTRINGIDA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.BancosCuentasBancarias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.BancosCuentasBancarias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.BancosCuentasBancarias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_BANCO, string NUM_CUENTA_BANCARIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.BancosCuentasBancarias.Delete, new { COD_BANCO = COD_BANCO, NUM_CUENTA_BANCARIA = NUM_CUENTA_BANCARIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.BancosCuentasBancarias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.BancosCuentasBancarias.Delete, new { COD_BANCO = COD_BANCO, NUM_CUENTA_BANCARIA = NUM_CUENTA_BANCARIA })
        };
    }
}

}