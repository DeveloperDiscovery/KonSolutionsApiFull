using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDocumentoOficialFormatosRepository : ITiposDocumentoOficialFormatosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDocumentoOficialFormatosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDocumentoOficialFormatos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoOficialFormatos>(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDocumentoOficialFormatos?> ShowbyidAsync(int NUM_FORMATO_DOCUMENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDocumentoOficialFormatos>(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Showbyid, new { NUM_FORMATO_DOCUMENTO = NUM_FORMATO_DOCUMENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Showcombox, "NUM_FORMATO_DOCUMENTO", "DES_FORMATO_DOCUMENTO");
    }

    public async Task<IEnumerable<TiposDocumentoOficialFormatos>> SearchAsync(TiposDocumentoOficialFormatosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoOficialFormatos>(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDocumentoOficialFormatosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FORMATO_DOCUMENTO", dto.NUM_FORMATO_DOCUMENTO);
        p.Add("DES_FORMATO_DOCUMENTO", dto.DES_FORMATO_DOCUMENTO);
        p.Add("COD_TIPO_DOCUMENTO_OFICIAL", dto.COD_TIPO_DOCUMENTO_OFICIAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposDocumentoOficialFormatosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FORMATO_DOCUMENTO", dto.NUM_FORMATO_DOCUMENTO);
        p.Add("DES_FORMATO_DOCUMENTO", dto.DES_FORMATO_DOCUMENTO);
        p.Add("COD_TIPO_DOCUMENTO_OFICIAL", dto.COD_TIPO_DOCUMENTO_OFICIAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_FORMATO_DOCUMENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Delete, new { NUM_FORMATO_DOCUMENTO = NUM_FORMATO_DOCUMENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposDocumentoOficialFormatos.Delete, new { NUM_FORMATO_DOCUMENTO = NUM_FORMATO_DOCUMENTO })
        };
    }
}

}