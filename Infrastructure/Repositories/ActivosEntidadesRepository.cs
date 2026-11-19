using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ActivosEntidadesRepository : IActivosEntidadesRepository
{
    private readonly IDbConnectionFactory _db;
    public ActivosEntidadesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ActivosEntidades>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosEntidades>(SP.SCHEMAActivos.ActivosEntidades.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ActivosEntidades?> ShowbyidAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ActivosEntidades>(SP.SCHEMAActivos.ActivosEntidades.Showbyid, new { COD_ACTIVO = COD_ACTIVO, COD_ENTIDAD = COD_ENTIDAD, FEC_ASIGNACION_INICIO = FEC_ASIGNACION_INICIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAActivos.ActivosEntidades.Showcombox);
    }

    public async Task<IEnumerable<ActivosEntidades>> SearchAsync(ActivosEntidadesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosEntidades>(SP.SCHEMAActivos.ActivosEntidades.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ActivosEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FEC_ASIGNACION_INICIO", dto.FEC_ASIGNACION_INICIO);
        p.Add("FEC_ASIGNACION_FINAL", dto.FEC_ASIGNACION_FINAL);
        p.Add("FLG_PORTABLE", dto.FLG_PORTABLE ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ActivosEntidades.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ActivosEntidades.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ActivosEntidades.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ActivosEntidadesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FEC_ASIGNACION_INICIO", dto.FEC_ASIGNACION_INICIO);
        p.Add("FEC_ASIGNACION_FINAL", dto.FEC_ASIGNACION_FINAL);
        p.Add("FLG_PORTABLE", dto.FLG_PORTABLE ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ActivosEntidades.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ActivosEntidades.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ActivosEntidades.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ACTIVO, decimal COD_ENTIDAD, DateTime FEC_ASIGNACION_INICIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ActivosEntidades.Delete, new { COD_ACTIVO = COD_ACTIVO, COD_ENTIDAD = COD_ENTIDAD, FEC_ASIGNACION_INICIO = FEC_ASIGNACION_INICIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ActivosEntidades.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ActivosEntidades.Delete, new { COD_ACTIVO = COD_ACTIVO, COD_ENTIDAD = COD_ENTIDAD, FEC_ASIGNACION_INICIO = FEC_ASIGNACION_INICIO })
        };
    }
}

}