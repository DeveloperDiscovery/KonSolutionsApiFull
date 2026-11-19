using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProgramacionLineasProduccionRepository : IProgramacionLineasProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public ProgramacionLineasProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProgramacionLineasProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionLineasProduccion>(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProgramacionLineasProduccion?> ShowbyidAsync(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProgramacionLineasProduccion>(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Showbyid, new { FEC_PROGRAMACION = FEC_PROGRAMACION, COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Showcombox);
    }

    public async Task<IEnumerable<ProgramacionLineasProduccion>> SearchAsync(ProgramacionLineasProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionLineasProduccion>(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProgramacionLineasProduccionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("FEC_PROGRAMACION", dto.FEC_PROGRAMACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProgramacionLineasProduccionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("FEC_PROGRAMACION", dto.FEC_PROGRAMACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(DateTime FEC_PROGRAMACION, decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Delete, new { FEC_PROGRAMACION = FEC_PROGRAMACION, COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionLineasProduccion.Delete, new { FEC_PROGRAMACION = FEC_PROGRAMACION, COD_ENTIDAD = COD_ENTIDAD })
        };
    }
}

}