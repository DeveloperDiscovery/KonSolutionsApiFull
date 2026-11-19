using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CajasRepository : ICajasRepository
{
    private readonly IDbConnectionFactory _db;
    public CajasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Cajas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Cajas>(SP.SCHEMAFinanzas.Cajas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Cajas?> ShowbyidAsync(string COD_CAJA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Cajas>(SP.SCHEMAFinanzas.Cajas.Showbyid, new { COD_CAJA = COD_CAJA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.Cajas.Showcombox);
    }

    public async Task<IEnumerable<Cajas>> SearchAsync(CajasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Cajas>(SP.SCHEMAFinanzas.Cajas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CajasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("DES_CAJA", dto.DES_CAJA);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("NUM_SERIE", dto.NUM_SERIE);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.Cajas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.Cajas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.Cajas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CajasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("DES_CAJA", dto.DES_CAJA);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("NUM_SERIE", dto.NUM_SERIE);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.Cajas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.Cajas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.Cajas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CAJA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.Cajas.Delete, new { COD_CAJA = COD_CAJA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.Cajas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.Cajas.Delete, new { COD_CAJA = COD_CAJA })
        };
    }
}

}