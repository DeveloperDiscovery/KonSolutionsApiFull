using System.Data;
using System.Globalization;
using System.Xml.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TrasladosUbicacionesRepository : ITrasladosUbicacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public TrasladosUbicacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TrasladoUbicacionesTipo>> TiposAsync(string codClaseMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TrasladoUbicacionesTipo>(
            SP.SCHEMALogistica.TrasladoUbicaciones.Tipos,
            new { COD_CLASE_MOVIMIENTO = codClaseMovimiento },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Tres resultsets, siempre en este orden: racks, casilleros y ubicaciones sueltas.</summary>
    public async Task<TrasladoUbicacionesMapa> MapaAsync(string codAlmacen)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.TrasladoUbicaciones.Mapa,
            new { COD_ALMACEN = codAlmacen },
            commandType: CommandType.StoredProcedure);
        return new TrasladoUbicacionesMapa
        {
            Racks      = (await multi.ReadAsync<TrasladoUbicacionesRack>()).ToList(),
            Casilleros = (await multi.ReadAsync<TrasladoUbicacionesCasillero>()).ToList(),
            Sueltas    = (await multi.ReadAsync<TrasladoUbicacionesUbicacion>()).ToList()
        };
    }

    public async Task<IEnumerable<TrasladoUbicacionesStock>> StockAsync(string codAlmacen, string codUbicacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TrasladoUbicacionesStock>(
            SP.SCHEMALogistica.TrasladoUbicaciones.Stock,
            new { COD_ALMACEN = codAlmacen, COD_UBICACION_ALMACEN = codUbicacion },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Graba el traslado en una sola llamada (el SP lo hace en una transacción).
    ///
    /// Las líneas viajan como XML y no como JSON: OPENJSON depende del nivel de compatibilidad de
    /// la base. Los números van con punto decimal, sin importar la cultura del servidor.
    ///
    /// Se ejecuta con Execute y los números se leen de los parámetros de salida: el SP, al crear
    /// cada línea, deja pasar el SELECT de secuencia del SP de detalle, y lo único que interesa es
    /// el resultado final.</summary>
    public async Task<TrasladoUbicacionesResultado> RegistraAsync(TrasladoUbicacionesRegistraDto dto, string codUsuario, string codEstacion)
    {
        static string T(string? s) => (s ?? "").Trim();
        static string N(decimal? v) => (v ?? 0).ToString(CultureInfo.InvariantCulture);
        var xml = new XElement("lineas",
            dto.Lineas.Select(l => new XElement("l",
                new XAttribute("p",    T(l.COD_PRODUCTO)),
                new XAttribute("lote", T(l.NUM_LOTE_CONTROL)),
                new XAttribute("proc", T(l.COD_PROCESO)),
                new XAttribute("op",   l.NUM_ORDEN_PRODUCCION ?? 0),
                new XAttribute("item", l.NUM_ORDEN_PRODUCCION_ITEM ?? 0),
                new XAttribute("uni",  l.NUM_ORDEN_PRODUCCION_UNIDAD ?? 0),
                new XAttribute("ori",  T(l.COD_UBICACION_ORIGEN)),
                new XAttribute("des",  T(l.COD_UBICACION_DESTINO)),
                new XAttribute("can",  N(l.CAN_PRODUCTO)),
                new XAttribute("res",  N(l.CAN_RESERVADA_MOVER ?? 0)))));

        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", T(dto.COD_ALMACEN));
        p.Add("COD_CLASE_MOVIMIENTO", T(dto.COD_CLASE_MOVIMIENTO));
        p.Add("COD_TIPO_MOVIMIENTO", T(dto.COD_TIPO_MOVIMIENTO));
        p.Add("FEC_MOVIMENTO_ALMACEN", (dto.FEC_MOVIMENTO_ALMACEN ?? DateTime.Today).Date, DbType.Date);
        p.Add("DES_OBSERVACION", string.IsNullOrWhiteSpace(dto.DES_OBSERVACION) ? null : dto.DES_OBSERVACION.Trim());
        p.Add("XML_LINEAS", xml.ToString(SaveOptions.DisableFormatting), DbType.Xml);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_MOVIMIENTO_SALIDA", dbType: DbType.Decimal, precision: 15, scale: 0, direction: ParameterDirection.Output);
        p.Add("NUM_MOVIMIENTO_INGRESO", dbType: DbType.Decimal, precision: 15, scale: 0, direction: ParameterDirection.Output);
        await conn.ExecuteAsync(SP.SCHEMALogistica.TrasladoUbicaciones.Registra, p, commandType: CommandType.StoredProcedure);
        return new TrasladoUbicacionesResultado
        {
            NUM_MOVIMIENTO_SALIDA  = p.Get<decimal?>("NUM_MOVIMIENTO_SALIDA"),
            NUM_MOVIMIENTO_INGRESO = p.Get<decimal?>("NUM_MOVIMIENTO_INGRESO")
        };
    }

    public async Task<IEnumerable<TrasladoUbicacionesHistorial>> HistorialAsync(string codAlmacen, DateTime? desde, DateTime? hasta)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TrasladoUbicacionesHistorial>(
            SP.SCHEMALogistica.TrasladoUbicaciones.Historial,
            new { COD_ALMACEN = codAlmacen, FEC_INICIAL = desde?.Date, FEC_FINAL = hasta?.Date },
            commandType: CommandType.StoredProcedure);
    }
}
