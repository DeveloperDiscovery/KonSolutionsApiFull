using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Lecturas y acciones del expediente del proyecto (script 56). Los errores de negocio los lanzan los procedimientos con RAISERROR: llegan como SqlException con el texto ya en español.</summary>
public class ProyectosExpedienteRepository : IProyectosExpedienteRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosExpedienteRepository(IDbConnectionFactory db) => _db = db;

    private static readonly CommandType SP = CommandType.StoredProcedure;

    public async Task<ExpedienteResumen?> ResumenAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ExpedienteResumen>("PROC_PROYECTOS_EXPEDIENTE_RESUMEN", new { NUM_PROYECTO = num }, commandType: SP);
    }

    public async Task<ExpedientePlanificacion> PlanificacionAsync(int num)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync("PROC_PROYECTOS_EXPEDIENTE_PLANIFICACION", new { NUM_PROYECTO = num }, commandType: SP);
        return new ExpedientePlanificacion
        {
            Fases = (await m.ReadAsync<ExpedienteFase>()).ToList(),
            Tareas = (await m.ReadAsync<ExpedienteTarea>()).ToList(),
            SubTareas = (await m.ReadAsync<ExpedienteSubTarea>()).ToList(),
            Hitos = (await m.ReadAsync<ExpedienteHito>()).ToList()
        };
    }

    public async Task<IEnumerable<ExpedienteRecurso>> RecursosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteRecurso>("PROC_PROYECTOS_EXPEDIENTE_RECURSOS", new { NUM_PROYECTO = num }, commandType: SP);
    }

    public async Task<ExpedienteCostos> CostosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync("PROC_PROYECTOS_EXPEDIENTE_COSTOS", new { NUM_PROYECTO = num }, commandType: SP);
        return new ExpedienteCostos
        {
            Resumen = await m.ReadFirstOrDefaultAsync<ExpedienteCostoResumen>(),
            PorFase = (await m.ReadAsync<ExpedienteCostoFase>()).ToList(),
            Gastos = (await m.ReadAsync<ExpedienteGasto>()).ToList(),
            Comprobantes = (await m.ReadAsync<ExpedienteComprobante>()).ToList(),
            Adicionales = (await m.ReadAsync<ExpedienteGastoAdicional>()).ToList()
        };
    }

    public async Task RecursoCostoGuardarAsync(int num, ExpedienteRecursoCostoDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", num); p.Add("COD_TAREA", dto.CodTarea, DbType.Byte); p.Add("COD_ENTIDAD_ASIGNADA", dto.CodEntidad); p.Add("COD_ROL_TAREA", dto.CodRol);
        p.Add("NUM_HORAS_PROGRAMADAS", dto.HorasProgramadas); p.Add("NUM_HORAS_EFECTIVAS", dto.HorasEfectivas); p.Add("VAL_COSTO_HORA", dto.CostoHora); p.Add("TAS_UTILIDAD", dto.Utilidad);
        p.Add("COD_USUARIO", codUsuario); p.Add("COD_ESTACION", codEstacion);
        await conn.ExecuteAsync("PROC_PROYECTOS_RECURSO_COSTO_GUARDAR", p, commandType: SP);
    }

    public async Task<IEnumerable<decimal>> EntidadesPropiasAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<decimal>("PROC_PROYECTOS_ENTIDADES_PROPIAS", commandType: SP);
    }

    public async Task<IEnumerable<ExpedienteTipoPropio>> TiposPropiosAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteTipoPropio>("PROC_PROYECTOS_TIPOS_ENTIDAD_PROPIOS_SHOW", commandType: SP);
    }

    public async Task TiposPropiosGuardarAsync(IEnumerable<string> tipos, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_TIPOS_ENTIDAD_PROPIOS_GUARDAR", new { TIPOS = string.Join(",", tipos), COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: SP);
    }

    public async Task<ExpedienteRiesgos> RiesgosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync("PROC_PROYECTOS_EXPEDIENTE_RIESGOS", new { NUM_PROYECTO = num }, commandType: SP);
        return new ExpedienteRiesgos { Riesgos = (await m.ReadAsync<ExpedienteRiesgo>()).ToList(), Planes = (await m.ReadAsync<ExpedientePlan>()).ToList() };
    }

    public async Task<ExpedienteDocumentos> DocumentosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync("PROC_PROYECTOS_EXPEDIENTE_DOCUMENTOS", new { NUM_PROYECTO = num }, commandType: SP);
        return new ExpedienteDocumentos { Archivos = (await m.ReadAsync<ExpedienteArchivo>()).ToList(), Memoria = (await m.ReadAsync<ExpedienteMemoria>()).ToList() };
    }

    public async Task<ExpedienteMateriales> MaterialesAsync(int num)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync("PROC_PROYECTOS_EXPEDIENTE_MATERIALES", new { NUM_PROYECTO = num }, commandType: SP);
        return new ExpedienteMateriales
        {
            Plan = (await m.ReadAsync<ExpedienteMaterial>()).ToList(),
            SinPlan = (await m.ReadAsync<ExpedienteConsumoSinPlan>()).ToList(),
            Movimientos = (await m.ReadAsync<ExpedienteMovimiento>()).ToList()
        };
    }

    public async Task<IEnumerable<ExpedienteTipoEntidad>> TiposEntidadAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteTipoEntidad>("PROC_PROYECTOS_EXPEDIENTE_TIPOS_ENTIDAD", new { NUM_PROYECTO = num }, commandType: SP);
    }

    public async Task GuardarTiposEntidadAsync(int num, IEnumerable<string> tipos, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_TIPOS_ENTIDAD_GUARDAR",
            new { NUM_PROYECTO = num, LISTA = string.Join(",", tipos), COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: SP);
    }

    public async Task<IEnumerable<ComboboxItem>> EntidadesElegiblesAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, "PROC_PROYECTOS_EXPEDIENTE_ENTIDADES_ELEGIBLES", new { NUM_PROYECTO = num });
    }

    public async Task RecalcularAsync(int num, string codUsuario, string codEstacion, DateTime? fecReal = null)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_RECALCULAR", new { NUM_PROYECTO = num, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion, FEC_REAL = fecReal?.Date }, commandType: SP);
    }

    /// <summary>La baja de una asignación lleva la clave completa (proyecto, tarea, persona y rol); la ruta estándar de la tabla no trae el rol.</summary>
    public async Task QuitarRecursoAsync(int num, int codTarea, decimal codEntidad, string codRol)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_PROYECTOS_RECURSO_HUMANO_ASIGNADO_DELETE", new { NUM_PROYECTO = num, COD_TAREA = codTarea, COD_ENTIDAD_ASIGNADA = codEntidad, COD_ROL_TAREA = codRol }, commandType: SP);
    }

    public async Task<IEnumerable<ExpedienteMovido>> MoverAsync(int num, ExpedienteMoverDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", num);
        p.Add("TIPO", dto.Tipo, DbType.AnsiStringFixedLength, size: 3);
        p.Add("COD", dto.Cod, DbType.Byte);
        p.Add("FEC_INICIO", dto.FecInicio?.Date, DbType.Date);
        p.Add("FEC_FINAL", dto.FecFinal.Date, DbType.Date);
        p.Add("MOVER_HIJOS", dto.MoverHijos);
        p.Add("MODO", dto.Modo == "R" ? "R" : "M", DbType.AnsiStringFixedLength, size: 1);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        return await conn.QueryAsync<ExpedienteMovido>("PROC_PROYECTOS_PLANIFICACION_MOVER", p, commandType: SP);
    }

    public async Task<ExpedienteAvanceResult?> AvanceAsync(int num, int codTarea, ExpedienteAvanceDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ExpedienteAvanceResult>("PROC_PROYECTOS_TAREAS_AVANCE",
            new { NUM_PROYECTO = num, COD_TAREA = codTarea, CAN_EJECUTADAS = dto.CanEjecutadas, TAS_EJECUCION = dto.TasEjecucion, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion, FEC_REAL = dto.FecReal?.Date }, commandType: SP);
    }

    public async Task<IEnumerable<ExpedienteEstadoHistorial>> EstadosHistorialAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteEstadoHistorial>("PROC_PROYECTOS_EXPEDIENTE_ESTADOS_HISTORIAL", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Cargo y area de una entidad (ADMINISTRACION.AREAS_CARGOS_ENTIDADES); vacios si no tiene.</summary>
    public async Task<ExpedienteCargoArea?> CargoAreaAsync(string codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ExpedienteCargoArea>("SELECT DES_CARGO, DES_AREA FROM dbo.FNC_ENTIDAD_CARGO_AREA(@cod)", new { cod = codEntidad });
    }

    public async Task<IEnumerable<ExpedienteOrganigramaNodo>> OrganigramaAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteOrganigramaNodo>("PROC_PROYECTOS_EXPEDIENTE_ORGANIGRAMA", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ExpedienteMemoria>> MemoriasAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteMemoria>("PROC_PROYECTOS_EXPEDIENTE_MEMORIAS", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ExpedienteEstadoIcono>> EstadosIconosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteEstadoIcono>("PROC_PROYECTOS_EXPEDIENTE_ESTADOS_ICONOS", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ExpedienteCompromiso>> CompromisosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteCompromiso>("PROC_PROYECTOS_EXPEDIENTE_COMPROMISOS", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ExpedienteCompromisoPersona>> CompromisoPersonasAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteCompromisoPersona>("PROC_PROYECTOS_COMPROMISO_PERSONAS", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ExpedienteCompromisoDecision>> CompromisoDecisionesAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteCompromisoDecision>("PROC_PROYECTOS_COMPROMISO_DECISIONES", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> CompromisoTiposAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, "PROC_PROYECTOS_TIPOS_DOCUMENTO_COMPROMISO_SHOWCOMBOX");
    }

    /// <summary>Alta (NumSecuencia vacio) o modificacion; devuelve la secuencia del documento.</summary>
    public async Task<int> CompromisoGuardarAsync(int num, ExpedienteCompromisoDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>("PROC_PROYECTOS_COMPROMISO_GUARDAR",
            new
            {
                NUM_PROYECTO = num, NUM_SECUENCIA = dto.NumSecuencia is > 0 ? dto.NumSecuencia : null, COD_TIPO_DOCUMENTO_COMPROMISO = dto.CodTipoDocumento,
                COD_FASE = dto.CodFase, COD_TAREA = dto.CodTarea, DES_DOCUMENTO = dto.DesDocumento, NUM_SECUENCIA_ARCHIVO = dto.NumSecuenciaArchivo,
                RESPONSABLES = string.Join(",", dto.Responsables.Distinct()), APROBADORES = string.Join(",", dto.Aprobadores.Distinct()),
                COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
            }, commandType: CommandType.StoredProcedure);
    }

    public async Task<string?> CompromisoDecidirAsync(int num, int secuencia, ExpedienteCompromisoDecidirDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<ExpedienteCompromisoEstadoResult>("PROC_PROYECTOS_COMPROMISO_DECIDIR",
            new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia, COD_ENTIDAD = dto.CodEntidad, COD_DECISION = dto.CodDecision, DES_OBSERVACION = dto.Observacion, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion },
            commandType: CommandType.StoredProcedure);
        return r?.COD_ESTADO_COMPROMISO;
    }

    public async Task CompromisoQuitarAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_COMPROMISO_QUITAR", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ExpedienteEvento>> EventosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ExpedienteEvento>("PROC_PROYECTOS_EXPEDIENTE_EVENTOS", new { NUM_PROYECTO = num }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Alta (siguiente secuencia del proyecto) o modificacion de un evento; devuelve su secuencia.</summary>
    public async Task<int> EventoGuardarAsync(int num, ExpedienteEventoDto dto, bool esNuevo, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var sec = dto.NumSecuencia ?? 0;
        if (esNuevo) sec = await conn.QueryFirstAsync<int>("SELECT ISNULL(MAX(NUM_SECUENCIA), 0) + 1 FROM PROYECTOS.PROYECTOS_EVENTOS WHERE NUM_PROYECTO = @num", new { num });
        var p = new { NUM_PROYECTO = num, NUM_SECUENCIA = sec, COD_FASE = dto.CodFase, COD_TAREA = dto.CodTarea, DES_EVENTO = dto.DesEvento.Trim(), FLG_RETRASO = dto.FlgRetraso,
                      FEC_HORA_INICIO = dto.FecHoraInicio, FEC_HORA_FINAL = dto.FecHoraFinal, COD_ENTIDAD_RESPONSABLE = dto.CodEntidadResponsable,
                      DES_OBSERVACION = string.IsNullOrWhiteSpace(dto.DesObservacion) ? null : dto.DesObservacion.Trim(), COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion };
        await conn.ExecuteAsync(esNuevo ? "PROC_PROYECTOS_PROYECTOS_EVENTOS_INSERT" : "PROC_PROYECTOS_PROYECTOS_EVENTOS_UPDATE", p, commandType: CommandType.StoredProcedure);
        return sec;
    }

    public async Task EventoQuitarAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_PROYECTOS_EVENTOS_DELETE", new { NUM_PROYECTO = num, NUM_SECUENCIA = secuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<ExpedienteEstadoResult?> EstadoAplicarAsync(string nivel, int num, int cod, ExpedienteEstadoDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        // nivel P: el propio proyecto (envio a aprobacion y su resolucion); F y T: fase y tarea
        if (nivel == "P")
            return await conn.QueryFirstOrDefaultAsync<ExpedienteEstadoResult>("PROC_PROYECTOS_ESTADO_PROYECTO_APLICAR",
                new { NUM_PROYECTO = num, COD_APLICACION = dto.CodAplicacion, COD_OPCION_APLICACION = dto.CodOpcionAplicacion, COD_BOTON_OPCION = dto.CodBotonOpcion, DES_OBSERVACION = dto.Observacion, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion },
                commandType: CommandType.StoredProcedure);
        return await conn.QueryFirstOrDefaultAsync<ExpedienteEstadoResult>("PROC_PROYECTOS_ESTADO_APLICAR",
            new { NIVEL = nivel, NUM_PROYECTO = num, COD = cod, COD_APLICACION = dto.CodAplicacion, COD_OPCION_APLICACION = dto.CodOpcionAplicacion, COD_BOTON_OPCION = dto.CodBotonOpcion, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task ConfirmarAsync(int num, int codTarea, ExpedienteConfirmarDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PROYECTOS_TAREAS_CONFIRMAR",
            new { NUM_PROYECTO = num, COD_TAREA = codTarea, FLG_APROBAR = dto.Aprobar, COD_ENTIDAD = dto.CodEntidad, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion, FEC_REAL = dto.FecReal?.Date }, commandType: SP);
    }

    public async Task<int> AsignarGastoAsync(int num, ExpedienteGastoDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>("PROC_PROYECTOS_GASTOS_ASIGNAR",
            new { NUM_PROYECTO = num, NUM_REGISTRO_COMPROBANTE = dto.NumRegistroComprobante, COD_FASE = dto.CodFase, COD_TAREA = dto.CodTarea, VAL_GASTO_ASIGNADO = dto.Valor, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: SP);
    }

    public async Task<ExpedienteArchivo?> ArchivoAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ExpedienteArchivo>(
            "SELECT NUM_PROYECTO, NUM_SECUENCIA, COD_FASE, COD_TAREA, DES_PATH_FILE, COD_USUARIO_REGISTRO, FEC_REGISTRO FROM PROYECTOS.PROYECTOS_ARCHIVOS WHERE NUM_PROYECTO = @num AND NUM_SECUENCIA = @secuencia",
            new { num, secuencia });
    }

    public async Task<int> ArchivoRegistrarAsync(int num, int? codFase, int? codTarea, string path, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        // la secuencia siguiente y el alta en un solo paso, para que dos subidas juntas no choquen
        return await conn.QueryFirstAsync<int>(@"
            DECLARE @s SMALLINT = (SELECT ISNULL(MAX(NUM_SECUENCIA), 0) + 1 FROM PROYECTOS.PROYECTOS_ARCHIVOS WITH (UPDLOCK, HOLDLOCK) WHERE NUM_PROYECTO = @num)
            INSERT PROYECTOS.PROYECTOS_ARCHIVOS (NUM_PROYECTO, NUM_SECUENCIA, COD_FASE, COD_TAREA, DES_PATH_FILE, COD_USUARIO_REGISTRO, COD_ESTACION_REGISTRO, FEC_REGISTRO)
            VALUES (@num, @s, @codFase, @codTarea, @path, @codUsuario, @codEstacion, GETDATE())
            SELECT CONVERT(INT, @s)",
            new { num, codFase, codTarea, path, codUsuario, codEstacion });
    }

    public async Task ArchivoQuitarAsync(int num, int secuencia)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("DELETE PROYECTOS.PROYECTOS_ARCHIVOS WHERE NUM_PROYECTO = @num AND NUM_SECUENCIA = @secuencia", new { num, secuencia });
    }

    public async Task<IEnumerable<PortafolioProyecto>> PortafolioAsync(string? estado, string? tipo, string? search)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PortafolioProyecto>("PROC_PROYECTOS_PORTAFOLIO",
            new { COD_ESTADO_ALIAS = estado, COD_TIPO_PROYECTO = tipo, SEARCH = search }, commandType: SP);
    }

    public async Task<IEnumerable<MiTarea>> MisTareasAsync(decimal codEntidad, bool soloAbiertas)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MiTarea>("PROC_PROYECTOS_MIS_TAREAS", new { COD_ENTIDAD = codEntidad, SOLO_ABIERTAS = soloAbiertas }, commandType: SP);
    }
}
