using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DocumentosOficialesPersonalRepository : IDocumentosOficialesPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public DocumentosOficialesPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DocumentosOficialesPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DocumentosOficialesPersonal>(SP.SCHEMARrhh.DocumentosOficialesPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DocumentosOficialesPersonal?> ShowbyidAsync(int NUM_DOCUMENTO_OFICIAL, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DocumentosOficialesPersonal>(SP.SCHEMARrhh.DocumentosOficialesPersonal.Showbyid, new { NUM_DOCUMENTO_OFICIAL = NUM_DOCUMENTO_OFICIAL, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.DocumentosOficialesPersonal.Showcombox, "NUM_DOCUMENTO_OFICIAL", "DES_COMENTARIOS");
    }

    public async Task<IEnumerable<DocumentosOficialesPersonal>> SearchAsync(DocumentosOficialesPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DocumentosOficialesPersonal>(SP.SCHEMARrhh.DocumentosOficialesPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DocumentosOficialesPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DOCUMENTO_OFICIAL", dto.NUM_DOCUMENTO_OFICIAL);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("DES_COMENTARIOS", dto.DES_COMENTARIOS);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DocumentosOficialesPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DocumentosOficialesPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DocumentosOficialesPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DocumentosOficialesPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DOCUMENTO_OFICIAL", dto.NUM_DOCUMENTO_OFICIAL);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("DES_COMENTARIOS", dto.DES_COMENTARIOS);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DocumentosOficialesPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DocumentosOficialesPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DocumentosOficialesPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_DOCUMENTO_OFICIAL, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DocumentosOficialesPersonal.Delete, new { NUM_DOCUMENTO_OFICIAL = NUM_DOCUMENTO_OFICIAL, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DocumentosOficialesPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DocumentosOficialesPersonal.Delete, new { NUM_DOCUMENTO_OFICIAL = NUM_DOCUMENTO_OFICIAL, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL })
        };
    }
}

}