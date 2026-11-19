using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GuiasRemisionRepository : IGuiasRemisionRepository
{
    private readonly IDbConnectionFactory _db;
    public GuiasRemisionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GuiaRemision>> ShowallAsync(GuiaRemisionFiltroDto filtro)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GuiaRemision>(SP.SCHEMALogistica.GuiasRemision.Showall, new
        {
            FEC_DESDE = filtro.FEC_DESDE?.Date,
            FEC_HASTA = filtro.FEC_HASTA?.Date,
            COD_ESTADO_ALIAS = string.IsNullOrWhiteSpace(filtro.COD_ESTADO_ALIAS) ? null : filtro.COD_ESTADO_ALIAS,
            filtro.NUM_MOVIMIENTO_ALMACEN
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<GuiaRemision?> ShowbyidAsync(int NUM_GUIA_REMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GuiaRemision>(SP.SCHEMALogistica.GuiasRemision.Showbyid, new { NUM_GUIA_REMISION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<GuiaRemision?> ShowbymovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GuiaRemision>(SP.SCHEMALogistica.GuiasRemision.Showbymovimiento, new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<GuiaRemisionEnvio>> EnviosAsync(int NUM_GUIA_REMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GuiaRemisionEnvio>(SP.SCHEMALogistica.GuiasRemision.EnviosShowall, new { NUM_GUIA_REMISION }, commandType: CommandType.StoredProcedure);
    }

    public Task<GuiaRemisionArchivo?> XmlAsync(int NUM_GUIA_REMISION) => Archivo(SP.SCHEMALogistica.GuiasRemision.Xml, "ARC_XML_FIRMADO", NUM_GUIA_REMISION);
    public Task<GuiaRemisionArchivo?> CdrAsync(int NUM_GUIA_REMISION) => Archivo(SP.SCHEMALogistica.GuiasRemision.Cdr, "ARC_CDR", NUM_GUIA_REMISION);

    /// <summary>El XML y el CDR vienen en columnas con nombre distinto: se leen por nombre y se
    /// ponen en un mismo tipo, así quien descarga no se entera de cuál era.</summary>
    private async Task<GuiaRemisionArchivo?> Archivo(string sp, string columna, int numGuia)
    {
        using var conn = _db.CreateConnection();
        var fila = await conn.QueryFirstOrDefaultAsync(sp, new { NUM_GUIA_REMISION = numGuia }, commandType: CommandType.StoredProcedure);
        if (fila is null) return null;
        var campos = (IDictionary<string, object?>)fila;
        return new GuiaRemisionArchivo
        {
            NUM_GUIA_REMISION = Convert.ToInt32(campos["NUM_GUIA_REMISION"]),
            NUM_GUIA = Convert.ToString(campos["NUM_GUIA"]) ?? string.Empty,
            Contenido = campos[columna] as byte[]
        };
    }

    public async Task<GuiaRemision> CrearAsync(GuiaRemisionCrearDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = DatosComunes(dto);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_SERIE", string.IsNullOrWhiteSpace(dto.COD_SERIE) ? null : dto.COD_SERIE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        // El SP termina devolviendo la guía creada (lo mismo que SHOWBYID).
        return await conn.QueryFirstAsync<GuiaRemision>(SP.SCHEMALogistica.GuiasRemision.Crear, p, commandType: CommandType.StoredProcedure);
    }

    public async Task RegistrarXmlAsync(GuiaRemisionXmlDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMALogistica.GuiasRemision.RegistrarXml, new
        {
            dto.NUM_GUIA_REMISION, dto.ARC_XML_FIRMADO, dto.DES_HASH_XML, dto.DES_HASH_ZIP, dto.DES_TEXTO_QR,
            COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task RegistrarEnvioAsync(GuiaRemisionEnvioDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMALogistica.GuiasRemision.RegistrarEnvio, new
        {
            dto.NUM_GUIA_REMISION, dto.NUM_TICKET, dto.DES_HASH_ZIP,
            COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task RegistrarRespuestaAsync(GuiaRemisionRespuestaDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMALogistica.GuiasRemision.RegistrarRespuesta, new
        {
            dto.NUM_GUIA_REMISION, dto.FLG_ACEPTADA, dto.COD_RESPUESTA_SUNAT, dto.DES_RESPUESTA_SUNAT, dto.ARC_CDR, dto.DES_HASH_ZIP,
            COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<GuiaRemision> ReintentarAsync(GuiaRemisionReintentarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = DatosComunes(dto);
        p.Add("NUM_GUIA_REMISION", dto.NUM_GUIA_REMISION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return await conn.QueryFirstAsync<GuiaRemision>(SP.SCHEMALogistica.GuiasRemision.Reintentar, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<GuiaRemisionOpciones?> OpcionesAsync(int NUM_GUIA_REMISION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GuiaRemisionOpciones>(SP.SCHEMALogistica.GuiasRemision.OpcionesShowbyid, new { NUM_GUIA_REMISION }, commandType: CommandType.StoredProcedure);
    }

    public async Task GuardarOpcionesAsync(int NUM_GUIA_REMISION, GuiaRemisionOpcionesDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMALogistica.GuiasRemision.OpcionesGuardar, new
        {
            NUM_GUIA_REMISION, dto.FLG_TRASLADO_M1L, dto.FLG_TRANSBORDO_PROGRAMADO,
            dto.COD_TIPO_DOCUMENTO_RELACIONADO, dto.DES_TIPO_DOCUMENTO_RELACIONADO, dto.NUM_DOCUMENTO_RELACIONADO, dto.NUM_RUC_EMISOR_RELACIONADO,
            COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los datos "para buscar y listar" son los mismos al crear y al reintentar.</summary>
    private static DynamicParameters DatosComunes(GuiaRemisionDatos d)
    {
        var p = new DynamicParameters();
        p.Add("FEC_INICIO_TRASLADO", d.FEC_INICIO_TRASLADO?.Date, DbType.Date);
        p.Add("COD_MODALIDAD_TRASLADO", d.COD_MODALIDAD_TRASLADO);
        p.Add("COD_MOTIVO_SUNAT", d.COD_MOTIVO_SUNAT);
        p.Add("COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT", d.COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT);
        p.Add("NUM_DOCUMENTO_DESTINATARIO", d.NUM_DOCUMENTO_DESTINATARIO);
        p.Add("DES_NOMBRE_DESTINATARIO", d.DES_NOMBRE_DESTINATARIO);
        p.Add("NUM_PLACA_PRINCIPAL", d.NUM_PLACA_PRINCIPAL);
        p.Add("CAN_PESO_BRUTO_TOTAL", d.CAN_PESO_BRUTO_TOTAL, DbType.Decimal, precision: 18, scale: 3);
        p.Add("COD_UNIDAD_PESO_SUNAT", d.COD_UNIDAD_PESO_SUNAT);
        return p;
    }

    public async Task<IReadOnlyList<IReadOnlyDictionary<string, object?>>> ReporteMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        // Sólo el número de movimiento: las fechas y los filtros del SP no se usan cuando llega.
        var filas = await conn.QueryAsync(SP.SCHEMALogistica.GuiasRemision.ReporteMovimientos, new { NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
        return filas
            .Select(f => (IReadOnlyDictionary<string, object?>)new Dictionary<string, object?>((IDictionary<string, object?>)f, StringComparer.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<IReadOnlyList<string>> SeriesParaMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        var series = await conn.QueryAsync<string>($"SELECT COD_SERIE FROM {SP.SCHEMALogistica.GuiasRemisionSeries.FuncionSeriesParaMovimiento}(@NUM_MOVIMIENTO_ALMACEN) ORDER BY COD_SERIE",
            new { NUM_MOVIMIENTO_ALMACEN });
        return series.ToList();
    }

    public async Task<string?> BloqueoMovimientoAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.ExecuteScalarAsync<string?>($"SELECT {SP.SCHEMALogistica.GuiasRemision.FuncionBloqueaMovimiento}(@NUM_MOVIMIENTO_ALMACEN)", new { NUM_MOVIMIENTO_ALMACEN });
    }

    public async Task<IEnumerable<GuiaRemisionCatalogoPendiente>> CatalogosPendientesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GuiaRemisionCatalogoPendiente>(SP.SCHEMALogistica.GuiasRemision.CatalogosRevisar, commandType: CommandType.StoredProcedure, commandTimeout: 120);
    }
}
