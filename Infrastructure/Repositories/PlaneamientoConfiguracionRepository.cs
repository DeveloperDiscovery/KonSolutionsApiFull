using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PlaneamientoConfiguracionRepository : IPlaneamientoConfiguracionRepository
{
    private readonly IDbConnectionFactory _db;
    public PlaneamientoConfiguracionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PlaneamientoConfiguracion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlaneamientoConfiguracion>(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PlaneamientoConfiguracion?> ShowbyidAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PlaneamientoConfiguracion>(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Showbyid, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Showcombox);
    }

    public async Task<IEnumerable<PlaneamientoConfiguracion>> SearchAsync(PlaneamientoConfiguracionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlaneamientoConfiguracion>(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PlaneamientoConfiguracionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA", dto.COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA);
        p.Add("COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD", dto.COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PlaneamientoConfiguracionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA", dto.COD_ESPECIFICACION_TECNICA_CAPACIDAD_MAQUINA);
        p.Add("COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD", dto.COD_UNIDAD_MEDIDA_DIVISORA_CAPACIDAD);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.PlaneamientoConfiguracion.Delete, new { NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}