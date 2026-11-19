namespace KonSolutionsApi.Domain.Models.Seguridad;

/// <summary>
/// Una muestra facial de un usuario: SEGURIDAD.USUARIOS_BIOMETRIA_FACIAL.
///
/// VAL_DESCRIPTOR son 128 flotantes de 8 bytes (1024 en total) tal como los
/// devuelve face-api.js. No es una foto y no se puede reconstruir una cara a
/// partir de él: es una lista de medidas. Aun así es un dato biométrico, y por eso
/// vive en su propia tabla con su auditoría y nunca sale hacia el navegador.
/// </summary>
public class UsuariosBiometriaFacial
{
    public string COD_USUARIO    { get; set; } = string.Empty;
    public int    NUM_MUESTRA    { get; set; }
    public byte[] VAL_DESCRIPTOR { get; set; } = Array.Empty<byte>();
    public DateTime? FEC_REGISTRO { get; set; }
}

/// <summary>Resumen por usuario: lo que necesita la pantalla de administración y el
/// login para saber si corresponde pedir verificación facial.</summary>
public class UsuariosBiometriaFacialEstado
{
    public string COD_USUARIO    { get; set; } = string.Empty;
    public string DES_USUARIO    { get; set; } = string.Empty;
    public string? DES_ESTADO    { get; set; }
    public bool   FLG_REGISTRADO { get; set; }
    public int    NUM_MUESTRAS   { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
}

/// <summary>
/// Conversión entre el vector que viaja como JSON y los bytes que van a la tabla.
///
/// POR QUE ESTA EN UN SOLO LUGAR
/// Si el alta serializara de una manera y la verificación leyera de otra, todas las
/// distancias saldrían mal y nadie podría entrar — y el síntoma sería "el
/// reconocimiento no funciona", que no lleva a mirar acá.
///
/// LITTLE-ENDIAN EXPLÍCITO, no BitConverter: BitConverter usa el orden de la máquina.
/// Hoy son todas little-endian, pero eso es una propiedad del hardware, no una
/// garantía, y lo que se graba en la tabla queda ahí para siempre.
/// </summary>
public static class DescriptorFacial
{
    /// <summary>128 flotantes es lo que devuelve el modelo de face-api.js. Cualquier
    /// otro largo no es un descriptor: viene de otro modelo o de un dato cortado.</summary>
    public const int Dimensiones = 128;
    public const int Bytes       = Dimensiones * sizeof(double);   // 1024

    public static byte[] ABytes(double[] v)
    {
        if (v is null || v.Length != Dimensiones)
            throw new ArgumentException(
                $"El descriptor facial debe tener {Dimensiones} valores; llegaron {v?.Length ?? 0}.");

        var b = new byte[Bytes];
        for (int i = 0; i < Dimensiones; i++)
            System.Buffers.Binary.BinaryPrimitives.WriteDoubleLittleEndian(
                b.AsSpan(i * sizeof(double)), v[i]);
        return b;
    }

    public static double[] ADoubles(byte[] b)
    {
        if (b is null || b.Length != Bytes)
            throw new ArgumentException(
                $"El descriptor guardado debe tener {Bytes} bytes; tiene {b?.Length ?? 0}.");

        var v = new double[Dimensiones];
        for (int i = 0; i < Dimensiones; i++)
            v[i] = System.Buffers.Binary.BinaryPrimitives.ReadDoubleLittleEndian(
                b.AsSpan(i * sizeof(double)));
        return v;
    }

    /// <summary>Distancia euclidiana. Cuanto más chica, más se parecen.</summary>
    public static double Distancia(double[] a, double[] b)
    {
        double suma = 0;
        int n = Math.Min(a.Length, b.Length);
        for (int i = 0; i < n; i++) { var d = a[i] - b[i]; suma += d * d; }
        return Math.Sqrt(suma);
    }
}
