using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposProyectoRepository : ITiposProyectoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposProyectoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposProyecto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposProyecto>(SP.SCHEMAProyectos.TiposProyecto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposProyecto?> ShowbyidAsync(string COD_TIPO_PROYECTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposProyecto>(SP.SCHEMAProyectos.TiposProyecto.Showbyid, new { COD_TIPO_PROYECTO = COD_TIPO_PROYECTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.TiposProyecto.Showcombox);
    }

    public async Task<IEnumerable<TiposProyecto>> SearchAsync(TiposProyectoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposProyecto>(SP.SCHEMAProyectos.TiposProyecto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposProyectoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PROYECTO", dto.COD_TIPO_PROYECTO);
        p.Add("DES_TIPO_PROYECTO", dto.DES_TIPO_PROYECTO);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_PRESUPUESTO", dto.FLG_PRESUPUESTO ?? false);
        p.Add("FLG_VALORADO", dto.FLG_VALORADO ?? false);
        p.Add("FLG_SERVICIO", dto.FLG_SERVICIO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.TiposProyecto.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAProyectos.TiposProyecto.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.TiposProyecto.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(TiposProyectoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PROYECTO", dto.COD_TIPO_PROYECTO);
        p.Add("DES_TIPO_PROYECTO", dto.DES_TIPO_PROYECTO);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        p.Add("FLG_PRESUPUESTO", dto.FLG_PRESUPUESTO ?? false);
        p.Add("FLG_VALORADO", dto.FLG_VALORADO ?? false);
        p.Add("FLG_SERVICIO", dto.FLG_SERVICIO ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.TiposProyecto.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAProyectos.TiposProyecto.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.TiposProyecto.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PROYECTO)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PROYECTO", COD_TIPO_PROYECTO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.TiposProyecto.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAProyectos.TiposProyecto.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.TiposProyecto.Delete, p)
            };
        }
    }
}
