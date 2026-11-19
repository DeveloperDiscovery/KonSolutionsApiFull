using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Xml.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Application.Reportes;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Reportes;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Reporteador: los SP de configuración (fuentes, diseños, botones) y la ejecución del SP de la fuente.
///
/// Los hijos (parámetros, columnas, elementos...) viajan como XML y no como JSON: OPENJSON depende del nivel de
/// compatibilidad de la base (mismo criterio que el resto de la API).
///
/// El SP de la fuente se ejecuta con un comando genérico (IDbCommand) y no con Dapper: no se conocen sus columnas
/// de antemano y puede devolver varios resultados. Su nombre sale SIEMPRE de la definición guardada en la base
/// (PROC_REPORTES_DEFINICION), nunca de lo que mande el navegador.</summary>
public class ReportesRepository : IReportesRepository
{
    private readonly IDbConnectionFactory _db;
    public ReportesRepository(IDbConnectionFactory db) => _db = db;

    private static string? Vacio(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
    private static XAttribute? At(string nombre, object? valor) => valor switch
    {
        null => null,
        string s when string.IsNullOrWhiteSpace(s) => null,
        bool b => new XAttribute(nombre, b ? "1" : "0"),
        IFormattable f => new XAttribute(nombre, f.ToString(null, CultureInfo.InvariantCulture)),
        _ => new XAttribute(nombre, valor.ToString()!.Trim())
    };
    private static string Xml(XElement e) => e.ToString(SaveOptions.DisableFormatting);

    // ── Catálogo ────────────────────────────────────────────────────────────────
    public async Task<List<ReporteProcedimiento>> ProcedimientosAsync(string? buscar)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteProcedimiento>(SP.SCHEMAReportes.Catalogo.Procedimientos,
            new { DES_BUSCAR = Vacio(buscar) }, commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task<List<ReporteParametroSp>> ParametrosSpAsync(string esquema, string procedimiento)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteParametroSp>(SP.SCHEMAReportes.Catalogo.ParametrosSp,
            new { DES_ESQUEMA = esquema, DES_PROCEDIMIENTO = procedimiento }, commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task<List<ReporteEtiqueta>> EtiquetasAsync(IEnumerable<string> columnas)
    {
        var xml = new XElement("cs", columnas.Where(c => !string.IsNullOrWhiteSpace(c)).Distinct(StringComparer.OrdinalIgnoreCase)
                                             .Select(c => new XElement("c", new XAttribute("n", c.Trim()))));
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("XML_COLUMNAS", Xml(xml), DbType.Xml);
        return (await conn.QueryAsync<ReporteEtiqueta>(SP.SCHEMAReportes.Catalogo.Etiquetas, p, commandType: CommandType.StoredProcedure)).ToList();
    }

    // ── Fuentes ─────────────────────────────────────────────────────────────────
    public async Task<List<ReporteFuente>> FuentesAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteFuente>(SP.SCHEMAReportes.Fuentes.Showall, commandType: CommandType.StoredProcedure)).ToList();
    }

    /// <summary>Cuatro resultados, siempre en este orden: fuente, parámetros, resultados y columnas.</summary>
    public async Task<ReporteFuenteDetalle?> FuenteAsync(int numFuente)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync(SP.SCHEMAReportes.Fuentes.Showbyid, new { NUM_FUENTE = numFuente }, commandType: CommandType.StoredProcedure);
        var f = (await m.ReadAsync<ReporteFuente>()).FirstOrDefault();
        if (f is null) return null;
        return new ReporteFuenteDetalle
        {
            Fuente     = f,
            Parametros = (await m.ReadAsync<ReporteFuenteParametro>()).ToList(),
            Resultados = (await m.ReadAsync<ReporteFuenteResultado>()).ToList(),
            Columnas   = (await m.ReadAsync<ReporteFuenteColumna>()).ToList()
        };
    }

