using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class GruposProductoRepository : IGruposProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public GruposProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<GruposProducto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposProducto>(SP.SCHEMAEstandar.GruposProducto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<GruposProducto?> ShowbyidAsync(string COD_GRUPO_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<GruposProducto>(SP.SCHEMAEstandar.GruposProducto.Showbyid, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.GruposProducto.Showcombox);
    }

    public async Task<IEnumerable<GruposProducto>> SearchAsync(GruposProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GruposProducto>(SP.SCHEMAEstandar.GruposProducto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(GruposProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("DES_GRUPO_PRODUCTO", dto.DES_GRUPO_PRODUCTO);
        p.Add("FLG_BIEN", dto.FLG_BIEN);
        p.Add("COD_CUBSO", dto.COD_CUBSO);
        p.Add("COD_GRUPO_PRODUCTO_ALIAS", dto.COD_GRUPO_PRODUCTO_ALIAS);
        p.Add("COD_PARTIDA_ARANCELARIA", dto.COD_PARTIDA_ARANCELARIA);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.GruposProducto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.GruposProducto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.GruposProducto.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.GruposProducto.Delete, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.GruposProducto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.GruposProducto.Delete, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO })
        };
    }
}

    public async Task<GruposProductoLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "ESTANDAR.GRUPOS_PRODUCTO" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.GruposProducto.Loadcombos,
            new { COD_TIPO_ESTADO = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);
        var estados = MapEstados(await multi.ReadAsync<dynamic>());

        // PROC_ESTANDAR_GRUPOS_PRODUCTO_LOADCOMBOS solo trae Estados — Unidad de Medida y
        // Tipo de Valor de Cálculo se cargan aparte (mismos SHOWCOMBOX que usan sus propios
        // módulos), mapeados a mano porque no alias sus columnas como Id/Texto.
        var unidadesMedida    = MapCombo(await conn.QueryAsync<dynamic>(SP.SCHEMAEstandar.UnidadesMedida.Showcombox, commandType: CommandType.StoredProcedure), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA");
        var tiposValorCalculo = MapCombo(await conn.QueryAsync<dynamic>(SP.SCHEMAComercial.TiposValorCalculo.Showcombox, commandType: CommandType.StoredProcedure), "COD_TIPO_VALOR_CALCULO", "DES_TIPO_VALOR_CALCULO");

        return new GruposProductoLoadCombosResult
        {
            CodTipoEstado     = te?.COD_TIPO_ESTADO ?? 0,
            Estados           = estados,
            UnidadesMedida    = unidadesMedida,
            TiposValorCalculo = tiposValorCalculo
        };
    }

    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d  = (IDictionary<string, object>)f;
            var id = d.TryGetValue(colId,    out var v1) ? v1?.ToString() ?? "" : "";
            var tx = d.TryGetValue(colTexto, out var v2) ? v2?.ToString() ?? "" : "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    private static List<EstadoItem> MapEstados(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var cod  = d.TryGetValue("COD_ESTADO",    out var v1) ? v1?.ToString() ?? "" : "";
            var des  = d.TryGetValue("DES_ESTADO",    out var v2) ? v2?.ToString() ?? "" : "";
            var back = d.TryGetValue("DES_BACKCOLOR", out var v3) ? v3?.ToString() ?? "" : "";
            var fore = d.TryGetValue("DES_FORECOLOR", out var v4) ? v4?.ToString() ?? "" : "";
            var def  = d.TryGetValue("FLG_DEFAULT",   out var v5) && v5 is not null && Convert.ToBoolean(v5);
            return new EstadoItem { CodEstado = int.TryParse(cod, out var n) ? n : 0,
                DesEstado = des, BackColor = back, ForeColor = fore, EsDefault = def };
        }).ToList();

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }
}