using System.Globalization;
using System.Text.RegularExpressions;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Services;

/// <summary>Todo lo que el validador necesita saber, ya leído. Así la validación no toca la base:
/// es una función de estos datos, y se prueba sin base.</summary>
public sealed class GuiaValidacionContexto
{
    /// <summary>Las filas de PROC_LOGISTICA_REPORTE_MOVIMIENTOS del movimiento: una por línea y por vehículo principal.</summary>
    public IReadOnlyList<IReadOnlyDictionary<string, object?>> Filas { get; init; } = Array.Empty<IReadOnlyDictionary<string, object?>>();
    public EmisionConfigEstado Config { get; init; } = new();
    /// <summary>La guía del movimiento (la vigente o, si no hay, la última). Null si nunca tuvo.</summary>
    public GuiaRemision? Guia { get; init; }
    /// <summary>Las series con las que se puede emitir (FNC_LOGISTICA_GUIA_SERIES_PARA_MOVIMIENTO).</summary>
    public IReadOnlyList<string> SeriesDisponibles { get; init; } = Array.Empty<string>();
    /// <summary>"Hoy": la fecha contra la que se comparan la del certificado y la de inicio de traslado.</summary>
    public DateTime Hoy { get; init; } = DateTime.Today;
    /// <summary>Se emite como traslado en vehículos de categoría M1 o L (opción de la emisión): SUNAT no exige los datos del vehículo ni del
    /// conductor, así que lo que falte de ellos avisa pero no impide emitir, y sin vehículo principal la modalidad es la privada.</summary>
    public bool TrasladoVehiculoM1L { get; init; }
}

/// <summary>El validador previo a la emisión de la guía de remisión remitente.
///
/// Revisa, ANTES de gastar un número y de hablar con SUNAT, que el movimiento esté en condiciones
/// de emitir. Distingue dos gravedades: ERROR impide emitir; AVISO deja emitir pero conviene
/// mirarlo (son reglas de las que no hay certeza de que SUNAT las exija, o datos que casi siempre
/// conviene tener).
///
/// Los datos son los que devuelve PROC_LOGISTICA_REPORTE_MOVIMIENTOS, que es de ustedes y puede
/// cambiar: si le falta una columna que acá se usa, lo dice con GRE-001 en vez de tomar el dato
/// por vacío y reportar mil errores falsos.</summary>
public static class GuiaRemisionValidador
{
    private const string Cfg = "Configuración", Guia = "Guía", Mov = "Movimiento", Rem = "Remitente", Dest = "Destinatario",
                         Tras = "Traslado", Pun = "Puntos", Trans = "Transporte", Merc = "Mercadería", Rep = "Reporte";

    /// <summary>Las columnas del SP sin las que no se puede validar. No están las que sólo alimentan un AVISO (COD_CUBSO, el registro
    /// MTC del transportista, los documentos del vehículo): si faltan, ese aviso simplemente no sale.</summary>
    public static readonly string[] ColumnasRequeridas =
    {
        "NUM_SECUENCIA", "COD_PRODUCTO", "DES_PRODUCTO", "CAN_SALIDA", "COD_UNIDAD_MEDIDA_SUNAT", "DES_SENTIDO",
        "NUM_RUC_REMITENTE", "DES_NOMBRE_REMITENTE",
        "COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT", "NUM_DOCUMENTO_DESTINATARIO", "DES_NOMBRE_DESTINATARIO",
        "FEC_INICIO_TRASLADO", "COD_MODALIDAD_TRASLADO", "CAN_PESO_BRUTO_TOTAL", "COD_UNIDAD_PESO_SUNAT", "COD_MOTIVO_SUNAT",
        "DES_DIRECCION_ORIGEN", "COD_UBIGEO_ORIGEN", "COD_SUCURSAL_SUNAT_ORIGEN",
        "DES_DIRECCION_DESTINO", "COD_UBIGEO_DESTINO", "COD_SUCURSAL_SUNAT_DESTINO",
        "COD_ENTIDAD_TRANSPORTISTA", "DES_NOMBRE_TRANSPORTISTA", "DES_PRIMER_NOMBRE_TRANSPORTISTA", "DES_APELLIDO_PATERNO_TRANSPORTISTA",
        "COD_TIPO_DOCUMENTO_TRANSPORTISTA_SUNAT", "NUM_DOCUMENTO_TRANSPORTISTA", "NUM_LICENCIA_TRANSPORTISTA",
        "DES_NOMBRE_TRANSPORTISTA_PUBLICO", "COD_TIPO_DOCUMENTO_TRANSPORTISTA_SUNAT_PUBLICO", "NUM_DOCUMENTO_TRANSPORTISTA_PUBLICO",
        "COD_TRANSPORTE_PRINCIPAL", "NUM_PLACA_PRINCIPAL",
        "COD_TRANSPORTE_ACARREO", "NUM_PLACA_ACARREO",
        "NUM_GUIAS_MOVIMIENTO", "NUM_TRANSPORTES_ACARREO"
    };

