using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SubProcesosRepository : ISubProcesosRepository
{
    private readonly IDbConnectionFactory _db;
    public SubProcesosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SubProcesos>> ShowallAsync(string COD_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubProcesos>(SP.SCHEMAProduccion.SubProcesos.Showall, new { COD_PROCESO = COD_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<SubProcesos?> ShowbyidAsync(string COD_PROCESO, string COD_SUB_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SubProcesos>(SP.SCHEMAProduccion.SubProcesos.Showbyid, new { COD_PROCESO = COD_PROCESO, COD_SUB_PROCESO = COD_SUB_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.SubProcesos.Showcombox, new { COD_PROCESO = COD_PROCESO });
    }

    public async Task<IEnumerable<SubProcesos>> SearchAsync(SubProcesosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubProcesos>(SP.SCHEMAProduccion.SubProcesos.Search, new { COD_PROCESO = dto.COD_PROCESO, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SubProcesosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("DES_SUB_PROCESO", dto.DES_SUB_PROCESO);
        p.Add("DES_ALIAS", dto.DES_ALIAS);
        p.Add("FLG_ORDEN_FABRICACION", dto.FLG_ORDEN_FABRICACION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION", dto.DES_NOMBRE_ORDEN_PRODUCCION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_ITEM", dto.DES_NOMBRE_ORDEN_PRODUCCION_ITEM);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD", dto.DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD);
        p.Add("DES_EQUIPO_PRODUCCION_PRINCIPAL", dto.DES_EQUIPO_PRODUCCION_PRINCIPAL);
        p.Add("DES_LINEA_PRODUCCION", dto.DES_LINEA_PRODUCCION);
        p.Add("FLG_EMBALAJE_FINAL", dto.FLG_EMBALAJE_FINAL);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD);
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("VAL_ULTIMO_CORRELATIVO", dto.VAL_ULTIMO_CORRELATIVO);
        p.Add("VAL_ULTIMO_CORRELATIVO_CONTENEDOR", dto.VAL_ULTIMO_CORRELATIVO_CONTENEDOR);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.SubProcesos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.SubProcesos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.SubProcesos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SubProcesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("DES_SUB_PROCESO", dto.DES_SUB_PROCESO);
        p.Add("DES_ALIAS", dto.DES_ALIAS);
        p.Add("FLG_ORDEN_FABRICACION", dto.FLG_ORDEN_FABRICACION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION", dto.DES_NOMBRE_ORDEN_PRODUCCION);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_ITEM", dto.DES_NOMBRE_ORDEN_PRODUCCION_ITEM);
        p.Add("DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD", dto.DES_NOMBRE_ORDEN_PRODUCCION_UNIDAD);
        p.Add("DES_EQUIPO_PRODUCCION_PRINCIPAL", dto.DES_EQUIPO_PRODUCCION_PRINCIPAL);
        p.Add("DES_LINEA_PRODUCCION", dto.DES_LINEA_PRODUCCION);
        p.Add("FLG_EMBALAJE_FINAL", dto.FLG_EMBALAJE_FINAL);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD);
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("VAL_ULTIMO_CORRELATIVO", dto.VAL_ULTIMO_CORRELATIVO);
        p.Add("VAL_ULTIMO_CORRELATIVO_CONTENEDOR", dto.VAL_ULTIMO_CORRELATIVO_CONTENEDOR);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.SubProcesos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.SubProcesos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.SubProcesos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_SUB_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.SubProcesos.Delete, new { COD_PROCESO = COD_PROCESO, COD_SUB_PROCESO = COD_SUB_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.SubProcesos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.SubProcesos.Delete, new { COD_PROCESO = COD_PROCESO, COD_SUB_PROCESO = COD_SUB_PROCESO })
        };
    }
}

}