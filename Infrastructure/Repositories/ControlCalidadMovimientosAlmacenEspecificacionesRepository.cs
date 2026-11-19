using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ControlCalidadMovimientosAlmacenEspecificacionesRepository : IControlCalidadMovimientosAlmacenEspecificacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public ControlCalidadMovimientosAlmacenEspecificacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ControlCalidadMovimientosAlmacenEspecificaciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadMovimientosAlmacenEspecificaciones>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ControlCalidadMovimientosAlmacenEspecificaciones?> ShowbyidAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ControlCalidadMovimientosAlmacenEspecificaciones>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Showbyid, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Showcombox, "NUM_CONTROL_CALIDAD", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<ControlCalidadMovimientosAlmacenEspecificaciones>> SearchAsync(ControlCalidadMovimientosAlmacenEspecificacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadMovimientosAlmacenEspecificaciones>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ControlCalidadMovimientosAlmacenEspecificacionesInsertDto dto, string codUsuario, string codEstacion)
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
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ControlCalidadMovimientosAlmacenEspecificacionesUpdateDto dto, string codUsuario, string codEstacion)
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
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD, string COD_ESPECIFICACION_TECNICA, int COD_ITEM_TECNICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacenEspecificaciones.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA, COD_ITEM_TECNICO = COD_ITEM_TECNICO })
        };
    }
}

}