    /// <summary>Los motivos de traslado (catálogo 20 de SUNAT) que el sistema sabe armar, con su nombre. Cada motivo pide nodos
    /// distintos en el XML; se habilita uno cuando están hechas sus reglas. Para sumar otro: agregarlo aquí, sus reglas en
    /// <see cref="Motivo"/> y el XML en el generador.</summary>
    public static readonly IReadOnlyDictionary<string, string> MotivosEmitibles = new Dictionary<string, string>
    {
        ["01"] = "Venta",
        ["04"] = "Traslado entre establecimientos de la misma empresa",
        ["06"] = "Devolución",
        ["13"] = "Otros"
    };

    private static readonly Regex SoloDigitos = new(@"^\d+$", RegexOptions.Compiled);
    private static readonly Regex Placa = new(@"^[A-Z0-9]{6,7}$", RegexOptions.Compiled);
    private static readonly string[] DocumentosDePersona = { "1", "4", "7", "A" };
    private const int DiasAvisoCertificado = 30;
    private const int MaximoDeLineasListadas = 5;

    public static GuiaValidacionResultado Validar(GuiaValidacionContexto ctx)
    {
        var items = new List<GuiaValidacionItem>();
        void Error(string codigo, string seccion, string mensaje) => items.Add(new() { Codigo = codigo, Gravedad = GuiaValidacionGravedad.Error, Seccion = seccion, Mensaje = mensaje });
        void Aviso(string codigo, string seccion, string mensaje) => items.Add(new() { Codigo = codigo, Gravedad = GuiaValidacionGravedad.Aviso, Seccion = seccion, Mensaje = mensaje });

        Configuracion(ctx, Error, Aviso);
        GuiaExistente(ctx, Error, Aviso);

        if (ctx.Filas.Count == 0)
        {
            Error("GRE-030", Mov, "El movimiento no existe o no tiene líneas.");
        }
        else
        {
            var faltan = ColumnasRequeridas.Where(c => !ctx.Filas[0].ContainsKey(c)).ToList();
            if (faltan.Count > 0)
                Error("GRE-001", Rep, $"Al procedimiento PROC_LOGISTICA_REPORTE_MOVIMIENTOS le faltan columnas que la guía necesita: {string.Join(", ", faltan)}. No se pudo revisar el movimiento.");
            else
                Datos(ctx, Error, Aviso);
        }

        var resultado = new GuiaValidacionResultado
        {
            // Los errores primero; dentro de cada gravedad, el orden en que se fueron encontrando.
            Items = items.OrderBy(i => i.Gravedad == GuiaValidacionGravedad.Error ? 0 : 1).ToList(),
            GuiaVigente = ctx.Guia
        };
        resultado.NumErrores = items.Count(i => i.Gravedad == GuiaValidacionGravedad.Error);
        resultado.NumAvisos = items.Count - resultado.NumErrores;
        resultado.PuedeEmitir = resultado.NumErrores == 0;
        return resultado;
    }

    // ── Configuración de emisión y series ────────────────────────────────────────────────────

