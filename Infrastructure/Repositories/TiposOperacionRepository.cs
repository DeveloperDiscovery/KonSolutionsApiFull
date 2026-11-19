using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOperacionRepository : ITiposOperacionRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOperacion>> ShowallAsync(string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacion>(SP.SCHEMAEstandar.TiposOperacion.Showall, new { COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOperacion?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOperacion>(SP.SCHEMAEstandar.TiposOperacion.Showbyid, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposOperacion.Showcombox);
    }

    public async Task<IEnumerable<TiposOperacion>> SearchAsync(string codClaseOperacion, TiposOperacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacion>(SP.SCHEMAEstandar.TiposOperacion.Search,
            new { COD_CLASE_OPERACION = codClaseOperacion, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("DES_TIPO_OPERACION", dto.DES_TIPO_OPERACION);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_PROVISION", dto.FLG_PROVISION ?? false);
        p.Add("FLG_CANCELACION", dto.FLG_CANCELACION ?? false);
        p.Add("FLG_BIEN", dto.FLG_BIEN ?? false);
        p.Add("FLG_DEVOLUCION", dto.FLG_DEVOLUCION ?? false);
        p.Add("FLG_ANULACION", dto.FLG_ANULACION ?? false);
        p.Add("FLG_ANTICIPO", dto.FLG_ANTICIPO ?? false);
        p.Add("FLG_CENTRO_COSTO", dto.FLG_CENTRO_COSTO ?? false);
        p.Add("FLG_PROYECTO", dto.FLG_PROYECTO ?? false);
        p.Add("FLG_LOTE_CONTROL", dto.FLG_LOTE_CONTROL ?? false);
        p.Add("FLG_CAJA", dto.FLG_CAJA ?? false);
        p.Add("FLG_BANCO", dto.FLG_BANCO ?? false);
        p.Add("FLG_EXTORNO", dto.FLG_EXTORNO ?? false);
        p.Add("FLG_COMERCIO_EXTERIOR", dto.FLG_COMERCIO_EXTERIOR ?? false);
        p.Add("FLG_ATIENDE_REQUERIMIENTO_COMPRA", dto.FLG_ATIENDE_REQUERIMIENTO_COMPRA ?? false);
        p.Add("FLG_COMPARACION_PRECIOS_COMPRA", dto.FLG_COMPARACION_PRECIOS_COMPRA ?? false);
        p.Add("FLG_INCIDENCIA_PRODUCTO", dto.FLG_INCIDENCIA_PRODUCTO ?? false);
        p.Add("FLG_CARGO", dto.FLG_CARGO ?? false);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO_DEFAULT", dto.COD_MOTIVO_DEFAULT);
        p.Add("COD_TIPO_OPERACION_SUNAT", dto.COD_TIPO_OPERACION_SUNAT);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_TIPO_PRODUCTO_DEFECTO", dto.COD_TIPO_PRODUCTO_DEFECTO);
        p.Add("COD_TIPO_PEDIDO_COMERCIAL", dto.COD_TIPO_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_CLASE_OPERACION_SINCRONIZADA", dto.COD_CLASE_OPERACION_SINCRONIZADA);
        p.Add("COD_TIPO_OPERACION_SINCRONIZADA", dto.COD_TIPO_OPERACION_SINCRONIZADA);
        p.Add("COD_TIPO_ESTADO_RELACIONADO", dto.COD_TIPO_ESTADO_RELACIONADO);
        p.Add("COD_TIPO_ESTADO_DETALLE_RELACIONADO", dto.COD_TIPO_ESTADO_DETALLE_RELACIONADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposOperacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposOperacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposOperacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposOperacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("DES_TIPO_OPERACION", dto.DES_TIPO_OPERACION);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_PROVISION", dto.FLG_PROVISION ?? false);
        p.Add("FLG_CANCELACION", dto.FLG_CANCELACION ?? false);
        p.Add("FLG_BIEN", dto.FLG_BIEN ?? false);
        p.Add("FLG_DEVOLUCION", dto.FLG_DEVOLUCION ?? false);
        p.Add("FLG_ANULACION", dto.FLG_ANULACION ?? false);
        p.Add("FLG_ANTICIPO", dto.FLG_ANTICIPO ?? false);
        p.Add("FLG_CENTRO_COSTO", dto.FLG_CENTRO_COSTO ?? false);
        p.Add("FLG_PROYECTO", dto.FLG_PROYECTO ?? false);
        p.Add("FLG_LOTE_CONTROL", dto.FLG_LOTE_CONTROL ?? false);
        p.Add("FLG_CAJA", dto.FLG_CAJA ?? false);
        p.Add("FLG_BANCO", dto.FLG_BANCO ?? false);
        p.Add("FLG_EXTORNO", dto.FLG_EXTORNO ?? false);
        p.Add("FLG_COMERCIO_EXTERIOR", dto.FLG_COMERCIO_EXTERIOR ?? false);
        p.Add("FLG_ATIENDE_REQUERIMIENTO_COMPRA", dto.FLG_ATIENDE_REQUERIMIENTO_COMPRA ?? false);
        p.Add("FLG_COMPARACION_PRECIOS_COMPRA", dto.FLG_COMPARACION_PRECIOS_COMPRA ?? false);
        p.Add("FLG_INCIDENCIA_PRODUCTO", dto.FLG_INCIDENCIA_PRODUCTO ?? false);
        p.Add("FLG_CARGO", dto.FLG_CARGO ?? false);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO_DEFAULT", dto.COD_MOTIVO_DEFAULT);
        p.Add("COD_TIPO_OPERACION_SUNAT", dto.COD_TIPO_OPERACION_SUNAT);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_TIPO_PRODUCTO_DEFECTO", dto.COD_TIPO_PRODUCTO_DEFECTO);
        p.Add("COD_TIPO_PEDIDO_COMERCIAL", dto.COD_TIPO_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_CLASE_OPERACION_SINCRONIZADA", dto.COD_CLASE_OPERACION_SINCRONIZADA);
        p.Add("COD_TIPO_OPERACION_SINCRONIZADA", dto.COD_TIPO_OPERACION_SINCRONIZADA);
        p.Add("COD_TIPO_ESTADO_RELACIONADO", dto.COD_TIPO_ESTADO_RELACIONADO);
        p.Add("COD_TIPO_ESTADO_DETALLE_RELACIONADO", dto.COD_TIPO_ESTADO_DETALLE_RELACIONADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposOperacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposOperacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposOperacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposOperacion.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposOperacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposOperacion.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION })
        };
    }
}

    /// <summary>PROC_ESTANDAR_TIPOS_OPERACION_LOADCOMBOS: tipos de entidad, tipos de pedido
    /// comercial, tipos de precio, condiciones de pago, clases de operación y estados en un
    /// solo viaje. El diálogo de Tipos de Operación no usa el combo de Estados (el estado
    /// del registro se maneja con EstadoCombo aparte) pero igual se lee el result set para
    /// avanzar el reader correctamente.</summary>
    public async Task<TiposOperacionLoadCombosResult> LoadcombosAsync(int codTipoEstadoClasesOperacion)
    {
        using var conn = _db.CreateConnection();

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.TiposOperacion.Loadcombos,
            new { COD_TIPO_ESTADO_CLASES_OPERACION = codTipoEstadoClasesOperacion },
            commandType: CommandType.StoredProcedure);

        var tiposEntidad         = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposPedidoComercial = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposPrecio          = MapCombo(await multi.ReadAsync<dynamic>());
        var condicionesPago      = MapCombo(await multi.ReadAsync<dynamic>());
        var clasesOperacion      = MapCombo(await multi.ReadAsync<dynamic>());
        var estados              = MapCombo(await multi.ReadAsync<dynamic>());

        return new TiposOperacionLoadCombosResult
        {
            CodTipoEstadoClasesOperacion = codTipoEstadoClasesOperacion,
            TiposEntidad = tiposEntidad,
            TiposPedidoComercial = tiposPedidoComercial,
            TiposPrecio = tiposPrecio,
            CondicionesPago = condicionesPago,
            ClasesOperacion = clasesOperacion,
            Estados = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera que empiece con "COD") y la de descripción (primera "DES"/"NOMBRE").</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id = colId is not null && d[colId] is not null ? d[colId].ToString() ?? "" : "",
                Texto = colTexto is not null && d[colTexto] is not null ? d[colTexto].ToString() ?? "" : ""
            });
        }
        return resultado;
    }

}