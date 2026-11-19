using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Aprobaciones;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AprobacionesDetalleRepository : IAprobacionesDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public AprobacionesDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AprobacionesDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AprobacionesDetalle>(SP.SCHEMAAprobaciones.AprobacionesDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AprobacionesDetalle?> ShowbyidAsync(int NUM_APROBACION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AprobacionesDetalle>(SP.SCHEMAAprobaciones.AprobacionesDetalle.Showbyid, new { NUM_APROBACION = NUM_APROBACION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAAprobaciones.AprobacionesDetalle.Showcombox, "NUM_APROBACION", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<AprobacionesDetalle>> SearchAsync(AprobacionesDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AprobacionesDetalle>(SP.SCHEMAAprobaciones.AprobacionesDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AprobacionesDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_APROBACION", dto.NUM_APROBACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("FEC_EVALUACION_MAXIMA", dto.FEC_EVALUACION_MAXIMA);
        p.Add("FEC_EVALUACION_EFECTUADA", dto.FEC_EVALUACION_EFECTUADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.AprobacionesDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.AprobacionesDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.AprobacionesDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo juego de parámetros que el alta: el SP _UPDATE se
    /// genera con el mismo contrato que el _INSERT.</summary>
    public async Task<WriteResult> UpdateAsync(AprobacionesDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_APROBACION", dto.NUM_APROBACION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("FEC_EVALUACION_MAXIMA", dto.FEC_EVALUACION_MAXIMA);
        p.Add("FEC_EVALUACION_EFECTUADA", dto.FEC_EVALUACION_EFECTUADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.AprobacionesDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.AprobacionesDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.AprobacionesDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_APROBACION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAAprobaciones.AprobacionesDetalle.Delete, new { NUM_APROBACION = NUM_APROBACION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAAprobaciones.AprobacionesDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAAprobaciones.AprobacionesDetalle.Delete, new { NUM_APROBACION = NUM_APROBACION, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}