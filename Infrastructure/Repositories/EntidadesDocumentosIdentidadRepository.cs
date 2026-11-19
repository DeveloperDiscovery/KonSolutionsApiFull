using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesDocumentosIdentidadRepository : IEntidadesDocumentosIdentidadRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesDocumentosIdentidadRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesDocumentosIdentidad>> ShowallAsync(decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesDocumentosIdentidad>(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Showall, new { COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesDocumentosIdentidad?> ShowbyidAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesDocumentosIdentidad>(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD = COD_TIPO_DOCUMENTO_IDENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Showcombox);
    }

    public async Task<IEnumerable<EntidadesDocumentosIdentidad>> SearchAsync(EntidadesDocumentosIdentidadSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesDocumentosIdentidad>(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Search, new { COD_ENTIDAD = dto.COD_ENTIDAD, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesDocumentosIdentidadInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("NUM_DOCUMENTO_IDENTIDAD", dto.NUM_DOCUMENTO_IDENTIDAD);
        p.Add("FEC_CADUCIDAD", dto.FEC_CADUCIDAD);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesDocumentosIdentidadUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("NUM_DOCUMENTO_IDENTIDAD", dto.NUM_DOCUMENTO_IDENTIDAD);
        p.Add("FEC_CADUCIDAD", dto.FEC_CADUCIDAD);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD = COD_TIPO_DOCUMENTO_IDENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EntidadesDocumentosIdentidad.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_DOCUMENTO_IDENTIDAD = COD_TIPO_DOCUMENTO_IDENTIDAD })
        };
    }
}

}