using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace KonSolutionsApi.Application.Services;

/// <summary>Un documento relacionado con el traslado (opcional): una factura, una orden de entrega, etc.
/// Tipo y descripción son los del catálogo 61 de SUNAT que el usuario elija; el emisor (RUC) es opcional.</summary>
public sealed class GuiaDocumentoRelacionado
{
    public string Numero { get; init; } = string.Empty;
    public string Tipo { get; init; } = string.Empty;
    public string Descripcion { get; init; } = string.Empty;
    public string? RucEmisor { get; init; }
}

/// <summary>Lo que necesita el XML de una guía: las filas de PROC_LOGISTICA_REPORTE_MOVIMIENTOS del movimiento, el número que
/// le tocó y el momento de la emisión. Lo demás (partes, traslado, transporte y líneas) sale de las filas.</summary>
public sealed class GuiaXmlEntrada
{
    /// <summary>Una fila por línea y por vehículo principal; la guía usa la primera para la cabecera y una por secuencia para las líneas.</summary>
    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Filas { get; init; } = Array.Empty<IReadOnlyDictionary<string, object?>>();
    public string Serie { get; init; } = string.Empty;
    public int Correlativo { get; init; }
    /// <summary>Fecha y hora de emisión: la genera la emisión, no se pide.</summary>
    public DateTime FechaHoraEmision { get; init; }
    /// <summary>Traslado en vehículos de categoría M1 o L (SUNAT no pide vehículo ni conductor: se indica con este marcador).</summary>
    public bool TrasladoVehiculoM1L { get; init; }
    /// <summary>Transbordo programado (el traslado cambia de vehículo en el camino).</summary>
    public bool TransbordoProgramado { get; init; }
    public IReadOnlyList<GuiaDocumentoRelacionado> DocumentosRelacionados { get; init; } = Array.Empty<GuiaDocumentoRelacionado>();
}

/// <summary>El generador del XML de la guía de remisión electrónica remitente (UBL 2.1, DespatchAdvice, tipo 09).
///
/// Es una función de sus datos: no toca la base ni la red, y se prueba sin ellas. Arma el documento SIN firmar, con el
/// espacio de la firma ya reservado dentro de ext:UBLExtensions (ver <see cref="GuiaRemisionFirma"/>).
///
/// La estructura sigue la de la guía remitente de la plataforma GRE de SUNAT (R.S. 123-2022): cabecera, documentos relacionados,
/// remitente, destinatario, el envío (motivo, peso, modalidad, transportista o conductor, puntos de partida y llegada,
/// vehículos) y una línea por producto. Qué dato del sistema va en qué nodo está documentado en cada método. Lo que depende
/// del motivo de traslado (ver GuiaRemisionValidador.MotivosEmitibles) se arma aquí y se exige allá.</summary>
public static class GuiaRemisionXmlGenerador
{
    public const string TipoDocumento = "09";

    private static readonly XNamespace Ubl = "urn:oasis:names:specification:ubl:schema:xsd:DespatchAdvice-2";
    private static readonly XNamespace Cac = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
    private static readonly XNamespace Cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
    private static readonly XNamespace Ext = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2";
    private static readonly XNamespace Ds = "http://www.w3.org/2000/09/xmldsig#";

    private const string Catalogos = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo";
    /// <summary>Los dos identificadores de la firma dentro del XML: el nodo cac:Signature apunta al ds:Signature por su Id.</summary>
    public const string IdFirma = "SignatureSP";
    private const string IdCabeceraFirma = "IDSignSP";
    private static readonly Regex CaracteresInvalidos = new(@"[^\u0009\u000A\u000D -퟿-�]", RegexOptions.Compiled);

    /// <summary>El nombre del archivo de la guía (sin extensión): RUC-09-SERIE-CORRELATIVO. El correlativo va sin ceros a la izquierda.</summary>
    public static string NombreArchivo(string ruc, string serie, int correlativo) => $"{ruc}-{TipoDocumento}-{serie}-{correlativo.ToString(CultureInfo.InvariantCulture)}";

