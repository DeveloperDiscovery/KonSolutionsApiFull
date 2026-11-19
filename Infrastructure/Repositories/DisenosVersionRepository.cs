using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosVersionRepository : IDisenosVersionRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosVersionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosVersion>> ShowallAsync(string codDiseno)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersion>(SP.SCHEMAIngenieria.DisenosVersion.Showall,
            new { COD_DISENO = codDiseno }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosVersion?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosVersion>(SP.SCHEMAIngenieria.DisenosVersion.Showbyid,
            new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>SHOWCOMBOX devuelve el registro completo (COD_/DES_), no Id/Texto,
    /// así que se materializa como DisenosVersion y el cliente arma el combo.</summary>
    public async Task<IEnumerable<DisenosVersion>> ShowcomboxAsync(string codDiseno)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersion>(SP.SCHEMAIngenieria.DisenosVersion.Showcombox,
            new { COD_DISENO = codDiseno }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosVersion>> SearchAsync(string codDiseno, DisenosVersionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersion>(SP.SCHEMAIngenieria.DisenosVersion.Search,
            new { COD_DISENO = codDiseno, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosVersionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("DES_VERSION", dto.DES_VERSION);
        p.Add("COD_CLASE_DESARROLLO", dto.COD_CLASE_DESARROLLO);
        p.Add("COD_TIPO_DESARROLLO", dto.COD_TIPO_DESARROLLO);
        p.Add("COD_RUTA_PRODUCCION", dto.COD_RUTA_PRODUCCION);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersion.Delete,
            new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersion.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION })
        };
    }
}

    /// <summary>PROC_INGENIERIA_DISENOS_VERSIONES_LOADCOMBOS ejecuta, en este orden: Clases
    /// de Desarrollo, Rutas de Producción, Unidades de Medida y Estados — ninguno alias sus
    /// columnas como Id/Texto, así que cada resultset se mapea a mano (mismo patrón que
    /// GruposProductoRepository.LoadcombosAsync). La tabla que se busca en
    /// TABLAS_TIPOS_ESTADO es la física (INGENIERIA.DISENOS_VERSION, singular) aunque el
    /// nombre del SP LOADCOMBOS use "VERSIONES" en plural.</summary>
    public async Task<DisenosVersionLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "INGENIERIA.DISENOS_VERSION" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAIngenieria.DisenosVersion.Loadcombos,
            new { COD_TIPO_ESTADO = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);

        var clasesDesarrollo = MapCombo(await multi.ReadAsync<dynamic>(), "COD_CLASE_DESARROLLO", "DES_CLASE_DESARROLLO");
        var produccionRutas  = MapCombo(await multi.ReadAsync<dynamic>(), "COD_RUTA_PRODUCCION", "DES_RUTA_PRODUCCION");
        var unidadesMedida   = MapCombo(await multi.ReadAsync<dynamic>(), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA");
        var estados          = MapEstados(await multi.ReadAsync<dynamic>());

        return new DisenosVersionLoadCombosResult
        {
            CodTipoEstado    = te?.COD_TIPO_ESTADO ?? 0,
            ClasesDesarrollo = clasesDesarrollo,
            ProduccionRutas  = produccionRutas,
            UnidadesMedida   = unidadesMedida,
            Estados          = estados
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
