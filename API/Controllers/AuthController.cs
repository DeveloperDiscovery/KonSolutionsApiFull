using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using KonSolutionsApi.Common;
using KonSolutionsApi.API.Filters;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.API.Controllers;

[Route("api/v1/auth")]
// Exento explícito: no se puede exigir un permiso para poder autenticarse, y los permisos
// se resuelven a partir del rol que este mismo controlador emite en el token.
[SinControlPermisos("Autenticación: es el que emite el token del que salen los permisos.")]
public class AuthController : BaseController
{
    private readonly IDbConnectionFactory _db;
    private readonly IConfiguration _config;
    private readonly ErrorLogService _log;
    private readonly KonSolutionsApi.Application.Services.ActiveDirectoryService _ad;

    public AuthController(IDbConnectionFactory db, IConfiguration config, ErrorLogService log,
                           KonSolutionsApi.Application.Services.ActiveDirectoryService ad)
    { _db = db; _config = config; _log = log; _ad = ad; }

    public class LoginRequest
    {
        public string COD_USUARIO  { get; set; } = string.Empty;
        public string COD_PASSWORD { get; set; } = string.Empty;
    }

    public class UsuarioSesion
    {
        public bool    FLG_ACCESO { get; set; }
        public string  COD_USUARIO { get; set; } = string.Empty;
        public string? DES_USUARIO { get; set; }
        public string? COD_PERFIL_USUARIO { get; set; }
        public string? DES_PERFIL_USUARIO { get; set; }
        public string? COD_ROL_USUARIO { get; set; }
        public string? DES_ROL_USUARIO { get; set; }
        public string? DES_CORREO_INSTITUCIONAL { get; set; }
        public string? NUM_TELEFONO { get; set; }
        // Hash de la contraseña (o texto plano heredado, ver migración). NUNCA se serializa
        // en la respuesta JSON — se usa solo internamente para Verify() y luego se descarta.
        [System.Text.Json.Serialization.JsonIgnore]
        public string? COD_PASSWORD { get; set; }
        public bool    FLG_VALIDA_DIRECTORIO_ACTIVO { get; set; }
        public bool    FLG_CHANGE_PASSSWORD { get; set; }   // 1 = debe cambiar password (NULL o vencido)
        public bool?   FLG_LOGIN_CREDENCIALES { get; set; }
        public bool?   FLG_LOGIN_FACE { get; set; }
        public bool?   FLG_REGISTRO_FACIAL { get; set; }
        public bool?   FLG_LOGIN_HUELLA_DACTILAR { get; set; }
        public bool    FLG_SEGURIDAD { get; set; }
        public bool    FLG_AUDITORIA { get; set; }
        public bool    FLG_ADMINISTRADOR { get; set; }
        public bool    FLG_DEVELOPER { get; set; }
        public string? COD_ESTADO { get; set; }
        public string? DES_ESTADO { get; set; }
        public string? DES_MENSAJE { get; set; }
        /// <summary>Entidad (ESTANDAR.ENTIDADES) vinculada a este usuario y su entidad de
        /// dependencia, si aplica — nuevas columnas de PROC_SEGURIDAD_USUARIOS_VALIDALOGIN.
        /// Dapper las enlaza automáticamente por nombre de columna, sin cambios en el query.</summary>
        public decimal? COD_ENTIDAD { get; set; }
        public decimal? COD_ENTIDAD_DEPENDENCIA { get; set; }
        public string? DES_ENTIDAD_DEPENDENCIA { get; set; }
        public byte[]? IMG_LOGO_DEPENDENCIA { get; set; }
    }

    /// <summary>Login: ejecuta PROC_SEGURIDAD_USUARIOS_VALIDALOGIN y emite JWT.
    /// Máximo 5 intentos por IP por minuto (ver política "login" en Program.cs) — antes no
    /// había ningún límite real (el middleware que debía imponerlo quedó vacío).</summary>
    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var usuario = await conn.QueryFirstOrDefaultAsync<UsuarioSesion>(
                SP.SCHEMASeguridad.Usuarios.Validalogin,
                new { req.COD_USUARIO, req.COD_PASSWORD },
                commandType: CommandType.StoredProcedure);

            // Garantizar que el código de usuario siempre esté presente en la respuesta
            if (usuario is not null && string.IsNullOrWhiteSpace(usuario.COD_USUARIO))
                usuario.COD_USUARIO = req.COD_USUARIO;

