using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class ConfiguracionEmisionElectronicaService : IConfiguracionEmisionElectronicaService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(ConfiguracionEmisionElectronicaService);

    // El contexto de cada cifrado: la columna de la que sale. Ver ICifradoSecretos.
    private const string CtxClientSecret = "ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA|ENC_CLIENT_SECRET";
    private const string CtxClaveSol = "ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA|ENC_CLAVE_SOL";
    private const string CtxCertificado = "ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA|ENC_CERTIFICADO";
    private const string CtxClaveCertificado = "ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA|ENC_CLAVE_CERTIFICADO";

    private readonly IConfiguracionEmisionElectronicaRepository _repo;
    private readonly ICifradoSecretos _cifrado;
    private readonly ErrorLogService _log;
    private readonly Func<DateTime> _ahora;

    public ConfiguracionEmisionElectronicaService(IConfiguracionEmisionElectronicaRepository repo, ICifradoSecretos cifrado, ErrorLogService log)
        : this(repo, cifrado, log, () => DateTime.Now) { }

    /// <summary>Para las pruebas: con el reloj en la mano.</summary>
    public ConfiguracionEmisionElectronicaService(IConfiguracionEmisionElectronicaRepository repo, ICifradoSecretos cifrado, ErrorLogService log, Func<DateTime> ahora)
    {
        _repo = repo; _cifrado = cifrado; _log = log; _ahora = ahora;
    }

    // ── Lo que ve la pantalla ────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<ConfiguracionEmisionElectronica>> GetAsync()
    {
        try
        {
            var reg = await _repo.ShowbyidAsync() ?? new ConfiguracionEmisionElectronicaRegistro();
            return ApiResponse<ConfiguracionEmisionElectronica>.Ok(Publicar(reg));
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(GetAsync));
            return ApiResponse<ConfiguracionEmisionElectronica>.Fail(ex.Message);
        }
    }

    /// <summary>De la fila con secretos a lo que puede salir: qué hay cargado, no qué dice.</summary>
    private ConfiguracionEmisionElectronica Publicar(ConfiguracionEmisionElectronicaRegistro r) => new()
    {
        COD_AMBIENTE = r.COD_AMBIENTE,
        FLG_ACTIVA = r.FLG_ACTIVA,
        DES_URL_SEGURIDAD = r.DES_URL_SEGURIDAD,
        DES_URL_API = r.DES_URL_API,
        DES_CLIENT_ID = r.DES_CLIENT_ID,
        DES_USUARIO_SOL = r.DES_USUARIO_SOL,
        TIENE_CLIENT_SECRET = r.ENC_CLIENT_SECRET is { Length: > 0 },
        TIENE_CLAVE_SOL = r.ENC_CLAVE_SOL is { Length: > 0 },
        TIENE_CERTIFICADO = r.ENC_CERTIFICADO is { Length: > 0 },
        TIENE_CLAVE_CERTIFICADO = r.ENC_CLAVE_CERTIFICADO is { Length: > 0 },
        NOM_ARCHIVO_CERTIFICADO = r.NOM_ARCHIVO_CERTIFICADO,
        DES_SUJETO_CERTIFICADO = r.DES_SUJETO_CERTIFICADO,
        DES_HUELLA_CERTIFICADO = r.DES_HUELLA_CERTIFICADO,
        FEC_VENCIMIENTO_CERTIFICADO = r.FEC_VENCIMIENTO_CERTIFICADO,
        NUM_DIAS_PARA_VENCER = r.FEC_VENCIMIENTO_CERTIFICADO is { } v ? (int)Math.Floor((v - _ahora()).TotalDays) : null,
        CIFRADO_DISPONIBLE = _cifrado.Disponible,
        DES_CIFRADO_NO_DISPONIBLE = _cifrado.Disponible ? null : _cifrado.MensajeNoDisponible,
        DES_FALTA = Falta(r),
        COD_USUARIO_ACTUALIZACION = r.COD_USUARIO_ACTUALIZACION,
        FEC_ACTUALIZACION = r.FEC_ACTUALIZACION
    };

    /// <summary>Lo que falta para poder activar la emisión, en una frase ("falta el client_id y la clave
    /// SOL"). Vacío si está completo. Es la misma lista que valida el SP al activar.</summary>
    public static string Falta(ConfiguracionEmisionElectronicaRegistro r)
    {
        var falta = new List<string>();
        if (string.IsNullOrWhiteSpace(r.DES_URL_SEGURIDAD)) falta.Add("la dirección de seguridad de SUNAT");
        if (string.IsNullOrWhiteSpace(r.DES_URL_API)) falta.Add("la dirección de la API de SUNAT");
        if (string.IsNullOrWhiteSpace(r.DES_CLIENT_ID)) falta.Add("el client_id");
        if (r.ENC_CLIENT_SECRET is not { Length: > 0 }) falta.Add("el client_secret");
        if (string.IsNullOrWhiteSpace(r.DES_USUARIO_SOL)) falta.Add("el usuario SOL");
        if (r.ENC_CLAVE_SOL is not { Length: > 0 }) falta.Add("la clave SOL");
        if (r.ENC_CERTIFICADO is not { Length: > 0 }) falta.Add("el certificado digital");
        if (r.ENC_CLAVE_CERTIFICADO is not { Length: > 0 }) falta.Add("la clave del certificado");
        return falta.Count == 0 ? string.Empty : "falta " + string.Join(", ", falta);
    }

    // ── Guardar ──────────────────────────────────────────────────────────────────────────────

    public async Task<ApiResponse<WriteResult>> UpdateAsync(ConfiguracionEmisionElectronicaUpdateDto dto, string codUsuario, string codEstacion)
    {
        try
        {
            var ambiente = (dto.COD_AMBIENTE ?? string.Empty).Trim().ToUpperInvariant();
            if (ambiente is not ("PRUEBAS" or "PRODUCCION"))
                return ApiResponse<WriteResult>.Fail("El ambiente es PRUEBAS o PRODUCCION.");

            var traeSecretos = Hay(dto.DES_CLIENT_SECRET_NUEVO) || Hay(dto.DES_CLAVE_SOL_NUEVA)
                            || Hay(dto.DES_CLAVE_CERTIFICADO_NUEVA) || Hay(dto.DES_CERTIFICADO_BASE64);
            if (traeSecretos && !_cifrado.Disponible)
                return ApiResponse<WriteResult>.Fail(_cifrado.MensajeNoDisponible);

            var actual = await _repo.ShowbyidAsync() ?? new ConfiguracionEmisionElectronicaRegistro();
            var nuevo = new ConfiguracionEmisionElectronicaRegistro
            {
                COD_AMBIENTE = ambiente,
                FLG_ACTIVA = dto.FLG_ACTIVA,
                DES_URL_SEGURIDAD = Limpio(dto.DES_URL_SEGURIDAD),
                DES_URL_API = Limpio(dto.DES_URL_API),
                DES_CLIENT_ID = Limpio(dto.DES_CLIENT_ID),
                DES_USUARIO_SOL = Limpio(dto.DES_USUARIO_SOL),
                // null = no cambiar
                ENC_CLIENT_SECRET = Hay(dto.DES_CLIENT_SECRET_NUEVO) ? _cifrado.CifrarTexto(dto.DES_CLIENT_SECRET_NUEVO!, CtxClientSecret) : null,
                ENC_CLAVE_SOL = Hay(dto.DES_CLAVE_SOL_NUEVA) ? _cifrado.CifrarTexto(dto.DES_CLAVE_SOL_NUEVA!, CtxClaveSol) : null
            };

            var error = PrepararCertificado(dto, actual, nuevo);
            if (error is not null) return ApiResponse<WriteResult>.Fail(error);

            var r = await _repo.UpdateAsync(nuevo, codUsuario, codEstacion);
            return ApiResponse<WriteResult>.FromWriteResult(r, "Configuración guardada.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(UpdateAsync));
            return ApiResponse<WriteResult>.Fail(ex.Message);
        }
    }

    /// <summary>El certificado y su clave. Se abren acá, antes de guardar: un .pfx dañado o con la
    /// clave equivocada se descubre al cargarlo y no el día que hay que firmar la primera guía.</summary>
    private string? PrepararCertificado(ConfiguracionEmisionElectronicaUpdateDto dto, ConfiguracionEmisionElectronicaRegistro actual, ConfiguracionEmisionElectronicaRegistro nuevo)
    {
        var claveNueva = Hay(dto.DES_CLAVE_CERTIFICADO_NUEVA) ? dto.DES_CLAVE_CERTIFICADO_NUEVA : null;

        if (Hay(dto.DES_CERTIFICADO_BASE64))
        {
            byte[] pfx;
            try { pfx = Convert.FromBase64String(QuitarPrefijo(dto.DES_CERTIFICADO_BASE64!)); }
            catch (FormatException) { return "El archivo del certificado no llegó bien (base64 inválido). Vuelve a cargarlo."; }

            // La clave: la que se escribió ahora o, si no, la ya guardada (se cambia el archivo y no la clave).
            var clave = claveNueva ?? ClaveGuardada(actual);
            if (clave is null) return "Para cargar el certificado hace falta su clave.";

            var abre = Abrir(pfx, clave);
            if (abre.Error is not null) return abre.Error;

            nuevo.ENC_CERTIFICADO = _cifrado.CifrarBytes(pfx, CtxCertificado);
            nuevo.NOM_ARCHIVO_CERTIFICADO = Limpio(dto.NOM_ARCHIVO_CERTIFICADO) ?? "certificado.pfx";
            nuevo.DES_SUJETO_CERTIFICADO = abre.Sujeto;
            nuevo.DES_HUELLA_CERTIFICADO = abre.Huella;
            nuevo.FEC_VENCIMIENTO_CERTIFICADO = abre.Vencimiento;
            if (claveNueva is not null) nuevo.ENC_CLAVE_CERTIFICADO = _cifrado.CifrarTexto(claveNueva, CtxClaveCertificado);
            return null;
        }

        if (claveNueva is not null)
        {
            // Sólo cambia la clave: si ya hay certificado, tiene que abrir con ella.
            if (actual.ENC_CERTIFICADO is { Length: > 0 })
            {
                byte[] pfx;
                try { pfx = _cifrado.DescifrarBytes(actual.ENC_CERTIFICADO, CtxCertificado); }
                catch (CryptographicException) { return "No se pudo leer el certificado guardado (la clave de cifrado del servidor cambió). Vuelve a cargarlo."; }
                var abre = Abrir(pfx, claveNueva);
                if (abre.Error is not null) return abre.Error;
            }
            nuevo.ENC_CLAVE_CERTIFICADO = _cifrado.CifrarTexto(claveNueva, CtxClaveCertificado);
        }
        return null;
    }

    private string? ClaveGuardada(ConfiguracionEmisionElectronicaRegistro actual)
    {
        if (actual.ENC_CLAVE_CERTIFICADO is not { Length: > 0 }) return null;
        try { return _cifrado.DescifrarTexto(actual.ENC_CLAVE_CERTIFICADO, CtxClaveCertificado); }
        catch (CryptographicException) { return null; }
    }

    private sealed record CertificadoLeido(string? Error, string? Sujeto, string? Huella, DateTime? Vencimiento);

    private static CertificadoLeido Abrir(byte[] pfx, string clave)
    {
        try
        {
            using var cert = X509CertificateLoader.LoadPkcs12(pfx, clave, X509KeyStorageFlags.EphemeralKeySet);
            if (!cert.HasPrivateKey)
                return new(MensajeSinClavePrivada, null, null, null);
            return new(null, cert.Subject, cert.GetCertHashString(HashAlgorithmName.SHA256), cert.NotAfter);
        }
        catch (CryptographicException)
        {
            return new("No se pudo abrir el certificado: la clave es incorrecta o el archivo no es un .pfx válido.", null, null, null);
        }
    }

    private const string MensajeSinClavePrivada = "El certificado no trae la clave privada: hace falta el .pfx completo, con la clave, para poder firmar.";

    // ── Para el cliente de SUNAT ─────────────────────────────────────────────────────────────

    public async Task<ApiResponse<CredencialesEmisionElectronica>> ObtenerCredencialesAsync()
    {
        try
        {
            if (!_cifrado.Disponible) return ApiResponse<CredencialesEmisionElectronica>.Fail(_cifrado.MensajeNoDisponible);
            var r = await _repo.ShowbyidAsync();
            if (r is null) return ApiResponse<CredencialesEmisionElectronica>.Fail("No está cargada la configuración de emisión electrónica.");
            if (!r.FLG_ACTIVA) return ApiResponse<CredencialesEmisionElectronica>.Fail("La emisión electrónica no está activada en la configuración.");
            var falta = Falta(r);
            if (falta.Length > 0) return ApiResponse<CredencialesEmisionElectronica>.Fail("La configuración de emisión electrónica está incompleta: " + falta + ".");

            return ApiResponse<CredencialesEmisionElectronica>.Ok(new CredencialesEmisionElectronica
            {
                Ambiente = r.COD_AMBIENTE,
                UrlSeguridad = r.DES_URL_SEGURIDAD!,
                UrlApi = r.DES_URL_API!,
                ClientId = r.DES_CLIENT_ID!,
                ClientSecret = _cifrado.DescifrarTexto(r.ENC_CLIENT_SECRET!, CtxClientSecret),
                UsuarioSol = r.DES_USUARIO_SOL!,
                ClaveSol = _cifrado.DescifrarTexto(r.ENC_CLAVE_SOL!, CtxClaveSol),
                Certificado = _cifrado.DescifrarBytes(r.ENC_CERTIFICADO!, CtxCertificado),
                ClaveCertificado = _cifrado.DescifrarTexto(r.ENC_CLAVE_CERTIFICADO!, CtxClaveCertificado)
            });
        }
        catch (CryptographicException)
        {
            return ApiResponse<CredencialesEmisionElectronica>.Fail("No se pudieron descifrar las credenciales: la clave de cifrado del servidor no es la que las cifró. Vuelve a cargarlas en la configuración.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(ObtenerCredencialesAsync));
            return ApiResponse<CredencialesEmisionElectronica>.Fail(ex.Message);
        }
    }

    public async Task<EmisionConfigEstado> EstadoAsync()
    {
        try
        {
            var r = await _repo.ShowbyidAsync();
            if (r is null) return new EmisionConfigEstado { Existe = false, CifradoDisponible = _cifrado.Disponible };
            return new EmisionConfigEstado
            {
                Existe = true,
                Activa = r.FLG_ACTIVA,
                DesFalta = Falta(r),
                VencimientoCertificado = r.FEC_VENCIMIENTO_CERTIFICADO,
                CifradoDisponible = _cifrado.Disponible
            };
        }
        catch (Exception ex)
        {
            _log.LogError(ex, Programa, Clase, nameof(EstadoAsync));
            return new EmisionConfigEstado { Existe = false, CifradoDisponible = _cifrado.Disponible };
        }
    }

    private static bool Hay(string? s) => !string.IsNullOrEmpty(s);
    private static string? Limpio(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    /// <summary>Un archivo leído en el navegador puede llegar como "data:...;base64,XXXX".</summary>
    private static string QuitarPrefijo(string s)
    {
        var i = s.IndexOf("base64,", StringComparison.OrdinalIgnoreCase);
        return (i >= 0 ? s[(i + 7)..] : s).Trim();
    }
}
