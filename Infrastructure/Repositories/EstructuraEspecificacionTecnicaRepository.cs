using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstructuraEspecificacionTecnicaRepository : IEstructuraEspecificacionTecnicaRepository
{
    private readonly IDbConnectionFactory _db;
    public EstructuraEspecificacionTecnicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EstructuraEspecificacionTecnica>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraEspecificacionTecnica>(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EstructuraEspecificacionTecnica?> ShowbyidAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EstructuraEspecificacionTecnica>(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Showbyid, new { NUM_ESTRUCTURA_ESPECIFICACION_TECNICA = NUM_ESTRUCTURA_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Showcombox);
    }

    public async Task<IEnumerable<EstructuraEspecificacionTecnica>> SearchAsync(EstructuraEspecificacionTecnicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EstructuraEspecificacionTecnica>(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EstructuraEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ESTRUCTURA_ESPECIFICACION_TECNICA", dto.NUM_ESTRUCTURA_ESPECIFICACION_TECNICA);
        p.Add("COD_ESTRUCTURA_ESPECIFICACION_TECNICA", dto.COD_ESTRUCTURA_ESPECIFICACION_TECNICA);
        p.Add("DES_ESTRUCTURA_ESPECIFICACION_TECNICA", dto.DES_ESTRUCTURA_ESPECIFICACION_TECNICA);
        p.Add("COD_TABLA_ESPECIFICACION_TECNICA", dto.COD_TABLA_ESPECIFICACION_TECNICA);
        p.Add("NUM_ESTRUCTURA_ESPECIFICACION_TECNICA_PADRE", dto.NUM_ESTRUCTURA_ESPECIFICACION_TECNICA_PADRE);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ESTRUCTURA_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Delete, new { NUM_ESTRUCTURA_ESPECIFICACION_TECNICA = NUM_ESTRUCTURA_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EstructuraEspecificacionTecnica.Delete, new { NUM_ESTRUCTURA_ESPECIFICACION_TECNICA = NUM_ESTRUCTURA_ESPECIFICACION_TECNICA })
        };
    }
}

}