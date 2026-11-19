using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosTiposEntidadRepository : IProyectosTiposEntidadRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosTiposEntidadRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProyectosTiposEntidad>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosTiposEntidad>(SP.SCHEMAProyectos.ProyectosTiposEntidad.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProyectosTiposEntidad?> ShowbyidAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProyectosTiposEntidad>(SP.SCHEMAProyectos.ProyectosTiposEntidad.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.ProyectosTiposEntidad.Showcombox);
    }

    public async Task<IEnumerable<ProyectosTiposEntidad>> SearchAsync(ProyectosTiposEntidadSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProyectosTiposEntidad>(SP.SCHEMAProyectos.ProyectosTiposEntidad.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosTiposEntidadInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosTiposEntidad.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosTiposEntidad.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosTiposEntidad.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO, string COD_TIPO_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.ProyectosTiposEntidad.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProyectos.ProyectosTiposEntidad.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.ProyectosTiposEntidad.Delete, new { NUM_PROYECTO = NUM_PROYECTO, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD })
        };
    }
}

}