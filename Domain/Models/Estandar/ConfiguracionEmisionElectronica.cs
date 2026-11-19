using System.ComponentModel.DataAnnotations;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>ESTANDAR.CONFIGURACION_EMISION_ELECTRONICA tal como está en la base, con las columnas
/// cifradas (ENC_*). SÓLO la usa la API: la pantalla recibe <see cref="ConfiguracionEmisionElectronica"/>,
/// que nunca lleva un secreto.</summary>
public class ConfiguracionEmisionElectronicaRegistro
{
    public byte NUM_CONFIGURACION { get; set; }
    public string COD_AMBIENTE { get; set; } = "PRUEBAS";
    public bool FLG_ACTIVA { get; set; }
    public string? DES_URL_SEGURIDAD { get; set; }
    public string? DES_URL_API { get; set; }
    public string? DES_CLIENT_ID { get; set; }
    public byte[]? ENC_CLIENT_SECRET { get; set; }
    public string? DES_USUARIO_SOL { get; set; }
    public byte[]? ENC_CLAVE_SOL { get; set; }
    public string? NOM_ARCHIVO_CERTIFICADO { get; set; }
    public byte[]? ENC_CERTIFICADO { get; set; }
    public byte[]? ENC_CLAVE_CERTIFICADO { get; set; }
    public string? DES_SUJETO_CERTIFICADO { get; set; }
    public string? DES_HUELLA_CERTIFICADO { get; set; }
    public DateTime? FEC_VENCIMIENTO_CERTIFICADO { get; set; }
    public string? COD_USUARIO_REGISTRO { get; set; }
    public string? COD_ESTACION_REGISTRO { get; set; }
    public DateTime? FEC_REGISTRO { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public string? COD_ESTACION_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>Lo que ve la pantalla de Configuración de emisión electrónica.
///
/// En lugar de los secretos, dice QUÉ HAY CARGADO (TIENE_*): así el formulario puede mostrar
/// "cargado" y dejar el campo vacío, y quien quiera cambiarlo escribe uno nuevo. Un secreto que
/// salió de la API una vez ya no es un secreto.</summary>
public class ConfiguracionEmisionElectronica
{
    public string COD_AMBIENTE { get; set; } = "PRUEBAS";
    public bool FLG_ACTIVA { get; set; }
    public string? DES_URL_SEGURIDAD { get; set; }
    public string? DES_URL_API { get; set; }
    public string? DES_CLIENT_ID { get; set; }
    public string? DES_USUARIO_SOL { get; set; }
    public bool TIENE_CLIENT_SECRET { get; set; }
    public bool TIENE_CLAVE_SOL { get; set; }
    public bool TIENE_CERTIFICADO { get; set; }
    public bool TIENE_CLAVE_CERTIFICADO { get; set; }
    public string? NOM_ARCHIVO_CERTIFICADO { get; set; }
    public string? DES_SUJETO_CERTIFICADO { get; set; }
    public string? DES_HUELLA_CERTIFICADO { get; set; }
    public DateTime? FEC_VENCIMIENTO_CERTIFICADO { get; set; }
    /// <summary>Días que le quedan al certificado (negativo: ya venció). Null si no hay certificado.</summary>
    public int? NUM_DIAS_PARA_VENCER { get; set; }
    /// <summary>False si el servidor no tiene la clave de cifrado (variable de entorno
    /// KonSolutions_EMISION_CLAVE): sin ella no se pueden guardar ni leer los secretos.</summary>
    public bool CIFRADO_DISPONIBLE { get; set; }
    public string? DES_CIFRADO_NO_DISPONIBLE { get; set; }
    /// <summary>Lo que falta para poder activar la emisión, en una frase. Vacío: está completo.</summary>
    public string? DES_FALTA { get; set; }
    public string? COD_USUARIO_ACTUALIZACION { get; set; }
    public DateTime? FEC_ACTUALIZACION { get; set; }
}

/// <summary>Lo que manda la pantalla al guardar. Los campos *_NUEVO / *_NUEVA sólo se mandan cuando
/// se escribió algo: vacío o null significa "no cambiar lo que hay".</summary>
public class ConfiguracionEmisionElectronicaUpdateDto
{
    [Required] public string COD_AMBIENTE { get; set; } = "PRUEBAS";
    public bool FLG_ACTIVA { get; set; }
    public string? DES_URL_SEGURIDAD { get; set; }
    public string? DES_URL_API { get; set; }
    public string? DES_CLIENT_ID { get; set; }
    public string? DES_USUARIO_SOL { get; set; }
    public string? DES_CLIENT_SECRET_NUEVO { get; set; }
    public string? DES_CLAVE_SOL_NUEVA { get; set; }
    public string? DES_CLAVE_CERTIFICADO_NUEVA { get; set; }
    /// <summary>El archivo .pfx en base64 (así viajan también los íconos y los logos de la app).</summary>
    public string? DES_CERTIFICADO_BASE64 { get; set; }
    public string? NOM_ARCHIVO_CERTIFICADO { get; set; }
}

/// <summary>Las credenciales ya descifradas, para el cliente de SUNAT. NUNCA sale por un endpoint.</summary>
public class CredencialesEmisionElectronica
{
    public string Ambiente { get; set; } = "PRUEBAS";
    public string UrlSeguridad { get; set; } = string.Empty;
    public string UrlApi { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string UsuarioSol { get; set; } = string.Empty;
    public string ClaveSol { get; set; } = string.Empty;
    public byte[] Certificado { get; set; } = Array.Empty<byte>();
    public string ClaveCertificado { get; set; } = string.Empty;
}

/// <summary>Un resumen de la configuración para el validador previo: qué hay y qué no, sin secretos.</summary>
public class EmisionConfigEstado
{
    public bool Existe { get; set; }
    public bool Activa { get; set; }
    public string DesFalta { get; set; } = string.Empty;
    public DateTime? VencimientoCertificado { get; set; }
    public bool CifradoDisponible { get; set; }
}
