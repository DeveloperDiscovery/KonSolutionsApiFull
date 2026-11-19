using System.Globalization;
using PdfSharp.Drawing;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Reportes;

// Diseño libre, fase 3b: lo que se imprime dentro de una sección con datos de otro lado.
//
// Subinforme (objeto SUB): otro diseño libre, con su propia fuente, que sale dentro del objeto cada vez que se imprime la
// sección: en el detalle, una vez por fila; en un encabezado o pie de grupo, una vez por grupo; en el encabezado o el pie
// del informe, una vez. Sus parámetros salen de los enlaces del objeto (DES_ENLACES, una línea por parámetro):
//     @NUM_ORDEN={NUM_ORDEN_COMPRA}     una columna de la fila que se imprime
//     @COD_MONEDA={@COD_MONEDA}         un parámetro del reporte
//     @FEC_CORTE={F.FechaCorte}         una fórmula o un campo de otro resultado ({R2.COLUMNA})
//     @COD_ESTADO=APR                   un valor fijo
// Un parámetro que no está en los enlaces sale de la columna de la fila con su mismo nombre; si no, del parámetro del
// reporte con su mismo nombre; si no, de su valor por defecto. Usuario, rol y estación los pone el servicio.
// El motor no toca la base: primero recorre el reporte y junta los pedidos (Pedidos); el servicio ejecuta cada combinación
// de valores distinta una sola vez; al armar, cada subinforme es un motor más que mide sus bandas en el ancho de su objeto.
// Sus bloques van entre los del reporte, con la banda que los tiene (Anfitrion): al repartir en páginas siguen a esa banda
// y se parten entre páginas como cualquier tabla. Un subinforme no tiene páginas propias: su encabezado de página sale debajo
// de su encabezado del informe y se repite arriba cuando el subinforme sigue en otra página (ahí van bien los títulos de
// sus columnas), con los encabezados de sus grupos que piden repetirse. Su pie de página y sus líneas que cruzan secciones
// no salen.
//
// Tabla cruzada (objeto CRU): un resultado resumido en filas (DES_CRUCE_FILAS) por columnas (DES_CRUCE_COLUMNAS), con la
// función (COD_FUNCION) de lo que resume (DES_ORIGEN) y totales por fila y por columna. Del resultado 1 resume las filas
// del grupo de la sección (en un encabezado o pie de grupo) o todas; de otro resultado, todas. Con enlaces, solo las que
// calzan con la fila que se imprime. Las columnas que no entran en el ancho del objeto siguen en otra tabla debajo, con
// las mismas filas.
//
// Tabla enlazada (TAB con enlaces): solo las filas del resultado que calzan con la fila que se imprime, por ejemplo el
// detalle de cada orden en un listado de órdenes:
//     NUM_ORDEN_COMPRA={NUM_ORDEN_COMPRA}
// (a la izquierda la columna de la tabla; a la derecha, como en un subinforme). Una línea con solo el nombre de la columna
// enlaza por la columna del mismo nombre.

internal sealed partial class ReporteBandasMotor
{
    private static readonly XColor Rojo = XColor.FromArgb(0xc6, 0x28, 0x28);
    private readonly Dictionary<string, ReporteFormula?> _enlacesCompilados = new(StringComparer.Ordinal);
    private readonly Dictionary<(ReporteTabla, int), Dictionary<string, List<object?[]>>> _indices = new();
    private XFont? _fuenteAviso;

    /// <summary>Lo que ocupa filas y puede seguir en otra página: parte la banda.</summary>
    private static bool EsTabular(ReporteLienzoObjeto o) => o.COD_TIPO_OBJETO is "TAB" or "CRU" or "SUB";

    /// <summary>Con qué valores hay que ejecutar los subinformes: arma el reporte sin dibujarlo y junta lo que pide cada uno.</summary>
    public List<ReporteSubPedido> Pedidos()
    {
        _pedidos = new List<ReporteSubPedido>();
        Armar();
        return _pedidos;
    }