            // El SP ya no compara la contraseña (eso quedó del lado de SQL Server antes
            // de esta migración a BCrypt). FLG_ACCESO ahora solo confirma que el usuario
            // existe; la validación real de la contraseña se hace aquí.
            //
            // Si el usuario tiene FLG_VALIDA_DIRECTORIO_ACTIVO = true, la contraseña NO
            // se valida contra COD_PASSWORD (BCrypt) sino contra el Directorio Activo
            // (bind LDAP) configurado en appsettings.json → ActiveDirectory. En ese caso
            // no aplica el flujo de "definir contraseña"/"cambio de contraseña vencida":
            // la contraseña la administra el AD, no KonSolutions.
            bool passwordValida;
            if (usuario is not null && usuario.FLG_VALIDA_DIRECTORIO_ACTIVO)
            {
                var (adOk, adError) = _ad.Validar(usuario.COD_USUARIO, req.COD_PASSWORD);
                passwordValida = adOk;
                if (!adOk && usuario.FLG_ACCESO)
                    return Unauthorized(ApiResponse<object>.Fail(adError ?? "Credenciales inválidas."));
            }
            else
            {
                // Caso especial: si el usuario es nuevo y nunca definió contraseña
                // (COD_PASSWORD es NULL), se le permite "entrar" sin verificar nada — el
                // frontend lo recibe con FLG_CHANGE_PASSSWORD=true y debe forzarlo a
                // definir una contraseña antes de continuar. Si COD_PASSWORD NO es null,
                // sí se exige que coincida, incluso si está vencida.
                passwordValida = usuario is not null
                    && (string.IsNullOrEmpty(usuario.COD_PASSWORD) || PasswordHasher.Verify(req.COD_PASSWORD, usuario.COD_PASSWORD));
            }

            // Mensaje genérico para usuario null o credenciales inválidas
            // No diferencia "usuario no existe" de "password incorrecto" — evita enumeración
            if (usuario is null || !usuario.FLG_ACCESO || !passwordValida)
            {
                var msg = (usuario is not null && usuario.FLG_ACCESO) ? usuario.DES_MENSAJE : null;
                return Unauthorized(ApiResponse<object>.Fail(msg ?? "Credenciales inválidas."));
            }

            // Migración transparente: si la contraseña guardada NO es null pero tampoco
            // tiene formato BCrypt (usuario creado antes de esta migración, en texto
            // plano), se fuerza el cambio de contraseña en este login. El password en
            // texto plano NUNCA se reescribe automáticamente. No aplica a usuarios
            // validados por Directorio Activo (ellos no usan COD_PASSWORD local).
            if (usuario.FLG_VALIDA_DIRECTORIO_ACTIVO)
                usuario.FLG_CHANGE_PASSSWORD = false;
            else if (!string.IsNullOrEmpty(usuario.COD_PASSWORD) && !PasswordHasher.EsHashValido(usuario.COD_PASSWORD))
                usuario.FLG_CHANGE_PASSSWORD = true;

            usuario.COD_PASSWORD = null;   // limpiar: por si algo lo serializara más adelante

            var jwt = _config.GetSection("JwtSettings");
            // Misma prioridad que Program.cs (variable de entorno KonSolutions_JWT_SECRET >
            // appsettings.json): esta lectura vivía separada de la de Program.cs y leía
            // directo de config, así que con appsettings.json vacío (como debe estar) armaba
            // una clave de largo cero y el login reventaba con IDX10703.
            var secretKeyRaw = Environment.GetEnvironmentVariable("KonSolutions_JWT_SECRET");
            if (string.IsNullOrWhiteSpace(secretKeyRaw)) secretKeyRaw = jwt["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyRaw!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var minutes = jwt.GetValue<int>("ExpirationMinutes");

            var claims = new List<Claim>
            {
                new("nameid", usuario.COD_USUARIO),
                new(ClaimTypes.Name, usuario.DES_USUARIO ?? usuario.COD_USUARIO),
                new("perfil", usuario.COD_PERFIL_USUARIO ?? string.Empty),
                new("rol", (usuario.COD_ROL_USUARIO ?? string.Empty).Trim()),
                // Flags del PERFIL (no del rol) — usados por políticas de autorización
                // que deben depender del perfil del usuario, como el acceso a
                // herramientas administrativas (Log de Errores, Configuración).
                new("flgSeguridad", usuario.FLG_SEGURIDAD ? "1" : "0"),
                new("flgAdministrador", usuario.FLG_ADMINISTRADOR ? "1" : "0"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"], audience: jwt["Audience"],
                claims: claims, expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds);

            return Ok(ApiResponse<object>.Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenType = "Bearer",
                ExpiresIn = minutes * 60,
                Usuario = usuario
            }));
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "KonSolutionsApi", nameof(AuthController), nameof(Login), $"Usuario={req.COD_USUARIO}");
            return StatusCode(500, ApiResponse<object>.Fail("Error interno del servidor."));
        }
    }
}
