using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository : IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposFamiliasSubFamiliasGruposEspecificacionTecnica?> ShowbyidAsync(int COD_GRUPO_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Showbyid, new { COD_GRUPO_ESPECIFICACION_TECNICA = COD_GRUPO_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Showcombox);
    }

    public async Task<IEnumerable<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>> SearchAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposFamiliasSubFamiliasGruposEspecificacionTecnica>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposFamiliasSubFamiliasGruposEspecificacionTecnicaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        // COD_GRUPO_ESPECIFICACION_TECNICA es IDENTITY — en el intento de UPDATE del SP se usa
        // para buscar el registro existente; si no matchea ninguno, el SP hace el INSERT
        // ignorando este valor y devuelve el código real generado por SCOPE_IDENTITY() en una
        // sola columna (no en la forma estándar de WriteResult), por eso se lee aparte abajo.
        p.Add("COD_GRUPO_ESPECIFICACION_TECNICA", dto.COD_GRUPO_ESPECIFICACION_TECNICA);
        p.Add("DES_GRUPO_ESPECIFICACION_TECNICA", dto.DES_GRUPO_ESPECIFICACION_TECNICA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        var fila = await conn.QueryFirstOrDefaultAsync(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Insert, p, commandType: CommandType.StoredProcedure);
        if (fila is null) return new WriteResult { Estado = "OK", AffectedRows = 1 }; // fue UPDATE, sin SELECT de retorno
        var d = (IDictionary<string, object>)fila;
        var nuevoCod = d.TryGetValue("COD_GRUPO_ESPECIFICACION_TECNICA", out var v) ? v?.ToString() : null;
        return new WriteResult { Estado = "OK", AffectedRows = 1, Mensaje = nuevoCod ?? "" };
    }

    public async Task<WriteResult> DeleteAsync(int COD_GRUPO_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Delete, new { COD_GRUPO_ESPECIFICACION_TECNICA = COD_GRUPO_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.GruposFamiliasSubFamiliasGruposEspecificacionTecnica.Delete, new { COD_GRUPO_ESPECIFICACION_TECNICA = COD_GRUPO_ESPECIFICACION_TECNICA })
        };
    }
}

}