using System.IO.Compression;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace KonSolutionsApi.Application.Services;

/// <summary>El XML ya firmado: sus bytes, el valor resumen (DigestValue) de la firma y el certificado con el que se firmó.</summary>
public sealed class GuiaFirmada
{
    public byte[] Xml { get; init; } = Array.Empty<byte>();
    /// <summary>El DigestValue de la referencia de la firma, en base64: es el «hash» que se imprime en la guía (DES_HASH_XML).</summary>
    public string Hash { get; init; } = string.Empty;
}

/// <summary>La firma digital de la guía: XMLDSig «enveloped» (la firma va dentro del propio XML, en ext:ExtensionContent), con el
/// certificado .pfx de la empresa. RSA con SHA-256, canonicalización C14N 1.0 y el certificado dentro de la firma (ds:X509Data).
///
/// Firma todo el documento (Reference URI=""). El certificado y su clave se cargan en memoria y no se guardan.</summary>
public static class GuiaRemisionFirma
{
    private const string NsExt = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";

    /// <summary>Firma el XML. Falla con un mensaje claro si el .pfx no se abre (clave equivocada) o no trae clave privada.</summary>
    public static GuiaFirmada Firmar(XDocument xml, byte[] pfx, string clave)
    {
        using var cert = Cargar(pfx, clave);
        using var rsa = cert.GetRSAPrivateKey() ?? throw new InvalidOperationException("El certificado digital no trae una clave privada RSA: no sirve para firmar.");

        var doc = new XmlDocument { PreserveWhitespace = true };
        doc.LoadXml((xml.Declaration?.ToString() ?? "") + xml.ToString(SaveOptions.DisableFormatting));

        var firma = new SignedXml(doc) { SigningKey = rsa };
        firma.Signature.Id = GuiaRemisionXmlGenerador.IdFirma;
        firma.SignedInfo!.CanonicalizationMethod = SignedXml.XmlDsigCanonicalizationUrl;
        firma.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;
        var referencia = new Reference("") { DigestMethod = SignedXml.XmlDsigSHA256Url };
        referencia.AddTransform(new XmlDsigEnvelopedSignatureTransform());
        firma.AddReference(referencia);
        var info = new KeyInfo();
        info.AddClause(new KeyInfoX509Data(cert));
        firma.KeyInfo = info;
        firma.ComputeSignature();

        var contenido = doc.GetElementsByTagName("ExtensionContent", NsExt).Cast<XmlNode>().FirstOrDefault()
                        ?? throw new InvalidOperationException("El XML no tiene el espacio para la firma (ext:ExtensionContent).");
        contenido.AppendChild(doc.ImportNode(firma.GetXml(), true));

        return new GuiaFirmada { Xml = new UTF8Encoding(false).GetBytes(doc.OuterXml), Hash = Convert.ToBase64String(referencia.DigestValue!) };
    }

    /// <summary>Comprueba la firma de un XML firmado (con el certificado que trae dentro). Falso si cambió algo después de firmar.</summary>
    public static bool Verificar(byte[] xmlFirmado)
    {
        var doc = new XmlDocument { PreserveWhitespace = true };
        using (var ms = new MemoryStream(xmlFirmado)) doc.Load(ms);
        var nodos = doc.GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#");
        if (nodos.Count != 1) return false;
        var firma = new SignedXml(doc);
        firma.LoadXml((XmlElement)nodos[0]!);
        return firma.CheckSignature();
    }

    private static X509Certificate2 Cargar(byte[] pfx, string clave)
    {
        try { return X509CertificateLoader.LoadPkcs12(pfx, clave); }
        catch (CryptographicException ex)
        {
            throw new InvalidOperationException("No se pudo abrir el certificado digital: la clave no es la del .pfx o el archivo está dañado.", ex);
        }
    }
}

/// <summary>El paquete que se manda a SUNAT: el XML firmado dentro de un ZIP, y su hash SHA-256.</summary>
public static class GuiaRemisionPaquete
{
    /// <summary>El ZIP con un solo archivo, <c>{nombreBase}.xml</c>, y el hash SHA-256 del ZIP en hexadecimal (minúsculas), como lo pide la API.</summary>
    public static (byte[] Zip, string HashHex) Crear(string nombreBase, byte[] xmlFirmado)
    {
        using var ms = new MemoryStream();
        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, leaveOpen: true))
        {
            var entrada = zip.CreateEntry(nombreBase + ".xml", CompressionLevel.Optimal);
            using var s = entrada.Open();
            s.Write(xmlFirmado, 0, xmlFirmado.Length);
        }
        var bytes = ms.ToArray();
        return (bytes, Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant());
    }
}