    private static void Configuracion(GuiaValidacionContexto ctx, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        var c = ctx.Config;
        if (!c.Existe)
            error("GRE-010", Cfg, "No está cargada la configuración de emisión electrónica. Complétala en Configuración de emisión electrónica.");
        else if (!c.Activa)
            error("GRE-010", Cfg, "La emisión electrónica no está activada. Actívala en Configuración de emisión electrónica.");
        if (c.Existe && !string.IsNullOrEmpty(c.DesFalta))
            error("GRE-011", Cfg, $"La configuración de emisión electrónica está incompleta: {c.DesFalta}.");
        if (!c.CifradoDisponible)
            error("GRE-012", Cfg, $"El servidor no tiene la clave para descifrar las credenciales (variable de entorno {CifradoSecretos.VariableDeEntorno}).");

        if (c.VencimientoCertificado is { } v)
        {
            var dias = (int)Math.Floor((v - ctx.Hoy).TotalDays);
            if (v < ctx.Hoy)
                error("GRE-013", Cfg, $"El certificado digital venció el {Fecha(v)}. Carga uno vigente.");
            else if (dias <= DiasAvisoCertificado)
                aviso("GRE-014", Cfg, $"El certificado digital vence en {dias} día{(dias == 1 ? "" : "s")} ({Fecha(v)}).");
        }

        if (ctx.SeriesDisponibles.Count == 0)
            error("GRE-015", Cfg, "No hay una serie activa para emitir la guía de este movimiento. Crea una en Series de guías de remisión.");
        else if (ctx.SeriesDisponibles.Count > 1)
            aviso("GRE-016", Cfg, $"Hay varias series activas que sirven para este movimiento ({string.Join(", ", ctx.SeriesDisponibles)}): al emitir hay que elegir con cuál.");
    }

    // ── La guía que el movimiento ya pueda tener ─────────────────────────────────────────────

    private static void GuiaExistente(GuiaValidacionContexto ctx, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        var g = ctx.Guia;
        if (g is null || !g.FLG_VIGENTE) return;
        switch (g.COD_ESTADO_ALIAS)
        {
            case "REC":
                aviso("GRE-021", Guia, $"La guía {g.NUM_GUIA} fue rechazada por SUNAT. Corrige lo que indica la respuesta y usa Reintentar: no se emite una guía nueva.");
                break;
            default:
                error("GRE-020", Guia, $"El movimiento ya tiene la guía {g.NUM_GUIA} ({g.DES_ESTADO ?? g.COD_ESTADO_ALIAS}).");
                break;
        }
    }

    // ── Los datos del movimiento ─────────────────────────────────────────────────────────────

    private static void Datos(GuiaValidacionContexto ctx, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        var f = ctx.Filas[0];

        // El sentido: la guía remitente se emite al sacar mercadería.
        if (!string.Equals(Txt(f, "DES_SENTIDO"), "SALIDA", StringComparison.OrdinalIgnoreCase))
            error("GRE-031", Mov, "El movimiento es un INGRESO: la guía de remisión remitente se emite para salidas.");

        // Un movimiento, una guía, un vehículo principal y como máximo un acarreo. (Un acarreo sin principal no llega acá: sin
        // principal no hay modalidad de traslado y lo detiene GRE-080.)
        var guias = Ent(f, "NUM_GUIAS_MOVIMIENTO");
        if (guias > 1)
            error("GRE-032", Trans, $"El movimiento tiene {guias} vehículos principales. Va una sola guía por movimiento: deja un solo vehículo principal (si la carga va en otro vehículo, es otro movimiento).");
        var acarreos = Ent(f, "NUM_TRANSPORTES_ACARREO");
        if (acarreos > 1)
            error("GRE-033", Trans, $"El movimiento tiene {acarreos} vehículos de acarreo. Se permite como máximo uno.");

        var motivo = Txt(f, "COD_MOTIVO_SUNAT");
        if (motivo is null)
            error("GRE-034", Mov, "El motivo del movimiento no tiene código SUNAT (catálogo 20). Complétalo en Motivos.");
        else if (!Regex.IsMatch(motivo, @"^\d{2}$"))
            error("GRE-034", Mov, $"El código SUNAT del motivo ({motivo}) no es válido: son dos dígitos (catálogo 20).");

        Motivo(f, motivo, error, aviso);
        Remitente(f, error);
        Destinatario(f, error);
        Traslado(f, ctx.Hoy, error, aviso);
        Puntos(f, motivo, error, aviso);
        Transporte(f, ctx.TrasladoVehiculoM1L, error, aviso);
        Mercaderia(ctx.Filas, error, aviso);
    }

