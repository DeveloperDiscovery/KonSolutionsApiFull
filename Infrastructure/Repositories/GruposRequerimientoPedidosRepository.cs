using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposRequerimientoPedidosRepository : IGruposRequerimientoPedidosRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposRequerimientoPedidosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposRequerimientoPedidos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposRequerimientoPedidos>(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposRequerimientoPedidos?> ShowbyidAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposRequerimientoPedidos>(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Showbyid, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO = COD_GRUPO_REQUERIMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Showcombox, "COD_GRUPO_PLANEAMIENTO", "DES_UNIDAD_MEDIDA");
    }

    public async Task<IEnumerable<GruposRequerimientoPedidos>> SearchAsync(GruposRequerimientoPedidosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposRequerimientoPedidos>(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposRequerimientoPedidosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("COD_GRUPO_REQUERIMIENTO", dto.COD_GRUPO_REQUERIMIENTO);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PRODUCTO_COMPONENTE", dto.COD_PRODUCTO_COMPONENTE);
        p.Add("CAN_REQUERIDO", dto.CAN_REQUERIDO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_REQUERIDO_SECUNDARIO", dto.CAN_REQUERIDO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("CAN_MERMA", dto.CAN_MERMA);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(GruposRequerimientoPedidosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PLANEAMIENTO", dto.COD_GRUPO_PLANEAMIENTO);
        p.Add("COD_GRUPO_REQUERIMIENTO", dto.COD_GRUPO_REQUERIMIENTO);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PRODUCTO_COMPONENTE", dto.COD_PRODUCTO_COMPONENTE);
        p.Add("CAN_REQUERIDO", dto.CAN_REQUERIDO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_REQUERIDO_SECUNDARIO", dto.CAN_REQUERIDO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("CAN_MERMA", dto.CAN_MERMA);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_GRUPO_PLANEAMIENTO, int COD_GRUPO_REQUERIMIENTO, int NUM_PEDIDO_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO = COD_GRUPO_REQUERIMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.GruposRequerimientoPedidos.Delete, new { COD_GRUPO_PLANEAMIENTO = COD_GRUPO_PLANEAMIENTO, COD_GRUPO_REQUERIMIENTO = COD_GRUPO_REQUERIMIENTO, NUM_PEDIDO_COMERCIAL = NUM_PEDIDO_COMERCIAL })
        };
    }
}

}