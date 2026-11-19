using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposSolicitudComercialAreasRepository : ITiposSolicitudComercialAreasRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposSolicitudComercialAreasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposSolicitudComercialAreas>> ShowallAsync(string codTipoSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitudComercialAreas>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreas.Showall,
            new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposSolicitudComercialAreas?> ShowbyidAsync(string codTipoSolicitudComercial, int codArea)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposSolicitudComercialAreas>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreas.Showbyid,
            new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial, COD_AREA = codArea },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposSolicitudComercialAreas>> ShowcomboxAsync(string codTipoSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitudComercialAreas>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreas.Showcombox,
            new { COD_TIPO_SOLICITUD_COMERICAL = codTipoSolicitudComercial },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposSolicitudComercialAreas>> SearchAsync(TiposSolicitudComercialAreasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSolicitudComercialAreas>(
            SP.SCHEMAComercial.TiposSolicitudComercialAreas.Search,
            new { COD_TIPO_SOLICITUD_COMERICAL = dto.COD_TIPO_SOLICITUD_COMERICAL, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposSolicitudComercialAreasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD_COMERICAL", dto.COD_TIPO_SOLICITUD_COMERICAL);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.TiposSolicitudComercialAreas.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.TiposSolicitudComercialAreas.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposSolicitudComercialAreas.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codTipoSolicitudComercial, int codArea)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SOLICITUD_COMERICAL", codTipoSolicitudComercial);
        p.Add("COD_AREA", codArea);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.TiposSolicitudComercialAreas.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.TiposSolicitudComercialAreas.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposSolicitudComercialAreas.Delete, p)
            };
        }
    }
}
