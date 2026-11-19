using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Finanzas;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Los comprobantes recibidos (script 41) y su paso a registro de compras (script 42). Cada método llama a un procedimiento; si una regla lo rechaza, la SqlException sube con el mensaje.</summary>
public class CpeRecibidosRepository : ICpeRecibidosRepository
{
    private readonly IDbConnectionFactory _db;
    public CpeRecibidosRepository(IDbConnectionFactory db) => _db = db;

    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    public async Task<IEnumerable<CpeRecibido>> SearchAsync(CpeRecibidoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", Nulo(dto.SEARCH));
        p.Add("NUM_RUC_EMISOR", Nulo(dto.NUM_RUC_EMISOR));
        p.Add("COD_TIPO_COMPROBANTE_SUNAT", Nulo(dto.COD_TIPO_COMPROBANTE_SUNAT));
        p.Add("COD_ESTADO_USO", Nulo(dto.COD_ESTADO_USO));
        p.Add("FEC_DESDE", dto.FEC_DESDE?.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA?.Date, DbType.Date);
        p.Add("NUM_FILAS", dto.NUM_FILAS is > 0 ? dto.NUM_FILAS : 1000);
        return await conn.QueryAsync<CpeRecibido>("PROC_FINANZAS_CPE_RECIBIDOS_SEARCH", p, commandType: CommandType.StoredProcedure);
    }

    public async Task<CpeRecibidoFicha?> ShowbyidAsync(decimal NUM_CPE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CpeRecibidoFicha>("PROC_FINANZAS_CPE_RECIBIDOS_SHOWBYID", new { NUM_CPE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<CpeRecibidoLinea>> LineasAsync(decimal NUM_CPE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CpeRecibidoLinea>("PROC_FINANZAS_CPE_RECIBIDOS_DETALLE_SHOWALL", new { NUM_CPE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<CpeRecibidoXml?> XmlAsync(decimal NUM_CPE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CpeRecibidoXml>("PROC_FINANZAS_CPE_RECIBIDOS_XML_SHOWBYID", new { NUM_CPE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<CpeRecibidoTipoOperacion?> TipoOperacionAsync(decimal NUM_CPE, string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CpeRecibidoTipoOperacion>("PROC_FINANZAS_CPE_RECIBIDOS_TIPO_OPERACION", new { NUM_CPE, COD_CLASE_OPERACION = codClaseOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task AsignarProductoAsync(decimal NUM_CPE, int NUM_ITEM, string? codProducto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_FINANZAS_CPE_RECIBIDOS_ASIGNAR_PRODUCTO",
            new { NUM_CPE, NUM_ITEM, COD_PRODUCTO = Nulo(codProducto), COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task DescartarAsync(decimal NUM_CPE, bool descartar, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_FINANZAS_CPE_RECIBIDOS_DESCARTAR",
            new { NUM_CPE, FLG_DESCARTAR = descartar, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<RecibidoCompletarResult> CompletarDatosAsync(decimal? NUM_DOCUMENTO, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<RecibidoCompletarResult>("PROC_XML_RECIBIDOS_COMPLETAR_DATOS",
            new { COD_CLASE = "CPE", NUM_DOCUMENTO, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
        return r ?? new RecibidoCompletarResult();
    }

    public async Task SunatRegistrarAsync(decimal NUM_CPE, string codigo, string mensaje)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_FINANZAS_CPE_RECIBIDOS_SUNAT_REGISTRAR", new { NUM_CPE, COD_ESTADO_SUNAT = codigo, DES_ESTADO_SUNAT = mensaje }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IReadOnlyList<decimal>> SunatPendientesAsync(int max)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<decimal>("SELECT NUM_CPE FROM FINANZAS.CPE_RECIBIDOS WHERE COD_ESTADO_SUNAT IS NULL OR COD_ESTADO_SUNAT = N'ERR' ORDER BY NUM_CPE OFFSET 0 ROWS FETCH NEXT @max ROWS ONLY", new { max })).ToList();
    }

    public async Task<CpeRecibidoGenerarResult> GenerarRegistroAsync(decimal NUM_CPE, CpeRecibidoGenerarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CPE", NUM_CPE);
        p.Add("COD_TIPO_OPERACION", Nulo(dto.COD_TIPO_OPERACION));
        p.Add("COD_CLASE_OPERACION", Nulo(dto.COD_CLASE_OPERACION));
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FLG_RECORDAR_EQUIVALENCIAS", dto.FLG_RECORDAR_EQUIVALENCIAS);
        p.Add("VALIDAR", dto.VALIDAR);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("MOSTRAR_RESULTADO", true);
        return await conn.QueryFirstAsync<CpeRecibidoGenerarResult>("PROC_FINANZAS_CPE_RECIBIDOS_GENERAR_REGISTRO", p, commandType: CommandType.StoredProcedure);
    }
}
