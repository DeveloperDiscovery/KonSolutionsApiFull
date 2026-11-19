namespace KonSolutionsApi.Common;

/// <summary>
/// Hash y verificación de contraseñas con BCrypt (work factor 12).
/// Las contraseñas NUNCA se guardan ni se comparan en texto plano:
/// - Al crear/cambiar: se hashea con HashPassword antes de persistir.
/// - Al validar (login o "no repetir la anterior"): se usa Verify, que internamente
///   re-hashea el texto plano recibido con el mismo salt del hash guardado y compara.
///   No existe — ni debe existir — una función para "desencriptar" un hash.
/// </summary>
public static class PasswordHasher
{
    private const int WorkFactor = 12;

    /// <summary>Genera el hash BCrypt de una contraseña en texto plano. Esto es lo que se persiste en BD.</summary>
    public static string Hash(string passwordTextoPlano) =>
        BCrypt.Net.BCrypt.HashPassword(passwordTextoPlano, workFactor: WorkFactor);

    /// <summary>
    /// Compara un texto plano contra un hash guardado. Devuelve true si coinciden.
    /// Si el hash guardado no tiene formato BCrypt válido (p. ej. contraseñas antiguas
    /// en texto plano, previas a esta migración), compara literal como último recurso —
    /// usar junto con EsHashValido para decidir si forzar el cambio de contraseña.
    /// </summary>
    public static bool Verify(string passwordTextoPlano, string? hashGuardado)
    {
        if (string.IsNullOrEmpty(hashGuardado)) return false;

        if (EsHashValido(hashGuardado))
        {
            try { return BCrypt.Net.BCrypt.Verify(passwordTextoPlano, hashGuardado); }
            catch { return false; }
        }

        // Contraseña heredada (texto plano, de antes de la migración a BCrypt).
        // Se compara literal por única vez — el caller debe forzar el cambio de
        // contraseña inmediatamente después si esto retorna true (ver UsuariosSesion.FLG_CHANGE_PASSSWORD).
        return string.Equals(passwordTextoPlano, hashGuardado, StringComparison.Ordinal);
    }

    /// <summary>True si el string tiene el formato de un hash BCrypt ($2a$, $2b$ o $2y$).</summary>
    public static bool EsHashValido(string? valor) =>
        !string.IsNullOrEmpty(valor) &&
        (valor.StartsWith("$2a$") || valor.StartsWith("$2b$") || valor.StartsWith("$2y$"));
}
