using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AplicacionesOpcionesAreasRepository : IAplicacionesOpcionesAreasRepository
{
    private readonly IDbConnectionFactory _db;
    public AplicacionesOpcionesAreasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AplicacionesOpcionesAreas>> ShowallAsync(string COD_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpcionesAreas>(
            SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Showall,
            new { COD_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<AplicacionesOpcionesAreas?> ShowbyidAsync(string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AplicacionesOpcionesAreas>(
            SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Showbyid,
            new { COD_APLICACION, COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AplicacionesOpcionesAreas>> SearchAsync(AplicacionesOpcionesAreasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpcionesAreas>(
            SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Search,
            new { COD_APLICACION = dto.COD_APLICACION, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AplicacionesOpcionesAreasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", COD_OPCION_APLICACION);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.AplicacionesOpcionesAreas.Delete, p)
            };
        }
    }
}
