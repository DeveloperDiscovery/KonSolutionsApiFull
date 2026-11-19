using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesCuentasBancariasRepository : IEntidadesCuentasBancariasRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesCuentasBancariasRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>@COD_BANCO no viaja: el SP lo trata como opcional (ver
    /// FIX_PROC_FINANZAS_ENTIDADES_CUENTAS_BANCARIAS_SHOWALL.sql) y sin él trae TODAS las
    /// cuentas de la entidad, sin importar en qué banco esté cada una.</summary>
    public async Task<IEnumerable<EntidadesCuentasBancarias>> ShowallAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesCuentasBancarias>(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Showall, new { COD_ENTIDAD_BANCARIA = codEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesCuentasBancarias?> ShowbyidAsync(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesCuentasBancarias>(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Showbyid, new { COD_ENTIDAD_BANCARIA = COD_ENTIDAD_BANCARIA, COD_BANCO = COD_BANCO, NUM_CUENTA_BANCARIA = NUM_CUENTA_BANCARIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Showcombox);
    }

    public async Task<IEnumerable<EntidadesCuentasBancarias>> SearchAsync(EntidadesCuentasBancariasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesCuentasBancarias>(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesCuentasBancariasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_BANCARIA", dto.COD_ENTIDAD_BANCARIA);
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("NUM_CUENTA_BANCARIA", dto.NUM_CUENTA_BANCARIA);
        p.Add("NUM_CUENTA_BANCARIA_CCI", dto.NUM_CUENTA_BANCARIA_CCI);
        p.Add("DES_CUENTA_BANCARIA", dto.DES_CUENTA_BANCARIA);
        p.Add("FLG_PRINCIPAL", dto.FLG_PRINCIPAL);
        p.Add("FLG_HABILITADA", dto.FLG_HABILITADA);
        p.Add("FLG_CUENTA_EMPRESA", dto.FLG_CUENTA_EMPRESA);
        p.Add("FLG_COBROS", dto.FLG_COBROS);
        p.Add("FLG_PAGOS", dto.FLG_PAGOS);
        p.Add("FLG_RESTRINGIDA", dto.FLG_RESTRINGIDA);
        p.Add("COD_TIPO_CUENTA_BANCARIA", dto.COD_TIPO_CUENTA_BANCARIA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesCuentasBancariasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_BANCARIA", dto.COD_ENTIDAD_BANCARIA);
        p.Add("COD_BANCO", dto.COD_BANCO);
        p.Add("NUM_CUENTA_BANCARIA", dto.NUM_CUENTA_BANCARIA);
        p.Add("NUM_CUENTA_BANCARIA_CCI", dto.NUM_CUENTA_BANCARIA_CCI);
        p.Add("DES_CUENTA_BANCARIA", dto.DES_CUENTA_BANCARIA);
        p.Add("FLG_PRINCIPAL", dto.FLG_PRINCIPAL);
        p.Add("FLG_HABILITADA", dto.FLG_HABILITADA);
        p.Add("FLG_CUENTA_EMPRESA", dto.FLG_CUENTA_EMPRESA);
        p.Add("FLG_COBROS", dto.FLG_COBROS);
        p.Add("FLG_PAGOS", dto.FLG_PAGOS);
        p.Add("FLG_RESTRINGIDA", dto.FLG_RESTRINGIDA);
        p.Add("COD_TIPO_CUENTA_BANCARIA", dto.COD_TIPO_CUENTA_BANCARIA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_BANCARIA, string COD_BANCO, string NUM_CUENTA_BANCARIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Delete, new { COD_ENTIDAD_BANCARIA = COD_ENTIDAD_BANCARIA, COD_BANCO = COD_BANCO, NUM_CUENTA_BANCARIA = NUM_CUENTA_BANCARIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.EntidadesCuentasBancarias.Delete, new { COD_ENTIDAD_BANCARIA = COD_ENTIDAD_BANCARIA, COD_BANCO = COD_BANCO, NUM_CUENTA_BANCARIA = NUM_CUENTA_BANCARIA })
        };
    }
}

}