using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosRepository : IDisenosRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Disenos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Disenos>(SP.SCHEMAIngenieria.Disenos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Disenos?> ShowbyidAsync(string COD_DISENO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Disenos>(SP.SCHEMAIngenieria.Disenos.Showbyid, new { COD_DISENO = COD_DISENO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.Disenos.Showcombox);
    }

    public async Task<IEnumerable<Disenos>> SearchAsync(DisenosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Disenos>(SP.SCHEMAIngenieria.Disenos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("DES_DISENO", dto.DES_DISENO);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_TEMPORADA", dto.NUM_TEMPORADA);
        p.Add("NUM_COLECCION", dto.NUM_COLECCION);
        p.Add("NUM_SOLICITUD_COMERICAL", dto.NUM_SOLICITUD_COMERICAL);
        p.Add("COD_TIPO_DISENO", dto.COD_TIPO_DISENO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.Disenos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.Disenos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.Disenos.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.Disenos.Delete, new { COD_DISENO = COD_DISENO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.Disenos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.Disenos.Delete, new { COD_DISENO = COD_DISENO })
        };
    }
}

    /// <summary>PROC_INGENIERIA_DISENOS_LOADCOMBOS ejecuta, en este orden: Tipos de Diseño,
    /// Grupos de Producto, Unidades de Medida, Entidades tipo Cliente ('CLI') y Estados —
    /// ninguno alias sus columnas como Id/Texto, así que cada resultset se mapea a mano
    /// (mismo patrón que GruposProductoRepository.LoadcombosAsync).</summary>
    public async Task<DisenosLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "INGENIERIA.DISENOS" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAIngenieria.Disenos.Loadcombos,
            new { COD_TIPO_ESTADO = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);

        var tiposDiseno    = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_DISENO", "DES_TIPO_DISENO");
        var gruposProducto = MapCombo(await multi.ReadAsync<dynamic>(), "COD_GRUPO_PRODUCTO", "DES_GRUPO_PRODUCTO");
        var unidadesMedida = MapCombo(await multi.ReadAsync<dynamic>(), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA");
        var clientes       = MapEntidadCombo(await multi.ReadAsync<dynamic>());
        var estados        = MapEstados(await multi.ReadAsync<dynamic>());

        return new DisenosLoadCombosResult
        {
            CodTipoEstado  = te?.COD_TIPO_ESTADO ?? 0,
            TiposDiseno    = tiposDiseno,
            GruposProducto = gruposProducto,
            UnidadesMedida = unidadesMedida,
            Clientes       = clientes,
            Estados        = estados
        };
    }

    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d  = (IDictionary<string, object>)f;
            var id = d.TryGetValue(colId,    out var v1) ? v1?.ToString() ?? "" : "";
            var tx = d.TryGetValue(colTexto, out var v2) ? v2?.ToString() ?? "" : "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    // Mismo criterio que EntidadesTiposEntidadesRepository.ShowcomboxidAsync: el SP trae
    // COD_ENTIDAD/DES_NOMBRE_COMPLETO/DES_COMERCIAL sin aliasar.
    private static List<ComboboxItem> MapEntidadCombo(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var texto = (d.TryGetValue("DES_NOMBRE_COMPLETO", out var nc) ? nc as string : null)
                        ?? (d.TryGetValue("DES_COMERCIAL", out var dc) ? dc as string : null)
                        ?? "";
            return new ComboboxItem { Id = d.TryGetValue("COD_ENTIDAD", out var idv) ? idv?.ToString() ?? "" : "", Texto = texto };
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