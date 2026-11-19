using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ControlCalidadMovimientosAlmacenRepository : IControlCalidadMovimientosAlmacenRepository
{
    private readonly IDbConnectionFactory _db;
    public ControlCalidadMovimientosAlmacenRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ControlCalidadMovimientosAlmacen>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadMovimientosAlmacen>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ControlCalidadMovimientosAlmacen?> ShowbyidAsync(int NUM_CONTROL_CALIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ControlCalidadMovimientosAlmacen>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Showbyid, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Showcombox, "NUM_CONTROL_CALIDAD", "DES_AUTORIZACION");
    }

    public async Task<IEnumerable<ControlCalidadMovimientosAlmacen>> SearchAsync(ControlCalidadMovimientosAlmacenSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ControlCalidadMovimientosAlmacen>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ControlCalidadMovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTROL_CALIDAD", dto.NUM_CONTROL_CALIDAD);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_ENTIDAD_EVALUADOR", dto.COD_ENTIDAD_EVALUADOR);
        p.Add("COD_ENTIDAD_APROBADOR", dto.COD_ENTIDAD_APROBADOR);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ControlCalidadMovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CONTROL_CALIDAD", dto.NUM_CONTROL_CALIDAD);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("COD_ENTIDAD_EVALUADOR", dto.COD_ENTIDAD_EVALUADOR);
        p.Add("COD_ENTIDAD_APROBADOR", dto.COD_ENTIDAD_APROBADOR);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CONTROL_CALIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ControlCalidadMovimientosAlmacen.Delete, new { NUM_CONTROL_CALIDAD = NUM_CONTROL_CALIDAD })
        };
    }
}

}