    // ── Lo que pide cada motivo de traslado ──────────────────────────────────────────────────

    private static void Motivo(IReadOnlyDictionary<string, object?> f, string? motivo, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        if (motivo is null || !Regex.IsMatch(motivo, @"^\d{2}$")) return;   // ya lo dijo GRE-034
        if (!MotivosEmitibles.ContainsKey(motivo))
        {
            var nombre = Txt(f, "DES_MOTIVO");
            error("GRE-110", Tras, $"El motivo de traslado {motivo}{(nombre is null ? "" : $" ({nombre})")} todavía no se emite desde el sistema. Por ahora se emiten: "
                                   + string.Join(", ", MotivosEmitibles.Select(m => $"{m.Key} ({m.Value})")) + ".");
            return;
        }

        var ruc = Txt(f, "NUM_RUC_REMITENTE");
        var tipoDest = Txt(f, "COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT");
        var docDest = Txt(f, "NUM_DOCUMENTO_DESTINATARIO");
        var mismaEmpresa = ruc is not null && tipoDest == "6" && docDest == ruc;

        switch (motivo)
        {
            case "04":
                // Entre establecimientos de la misma empresa: el destinatario ES el remitente (mismo RUC).
                if (!mismaEmpresa)
                    error("GRE-111", Dest, "En un traslado entre establecimientos (motivo 04) el destinatario es la misma empresa: su RUC tiene que ser el del remitente.");
                break;
            case "01":
            case "06":
                // Una venta o una devolución a uno mismo casi siempre es, en realidad, un traslado entre establecimientos.
                if (mismaEmpresa)
                    aviso("GRE-112", Dest, $"El destinatario es la propia empresa. Si es un traslado entre sus establecimientos, el motivo correcto es 04 (no {motivo}).");
                break;
            case "13":
                // «Otros»: SUNAT pide la descripción del motivo. Se toma de la observación del movimiento y, si no hay, del nombre del motivo.
                if (Txt(f, "DES_OBSERVACION") is null && Txt(f, "DES_MOTIVO") is null)
                    error("GRE-113", Tras, "En el motivo 13 (Otros) SUNAT pide la descripción del motivo: escríbela en la observación del movimiento.");
                break;
        }
    }

    private static void Remitente(IReadOnlyDictionary<string, object?> f, Action<string, string, string> error)
    {
        var ruc = Txt(f, "NUM_RUC_REMITENTE");
        if (ruc is null || !EsRuc(ruc))
            error("GRE-040", Rem, "El RUC de la empresa no está cargado o no tiene 11 dígitos. Complétalo en Configuración General.");
        if (Txt(f, "DES_NOMBRE_REMITENTE") is null)
            error("GRE-041", Rem, "Falta la razón social de la empresa en Configuración General.");
    }

    private static void Destinatario(IReadOnlyDictionary<string, object?> f, Action<string, string, string> error)
    {
        var tipo = Txt(f, "COD_TIPO_DOCUMENTO_DESTINATARIO_SUNAT");
        var numero = Txt(f, "NUM_DOCUMENTO_DESTINATARIO");
        if (tipo is null)
            error("GRE-050", Dest, "El destinatario no tiene tipo de documento con código SUNAT. Revisa el tipo de documento de identidad de la entidad.");
        if (numero is null)
            error("GRE-051", Dest, "El destinatario no tiene número de documento.");
        else if (tipo == "6" && !EsRuc(numero))
            error("GRE-052", Dest, $"El RUC del destinatario ({numero}) no tiene 11 dígitos.");
        else if (tipo == "1" && !(numero.Length == 8 && SoloDigitos.IsMatch(numero)))
            error("GRE-052", Dest, $"El DNI del destinatario ({numero}) no tiene 8 dígitos.");
        if (Txt(f, "DES_NOMBRE_DESTINATARIO") is null)
            error("GRE-053", Dest, "El destinatario no tiene nombre o razón social.");
    }

