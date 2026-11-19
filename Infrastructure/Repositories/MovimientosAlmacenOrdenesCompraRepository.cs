using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Las cuatro consultas de la pestaña "Órdenes de Compra" de un movimiento.
///
/// Lee las cuatro grillas y ESCRIBE las tres acciones de la pestaña: asignar, quitar y
/// grabar el lote y la ubicación elegidos.
///
/// Las tres escrituras van por SP propios que resuelven todo del lado de la base —crear la
/// cabecera del movimiento si falta, insertar el detalle, mover el stock, actualizar lo
/// atendido del pedido—. Acá no hay lógica: se pasan los parámetros y se traduce el error.</summary>
public class MovimientosAlmacenOrdenesCompraRepository : IMovimientosAlmacenOrdenesCompraRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenOrdenesCompraRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Órdenes de compra que este movimiento PODRÍA atender. El SP resuelve solo la
    /// entidad a partir del movimiento, así que acá alcanza con el número.</summary>
    public async Task<IEnumerable<MovimientoAlmacenOrdenCompra>> DisponiblesAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenOrdenCompra>(
            SP.SCHEMALogistica.MovimientosAlmacenOrdenesCompra.Disponibles,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Líneas pendientes de las órdenes elegidas.
    ///
    /// El SP recibe la lista de pedidos como UNA cadena y la resuelve con CHARINDEX, así que
    /// el separador no es libre: se manda tal como el SP lo espera y quien llama no tiene que
    /// saberlo. Si la lista viene vacía no se consulta: CHARINDEX contra una cadena vacía
    /// devuelve 0 para todo, pero pedirlo igual sería un viaje al servidor para nada.</summary>
    public async Task<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>> DisponiblesDetalleAsync(
        decimal NUM_MOVIMIENTO_ALMACEN, string codClaseOperacion, string codTipoOperacion, string lstPedidos)
    {
        if (string.IsNullOrWhiteSpace(lstPedidos)) return Array.Empty<MovimientoAlmacenOrdenCompraDetalle>();

        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenOrdenCompraDetalle>(
            SP.SCHEMALogistica.MovimientosAlmacenOrdenesCompra.DisponiblesDetalle,
            new
            {
                NUM_MOVIMIENTO_ALMACEN,
                COD_CLASE_OPERACION      = codClaseOperacion,
                COD_TIPO_OPERACION       = codTipoOperacion,
                LST_PEDIDOS_COMERCIALES  = lstPedidos
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MovimientoAlmacenOrdenCompra>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenOrdenCompra>(
            SP.SCHEMALogistica.MovimientosAlmacenOrdenesCompra.Asignados,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MovimientoAlmacenOrdenCompraDetalle>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenOrdenCompraDetalle>(
            SP.SCHEMALogistica.MovimientosAlmacenOrdenesCompra.AsignadosDetalle,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    // ── Escritura ────────────────────────────────────────────────────────────────

    /// <summary>Asigna al movimiento las líneas de la cadena.
    ///
    /// La cadena la arma ListaPedidoSecuencia y llega ya formada: acá NO se vuelve a tocar.
    /// Es a propósito — si el formato se armara en dos lugares, el día que cambie uno el
    /// otro seguiría mandando lo viejo y el SP devolvería una lista vacía sin error.</summary>
    public async Task<WriteResult> AsignarAsync(decimal numMovimiento, string codClaseOperacion, string codTipoOperacion,
        string lineas, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        // La clase y el tipo son OBLIGATORIOS desde el script 47. Sin ellos el SP no puede
        // distinguir el pedido 2 de Órdenes de Compra del pedido 2 de Proformas: la
        // numeración se reinicia por clase de operación.
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("DES_PEDIDO_COMERCIAL_SECUENCIA", lineas);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return await EjecutarAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenOrdenesCompra.Asigna, p);
    }

    public async Task<WriteResult> QuitarAsync(decimal numMovimiento, string codClaseOperacion, string codTipoOperacion,
        string lineas, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        // La clase y el tipo son OBLIGATORIOS desde el script 47. Sin ellos el SP no puede
        // distinguir el pedido 2 de Órdenes de Compra del pedido 2 de Proformas: la
        // numeración se reinicia por clase de operación.
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("DES_PEDIDO_COMERCIAL_SECUENCIA", lineas);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return await EjecutarAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenOrdenesCompra.Desasigna, p);
    }

    /// <summary>Graba el lote y la ubicación en la LÍNEA DEL PEDIDO, que es de donde ASIGNA
    /// los lee después. Ese es todo el circuito: el usuario elige, esto lo deja en el pedido,
    /// y al asignar el SP lo levanta de ahí.</summary>
    public async Task<WriteResult> GuardarLoteUbicacionAsync(string codClaseOperacion, string codTipoOperacion,
        decimal numPedidoComercial, int numSecuencia, string? numLoteControl, string? codUbicacionAlmacen)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        p.Add("NUM_PEDIDO_COMERCIAL", numPedidoComercial);
        p.Add("NUM_SECUENCIA", numSecuencia);
        // Cadena vacía -> NULL. En estas dos columnas el vacío no es un valor: una ubicación
        // "" no existe en ALMACENES_UBICACIONES_ALMACEN y dispararía un 547 al grabar.
        p.Add("NUM_LOTE_CONTROL", string.IsNullOrWhiteSpace(numLoteControl) ? null : numLoteControl);
        p.Add("COD_UBICACION_ALMACEN", string.IsNullOrWhiteSpace(codUbicacionAlmacen) ? null : codUbicacionAlmacen);
        return await EjecutarAsync(conn, SP.SCHEMAComercial.PedidosComercialesDetalle.UpdateOtros, p);
    }

    /// <summary>Ejecuta y traduce el SqlException al WriteResult que espera el front. Mismo
    /// patrón que PedidosComercialesRequerimientosRepository.</summary>
    private static async Task<WriteResult> EjecutarAsync(IDbConnection conn, string procedimiento, DynamicParameters p)
    {
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(procedimiento, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR",
                Mensaje = ex.Message,
                ErrorNumber = ex.Number,
                ErrorProcedure = procedimiento,
                SqlSentence = SqlSentenceHelper.Build(procedimiento, p)
            };
        }
    }
}