    // ── Enlaces ─────────────────────────────────────────────────────────────────
    /// <summary>Las líneas de los enlaces: «NOMBRE=expresión» (con o sin @). Una línea con solo un nombre es
    /// «NOMBRE={NOMBRE}»; una vacía se salta.</summary>
    internal static List<(string Nombre, string Expresion)> Enlaces(string? texto)
    {
        var res = new List<(string, string)>();
        if (string.IsNullOrWhiteSpace(texto)) return res;
        foreach (var linea in texto.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n'))
        {
            var i = linea.IndexOf('=');
            var nombre = ReporteTexto.SinArroba(i < 0 ? linea : linea[..i]);
            if (nombre.Length == 0) continue;
            res.Add((nombre, i < 0 ? "{" + nombre + "}" : linea[(i + 1)..].Trim()));
        }
        return res;
    }

    /// <summary>El valor de la expresión de un enlace en la banda que se imprime. Una sola marca da el valor tal cual: {COLUMNA}
    /// de la fila, {@PARAMETRO}, {F.Formula}, {R2.COLUMNA}, {USUARIO}...; un texto con marcas, el texto con los valores
    /// escritos; sin marcas, el texto fijo. Vacío: null.</summary>
    private object? ValorEnlace(string expresion, ReporteBandasInstancia inst)
    {
        var e = expresion.Trim();
        if (e.Length == 0) return null;
        var m = Marca.Match(e);
        if (m.Success && m.Index == 0 && m.Length == e.Length)
        {
            var clave = m.Groups[1].Value.Trim();
            if (ReporteFormulasDatos.EsReferencia(clave))
            {
                if (!_enlacesCompilados.TryGetValue(clave, out var f))
                    _enlacesCompilados[clave] = f = ReporteFormula.Probar("{" + clave + "}", out var c, out _) ? c : null;
                return f is null ? null : Limpio(_f.Evaluar(f, Lugar(inst)));
            }
            if (clave.StartsWith('@'))
                return _ctx.Valores.FirstOrDefault(x => string.Equals(x.Campo, clave[1..], StringComparison.OrdinalIgnoreCase))?.Valor;
            if (_tabla is not null && _tabla.Indice(clave) >= 0) return ValorFila(inst, clave);
            if (MarcaSimple.IsMatch(clave)) return Limpio(Especial(clave, null, inst, 0, 0));
        }
        return Limpio(Reemplazar(e, inst, 0, 0, especiales: true));
    }

    /// <summary>El valor sin formato de una columna del resultado 1 en la fila de la banda.</summary>
    private object? ValorFila(ReporteBandasInstancia inst, string columna)
        => _tabla is not null && inst.Fila >= 0 && inst.Fila < _filas.Count ? Limpio(_tabla.Valor(_filas[inst.Fila], columna)) : null;

    private static object? Limpio(object? v) => v switch
    {
        DBNull => null,
        string s when string.IsNullOrWhiteSpace(s) => null,
        _ => v
    };

    private static object? Celda(object?[] fila, int i) => i >= 0 && i < fila.Length ? Limpio(fila[i]) : null;

    /// <summary>Un valor como texto que ReporteValores.Convertir sabe leer (fecha ISO, punto decimal).</summary>
    private static string? TextoCrudo(object? v) => Limpio(v) switch
    {
        null => null,
        string s => s.Trim(),
        DateTime d => d.ToString(d.TimeOfDay == TimeSpan.Zero ? "yyyy-MM-dd" : "yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture),
        DateTimeOffset o => o.DateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture),
        DateOnly x => x.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        bool b => b ? "1" : "0",
        double x => x.ToString("0.###############", CultureInfo.InvariantCulture),
        float x => x.ToString("0.#######", CultureInfo.InvariantCulture),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        var x => Convert.ToString(x, CultureInfo.InvariantCulture)
    };

    /// <summary>La clave de un valor para comparar y agrupar: los números iguales dan lo mismo aunque sean de tipos distintos
    /// (5, 5.00), los textos sin importar mayúsculas ni espacios a los lados. null: vacío, que no calza con nada.</summary>
    private static string? ClaveValor(object? v) => Limpio(v) switch
    {
        null => null,
        string s => "T" + s.Trim().ToUpperInvariant(),
        DateTime d => "D" + d.Ticks.ToString(CultureInfo.InvariantCulture),
        bool b => b ? "B1" : "B0",
        var x when ReporteFormato.ADecimal(x, out var n) => "N" + n.ToString("0.############################", CultureInfo.InvariantCulture),
        var x => "O" + Convert.ToString(x, CultureInfo.InvariantCulture)?.ToUpperInvariant()
    };

    /// <summary>¿Calzan dos valores? Como en SQL, un vacío no calza con nada. Un número calza con un texto que tiene ese número
    /// (un código guardado como texto en un resultado y como número en otro).</summary>
    private static bool Iguales(object? a, object? b)
    {
        var ka = ClaveValor(a);
        var kb = ClaveValor(b);
        if (ka is null || kb is null) return false;
        if (ka == kb) return true;
        return (Limpio(a) is string) != (Limpio(b) is string)
               && ReporteFormato.ADecimal(a, out var x) && ReporteFormato.ADecimal(b, out var y) && x == y;
    }

    /// <summary>Las filas que calzan con la fila que se imprime según los enlaces («COLUMNA=expresión»). Sin enlaces, todas.
    /// Si el resultado ya no trae una columna de los enlaces, ninguna (mejor nada que filas de otra fila).</summary>
    private List<object?[]> Filtrar(ReporteTabla datos, List<object?[]> filas, string? enlaces, ReporteBandasInstancia inst)
    {
        var lista = Enlaces(enlaces);
        if (lista.Count == 0) return filas;
        var condiciones = new List<(int Indice, object? Valor)>();
        foreach (var (columna, expresion) in lista)
        {
            var i = datos.Indice(columna);
            if (i < 0) return new List<object?[]>();
            condiciones.Add((i, ValorEnlace(expresion, inst)));
        }
        // Con todas las filas del resultado, se busca por la primera columna en un índice (armado una vez por resultado):
        // en un listado de mil órdenes con su detalle, no se recorre el detalle mil veces.
        IEnumerable<object?[]> candidatas = filas;
        var (i0, v0) = condiciones[0];
        var deTexto = datos.Columnas[i0].Tipo == typeof(string);
        if (ReferenceEquals(filas, datos.Filas) && ClaveValor(v0) is { } k0 && (Limpio(v0) is string) == deTexto)
            candidatas = Indice(datos, i0).TryGetValue(k0, out var l) ? l : new List<object?[]>();
        return candidatas.Where(f => condiciones.All(c => Iguales(Celda(f, c.Indice), c.Valor))).ToList();
    }

    private Dictionary<string, List<object?[]>> Indice(ReporteTabla datos, int columna)
    {
        if (_indices.TryGetValue((datos, columna), out var d)) return d;
        d = new Dictionary<string, List<object?[]>>(StringComparer.Ordinal);
        foreach (var f in datos.Filas)
        {
            if (ClaveValor(Celda(f, columna)) is not { } k) continue;
            if (!d.TryGetValue(k, out var l)) d[k] = l = new List<object?[]>();
            l.Add(f);
        }
        return _indices[(datos, columna)] = d;
    }

    // ── Subinformes ─────────────────────────────────────────────────────────────
    /// <summary>Con qué valores se imprime un subinforme en esta banda (y la clave de esa combinación).</summary>
    private ReporteSubPedido PedidoSub(ReporteLienzoObjeto sub, ReporteSubinforme s, ReporteBandasInstancia inst)
    {
        var enlaces = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var (nombre, expresion) in Enlaces(sub.DES_ENLACES)) enlaces[nombre] = expresion;
        var valores = new List<ReporteValorParametro>();
        string? error = null;
        foreach (var p in s.Definicion.Parametros.OrderBy(x => x.NUM_ORDEN))
        {
            // Usuario, rol y estación: los pone el servicio, como en el reporte principal.
            if (p.FLG_SISTEMA)
            {
                valores.Add(new ReporteValorParametro { Parametro = p });
                continue;
            }
            var campo = ReporteTexto.SinArroba(p.DES_PARAMETRO);
            var etiqueta = ReporteColumnas.Vacio(p.DES_ETIQUETA) ?? ReporteTexto.EtiquetaDesdeNombre(p.DES_PARAMETRO);
            object? crudo = null;
            string? visible = null;
            if (enlaces.TryGetValue(campo, out var expresion)) crudo = ValorEnlace(expresion, inst);
            else if (_tabla is not null && _tabla.Indice(campo) >= 0) crudo = ValorFila(inst, campo);
            else if (_ctx.Valores.FirstOrDefault(v => string.Equals(v.Campo, campo, StringComparison.OrdinalIgnoreCase)) is { } delReporte)
            {
                crudo = delReporte.Valor;
                visible = delReporte.Texto;
            }
            var texto = TextoCrudo(crudo) ?? ReporteValores.Defecto(p.COD_VALOR_DEFECTO, p.DES_VALOR_DEFECTO, _ctx.Fecha);
            object? valor = null;
            try { valor = ReporteValores.Convertir(texto, p.DES_TIPO_DATO, etiqueta); }
            catch (FormatException ex) { error ??= ex.Message; }
            var deSistema = (p.COD_VALOR_DEFECTO ?? "").ToUpperInvariant() is "USUARIO" or "ROL" or "ESTACION";
            if (valor is null && p.FLG_OBLIGATORIO && !deSistema) error ??= $"Falta {etiqueta}.";
            valores.Add(new ReporteValorParametro { Parametro = p, Valor = valor, Texto = ReporteValores.TextoVisible(valor, visible, p) });
        }
        var clave = string.Join("\u001f", valores.Where(v => !v.Parametro.FLG_SISTEMA).Select(v => v.Campo + "=" + TextoCrudo(v.Valor)));
        return new ReporteSubPedido { NumDiseno = sub.NUM_DISENO_SUB ?? 0, Clave = clave, Valores = valores, Error = error };
    }