    /// <summary>El número que se escribe en el XML (cbc:ID): SERIE-CORRELATIVO, sin ceros a la izquierda en el correlativo.</summary>
    public static string NumeroEnXml(string serie, int correlativo) => $"{serie}-{correlativo.ToString(CultureInfo.InvariantCulture)}";

    public static XDocument Generar(GuiaXmlEntrada e)
    {
        if (e.Filas.Count == 0) throw new InvalidOperationException("El movimiento no tiene líneas: no hay guía que armar.");
        var f = e.Filas[0];
        var ruc = Txt(f, "NUM_RUC_REMITENTE") ?? throw new InvalidOperationException("Falta el RUC del remitente.");
        var razon = Txt(f, "DES_NOMBRE_REMITENTE") ?? throw new InvalidOperationException("Falta la razón social del remitente.");
        var motivo = Txt(f, "COD_MOTIVO_SUNAT") ?? throw new InvalidOperationException("Falta el motivo de traslado.");
        var modalidad = GuiaRemisionValidador.ModalidadDeTraslado(f, e.TrasladoVehiculoM1L) ?? throw new InvalidOperationException("Falta la modalidad de traslado (público o privado).");

        var raiz = new XElement(Ubl + "DespatchAdvice",
            new XAttribute(XNamespace.Xmlns + "cac", Cac), new XAttribute(XNamespace.Xmlns + "cbc", Cbc),
            new XAttribute(XNamespace.Xmlns + "ds", Ds), new XAttribute(XNamespace.Xmlns + "ext", Ext),
            // El espacio de la firma: la firma digital se inserta aquí (ext:ExtensionContent).
            new XElement(Ext + "UBLExtensions", new XElement(Ext + "UBLExtension", new XElement(Ext + "ExtensionContent"))),
            new XElement(Cbc + "UBLVersionID", "2.1"),
            new XElement(Cbc + "CustomizationID", "2.0"),
            new XElement(Cbc + "ID", NumeroEnXml(e.Serie, e.Correlativo)),
            new XElement(Cbc + "IssueDate", e.FechaHoraEmision.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            new XElement(Cbc + "IssueTime", e.FechaHoraEmision.ToString("HH:mm:ss", CultureInfo.InvariantCulture)),
            new XElement(Cbc + "DespatchAdviceTypeCode", new XAttribute("listAgencyName", "PE:SUNAT"), new XAttribute("listName", "Tipo de Documento"),
                new XAttribute("listURI", Catalogos + "01"), TipoDocumento));

        // La observación del movimiento (SUNAT admite hasta 250 caracteres).
        if (Txt(f, "DES_OBSERVACION") is { } obs)
            raiz.Add(new XElement(Cbc + "Note", new XCData(Corto(obs, 250))));

        foreach (var d in e.DocumentosRelacionados)
            raiz.Add(DocumentoRelacionado(d));

        raiz.Add(
            new XElement(Cac + "Signature",
                new XElement(Cbc + "ID", IdCabeceraFirma),
                new XElement(Cac + "SignatoryParty",
                    new XElement(Cac + "PartyIdentification", new XElement(Cbc + "ID", ruc)),
                    new XElement(Cac + "PartyName", new XElement(Cbc + "Name", new XCData(Limpio(razon))))),
                new XElement(Cac + "DigitalSignatureAttachment", new XElement(Cac + "ExternalReference", new XElement(Cbc + "URI", "#" + IdFirma)))),
            // El remitente: la empresa.
            new XElement(Cac + "DespatchSupplierParty", Parte(ruc, "6", razon)),
            // El destinatario.
            new XElement(Cac + "DeliveryCustomerParty",
                Parte(Txt(f, "NUM_DOCUMENTO_DESTINATARIO") ?? throw new InvalidOperationException("Falta el documento del destinatario."),
                      Txt(f, "COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT") ?? throw new InvalidOperationException("Falta el tipo de documento del destinatario."),
                      Txt(f, "DES_NOMBRE_DESTINATARIO") ?? throw new InvalidOperationException("Falta el nombre del destinatario."))),
            Envio(e, f, ruc, motivo, modalidad));

        var n = 0;
        foreach (var l in LineasUnicas(e.Filas))
            raiz.Add(Linea(++n, l));

        return new XDocument(new XDeclaration("1.0", "UTF-8", "no"), raiz);
    }

    // ── Partes ───────────────────────────────────────────────────────────────────────────────

    private static XElement Parte(string numero, string tipoDocumento, string nombre) =>
        new(Cac + "Party",
            new XElement(Cac + "PartyIdentification", new XElement(Cbc + "ID", Esquema06(tipoDocumento), numero)),
            new XElement(Cac + "PartyLegalEntity", new XElement(Cbc + "RegistrationName", new XCData(Limpio(nombre)))));

    /// <summary>Los atributos del tipo de documento de identidad (catálogo 06): el tipo y de dónde sale.</summary>
    private static object[] Esquema06(string tipoDocumento) => new object[]
    {
        new XAttribute("schemeID", tipoDocumento), new XAttribute("schemeName", "Documento de Identidad"),
        new XAttribute("schemeAgencyName", "PE:SUNAT"), new XAttribute("schemeURI", Catalogos + "06")
    };

    private static XElement DocumentoRelacionado(GuiaDocumentoRelacionado d)
    {
        var x = new XElement(Cac + "AdditionalDocumentReference",
            new XElement(Cbc + "ID", d.Numero),
            new XElement(Cbc + "DocumentTypeCode", new XAttribute("listAgencyName", "PE:SUNAT"), new XAttribute("listName", "Documento relacionado al transporte"),
                new XAttribute("listURI", Catalogos + "61"), d.Tipo),
            new XElement(Cbc + "DocumentType", d.Descripcion));
        if (!string.IsNullOrWhiteSpace(d.RucEmisor))
            x.Add(new XElement(Cac + "IssuerParty", new XElement(Cac + "PartyIdentification", new XElement(Cbc + "ID", Esquema06("6"), d.RucEmisor))));
        return x;
    }

    // ── El envío: motivo, peso, modalidad, transporte, puntos y vehículos ────────────────────

    private static XElement Envio(GuiaXmlEntrada e, IReadOnlyDictionary<string, object?> f, string ruc, string motivo, string modalidad)
    {
        var envio = new XElement(Cac + "Shipment", new XElement(Cbc + "ID", "SUNAT_Envio"),
            new XElement(Cbc + "HandlingCode", new XAttribute("listName", "Motivo de traslado"), new XAttribute("listAgencyName", "PE:SUNAT"),
                new XAttribute("listURI", Catalogos + "20"), motivo));

        // «Otros» (13): SUNAT pide la descripción del motivo. Es la observación del movimiento y, si no hay, el nombre del motivo.
        if (motivo == "13")
            envio.Add(new XElement(Cbc + "HandlingInstructions", Corto(Txt(f, "DES_OBSERVACION") ?? Txt(f, "DES_MOTIVO") ?? "Otros", 100)));

        var peso = Dec(f, "CAN_PESO_BRUTO_TOTAL") ?? throw new InvalidOperationException("Falta el peso bruto total.");
        envio.Add(new XElement(Cbc + "GrossWeightMeasure", new XAttribute("unitCode", Txt(f, "COD_UNIDAD_PESO_SUNAT") ?? "KGM"), Num(peso, 3)));

        // Los indicadores del traslado (punto 15): M1/L y transbordo programado.
        if (e.TrasladoVehiculoM1L)
            envio.Add(new XElement(Cbc + "SpecialInstructions", "SUNAT_Envio_IndicadorTrasladoVehiculoM1L"));
        if (e.TransbordoProgramado)
            envio.Add(new XElement(Cbc + "SpecialInstructions", "SUNAT_Envio_IndicadorTransbordoProgramado"));

        // La modalidad (catálogo 18: 01 público, 02 privado) y, con ella, quién transporta.
        var etapa = new XElement(Cac + "ShipmentStage",
            new XElement(Cbc + "TransportModeCode", new XAttribute("listName", "Modalidad de traslado"), new XAttribute("listAgencyName", "PE:SUNAT"),
                new XAttribute("listURI", Catalogos + "18"), modalidad),
            new XElement(Cac + "TransitPeriod", new XElement(Cbc + "StartDate", (Fec(f, "FEC_INICIO_TRASLADO") ?? throw new InvalidOperationException("Falta la fecha de inicio de traslado."))
                .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));
        if (modalidad == "01")
        {
            // Transporte público: se declara la empresa transportista (su RUC, su razón social y su registro MTC). Sin vehículo ni conductor.
            var transportista = new XElement(Cac + "CarrierParty",
                new XElement(Cac + "PartyIdentification", new XElement(Cbc + "ID", Esquema06("6"), Txt(f, "NUM_DOCUMENTO_TRANSPORTISTA_PUBLICO") ?? throw new InvalidOperationException("Falta el RUC del transportista."))));
            var legal = new XElement(Cac + "PartyLegalEntity",
                new XElement(Cbc + "RegistrationName", new XCData(Limpio(Txt(f, "DES_NOMBRE_TRANSPORTISTA_PUBLICO") ?? Txt(f, "DES_NOMBRE_TRANSPORTISTA") ?? throw new InvalidOperationException("Falta la razón social del transportista.")))));
            if (Txt(f, "NUM_CERTIFICADO_HABILITACION_MTC_PUBLICO") is { } mtc)
                legal.Add(new XElement(Cbc + "CompanyID", mtc));
            transportista.Add(legal);
            etapa.Add(transportista);
        }
        else if (!e.TrasladoVehiculoM1L || Txt(f, "NUM_DOCUMENTO_TRANSPORTISTA") is not null)
        {
            // Transporte privado: se declara el conductor principal (documento, nombres y brevete). El vehículo va más abajo.
            // En un traslado M1 o L el conductor no es obligatorio: se declara si el movimiento lo tiene y se omite si no.
            var nombres = string.Join(" ", new[] { Txt(f, "DES_PRIMER_NOMBRE_TRANSPORTISTA"), Txt(f, "DES_SEGUNDO_NOMBRE_TRANSPORTISTA") }.Where(x => x is not null));
            var apellidos = string.Join(" ", new[] { Txt(f, "DES_APELLIDO_PATERNO_TRANSPORTISTA"), Txt(f, "DES_APELLIDO_MATERNO_TRANSPORTISTA") }.Where(x => x is not null));
            var conductor = new XElement(Cac + "DriverPerson",
                new XElement(Cbc + "ID", Esquema06(Txt(f, "COD_TIPO_DOCUMENTO_TRANSPORTISTA_SUNAT") ?? throw new InvalidOperationException("Falta el tipo de documento del conductor.")),
                    Txt(f, "NUM_DOCUMENTO_TRANSPORTISTA") ?? throw new InvalidOperationException("Falta el documento del conductor.")),
                new XElement(Cbc + "FirstName", Limpio(nombres)),
                new XElement(Cbc + "FamilyName", Limpio(apellidos)),
                new XElement(Cbc + "JobTitle", "Principal"));
            if (Txt(f, "NUM_LICENCIA_TRANSPORTISTA") is { } brevete)
                conductor.Add(new XElement(Cac + "IdentityDocumentReference", new XElement(Cbc + "ID", brevete)));
            else if (!e.TrasladoVehiculoM1L)
                throw new InvalidOperationException("Falta el brevete del conductor.");
            etapa.Add(conductor);
        }
        envio.Add(etapa);

        // Los puntos de llegada y de partida: ubigeo (INEI), código de establecimiento anexo si es un local de la empresa, y la dirección.
        envio.Add(new XElement(Cac + "Delivery",
            new XElement(Cac + "DeliveryAddress", Punto(ruc, Txt(f, "COD_UBIGEO_DESTINO"), Txt(f, "COD_SUCURSAL_SUNAT_DESTINO"), Txt(f, "DES_DIRECCION_DESTINO"), "llegada")),
            new XElement(Cac + "Despatch", new XElement(Cac + "DespatchAddress", Punto(ruc, Txt(f, "COD_UBIGEO_ORIGEN"), Txt(f, "COD_SUCURSAL_SUNAT_ORIGEN"), Txt(f, "DES_DIRECCION_ORIGEN"), "partida")))));

        // El vehículo (solo transporte privado): placa del principal, su tarjeta o certificado, y el vehículo de acarreo como secundario.
        if (modalidad == "02" && Txt(f, "NUM_PLACA_PRINCIPAL") is { } placa)
        {
            var equipo = new XElement(Cac + "TransportEquipment", new XElement(Cbc + "ID", GuiaRemisionValidador.NormalizarPlaca(placa)));
            if (Tarjeta(f, "PRINCIPAL") is { } tarjeta)
                equipo.Add(new XElement(Cac + "ApplicableTransportMeans", new XElement(Cbc + "RegistrationNationalityID", tarjeta)));
            if (Txt(f, "NUM_PLACA_ACARREO") is { } placaAcarreo)
            {
                var secundario = new XElement(Cac + "AttachedTransportEquipment", new XElement(Cbc + "ID", GuiaRemisionValidador.NormalizarPlaca(placaAcarreo)));
                if (Tarjeta(f, "ACARREO") is { } tarjetaAcarreo)
                    secundario.Add(new XElement(Cac + "ApplicableTransportMeans", new XElement(Cbc + "RegistrationNationalityID", tarjetaAcarreo)));
                equipo.Add(secundario);
            }
            envio.Add(new XElement(Cac + "TransportHandlingUnit", equipo));
        }
        return envio;
    }

    /// <summary>La tarjeta única de circulación o certificado de habilitación vehicular del vehículo: el «certificado vehicular» del sistema;
    /// si no lo tiene, su constancia de inscripción. (El certificado de habilitación MTC del vehículo no se envía: va con la autorización
    /// especial, que pide el código de la entidad que la emitió y el sistema todavía no lo guarda.)</summary>
    private static string? Tarjeta(IReadOnlyDictionary<string, object?> f, string cual) =>
        Txt(f, "NUM_CERTIFICADO_VEHICULAR_" + cual) ?? Txt(f, "NUM_CONSTANCIA_INSCRIPCION_" + cual);

    private static object[] Punto(string ruc, string? ubigeo, string? establecimiento, string? direccion, string cual)
    {
        var partes = new List<object>
        {
            new XElement(Cbc + "ID", new XAttribute("schemeAgencyName", "PE:INEI"), new XAttribute("schemeName", "Ubigeos"),
                ubigeo ?? throw new InvalidOperationException($"Falta el ubigeo del punto de {cual}."))
        };
        if (establecimiento is not null)
            partes.Add(new XElement(Cbc + "AddressTypeCode", new XAttribute("listID", ruc), new XAttribute("listAgencyName", "PE:SUNAT"),
                new XAttribute("listName", "Establecimientos anexos"), establecimiento));
        partes.Add(new XElement(Cac + "AddressLine", new XElement(Cbc + "Line", Limpio(direccion ?? throw new InvalidOperationException($"Falta la dirección del punto de {cual}.")))));
        return partes.ToArray();
    }

    // ── Las líneas ───────────────────────────────────────────────────────────────────────────

    /// <summary>Cada línea puede venir repetida (una vez por vehículo principal): una por secuencia.</summary>
    private static IEnumerable<IReadOnlyDictionary<string, object?>> LineasUnicas(IReadOnlyList<IReadOnlyDictionary<string, object?>> filas) =>
        filas.GroupBy(r => Txt(r, "NUM_SECUENCIA") ?? "?").Select(g => g.First());

    private static XElement Linea(int n, IReadOnlyDictionary<string, object?> l)
    {
        var cantidad = Dec(l, "CAN_SALIDA") ?? throw new InvalidOperationException($"La línea {n} no tiene cantidad de salida.");
        var item = new XElement(Cac + "Item",
            new XElement(Cbc + "Description", new XCData(Limpio(Txt(l, "DES_PRODUCTO") ?? throw new InvalidOperationException($"La línea {n} no tiene descripción")))),
            new XElement(Cac + "SellersItemIdentification", new XElement(Cbc + "ID", Limpio(Txt(l, "COD_PRODUCTO") ?? n.ToString(CultureInfo.InvariantCulture)))));
        // El código de producto SUNAT (UNSPSC): solo si el producto lo tiene.
        if (Txt(l, "COD_CUBSO") is { } cubso)
            item.Add(new XElement(Cac + "CommodityClassification", new XElement(Cbc + "ItemClassificationCode",
                new XAttribute("listID", "UNSPSC"), new XAttribute("listAgencyName", "GS1 US"), new XAttribute("listName", "Item Classification"), cubso)));
        return new XElement(Cac + "DespatchLine",
            new XElement(Cbc + "ID", n.ToString(CultureInfo.InvariantCulture)),
            new XElement(Cbc + "DeliveredQuantity", new XAttribute("unitCode", Txt(l, "COD_UNIDAD_MEDIDA_SUNAT") ?? throw new InvalidOperationException($"La línea {n} no tiene unidad SUNAT")), Num(cantidad, 10, true)),
            new XElement(Cac + "OrderLineReference", new XElement(Cbc + "LineID", n.ToString(CultureInfo.InvariantCulture))),
            item);
    }

    // ── Ayudas ───────────────────────────────────────────────────────────────────────────────

    /// <summary>El número con punto decimal; con <paramref name="recortar"/>, sin ceros de sobra a la derecha.</summary>
    private static string Num(decimal d, int decimales, bool recortar = false)
    {
        var s = Math.Round(d, decimales, MidpointRounding.AwayFromZero).ToString("F" + decimales, CultureInfo.InvariantCulture);
        if (!recortar) return s;
        s = s.TrimEnd('0');
        return s.TrimEnd('.');
    }

    private static string Corto(string s, int max) { s = Limpio(s); return s.Length <= max ? s : s[..max]; }

    /// <summary>Sin los caracteres que XML no admite, y sin saltos de línea ni espacios de sobra.</summary>
    private static string Limpio(string s) => Regex.Replace(CaracteresInvalidos.Replace(s, " "), @"\s+", " ").Trim();

    private static string? Txt(IReadOnlyDictionary<string, object?> f, string c)
    {
        if (!f.TryGetValue(c, out var v) || v is null || v is DBNull) return null;
        var s = Convert.ToString(v, CultureInfo.InvariantCulture)?.Trim();
        return string.IsNullOrEmpty(s) ? null : s;
    }

    private static decimal? Dec(IReadOnlyDictionary<string, object?> f, string c)
        => Txt(f, c) is { } s && decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : null;

    private static DateTime? Fec(IReadOnlyDictionary<string, object?> f, string c)
    {
        if (!f.TryGetValue(c, out var v) || v is null || v is DBNull) return null;
        return v switch { DateTime d => d, DateOnly d => d.ToDateTime(TimeOnly.MinValue), _ => DateTime.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.None, out var x) ? x : null };
    }

    /// <summary>El texto del código QR de la representación impresa: RUC del emisor, tipo de documento (09), serie, número (8 dígitos),
    /// fecha de emisión (dd/MM/yyyy), tipo y número de documento del destinatario, separados por barras.</summary>
    public static string TextoQr(string ruc, string serie, int correlativo, DateTime emision, string tipoDocDestinatario, string numDocDestinatario) =>
        string.Join("|", ruc, TipoDocumento, serie, correlativo.ToString("D8", CultureInfo.InvariantCulture),
                    emision.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), tipoDocDestinatario, numDocDestinatario);
}
