using System.IO.Compression;
using QRCoder;

namespace KonSolutionsApi.Application.Reportes;

// Códigos de barras y QR del diseño libre (objeto BAR):
//   - C128: Code 128. Letras sin tilde, números y signos (ASCII 32 a 126). Usa el juego C (dos números por barra) en los
//     tramos largos de números, así el código queda más corto.
//   - EAN: EAN-13. 12 números (el dígito de control se calcula) o 13 (se comprueba).
//   - QR: con QRCoder (MIT), corrección de errores M (se puede leer con un 15 % dañado).
// Aquí solo se arman los módulos (barra o espacio; en el QR, la cuadrícula). El PDF los dibuja como rectángulos y el Excel
// con formato los pone como imagen PNG (también armada aquí, sin librerías de dibujo).

/// <summary>Un código de barras de una línea: los módulos (true = barra), los que son guardas en un EAN (barras más largas)
/// y el texto que va debajo.</summary>
public sealed class ReporteBarras
{
    public bool[]  Modulos  { get; init; } = Array.Empty<bool>();
    public bool[]  Guardas  { get; init; } = Array.Empty<bool>();
    public string  Texto    { get; init; } = "";
    /// <summary>Módulos de margen en blanco a cada lado (zona de silencio): sin ellos un lector puede no leerlo.</summary>
    public int     MargenIzquierdo { get; init; }
    public int     MargenDerecho   { get; init; }
    public int     Total => MargenIzquierdo + Modulos.Length + MargenDerecho;
}

public static class ReporteCodigos
{
    public static readonly string[] Tipos = { "C128", "EAN", "QR" };

    // Code 128: ancho de barra, espacio, barra... de cada valor (0 a 106); el 106 es el de parada, con su barra final.
    private static readonly string[] Patrones128 =
    {
        "212222", "222122", "222221", "121223", "121322", "131222", "122213", "122312", "132212", "221213",
        "221312", "231212", "112232", "122132", "122231", "113222", "123122", "123221", "223211", "221132",
        "221231", "213212", "223112", "312131", "311222", "321122", "321221", "312212", "322112", "322211",
        "212123", "212321", "232121", "111323", "131123", "131321", "112313", "132113", "132311", "211313",
        "231113", "231311", "112133", "112331", "132131", "113123", "113321", "133121", "313121", "211331",
        "231131", "213113", "213311", "213131", "311123", "311321", "331121", "312113", "312311", "332111",
        "314111", "221411", "431111", "111224", "111422", "121124", "121421", "141122", "141221", "112214",
        "112412", "122114", "122411", "142112", "142211", "241211", "221114", "413111", "241112", "134111",
        "111242", "121142", "121241", "114212", "124112", "124211", "411212", "421112", "421211", "212141",
        "214121", "412121", "111143", "111341", "131141", "114113", "114311", "411113", "411311", "113141",
        "114131", "311141", "411131", "211412", "211214", "211232", "2331112"
    };
    private const int InicioB = 104, InicioC = 105, CambioC = 99, CambioB = 100, Parada = 106;

    // EAN-13: los dígitos de la izquierda van en juego L o G según el primer dígito; los de la derecha, en juego R.
    private static readonly string[] JuegoL = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
    private static readonly string[] JuegoG = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
    private static readonly string[] JuegoR = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
    private static readonly string[] Paridad = { "LLLLLL", "LLGLGG", "LLGGLG", "LLGGGL", "LGLLGG", "LGGLLG", "LGGGLL", "LGLGLG", "LGLGGL", "LGGLGL" };

    /// <summary>Las barras de un código de una línea (C128 o EAN); null con el motivo si el texto no se puede codificar.</summary>
    public static ReporteBarras? Barras(string tipo, string texto, out string? error)
    {
        error = null;
        return tipo switch
        {
            "EAN" => Ean13(texto, out error),
            _     => Code128(texto, out error)
        };
    }