    /// <summary>Un subinforme en la banda: sus bloques, medidos por su propio motor en el ancho del objeto. Si no se pudo
    /// ejecutar, el motivo en rojo en su lugar. Al juntar los pedidos, solo anota con qué valores se ejecuta.</summary>
    private IEnumerable<ReporteBandasBloque> BloquesSub(ReporteLienzoObjeto sub, ReporteBandasInstancia inst, ref bool primero)
    {
        var res = new List<ReporteBandasBloque>();
        // Un subinforme no tiene subinformes adentro (la base no deja grabarlo así).
        if (_esSub) return res;
        _ctx.Subinformes.TryGetValue(sub.NUM_DISENO_SUB ?? 0, out var s);
        var pedido = s is { Error: null } ? PedidoSub(sub, s, inst) : null;
        if (_pedidos is not null)
        {
            if (pedido is not null) _pedidos.Add(pedido);
            return res;
        }

        var nombre = ReporteColumnas.Vacio(s?.Definicion.Cabecera.DES_DISENO) ?? $"diseño {sub.NUM_DISENO_SUB}";
        string? aviso = null;
        ReporteDatos? datos = null;
        if (s is null) aviso = $"El subinforme «{nombre}» no se ejecutó.";
        else if (s.Error is not null) aviso = $"Subinforme «{nombre}»: {s.Error}";
        else if (pedido!.Error is not null) aviso = $"Subinforme «{nombre}»: {pedido.Error}";
        else if (s.Errores.TryGetValue(pedido.Clave, out var err)) aviso = $"Subinforme «{nombre}»: {err}";
        else if (!s.Datos.TryGetValue(pedido.Clave, out datos) || s.Definicion.Lienzo is null) aviso = $"El subinforme «{nombre}» no se ejecutó.";
        if (aviso is not null)
        {
            if (sub.FLG_SUPRIMIR_VACIO && pedido?.Error is not null) return res;
            res.Add(new ReporteBandasBloque
            {
                Inst = inst, Tipo = ReporteBloqueTipo.Aviso, Primero = primero, Aviso = aviso, Objeto = sub,
                Alto = AltoFila(new List<string> { aviso }, new List<double> { Math.Max(10, (double)sub.NUM_ANCHO) }, FuenteAviso)
            });
            primero = false;
            return res;
        }

        var r1 = datos!.Tabla(1) ?? datos.Tablas.FirstOrDefault();
        if (sub.FLG_SUPRIMIR_VACIO && (r1 is null || r1.Filas.Count == 0)) return res;
        var subCtx = new ReporteContexto
        {
            Definicion = s!.Definicion, Datos = datos, Valores = s.Valores.GetValueOrDefault(pedido!.Clave) ?? pedido.Valores,
            Empresa = _ctx.Empresa, Usuario = _ctx.Usuario, Fecha = _ctx.Fecha
        };
        var motor = new ReporteBandasMotor(subCtx, s.Definicion.Lienzo!, this, sub);
        var bloques = motor.Armar();
        if (bloques.Count == 0) return res;
        // La banda del reporte empieza en un bloque suyo: si el subinforme es lo primero, una marca sin alto.
        if (primero)
        {
            res.Add(new ReporteBandasBloque { Inst = inst, Tipo = ReporteBloqueTipo.Estatico, Primero = true, Alto = 0 });
            primero = false;
        }
        foreach (var b in bloques)
        {
            b.Motor = motor;
            b.Anfitrion = inst;
        }
        res.AddRange(bloques);
        return res;
    }

