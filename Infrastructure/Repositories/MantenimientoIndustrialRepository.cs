using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Mantenimiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MantenimientoIndustrialRepository : IMantenimientoIndustrialRepository
{
    private readonly IDbConnectionFactory _db;
    public MantenimientoIndustrialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MantenimientoIndustrial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrial>(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MantenimientoIndustrial?> ShowbyidAsync(int NUM_MANTENIMIENTO_INDUSTRIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MantenimientoIndustrial>(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Showbyid, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAMantenimiento.MantenimientoIndustrial.Showcombox, "NUM_MANTENIMIENTO_INDUSTRIAL", "DES_MANTENIMIENTO_INDUSTRIAL");
    }

    public async Task<IEnumerable<MantenimientoIndustrial>> SearchAsync(MantenimientoIndustrialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MantenimientoIndustrial>(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MantenimientoIndustrialInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("DES_MANTENIMIENTO_INDUSTRIAL", dto.DES_MANTENIMIENTO_INDUSTRIAL);
        p.Add("COD_TIPO_MANTENIMIENTO_INDUSTRIAL", dto.COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
        p.Add("FEC_INICIO_PROGRAMACION", dto.FEC_INICIO_PROGRAMACION);
        p.Add("FEC_FIN_PROGRAMACION", dto.FEC_FIN_PROGRAMACION);
        p.Add("FEC_INICIO_EJECUTADA", dto.FEC_INICIO_EJECUTADA);
        p.Add("FEC_FIN_EJECUTADA", dto.FEC_FIN_EJECUTADA);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MantenimientoIndustrialUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANTENIMIENTO_INDUSTRIAL", dto.NUM_MANTENIMIENTO_INDUSTRIAL);
        p.Add("DES_MANTENIMIENTO_INDUSTRIAL", dto.DES_MANTENIMIENTO_INDUSTRIAL);
        p.Add("COD_TIPO_MANTENIMIENTO_INDUSTRIAL", dto.COD_TIPO_MANTENIMIENTO_INDUSTRIAL);
        p.Add("FEC_INICIO_PROGRAMACION", dto.FEC_INICIO_PROGRAMACION);
        p.Add("FEC_FIN_PROGRAMACION", dto.FEC_FIN_PROGRAMACION);
        p.Add("FEC_INICIO_EJECUTADA", dto.FEC_INICIO_EJECUTADA);
        p.Add("FEC_FIN_EJECUTADA", dto.FEC_FIN_EJECUTADA);
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrial.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MANTENIMIENTO_INDUSTRIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAMantenimiento.MantenimientoIndustrial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAMantenimiento.MantenimientoIndustrial.Delete, new { NUM_MANTENIMIENTO_INDUSTRIAL = NUM_MANTENIMIENTO_INDUSTRIAL })
        };
    }
}

}