using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CajasSaldosRepository : ICajasSaldosRepository
{
    private readonly IDbConnectionFactory _db;
    public CajasSaldosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CajasSaldos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CajasSaldos>(SP.SCHEMAFinanzas.CajasSaldos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CajasSaldos?> ShowbyidAsync(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CajasSaldos>(SP.SCHEMAFinanzas.CajasSaldos.Showbyid, new { COD_CAJA = COD_CAJA, FEC_CIERRE = FEC_CIERRE, COD_TURNO = COD_TURNO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAFinanzas.CajasSaldos.Showcombox);
    }

    public async Task<IEnumerable<CajasSaldos>> SearchAsync(CajasSaldosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CajasSaldos>(SP.SCHEMAFinanzas.CajasSaldos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CajasSaldosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("FEC_CIERRE", dto.FEC_CIERRE);
        p.Add("COD_TURNO", dto.COD_TURNO);
        p.Add("COD_ENTIDAD_CAJERO", dto.COD_ENTIDAD_CAJERO);
        p.Add("VAL_SALDO_INICIAL", dto.VAL_SALDO_INICIAL);
        p.Add("VAL_MOVIMIENTOS_INGRESO", dto.VAL_MOVIMIENTOS_INGRESO);
        p.Add("VAL_MOVIMIENTOS_SALIDA", dto.VAL_MOVIMIENTOS_SALIDA);
        p.Add("VAL_SALDO_FINAL", dto.VAL_SALDO_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.CajasSaldos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.CajasSaldos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.CajasSaldos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CajasSaldosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CAJA", dto.COD_CAJA);
        p.Add("FEC_CIERRE", dto.FEC_CIERRE);
        p.Add("COD_TURNO", dto.COD_TURNO);
        p.Add("COD_ENTIDAD_CAJERO", dto.COD_ENTIDAD_CAJERO);
        p.Add("VAL_SALDO_INICIAL", dto.VAL_SALDO_INICIAL);
        p.Add("VAL_MOVIMIENTOS_INGRESO", dto.VAL_MOVIMIENTOS_INGRESO);
        p.Add("VAL_MOVIMIENTOS_SALIDA", dto.VAL_MOVIMIENTOS_SALIDA);
        p.Add("VAL_SALDO_FINAL", dto.VAL_SALDO_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.CajasSaldos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.CajasSaldos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.CajasSaldos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CAJA, DateTime FEC_CIERRE, string COD_TURNO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAFinanzas.CajasSaldos.Delete, new { COD_CAJA = COD_CAJA, FEC_CIERRE = FEC_CIERRE, COD_TURNO = COD_TURNO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAFinanzas.CajasSaldos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.CajasSaldos.Delete, new { COD_CAJA = COD_CAJA, FEC_CIERRE = FEC_CIERRE, COD_TURNO = COD_TURNO })
        };
    }
}

}