    /// <summary>Lo que repite arriba de la página un subinforme que sigue de la anterior: su encabezado de página (si lo que
    /// viene no es justo su principio) y los encabezados de sus grupos abiertos que piden repetirse.</summary>
    internal List<ReporteBandasBloque> Continuacion(ReporteBandasBloque proximo)
    {
        var res = new List<ReporteBandasBloque>();
        var inst = proximo.Inst;
        if (inst.Banda.COD_TIPO_BANDA is "RH" or "PH") return res;
        foreach (var b in Bandas("PH"))
            res.AddRange(Partir(new ReporteBandasInstancia { Banda = b, Fila = Math.Max(inst.Fila, -1) }, true));
        var tope = inst.Banda.COD_TIPO_BANDA == "GH" && proximo.Primero ? inst.Banda.NUM_NIVEL - 1 : inst.Grupos.Length;
        for (var n = 0; n < tope && n < inst.Grupos.Length && n < _grupos.Count; n++)
        {
            var g = inst.Grupos[n];
            if (g is null || !_grupos[n].FLG_REPETIR_ENCABEZADO || !_encabezados.TryGetValue(g, out var gh) || gh == inst) continue;
            res.AddRange(Partir(gh, true));
        }
        return res;
    }

    private XFont FuenteAviso => _fuenteAviso ??= new XFont(ReporteFuentesPdf.Familia, 7, XFontStyleEx.Italic);

