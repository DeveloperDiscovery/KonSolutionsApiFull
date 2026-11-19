using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProduccionRutasOperacionesEstructuraRepository : IProduccionRutasOperacionesEstructuraRepository
{
    private readonly IDbConnectionFactory _db;
    public ProduccionRutasOperacionesEstructuraRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProduccionRutasOperacionesEstructura>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProduccionRutasOperacionesEstructura>(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProduccionRutasOperacionesEstructura?> ShowbyidAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProduccionRutasOperacionesEstructura>(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Showbyid, new { COD_RUTA_PRODUCCION = COD_RUTA_PRODUCCION, NUM_ESTRUCTURA_RUTA_PROCESO = NUM_ESTRUCTURA_RUTA_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Showcombox, "COD_RUTA_PRODUCCION", "DES_RUTA_PRODUCCION");
    }

    public async Task<IEnumerable<ProduccionRutasOperacionesEstructura>> SearchAsync(ProduccionRutasOperacionesEstructuraSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProduccionRutasOperacionesEstructura>(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProduccionRutasOperacionesEstructuraInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("NUM_ESTRUCTURA_RUTA_PROCESO", dto.NUM_ESTRUCTURA_RUTA_PROCESO);
        p.Add("COD_ESTRUCTURA_RUTA_PROCESO", dto.COD_ESTRUCTURA_RUTA_PROCESO);
        p.Add("FLG_PROCESO", dto.FLG_PROCESO ?? false);
        p.Add("FLG_SUB_PROCESO", dto.FLG_SUB_PROCESO ?? false);
        p.Add("FLG_CLASE_OPERACION_PRODUCCION", dto.FLG_CLASE_OPERACION_PRODUCCION ?? false);
        p.Add("FLG_TIPO_OPERACION_PRODUCCION", dto.FLG_TIPO_OPERACION_PRODUCCION ?? false);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_LEAD_TIME", dto.CAN_LEAD_TIME);
        p.Add("FLG_MANUAL", dto.FLG_MANUAL ?? false);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("FLG_REGISTRA_INGRESO", dto.FLG_REGISTRA_INGRESO ?? false);
        p.Add("FLG_REGISTRA_SALIDA", dto.FLG_REGISTRA_SALIDA ?? false);
        p.Add("FLG_PRINCIPAL", dto.FLG_PRINCIPAL ?? false);
        p.Add("FLG_CONTENEDOR_PRODUCCION", dto.FLG_CONTENEDOR_PRODUCCION ?? false);
        p.Add("NUM_ESTRUCTURA_RUTA_PROCESO_PADRE", dto.NUM_ESTRUCTURA_RUTA_PROCESO_PADRE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProduccionRutasOperacionesEstructuraUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("NUM_ESTRUCTURA_RUTA_PROCESO", dto.NUM_ESTRUCTURA_RUTA_PROCESO);
        p.Add("COD_ESTRUCTURA_RUTA_PROCESO", dto.COD_ESTRUCTURA_RUTA_PROCESO);
        p.Add("FLG_PROCESO", dto.FLG_PROCESO ?? false);
        p.Add("FLG_SUB_PROCESO", dto.FLG_SUB_PROCESO ?? false);
        p.Add("FLG_CLASE_OPERACION_PRODUCCION", dto.FLG_CLASE_OPERACION_PRODUCCION ?? false);
        p.Add("FLG_TIPO_OPERACION_PRODUCCION", dto.FLG_TIPO_OPERACION_PRODUCCION ?? false);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_LEAD_TIME", dto.CAN_LEAD_TIME);
        p.Add("FLG_MANUAL", dto.FLG_MANUAL ?? false);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("FLG_REGISTRA_INGRESO", dto.FLG_REGISTRA_INGRESO ?? false);
        p.Add("FLG_REGISTRA_SALIDA", dto.FLG_REGISTRA_SALIDA ?? false);
        p.Add("FLG_PRINCIPAL", dto.FLG_PRINCIPAL ?? false);
        p.Add("FLG_CONTENEDOR_PRODUCCION", dto.FLG_CONTENEDOR_PRODUCCION ?? false);
        p.Add("NUM_ESTRUCTURA_RUTA_PROCESO_PADRE", dto.NUM_ESTRUCTURA_RUTA_PROCESO_PADRE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_RUTA_PRODUCCION, int NUM_ESTRUCTURA_RUTA_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Delete, new { COD_RUTA_PRODUCCION = COD_RUTA_PRODUCCION, NUM_ESTRUCTURA_RUTA_PROCESO = NUM_ESTRUCTURA_RUTA_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.ProduccionRutasOperacionesEstructura.Delete, new { COD_RUTA_PRODUCCION = COD_RUTA_PRODUCCION, NUM_ESTRUCTURA_RUTA_PROCESO = NUM_ESTRUCTURA_RUTA_PROCESO })
        };
    }
}

}