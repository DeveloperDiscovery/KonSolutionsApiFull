using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ControlCalidadProduccionRepository : IControlCalidadProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public ControlCalidadProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ControlCalidadProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadProduccion>(SP.SCHEMAProduccion.ControlCalidadProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ControlCalidadProduccion?> ShowbyidAsync(int NUM_CONTROL_CALIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ControlCalidadProduccion>(SP.SCHEMAProduccion.ControlCalidadProduccion.Showbyid, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.ControlCalidadProduccion.Showcombox, "NUM_CONTROL_CALIDAD", "DES_CONTROL_CALIDAD");
    }

    public async Task<IEnumerable<ControlCalidadProduccion>> SearchAsync(ControlCalidadProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadProduccion>(SP.SCHEMAProduccion.ControlCalidadProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ControlCalidadProduccionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTROL_CALIDAD", dto.NUM_CONTROL_CALIDAD);
        p.Add("DES_CONTROL_CALIDAD", dto.DES_CONTROL_CALIDAD);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_ENTIDAD_EVALUADOR", dto.COD_ENTIDAD_EVALUADOR);
        p.Add("COD_ENTIDAD_APROBADOR", dto.COD_ENTIDAD_APROBADOR);
        p.Add("NUM_APROBACION", dto.NUM_APROBACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ControlCalidadProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ControlCalidadProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ControlCalidadProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ControlCalidadProduccionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTROL_CALIDAD", dto.NUM_CONTROL_CALIDAD);
        p.Add("DES_CONTROL_CALIDAD", dto.DES_CONTROL_CALIDAD);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_ENTIDAD_EVALUADOR", dto.COD_ENTIDAD_EVALUADOR);
        p.Add("COD_ENTIDAD_APROBADOR", dto.COD_ENTIDAD_APROBADOR);
        p.Add("NUM_APROBACION", dto.NUM_APROBACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ControlCalidadProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ControlCalidadProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ControlCalidadProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ControlCalidadProduccion.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ControlCalidadProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ControlCalidadProduccion.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD })
        };
    }
}

}