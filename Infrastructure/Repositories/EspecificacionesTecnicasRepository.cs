using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EspecificacionesTecnicasRepository : IEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public EspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesTecnicas>(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EspecificacionesTecnicas?> ShowbyidAsync(string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EspecificacionesTecnicas>(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Showbyid, new { COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.EspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<EspecificacionesTecnicas>> SearchAsync(EspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EspecificacionesTecnicas>(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("DES_ESPECIFICACION_TECNICA", dto.DES_ESPECIFICACION_TECNICA);
        p.Add("DES_ESPECIFICACION_TECNICA_SINGULAR", dto.DES_ESPECIFICACION_TECNICA_SINGULAR);
        p.Add("COD_ESPECIFICACION_TECNICA_PADRE", NullSiVacio(dto.COD_ESPECIFICACION_TECNICA_PADRE));
        p.Add("COD_TIPO_ESPECIFICACION_TECNICA", dto.COD_TIPO_ESPECIFICACION_TECNICA);
        p.Add("FLG_HAS_CHILDREN", dto.FLG_HAS_CHILDREN ?? false);
        p.Add("FLG_LIST", dto.FLG_LIST ?? false);
        p.Add("FLG_IS_CHILDREN", dto.FLG_IS_CHILDREN ?? false);
        p.Add("FLG_DEPENDS_ENTITY", dto.FLG_DEPENDS_ENTITY ?? false);
        p.Add("FLG_MULTIPLE_CHOICE", dto.FLG_MULTIPLE_CHOICE ?? false);
        p.Add("FLG_ITEMS_WITH_VALUE", dto.FLG_ITEMS_WITH_VALUE ?? false);
        p.Add("FLG_PRODUCT_ASSEMBLY", dto.FLG_PRODUCT_ASSEMBLY ?? false);
        p.Add("FLG_QUALITY_CONTROL", dto.FLG_QUALITY_CONTROL ?? false);
        p.Add("FLG_OBSERVACION", dto.FLG_OBSERVACION ?? false);
        p.Add("FLG_RANGE", dto.FLG_RANGE ?? false);
        p.Add("FLG_EDITABLE_ORDER_TAKING", dto.FLG_EDITABLE_ORDER_TAKING ?? false);
        p.Add("FLG_IS_COLOR", dto.FLG_IS_COLOR ?? false);
        p.Add("COD_CLASE_DEFECTO_TECNICO", NullSiVacio(dto.COD_CLASE_DEFECTO_TECNICO));
        p.Add("COD_TIPO_DEFECTO_TECNICO_DEFECTO", NullSiVacio(dto.COD_TIPO_DEFECTO_TECNICO_DEFECTO));
        p.Add("DES_PREFIJO", dto.DES_PREFIJO);
        p.Add("DES_SUFIJO", dto.DES_SUFIJO);
        p.Add("CAN_ENTERO", dto.CAN_ENTERO);
        p.Add("CAN_DECIMAL", dto.CAN_DECIMAL);
        p.Add("COD_UNIDAD_MEDIDA", NullSiVacio(dto.COD_UNIDAD_MEDIDA));
        p.Add("COD_UNIDAD_MEDIDA_DIVISORA", NullSiVacio(dto.COD_UNIDAD_MEDIDA_DIVISORA));
        p.Add("COD_UNIDAD_MEDIDA_ITEM", NullSiVacio(dto.COD_UNIDAD_MEDIDA_ITEM));
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Delete, new { COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.EspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.EspecificacionesTecnicas.Delete, new { COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA })
        };
    }
}

    public async Task<EspecificacionesTecnicasLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "INGENIERIA.ESPECIFICACIONES_TECNICAS" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAIngenieria.EspecificacionesTecnicas.Loadcombos,
            new { COD_TIPO_ESTADO = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);
        return new EspecificacionesTecnicasLoadCombosResult
        {
            CodTipoEstado       = te?.COD_TIPO_ESTADO ?? 0,
            ClasesDefecto       = MapCombo(await multi.ReadAsync<dynamic>(), "COD_CLASE_DEFECTO_TECNICO", "DES_CLASE_DEFECTO_TECNICO"),
            TiposEspecificacion = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_ESPECIFICACION_TECNICA", "DES_TIPO_ESPECIFICACION_TECNICA"),
            Estados             = MapEstados(await multi.ReadAsync<dynamic>()),
            UnidadesMedida      = MapCombo(await multi.ReadAsync<dynamic>(), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA")
            // El SP LOADCOMBOS todavía ejecuta un 5to SELECT de Entidades (PROC_ESTANDAR_ENTIDADES_SHOWCOMBOX)
            // pero ya no se usa: la tabla no tiene columna COD_ENTIDAD — se descarta ese result set.
        };
    }

    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
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

    private static string? NullSiVacio(string? valor) => string.IsNullOrWhiteSpace(valor) ? null : valor;

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }
}
