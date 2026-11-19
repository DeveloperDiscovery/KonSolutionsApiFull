using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DocumentosOficialesRepository : IDocumentosOficialesRepository
{
    private readonly IDbConnectionFactory _db;
    public DocumentosOficialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DocumentosOficiales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DocumentosOficiales>(SP.SCHEMARrhh.DocumentosOficiales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DocumentosOficiales?> ShowbyidAsync(int NUM_DOCUMENTO_OFICIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DocumentosOficiales>(SP.SCHEMARrhh.DocumentosOficiales.Showbyid, new { NUM_DOCUMENTO_OFICIAL = NUM_DOCUMENTO_OFICIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.DocumentosOficiales.Showcombox, "NUM_DOCUMENTO_OFICIAL", "DES_DOCUMENTO_OFICIAL");
    }

    public async Task<IEnumerable<DocumentosOficiales>> SearchAsync(DocumentosOficialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DocumentosOficiales>(SP.SCHEMARrhh.DocumentosOficiales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DocumentosOficialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DOCUMENTO_OFICIAL", dto.NUM_DOCUMENTO_OFICIAL);
        p.Add("DES_DOCUMENTO_OFICIAL", dto.DES_DOCUMENTO_OFICIAL);
        p.Add("DES_FORMATO_DOCUMENTO", dto.DES_FORMATO_DOCUMENTO);
        p.Add("FEC_DOCUMENTO", dto.FEC_DOCUMENTO);
        p.Add("COD_TIPO_DOCUMENTO_OFICIAL", dto.COD_TIPO_DOCUMENTO_OFICIAL);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DocumentosOficiales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DocumentosOficiales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DocumentosOficiales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DocumentosOficialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DOCUMENTO_OFICIAL", dto.NUM_DOCUMENTO_OFICIAL);
        p.Add("DES_DOCUMENTO_OFICIAL", dto.DES_DOCUMENTO_OFICIAL);
        p.Add("DES_FORMATO_DOCUMENTO", dto.DES_FORMATO_DOCUMENTO);
        p.Add("FEC_DOCUMENTO", dto.FEC_DOCUMENTO);
        p.Add("COD_TIPO_DOCUMENTO_OFICIAL", dto.COD_TIPO_DOCUMENTO_OFICIAL);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DocumentosOficiales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DocumentosOficiales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DocumentosOficiales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_DOCUMENTO_OFICIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DocumentosOficiales.Delete, new { NUM_DOCUMENTO_OFICIAL = NUM_DOCUMENTO_OFICIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DocumentosOficiales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DocumentosOficiales.Delete, new { NUM_DOCUMENTO_OFICIAL = NUM_DOCUMENTO_OFICIAL })
        };
    }
}

}