    private static void Traslado(IReadOnlyDictionary<string, object?> f, DateTime hoy, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        if (Fec(f, "FEC_INICIO_TRASLADO") is not { } inicio)
            error("GRE-060", Tras, "Falta la fecha de inicio de traslado (Fecha de Inicio de Traslado del movimiento).");
        else if (inicio.Date < hoy.Date)
            aviso("GRE-061", Tras, $"La fecha de inicio de traslado ({Fecha(inicio)}) es anterior a la fecha de emisión (hoy). Si SUNAT la rechaza, corrige la fecha.");

        var peso = Dec(f, "CAN_PESO_BRUTO_TOTAL");
        if (peso is null or <= 0)
            error("GRE-062", Tras, "El peso bruto total es cero. Revisa la cantidad secundaria (el peso) de las líneas.");
        var unidad = Txt(f, "COD_UNIDAD_PESO_SUNAT");
        if (unidad is null)
            error("GRE-063", Tras, "Las líneas tienen unidades de peso distintas, o una sin código SUNAT: el peso bruto necesita una sola unidad.");
        else if (unidad is not ("KGM" or "TNE"))
            aviso("GRE-064", Tras, $"La unidad del peso bruto es {unidad}: SUNAT espera kilogramos (KGM) o toneladas (TNE).");
    }

    private static void Puntos(IReadOnlyDictionary<string, object?> f, string? motivo, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        var dirO = Txt(f, "DES_DIRECCION_ORIGEN"); var ubiO = Txt(f, "COD_UBIGEO_ORIGEN"); var estO = Txt(f, "COD_SUCURSAL_SUNAT_ORIGEN");
        var dirD = Txt(f, "DES_DIRECCION_DESTINO"); var ubiD = Txt(f, "COD_UBIGEO_DESTINO"); var estD = Txt(f, "COD_SUCURSAL_SUNAT_DESTINO");

        if (dirO is null) error("GRE-070", Pun, "El punto de partida no tiene dirección. Revisa la dirección de la sucursal del almacén.");
        if (ubiO is null || !EsUbigeo(ubiO)) error("GRE-071", Pun, "El punto de partida no tiene ubigeo (6 dígitos). Revisa el ubigeo de la sucursal del almacén.");
        if (dirD is null) error("GRE-072", Pun, "El punto de llegada no tiene dirección. Elige la dirección de entrega en la pestaña Transportista.");
        if (ubiD is null || !EsUbigeo(ubiD)) error("GRE-073", Pun, "El punto de llegada no tiene ubigeo (6 dígitos). Revisa el ubigeo de la dirección elegida.");

        if (dirO is not null && dirD is not null && ubiO is not null && ubiO == ubiD
            && Normalizar(dirO) == Normalizar(dirD) && string.Equals(estO, estD, StringComparison.OrdinalIgnoreCase))
            error("GRE-074", Pun, "El punto de partida y el de llegada son el mismo lugar. Revisa la dirección de entrega elegida.");

        if (motivo == "04")
        {
            if (estO is null) error("GRE-075", Pun, "En un traslado entre establecimientos (motivo 04) falta el código de establecimiento SUNAT de la sucursal de partida.");
            if (estD is null) error("GRE-075", Pun, "En un traslado entre establecimientos (motivo 04) falta el código de establecimiento SUNAT de la sucursal de llegada.");
        }
        else if (estO is null)
            aviso("GRE-076", Pun, "La sucursal de partida no tiene código de establecimiento SUNAT (0000 si es la matriz). Complétalo en Sucursales.");
    }