    public static ReporteBarras? Code128(string texto, out string? error)
    {
        error = null;
        if (texto.Length == 0) { error = "Code 128: no hay nada que codificar."; return null; }
        if (texto.Length > 80) { error = "Code 128: hasta 80 caracteres."; return null; }
        if (texto.Any(c => c < 32 || c > 126)) { error = "Code 128: solo letras sin tilde, números y signos."; return null; }

        static int Digitos(string s, int i) { var n = 0; while (i + n < s.Length && char.IsAsciiDigit(s[i + n])) n++; return n; }
        var valores = new List<int>();
        var i = 0;
        // Empieza en C si arranca con 4 números o más (o si todo son números, en pares); si no, en B.
        var enC = Digitos(texto, 0) >= 4 || (Digitos(texto, 0) == texto.Length && texto.Length % 2 == 0);
        valores.Add(enC ? InicioC : InicioB);
        while (i < texto.Length)
        {
            if (enC)
            {
                if (Digitos(texto, i) >= 2) { valores.Add((texto[i] - '0') * 10 + (texto[i + 1] - '0')); i += 2; continue; }
                valores.Add(CambioB);
                enC = false;
                continue;
            }
            // En B: pasa a C con una tirada de números larga (4 al final, 6 en medio), en pares.
            var n = Digitos(texto, i);
            if (n >= 6 || (n >= 4 && i + n == texto.Length))
            {
                if (n % 2 == 1) { valores.Add(texto[i] - 32); i++; }
                valores.Add(CambioC);
                enC = true;
                continue;
            }
            valores.Add(texto[i] - 32);
            i++;
        }
        var suma = valores[0];
        for (var k = 1; k < valores.Count; k++) suma += valores[k] * k;
        valores.Add(suma % 103);
        valores.Add(Parada);

        var modulos = new List<bool>();
        foreach (var v in valores)
        {
            var barra = true;
            foreach (var ancho in Patrones128[v])
            {
                for (var k = 0; k < ancho - '0'; k++) modulos.Add(barra);
                barra = !barra;
            }
        }
        return new ReporteBarras { Modulos = modulos.ToArray(), Guardas = new bool[modulos.Count], Texto = texto, MargenIzquierdo = 10, MargenDerecho = 10 };
    }

    /// <summary>El dígito de control de un EAN-13 (con los 12 primeros).</summary>
    public static int ControlEan(string doce)
    {
        var suma = 0;
        for (var k = 0; k < 12; k++) suma += (doce[k] - '0') * (k % 2 == 0 ? 1 : 3);
        return (10 - suma % 10) % 10;
    }

    public static ReporteBarras? Ean13(string texto, out string? error)
    {
        error = null;
        var t = texto.Replace(" ", "").Replace("-", "");
        if (!(t.Length is 12 or 13) || !t.All(char.IsAsciiDigit)) { error = "EAN-13: 12 o 13 números."; return null; }
        var control = ControlEan(t[..12]);
        if (t.Length == 13 && t[12] - '0' != control) { error = $"EAN-13: el dígito de control no cuadra (debería ser {control})."; return null; }
        if (t.Length == 12) t += control;

        var bits = new System.Text.StringBuilder("101");
        var paridad = Paridad[t[0] - '0'];
        for (var k = 1; k <= 6; k++) bits.Append(paridad[k - 1] == 'L' ? JuegoL[t[k] - '0'] : JuegoG[t[k] - '0']);
        bits.Append("01010");
        for (var k = 7; k <= 12; k++) bits.Append(JuegoR[t[k] - '0']);
        bits.Append("101");
        var modulos = bits.ToString().Select(c => c == '1').ToArray();
        var guardas = new bool[modulos.Length];
        foreach (var k in new[] { 0, 1, 2, 45, 46, 47, 48, 49, 92, 93, 94 }) guardas[k] = true;
        return new ReporteBarras { Modulos = modulos, Guardas = guardas, Texto = t, MargenIzquierdo = 11, MargenDerecho = 7 };
    }

    /// <summary>La cuadrícula del QR (true = módulo oscuro), con su margen de 4 módulos en blanco alrededor.</summary>
    public static bool[,]? Qr(string texto, out string? error)
    {
        error = null;
        if (texto.Length == 0) { error = "QR: no hay nada que codificar."; return null; }
        try
        {
            using var generador = new QRCodeGenerator();
            using var datos = generador.CreateQrCode(texto, QRCodeGenerator.ECCLevel.M);
            var filas = datos.ModuleMatrix;
            var n = filas.Count;
            var m = new bool[n, n];
            for (var y = 0; y < n; y++)
                for (var x = 0; x < n; x++)
                    m[y, x] = filas[y][x];
            return m;
        }
        catch (Exception)
        {
            error = "QR: el texto es demasiado largo.";
            return null;
        }
    }

