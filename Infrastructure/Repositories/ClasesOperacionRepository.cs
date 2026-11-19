using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesOperacionRepository : IClasesOperacionRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesOperacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesOperacion>(SP.SCHEMAEstandar.ClasesOperacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesOperacion?> ShowbyidAsync(string COD_CLASE_OPERACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesOperacion>(SP.SCHEMAEstandar.ClasesOperacion.Showbyid, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.ClasesOperacion.Showcombox);
    }

    public async Task<IEnumerable<ClasesOperacion>> SearchAsync(ClasesOperacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesOperacion>(SP.SCHEMAEstandar.ClasesOperacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("DES_CLASE_OPERACION", dto.DES_CLASE_OPERACION);
        p.Add("COD_TIPO_ENTIDAD_RELACIONADA", dto.COD_TIPO_ENTIDAD_RELACIONADA);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_CAMBIO", dto.FLG_CAMBIO ?? false);
        p.Add("FLG_FINANZAS", dto.FLG_FINANZAS ?? false);
        p.Add("FLG_RENDICION", dto.FLG_RENDICION ?? false);
        p.Add("FLG_PERMUTA", dto.FLG_PERMUTA ?? false);
        p.Add("FLG_BANCO", dto.FLG_BANCO ?? false);
        p.Add("FLG_CAJA", dto.FLG_CAJA ?? false);
        p.Add("FLG_REGIMEN_ESPECIAL", dto.FLG_REGIMEN_ESPECIAL ?? false);
        p.Add("COD_TIPO_ESTADO_RELACIONADO", dto.COD_TIPO_ESTADO_RELACIONADO);
        p.Add("COD_TIPO_MOTIVO_RELACIONADO", dto.COD_TIPO_MOTIVO_RELACIONADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ClasesOperacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ClasesOperacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ClasesOperacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesOperacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("DES_CLASE_OPERACION", dto.DES_CLASE_OPERACION);
        p.Add("COD_TIPO_ENTIDAD_RELACIONADA", dto.COD_TIPO_ENTIDAD_RELACIONADA);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_CAMBIO", dto.FLG_CAMBIO ?? false);
        p.Add("FLG_FINANZAS", dto.FLG_FINANZAS ?? false);
        p.Add("FLG_RENDICION", dto.FLG_RENDICION ?? false);
        p.Add("FLG_PERMUTA", dto.FLG_PERMUTA ?? false);
        p.Add("FLG_BANCO", dto.FLG_BANCO ?? false);
        p.Add("FLG_CAJA", dto.FLG_CAJA ?? false);
        p.Add("FLG_REGIMEN_ESPECIAL", dto.FLG_REGIMEN_ESPECIAL ?? false);
        p.Add("COD_TIPO_ESTADO_RELACIONADO", dto.COD_TIPO_ESTADO_RELACIONADO);
        p.Add("COD_TIPO_MOTIVO_RELACIONADO", dto.COD_TIPO_MOTIVO_RELACIONADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ClasesOperacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ClasesOperacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ClasesOperacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ClasesOperacion.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ClasesOperacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ClasesOperacion.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION })
        };
    }
}

    /// <summary>PROC_ESTANDAR_CLASES_OPERACION_LOADCOMBOS: SP ya existente en BD (antes
    /// huérfano, sin constante ni wiring) — bundlea TiposEntidades + TiposEstados +
    /// TiposMotivos (ya venían con Task.WhenAll) + Estado (COD_ESTADO/DES_ESTADO/colores) en
    /// un solo viaje. Su único parámetro real se llama @COD_TIPO_ESTADO_CLASES_OPERACION
    /// (no @COD_TIPO_ESTADO como en el patrón de Areas) — se respeta el nombre real del SP.
    /// Si codTipoEstado &lt;= 0, se resuelve el de ESTANDAR.CLASES_OPERACION automáticamente
    /// (mismo mecanismo que EstadosPorTablaController).</summary>
    public async Task<ClasesOperacionLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "ESTANDAR.CLASES_OPERACION" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.ClasesOperacion.Loadcombos,
            new { COD_TIPO_ESTADO_CLASES_OPERACION = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tiposEntidades = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposEstados     = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposMotivos     = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas     = await multi.ReadAsync<dynamic>();

        var estados = MapEstados(estadosFilas);

        return new ClasesOperacionLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposEntidades = tiposEntidades,
            TiposEstados = tiposEstados,
            TiposMotivos = tiposMotivos,
            Estados = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera COD_*) y de descripción (primera DES_*/NOMBRE_*) — nunca busca columnas
    /// literales "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    private static List<EstadoItem> MapEstados(IEnumerable<dynamic> filas) => filas.Select(f =>
    {
        var d = (IDictionary<string, object>)f;
        var codStr = d.TryGetValue("COD_ESTADO", out var id) ? id?.ToString() ?? "" : "";
        return new EstadoItem
        {
            CodEstado = int.TryParse(codStr, out var n) ? n : 0,
            DesEstado = d.TryGetValue("DES_ESTADO", out var tx) ? tx?.ToString() ?? "" : "",
            BackColor = d.TryGetValue("DES_BACKCOLOR", out var bc) ? bc?.ToString() ?? "" : "",
            ForeColor = d.TryGetValue("DES_FORECOLOR", out var fc) ? fc?.ToString() ?? "" : "",
            EsDefault = d.TryGetValue("FLG_DEFAULT", out var df) && df is not null && Convert.ToBoolean(df)
        };
    }).ToList();

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}