    /// <summary>El motivo por el que no salió un subinforme, en rojo, en el ancho de su objeto.</summary>
    private void DibujarAviso(XGraphics g, ReporteBandasBloque b, double x0, double y)
    {
        if (b.Objeto is not { } o || string.IsNullOrEmpty(b.Aviso)) return;
        var x = x0 + (double)o.NUM_X;
        var w = Math.Max(10, (double)o.NUM_ANCHO);
        var f = FuenteAviso;
        var lh = f.GetHeight();
        var brocha = new XSolidBrush(Rojo);
        var yl = (y + Relleno) * MmPt;
        foreach (var linea in Partir(b.Aviso, f, (w - 2 * Relleno) * MmPt))
        {
            g.DrawString(linea, f, brocha, new XRect((x + Relleno) * MmPt, yl, (w - 2 * Relleno) * MmPt, lh), XStringFormats.TopLeft);
            yl += lh;
        }
    }

    // ── Resúmenes ───────────────────────────────────────────────────────────────
    /// <summary>Una función de resumen sobre unos valores (sin vacíos): el valor sin formato, el formato con que se escribe y
    /// su tipo. SUM, AVG, CNT y CND dan un número; MAX y MIN, el mayor o el menor. Valor null: no hay qué mostrar.</summary>
    private static (object? Valor, string? Formato, Type Tipo) Agregado(string funcion, List<object> valores, ReporteColumnaVista? vista, string? propio)
    {
        var tipo = vista?.Tipo ?? typeof(string);
        switch (funcion)
        {
            case "CNT":
                return ((decimal)valores.Count, propio ?? "#,##0", typeof(decimal));
            case "CND":
                return ((decimal)valores.Select(v => Convert.ToString(v, CultureInfo.InvariantCulture)).Distinct().Count(), propio ?? "#,##0", typeof(decimal));
            case "SUM":
            case "AVG":
            {
                decimal s = 0; var n = 0;
                foreach (var v in valores) if (ReporteFormato.ADecimal(v, out var d)) { s += d; n++; }
                var formato = propio ?? vista?.Formato ?? (ReporteFormato.EsEntero(tipo) && funcion == "SUM" ? "#,##0" : "#,##0.00");
                if (ReporteFormato.EsLetras(formato)) formato = "#,##0.00";
                if (funcion == "AVG") return (n == 0 ? null : s / n, formato, typeof(decimal));
                return (s, formato, typeof(decimal));
            }
            case "MAX":
            case "MIN":
            {
                if (valores.Count == 0) return (null, null, tipo);
                var orden = valores.OrderBy(v => v, ComparadorValores.Instancia).ToList();
                return (funcion == "MAX" ? orden[^1] : orden[0], propio ?? vista?.Formato, tipo);
            }
            default:
                return (null, null, tipo);
        }
    }

