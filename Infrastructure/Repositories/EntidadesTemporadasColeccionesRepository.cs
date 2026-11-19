using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesTemporadasColeccionesRepository : IEntidadesTemporadasColeccionesRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesTemporadasColeccionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesTemporadasColecciones>> ShowallAsync(decimal codEntidad, int numTemporada)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTemporadasColecciones>(
            SP.SCHEMAComercial.EntidadesTemporadasColecciones.Showall,
            new { COD_ENTIDAD = codEntidad, NUM_TEMPORADA = numTemporada }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesTemporadasColecciones?> ShowbyidAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesTemporadasColecciones>(
            SP.SCHEMAComercial.EntidadesTemporadasColecciones.Showbyid,
            new { COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal codEntidad, int numTemporada)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesTemporadasColecciones.Showcombox, new { COD_ENTIDAD = codEntidad, NUM_TEMPORADA = numTemporada });
    }

    public async Task<IEnumerable<EntidadesTemporadasColecciones>> SearchAsync(EntidadesTemporadasColeccionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTemporadasColecciones>(
            SP.SCHEMAComercial.EntidadesTemporadasColecciones.Search,
            new { dto.COD_ENTIDAD, dto.NUM_TEMPORADA, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesTemporadasColeccionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_TEMPORADA", dto.NUM_TEMPORADA);
        p.Add("NUM_COLECCION", dto.NUM_COLECCION);
        p.Add("DES_COLECCION", dto.DES_COLECCION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesTemporadasColecciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesTemporadasColecciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesTemporadasColecciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAComercial.EntidadesTemporadasColecciones.Delete,
            new { COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesTemporadasColecciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesTemporadasColecciones.Delete, new { COD_ENTIDAD, NUM_TEMPORADA, NUM_COLECCION })
        };
    }
}
}
