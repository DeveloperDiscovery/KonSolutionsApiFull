using System.Data;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfirmacionRecepcionRepository : IConfirmacionRecepcionRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfirmacionRecepcionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RecepcionPendiente>> PendientesAsync(string codRol, string codUsuario, string? codAlmacen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RecepcionPendiente>(
            SP.SCHEMALogistica.ConfirmacionRecepcion.Pendientes,
            new { COD_ROL_USUARIO = codRol, COD_USUARIO = codUsuario, COD_ALMACEN = codAlmacen },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RecepcionLinea>> DetalleAsync(decimal numMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RecepcionLinea>(
            SP.SCHEMALogistica.ConfirmacionRecepcion.Detalle,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Dos resultsets, siempre en este orden: el estado RECHAZADO con su tipo de motivo y
    /// los motivos de ese tipo.</summary>
    public async Task<RecepcionMotivosRechazo> MotivosRechazoAsync(decimal numMovimiento)
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.ConfirmacionRecepcion.MotivosRechazo,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento },
            commandType: CommandType.StoredProcedure);
        return new RecepcionMotivosRechazo
        {
            Estado  = (await multi.ReadAsync<RecepcionEstadoRechazo>()).FirstOrDefault() ?? new(),
            Motivos = (await multi.ReadAsync<RecepcionMotivo>()).ToList()
        };
    }

    /// <summary>Confirma en una sola llamada (el SP lo hace en una transacción).
    ///
    /// Las líneas viajan como XML y no como JSON: OPENJSON depende del nivel de compatibilidad de
    /// la base (mismo criterio que el traslado entre ubicaciones).
    ///
    /// Se ejecuta con Execute y el número se lee del parámetro de salida: los SP de cabecera y de
    /// líneas que usa por dentro dejan pasar sus SELECT de número y de secuencia.</summary>
    public async Task<RecepcionResultado> ConfirmarAsync(RecepcionConfirmarDto dto, string codUsuario, string codEstacion)
    {
        var xml = new XElement("lineas",
            dto.Lineas.Select(l => new XElement("l",
                new XAttribute("sec", l.NUM_SECUENCIA ?? 0),
                new XAttribute("ubi", (l.COD_UBICACION_DESTINO ?? "").Trim()))));

        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_ROL_USUARIO", (dto.COD_ROL_USUARIO ?? "").Trim());
        p.Add("XML_LINEAS", xml.ToString(SaveOptions.DisableFormatting), DbType.Xml);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_MOVIMIENTO_INGRESO", dbType: DbType.Decimal, precision: 15, scale: 0, direction: ParameterDirection.Output);
        // Las etiquetas leídas al recibir (opcional): sin ellas el parámetro no se manda y el SP se comporta como antes.
        if (dto.Etiquetas is { Count: > 0 })
        {
            var etiquetas = new XElement("etiquetas",
                dto.Etiquetas.Select(e => new XElement("e",
                    new XAttribute("sec", e.NUM_SECUENCIA ?? 0),
                    new XAttribute("etq", (e.COD_STICKER_RFID ?? "").Trim()),
                    new XAttribute("can", e.CAN_PRODUCTO ?? 1m),
                    new XAttribute("f", e.FLG_FORZAR ? 1 : 0))));
            p.Add("XML_ETIQUETAS", etiquetas.ToString(SaveOptions.DisableFormatting), DbType.Xml);
        }
        await conn.ExecuteAsync(SP.SCHEMALogistica.ConfirmacionRecepcion.Confirmar, p, commandType: CommandType.StoredProcedure);
        return new RecepcionResultado { NUM_MOVIMIENTO_ALMACEN = p.Get<decimal?>("NUM_MOVIMIENTO_INGRESO") };
    }

    /// <summary>Rechaza en una sola llamada: el ingreso por anulación de salida en el origen, los
    /// estados, el motivo y la observación, en una transacción.</summary>
    public async Task<RecepcionResultado> RechazarAsync(RecepcionRechazarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_ROL_USUARIO", (dto.COD_ROL_USUARIO ?? "").Trim());
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("DES_OBSERVACION", string.IsNullOrWhiteSpace(dto.DES_OBSERVACION) ? null : dto.DES_OBSERVACION.Trim());
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_MOVIMIENTO_ANULACION", dbType: DbType.Decimal, precision: 15, scale: 0, direction: ParameterDirection.Output);
        await conn.ExecuteAsync(SP.SCHEMALogistica.ConfirmacionRecepcion.Rechazar, p, commandType: CommandType.StoredProcedure);
        return new RecepcionResultado { NUM_MOVIMIENTO_ALMACEN = p.Get<decimal?>("NUM_MOVIMIENTO_ANULACION") };
    }

    /// <summary>Por cada línea de la salida: si su producto se lee por RFID, lo enviado y lo que la salida tiene etiquetado,
    /// más el requisito de lectura del tipo de ingreso que se generará.</summary>
    public async Task<IEnumerable<RecepcionLecturaLinea>> LecturaContextoAsync(decimal numMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RecepcionLecturaLinea>(
            SP.SCHEMALogistica.ConfirmacionRecepcion.LecturaContexto,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Las etiquetas que la salida leyó (todas, o solo las de una línea): las que pasan al ingreso si la recepción se confirma sin leerlas.
    /// Es el mismo listado de las lecturas de un movimiento (PROC_..._DETALLE_CONTEO_RFID_SHOWALL), pero se pide desde acá para que lo vea
    /// quien recibe con el permiso de la recepción, sin necesitar el del detalle de movimientos.</summary>
    public async Task<IEnumerable<MovimientosAlmacenEtiqueta>> EtiquetasAsync(decimal numMovimiento, int? numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacenEtiqueta>(
            SP.SCHEMALogistica.MovimientosAlmacenDetalleConteoRfid.Showall,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Revisa una etiqueta contra la salida SIN grabar. Si el SP la rechaza no se propaga como excepción sino como
    /// un resultado con ErrorNumber: la pantalla decide con ese número si pide el peso (50708) o pregunta si se fuerza (50706).</summary>
    public async Task<RecepcionLecturaResultado> LeerAsync(RecepcionLecturaDto dto)
    {
        XElement? leidas = null;
        if (dto.Leidas is { Count: > 0 })
            leidas = new XElement("leidas", dto.Leidas.Select(l => new XElement("l",
                new XAttribute("sec", l.NUM_SECUENCIA ?? 0), new XAttribute("can", l.CAN_PRODUCTO ?? 0m))));
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_STICKER_RFID", (dto.COD_STICKER_RFID ?? "").Trim());
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("FLG_FORZAR", dto.FLG_FORZAR);
        p.Add("XML_LEIDAS", leidas?.ToString(SaveOptions.DisableFormatting), DbType.Xml);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<RecepcionLecturaResultado>(
                SP.SCHEMALogistica.ConfirmacionRecepcion.Leer, p, commandType: CommandType.StoredProcedure);
            return r ?? new RecepcionLecturaResultado { ErrorNumber = -1 };
        }
        catch (SqlException ex)
        {
            return new RecepcionLecturaResultado { ErrorNumber = ex.Number, DES_ADVERTENCIA = ex.Message };
        }
    }
}
