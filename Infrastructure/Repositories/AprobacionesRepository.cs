using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Aprobaciones;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AprobacionesRepository : IAprobacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public AprobacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Aprobaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Aprobaciones>(SP.SCHEMAAprobaciones.Aprobaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Aprobaciones?> ShowbyidAsync(int NUM_APROBACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Aprobaciones>(SP.SCHEMAAprobaciones.Aprobaciones.Showbyid, new { NUM_APROBACION = NUM_APROBACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAAprobaciones.Aprobaciones.Showcombox, "NUM_APROBACION", "DES_APROBACION");
    }

    public async Task<IEnumerable<Aprobaciones>> SearchAsync(AprobacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Aprobaciones>(SP.SCHEMAAprobaciones.Aprobaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AprobacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_APROBACION", dto.NUM_APROBACION);
        p.Add("DES_APROBACION", dto.DES_APROBACION);
        p.Add("FEC_SOLICITADO", dto.FEC_SOLICITADO);
        p.Add("FEC_EVALUACION_MAXIMA", dto.FEC_EVALUACION_MAXIMA);
        p.Add("FEC_EVALUACION_EFECTUADA", dto.FEC_EVALUACION_EFECTUADA);
        p.Add("COD_ENTIDAD_DESARROLLADORA", dto.COD_ENTIDAD_DESARROLLADORA);
        p.Add("COD_ENTIDAD_EVALUADORA", dto.COD_ENTIDAD_EVALUADORA);
        p.Add("COD_TIPO_APROBACION", dto.COD_TIPO_APROBACION);
        p.Add("NUM_SOLICITUD", dto.NUM_SOLICITUD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.Aprobaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.Aprobaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.Aprobaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo juego de parámetros que el alta: el SP _UPDATE se
    /// genera con el mismo contrato que el _INSERT.</summary>
    public async Task<WriteResult> UpdateAsync(AprobacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_APROBACION", dto.NUM_APROBACION);
        p.Add("DES_APROBACION", dto.DES_APROBACION);
        p.Add("FEC_SOLICITADO", dto.FEC_SOLICITADO);
        p.Add("FEC_EVALUACION_MAXIMA", dto.FEC_EVALUACION_MAXIMA);
        p.Add("FEC_EVALUACION_EFECTUADA", dto.FEC_EVALUACION_EFECTUADA);
        p.Add("COD_ENTIDAD_DESARROLLADORA", dto.COD_ENTIDAD_DESARROLLADORA);
        p.Add("COD_ENTIDAD_EVALUADORA", dto.COD_ENTIDAD_EVALUADORA);
        p.Add("COD_TIPO_APROBACION", dto.COD_TIPO_APROBACION);
        p.Add("NUM_SOLICITUD", dto.NUM_SOLICITUD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.Aprobaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.Aprobaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.Aprobaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_APROBACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.Aprobaciones.Delete, new { NUM_APROBACION = NUM_APROBACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.Aprobaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.Aprobaciones.Delete, new { NUM_APROBACION = NUM_APROBACION })
        };
    }
}

}