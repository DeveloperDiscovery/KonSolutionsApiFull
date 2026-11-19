using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AplicacionesOpcionesRepository : IAplicacionesOpcionesRepository
{
    private readonly IDbConnectionFactory _db;
    private readonly ILogger<AplicacionesOpcionesRepository> _log;

    public AplicacionesOpcionesRepository(IDbConnectionFactory db,
                                          ILogger<AplicacionesOpcionesRepository> log)
    {
        _db  = db;
        _log = log;
    }

    public async Task<IEnumerable<AplicacionesOpciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpciones>(SP.SCHEMASeguridad.AplicacionesOpciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AplicacionesOpciones?> ShowbyidAsync(string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AplicacionesOpciones>(SP.SCHEMASeguridad.AplicacionesOpciones.Showbyid, new { COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.AplicacionesOpciones.Showcombox);
    }

    public async Task<IEnumerable<AplicacionesOpciones>> SearchAsync(AplicacionesOpcionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpciones>(SP.SCHEMASeguridad.AplicacionesOpciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AplicacionesOpcionesInsertDto dto, string codUsuario, string codEstacion)
    {
        _log.LogInformation(
            "[INSERT] SP={SP} | COD_APLICACION={App} | COD_OPCION={Opc} | COD_OPCION_PADRE={Padre} | " +
            "DES={Des} | TIPO={Tipo} | FORMULARIO={Form} | FLG_PWD={Pwd} | " +
            "COD_TIPO_ESTADO={TipoEst} | COD_ESTADO={Est} | USUARIO={Usr} | ESTACION={Est2}",
            SP.SCHEMASeguridad.AplicacionesOpciones.Insert,
            dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.COD_OPCION_APLICACION_PADRE,
            dto.DES_OPCION_APLICACION, dto.COD_TIPO_OPCION_APLICACION, dto.DES_FORMULARIO,
            dto.FLG_EXIGE_PASSWORD, dto.COD_TIPO_ESTADO, dto.COD_ESTADO,
            codUsuario, codEstacion);

        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION",              dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION",       dto.COD_OPCION_APLICACION);
        p.Add("COD_OPCION_APLICACION_PADRE", dto.COD_OPCION_APLICACION_PADRE ?? string.Empty);
        p.Add("DES_OPCION_APLICACION",       dto.DES_OPCION_APLICACION);
        p.Add("COD_TIPO_OPCION_APLICACION",  dto.COD_TIPO_OPCION_APLICACION);
        p.Add("DES_FORMULARIO",              dto.DES_FORMULARIO ?? string.Empty);
        p.Add("DES_PARAMETRO",               dto.DES_PARAMETRO);
        p.Add("FLG_EXIGE_PASSWORD", dto.FLG_EXIGE_PASSWORD ?? false);
        p.Add("FLG_TECLADO_VIRTUAL", dto.FLG_TECLADO_VIRTUAL ?? false);
        p.Add("FLG_MULTI_LOAD", dto.FLG_MULTI_LOAD ?? false);
        p.Add("IMG_ICONO",                   dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("DES_BACKCOLOR",               dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR",               dto.DES_FORECOLOR);
        p.Add("COD_TIPO_ESTADO",             dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO",                  dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO",        codUsuario);
        p.Add("COD_ESTACION_REGISTRO",       codEstacion);

        p.Add("DES_OBJETO", dto.DES_OBJETO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMASeguridad.AplicacionesOpciones.Insert, p, commandType: CommandType.StoredProcedure);

        _log.LogInformation("[INSERT] Resultado: Estado={Estado} Filas={Filas} Mensaje={Msg}",
            r?.Estado, r?.AffectedRows, r?.Mensaje);

        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.AplicacionesOpciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.AplicacionesOpciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> UpdateAsync(AplicacionesOpcionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_OPCION_APLICACION_PADRE", dto.COD_OPCION_APLICACION_PADRE ?? string.Empty);
        p.Add("DES_OPCION_APLICACION", dto.DES_OPCION_APLICACION);
        p.Add("COD_TIPO_OPCION_APLICACION", dto.COD_TIPO_OPCION_APLICACION);
        p.Add("DES_FORMULARIO", dto.DES_FORMULARIO ?? string.Empty);
        p.Add("DES_PARAMETRO", dto.DES_PARAMETRO);
        p.Add("FLG_EXIGE_PASSWORD", dto.FLG_EXIGE_PASSWORD ?? false);
        p.Add("FLG_TECLADO_VIRTUAL", dto.FLG_TECLADO_VIRTUAL ?? false);
        p.Add("FLG_MULTI_LOAD", dto.FLG_MULTI_LOAD ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("DES_BACKCOLOR", dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR", dto.DES_FORECOLOR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        // Van como _REGISTRO y no como _ACTUALIZACION: el SP declara sólo esos dos parámetros y
        // adentro los escribe en las columnas de ACTUALIZACION cuando la fila ya existe. Mandar
        // además @COD_USUARIO_ACTUALIZACION daba "el procedimiento tiene demasiados argumentos"
        // al modificar una opción de menú.
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("DES_OBJETO", dto.DES_OBJETO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.AplicacionesOpciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.AplicacionesOpciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.AplicacionesOpciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_APLICACION, string COD_OPCION_APLICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.AplicacionesOpciones.Delete, new { COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.AplicacionesOpciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.AplicacionesOpciones.Delete, new { COD_APLICACION = COD_APLICACION, COD_OPCION_APLICACION = COD_OPCION_APLICACION })
        };
    }
}

    public async Task<WriteResult> MoveAsync(AplicacionesOpcionesMoveDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_APLICACION_DESTINO", dto.COD_APLICACION_DESTINO);
        p.Add("COD_OPCION_APLICACION_DESTINO", dto.COD_OPCION_APLICACION_DESTINO);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        await conn.ExecuteAsync(SP.SCHEMASeguridad.AplicacionesOpciones.Move, p, commandType: CommandType.StoredProcedure);
        return new WriteResult { Estado = "OK", AffectedRows = 1 };
    }

    /// <summary>PROC_SEGURIDAD_APLICACIONES_OPCIONES_LOADCOMBOS: Tipo de opción + Aplicación
    /// (ya vienen aliados Id/Texto) y Estados (COD_ESTADO/DES_ESTADO/colores) en un solo viaje.
    /// Si codTipoEstado &lt;= 0, se resuelve el de SEGURIDAD.APLICACIONES_OPCIONES automáticamente
    /// (mismo mecanismo que EstadosPorTablaController), para que el cliente no tenga que saberlo.</summary>
    public async Task<AplicacionesOpcionesLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "SEGURIDAD.APLICACIONES_OPCIONES" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMASeguridad.AplicacionesOpciones.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tiposOpciones = MapCombo(await multi.ReadAsync<dynamic>());
        var aplicaciones  = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas  = await multi.ReadAsync<dynamic>();

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

        return new AplicacionesOpcionesLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposOpciones = tiposOpciones,
            Aplicaciones  = aplicaciones,
            Estados       = estados
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