    // ── Imagen PNG (para el Excel con formato) ──────────────────────────────────
    /// <summary>Un PNG en colores (RGB) de una cuadrícula: true pinta con «tinta», false con «papel».</summary>
    public static byte[] Png(bool[,] puntos, (byte R, byte G, byte B) tinta, (byte R, byte G, byte B) papel)
    {
        var alto = puntos.GetLength(0);
        var ancho = puntos.GetLength(1);
        var crudo = new byte[alto * (1 + ancho * 3)];
        var p = 0;
        for (var y = 0; y < alto; y++)
        {
            crudo[p++] = 0;                                            // sin filtro
            for (var x = 0; x < ancho; x++)
            {
                var c = puntos[y, x] ? tinta : papel;
                crudo[p++] = c.R; crudo[p++] = c.G; crudo[p++] = c.B;
            }
        }
        using var ms = new MemoryStream();
        ms.Write(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });
        var ihdr = new byte[13];
        Escribir32(ihdr, 0, (uint)ancho);
        Escribir32(ihdr, 4, (uint)alto);
        ihdr[8] = 8;                                                    // 8 bits por canal
        ihdr[9] = 2;                                                    // RGB
        Trozo(ms, "IHDR", ihdr);
        using (var z = new MemoryStream())
        {
            using (var zl = new ZLibStream(z, CompressionLevel.Optimal, leaveOpen: true)) zl.Write(crudo);
            Trozo(ms, "IDAT", z.ToArray());
        }
        Trozo(ms, "IEND", Array.Empty<byte>());
        return ms.ToArray();
    }

    /// <summary>La cuadrícula de un código de barras de una línea: cada módulo «escala» puntos de ancho, «alto» de alto.</summary>
    public static bool[,] Puntos(ReporteBarras b, int escala, int alto)
    {
        var ancho = b.Total * escala;
        var m = new bool[alto, ancho];
        for (var k = 0; k < b.Modulos.Length; k++)
        {
            if (!b.Modulos[k]) continue;
            var x0 = (b.MargenIzquierdo + k) * escala;
            for (var y = 0; y < alto; y++)
                for (var x = x0; x < x0 + escala; x++) m[y, x] = true;
        }
        return m;
    }

    /// <summary>La cuadrícula del QR agrandada: cada módulo, «escala» × «escala» puntos.</summary>
    public static bool[,] Puntos(bool[,] qr, int escala)
    {
        var n = qr.GetLength(0);
        var m = new bool[n * escala, n * escala];
        for (var y = 0; y < n * escala; y++)
            for (var x = 0; x < n * escala; x++)
                m[y, x] = qr[y / escala, x / escala];
        return m;
    }

    private static void Escribir32(byte[] b, int i, uint v)
    {
        b[i] = (byte)(v >> 24); b[i + 1] = (byte)(v >> 16); b[i + 2] = (byte)(v >> 8); b[i + 3] = (byte)v;
    }

    private static void Trozo(Stream s, string tipo, byte[] datos)
    {
        var largo = new byte[4];
        Escribir32(largo, 0, (uint)datos.Length);
        s.Write(largo);
        var cabeza = System.Text.Encoding.ASCII.GetBytes(tipo);
        s.Write(cabeza);
        s.Write(datos);
        var crc = new byte[4];
        Escribir32(crc, 0, Crc(cabeza, datos));
        s.Write(crc);
    }

    private static readonly uint[] TablaCrc = Enumerable.Range(0, 256).Select(n =>
    {
        var c = (uint)n;
        for (var k = 0; k < 8; k++) c = (c & 1) != 0 ? 0xEDB88320u ^ (c >> 1) : c >> 1;
        return c;
    }).ToArray();

    private static uint Crc(byte[] a, byte[] b)
    {
        var c = 0xFFFFFFFFu;
        foreach (var x in a) c = TablaCrc[(c ^ x) & 0xFF] ^ (c >> 8);
        foreach (var x in b) c = TablaCrc[(c ^ x) & 0xFF] ^ (c >> 8);
        return c ^ 0xFFFFFFFFu;
    }
}