    public async Task<int> FuenteGrabarAsync(ReporteFuenteDetalle d, string codUsuario, string codEstacion)
    {
        var f = d.Fuente;
        var xp = new XElement("ps", d.Parametros.Select(x => new XElement("p",
            At("n", x.DES_PARAMETRO), At("o", x.NUM_ORDEN), At("et", x.DES_ETIQUETA), At("c", x.COD_CONTROL),
            At("cp", x.DES_COMBO_PROCEDIMIENTO), At("ci", x.DES_COMBO_COLUMNA_ID), At("ct", x.DES_COMBO_COLUMNA_TEXTO),
            At("vd", x.COD_VALOR_DEFECTO), At("vv", x.DES_VALOR_DEFECTO), At("ob", x.FLG_OBLIGATORIO), At("si", x.FLG_SISTEMA),
            At("vi", x.FLG_VISIBLE))));
        var xr = new XElement("rs", d.Resultados.Select(x => new XElement("r", At("n", x.NUM_RESULTADO), At("d", x.DES_RESULTADO), At("p", x.COD_PAPEL))));
        var xc = new XElement("cs", d.Columnas.Select(x => new XElement("c",
            At("r", x.NUM_RESULTADO), At("n", x.DES_COLUMNA), At("o", x.NUM_ORDEN), At("t", x.DES_TIPO_DATO), At("et", x.DES_ETIQUETA),
            At("f", x.DES_FORMATO), At("a", x.COD_ALINEACION))));

        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FUENTE", f.NUM_FUENTE);
        p.Add("DES_FUENTE", Vacio(f.DES_FUENTE));
        p.Add("DES_ESQUEMA", Vacio(f.DES_ESQUEMA) ?? "dbo");
        p.Add("DES_PROCEDIMIENTO", Vacio(f.DES_PROCEDIMIENTO));
        p.Add("COD_APLICACION", Vacio(f.COD_APLICACION));
        p.Add("NUM_TIEMPO_MAXIMO", f.NUM_TIEMPO_MAXIMO);
        p.Add("DES_OBSERVACION", Vacio(f.DES_OBSERVACION));
        p.Add("FLG_HABILITADO", f.FLG_HABILITADO);
        p.Add("XML_PARAMETROS", Xml(xp), DbType.Xml);
        p.Add("XML_RESULTADOS", Xml(xr), DbType.Xml);
        p.Add("XML_COLUMNAS", Xml(xc), DbType.Xml);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_FUENTE_SALIDA", dbType: DbType.Int32, direction: ParameterDirection.Output);
        await conn.ExecuteAsync(SP.SCHEMAReportes.Fuentes.Grabar, p, commandType: CommandType.StoredProcedure);
        return p.Get<int?>("NUM_FUENTE_SALIDA") ?? 0;
    }

    public async Task FuenteEliminarAsync(int numFuente)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMAReportes.Fuentes.Delete, new { NUM_FUENTE = numFuente }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Agrega a la fuente las columnas que todavía no tiene (las demás no se tocan). Devuelve cuántas agregó.</summary>
    public async Task<int> FuenteColumnasAgregarAsync(int numFuente, IEnumerable<ReporteFuenteColumna> columnas)
    {
        var xc = new XElement("cs", columnas.Select(x => new XElement("c",
            At("r", x.NUM_RESULTADO), At("n", x.DES_COLUMNA), At("o", x.NUM_ORDEN), At("t", x.DES_TIPO_DATO), At("et", x.DES_ETIQUETA),
            At("f", x.DES_FORMATO), At("a", x.COD_ALINEACION))));
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FUENTE", numFuente);
        p.Add("XML_COLUMNAS", Xml(xc), DbType.Xml);
        return await conn.ExecuteScalarAsync<int?>(SP.SCHEMAReportes.Fuentes.ColumnasAgregar, p, commandType: CommandType.StoredProcedure) ?? 0;
    }

