using System.Data;
using System.Text.Json;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Operaciones, tiempos estandar y rutas con tiempos (scripts SQL 119 a 122). Las validaciones salen como SqlException
/// (50000 de los catalogos estandar, 52442 y 52500 a 52559 de los scripts 120 a 122) y las atiende el servicio.</summary>
public class OperacionesTiemposRepository : IOperacionesTiemposRepository
{
    private const string P = "PROC_INGENIERIA_";
    private readonly IDbConnectionFactory _db;
    public OperacionesTiemposRepository(IDbConnectionFactory db) => _db = db;

    private static readonly CommandType SP = CommandType.StoredProcedure;

    // ===== O1: catalogos =====
    private sealed record Cat(string Sch, string Tab, string Cod, string Des, string? Detalle, string? Orden, string? Flg1, string? Flg2, string? Regla);
    public static readonly string[] Catalogos = { "categorias", "maquinas", "metodos", "distribuciones", "materiales" };
    private static readonly Dictionary<string, Cat> Cats = new()
    {
        ["categorias"] = new("INGENIERIA", "CATEGORIAS_OCUPACIONALES", "COD_CATEGORIA_OCUPACIONAL", "DES_CATEGORIA_OCUPACIONAL", "DES_CRITERIO", "NUM_ORDEN", null, null, null),
        ["maquinas"] = new("INGENIERIA", "TIPOS_MAQUINA", "COD_TIPO_MAQUINA", "DES_TIPO_MAQUINA", null, null, "FLG_REQUIERE_MAQUINA", null, null),
        ["metodos"] = new("INGENIERIA", "METODOS_TIEMPO", "COD_METODO_TIEMPO", "DES_METODO_TIEMPO", null, null, null, null, null),
        ["distribuciones"] = new("PRODUCCION", "TIPOS_DISTRIBUCION_LINEA", "COD_TIPO_DISTRIBUCION", "DES_TIPO_DISTRIBUCION", null, null, "FLG_FLUJO_UNITARIO", "FLG_REQUIERE_PRECEDENCIA", "COD_REGLA_CICLO"),
        // tipos de material de la estructura (script 154/155): ejemplos = detalle, FLG_HABILITADO = FLG_1, la abreviatura viaja en COD_REGLA
        ["materiales"] = new("INGENIERIA", "TIPOS_MATERIAL_ESTRUCTURA", "COD_TIPO_MATERIAL", "DES_TIPO_MATERIAL", "DES_EJEMPLOS", "NUM_ORDEN", "FLG_HABILITADO", null, "DES_ABREVIATURA"),
    };
    private static Cat De(string catalogo) => Cats.TryGetValue(catalogo, out var c) ? c : throw new ArgumentException("Catalogo desconocido: " + catalogo);
    private static string Sp(Cat c, string accion) => $"PROC_{c.Sch}_{c.Tab}_{accion}";

    public async Task<List<CatalogoOperacionItem>> CatalogoListarAsync(string catalogo, bool soloActivos)
    {
        var c = De(catalogo);
        using var conn = _db.CreateConnection();
        var filas = await conn.QueryAsync(Sp(c, soloActivos ? "SHOWCOMBOX" : "SHOWALL"), commandType: SP);
        return filas.Select(f =>
        {
            var d = (IDictionary<string, object?>)f;
            object? V(string? col) => col != null && d.TryGetValue(col, out var v) && v is not DBNull ? v : null;
            return new CatalogoOperacionItem
            {
                COD = Convert.ToString(V(c.Cod)) ?? "",
                DES = Convert.ToString(V(c.Des)) ?? "",
                DES_DETALLE = Convert.ToString(V(c.Detalle)),
                NUM_ORDEN = V(c.Orden) is { } o ? Convert.ToInt32(o) : null,
                FLG_1 = V(c.Flg1) is { } f1 ? Convert.ToBoolean(f1) : null,
                FLG_2 = V(c.Flg2) is { } f2 ? Convert.ToBoolean(f2) : null,
                COD_REGLA = Convert.ToString(V(c.Regla)),
                COD_TIPO_ESTADO = V("COD_TIPO_ESTADO") is { } te ? Convert.ToInt16(te) : null,
                COD_ESTADO = V("COD_ESTADO") is { } es ? Convert.ToInt16(es) : null,
                DES_ESTADO = Convert.ToString(V("DES_ESTADO")),
                DES_BACKCOLOR = Convert.ToString(V("DES_BACKCOLOR")),
                DES_FORECOLOR = Convert.ToString(V("DES_FORECOLOR")),
            };
        }).ToList();
    }