    private static void Transporte(IReadOnlyDictionary<string, object?> f, bool m1l, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        var modalidad = ModalidadDeTraslado(f, m1l);
        if (modalidad is null)
        {
            error("GRE-080", Trans, "El movimiento no tiene vehículo principal: sin él no se sabe si el traslado es público o privado. Agrégalo en la pestaña Transportista.");
            return;
        }

        if (modalidad == "01")
        {
            // Transporte público: se declara la empresa transportista. Vehículo y conductor no se declaran.
            if (Txt(f, "COD_ENTIDAD_TRANSPORTISTA") is null)
            {
                error("GRE-081", Trans, "Falta el transportista (la empresa de transporte público) en la pestaña Transportista.");
                return;
            }
            var tipo = Txt(f, "COD_TIPO_DOCUMENTO_TRANSPORTISTA_SUNAT_PUBLICO");
            var numero = Txt(f, "NUM_DOCUMENTO_TRANSPORTISTA_PUBLICO");
            if (tipo != "6" || numero is null || !EsRuc(numero))
                error("GRE-082", Trans, "El transportista público tiene que identificarse con RUC (tipo 6, 11 dígitos). Revisa su documento de identidad en el maestro de entidades.");
            if ((Txt(f, "DES_NOMBRE_TRANSPORTISTA_PUBLICO") ?? Txt(f, "DES_NOMBRE_TRANSPORTISTA")) is null)
                error("GRE-083", Trans, "El transportista público no tiene razón social.");
            if (Txt(f, "NUM_CERTIFICADO_HABILITACION_MTC_PUBLICO") is null)
                aviso("GRE-084", Trans, "El transportista no tiene número de registro MTC: SUNAT puede pedirlo para el transporte público.");
            return;
        }

        // Transporte privado: se declara el conductor y el vehículo. En un traslado M1 o L (opción de la emisión) SUNAT no los exige:
        // lo que falte se avisa pero no impide emitir.
        var exigido = m1l ? aviso : error;
        if (Txt(f, "COD_ENTIDAD_TRANSPORTISTA") is null)
            exigido("GRE-085", Trans, "Falta el conductor (el transportista del movimiento) en la pestaña Transportista.");
        else
        {
            var tipo = Txt(f, "COD_TIPO_DOCUMENTO_TRANSPORTISTA_SUNAT");
            if (tipo is null || !DocumentosDePersona.Contains(tipo))
                exigido("GRE-086", Trans, "El conductor tiene que identificarse con DNI, carné de extranjería, pasaporte o cédula diplomática (no con RUC). Revisa su documento en el maestro de entidades.");
            if (Txt(f, "NUM_DOCUMENTO_TRANSPORTISTA") is null)
                exigido("GRE-086", Trans, "El conductor no tiene número de documento de identidad.");
            if (Txt(f, "DES_PRIMER_NOMBRE_TRANSPORTISTA") is null || Txt(f, "DES_APELLIDO_PATERNO_TRANSPORTISTA") is null)
                exigido("GRE-087", Trans, "El conductor necesita nombre y apellido paterno separados en el maestro de entidades.");
            if (Txt(f, "NUM_LICENCIA_TRANSPORTISTA") is null)
                exigido("GRE-088", Trans, "Falta el brevete (licencia de conducir) del conductor: se registra como documento de identidad de su entidad, del tipo Brevete.");
        }

        var placa = Txt(f, "NUM_PLACA_PRINCIPAL");
        if (placa is null)
            exigido("GRE-089", Trans, "El vehículo principal no tiene placa (especificación técnica de placa del transporte).");
        else if (!Placa.IsMatch(NormalizarPlaca(placa)))
            aviso("GRE-090", Trans, $"La placa del vehículo principal ({placa}) no parece una placa: se envía sin guion ni espacios y con 6 o 7 caracteres.");
        // Estos tres datos salen de las especificaciones técnicas del vehículo; cuál va en qué parte del XML se decide al generarlo.
        if (Txt(f, "NUM_CONSTANCIA_INSCRIPCION_PRINCIPAL") is null && Txt(f, "NUM_CERTIFICADO_VEHICULAR_PRINCIPAL") is null && Txt(f, "NUM_CERTIFICADO_HABILITACION_MTC_PRINCIPAL") is null)
            aviso("GRE-091", Trans, "El vehículo principal no tiene constancia de inscripción, certificado vehicular ni certificado de habilitación MTC. SUNAT puede rechazar la guía si lo exige para ese vehículo (no lo pide en categorías M1 o L).");

        if (Txt(f, "COD_TRANSPORTE_ACARREO") is not null && Txt(f, "NUM_PLACA_ACARREO") is null)
            exigido("GRE-092", Trans, "El vehículo de acarreo no tiene placa (especificación técnica de placa del transporte).");
    }