    // ── Diseños ─────────────────────────────────────────────────────────────────
    public async Task<List<ReporteDiseno>> DisenosAsync(int? numFuente)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteDiseno>(SP.SCHEMAReportes.Disenos.Showall, new { NUM_FUENTE = numFuente }, commandType: CommandType.StoredProcedure)).ToList();
    }

    /// <summary>Cuatro resultados: diseño, secciones, elementos y formatos.</summary>
    public async Task<ReporteDisenoDetalle?> DisenoAsync(int numDiseno)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync(SP.SCHEMAReportes.Disenos.Showbyid, new { NUM_DISENO = numDiseno }, commandType: CommandType.StoredProcedure);
        var d = (await m.ReadAsync<ReporteDiseno>()).FirstOrDefault();
        if (d is null) return null;
        var r = new ReporteDisenoDetalle
        {
            Diseno    = d,
            Secciones = (await m.ReadAsync<ReporteDisenoSeccion>()).ToList(),
            Elementos = (await m.ReadAsync<ReporteDisenoElemento>()).ToList(),
            Formatos  = (await m.ReadAsync<ReporteDisenoPlantilla>()).ToList()
        };
        if (d.COD_TIPO_DISENO == "BAN") r.Lienzo = await LienzoAsync(numDiseno);
        return r;
    }

    /// <summary>Cinco resultados: grupos, bandas, objetos, fórmulas y condiciones del diseño libre.</summary>
    public async Task<ReporteLienzo> LienzoAsync(int numDiseno)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync(SP.SCHEMAReportes.Disenos.LienzoShowbyid, new { NUM_DISENO = numDiseno }, commandType: CommandType.StoredProcedure);
        var l = new ReporteLienzo
        {
            Grupos  = (await m.ReadAsync<ReporteLienzoGrupo>()).ToList(),
            Bandas  = (await m.ReadAsync<ReporteLienzoBanda>()).ToList(),
            Objetos = (await m.ReadAsync<ReporteLienzoObjeto>()).ToList()
        };
        // Las fórmulas y las condiciones vienen del SP de la fase 2; con el SP anterior, el lienzo sale sin ellas.
        if (!m.IsConsumed) l.Formulas = (await m.ReadAsync<ReporteLienzoFormula>()).ToList();
        if (!m.IsConsumed) l.Condiciones = (await m.ReadAsync<ReporteLienzoCondicion>()).ToList();
        return l;
    }

    /// <summary>Reemplaza el lienzo completo. La imagen propia de un objeto viaja en base64 dentro del XML.</summary>
    public async Task LienzoGrabarAsync(int numDiseno, ReporteLienzo l, string codUsuario, string codEstacion)
    {
        var xg = new XElement("gs", l.Grupos.Select(g => new XElement("g",
            At("n", g.NUM_NIVEL), At("c", g.DES_COLUMNA), At("o", g.COD_ORDEN), At("mj", g.FLG_MANTENER_JUNTO),
            At("re", g.FLG_REPETIR_ENCABEZADO), At("np", g.FLG_NUEVA_PAGINA))));
        var xb = new XElement("bs", l.Bandas.Select(b => new XElement("b",
            At("id", b.NUM_BANDA), At("t", b.COD_TIPO_BANDA), At("n", b.NUM_NIVEL), At("s", b.NUM_SUBSECCION), At("al", b.NUM_ALTO),
            At("su", b.FLG_SUPRIMIR), At("sb", b.FLG_SUPRIMIR_EN_BLANCO), At("pa", b.FLG_NUEVA_PAGINA_ANTES),
            At("pd", b.FLG_NUEVA_PAGINA_DESPUES), At("mj", b.FLG_MANTENER_JUNTO), At("cf", b.DES_COLOR_FONDO), At("cz", b.DES_COLOR_CEBRA))));
        var xo = new XElement("os", l.Objetos.Select(o => new XElement("o",
            At("id", o.NUM_OBJETO), At("b", o.NUM_BANDA), At("t", o.COD_TIPO_OBJETO), At("x", o.NUM_X), At("y", o.NUM_Y),
            At("an", o.NUM_ANCHO), At("al", o.NUM_ALTO), At("z", o.NUM_ORDEN), At("r", o.NUM_RESULTADO), At("or", o.DES_ORIGEN),
            At("fn", o.COD_FUNCION), At("nv", o.NUM_NIVEL), At("f", o.DES_FORMATO), At("le", o.DES_FUENTE), At("ta", o.NUM_TAMANO_LETRA),
            At("ne", o.FLG_NEGRITA), At("cu", o.FLG_CURSIVA), At("sr", o.FLG_SUBRAYADO), At("co", o.DES_COLOR), At("cf", o.DES_COLOR_FONDO),
            At("a", o.COD_ALINEACION), At("bo", o.DES_BORDES), At("gb", o.NUM_GROSOR_BORDE), At("cb", o.DES_COLOR_BORDE),
            At("pc", o.FLG_PUEDE_CRECER), At("sv", o.FLG_SUPRIMIR_VACIO), At("sp", o.FLG_SUPRIMIR_REPETIDO),
            At("img", o.IMG_IMAGEN is { Length: > 0 } img ? Convert.ToBase64String(img) : null),
            At("bf", o.NUM_BANDA_FIN), At("yf", o.NUM_Y_FIN), At("gi", o.NUM_GIRO), At("ha", o.FLG_HASTA_ABAJO), At("cc", o.COD_CODIGO),
            At("tc", o.FLG_TEXTO_CODIGO), At("ds", o.NUM_DISENO_SUB), At("en", o.DES_ENLACES), At("xf", o.DES_CRUCE_FILAS),
            At("xc", o.DES_CRUCE_COLUMNAS), At("gr", o.COD_GRAFICO), At("ap", o.FLG_APILADO), At("va", o.FLG_VALORES),
            At("mx", o.NUM_MAXIMO_CATEGORIAS), At("ov", o.FLG_ORDEN_VALOR))));
        var xf = new XElement("fs", l.Formulas.Select(f => new XElement("f",
            At("n", f.DES_NOMBRE), At("x", f.DES_FORMULA), At("d", f.DES_DESCRIPCION))));
        var xc = new XElement("cs", l.Condiciones.Select(c => new XElement("c",
            At("e", c.COD_ELEMENTO), At("id", c.NUM_ELEMENTO), At("p", c.COD_PROPIEDAD), At("x", c.DES_FORMULA))));

        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DISENO", numDiseno);
        p.Add("XML_GRUPOS", Xml(xg), DbType.Xml);
        p.Add("XML_BANDAS", Xml(xb), DbType.Xml);
        p.Add("XML_OBJETOS", Xml(xo), DbType.Xml);
        p.Add("XML_FORMULAS", Xml(xf), DbType.Xml);
        p.Add("XML_CONDICIONES", Xml(xc), DbType.Xml);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        await conn.ExecuteAsync(SP.SCHEMAReportes.Disenos.LienzoGrabar, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> DisenoGrabarAsync(ReporteDisenoDetalle d, string codUsuario, string codEstacion)
    {
        var x = d.Diseno;
        var xs = new XElement("ss", d.Secciones.Select(s => new XElement("s",
            At("r", s.NUM_RESULTADO), At("p", s.COD_PRESENTACION), At("t", s.DES_TITULO), At("o", s.NUM_ORDEN), At("c", s.NUM_COLUMNAS))));
        var xe = new XElement("es", d.Elementos.Select(e => new XElement("e",
            At("r", e.NUM_RESULTADO), At("n", e.DES_COLUMNA), At("v", e.FLG_VISIBLE), At("o", e.NUM_ORDEN), At("et", e.DES_ETIQUETA),
            At("an", e.NUM_ANCHO), At("a", e.COD_ALINEACION), At("f", e.DES_FORMATO), At("to", e.FLG_TOTALIZA), At("g", e.NUM_NIVEL_GRUPO),
            At("od", e.COD_ORDEN), At("po", e.NUM_PRIORIDAD_ORDEN))));
        var xf = new XElement("fs", d.Formatos.Select(f => new XElement("f", At("c", f.COD_FORMATO), At("h", f.FLG_HABILITADO), At("cf", f.FLG_CON_FORMATO))));

        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DISENO", x.NUM_DISENO);
        p.Add("NUM_FUENTE", x.NUM_FUENTE);
        p.Add("DES_DISENO", Vacio(x.DES_DISENO));
        p.Add("COD_TIPO_DISENO", x.COD_TIPO_DISENO);
        p.Add("DES_TITULO", Vacio(x.DES_TITULO));
        p.Add("DES_SUBTITULO", Vacio(x.DES_SUBTITULO));
        p.Add("COD_ORIENTACION", x.COD_ORIENTACION);
        p.Add("COD_PAPEL", x.COD_PAPEL);
        p.Add("NUM_TAMANO_LETRA", (byte)Math.Clamp(x.NUM_TAMANO_LETRA, 6, 14));
        p.Add("FLG_LOGO", x.FLG_LOGO);
        p.Add("FLG_DATOS_EMPRESA", x.FLG_DATOS_EMPRESA);
        p.Add("FLG_FILTROS_PIE", x.FLG_FILTROS_PIE);
        p.Add("FLG_SUBTOTAL_GRUPO", x.FLG_SUBTOTAL_GRUPO);
        p.Add("FLG_SALTO_GRUPO", x.FLG_SALTO_GRUPO);
        p.Add("FLG_TOTAL_GENERAL", x.FLG_TOTAL_GENERAL);
        p.Add("DES_FIRMAS", Vacio(x.DES_FIRMAS));
        p.Add("DES_PIE", Vacio(x.DES_PIE));
        p.Add("DES_SEPARADOR_CSV", Vacio(x.DES_SEPARADOR_CSV));
        p.Add("FLG_HABILITADO", x.FLG_HABILITADO);
        p.Add("XML_SECCIONES", Xml(xs), DbType.Xml);
        p.Add("XML_ELEMENTOS", Xml(xe), DbType.Xml);
        p.Add("XML_FORMATOS", Xml(xf), DbType.Xml);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_DISENO_SALIDA", dbType: DbType.Int32, direction: ParameterDirection.Output);
        await conn.ExecuteAsync(SP.SCHEMAReportes.Disenos.Grabar, p, commandType: CommandType.StoredProcedure);
        return p.Get<int?>("NUM_DISENO_SALIDA") ?? 0;
    }

    public async Task DisenoEliminarAsync(int numDiseno)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMAReportes.Disenos.Delete, new { NUM_DISENO = numDiseno }, commandType: CommandType.StoredProcedure);
    }

    public async Task PlantillaGrabarAsync(int numDiseno, string formato, byte[]? contenido, string? nombre, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DISENO", numDiseno);
        p.Add("COD_FORMATO", formato);
        p.Add("IMG_PLANTILLA", contenido, DbType.Binary, size: -1);
        p.Add("DES_NOMBRE_ARCHIVO", Vacio(nombre));
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        await conn.ExecuteAsync(SP.SCHEMAReportes.Disenos.PlantillaGrabar, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<(byte[]? Contenido, string? Nombre)> PlantillaAsync(int numDiseno, string formato)
    {
        using var conn = _db.CreateConnection();
        var fila = (await conn.QueryAsync(SP.SCHEMAReportes.Disenos.PlantillaShow, new { NUM_DISENO = numDiseno, COD_FORMATO = formato },
            commandType: CommandType.StoredProcedure)).FirstOrDefault();
        if (fila is null) return (null, null);
        var d = (IDictionary<string, object?>)fila;
        return (d["IMG_PLANTILLA"] as byte[], d["DES_NOMBRE_ARCHIVO"] as string);
    }

    // ── Botones ─────────────────────────────────────────────────────────────────
    public async Task<List<ReporteBoton>> BotonesAsync(string? codAplicacion, string? buscar, bool soloAsignados)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteBoton>(SP.SCHEMAReportes.Botones.Showall,
            new { COD_APLICACION = Vacio(codAplicacion), DES_BUSCAR = Vacio(buscar), FLG_SOLO_ASIGNADOS = soloAsignados },
            commandType: CommandType.StoredProcedure)).ToList();
    }

    /// <summary>Dos resultados: el botón con su asignación y los parámetros del diseño con su origen.</summary>
    public async Task<ReporteBotonDetalle?> BotonAsync(string codAplicacion, string codOpcion, string codBoton, int? numDiseno)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync(SP.SCHEMAReportes.Botones.Showbyid,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcion, COD_BOTON_OPCION = codBoton, NUM_DISENO = numDiseno },
            commandType: CommandType.StoredProcedure);
        var b = (await m.ReadAsync<ReporteBoton>()).FirstOrDefault();
        if (b is null) return null;
        return new ReporteBotonDetalle { Boton = b, Parametros = (await m.ReadAsync<ReporteBotonParametro>()).ToList() };
    }

    public async Task BotonGrabarAsync(ReporteBotonGrabarDto dto, string codUsuario, string codEstacion)
    {
        var xml = new XElement("ps", dto.Parametros.Where(x => !x.FLG_SISTEMA).Select(x => new XElement("p",
            At("n", x.DES_PARAMETRO), At("o", x.COD_ORIGEN), At("v", x.DES_VALOR))));
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION.Trim());
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION.Trim());
        p.Add("COD_BOTON_OPCION", dto.COD_BOTON_OPCION.Trim());
        p.Add("NUM_DISENO", dto.NUM_DISENO);
        p.Add("COD_FORMATO_DEFECTO", dto.COD_FORMATO_DEFECTO);
        p.Add("FLG_ELEGIR_FORMATO", dto.FLG_ELEGIR_FORMATO);
        p.Add("FLG_VISTA_PREVIA", dto.FLG_VISTA_PREVIA);
        p.Add("XML_PARAMETROS", Xml(xml), DbType.Xml);
        p.Add("COD_KEY_PATHFILE", Vacio(dto.COD_KEY_PATHFILE));
        p.Add("DES_NOMBRE_ARCHIVO", Vacio(dto.DES_NOMBRE_ARCHIVO));
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        await conn.ExecuteAsync(SP.SCHEMAReportes.Botones.Grabar, p, commandType: CommandType.StoredProcedure);
    }

    public async Task BotonEliminarAsync(string codAplicacion, string codOpcion, string codBoton)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMAReportes.Botones.Delete,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcion, COD_BOTON_OPCION = codBoton },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Las rutas activas de ENTORNO.PATHFILES, para la copia del archivo de un botón.</summary>
    public async Task<List<ReporteRuta>> RutasAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteRuta>(SP.SCHEMAReportes.Botones.Rutas, commandType: CommandType.StoredProcedure)).ToList();
    }

    // ── Ejecución ───────────────────────────────────────────────────────────────
    public async Task<List<ReporteOpcionBoton>> OpcionBotonesAsync(string codAplicacion, string codOpcion, string codRol)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteOpcionBoton>(SP.SCHEMAReportes.Ejecucion.OpcionBotones,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcion, COD_ROL_USUARIO = codRol },
            commandType: CommandType.StoredProcedure)).ToList();
    }

    /// <summary>Siete resultados, siempre en este orden: cabecera, parámetros, resultados, columnas, secciones,
    /// elementos y formatos. Por botón, el SP valida que el usuario sea del rol y que el rol tenga el botón.</summary>
    public async Task<ReporteDefinicion> DefinicionAsync(int? numDiseno, string? codAplicacion, string? codOpcion, string? codBoton, string? codRol, string? codUsuario)
    {
        using var conn = _db.CreateConnection();
        using var m = await conn.QueryMultipleAsync(SP.SCHEMAReportes.Ejecucion.Definicion,
            new
            {
                NUM_DISENO = numDiseno, COD_APLICACION = Vacio(codAplicacion), COD_OPCION_APLICACION = Vacio(codOpcion),
                COD_BOTON_OPCION = Vacio(codBoton), COD_ROL_USUARIO = Vacio(codRol), COD_USUARIO = Vacio(codUsuario)
            },
            commandType: CommandType.StoredProcedure);
        var def = new ReporteDefinicion
        {
            Cabecera   = (await m.ReadAsync<ReporteDefinicionCabecera>()).FirstOrDefault() ?? throw new InvalidOperationException("El diseño no existe."),
            Parametros = (await m.ReadAsync<ReporteFuenteParametro>()).ToList(),
            Resultados = (await m.ReadAsync<ReporteFuenteResultado>()).ToList(),
            Columnas   = (await m.ReadAsync<ReporteFuenteColumna>()).ToList(),
            Secciones  = (await m.ReadAsync<ReporteDisenoSeccion>()).ToList(),
            Elementos  = (await m.ReadAsync<ReporteDisenoElemento>()).ToList(),
            Formatos   = (await m.ReadAsync<ReporteDisenoPlantilla>()).ToList()
        };
        // La definición ya validó el permiso: el lienzo del diseño libre se lee aparte.
        if (def.Cabecera.COD_TIPO_DISENO == "BAN" && def.Cabecera.NUM_DISENO is { } n) def.Lienzo = await LienzoAsync(n);
        return def;
    }

    public async Task<ReporteEmpresa?> EmpresaAsync(string codUsuario)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<ReporteEmpresa>(SP.SCHEMAReportes.Ejecucion.Empresa, new { COD_USUARIO = codUsuario },
            commandType: CommandType.StoredProcedure)).FirstOrDefault();
    }

    /// <summary>Ejecuta el SP de la fuente y lee TODOS sus resultados. Los parámetros sin valor no se mandan: así
    /// el SP usa su valor por defecto (NULL = todos, en los de reporte). Si pasa de maxFilas, corta con un mensaje
    /// para el usuario en vez de llenar la memoria del servidor.</summary>
    public async Task<ReporteDatos> EjecutarAsync(string esquema, string procedimiento, IEnumerable<ReporteParametroEjecucion> parametros, int segundos, int maxFilas)
    {
        using var conn = _db.CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = $"{Cita(esquema)}.{Cita(procedimiento)}";
        cmd.CommandTimeout = segundos;
        foreach (var p in parametros) cmd.Parameters.Add(Parametro(cmd, p));

        var datos = new ReporteDatos();
        var total = 0;
        using var rd = cmd is DbCommand dc ? await dc.ExecuteReaderAsync() : cmd.ExecuteReader();
        var numero = 0;
        do
        {
            if (rd.FieldCount == 0) continue;
            var t = new ReporteTabla { Numero = ++numero };
            for (var i = 0; i < rd.FieldCount; i++)
            {
                var nombre = rd.GetName(i);
                t.Columnas.Add(new ReporteColumnaDato
                {
                    Nombre  = string.IsNullOrWhiteSpace(nombre) ? $"COLUMNA_{i + 1}" : nombre,
                    Tipo    = rd.GetFieldType(i),
                    TipoSql = rd.GetDataTypeName(i)
                });
            }
            while (rd is DbDataReader ar ? await ar.ReadAsync() : rd.Read())
            {
                if (++total > maxFilas)
                    throw new ReporteLimiteException($"El reporte trae más de {maxFilas:N0} filas: acota los filtros (por ejemplo, un rango de fechas más corto).");
                var fila = new object?[rd.FieldCount];
                rd.GetValues(fila!);
                for (var i = 0; i < fila.Length; i++) if (fila[i] is DBNull) fila[i] = null;
                t.Filas.Add(fila);
            }
            datos.Tablas.Add(t);
        }
        while (rd is DbDataReader ar2 ? await ar2.NextResultAsync() : rd.NextResult());
        return datos;
    }

    /// <summary>Los ítems del combo de un parámetro. El SP del combo recibe, por nombre, los valores que ya tiene el
    /// diálogo (el combo de tipos de operación recibe @COD_CLASE_OPERACION, por ejemplo). Sin columnas configuradas,
    /// la primera es el código y la segunda el texto.</summary>
    public async Task<List<ComboboxItem>> ComboAsync(string procedimiento, IReadOnlyDictionary<string, object?> valores, string? columnaId, string? columnaTexto)
    {
        var partes = procedimiento.Split('.', 2, StringSplitOptions.TrimEntries);
        var (esquema, nombre) = partes.Length == 2 ? (partes[0].Trim('[', ']'), partes[1].Trim('[', ']')) : ("dbo", partes[0].Trim('[', ']'));
        var declarados = await ParametrosSpAsync(esquema, nombre);

        using var conn = _db.CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = $"{Cita(esquema)}.{Cita(nombre)}";
        cmd.CommandTimeout = 30;
        foreach (var d in declarados.Where(x => !x.FLG_SALIDA))
        {
            if (!valores.TryGetValue(ReporteTexto.SinArroba(d.DES_PARAMETRO), out var v) || v is null) continue;
            cmd.Parameters.Add(Parametro(cmd, new ReporteParametroEjecucion(d.DES_PARAMETRO, d.DES_TIPO_DATO, v, d.NUM_LONGITUD, d.NUM_PRECISION, d.NUM_ESCALA)));
        }
        var lista = new List<ComboboxItem>();
        using var rd = cmd is DbCommand dc ? await dc.ExecuteReaderAsync() : cmd.ExecuteReader();
        if (rd.FieldCount == 0) return lista;
        int Pos(string? col, int defecto)
        {
            if (string.IsNullOrWhiteSpace(col)) return defecto;
            for (var i = 0; i < rd.FieldCount; i++)
                if (string.Equals(rd.GetName(i), col.Trim(), StringComparison.OrdinalIgnoreCase)) return i;
            throw new InvalidOperationException($"El combo no devuelve la columna {col}.");
        }
        var iId = Pos(columnaId, 0);
        var iTexto = Pos(columnaTexto, rd.FieldCount > 1 ? 1 : 0);
        while (rd is DbDataReader ar ? await ar.ReadAsync() : rd.Read())
        {
            var id = rd.IsDBNull(iId) ? "" : Convert.ToString(rd.GetValue(iId), CultureInfo.InvariantCulture) ?? "";
            var texto = rd.IsDBNull(iTexto) ? "" : Convert.ToString(rd.GetValue(iTexto), CultureInfo.InvariantCulture) ?? "";
            lista.Add(new ComboboxItem { Id = id.Trim(), Texto = texto.Trim() });
            if (lista.Count >= 5000) break;
        }
        return lista;
    }

    private static string Cita(string nombre) => "[" + nombre.Replace("]", "]]") + "]";

    private static IDbDataParameter Parametro(IDbCommand cmd, ReporteParametroEjecucion p)
    {
        var dp = cmd.CreateParameter();
        dp.ParameterName = p.Nombre.StartsWith('@') ? p.Nombre : "@" + p.Nombre;
        dp.Direction = ParameterDirection.Input;
        var tipo = (p.TipoSql ?? "").Trim().ToLowerInvariant();
        dp.DbType = tipo switch
        {
            "date"                   => DbType.Date,
            "datetime"               => DbType.DateTime,
            "smalldatetime"          => DbType.DateTime,
            "datetime2"              => DbType.DateTime2,
            "datetimeoffset"         => DbType.DateTimeOffset,
            "int"                    => DbType.Int32,
            "smallint"               => DbType.Int16,
            "tinyint"                => DbType.Byte,
            "bigint"                 => DbType.Int64,
            "decimal" or "numeric"   => DbType.Decimal,
            "money" or "smallmoney"  => DbType.Currency,
            "float"                  => DbType.Double,
            "real"                   => DbType.Single,
            "bit"                    => DbType.Boolean,
            "uniqueidentifier"       => DbType.Guid,
            "varchar" or "char" or "text" => DbType.AnsiString,
            "xml"                    => DbType.Xml,
            _                        => DbType.String
        };
        if (dp.DbType is DbType.Decimal)
        {
            if (p.Precision is > 0) dp.Precision = (byte)p.Precision.Value;
            if (p.Escala is >= 0) dp.Scale = (byte)p.Escala.Value;
        }
        if (dp.DbType is DbType.String or DbType.AnsiString)
            dp.Size = p.Longitud is > 0 ? p.Longitud.Value : -1;
        dp.Value = p.Valor;
        return dp;
    }
}