    /// <summary>Qué filas resume una tabla cruzada o un gráfico: del resultado 1, las del grupo de la sección (en un
    /// encabezado o pie de grupo) o todas; de otro resultado, todas. Con enlaces, solo las que calzan con la fila que se
    /// imprime.</summary>
    private List<object?[]> FilasResumen(ReporteTabla datos, string? enlaces, ReporteBandasInstancia inst)
    {
        List<object?[]> filas;
        if (ReferenceEquals(datos, _tabla))
        {
            var nivel = inst.Banda.COD_TIPO_BANDA is "GH" or "GF" ? inst.Banda.NUM_NIVEL : 0;
            filas = nivel > 0 && nivel <= inst.Grupos.Length && inst.Grupos[nivel - 1] is { } g
                ? _filas.GetRange(g.Desde, Math.Max(0, Math.Min(g.Hasta, _filas.Count) - g.Desde))
                : _filas;
        }
        else filas = datos.Filas;
        return Filtrar(datos, filas, enlaces, inst);
    }

    // ── Tabla cruzada ───────────────────────────────────────────────────────────
    private IEnumerable<ReporteBandasBloque> BloquesCruzada(ReporteLienzoObjeto cru, ReporteBandasInstancia inst, ref bool primero)
    {
        var res = new List<ReporteBandasBloque>();
        var r = cru.NUM_RESULTADO ?? 1;
        var datos = _tabla is not null && r == _tabla.Numero ? _tabla : _ctx.Datos.Tabla(r);
        var colFilas = ReporteColumnas.Vacio(cru.DES_CRUCE_FILAS);
        var colColumnas = ReporteColumnas.Vacio(cru.DES_CRUCE_COLUMNAS);
        var colValor = ReporteColumnas.Vacio(cru.DES_ORIGEN);
        var funcion = (cru.COD_FUNCION ?? "SUM").Trim().ToUpperInvariant();
        if (datos is null || colFilas is null || colColumnas is null) return res;
        var iF = datos.Indice(colFilas);
        var iC = datos.Indice(colColumnas);
        var iV = colValor is null ? -1 : datos.Indice(colValor);
        if (iF < 0 || iC < 0 || (iV < 0 && funcion != "CNT")) return res;

        var filas = FilasResumen(datos, cru.DES_ENLACES, inst);
        if (filas.Count == 0 && cru.FLG_SUPRIMIR_VACIO) return res;

        var vF = Vista(r, colFilas);
        var vC = Vista(r, colColumnas);
        var vV = colValor is null ? null : Vista(r, colValor);
        var propio = ReporteColumnas.Vacio(cru.DES_FORMATO);

        // Los valores distintos de las filas y de las columnas, y lo que cae en cada celda, en cada fila, en cada columna y en
        // total (la fila y la columna de totales resumen los valores, no las celdas: un promedio es el de todos).
        var deFilas = new Dictionary<string, object?>(StringComparer.Ordinal);
        var deColumnas = new Dictionary<string, object?>(StringComparer.Ordinal);
        var enCelda = new Dictionary<(string, string), List<object>>();
        var enFila = new Dictionary<string, List<object>>(StringComparer.Ordinal);
        var enColumna = new Dictionary<string, List<object>>(StringComparer.Ordinal);
        var todos = new List<object>();
        foreach (var f in filas)
        {
            var vf = Celda(f, iF);
            var vc = Celda(f, iC);
            var kf = ClaveValor(vf) ?? "";
            var kc = ClaveValor(vc) ?? "";
            deFilas.TryAdd(kf, vf);
            deColumnas.TryAdd(kc, vc);
            if (!enCelda.TryGetValue((kf, kc), out var lc)) enCelda[(kf, kc)] = lc = new List<object>();
            if (!enFila.TryGetValue(kf, out var lf)) enFila[kf] = lf = new List<object>();
            if (!enColumna.TryGetValue(kc, out var lk)) enColumna[kc] = lk = new List<object>();
            var v = iV < 0 ? 1m : Celda(f, iV);
            if (v is null) continue;
            lc.Add(v);
            lf.Add(v);
            lk.Add(v);
            todos.Add(v);
        }
        // Ordenadas por su valor, los vacíos al final.
        var ordenFilas = deFilas.OrderBy(x => x.Value, ReporteListado.Comparador.Instancia).ToList();
        var ordenColumnas = deColumnas.OrderBy(x => x.Value, ReporteListado.Comparador.Instancia).ToList();
        var nC = ordenColumnas.Count;

        (object? Valor, string? Formato, Type Tipo) Calcular(List<object>? l)
            => l is null ? (null, null, typeof(string)) : Agregado(funcion, l, vV, propio);
        static string Escribir((object? Valor, string? Formato, Type Tipo) a)
            => a.Valor is null ? "" : ReporteFormato.Texto(a.Valor, a.Formato, a.Tipo);
        static string Rotulo(object? v, ReporteColumnaVista? vista)
        {
            var t = v is null ? "" : ReporteFormato.Texto(v, vista?.Formato, vista?.Tipo ?? v.GetType());
            return t.Length == 0 ? "(sin dato)" : t;
        }

        // Cada fila: una celda por columna y el total de la fila al final; una celda sin filas queda en blanco.
        var matriz = ordenFilas.Select(kf =>
        {
            var fila = new (object? Valor, string? Formato, Type Tipo)[nC + 1];
            for (var j = 0; j < nC; j++) fila[j] = Calcular(enCelda.GetValueOrDefault((kf.Key, ordenColumnas[j].Key)));
            fila[nC] = Calcular(enFila[kf.Key]);
            return fila;
        }).ToList();
        var total = new (object? Valor, string? Formato, Type Tipo)[nC + 1];
        for (var j = 0; j < nC; j++) total[j] = Calcular(enColumna[ordenColumnas[j].Key]);
        total[nC] = Calcular(todos);

        // El formato de Excel de cada columna (todas sus celdas se resumen igual).
        string? FormatoExcel(int j)
        {
            foreach (var a in matriz.Select(m => m[j]).Append(total[j]))
                if (a.Valor is not null)
                    return a.Valor is string or bool ? null : ReporteFormato.Excel(a.Formato, new ReporteColumnaVista { Tipo = a.Tipo, Formato = a.Formato }, a.Valor);
            return null;
        }

        var fuente = Fuente(cru, null);
        var negrita = Fuente(cru, true);
        double Mide(string s, XFont f) => s.Replace("\r\n", "\n").Split('\n').Max(x => _medir.MeasureString(x, f).Width) / MmPt;
        var ancho = Math.Max(10, (double)cru.NUM_ANCHO);
        var etiquetaFilas = vF?.Etiqueta ?? ReporteTexto.EtiquetaDesdeNombre(colFilas);
        // La fila y la columna de los totales dicen qué resumen: el total, o el máximo, el mínimo o el promedio de la fila.
        var nombreTotal = funcion switch { "MAX" => "Máximo", "MIN" => "Mínimo", "AVG" => "Promedio", _ => "Total" };
        var rotulos = ordenFilas.Select(x => Rotulo(x.Value, vF)).ToList();
        var titulos = ordenColumnas.Select(x => Rotulo(x.Value, vC)).Append(nombreTotal).ToList();
        var textos = matriz.Select(m => m.Select(Escribir).ToList()).ToList();
        var textoTotal = total.Select(Escribir).ToList();
        // La columna de las filas, lo que pida su texto más largo (entre 18 mm y casi la mitad del ancho; lo más largo se
        // parte en dos renglones). Las de los valores, todas iguales: lo que pida el número más largo (y el título, hasta
        // 30 mm), al menos 12 mm.
        var anchoFilas = Math.Clamp(rotulos.Select(t => Mide(t, fuente)).Append(Mide(etiquetaFilas, negrita)).Append(Mide(nombreTotal, negrita)).Max()
                                    + 2 * Relleno + 0.5, 18, Math.Max(18, ancho * 0.45));
        var maxValor = textos.SelectMany(x => x).Select(t => Mide(t, fuente)).Concat(textoTotal.Select(t => Mide(t, negrita))).DefaultIfEmpty(0).Max();
        var maxTitulo = titulos.Select(t => Mide(t, negrita)).DefaultIfEmpty(0).Max();
        var anchoValor = Math.Max(12, Math.Max(maxValor, Math.Min(maxTitulo, 30)) + 2 * Relleno + 0.5);
        anchoValor = Math.Min(anchoValor, Math.Max(12, ancho - anchoFilas));
        var caben = Math.Max(1, (int)Math.Floor((ancho - anchoFilas + 0.001) / anchoValor));
        // Si no entran, se reparten parejo entre las tablas (que la última no quede solo con el total).
        var tramos = (nC + 1 + caben - 1) / caben;
        var porTramo = (nC + 1 + tramos - 1) / tramos;
        var alineacion = vV is not null && funcion is "MAX" or "MIN" && !ReporteFormato.EsNumerico(vV.Tipo) ? vV.Alineacion : 'D';
        var columnaFilas = new ReporteColumnaVista { Nombre = colFilas, Etiqueta = etiquetaFilas, Alineacion = 'I' };

        // Las columnas que no entran siguen en otra tabla debajo, con las mismas filas; el total de la fila va en la última.
        for (var desde = 0; desde <= nC; desde += porTramo)
        {
            var hasta = Math.Min(nC + 1, desde + porTramo);
            var n = hasta - desde;
            if (desde > 0) res.Add(new ReporteBandasBloque { Inst = inst, Tipo = ReporteBloqueTipo.Estatico, Alto = 2.5 });
            var columnas = new List<ReporteColumnaVista> { columnaFilas };
            var anchos = new List<double> { anchoFilas };
            var formatos = new List<string?> { null };
            for (var j = desde; j < hasta; j++)
            {
                columnas.Add(new ReporteColumnaVista { Nombre = "#" + j.ToString(CultureInfo.InvariantCulture), Etiqueta = titulos[j], Alineacion = alineacion, Tipo = typeof(decimal) });
                anchos.Add(anchoValor);
                formatos.Add(FormatoExcel(j));
            }
            var t = new ReporteBandasTabla
            {
                Objeto = cru, Columnas = columnas, Anchos = anchos, Fuente = fuente, Negrita = negrita, ConTitulo = true,
                Celdas = textos.Select((c, i) => c.Skip(desde).Take(n).Prepend(rotulos[i]).ToList()).ToList(),
                FilaTotal = textoTotal.Skip(desde).Take(n).Prepend(nombreTotal).ToList(),
                Crudos = matriz.Select(m => m.Skip(desde).Take(n).Select(a => a.Valor).Prepend(null).ToArray()).ToList(),
                CrudosTotal = total.Skip(desde).Take(n).Select(a => a.Valor).Prepend(null).ToArray(),
                FormatosExcel = formatos.ToArray()
            };
            var titulo = new ReporteBandasBloque
            {
                Inst = inst, Tipo = ReporteBloqueTipo.TituloTabla, Primero = primero, Tabla = t,
                Alto = AltoFila(columnas.Select(c => c.Etiqueta).ToList(), anchos, negrita)
            };
            t.Titulo = titulo;
            res.Add(titulo);
            primero = false;
            for (var i = 0; i < t.Celdas.Count; i++)
                res.Add(new ReporteBandasBloque { Inst = inst, Tipo = ReporteBloqueTipo.FilaTabla, Tabla = t, FilaTabla = i, Alto = AltoFila(t.Celdas[i], anchos, fuente) });
            if (t.Celdas.Count > 0)
                res.Add(new ReporteBandasBloque { Inst = inst, Tipo = ReporteBloqueTipo.TotalTabla, Tabla = t, Alto = AltoFila(t.FilaTotal, anchos, negrita) });
        }
        return res;
    }
}