    private static void Mercaderia(IReadOnlyList<IReadOnlyDictionary<string, object?>> filas, Action<string, string, string> error, Action<string, string, string> aviso)
    {
        // Cada línea puede venir repetida (una vez por vehículo principal): se revisa una vez por secuencia.
        var lineas = filas.GroupBy(r => Txt(r, "NUM_SECUENCIA") ?? "?").Select(g => g.First()).ToList();
        var sinCantidad = new List<string>(); var sinUnidad = new List<string>(); var sinNombre = new List<string>(); var sinCubso = new List<string>();
        foreach (var l in lineas)
        {
            var etiqueta = $"{Txt(l, "NUM_SECUENCIA")} ({Txt(l, "COD_PRODUCTO") ?? "sin código"})";
            if (Dec(l, "CAN_SALIDA") is null or <= 0) sinCantidad.Add(etiqueta);
            if (Txt(l, "COD_UNIDAD_MEDIDA_SUNAT") is null) sinUnidad.Add(etiqueta);
            if (Txt(l, "DES_PRODUCTO") is null) sinNombre.Add(etiqueta);
            if (Txt(l, "COD_CUBSO") is null) sinCubso.Add(etiqueta);
        }
        if (sinCantidad.Count > 0) error("GRE-100", Merc, $"Líneas sin cantidad de salida: {Lista(sinCantidad)}.");
        if (sinUnidad.Count > 0) error("GRE-101", Merc, $"Líneas cuya unidad de medida no tiene código SUNAT: {Lista(sinUnidad)}. Complétalo en Unidades de medida.");
        if (sinNombre.Count > 0) error("GRE-102", Merc, $"Líneas sin descripción del producto: {Lista(sinNombre)}.");
        if (sinCubso.Count > 0) aviso("GRE-103", Merc, $"Productos sin código de producto SUNAT: {Lista(sinCubso)}. SUNAT lo exige para los bienes normalizados.");
    }

    // ── Ayudas ───────────────────────────────────────────────────────────────────────────────

    private static string Lista(List<string> l) => l.Count <= MaximoDeLineasListadas
        ? string.Join(", ", l)
        : string.Join(", ", l.Take(MaximoDeLineasListadas)) + $" y {l.Count - MaximoDeLineasListadas} más";

    private static bool EsRuc(string s) => s.Length == 11 && SoloDigitos.IsMatch(s);
    private static bool EsUbigeo(string s) => s.Length == 6 && SoloDigitos.IsMatch(s);
    private static string Normalizar(string s) => Regex.Replace(s.Trim().ToUpperInvariant(), @"\s+", " ");
    /// <summary>La modalidad del traslado (catálogo 18: 01 público, 02 privado) sale del vehículo principal. En un traslado M1 o L sin vehículo
    /// principal es la privada: SUNAT no pide vehículo.</summary>
    public static string? ModalidadDeTraslado(IReadOnlyDictionary<string, object?> f, bool m1l) => Txt(f, "COD_MODALIDAD_TRASLADO") ?? (m1l ? "02" : null);

    public static string NormalizarPlaca(string s) => Regex.Replace(s.ToUpperInvariant(), @"[\s\-]", "");
    private static string Fecha(DateTime d) => d.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

    /// <summary>Texto sin espacios de los bordes; vacío = no hay dato.</summary>
    private static string? Txt(IReadOnlyDictionary<string, object?> f, string c)
    {
        if (!f.TryGetValue(c, out var v) || v is null || v is DBNull) return null;
        var s = Convert.ToString(v, CultureInfo.InvariantCulture)?.Trim();
        return string.IsNullOrEmpty(s) ? null : s;
    }

    private static int Ent(IReadOnlyDictionary<string, object?> f, string c)
        => Txt(f, c) is { } s && decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? (int)d : 0;

    private static decimal? Dec(IReadOnlyDictionary<string, object?> f, string c)
        => Txt(f, c) is { } s && decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : null;

    private static DateTime? Fec(IReadOnlyDictionary<string, object?> f, string c)
    {
        if (!f.TryGetValue(c, out var v) || v is null || v is DBNull) return null;
        return v switch { DateTime d => d, DateOnly d => d.ToDateTime(TimeOnly.MinValue), _ => DateTime.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.None, out var x) ? x : null };
    }
}
