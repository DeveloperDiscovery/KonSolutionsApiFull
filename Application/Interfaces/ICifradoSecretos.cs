namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Cifrado de lo secreto que se guarda en la base: la clave SOL, el client_secret de SUNAT
/// y el certificado digital con su clave.
///
/// La clave de cifrado NO está en la base ni en appsettings.json: vive en la variable de entorno
/// KonSolutions_EMISION_CLAVE, igual que la cadena de conexión y el secreto del JWT. Así, quien
/// obtiene una copia de la base no obtiene las credenciales.
///
/// "contexto" amarra cada texto cifrado a la columna de donde sale: un cifrado copiado de una
/// columna a otra deja de descifrar.</summary>
public interface ICifradoSecretos
{
    /// <summary>False si la variable de entorno falta o no es válida. No tumba la API: sólo
    /// impide guardar y leer secretos, y la pantalla lo avisa.</summary>
    bool Disponible { get; }
    string MensajeNoDisponible { get; }
    byte[] CifrarTexto(string texto, string contexto);
    string DescifrarTexto(byte[] cifrado, string contexto);
    byte[] CifrarBytes(byte[] datos, string contexto);
    byte[] DescifrarBytes(byte[] cifrado, string contexto);
}
