using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposSolicitudComercialRepository : ITiposSolicitudComercialRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposSolicitudComercialRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposSolicitudComercial>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitudComercial>(SP.SCHEMAComercial.TiposSolicitudComercial.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposSolicitudComercial?> ShowbyidAsync(string codTipoSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposSolicitudComercial>(SP.SCHEMAComercial.TiposSolicitudComercial.Showbyid, new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposSolicitudComercial.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(TiposSolicitudComercialInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD_COMERICAL", dto.COD_TIPO_SOLICITUD_COMERICAL);
        p.Add("DES_TIPO_SOLICITUD_COMERICAL", dto.DES_TIPO_SOLICITUD_COMERICAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposSolicitudComercial.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposSolicitudComercial.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposSolicitudComercial.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codTipoSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposSolicitudComercial.Delete, new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposSolicitudComercial.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposSolicitudComercial.Delete, new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial })
        };
    }
}
}
