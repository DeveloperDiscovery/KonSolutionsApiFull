using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Registro de Compras (FINANZAS.COMPROBANTES_PAGO + _DETALLE). La cabecera y las líneas se guardan con los procedimientos ESTÁNDAR (PROC_FINANZAS_COMPROBANTES_PAGO_INSERT / _UPDATE / _DELETE y
/// _DETALLE_INSERT / _UPDATE / _DELETE), que recalculan los totales en FINANZAS.COMPROBANTES_PAGO_CALCULO. Lo propio del módulo (órdenes, conciliación, flujo, XML) pasa por los
/// PROC_FINANZAS_REGISTRO_COMPRAS_* del script 39, que lanzan errores propios (50680 a 50699) y aquí vuelven como WriteResult con el mensaje tal cual.</summary>
public class RegistroComprasRepository : IRegistroComprasRepository
{
    private readonly IDbConnectionFactory _db;
    public RegistroComprasRepository(IDbConnectionFactory db) => _db = db;

    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    private static WriteResult Error(SqlException ex, string sp, object p) => new()
    {
        Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p)
    };

    // ───────── cabecera ─────────

    public async Task<IEnumerable<RegistroCompra>> SearchAsync(RegistroCompraSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", Nulo(dto.COD_CLASE_OPERACION));
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("COD_TIPO_OPERACION", Nulo(dto.COD_TIPO_OPERACION));
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_ESTADO_ALIAS_CONCILIACION", Nulo(dto.COD_ESTADO_ALIAS_CONCILIACION));
        p.Add("FEC_DESDE", dto.FEC_DESDE?.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA?.Date, DbType.Date);
        p.Add("NUM_FILAS", dto.NUM_FILAS ?? 1000);
        return await conn.QueryAsync<RegistroCompra>(SP.SCHEMAFinanzas.RegistroCompras.Search, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<RegistroCompra?> ShowbyidAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegistroCompra>(SP.SCHEMAFinanzas.RegistroCompras.Showbyid, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los parámetros del alta y de la modificación ESTÁNDAR de FINANZAS.COMPROBANTES_PAGO (PROC_FINANZAS_COMPROBANTES_PAGO_INSERT / _UPDATE): el estándar pide todas las columnas, así que lo que
    /// el Registro de Compras no maneja va vacío en el alta y, en la modificación, tal como ya estaba (<paramref name="actual"/>). El total de la cabecera no se escribe: lo recalcula el estándar al registrar las líneas.</summary>
    private static DynamicParameters ParametrosCabecera(decimal numRegistro, string codClase, RegistroCompraSaveDto dto, ComprobantesPago? actual, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", numRegistro);
        p.Add("COD_CLASE_OPERACION", codClase);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION?.Trim());
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO?.Trim());
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO?.Trim());
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO?.Date, DbType.Date);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO?.Date, DbType.Date);
        p.Add("FEC_RECEPCION", dto.FEC_RECEPCION?.Date, DbType.Date);
        p.Add("FEC_DEVOLUCION", actual?.FEC_DEVOLUCION?.Date, DbType.Date);
        p.Add("FEC_EMBARQUE", actual?.FEC_EMBARQUE?.Date, DbType.Date);
        p.Add("FEC_COMERCIAL", actual?.FEC_COMERCIAL?.Date, DbType.Date);
        p.Add("FEC_REGISTRO_CAJA", actual?.FEC_REGISTRO_CAJA?.Date, DbType.Date);
        p.Add("COD_CAJA", Nulo(actual?.COD_CAJA));
        p.Add("NUM_UNIDAD_COMERCIAL", actual?.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.COD_TIPO_DESCUENTO is null ? null : dto.TAS_DESCUENTO);      // sin tipo no hay tasa
        p.Add("COD_INCOTERM", Nulo(dto.COD_INCOTERM));
        p.Add("COD_CENTRO_COSTO_SOLICITANTE", Nulo(dto.COD_CENTRO_COSTO_SOLICITANTE));
        p.Add("COD_CENTRO_COSTO_DESTINO", Nulo(dto.COD_CENTRO_COSTO_DESTINO));
        p.Add("COD_SUCURSAL", Nulo(dto.COD_SUCURSAL));
        p.Add("COD_MONEDA", Nulo(dto.COD_MONEDA));
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_TOTAL", actual?.VAL_TOTAL ?? 0m);
        p.Add("DES_OBSERVACION", Nulo(dto.DES_OBSERVACION));
        p.Add("COD_ENTIDAD_VENDEDOR", actual?.COD_ENTIDAD_VENDEDOR);
        p.Add("NUM_PEDIDO_COMERCIAL", actual?.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_PERIODO_CONTABLE", actual?.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", Nulo(actual?.COD_ASIENTO_CONTABLE));
        p.Add("NUM_ASIENTO_CONTABLE", actual?.NUM_ASIENTO_CONTABLE);
        p.Add("COD_TIPO_ESTADO_PAGO", actual?.COD_TIPO_ESTADO_PAGO);
        p.Add("COD_ESTADO_PAGO", actual?.COD_ESTADO_PAGO);
        p.Add("COD_TIPO_ESTADO", actual?.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", actual?.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("DES_NOMBRE_ARCHIVO_PDF", Nulo(dto.DES_NOMBRE_ARCHIVO_PDF));
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        return p;
    }

    /// <summary>Alta con el procedimiento estándar: con NUM_REGISTRO = 0 él asigna el siguiente número (como las órdenes) y lo devuelve; nace en estado REG.</summary>
    public async Task<WriteResult> InsertAsync(RegistroCompraInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = ParametrosCabecera(0, dto.COD_CLASE_OPERACION?.Trim() ?? "", dto, null, codUsuario, codEstacion);
        p.Add("COD_ESTADO_ALIAS_CONCILIACION", "REG");
        p.Add("NUM_REGISTRO_GENERADO", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 15, scale: 0);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.ComprobantesPago.Insert, p, commandType: CommandType.StoredProcedure);
            var numero = p.Get<decimal?>("NUM_REGISTRO_GENERADO") ?? 0;
            return new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = numero.ToString("0") };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.ComprobantesPago.Insert, p); }
    }

    /// <summary>Modificación con el procedimiento estándar. Como el estándar reemplaza todas las columnas, primero se lee el comprobante (SHOWBYID estándar) para devolverle lo que esta pantalla no toca.</summary>
    public async Task<WriteResult> UpdateAsync(RegistroCompraUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var actual = await conn.QueryFirstOrDefaultAsync<ComprobantesPago>(SP.SCHEMAFinanzas.ComprobantesPago.Showbyid, new { dto.NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
            if (actual is null)
                return new WriteResult { Estado = "ERROR", Mensaje = "El comprobante no existe.", ErrorNumber = 50684, ErrorProcedure = SP.SCHEMAFinanzas.ComprobantesPago.Showbyid };
            var p = ParametrosCabecera(dto.NUM_REGISTRO, actual.COD_CLASE_OPERACION, dto, actual, codUsuario, codEstacion);
            try
            {
                await conn.ExecuteAsync(SP.SCHEMAFinanzas.ComprobantesPago.Update, p, commandType: CommandType.StoredProcedure);
                return new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = dto.NUM_REGISTRO.ToString("0") };
            }
            catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.ComprobantesPago.Update, p); }
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.ComprobantesPago.Showbyid, new { dto.NUM_REGISTRO }); }
    }

    public async Task<WriteResult> DeleteAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        var p = new { NUM_REGISTRO };
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.ComprobantesPago.Delete, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.ComprobantesPago.Delete, p); }
    }

    /// <summary>Un tipo de operación de la clase con lo que habilita (centro de costo, proyecto, lote, comercio exterior) y si es de bienes o de servicios.</summary>
    public async Task<RegistroCompraTipoOperacion?> TipoOperacionAsync(string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        var tipos = await conn.QueryAsync<RegistroCompraTipoOperacion>(SP.SCHEMAFinanzas.RegistroCompras.TiposOperacion, new { COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
        return tipos.FirstOrDefault(x => string.Equals(x.COD_TIPO_OPERACION, codTipoOperacion, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>El detalle de los valores totales del documento (Concepto / Tasa / Valor), de FINANZAS.COMPROBANTES_PAGO_CALCULO, con el procedimiento estándar de consulta.</summary>
    public async Task<IEnumerable<RegistroCompraCalculo>> CalculoAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraCalculo>(SP.SCHEMAFinanzas.ComprobantesPagoCalculo.Showall, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los catálogos de la pantalla. El procedimiento devuelve nueve resultados en este orden: tipos de operación, tipos de comprobante por operación, tipos de comprobante,
    /// monedas, formas de pago, condiciones de pago, incoterms, proyectos y tipos de descuento (los seis últimos son los SHOWCOMBOX de siempre).</summary>
    public async Task<RegistroCompraCombos> LoadcombosAsync(string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        using var g = await conn.QueryMultipleAsync(SP.SCHEMAFinanzas.RegistroCompras.Loadcombos, new { COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
        var r = new RegistroCompraCombos
        {
            TIPOS_OPERACION = (await g.ReadAsync<RegistroCompraTipoOperacion>()).ToList(),
            TIPOS_COMPROBANTE_POR_OPERACION = (await g.ReadAsync<RegistroCompraTipoComprobante>()).ToList(),
            TIPOS_COMPROBANTE = (await g.ReadAsync<RegistroCompraTipoComprobante>()).ToList(),
            MONEDAS = Combo(await g.ReadAsync(), "COD_MONEDA", "DES_MONEDA"),
            FORMAS_PAGO = Combo(await g.ReadAsync(), "COD_FORMA_PAGO", "DES_FORMA_PAGO"),
            CONDICIONES_PAGO = Combo(await g.ReadAsync(), "COD_CONDICION_PAGO", "DES_CONDICION_PAGO"),
            INCOTERMS = Combo(await g.ReadAsync(), "COD_INCOTERM", "DES_INCOTERM"),
            PROYECTOS = Combo(await g.ReadAsync(), "NUM_PROYECTO", "DES_PROYECTO"),
            // el noveno resultado es del script 39 v7: con un SQL anterior no existe y la pantalla debe abrir igual (sin tipos de descuento)
            TIPOS_DESCUENTO = g.IsConsumed ? new List<RegistroCompraTipoDescuento>() : (await g.ReadAsync<RegistroCompraTipoDescuento>()).ToList()
        };
        return r;
    }

    private static List<RegistroCompraCombo> Combo(IEnumerable<dynamic> filas, string colId, string colTexto)
    {
        var lista = new List<RegistroCompraCombo>();
        foreach (IDictionary<string, object?> f in filas)
        {
            f.TryGetValue(colId, out var id);
            f.TryGetValue(colTexto, out var texto);
            lista.Add(new RegistroCompraCombo { Id = Convert.ToString(id, System.Globalization.CultureInfo.InvariantCulture), Texto = Convert.ToString(texto, System.Globalization.CultureInfo.InvariantCulture) });
        }
        return lista;
    }

    // ───────── líneas ─────────

    public async Task<IEnumerable<RegistroCompraLinea>> LineasAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraLinea>(SP.SCHEMAFinanzas.RegistroCompras.DetalleSearch, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Alta (sin NUM_SECUENCIA: el estándar asigna la siguiente línea) o modificación de una línea con los procedimientos ESTÁNDAR del detalle; el total y los descuentos de la línea se calculan si no se mandan.</summary>
    public async Task<WriteResult> LineaGuardarAsync(decimal NUM_REGISTRO, RegistroCompraLineaDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        var esAlta = dto.NUM_SECUENCIA is null or 0;
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_SECUENCIA", esAlta ? 0 : dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO", Nulo(dto.COD_PRODUCTO));
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", Nulo(dto.COD_UNIDAD_MEDIDA));
        p.Add("CAN_PRODUCTO_SECUDARIO", (decimal?)null);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", (string?)null);
        p.Add("COD_TIPO_DESCUENTO", dto.COD_TIPO_DESCUENTO);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("VAL_DESCUENTO", dto.VAL_DESCUENTO);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("NUM_SECUENCIA_PEDIDO_COMERCIAL", dto.NUM_SECUENCIA_PEDIDO_COMERCIAL);
        p.Add("COD_TIPO_ESTADO", (int?)null);
        p.Add("COD_ESTADO", (int?)null);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_CLASE_OPERACION_PEDIDO", Nulo(dto.COD_CLASE_OPERACION_PEDIDO));
        p.Add("COD_TIPO_OPERACION_PEDIDO", Nulo(dto.COD_TIPO_OPERACION_PEDIDO));
        p.Add("DES_PRODUCTO_COMPROBANTE", Nulo(dto.DES_PRODUCTO_COMPROBANTE));
        p.Add("COD_PRODUCTO_ENTIDAD", Nulo(dto.COD_PRODUCTO_ENTIDAD));
        p.Add("COD_UNIDAD_MEDIDA_COMPROBANTE", Nulo(dto.COD_UNIDAD_MEDIDA_COMPROBANTE));
        var sp = esAlta ? SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Insert : SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Update;
        if (esAlta) p.Add("NUM_SECUENCIA_GENERADA", dbType: DbType.Int32, direction: ParameterDirection.Output);
        try
        {
            await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);
            var seq = esAlta ? p.Get<int?>("NUM_SECUENCIA_GENERADA") : dto.NUM_SECUENCIA;
            return new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = (seq ?? 0).ToString() };
        }
        catch (SqlException ex) { return Error(ex, sp, p); }
    }

    public async Task<WriteResult> LineaDeleteAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        var p = new { NUM_REGISTRO, NUM_SECUENCIA };
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Delete, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.ComprobantesPagoDetalle.Delete, p); }
    }

    // ───────── órdenes ─────────

    public async Task<IEnumerable<RegistroCompraPedido>> PedidosAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraPedido>(SP.SCHEMAFinanzas.RegistroCompras.PedidosSearch, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RegistroCompraPedido>> PedidosDisponiblesAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraPedido>(SP.SCHEMAFinanzas.RegistroCompras.PedidosDisponibles, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RegistroCompraPedidoLinea>> PedidosLineasAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraPedidoLinea>(SP.SCHEMAFinanzas.RegistroCompras.PedidosDetalleDisponibles, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> PedidoAsociarAsync(decimal NUM_REGISTRO, RegistroCompraPedidoAsociarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION?.Trim());
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION?.Trim());
        p.Add("NUM_PEDIDO_COMERCIAL", dto.NUM_PEDIDO_COMERCIAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.PedidosAsociar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.PedidosAsociar, p); }
    }

    public async Task<WriteResult> PedidoDesasociarAsync(decimal NUM_REGISTRO, string codClaseOperacion, string codTipoOperacion, decimal numPedidoComercial)
    {
        using var conn = _db.CreateConnection();
        var p = new { NUM_REGISTRO, COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, NUM_PEDIDO_COMERCIAL = numPedidoComercial };
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.PedidosDesasociar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.PedidosDesasociar, p); }
    }

    // ───────── notas de crédito y débito por ítem ─────────

    public async Task<IEnumerable<RegistroCompraIncidenciaOrigen>> IncidenciasOrigenesAsync(decimal NUM_REGISTRO, string? search)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraIncidenciaOrigen>(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasOrigenes, new { NUM_REGISTRO, SEARCH = search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RegistroCompraIncidenciaLinea>> IncidenciasDisponiblesAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraIncidenciaLinea>(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasDisponibles, new { NUM_REGISTRO, NUM_REGISTRO_ORIGEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RegistroCompraIncidencia>> IncidenciasAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraIncidencia>(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasShowall, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> IncidenciasGuardarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciasGuardarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var inv = System.Globalization.CultureInfo.InvariantCulture;
        var xml = new System.Text.StringBuilder("<r>");
        foreach (var i in dto.Items ?? new())
        {
            xml.Append("<i s=\"").Append(i.NUM_SECUENCIA.ToString(inv)).Append("\" o=\"").Append(i.NUM_REGISTRO_ASOCIADO.ToString(inv))
               .Append("\" os=\"").Append(i.NUM_SECUENCIA_ASOCIADO.ToString(inv)).Append('"');
            if (i.CAN_APLICADA is { } c) xml.Append(" c=\"").Append(c.ToString(inv)).Append('"');
            if (i.VAL_APLICADO is { } v) xml.Append(" v=\"").Append(v.ToString(inv)).Append('"');
            xml.Append("/>");
        }
        xml.Append("</r>");
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("DES_XML", xml.ToString(), DbType.Xml);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuardar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = (dto.Items ?? new()).Count };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuardar, p); }
    }

    public async Task<WriteResult> IncidenciasAnulacionTotalAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ORIGEN, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_REGISTRO_ORIGEN", NUM_REGISTRO_ORIGEN);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasAnulacionTotal, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.IncidenciasAnulacionTotal, p); }
    }

    public async Task<WriteResult> IncidenciasAdicionarAsync(decimal NUM_REGISTRO, RegistroCompraIncidenciaAdicionarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var inv = System.Globalization.CultureInfo.InvariantCulture;
        var xml = new System.Text.StringBuilder("<r>");
        foreach (var i in dto.Items ?? new())
        {
            xml.Append("<i os=\"").Append(i.NUM_SECUENCIA_ASOCIADO.ToString(inv)).Append('"');
            if (i.CAN_APLICADA is { } c) xml.Append(" c=\"").Append(c.ToString(inv)).Append('"');
            if (i.VAL_APLICADO is { } v) xml.Append(" v=\"").Append(v.ToString(inv)).Append('"');
            xml.Append("/>");
        }
        xml.Append("</r>");
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_REGISTRO_ORIGEN", dto.NUM_REGISTRO_ORIGEN);
        p.Add("DES_XML", xml.ToString(), DbType.Xml);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasAdicionar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = (dto.Items ?? new()).Count };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.IncidenciasAdicionar, p); }
    }

    public async Task<IEnumerable<RegistroCompraIncidenciaGuia>> IncidenciasGuiasDisponiblesAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraIncidenciaGuia>(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuiasDisponibles, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RegistroCompraAnticipo>> AnticiposDisponiblesAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraAnticipo>(SP.SCHEMAFinanzas.RegistroCompras.AnticiposDisponibles, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RegistroCompraAnticipo>> AnticiposAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraAnticipo>(SP.SCHEMAFinanzas.RegistroCompras.Anticipos, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> AnticipoAsociarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_REGISTRO_ANTICIPO", NUM_REGISTRO_ANTICIPO);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.AnticipoAsociar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.AnticipoAsociar, p); }
    }

    public async Task<WriteResult> AnticipoQuitarAsync(decimal NUM_REGISTRO, decimal NUM_REGISTRO_ANTICIPO)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_REGISTRO_ANTICIPO", NUM_REGISTRO_ANTICIPO);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.AnticipoQuitar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.AnticipoQuitar, p); }
    }

    public async Task<IEnumerable<RegistroCompraIncidenciaGuiaAdjunta>> IncidenciasGuiasAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraIncidenciaGuiaAdjunta>(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuias, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RegistroCompraIncidenciaLineaGuia?> IncidenciasLineaGuiaAsync(decimal NUM_REGISTRO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegistroCompraIncidenciaLineaGuia>(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasLineaGuia, new { NUM_REGISTRO, NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> IncidenciasGuiaAsociarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuiaAsociar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuiaAsociar, p); }
    }

    public async Task<WriteResult> IncidenciasGuiaQuitarAsync(decimal NUM_REGISTRO, decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("NUM_MOVIMIENTO_ALMACEN", NUM_MOVIMIENTO_ALMACEN);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuiaQuitar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.IncidenciasGuiaQuitar, p); }
    }

    public async Task<WriteResult> AutovincularAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        var p = new { NUM_REGISTRO };
        try
        {
            var n = await conn.QueryFirstOrDefaultAsync<int?>(SP.SCHEMAFinanzas.RegistroCompras.Autovincular, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = n ?? 0 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMAFinanzas.RegistroCompras.Autovincular, p); }
    }

    // ───────── conciliación y flujo ─────────

    public async Task<IEnumerable<RegistroCompraHallazgo>> ConciliacionAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroCompraHallazgo>(SP.SCHEMAFinanzas.RegistroCompras.ConciliacionSearch, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<(WriteResult Resultado, RegistroCompraValidacion? Validacion)> ValidarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("COD_USUARIO_VALIDACION", codUsuario);
        p.Add("COD_ESTACION_VALIDACION", codEstacion);
        p.Add("COD_ESTADO_ALIAS_CONCILIACION", dbType: DbType.String, size: 3, direction: ParameterDirection.Output);
        try
        {
            var hallazgos = (await conn.QueryAsync<RegistroCompraHallazgo>(SP.SCHEMAFinanzas.RegistroCompras.Validar, p, commandType: CommandType.StoredProcedure)).ToList();
            var estado = p.Get<string>("COD_ESTADO_ALIAS_CONCILIACION") ?? "";
            return (new WriteResult { Estado = "OK", AffectedRows = hallazgos.Count },
                    new RegistroCompraValidacion { NUM_REGISTRO = NUM_REGISTRO, COD_ESTADO_ALIAS_CONCILIACION = estado, HALLAZGOS = hallazgos });
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMAFinanzas.RegistroCompras.Validar, p), null); }
    }

    private async Task<WriteResult> Ejecuta(string sp, DynamicParameters p)
    {
        using var conn = _db.CreateConnection();
        try
        {
            await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, sp, p); }
    }

    public Task<WriteResult> EnviarAprobacionAsync(decimal NUM_REGISTRO, string? justificacion, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("DES_JUSTIFICACION", Nulo(justificacion));
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return Ejecuta(SP.SCHEMAFinanzas.RegistroCompras.EnviarAprobacion, p);
    }

    public Task<WriteResult> AprobarAsync(decimal NUM_REGISTRO, string? justificacion, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        p.Add("DES_JUSTIFICACION", Nulo(justificacion));
        return Ejecuta(SP.SCHEMAFinanzas.RegistroCompras.Aprobar, p);
    }

    public Task<WriteResult> ContabilizarAsync(decimal NUM_REGISTRO, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return Ejecuta(SP.SCHEMAFinanzas.RegistroCompras.Contabilizar, p);
    }

    public Task<WriteResult> AnularAsync(decimal NUM_REGISTRO, string? justificacion, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO", NUM_REGISTRO);
        p.Add("DES_JUSTIFICACION", Nulo(justificacion));
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return Ejecuta(SP.SCHEMAFinanzas.RegistroCompras.Anular, p);
    }

    // ───────── XML ─────────

    public async Task<RegistroCompraXml?> XmlAsync(decimal NUM_REGISTRO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegistroCompraXml>(SP.SCHEMAFinanzas.RegistroCompras.XmlShowbyid, new { NUM_REGISTRO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<(WriteResult Resultado, RegistroCompraImportado? Importado)> ImportarXmlAsync(RegistroCompraImportarXmlDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION?.Trim());
        p.Add("DES_XML", dto.DES_XML, DbType.String);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("DES_NOMBRE_ARCHIVO", Nulo(dto.DES_NOMBRE_ARCHIVO));
        p.Add("COD_TIPO_OPERACION", Nulo(dto.COD_TIPO_OPERACION));
        p.Add("VALIDAR", dto.VALIDAR);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<RegistroCompraImportado>(SP.SCHEMAFinanzas.RegistroCompras.ImportarXml, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = (r?.NUM_REGISTRO ?? 0).ToString("0") }, r);
        }
        catch (SqlException ex)
        {
            // el XML completo no va en la sentencia que se muestra
            var sentencia = SqlSentenceHelper.Build(SP.SCHEMAFinanzas.RegistroCompras.ImportarXml, new { dto.COD_CLASE_OPERACION, DES_XML = "(" + dto.DES_XML?.Length + " caracteres)", dto.DES_NOMBRE_ARCHIVO });
            return (new WriteResult { Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = SP.SCHEMAFinanzas.RegistroCompras.ImportarXml, SqlSentence = sentencia }, null);
        }
    }

    public async Task<RegistroComprasConfigXml?> ConfigXmlAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegistroComprasConfigXml>(SP.SCHEMAEstandar.ConfiguracionGeneral.Showall, commandType: CommandType.StoredProcedure);
    }
}
