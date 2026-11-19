using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PedidosComercialesRepository : IPedidosComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public PedidosComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PedidosComerciales>> ShowallAsync(string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComerciales>(SP.SCHEMAComercial.PedidosComerciales.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<PedidosComerciales?> ShowbyidAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PedidosComerciales>(SP.SCHEMAComercial.PedidosComerciales.Showbyid,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, NUM_PEDIDO_COMERCIAL = numPedidoComercial },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PedidosComerciales>> SearchAsync(PedidosComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidosComerciales>(SP.SCHEMAComercial.PedidosComerciales.Search,
            new { COD_CLASE_OPERACION = dto.CodClaseOperacion, COD_TIPO_OPERACION = dto.CodTipoOperacion, SEARCH = dto.Search ?? "" },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        // 0 = alta nueva — el SP autonumera si no encuentra el registro (ver comentario en el DTO).
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL ?? 0);
        p.Add("DES_PEDIDO_COMERCIAL", dto.DES_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_DOCUMENTO", dto.NUM_DOCUMENTO);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TIPO_PEDIDO_COMERCIAL", dto.COD_TIPO_PEDIDO_COMERCIAL);
        p.Add("FEC_PEDIDO", dto.FEC_PEDIDO);
        p.Add("FEC_VECIMIENTO", dto.FEC_VECIMIENTO);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("FEC_ENTREGA_PUBLICA", dto.FEC_ENTREGA_PUBLICA);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_TEMPORADA", dto.NUM_TEMPORADA);
        p.Add("NUM_COLECCION", dto.NUM_COLECCION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("COD_CENTRO_COSTO_SOLICITANTE", dto.COD_CENTRO_COSTO_SOLICITANTE);
        p.Add("COD_CENTRO_COSTO_DESTINO", dto.COD_CENTRO_COSTO_DESTINO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("COD_ENTIDAD_VENDEDOR", dto.COD_ENTIDAD_VENDEDOR);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("COD_PAIS_DESTINO", dto.COD_PAIS_DESTINO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_PEDIDO_COMERCIAL_RELACIONADO", dto.NUM_PEDIDO_COMERCIAL_RELACIONADO);
        p.Add("NUM_TRACKING", dto.NUM_TRACKING);
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComerciales.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComerciales.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComerciales.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(PedidosComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("DES_PEDIDO_COMERCIAL", dto.DES_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_DOCUMENTO", dto.NUM_DOCUMENTO);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_TIPO_PEDIDO_COMERCIAL", dto.COD_TIPO_PEDIDO_COMERCIAL);
        p.Add("FEC_PEDIDO", dto.FEC_PEDIDO);
        p.Add("FEC_VECIMIENTO", dto.FEC_VECIMIENTO);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("FEC_ENTREGA_PUBLICA", dto.FEC_ENTREGA_PUBLICA);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_TEMPORADA", dto.NUM_TEMPORADA);
        p.Add("NUM_COLECCION", dto.NUM_COLECCION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("COD_CENTRO_COSTO_SOLICITANTE", dto.COD_CENTRO_COSTO_SOLICITANTE);
        p.Add("COD_CENTRO_COSTO_DESTINO", dto.COD_CENTRO_COSTO_DESTINO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("COD_ENTIDAD_VENDEDOR", dto.COD_ENTIDAD_VENDEDOR);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("COD_PAIS_DESTINO", dto.COD_PAIS_DESTINO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("NUM_RENDICION_CUENTAS", dto.NUM_RENDICION_CUENTAS);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("NUM_PEDIDO_COMERCIAL_RELACIONADO", dto.NUM_PEDIDO_COMERCIAL_RELACIONADO);
        p.Add("NUM_TRACKING", dto.NUM_TRACKING);
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComerciales.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComerciales.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComerciales.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, NUM_PEDIDO_COMERCIAL = numPedidoComercial };
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.PedidosComerciales.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.PedidosComerciales.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.PedidosComerciales.Delete, p)
            };
        }
    }

    /// <summary>PROC_COMERCIAL_PEDIDOS_COMERCIALES_LOADCOMBOS: los nueve catálogos de la
    /// cabecera en un solo viaje, en vez de las nueve llamadas HTTP en paralelo que hacía
    /// la pantalla al abrirse.
    /// El orden de las lecturas DEBE coincidir con el orden de los result sets del SP.</summary>
    public async Task<PedidosComercialesLoadCombosResult> LoadcombosAsync(string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAComercial.PedidosComerciales.Loadcombos,
            new { COD_CLASE_OPERACION = codClaseOperacion },
            commandType: CommandType.StoredProcedure);

        var tiposOperacion  = (await multi.ReadAsync<TipoOperacionDeClase>()).ToList();
        var comprobantes    = (await multi.ReadAsync<TipoComprobantePagoDeOperacion>()).ToList();
        var paises          = MapCombo(await multi.ReadAsync<dynamic>());
        var incoterms       = MapCombo(await multi.ReadAsync<dynamic>());
        var monedas         = MapCombo(await multi.ReadAsync<dynamic>());
        var formasPago      = MapCombo(await multi.ReadAsync<dynamic>());
        var condicionesPago = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposDescuento  = (await multi.ReadAsync<TipoDescuentoConTasa>()).ToList();
        // Proyectos no tiene columna COD_*: su clave es NUM_PROYECTO, así que se le indican
        // las columnas a mano en vez de dejar que MapCombo las adivine.
        var proyectos       = MapCombo(await multi.ReadAsync<dynamic>(), "NUM_PROYECTO", "DES_PROYECTO");

        return new PedidosComercialesLoadCombosResult
        {
            CodClaseOperacion    = codClaseOperacion,
            TiposOperacion       = tiposOperacion,
            TiposComprobantePago = comprobantes,
            Paises               = paises,
            Incoterms            = incoterms,
            Monedas              = monedas,
            FormasPago           = formasPago,
            CondicionesPago      = condicionesPago,
            TiposDescuento       = tiposDescuento,
            Proyectos            = proyectos
        };
    }

    /// <summary>Convierte un result set anónimo en items de combo. Por defecto toma la
    /// primera columna COD* como Id y la primera DES* como Texto, que es la convención de
    /// los SHOWCOMBOX; los dos parámetros permiten forzar otras columnas para los catálogos
    /// que no la siguen (Proyectos usa NUM_PROYECTO).</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas,
                                               string? columnaId = null, string? columnaTexto = null)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = columnaId is not null && d.ContainsKey(columnaId)
                ? columnaId
                : d.Keys.FirstOrDefault(k => k.StartsWith("COD", StringComparison.OrdinalIgnoreCase));
            var colTexto = columnaTexto is not null && d.ContainsKey(columnaTexto)
                ? columnaTexto
                : d.Keys.FirstOrDefault(k => k.StartsWith("DES", StringComparison.OrdinalIgnoreCase)
                                          || k.StartsWith("NOMBRE", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id    = colId    is not null && d[colId]    is not null ? d[colId].ToString()    ?? "" : "",
                Texto = colTexto is not null && d[colTexto] is not null ? d[colTexto].ToString() ?? "" : ""
            });
        }
        return resultado;
    }
    /// <summary>PROC_COMERCIAL_PEDIDOS_COMERCIALES_ATENCION: lo pedido, lo atendido y el porcentaje de cada pedido de la clase (y tipo) de operación.</summary>
    public async Task<IEnumerable<PedidoComercialAtencion>> AtencionAsync(string codClaseOperacion, string? codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PedidoComercialAtencion>(SP.SCHEMAComercial.PedidosComerciales.Atencion,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion },
            commandType: CommandType.StoredProcedure);
    }
}
