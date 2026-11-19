using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RegistroProduccionRepository : IRegistroProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public RegistroProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RegistroProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroProduccion>(SP.SCHEMAProduccion.RegistroProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RegistroProduccion?> ShowbyidAsync(int NUM_REGISTRO_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegistroProduccion>(SP.SCHEMAProduccion.RegistroProduccion.Showbyid, new { NUM_REGISTRO_PRODUCCION = NUM_REGISTRO_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.RegistroProduccion.Showcombox, "NUM_REGISTRO_PRODUCCION", "DES_CATEGORIA_OPERACION_PRODUCCION");
    }

    public async Task<IEnumerable<RegistroProduccion>> SearchAsync(RegistroProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroProduccion>(SP.SCHEMAProduccion.RegistroProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RegistroProduccionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO_PRODUCCION", dto.NUM_REGISTRO_PRODUCCION);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("NUM_SECUENCIA_RUTA_PRODUCCION", dto.NUM_SECUENCIA_RUTA_PRODUCCION);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_ENTIDAD_REGISTRO", dto.COD_ENTIDAD_REGISTRO);
        p.Add("COD_CLASE_OPERACION_PRODUCCION", dto.COD_CLASE_OPERACION_PRODUCCION);
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("NUM_CONTENEDOR_PRODUCCION", dto.NUM_CONTENEDOR_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCIDA_SECUNDARIA", dto.CAN_PRODUCIDA_SECUNDARIA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("NUM_REGISTRO_PRODUCCION_ASOCIADO", dto.NUM_REGISTRO_PRODUCCION_ASOCIADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.RegistroProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.RegistroProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.RegistroProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RegistroProduccionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO_PRODUCCION", dto.NUM_REGISTRO_PRODUCCION);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("NUM_SECUENCIA_RUTA_PRODUCCION", dto.NUM_SECUENCIA_RUTA_PRODUCCION);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_ENTIDAD_REGISTRO", dto.COD_ENTIDAD_REGISTRO);
        p.Add("COD_CLASE_OPERACION_PRODUCCION", dto.COD_CLASE_OPERACION_PRODUCCION);
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("COD_CATEGORIA_OPERACION_PRODUCCION", dto.COD_CATEGORIA_OPERACION_PRODUCCION);
        p.Add("NUM_CONTENEDOR_PRODUCCION", dto.NUM_CONTENEDOR_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCIDA_SECUNDARIA", dto.CAN_PRODUCIDA_SECUNDARIA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("NUM_REGISTRO_PRODUCCION_ASOCIADO", dto.NUM_REGISTRO_PRODUCCION_ASOCIADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.RegistroProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.RegistroProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.RegistroProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_REGISTRO_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.RegistroProduccion.Delete, new { NUM_REGISTRO_PRODUCCION = NUM_REGISTRO_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.RegistroProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.RegistroProduccion.Delete, new { NUM_REGISTRO_PRODUCCION = NUM_REGISTRO_PRODUCCION })
        };
    }
}

}