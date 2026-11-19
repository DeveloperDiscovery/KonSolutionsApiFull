using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MantenimientoIndustrialDetalleRepository : IMantenimientoIndustrialDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public MantenimientoIndustrialDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MantenimientoIndustrialDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalle>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MantenimientoIndustrialDetalle?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MantenimientoIndustrialDetalle>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Showbyid, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Showcombox, "NUM_MANTENIMIENTO_INDUSTRIAL", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<MantenimientoIndustrialDetalle>> SearchAsync(MantenimientoIndustrialDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrialDetalle>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MantenimientoIndustrialDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ACTIVO_MAQUINA", dto.COD_ACTIVO_MAQUINA);
        p.Add("FEC_INICIO_PROGRAMACION", dto.FEC_INICIO_PROGRAMACION);
        p.Add("FEC_FIN_PROGRAMACION", dto.FEC_FIN_PROGRAMACION);
        p.Add("FEC_INICIO_EJECUTADA", dto.FEC_INICIO_EJECUTADA);
        p.Add("FEC_FIN_EJECUTADA", dto.FEC_FIN_EJECUTADA);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MantenimientoIndustrialDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ACTIVO_MAQUINA", dto.COD_ACTIVO_MAQUINA);
        p.Add("FEC_INICIO_PROGRAMACION", dto.FEC_INICIO_PROGRAMACION);
        p.Add("FEC_FIN_PROGRAMACION", dto.FEC_FIN_PROGRAMACION);
        p.Add("FEC_INICIO_EJECUTADA", dto.FEC_INICIO_EJECUTADA);
        p.Add("FEC_FIN_EJECUTADA", dto.FEC_FIN_EJECUTADA);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrialDetalle.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}