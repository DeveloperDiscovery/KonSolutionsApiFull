using System.Security.Cryptography;
using System.Text;
using KonSolutionsApi.Application.Interfaces;

namespace KonSolutionsApi.Application.Services;

/// <summary>AES-256-GCM. Cada cifrado lleva su propio nonce de 12 bytes y su etiqueta de 16, y el
/// contexto entra como dato asociado: si alguien altera el cifrado, o lo mueve de columna, el
/// descifrado falla en vez de devolver basura.
///
/// Formato: [versión 1 byte][nonce 12][etiqueta 16][texto cifrado]. La versión deja lugar para
/// cambiar de algoritmo o de clave sin dejar ilegible lo ya guardado.</summary>
public sealed class CifradoSecretos : ICifradoSecretos
{
    public const string VariableDeEntorno = "KonSolutions_EMISION_CLAVE";
    private const byte Version = 1;
    private const int LargoNonce = 12;
    private const int LargoEtiqueta = 16;

    private readonly byte[]? _clave;

    /// <summary>La clave sale de la variable de entorno: 32 bytes en base64.</summary>
    public CifradoSecretos() : this(Environment.GetEnvironmentVariable(VariableDeEntorno)) { }

    /// <summary>Para las pruebas: la clave en base64, sin pasar por el entorno.</summary>
    public CifradoSecretos(string? claveBase64)
    {
        if (string.IsNullOrWhiteSpace(claveBase64))
        {
            MensajeNoDisponible = $"Falta configurar la variable de entorno {VariableDeEntorno} (32 bytes en base64). Sin ella no se pueden guardar ni leer las credenciales de emisión.";
            return;
        }
        try
        {
            var bytes = Convert.FromBase64String(claveBase64.Trim());
            if (bytes.Length != 32)
            {
                MensajeNoDisponible = $"La variable de entorno {VariableDeEntorno} tiene {bytes.Length} bytes: tienen que ser 32 (AES-256).";
                return;
            }
            _clave = bytes;
        }
        catch (FormatException)
        {
            MensajeNoDisponible = $"La variable de entorno {VariableDeEntorno} no es un base64 válido.";
        }
    }

    public bool Disponible => _clave is not null;
    public string MensajeNoDisponible { get; } = string.Empty;

    public byte[] CifrarTexto(string texto, string contexto) => CifrarBytes(Encoding.UTF8.GetBytes(texto), contexto);
    public string DescifrarTexto(byte[] cifrado, string contexto) => Encoding.UTF8.GetString(DescifrarBytes(cifrado, contexto));

    public byte[] CifrarBytes(byte[] datos, string contexto)
    {
        var clave = ClaveOError();
        var resultado = new byte[1 + LargoNonce + LargoEtiqueta + datos.Length];
        resultado[0] = Version;
        var nonce = resultado.AsSpan(1, LargoNonce);
        var etiqueta = resultado.AsSpan(1 + LargoNonce, LargoEtiqueta);
        var cifrado = resultado.AsSpan(1 + LargoNonce + LargoEtiqueta);
        RandomNumberGenerator.Fill(nonce);
        using var aes = new AesGcm(clave, LargoEtiqueta);
        aes.Encrypt(nonce, datos, cifrado, etiqueta, Encoding.UTF8.GetBytes(contexto));
        return resultado;
    }

    public byte[] DescifrarBytes(byte[] cifrado, string contexto)
    {
        var clave = ClaveOError();
        if (cifrado is null || cifrado.Length < 1 + LargoNonce + LargoEtiqueta || cifrado[0] != Version)
            throw new CryptographicException("El dato cifrado no tiene el formato esperado.");
        var nonce = cifrado.AsSpan(1, LargoNonce);
        var etiqueta = cifrado.AsSpan(1 + LargoNonce, LargoEtiqueta);
        var texto = cifrado.AsSpan(1 + LargoNonce + LargoEtiqueta);
        var plano = new byte[texto.Length];
        using var aes = new AesGcm(clave, LargoEtiqueta);
        // Si la clave cambió, el dato se alteró o es de otra columna, acá lanza CryptographicException.
        aes.Decrypt(nonce, texto, etiqueta, plano, Encoding.UTF8.GetBytes(contexto));
        return plano;
    }

    private byte[] ClaveOError() => _clave ?? throw new InvalidOperationException(MensajeNoDisponible);
}
