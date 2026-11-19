using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposMantenimientoIndustrialRepository : ITiposMantenimientoIndustrialRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposMantenimientoIndustrialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposMantenimientoIndustrial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMantenimientoIndustrial>(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposMantenimientoIndustrial?> ShowbyidAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposMantenimientoIndustrial>(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Showbyid, new { COD_TIPO_MANTENIMIENTO_INDUSTRIAL = COD_TIPO_MANTENIMIENTO_INDUSTRIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Showcombox);
    }

    public async Task<IEnumerable<TiposMantenimientoIndustrial>> SearchAsync(TiposMantenimientoIndustrialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMantenimientoIndustrial>(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposMantenimientoIndustrialInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MANTENIMIENTO_INDUSTRIAL", dto.COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
        p.Add("DES_TIPO_MANTENIMIENTO_INDUSTRIAL", dto.DES_TIPO_MANTENIMIENTO_INDUSTRIAL);
        p.Add("COD_TIPO_PROGRAMACION", dto.COD_TIPO_PROGRAMACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposMantenimientoIndustrialUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MANTENIMIENTO_INDUSTRIAL", dto.COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
        p.Add("DES_TIPO_MANTENIMIENTO_INDUSTRIAL", dto.DES_TIPO_MANTENIMIENTO_INDUSTRIAL);
        p.Add("COD_TIPO_PROGRAMACION", dto.COD_TIPO_PROGRAMACION);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_MANTENIMIENTO_INDUSTRIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Delete, new { COD_TIPO_MANTENIMIENTO_INDUSTRIAL = COD_TIPO_MANTENIMIENTO_INDUSTRIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.TiposMantenimientoIndustrial.Delete, new { COD_TIPO_MANTENIMIENTO_INDUSTRIAL = COD_TIPO_MANTENIMIENTO_INDUSTRIAL })
        };
    }
}

}