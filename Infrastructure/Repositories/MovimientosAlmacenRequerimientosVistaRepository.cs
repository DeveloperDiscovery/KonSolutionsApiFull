using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Las cuatro consultas de la pestaña "Requerimientos" de un movimiento.
///
/// Se llama ...VistaRepository y no ...Repository porque ya existe
/// MovimientosAlmacenRequerimientosStockRepository, que es el de la tabla de enlace
/// (alta y baja). Este es sólo de lectura y para la pantalla; dos cosas distintas con
/// nombres distintos.</summary>
public class MovimientosAlmacenRequerimientosVistaRepository : IMovimientosAlmacenRequerimientosVistaRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenRequerimientosVistaRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Requerimientos que se pueden atender. SIN PARÁMETROS, a diferencia del lado
    /// de órdenes de compra: el SP no filtra por movimiento —devuelve todos los de consumo
    /// de producción reservados—, así que la lista es la misma para cualquier movimiento.</summary>
    public async Task<IEnumerable<MovimientoAlmacenRequerimiento>> DisponiblesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenRequerimiento>(
            SP.SCHEMALogistica.MovimientosAlmacenRequerimientos.Disponibles,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MovimientoAlmacenRequerimiento>> AsignadosAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenRequerimiento>(
            SP.SCHEMALogistica.MovimientosAlmacenRequerimientos.Asignados,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MovimientoAlmacenRequerimientoDetalle>> AsignadosDetalleAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoAlmacenRequerimientoDetalle>(
            SP.SCHEMALogistica.MovimientosAlmacenRequerimientos.AsignadosDetalle,
            new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Cobertura de UN requerimiento: llama al SP de detalle de disponibles, que desde
    /// que recibe el movimiento devuelve DOS resultados —las líneas con su cobertura y el
    /// stock que se despacharía, con la reserva de origen—.
    ///
    /// OJO: el SP no es de sólo lectura. Borra e inserta en
    /// LOGISTICA.MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DETALLE_SELECCIONADO para el par
    /// movimiento + requerimiento en cada llamada. Por eso el endpoint es POST.</summary>
    public async Task<MovimientoAlmacenRequerimientoCobertura> CoberturaAsync(
        decimal numMovimiento, int numRequerimiento, string? criterio)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.MovimientosAlmacenRequerimientos.DisponiblesDetalle,
            new
            {
                NUM_MOVIMIENTO_ALMACEN = numMovimiento,
                NUM_REQUERIMIENTO_STOCK = numRequerimiento,
                COD_CRITERIO = string.IsNullOrWhiteSpace(criterio) ? "MAYOR" : criterio
            },
            commandType: CommandType.StoredProcedure);

        return new MovimientoAlmacenRequerimientoCobertura
        {
            Lineas = (await multi.ReadAsync<MovimientoAlmacenRequerimientoCoberturaLinea>()).ToList(),
            Stock  = (await multi.ReadAsync<MovimientoAlmacenRequerimientoCoberturaStock>()).ToList()
        };
    }

    // ── Escritura ────────────────────────────────────────────────────────────────

    /// <summary>Asigna al movimiento las líneas de la cadena.
    ///
    /// La cadena la arma ListaPedidoSecuencia y llega ya formada: acá NO se vuelve a tocar.
    /// Es la MISMA clase y el MISMO formato que usa Órdenes de Compra, a propósito: dos
    /// formatos conviviendo es exactamente lo que rompió el DESASIGNA de comerciales.
    ///
    /// El SP toma el stock de LOGISTICA.MOVIMIENTOS_ALMACEN_REQUERIMIENTO_STOCK_DETALLE_SELECCIONADO
    /// (lo escribió CoberturaAsync) y sólo de las líneas de la cadena. El criterio se conserva
    /// porque es parte de la firma del SP, pero ya no decide la ubicación.</summary>
    public async Task<WriteResult> AsignarAsync(decimal numMovimiento, string lineas, string criterio,
        string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        p.Add("DES_REQUERIMIENTO_SECUENCIA", lineas);
        p.Add("COD_CRITERIO", string.IsNullOrWhiteSpace(criterio) ? "MAYOR" : criterio);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return await EjecutarAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenRequerimientos.Asigna, p);
    }

    /// <summary>Quita del movimiento las líneas de la cadena. NO lleva criterio: el SP revierte
    /// los detalles del movimiento ligados a cada línea (stock, reserva y atención del
    /// requerimiento), así que no hay nada que volver a elegir.</summary>
    public async Task<WriteResult> QuitarAsync(decimal numMovimiento, string lineas,
        string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        p.Add("DES_REQUERIMIENTO_SECUENCIA", lineas);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return await EjecutarAsync(conn, SP.SCHEMALogistica.MovimientosAlmacenRequerimientos.Desasigna, p);
    }

    /// <summary>Ejecuta y traduce el SqlException al WriteResult que espera el front. Mismo
    /// patrón que MovimientosAlmacenOrdenesCompraRepository.</summary>
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
