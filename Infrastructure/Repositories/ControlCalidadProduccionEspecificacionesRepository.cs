using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ControlCalidadProduccionEspecificacionesRepository : IControlCalidadProduccionEspecificacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public ControlCalidadProduccionEspecificacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ControlCalidadProduccionEspecificaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadProduccionEspecificaciones>(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ControlCalidadProduccionEspecificaciones?> ShowbyidAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ControlCalidadProduccionEspecificaciones>(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Showbyid, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Showcombox);
    }

    public async Task<IEnumerable<ControlCalidadProduccionEspecificaciones>> SearchAsync(ControlCalidadProduccionEspecificacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadProduccionEspecificaciones>(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ControlCalidadProduccionEspecificacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTROL_CALIDAD", dto.NUM_CONTROL_CALIDAD);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPACIFICACION_TENICA", dto.VAL_ESPACIFICACION_TENICA);
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ControlCalidadProduccionEspecificacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTROL_CALIDAD", dto.NUM_CONTROL_CALIDAD);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPACIFICACION_TENICA", dto.VAL_ESPACIFICACION_TENICA);
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadProduccionEspecificaciones.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO })
        };
    }
}

}