    public async Task CatalogoGuardarAsync(string catalogo, CatalogoOperacionItem it, bool nuevo, string codUsuario, string codEstacion)
    {
        var c = De(catalogo);
        var p = new DynamicParameters();
        p.Add(c.Cod, it.COD);
        p.Add(c.Des, it.DES);
        if (c.Detalle != null) p.Add(c.Detalle, string.IsNullOrWhiteSpace(it.DES_DETALLE) ? null : it.DES_DETALLE);
        if (c.Orden != null) p.Add(c.Orden, (short)(it.NUM_ORDEN ?? 0));
        if (c.Flg1 != null) p.Add(c.Flg1, it.FLG_1 ?? false);
        if (c.Flg2 != null) p.Add(c.Flg2, it.FLG_2 ?? false);
        if (c.Regla != null) p.Add(c.Regla, it.COD_REGLA);
        p.Add("COD_TIPO_ESTADO", it.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", it.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(Sp(c, nuevo ? "INSERT" : "UPDATE"), p, commandType: SP);
    }

    public async Task CatalogoEliminarAsync(string catalogo, string cod)
    {
        var c = De(catalogo);
        var p = new DynamicParameters();
        p.Add(c.Cod, cod);
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(Sp(c, "DELETE"), p, commandType: SP);
    }

    public async Task<Dictionary<string, List<CatalogoOperacionItem>>> CombosAsync()
    {
        var r = new Dictionary<string, List<CatalogoOperacionItem>>();
        foreach (var c in Catalogos) r[c] = await CatalogoListarAsync(c, true);
        using var conn = _db.CreateConnection();
        // tipos de operacion: COD_REGLA lleva la clase (manual, maquina...); subprocesos: DES_DETALLE lleva el proceso al que pertenecen
        r["tipos"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            "SELECT t.COD_TIPO_OPERACION_PRODUCCION AS COD, t.DES_TIPO_OPERACION_PRODUCCION AS DES, t.COD_CLASE_OPERACION_PRODUCCION AS COD_REGLA FROM PLANEAMIENTO.TIPOS_OPERACION_PRODUCCION t ORDER BY t.DES_TIPO_OPERACION_PRODUCCION")).ToList();
        r["procesos"] = (await conn.QueryAsync<CatalogoOperacionItem>("SELECT p.COD_PROCESO AS COD, p.DES_PROCESO AS DES FROM PRODUCCION.PROCESOS p ORDER BY p.DES_PROCESO")).ToList();
        r["subprocesos"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            "SELECT s.COD_SUB_PROCESO AS COD, s.DES_SUB_PROCESO AS DES, s.COD_PROCESO AS DES_DETALLE FROM PRODUCCION.SUB_PROCESOS s ORDER BY s.DES_SUB_PROCESO")).ToList();
        r["unidades"] = (await conn.QueryAsync<CatalogoOperacionItem>(
            "SELECT u.COD_UNIDAD_MEDIDA AS COD, u.DES_UNIDAD_MEDIDA AS DES FROM ESTANDAR.UNIDADES_MEDIDA u WHERE u.COD_UNIDAD_MEDIDA IN ('MIN', 'HUR', 'DAY', 'SEC') ORDER BY u.DES_UNIDAD_MEDIDA")).ToList();
        return r;
    }

    // ===== O2: operaciones y composicion =====
    public async Task<List<Operacion>> OperacionesAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<Operacion>(P + "OPERACIONES_SHOWALL", commandType: SP)).ToList();
    }

    public async Task<Operacion?> OperacionAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Operacion>(P + "OPERACIONES_SHOWBYID", new { COD_OPERACION = cod }, commandType: SP);
    }

    public async Task OperacionGuardarAsync(OperacionGuardarDto d, bool nuevo, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "OPERACIONES_" + (nuevo ? "INSERT" : "UPDATE"), new
        {
            d.COD_OPERACION, d.DES_OPERACION, d.COD_TIPO_OPERACION_PRODUCCION, d.COD_PROCESO,
            COD_SUB_PROCESO = Vacio(d.COD_SUB_PROCESO), COD_CATEGORIA_OCUPACIONAL = Vacio(d.COD_CATEGORIA_OCUPACIONAL), COD_TIPO_MAQUINA = Vacio(d.COD_TIPO_MAQUINA),
            COD_UNIDAD_MEDIDA_TIEMPO = Vacio(d.COD_UNIDAD_MEDIDA_TIEMPO), DES_OBSERVACION = Vacio(d.DES_OBSERVACION),
            d.COD_TIPO_ESTADO, d.COD_ESTADO, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion
        }, commandType: SP);
    }

    private static string? Vacio(string? s) => string.IsNullOrWhiteSpace(s) ? null : s;

    public async Task OperacionEliminarAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "OPERACIONES_DELETE", new { COD_OPERACION = cod }, commandType: SP);
    }

    public async Task<string> SiguienteCodigoAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<string>("SELECT INGENIERIA.FN_OPERACION_SIGUIENTE_CODIGO()");
    }

    public async Task<List<OperacionComponente>> ComponentesAsync(string codPadre)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<OperacionComponente>(P + "OPERACIONES_COMPONENTES_SHOWBYPADRE", new { COD_OPERACION_PADRE = codPadre }, commandType: SP)).ToList();
    }

    public async Task ComponentesGuardarAsync(OperacionComponentesGuardarDto dto, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(dto.Componentes.Select(x => new { x.COD_OPERACION_HIJA, x.VAL_FRECUENCIA, x.FLG_PARALELO }));
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "OPERACIONES_COMPONENTES_GUARDAR", new { dto.COD_OPERACION_PADRE, JSON = json, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<List<OperacionArbolNodo>> ArbolAsync(string cod, DateTime? fecha)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<OperacionArbolNodo>(P + "OPERACIONES_ARBOL_SAM", new { COD_OPERACION = cod, FECHA = fecha }, commandType: SP)).ToList();
    }

    public async Task<List<OperacionUsadaEn>> UsadaEnAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<OperacionUsadaEn>(P + "OPERACIONES_USADA_EN", new { COD_OPERACION = cod }, commandType: SP)).ToList();
    }

    public async Task<List<OperacionUsadaEnRuta>> UsadaEnRutasAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<OperacionUsadaEnRuta>(P + "OPERACIONES_USADA_EN_RUTAS", new { COD_OPERACION = cod }, commandType: SP)).ToList();
    }

    // ===== O3: tiempos y estudios =====
    public async Task<List<OperacionTiempo>> TiemposAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<OperacionTiempo>(P + "OPERACIONES_TIEMPOS_SHOWBYOPERACION", new { COD_OPERACION = cod }, commandType: SP)).ToList();
    }

    public async Task<OperacionTiempoResultado> TiempoGuardarAsync(OperacionTiempoGuardarDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<OperacionTiempoResultado>(P + "OPERACIONES_TIEMPOS_GUARDAR", new
        {
            d.COD_OPERACION, d.NUM_VERSION, d.COD_METODO_TIEMPO, d.NUM_ESTUDIO_TIEMPO, d.VAL_TIEMPO_BASE, d.VAL_SUPLEMENTO_PCT, DES_OBSERVACION = Vacio(d.DES_OBSERVACION),
            COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion, MOSTRAR = true
        }, commandType: SP);
    }

    public async Task TiempoAprobarAsync(OperacionTiempoClaveDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "OPERACIONES_TIEMPOS_APROBAR", new { d.COD_OPERACION, d.NUM_VERSION, d.FEC_VIGENCIA_DESDE, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task TiempoEliminarAsync(OperacionTiempoClaveDto d)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "OPERACIONES_TIEMPOS_ELIMINAR", new { d.COD_OPERACION, d.NUM_VERSION }, commandType: SP);
    }

    public async Task<List<EstudioTiempo>> EstudiosAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<EstudioTiempo>(P + "ESTUDIOS_TIEMPO_SHOWBYOPERACION", new { COD_OPERACION = cod }, commandType: SP)).ToList();
    }

    public async Task<List<EstudioTiempoElemento>> EstudioElementosAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<EstudioTiempoElemento>(P + "ESTUDIOS_TIEMPO_ELEMENTOS", new { NUM_ESTUDIO_TIEMPO = num }, commandType: SP)).ToList();
    }

    public async Task<EstudioTiempoResultado> EstudioGuardarAsync(EstudioTiempoGuardarDto d, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(d.Elementos.Select(e => new { e.DES_ELEMENTO, e.VAL_TIEMPO_OBSERVADO, e.VAL_FRECUENCIA, e.FLG_ELEMENTO_MAQUINA }));
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<EstudioTiempoResultado>(P + "ESTUDIOS_TIEMPO_GUARDAR", new
        {
            d.NUM_ESTUDIO_TIEMPO, d.COD_OPERACION, d.COD_METODO_TIEMPO, d.FEC_ESTUDIO, d.COD_ENTIDAD_ANALISTA, d.VAL_RITMO_PCT, d.CAN_CICLOS, DES_OBSERVACION = Vacio(d.DES_OBSERVACION), JSON = json,
            COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion, MOSTRAR = true
        }, commandType: SP);
    }

    public async Task EstudioEliminarAsync(int num)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTUDIOS_TIEMPO_ELIMINAR", new { NUM_ESTUDIO_TIEMPO = num }, commandType: SP);
    }

    public async Task<List<SamPorCategoria>> SamPorCategoriaOperacionAsync(string cod, DateTime? fecha)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<SamPorCategoria>(P + "OPERACIONES_SAM_POR_CATEGORIA", new { COD_OPERACION = cod, FECHA = fecha }, commandType: SP)).ToList();
    }

    public async Task<List<SamPorClase>> SamPorClaseOperacionAsync(string cod, DateTime? fecha)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<SamPorClase>(P + "OPERACIONES_SAM_POR_CLASE", new { COD_OPERACION = cod, FECHA = fecha }, commandType: SP)).ToList();
    }

    // ===== O4: rutas con tiempos =====
    public async Task<List<RutaTiempoResumen>> RutasAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<RutaTiempoResumen>(P + "RUTAS_TIEMPOS_LISTA", commandType: SP)).ToList();
    }

    public async Task<List<RutaNodo>> RutaArbolAsync(string ruta, DateTime? fecha)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<RutaNodo>(P + "RUTAS_ARBOL_SAM", new { COD_RUTA_PRODUCCION = ruta, FECHA = fecha }, commandType: SP)).ToList();
    }

    public async Task<int> RutaProcesoAgregarAsync(RutaProcesoAgregarDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "RUTAS_PROCESO_AGREGAR", new { d.COD_RUTA_PRODUCCION, d.COD_PROCESO, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<int> RutaSubprocesoAgregarAsync(RutaSubprocesoAgregarDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "RUTAS_SUBPROCESO_AGREGAR", new { d.COD_RUTA_PRODUCCION, d.NUM_PADRE, d.COD_SUB_PROCESO, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<RutaOperacionAgregarResultado> RutaOperacionAgregarAsync(RutaOperacionAgregarDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<RutaOperacionAgregarResultado>(P + "RUTAS_OPERACIONES_AGREGAR",
            new { d.COD_RUTA_PRODUCCION, d.NUM_PADRE, d.COD_OPERACION, d.VAL_FRECUENCIA, d.FECHA, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<int> RutaQuitarAsync(RutaFilaClaveDto d)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "RUTAS_OPERACIONES_QUITAR", new { d.COD_RUTA_PRODUCCION, d.NUM_ESTRUCTURA_RUTA_PROCESO }, commandType: SP);
    }

    public async Task RutaTiempoPropioAsync(RutaFilaClaveDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "RUTAS_OPERACION_TIEMPO_PROPIO", new { d.COD_RUTA_PRODUCCION, d.NUM_ESTRUCTURA_RUTA_PROCESO, d.VAL_SAM, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task RutaOperacionEditarAsync(RutaOperacionEditarDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "RUTAS_OPERACION_EDITAR", new { d.COD_RUTA_PRODUCCION, d.NUM_ESTRUCTURA_RUTA_PROCESO, d.VAL_FRECUENCIA, d.FLG_PARALELO, d.COD_CATEGORIA_OCUPACIONAL, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<bool> RutaMoverAsync(RutaMoverDto d)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "RUTAS_ELEMENTO_MOVER", new { d.COD_RUTA_PRODUCCION, d.NUM_ESTRUCTURA_RUTA_PROCESO, d.DIRECCION }, commandType: SP) == 1;
    }

    public async Task<int> RutaRecalcularAsync(RutaRecalcularDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "RUTAS_RECALCULAR_SAM", new { d.COD_RUTA_PRODUCCION, d.FECHA, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task<List<SamPorCategoria>> RutaSamPorCategoriaAsync(string ruta)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<SamPorCategoria>(P + "RUTAS_SAM_POR_CATEGORIA", new { COD_RUTA_PRODUCCION = ruta }, commandType: SP)).ToList();
    }

    public async Task<List<SamPorClase>> RutaSamPorClaseAsync(string ruta)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<SamPorClase>(P + "RUTAS_SAM_POR_CLASE", new { COD_RUTA_PRODUCCION = ruta }, commandType: SP)).ToList();
    }

    public async Task<List<RutaPrecedencia>> PrecedenciasAsync(string ruta)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<RutaPrecedencia>(P + "RUTAS_PRECEDENCIAS_SHOWBYRUTA", new { COD_RUTA_PRODUCCION = ruta }, commandType: SP)).ToList();
    }

    public async Task<int> PrecedenciasGenerarAsync(RutaPrecedenciasGenerarDto d, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "RUTAS_PRECEDENCIAS_GENERAR", new { d.COD_RUTA_PRODUCCION, d.REEMPLAZAR, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }

    public async Task PrecedenciasGuardarAsync(RutaPrecedenciasGuardarDto d, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(d.Precedencias.Select(x => new { x.NUM_ESTRUCTURA_ANTECESORA, x.NUM_ESTRUCTURA_SUCESORA }));
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "RUTAS_PRECEDENCIAS_GUARDAR", new { d.COD_RUTA_PRODUCCION, JSON = json, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion }, commandType: SP);
    }
}
