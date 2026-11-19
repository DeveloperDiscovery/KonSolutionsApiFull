using System.Data;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RfidEmisionesRepository : IRfidEmisionesRepository
{
    private readonly IDbConnectionFactory _db;
    public RfidEmisionesRepository(IDbConnectionFactory db) => _db = db;

    // ───────── lectura ─────────

    public async Task<IEnumerable<RfidEmision>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RfidEmision>(SP.SCHEMARfid.Emisiones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RfidEmision?> ShowbyidAsync(int NUM_EMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RfidEmision>(
            SP.SCHEMARfid.Emisiones.Showbyid, new { NUM_EMISION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidEmision>> SearchAsync(RfidEmisionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("COD_ORIGEN", Nulo(dto.COD_ORIGEN));
        p.Add("COD_ESTADO_EMISION", Nulo(dto.COD_ESTADO_EMISION));
        p.Add("FEC_DESDE", dto.FEC_DESDE?.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA?.Date, DbType.Date);
        return await conn.QueryAsync<RfidEmision>(SP.SCHEMARfid.Emisiones.Search, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidEmisionDetalle>> DetalleAsync(int NUM_EMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RfidEmisionDetalle>(
            SP.SCHEMARfid.Emisiones.DetalleShowall, new { NUM_EMISION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidEtiqueta>> EtiquetasAsync(int NUM_EMISION, int? NUM_ITEM, string? COD_ESTADO_ETIQUETA, int? TOP)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", NUM_EMISION);
        p.Add("NUM_ITEM", NUM_ITEM);
        p.Add("COD_ESTADO_ETIQUETA", Nulo(COD_ESTADO_ETIQUETA));
        p.Add("TOP", TOP);
        return await conn.QueryAsync<RfidEtiqueta>(SP.SCHEMARfid.Etiquetas.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidEtiqueta>> EtiquetasSearchAsync(RfidEtiquetaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("COD_ESTADO_ETIQUETA", Nulo(dto.COD_ESTADO_ETIQUETA));
        p.Add("NUM_EMISION", dto.NUM_EMISION);
        p.Add("COD_PRODUCTO", Nulo(dto.COD_PRODUCTO));
        p.Add("TOP", dto.TOP);
        return await conn.QueryAsync<RfidEtiqueta>(SP.SCHEMARfid.Etiquetas.Search, p, commandType: CommandType.StoredProcedure);
    }

    // ───────── emisión ─────────

    /// <summary>Las líneas viajan como XML (igual que los almacenes de un cierre): &lt;lineas&gt;&lt;l sec="1" n="4"/&gt;...&lt;/lineas&gt;.
    /// Un rollo es un &lt;e cant="30.5"/&gt; dentro de su línea. Los decimales van con punto, sin importar la cultura del servidor.</summary>
    public static string ArmaXmlLineas(IEnumerable<RfidEmisionLineaDto> lineas)
    {
        static string Num(decimal d) => d.ToString("0.########", CultureInfo.InvariantCulture);
        var raiz = new XElement("lineas");
        foreach (var l in lineas)
        {
            var e = new XElement("l");
            if (l.SEC.HasValue) e.Add(new XAttribute("sec", l.SEC.Value));
            if (!string.IsNullOrWhiteSpace(l.UBI)) e.Add(new XAttribute("ubi", l.UBI.Trim()));
            if (!string.IsNullOrWhiteSpace(l.PROD)) e.Add(new XAttribute("prod", l.PROD.Trim()));
            if (!string.IsNullOrWhiteSpace(l.LOTE)) e.Add(new XAttribute("lote", l.LOTE.Trim()));
            if (!string.IsNullOrWhiteSpace(l.PROC)) e.Add(new XAttribute("proc", l.PROC.Trim()));
            if (l.OP.HasValue) e.Add(new XAttribute("op", l.OP.Value));
            if (l.ITEM.HasValue) e.Add(new XAttribute("item", l.ITEM.Value));
            if (l.UNI.HasValue) e.Add(new XAttribute("uni", l.UNI.Value));
            if (l.ROLLOS is { Count: > 0 })
                foreach (var r in l.ROLLOS) e.Add(new XElement("e", new XAttribute("cant", Num(r))));
            else
            {
                if (l.N.HasValue) e.Add(new XAttribute("n", l.N.Value));
                if (l.CANT.HasValue) e.Add(new XAttribute("cant", Num(l.CANT.Value)));
            }
            raiz.Add(e);
        }
        return raiz.ToString(SaveOptions.DisableFormatting);
    }

    public async Task<WriteResult> InsertAsync(RfidEmisionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ORIGEN", dto.COD_ORIGEN?.Trim().ToUpperInvariant());
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("COD_PRODUCTO_EQUIPO", Nulo(dto.COD_PRODUCTO_EQUIPO));
        p.Add("DES_OBSERVACION", Nulo(dto.DES_OBSERVACION));
        p.Add("XML_LINEAS", ArmaXmlLineas(dto.LINEAS ?? new()), DbType.Xml);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<RfidEmisionCreada>(
                SP.SCHEMARfid.Emisiones.Insert, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = r?.CAN_ETIQUETAS ?? 0, Codigo = (r?.NUM_EMISION ?? 0).ToString() };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMARfid.Emisiones.Insert, p); }
    }

    public async Task<WriteResult> UpdateAsync(RfidEmisionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", dto.NUM_EMISION);
        p.Add("COD_PRODUCTO_EQUIPO", Nulo(dto.COD_PRODUCTO_EQUIPO));
        p.Add("DES_OBSERVACION", Nulo(dto.DES_OBSERVACION));
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario);
        p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMARfid.Emisiones.Update, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMARfid.Emisiones.Update, p); }
    }

    public async Task<WriteResult> AnularAsync(int NUM_EMISION, RfidAnularDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", NUM_EMISION);
        p.Add("DES_MOTIVO_ANULACION", dto.DES_MOTIVO_ANULACION?.Trim());
        p.Add("COD_USUARIO_ANULACION", codUsuario);
        p.Add("COD_ESTACION_ANULACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMARfid.Emisiones.Anular, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMARfid.Emisiones.Anular, p); }
    }

    public async Task<WriteResult> DeleteAsync(int NUM_EMISION)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", NUM_EMISION);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMARfid.Emisiones.Delete, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMARfid.Emisiones.Delete, p); }
    }

    // ───────── etiquetas ─────────

    public async Task<(WriteResult Resultado, RfidEnrolado? Enrolado)> EnrolarAsync(int NUM_EMISION, RfidEnrolarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", NUM_EMISION);
        p.Add("COD_TID", dto.COD_TID?.Trim());
        p.Add("NUM_ITEM", dto.NUM_ITEM);
        p.Add("NUM_ETIQUETA", dto.NUM_ETIQUETA);
        p.Add("COD_PRODUCTO_EQUIPO", Nulo(dto.COD_PRODUCTO_EQUIPO));
        p.Add("COD_USUARIO_ENROLAMIENTO", codUsuario);
        p.Add("COD_ESTACION_ENROLAMIENTO", codEstacion);
        try
        {
            var e = await conn.QueryFirstOrDefaultAsync<RfidEnrolado>(
                SP.SCHEMARfid.Etiquetas.Enrolar, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = e?.COD_STICKER_RFID ?? string.Empty }, e);
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMARfid.Etiquetas.Enrolar, p), null); }
    }

    public async Task<WriteResult> GrabadaAsync(RfidGrabadaDto dto, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TID", dto.COD_TID?.Trim());
        p.Add("COD_EPC_LEIDO", Nulo(dto.COD_EPC_LEIDO));
        p.Add("COD_USUARIO_GRABACION", codUsuario);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMARfid.Etiquetas.Grabada, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMARfid.Etiquetas.Grabada, p); }
    }

    public async Task<(WriteResult Resultado, RfidEtiquetaAnulada? Anulada)> AnularEtiquetaAsync(
        int NUM_EMISION, int NUM_ITEM, int NUM_ETIQUETA, RfidEtiquetaAnularDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", NUM_EMISION);
        p.Add("NUM_ITEM", NUM_ITEM);
        p.Add("NUM_ETIQUETA", NUM_ETIQUETA);
        p.Add("DES_MOTIVO_ANULACION", dto.DES_MOTIVO_ANULACION?.Trim());
        p.Add("FLG_REPONER", dto.FLG_REPONER);
        p.Add("COD_USUARIO_ANULACION", codUsuario);
        p.Add("COD_ESTACION_ANULACION", codEstacion);
        try
        {
            var a = await conn.QueryFirstOrDefaultAsync<RfidEtiquetaAnulada>(
                SP.SCHEMARfid.Etiquetas.Anular, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = a?.NUM_ETIQUETA_REPUESTA?.ToString() ?? string.Empty }, a);
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMARfid.Etiquetas.Anular, p), null); }
    }

    // ───────── orígenes ─────────

    public async Task<IEnumerable<RfidOrigenCompra>> OrigenComprasAsync(RfidOrigenCompraSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("FEC_DESDE", dto.FEC_DESDE?.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA?.Date, DbType.Date);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("FLG_SOLO_PENDIENTES", dto.FLG_SOLO_PENDIENTES ?? true);
        p.Add("TOP", dto.TOP);
        return await conn.QueryAsync<RfidOrigenCompra>(SP.SCHEMARfid.Origen.ComprasSearch, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidOrigenCompraLinea>> OrigenComprasLineasAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RfidOrigenCompraLinea>(
            SP.SCHEMARfid.Origen.ComprasLineas, new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<RfidOrigenStock>> OrigenStockAsync(RfidOrigenStockSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN?.Trim());
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("FLG_SOLO_PENDIENTES", dto.FLG_SOLO_PENDIENTES ?? true);
        p.Add("TOP", dto.TOP);
        return await conn.QueryAsync<RfidOrigenStock>(SP.SCHEMARfid.Origen.StockSearch, p, commandType: CommandType.StoredProcedure);
    }

    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    // Los SP lanzan con THROW (50620–50643): el texto del error ES el mensaje para el usuario.
    private static WriteResult Error(SqlException ex, string sp, DynamicParameters p) => new()
    {
        Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
        ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p)
    };
}
