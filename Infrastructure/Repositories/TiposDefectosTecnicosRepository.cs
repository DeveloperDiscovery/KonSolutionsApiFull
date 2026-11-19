using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDefectosTecnicosRepository : ITiposDefectosTecnicosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDefectosTecnicosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDefectosTecnicos>> ShowallAsync(string COD_CLASE_DEFECTO_TECNICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDefectosTecnicos>(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Showall, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDefectosTecnicos?> ShowbyidAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDefectosTecnicos>(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Showbyid, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO = COD_TIPO_DEFECTO_TECNICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Showcombox);
    }

    public async Task<IEnumerable<TiposDefectosTecnicos>> SearchAsync(TiposDefectosTecnicosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDefectosTecnicos>(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDefectosTecnicosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_DEFECTO_TECNICO", dto.DES_DEFECTO_TECNICO);
        p.Add("FLG_OBSERVACION", dto.FLG_OBSERVACION ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposDefectosTecnicosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_DEFECTO_TECNICO", dto.DES_DEFECTO_TECNICO);
        p.Add("FLG_OBSERVACION", dto.FLG_OBSERVACION ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO, string COD_TIPO_DEFECTO_TECNICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Delete, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO = COD_TIPO_DEFECTO_TECNICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.TiposDefectosTecnicos.Delete, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO, COD_TIPO_DEFECTO_TECNICO = COD_TIPO_DEFECTO_TECNICO })
        };
    }
}

}