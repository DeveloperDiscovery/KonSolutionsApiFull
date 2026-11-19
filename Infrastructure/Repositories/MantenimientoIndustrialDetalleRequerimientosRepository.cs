using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MantenimientoIndustrialDetalleRequerimientosRepository : IMantenimientoIndustrialDetalleRequerimientosRepository
{
    private readonly IDbConnectionFactory _db;
    public MantenimientoIndustrialDetalleRequerimientosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalleRequerimientos>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MantenimientoIndustrialDetalleRequerimientos?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MantenimientoIndustrialDetalleRequerimientos>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Showbyid, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Showcombox, "NUM_MANTENIMIENTO_INDUSTRIAL", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<MantenimientoIndustrialDetalleRequerimientos>> SearchAsync(MantenimientoIndustrialDetalleRequerimientosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalleRequerimientos>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleRequerimientosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_REQUERIDO", dto.CAN_REQUERIDO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MantenimientoIndustrialDetalleRequerimientosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_REQUERIDO", dto.CAN_REQUERIDO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalleRequerimientos.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}