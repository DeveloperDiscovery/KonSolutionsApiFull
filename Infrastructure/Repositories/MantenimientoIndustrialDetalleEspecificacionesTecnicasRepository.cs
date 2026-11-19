using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MantenimientoIndustrialDetalleEspecificacionesTecnicasRepository : IMantenimientoIndustrialDetalleEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public MantenimientoIndustrialDetalleEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalleEspecificacionesTecnicas>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MantenimientoIndustrialDetalleEspecificacionesTecnicas?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MantenimientoIndustrialDetalleEspecificacionesTecnicas>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Showbyid, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<MantenimientoIndustrialDetalleEspecificacionesTecnicas>> SearchAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalleEspecificacionesTecnicas>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MantenimientoIndustrialDetalleEspecificacionesTecnicasUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEspecificacionesTecnicas.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA })
        };
    }
}

}