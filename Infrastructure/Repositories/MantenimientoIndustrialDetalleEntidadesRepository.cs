using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MantenimientoIndustrialDetalleEntidadesRepository : IMantenimientoIndustrialDetalleEntidadesRepository
{
    private readonly IDbConnectionFactory _db;
    public MantenimientoIndustrialDetalleEntidadesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MantenimientoIndustrialDetalleEntidades>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalleEntidades>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MantenimientoIndustrialDetalleEntidades?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MantenimientoIndustrialDetalleEntidades>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Showbyid, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ENTIDAD_ASIGNADA = COD_ENTIDAD_ASIGNADA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Showcombox);
    }

    public async Task<IEnumerable<MantenimientoIndustrialDetalleEntidades>> SearchAsync(MantenimientoIndustrialDetalleEntidadesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalleEntidades>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, decimal COD_ENTIDAD_ASIGNADA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ENTIDAD_ASIGNADA = COD_ENTIDAD_ASIGNADA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleEntidades.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_ENTIDAD_ASIGNADA = COD_ENTIDAD_ASIGNADA })
        };
    }
}

}