using System.Data;
using System.Text.Json;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Personal por categoria ocupacional y armado de lineas (scripts SQL 124 y 125). Las validaciones salen como SqlException
/// (50000 de los catalogos estandar y 52560 a 52599 de los scripts) y las atiende el servicio.</summary>
public class PersonalLineasRepository : IPersonalLineasRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalLineasRepository(IDbConnectionFactory db) => _db = db;

    private static readonly CommandType SP = CommandType.StoredProcedure;

    // ===== O5 =====
    public async Task<List<PersonalOperacionItem>> PersonalListarAsync(string? buscar)
    {
        using var conn = _db.CreateConnection();
        if (string.IsNullOrWhiteSpace(buscar))
            return (await conn.QueryAsync<PersonalOperacionItem>("PROC_PRODUCCION_PERSONAL_OPERACION_SHOWALL", commandType: SP)).ToList();
        return (await conn.QueryAsync<PersonalOperacionItem>("PROC_PRODUCCION_PERSONAL_OPERACION_SEARCH", new { SEARCH = buscar.Trim() }, commandType: SP)).ToList();
    }

    public async Task<PersonalOperacionItem?> PersonalAsync(int codEntidad)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<PersonalOperacionItem>("PROC_PRODUCCION_PERSONAL_OPERACION_SHOWBYID", new { COD_ENTIDAD_PERSONAL = codEntidad }, commandType: SP)).FirstOrDefault();
    }

    public async Task PersonalGuardarAsync(PersonalOperacionGuardarDto d, bool nuevo, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", d.COD_ENTIDAD_PERSONAL);
        p.Add("COD_CATEGORIA_OCUPACIONAL", d.COD_CATEGORIA_OCUPACIONAL);
        p.Add("COD_PROCESO", d.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", string.IsNullOrWhiteSpace(d.COD_SUB_PROCESO) ? null : d.COD_SUB_PROCESO);
        p.Add("COD_OPERACION", string.IsNullOrWhiteSpace(d.COD_OPERACION) ? null : d.COD_OPERACION);
        p.Add("COD_TIPO_ESTADO", d.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", d.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PRODUCCION_PERSONAL_OPERACION_" + (nuevo ? "INSERT" : "UPDATE"), p, commandType: SP);
    }

    public async Task PersonalEliminarAsync(int codEntidad)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_PRODUCCION_PERSONAL_OPERACION_DELETE", new { COD_ENTIDAD_PERSONAL = codEntidad }, commandType: SP);
    }

    public async Task<List<PersonaDisponible>> PersonalDisponiblesAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<PersonaDisponible>("PROC_PRODUCCION_PERSONAL_OPERACION_DISPONIBLES", commandType: SP)).ToList();
    }

    public async Task<List<PersonalResumenItem>> PersonalResumenAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<PersonalResumenItem>("PROC_PRODUCCION_PERSONAL_OPERACION_RESUMEN", commandType: SP)).ToList();
    }

    /// <summary>Combos del alta: categorias activas, procesos, subprocesos (DES_DETALLE = su proceso) y operaciones simples activas
    /// (DES_DETALLE = su proceso, COD_REGLA = su categoria).</summary>
    public async Task<Dictionary<string, List<CatalogoOperacionItem>>> PersonalCombosAsync()
    {
        using var conn = _db.CreateConnection();
        var r = new Dictionary<string, List<CatalogoOperacionItem>>();
        r["categorias"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            "SELECT c.COD_CATEGORIA_OCUPACIONAL AS COD, c.DES_CATEGORIA_OCUPACIONAL AS DES, c.DES_CRITERIO AS DES_DETALLE FROM INGENIERIA.CATEGORIAS_OCUPACIONALES c WHERE c.COD_ESTADO = 0 ORDER BY c.NUM_ORDEN, c.COD_CATEGORIA_OCUPACIONAL")).ToList();
        r["procesos"] = (await conn.QueryAsync<CatalogoOperacionItem>("SELECT p.COD_PROCESO AS COD, p.DES_PROCESO AS DES FROM PRODUCCION.PROCESOS p ORDER BY p.DES_PROCESO")).ToList();
        r["subprocesos"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            "SELECT s.COD_SUB_PROCESO AS COD, s.DES_SUB_PROCESO AS DES, s.COD_PROCESO AS DES_DETALLE FROM PRODUCCION.SUB_PROCESOS s ORDER BY s.DES_SUB_PROCESO")).ToList();
        r["operaciones"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            @"SELECT o.COD_OPERACION AS COD, o.DES_OPERACION AS DES, o.COD_PROCESO AS DES_DETALLE, o.COD_CATEGORIA_OCUPACIONAL AS COD_REGLA
              FROM INGENIERIA.OPERACIONES o
              WHERE o.COD_ESTADO = 0 AND NOT EXISTS (SELECT 1 FROM INGENIERIA.OPERACIONES_COMPONENTES c WHERE c.COD_OPERACION_PADRE = o.COD_OPERACION)
              ORDER BY o.DES_OPERACION")).ToList();
        return r;
    }

    // ===== O6 =====
    public async Task<List<Armado>> ArmadosAsync(string? buscar)
    {
        using var conn = _db.CreateConnection();
        if (string.IsNullOrWhiteSpace(buscar))
            return (await conn.QueryAsync<Armado>("PROC_INGENIERIA_LINEAS_ARMADOS_SHOWALL", commandType: SP)).ToList();
        return (await conn.QueryAsync<Armado>("PROC_INGENIERIA_LINEAS_ARMADOS_SEARCH", new { SEARCH = buscar.Trim() }, commandType: SP)).ToList();
    }

    public async Task<Armado?> ArmadoAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<Armado>("PROC_INGENIERIA_LINEAS_ARMADOS_SHOWBYID", new { COD_ARMADO = cod }, commandType: SP)).FirstOrDefault();
    }

    public async Task ArmadoGuardarAsync(ArmadoGuardarDto d, bool nuevo, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_ARMADO", d.COD_ARMADO);
        p.Add("DES_ARMADO", d.DES_ARMADO);
        p.Add("COD_RUTA_PRODUCCION", d.COD_RUTA_PRODUCCION);
        p.Add("COD_PROCESO", d.COD_PROCESO);
        p.Add("COD_TIPO_DISTRIBUCION", d.COD_TIPO_DISTRIBUCION);
        p.Add("COD_LINEA_PRODUCCION", string.IsNullOrWhiteSpace(d.COD_LINEA_PRODUCCION) ? null : d.COD_LINEA_PRODUCCION);
        p.Add("VAL_MINUTOS_TURNO", d.VAL_MINUTOS_TURNO);
        p.Add("TAS_EFICIENCIA", d.TAS_EFICIENCIA);
        p.Add("VAL_META_UNIDADES", d.VAL_META_UNIDADES);
        p.Add("DES_OBSERVACION", string.IsNullOrWhiteSpace(d.DES_OBSERVACION) ? null : d.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", d.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", d.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_INGENIERIA_LINEAS_ARMADOS_" + (nuevo ? "INSERT" : "UPDATE"), p, commandType: SP);
    }

    public async Task ArmadoEliminarAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_INGENIERIA_LINEAS_ARMADOS_DELETE", new { COD_ARMADO = cod }, commandType: SP);
    }

    public async Task<string> ArmadoSiguienteCodigoAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<string>("SELECT INGENIERIA.FN_ARMADO_SIGUIENTE_CODIGO()");
    }

    public async Task<List<ArmadoPuesto>> ArmadoPuestosAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoPuesto>("PROC_INGENIERIA_LINEAS_ARMADOS_PUESTOS_SHOWBYARMADO", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task<List<ArmadoHoja>> ArmadoHojasAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoHoja>("PROC_INGENIERIA_LINEAS_ARMADOS_HOJAS", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task<List<ArmadoAviso>> ArmadoAvisosAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoAviso>("PROC_INGENIERIA_LINEAS_ARMADOS_VALIDAR", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task<List<ArmadoOperariosCategoria>> ArmadoOperariosPorCategoriaAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoOperariosCategoria>("PROC_INGENIERIA_LINEAS_ARMADOS_OPERARIOS_POR_CATEGORIA", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task ArmadoPuestosGuardarAsync(ArmadoPuestosGuardarDto d, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(d.Puestos.Select(x => new { x.NUM_PUESTO, x.DES_PUESTO, x.NUM_OPERARIOS, x.HOJAS }));
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_INGENIERIA_LINEAS_ARMADOS_PUESTOS_GUARDAR", new { d.COD_ARMADO, JSON = json, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<ArmadoSugerido> ArmadoSugerirAsync(string cod, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoSugerido>("PROC_INGENIERIA_LINEAS_ARMADOS_SUGERIR", new { COD_ARMADO = cod, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP)).FirstOrDefault() ?? new ArmadoSugerido();
    }

    /// <summary>Combos del armado: rutas con operaciones del catalogo, los procesos de cada ruta (DES_DETALLE = ruta), las lineas de produccion
    /// (DES_DETALLE = proceso, COD_REGLA = su disposicion por defecto) y las disposiciones (COD_REGLA = regla de ciclo, FLG_1 flujo unitario, FLG_2 exige precedencia).</summary>
    public async Task<Dictionary<string, List<CatalogoOperacionItem>>> ArmadoCombosAsync()
    {
        using var conn = _db.CreateConnection();
        var r = new Dictionary<string, List<CatalogoOperacionItem>>();
        r["rutas"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            @"SELECT r.COD_RUTA_PRODUCCION AS COD, r.DES_RUTA_PRODUCCION AS DES FROM INGENIERIA.PRODUCCION_RUTAS r
              WHERE EXISTS (SELECT 1 FROM INGENIERIA.VW_RUTAS_NODOS e WHERE e.COD_RUTA_PRODUCCION = r.COD_RUTA_PRODUCCION AND e.COD_OPERACION IS NOT NULL)
              ORDER BY r.COD_RUTA_PRODUCCION")).ToList();
        r["rutaprocesos"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            @"SELECT e.COD_PROCESO AS COD, p.DES_PROCESO AS DES, e.COD_RUTA_PRODUCCION AS DES_DETALLE FROM INGENIERIA.VW_RUTAS_NODOS e
              JOIN PRODUCCION.PROCESOS p ON p.COD_PROCESO = e.COD_PROCESO
              WHERE e.NUM_ESTRUCTURA_RUTA_PROCESO_PADRE IS NULL AND e.FLG_PROCESO = 1 ORDER BY e.COD_RUTA_PRODUCCION, e.NUM_ORDEN")).ToList();
        r["lineas"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            @"SELECT l.COD_LINEA_PRODUCCION AS COD, l.DES_LINEA_PRODUCCION AS DES, l.COD_PROCESO AS DES_DETALLE, l.COD_TIPO_DISTRIBUCION_DEFECTO AS COD_REGLA
              FROM PRODUCCION.PROCESOS_LINEAS l ORDER BY l.COD_PROCESO, l.COD_LINEA_PRODUCCION")).ToList();
        r["distribuciones"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            @"SELECT d.COD_TIPO_DISTRIBUCION AS COD, d.DES_TIPO_DISTRIBUCION AS DES, d.COD_REGLA_CICLO AS COD_REGLA, d.FLG_FLUJO_UNITARIO AS FLG_1, d.FLG_REQUIERE_PRECEDENCIA AS FLG_2
              FROM PRODUCCION.TIPOS_DISTRIBUCION_LINEA d WHERE d.COD_ESTADO = 0 ORDER BY d.DES_TIPO_DISTRIBUCION")).ToList();
        return r;
    }

    // ===== O7 =====
    public async Task<List<ArmadoPersonaAsignada>> ArmadoPersonalAsignadoAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoPersonaAsignada>("PROC_INGENIERIA_LINEAS_ARMADOS_PERSONAL_ASIGNADO", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task<List<ArmadoPersonaDisponible>> ArmadoPersonalDisponibleAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoPersonaDisponible>("PROC_INGENIERIA_LINEAS_ARMADOS_PERSONAL_DISPONIBLE", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task<List<ArmadoCobertura>> ArmadoCoberturaAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoCobertura>("PROC_INGENIERIA_LINEAS_ARMADOS_PERSONAL_COBERTURA", new { COD_ARMADO = cod }, commandType: SP)).ToList();
    }

    public async Task ArmadoPersonalGuardarAsync(ArmadoPersonalGuardarDto d, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(d.Puestos.Select(x => new { x.NUM_PUESTO, x.PERSONAS }));
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync("PROC_INGENIERIA_LINEAS_ARMADOS_PERSONAL_GUARDAR", new { d.COD_ARMADO, JSON = json, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<ArmadoPersonalSugerido> ArmadoPersonalSugerirAsync(string cod, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ArmadoPersonalSugerido>("PROC_INGENIERIA_LINEAS_ARMADOS_PERSONAL_SUGERIR", new { COD_ARMADO = cod, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP)).FirstOrDefault() ?? new ArmadoPersonalSugerido();
    }

    /// <summary>Compara las disposiciones: por cada una, el balance que armaria "sugerir" y el personal que le faltaria (la base lo calcula con armados temporales que borra al terminar).</summary>
    public async Task<ArmadoComparacionResultado> ArmadoCompararAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        var r = new ArmadoComparacionResultado();
        r.Disposiciones = (await conn.QueryAsync<ArmadoComparacion>("PROC_INGENIERIA_LINEAS_ARMADOS_COMPARAR", new { COD_ARMADO = cod, DETALLE = false }, commandType: SP)).ToList();
        r.Categorias = (await conn.QueryAsync<ArmadoComparacionCategoria>("PROC_INGENIERIA_LINEAS_ARMADOS_COMPARAR", new { COD_ARMADO = cod, DETALLE = true }, commandType: SP)).ToList();
        return r;
    }
}
