using System.DirectoryServices.Protocols;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace KonSolutionsApi.Application.Services;

/// <summary>
/// Valida credenciales contra un Directorio Activo (LDAP) mediante un bind simple.
/// Se usa solo para los usuarios con FLG_VALIDA_DIRECTORIO_ACTIVO = true; el resto
/// sigue autenticando contra COD_PASSWORD (BCrypt) en SEGURIDAD.USUARIOS.
/// </summary>
public class ActiveDirectoryService
{
    private readonly string  _server;
    private readonly int     _port;
    private readonly string  _domain;
    private readonly bool    _useSsl;
    private readonly bool    _configurado;

    public ActiveDirectoryService(IConfiguration config)
    {
        var section  = config.GetSection("ActiveDirectory");
        _server = section["Server"] ?? string.Empty;
        _port   = section.GetValue<int?>("Port") ?? 389;
        _domain = section["Domain"] ?? string.Empty;
        _useSsl = section.GetValue<bool?>("UseSsl") ?? false;
        _configurado = !string.IsNullOrWhiteSpace(_server);
    }

    /// <summary>
    /// Intenta autenticar codUsuario/password contra el AD mediante un bind LDAP.
    /// Devuelve (true, null) si el bind fue exitoso, o (false, mensaje) en caso contrario.
    /// </summary>
    public (bool ok, string? error) Validar(string codUsuario, string password)
    {
        if (!_configurado)
            return (false, "El Directorio Activo no está configurado (ActiveDirectory:Server vacío en appsettings.json).");

        try
        {
            var identificador = new LdapDirectoryIdentifier(_server, _port);
            using var conn = new LdapConnection(identificador)
            {
                AuthType = AuthType.Basic,
                Timeout = TimeSpan.FromSeconds(8)
            };
            conn.SessionOptions.ProtocolVersion = 3;
            if (_useSsl) conn.SessionOptions.SecureSocketLayer = true;

            var userPrincipal = string.IsNullOrWhiteSpace(_domain) ? codUsuario : $"{codUsuario}@{_domain}";
            conn.Credential = new NetworkCredential(userPrincipal, password);
            conn.Bind();   // lanza LdapException si las credenciales son inválidas
            return (true, null);
        }
        catch (LdapException)
        {
            return (false, "Credenciales de Directorio Activo inválidas.");
        }
        catch (Exception ex)
        {
            return (false, $"No se pudo contactar al Directorio Activo: {ex.Message}");
        }
    }
}
