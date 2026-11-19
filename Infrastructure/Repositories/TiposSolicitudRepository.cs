using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Solicitudes;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposSolicitudRepository : ITiposSolicitudRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposSolicitudRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposSolicitud>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitud>(SP.SCHEMASolicitudes.TiposSolicitud.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposSolicitud?> ShowbyidAsync(string COD_TIPO_SOLICITUD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposSolicitud>(SP.SCHEMASolicitudes.TiposSolicitud.Showbyid, new { COD_TIPO_SOLICITUD = COD_TIPO_SOLICITUD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASolicitudes.TiposSolicitud.Showcombox);
    }

    public async Task<IEnumerable<TiposSolicitud>> SearchAsync(TiposSolicitudSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitud>(SP.SCHEMASolicitudes.TiposSolicitud.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposSolicitudInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD", dto.COD_TIPO_SOLICITUD);
        p.Add("DES_TIPO_SOLICITUD", dto.DES_TIPO_SOLICITUD);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.TiposSolicitud.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.TiposSolicitud.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.TiposSolicitud.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposSolicitudUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD", dto.COD_TIPO_SOLICITUD);
        p.Add("DES_TIPO_SOLICITUD", dto.DES_TIPO_SOLICITUD);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.TiposSolicitud.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.TiposSolicitud.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.TiposSolicitud.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_SOLICITUD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASolicitudes.TiposSolicitud.Delete, new { COD_TIPO_SOLICITUD = COD_TIPO_SOLICITUD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASolicitudes.TiposSolicitud.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASolicitudes.TiposSolicitud.Delete, new { COD_TIPO_SOLICITUD = COD_TIPO_SOLICITUD })
        };
    }
}

}