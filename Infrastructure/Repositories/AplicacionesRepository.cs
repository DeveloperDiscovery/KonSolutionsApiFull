using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AplicacionesRepository : IAplicacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public AplicacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Aplicaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Aplicaciones>(SP.SCHEMASeguridad.Aplicaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Aplicaciones?> ShowbyidAsync(string COD_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Aplicaciones>(SP.SCHEMASeguridad.Aplicaciones.Showbyid, new { COD_APLICACION = COD_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.Aplicaciones.Showcombox);
    }

    public async Task<IEnumerable<Aplicaciones>> SearchAsync(AplicacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Aplicaciones>(SP.SCHEMASeguridad.Aplicaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AplicacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("DES_APLICACION", dto.DES_APLICACION);
        p.Add("FLG_APP_MOVIL", dto.FLG_APP_MOVIL);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("DES_BACKCOLOR", dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR", dto.DES_FORECOLOR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.Aplicaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.Aplicaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.Aplicaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AplicacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("DES_APLICACION", dto.DES_APLICACION);
        p.Add("FLG_APP_MOVIL", dto.FLG_APP_MOVIL);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("DES_BACKCOLOR", dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR", dto.DES_FORECOLOR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.Aplicaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.Aplicaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.Aplicaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_APLICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.Aplicaciones.Delete, new { COD_APLICACION = COD_APLICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.Aplicaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.Aplicaciones.Delete, new { COD_APLICACION = COD_APLICACION })
        };
    }
}

}