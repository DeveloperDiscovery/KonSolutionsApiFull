using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDocumentoOficialRepository : ITiposDocumentoOficialRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDocumentoOficialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDocumentoOficial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoOficial>(SP.SCHEMARrhh.TiposDocumentoOficial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDocumentoOficial?> ShowbyidAsync(string COD_TIPO_DOCUMENTO_OFICIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDocumentoOficial>(SP.SCHEMARrhh.TiposDocumentoOficial.Showbyid, new { COD_TIPO_DOCUMENTO_OFICIAL = COD_TIPO_DOCUMENTO_OFICIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposDocumentoOficial.Showcombox);
    }

    public async Task<IEnumerable<TiposDocumentoOficial>> SearchAsync(TiposDocumentoOficialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoOficial>(SP.SCHEMARrhh.TiposDocumentoOficial.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDocumentoOficialInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_OFICIAL", dto.COD_TIPO_DOCUMENTO_OFICIAL);
        p.Add("DES_TIPO_DOCUMENTO_OFICIAL", dto.DES_TIPO_DOCUMENTO_OFICIAL);
        p.Add("FLG_VISUALIZACION_INTERNA", dto.FLG_VISUALIZACION_INTERNA ?? false);
        p.Add("FLG_ENVIAR_DOCUMENTO_CORREO", dto.FLG_ENVIAR_DOCUMENTO_CORREO ?? false);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposDocumentoOficial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposDocumentoOficial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposDocumentoOficial.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposDocumentoOficialUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_OFICIAL", dto.COD_TIPO_DOCUMENTO_OFICIAL);
        p.Add("DES_TIPO_DOCUMENTO_OFICIAL", dto.DES_TIPO_DOCUMENTO_OFICIAL);
        p.Add("FLG_VISUALIZACION_INTERNA", dto.FLG_VISUALIZACION_INTERNA ?? false);
        p.Add("FLG_ENVIAR_DOCUMENTO_CORREO", dto.FLG_ENVIAR_DOCUMENTO_CORREO ?? false);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposDocumentoOficial.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposDocumentoOficial.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposDocumentoOficial.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_DOCUMENTO_OFICIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposDocumentoOficial.Delete, new { COD_TIPO_DOCUMENTO_OFICIAL = COD_TIPO_DOCUMENTO_OFICIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposDocumentoOficial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposDocumentoOficial.Delete, new { COD_TIPO_DOCUMENTO_OFICIAL = COD_TIPO_DOCUMENTO_OFICIAL })
        };
    }
}

}