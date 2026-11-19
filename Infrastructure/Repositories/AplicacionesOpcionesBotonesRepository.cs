using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AplicacionesOpcionesBotonesRepository : IAplicacionesOpcionesBotonesRepository
{
    private readonly IDbConnectionFactory _db;
    public AplicacionesOpcionesBotonesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AplicacionesOpcionesBotones>> ShowallAsync(string codAplicacion, string codOpcionAplicacion)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion };
        return await conn.QueryAsync<AplicacionesOpcionesBotones>(
            SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<AplicacionesOpcionesBotones?> ShowbyidAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AplicacionesOpcionesBotones>(SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Showbyid, new { COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION, COD_BOTON_OPCION = COD_BOTON_OPCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Showcombox);
    }

    public async Task<IEnumerable<AplicacionesOpcionesBotones>> SearchAsync(AplicacionesOpcionesBotonesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpcionesBotones>(SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AplicacionesOpcionesBotonesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();

        // Usar el valor que viene en el DTO; null si no se envía (el SP maneja el caso)
        var codBotonEstandar = string.IsNullOrWhiteSpace(dto.COD_BOTON_OPCION_ESTANDAR)
            ? null
            : dto.COD_BOTON_OPCION_ESTANDAR;

        var codBotonOpcion = string.IsNullOrWhiteSpace(dto.COD_BOTON_OPCION) ? null : dto.COD_BOTON_OPCION;

        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_BOTON_OPCION", codBotonOpcion, DbType.String);
        p.Add("DES_BOTON_OPCION", dto.DES_BOTON_OPCION);
        p.Add("DES_TOOLTIPTEXT", dto.DES_TOOLTIPTEXT, DbType.String);
        p.Add("DES_ALIAS", dto.DES_ALIAS, DbType.String);
        p.Add("COD_BOTON_OPCION_ESTANDAR", codBotonEstandar, DbType.String);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("FLG_GRILLA", dto.FLG_GRILLA);
        p.Add("FLG_HEADER", dto.FLG_HEADER);
        p.Add("FLG_DIALOG", dto.FLG_DIALOG);
        p.Add("FLG_ACCION_POST", dto.FLG_ACCION_POST ?? false);
        p.Add("FLG_ACCION_PUT", dto.FLG_ACCION_PUT ?? false);
        p.Add("FLG_ACCION_DELETE", dto.FLG_ACCION_DELETE ?? false);
        p.Add("FLG_ACCION_GET", dto.FLG_ACCION_GET ?? false);
        p.Add("DES_METODO", dto.DES_METODO, DbType.String);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION, string COD_BOTON_OPCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Delete, new { COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION, COD_BOTON_OPCION = COD_BOTON_OPCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Delete, new { COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION, COD_BOTON_OPCION = COD_BOTON_OPCION })
        };
    }
}

    /// <summary>PROC_SEGURIDAD_APLICACIONES_OPCIONES_BOTONES_LOADCOMBOS: Botón estándar
    /// (ya viene aliado Id/Texto) y Estados (COD_ESTADO/DES_ESTADO/colores) en un solo viaje.
    /// Si codTipoEstado &lt;= 0, se resuelve el de SEGURIDAD.APLICACIONES_OPCIONES_BOTONES
    /// automáticamente (mismo mecanismo que EstadosPorTablaController).</summary>
    public async Task<AplicacionesOpcionesBotonesLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "SEGURIDAD.APLICACIONES_OPCIONES_BOTONES" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASeguridad.AplicacionesOpcionesBotones.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var botonesEstandar = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas    = await multi.ReadAsync<dynamic>();

        var estados = estadosFilas.Select(f =>
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

        return new AplicacionesOpcionesBotonesLoadCombosResult
        {
            CodTipoEstado   = codTipoEstado,
            BotonesEstandar = botonesEstandar,
            Estados         = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera COD_*) y la de descripción (primera DES_*/NOMBRE_*) — nunca busca columnas
    /// literales "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var list = new List<ComboboxItem>();
        foreach (var f in filas)
        {
            var d = (IDictionary<string, object>)f;
            var idCol = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var txtCol = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            var id = idCol != null ? d[idCol]?.ToString() : null;
            var tx = txtCol != null ? d[txtCol]?.ToString() : null;
            list.Add(new ComboboxItem { Id = id ?? "", Texto = tx ?? "" });
        }
        return list;
    }

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}