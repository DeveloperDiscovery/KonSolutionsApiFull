using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Las guías de remisión recibidas (script 41) y su paso a ingreso de almacén (script 42).</summary>
public class GreRecibidasRepository : IGreRecibidasRepository
{
    private readonly IDbConnectionFactory _db;
    public GreRecibidasRepository(IDbConnectionFactory db) => _db = db;

    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    public async Task<IEnumerable<GreRecibida>> SearchAsync(GreRecibidaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("NUM_RUC_REMITENTE", Nulo(dto.NUM_RUC_REMITENTE));
        p.Add("COD_TIPO_GUIA", Nulo(dto.COD_TIPO_GUIA));
        p.Add("COD_ESTADO_USO", Nulo(dto.COD_ESTADO_USO));
        p.Add("FEC_DESDE", dto.FEC_DESDE?.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA?.Date, DbType.Date);
        p.Add("NUM_FILAS", dto.NUM_FILAS is > 0 ? dto.NUM_FILAS : 1000);
        return await conn.QueryAsync<GreRecibida>("PROC_LOGISTICA_GRE_RECIBIDAS_SEARCH", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<GreRecibidaFicha?> ShowbyidAsync(decimal NUM_GRE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GreRecibidaFicha>("PROC_LOGISTICA_GRE_RECIBIDAS_SHOWBYID", new { NUM_GRE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<GreRecibidaLinea>> LineasAsync(decimal NUM_GRE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GreRecibidaLinea>("PROC_LOGISTICA_GRE_RECIBIDAS_DETALLE_SHOWALL", new { NUM_GRE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<GreRecibidaXml?> XmlAsync(decimal NUM_GRE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GreRecibidaXml>("PROC_LOGISTICA_GRE_RECIBIDAS_XML_SHOWBYID", new { NUM_GRE }, commandType: CommandType.StoredProcedure);
    }

    public async Task AsignarProductoAsync(decimal NUM_GRE, int NUM_ITEM, string? codProducto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_LOGISTICA_GRE_RECIBIDAS_ASIGNAR_PRODUCTO",
            new { NUM_GRE, NUM_ITEM, COD_PRODUCTO = Nulo(codProducto), COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task DescartarAsync(decimal NUM_GRE, bool descartar, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_LOGISTICA_GRE_RECIBIDAS_DESCARTAR",
            new { NUM_GRE, FLG_DESCARTAR = descartar, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RecibidoCompletarResult> CompletarDatosAsync(decimal? NUM_DOCUMENTO, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<RecibidoCompletarResult>("PROC_XML_RECIBIDOS_COMPLETAR_DATOS",
            new { COD_CLASE = "GRE", NUM_DOCUMENTO, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
        return r ?? new RecibidoCompletarResult();
    }

    public async Task<RecibidoSunatResult> ValidarAsync(decimal? NUM_GRE)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<RecibidoSunatResult>("PROC_LOGISTICA_GRE_RECIBIDAS_VALIDAR", new { NUM_GRE }, commandType: CommandType.StoredProcedure);
        return r ?? new RecibidoSunatResult();
    }

    public async Task<GreRecibidaGenerarResult> GenerarMovimientoAsync(decimal NUM_GRE, GreRecibidaGenerarDto dto, string codClaseMovimiento, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_GRE", NUM_GRE);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("COD_CLASE_MOVIMIENTO", codClaseMovimiento);
        p.Add("COD_TIPO_MOVIMIENTO", Nulo(dto.COD_TIPO_MOVIMIENTO));
        p.Add("COD_UBICACION_ALMACEN", Nulo(dto.COD_UBICACION_ALMACEN));
        p.Add("FEC_MOVIMIENTO", dto.FEC_MOVIMIENTO?.Date, DbType.Date);
        p.Add("FLG_RECORDAR_EQUIVALENCIAS", dto.FLG_RECORDAR_EQUIVALENCIAS);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("MOSTRAR_RESULTADO", true);
        return await conn.QueryFirstAsync<GreRecibidaGenerarResult>("PROC_LOGISTICA_GRE_RECIBIDAS_GENERAR_MOVIMIENTO", p, commandType: CommandType.StoredProcedure);
    }
}
