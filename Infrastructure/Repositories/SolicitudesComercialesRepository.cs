using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SolicitudesComercialesRepository : ISolicitudesComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public SolicitudesComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SolicitudesComerciales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComerciales>(SP.SCHEMAComercial.SolicitudesComerciales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<SolicitudesComerciales?> ShowbyidAsync(int numSolicitudComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SolicitudesComerciales>(SP.SCHEMAComercial.SolicitudesComerciales.Showbyid, new { NUM_SOLICITUD_COMERCIAL = numSolicitudComercial }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal? codEntidadCliente)
    {
        using var conn = _db.CreateConnection();
        // Mismo caso: la clave es NUM_SOLICITUD_COMERCIAL, no COD_*; por convención el
        // lector tomaba COD_TIPO_SOLICITUD_COMERCIAL como identificador.
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.SolicitudesComerciales.Showcombox,
            columnaId: "NUM_SOLICITUD_COMERCIAL", columnaTexto: "DES_SOLICITUD_COMERCIAL",
            parametros: new { COD_ENTIDAD_CLIENTE = codEntidadCliente });
    }

    public async Task<IEnumerable<SolicitudesComerciales>> SearchAsync(SolicitudesComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SolicitudesComerciales>(SP.SCHEMAComercial.SolicitudesComerciales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>PROC_COMERCIAL_SOLICITUDES_COMERCIALES_INSERT es uno de los 8 procedimientos
    /// con lógica propia del proyecto (ver el comentario de RESTAURAR_SP_LOGICA_PROPIA.sql):
    /// hace UPDATE y, si no encuentra la fila, INSERT — mismo endpoint para alta y
    /// modificación. NO recibe COD_ENTIDAD_ASESOR_COMERCIAL como parámetro: lo calcula él
    /// mismo buscando el asesor asignado al cliente en
    /// COMERCIAL.ENTIDADES_ASESORES_COMERCIALES. Mandárselo como argumento extra es
    /// justamente el error que describe ese comentario — "tiene demasiados argumentos" — y
    /// por eso no está en esta lista aunque el DTO lo declare (lo llena la respuesta de
    /// consulta, no el alta).</summary>
    public async Task<WriteResult> InsertAsync(SolicitudesComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_COMERCIAL", dto.NUM_SOLICITUD_COMERCIAL);
        p.Add("DES_SOLICITUD_COMERCIAL", dto.DES_SOLICITUD_COMERCIAL);
        p.Add("FEC_SOLICITUD_COMERCIAL", dto.FEC_SOLICITUD_COMERCIAL);
        p.Add("FEC_REQUERIDA_ATENCION", dto.FEC_REQUERIDA_ATENCION);
        p.Add("FEC_ASIGNACION_EFECTUADA", dto.FEC_ASIGNACION_EFECTUADA);
        p.Add("FEC_ATENCION_EFECTUADA", dto.FEC_ATENCION_EFECTUADA);
        p.Add("COD_TIPO_SOLICITUD_COMERCIAL", dto.COD_TIPO_SOLICITUD_COMERCIAL);
        p.Add("COD_ENTIDAD_CLIENTE", dto.COD_ENTIDAD_CLIENTE);
        p.Add("NUM_TEMPORADA", dto.NUM_TEMPORADA);
        p.Add("NUM_COLECCION", dto.NUM_COLECCION);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SolicitudesComerciales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SolicitudesComerciales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SolicitudesComerciales.Insert, p)
        };
    }
}

    // El SP no borra físicamente: anula la solicitud (y en cascada sus Diseños/Versiones) y
    // dispara la notificación de anulación — por eso necesita saber QUIÉN la anula.
    public async Task<WriteResult> DeleteAsync(int numSolicitudComercial, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAComercial.SolicitudesComerciales.Delete,
            new
            {
                NUM_SOLICITUD_COMERCIAL = numSolicitudComercial,
                COD_USUARIO_REGISTRO = codUsuario,
                COD_ESTACION_REGISTRO = codEstacion
            },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SolicitudesComerciales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SolicitudesComerciales.Delete, new
            {
                NUM_SOLICITUD_COMERCIAL = numSolicitudComercial,
                COD_USUARIO_REGISTRO = codUsuario,
                COD_ESTACION_REGISTRO = codEstacion
            })
        };
    }
}

    public async Task<WriteResult> AsignarResponsableAsync(SolicitudesComercialesAsignarResponsableDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_COMERCIAL", dto.NUM_SOLICITUD_COMERCIAL);
        p.Add("COD_TIPO_SOLICITUD_COMERCIAL", dto.COD_TIPO_SOLICITUD_COMERCIAL);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_APLICACION_ORIGEN", dto.COD_APLICACION_ORIGEN);
        p.Add("COD_OPCION_APLICACION_ORIGEN", dto.COD_OPCION_APLICACION_ORIGEN);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SolicitudesComerciales.AsignarResponsable, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SolicitudesComerciales.AsignarResponsable,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SolicitudesComerciales.AsignarResponsable, p)
        };
    }
}

    public async Task<WriteResult> ReasignaAsesorAsync(SolicitudesComercialesReasignaAsesorDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_SOLICITUD_COMERCIAL", dto.NUM_SOLICITUD_COMERCIAL);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.SolicitudesComerciales.ReasignaAsesor, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.SolicitudesComerciales.ReasignaAsesor,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.SolicitudesComerciales.ReasignaAsesor, p)
        };
    }
}

    /// <summary>PROC_COMERCIAL_SOLICITUDES_LOADCOMBOS ejecuta, en este orden: Tipos de
    /// Solicitud Comercial, Entidades tipo Cliente ('CLI') y Estados — ninguno de los tres
    /// alias sus columnas como Id/Texto, así que cada resultset se mapea a mano (mismo
    /// patrón que GruposProductoRepository.LoadcombosAsync).</summary>
    public async Task<SolicitudesComercialesLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "COMERCIAL.SOLICITUDES_COMERCIALES" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAComercial.SolicitudesComerciales.Loadcombos,
            new { COD_TIPO_ESTADO = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);

        var tiposSolicitud = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_SOLICITUD_COMERICAL", "DES_TIPO_SOLICITUD_COMERICAL");
        var clientes        = MapEntidadCombo(await multi.ReadAsync<dynamic>());
        var estados          = MapEstados(await multi.ReadAsync<dynamic>());
        // Entidades (todas) — 4° resultset, agregado al final del SP (EXEC
        // PROC_ESTANDAR_ENTIDADES_SHOWCOMBOX). Antes se traía con una llamada HTTP aparte a
        // EstandarRoute.EntidadesCombobox (EntidadesRepository.ShowcomboxAsync); se replica
        // acá la MISMA lógica de prioridad de columnas de texto que usa ese método.
        var entidades        = MapEntidadesCombo(await multi.ReadAsync<dynamic>());

        return new SolicitudesComercialesLoadCombosResult
        {
            CodTipoEstado  = te?.COD_TIPO_ESTADO ?? 0,
            TiposSolicitud = tiposSolicitud,
            Clientes       = clientes,
            Estados        = estados,
            Entidades      = entidades
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

    // Misma lógica de prioridad de EntidadesRepository.ShowcomboxAsync: primero
    // COD_ENTIDAD/Id para el código, y para el texto DES_NOMBRE_COMPLETO, luego
    // DES_COMERCIAL, luego DES_NOMBRE, luego DES_ENTIDAD (primera columna no vacía).
    private static List<ComboboxItem> MapEntidadesCombo(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var id = new[] { "Id", "COD_ENTIDAD" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
            var tx = new[] { "Texto", "DES_NOMBRE_COMPLETO", "DES_COMERCIAL", "DES_NOMBRE", "DES_ENTIDAD" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
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
