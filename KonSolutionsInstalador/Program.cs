using System.Security.Cryptography;
using KonSolutionsInstalador;

// Instalador de la API (servicio de Windows KonSolutionsApi). Sin argumentos: menu interactivo. Con argumentos (desatendido):
//   Instalador.exe --instalar --destino C:\KonSolutions\Api --puerto 5001 [--http] [--certificado "nombre del certificado" | --pfx C:\cert.pfx --clave-pfx ...] --conexion "Server=..." --origenes https://web.empresa.com
//                  [--cuenta DOM\usu --clave-cuenta ...] [--sin-firewall] [--sin-iniciar]
//   Instalador.exe --desinstalar [--borrar-archivos --destino ...]      Instalador.exe --estado
const string VarDb = "KonSolutions_DB_CONNECTION", VarJwt = "KonSolutions_JWT_SECRET", VarEmision = "KonSolutions_EMISION_CLAVE";
var servicio = new Servicio("KonSolutionsApi", "KonSolutionsApi.exe", "KonSolutions API", "API del sistema KonSolutions (servidor de datos de la aplicacion web).");
const string Regla = "KonSolutions API";
Console.OutputEncoding = System.Text.Encoding.UTF8;

string? Arg(string n) { var i = Array.IndexOf(args, n); return i >= 0 && i + 1 < args.Length && !args[i + 1].StartsWith("--") ? args[i + 1] : null; }
bool Tiene(string n) => args.Contains(n);
string? Maquina(string v) => Environment.GetEnvironmentVariable(v, EnvironmentVariableTarget.Machine);

Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("KonSolutions - Instalador de la API (servicio de Windows KonSolutionsApi)"); Console.ResetColor();
if (!OperatingSystem.IsWindows()) { Consola.Error("Este instalador es para Windows."); return 1; }
if (!EsAdministrador()) { Consola.Error("Ejecutalo como Administrador (clic derecho > Ejecutar como administrador)."); return 1; }

if (Tiene("--estado")) { Console.WriteLine("Servicio: " + servicio.Estado()); return 0; }
if (Tiene("--desinstalar")) return Desinstalar(Tiene("--borrar-archivos"), Arg("--destino"), false);
if (Tiene("--instalar")) return await Instalar(LeerArgs(), false);

while (true)
{
    Console.WriteLine();
    Console.WriteLine($"  Estado del servicio: {servicio.Estado()}");
    Console.WriteLine("  1) Instalar o actualizar");
    Console.WriteLine("  2) Desinstalar");
    Console.WriteLine("  3) Iniciar / reiniciar");
    Console.WriteLine("  4) Detener");
    Console.WriteLine("  5) Salir");
    switch (Consola.Preguntar("Elige", "1"))
    {
        case "1": await Instalar(Preguntar(), true); break;
        case "2": Desinstalar(Consola.Confirmar("Borrar tambien los archivos de la carpeta de instalacion?", false), null, true); break;
        case "3": servicio.Detener(); { var r = servicio.Iniciar(); if (r.Ok) Consola.Ok(r.Mensaje); else Consola.Error(r.Mensaje); } break;
        case "4": servicio.Detener(); Consola.Ok("Detenido: " + servicio.Estado()); break;
        case "5": return 0;
    }
}

static bool EsAdministrador() { using var id = System.Security.Principal.WindowsIdentity.GetCurrent(); return new System.Security.Principal.WindowsPrincipal(id).IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator); }

Ajustes LeerArgs() => new()
{
    Destino = Arg("--destino") ?? @"C:\KonSolutions\Api", Puerto = int.TryParse(Arg("--puerto"), out var p) ? p : 5001, Https = !Tiene("--http"), Certificado = Arg("--certificado") ?? "", Pfx = Arg("--pfx") ?? "", ClavePfx = Arg("--clave-pfx") ?? "",
    Conexion = Arg("--conexion") ?? "", Origenes = (Arg("--origenes") ?? "").Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList(),
    Cuenta = Arg("--cuenta") ?? "", ClaveCuenta = Arg("--clave-cuenta") ?? "", Firewall = !Tiene("--sin-firewall"), Iniciar = !Tiene("--sin-iniciar")
};

Ajustes Preguntar()
{
    Consola.Titulo("Datos de la instalacion (Enter acepta lo que sale entre corchetes)");
    var a = new Ajustes();
    a.Destino = Consola.Preguntar("Carpeta de instalacion", a.Destino);
    var previo = JsonConfig.Leer(Path.Combine(a.Destino, "appsettings.Production.json"));
    a.Https = Consola.Confirmar("Usar HTTPS (recomendado)?", true);
    var puertoPrevio = int.TryParse(Url(previo).Split(':').LastOrDefault(), out var pp) ? pp : (a.Https ? 5001 : 5000);
    a.Puerto = int.TryParse(Consola.Preguntar("Puerto", puertoPrevio.ToString()), out var pt) ? pt : puertoPrevio;
    if (a.Https)
    {
        var certs = Escucha.CertificadosDisponibles();
        if (certs.Count > 0)
        {
            Console.WriteLine("  Certificados de la maquina (Personal, con clave privada, vigentes):");
            for (int i = 0; i < certs.Count; i++) Console.WriteLine($"    {i + 1}) {certs[i].Asunto}   (vence {certs[i].Vence:yyyy-MM-dd})");
            Console.WriteLine("    0) Usar un archivo .pfx");
            var e = Consola.Preguntar("Elige el certificado", "1");
            if (int.TryParse(e, out var n) && n >= 1 && n <= certs.Count) a.Certificado = certs[n - 1].Asunto;
        }
        if (a.Certificado == "") { a.Pfx = Consola.Preguntar("Ruta del archivo .pfx"); a.ClavePfx = Consola.PreguntarOculto("Clave del .pfx"); }
    }
    a.Conexion = Consola.PreguntarOculto(Maquina(VarDb) is null ? "Cadena de conexion a SQL Server" : "Cadena de conexion a SQL Server (ya hay una guardada)");
    var origPrev = string.Join(", ", JsonConfig.Lista(previo, "Cors:Origenes"));
    Console.WriteLine("  Direcciones desde donde se abre la web (separadas por coma), por ejemplo https://sistema.empresa.com . Sin esto el navegador bloquea las llamadas a la API (CORS).");
    a.Origenes = Consola.Preguntar("Direcciones de la web", origPrev == "" ? null : origPrev).Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
    Console.WriteLine("  Cuenta con que corre el servicio. Si la API lee carpetas de red (rutas de archivos), usa una cuenta con acceso: LocalSystem no entra a carpetas de red. Vacio = LocalSystem.");
    a.Cuenta = Consola.Preguntar("Cuenta (DOMINIO\\usuario)", "");
    if (a.Cuenta != "") a.ClaveCuenta = Consola.PreguntarOculto("Clave de esa cuenta");
    a.Firewall = Consola.Confirmar($"Abrir el puerto {a.Puerto} en el Firewall de Windows?", true);
    return a;
}

static string Url(System.Text.Json.Nodes.JsonObject c) { var u = JsonConfig.Texto(c, "Kestrel:Endpoints:Https:Url"); return u != "" ? u : JsonConfig.Texto(c, "Kestrel:Endpoints:Http:Url"); }

async Task<int> Instalar(Ajustes a, bool interactivo)
{
    Consola.Titulo("1. Archivos de la API");
    var origen = Path.Combine(AppContext.BaseDirectory, "Api");
    if (!File.Exists(Path.Combine(origen, servicio.Exe))) { Consola.Error($"No encuentro {servicio.Exe} en {origen}. Usa la carpeta que genera publicar.ps1 (Instalador.exe y la subcarpeta Api juntos)."); return 1; }
    var cfg = Path.Combine(a.Destino, "appsettings.Production.json");
    var previo = JsonConfig.Leer(cfg);

    Consola.Titulo("2. Comprobaciones");
    var fallas = 0;
    var conexion = a.Conexion != "" ? a.Conexion : Maquina(VarDb) ?? "";
    if (conexion == "") { Consola.Error("Falta la cadena de conexion a SQL Server."); fallas++; }
    else { var r = await Pruebas.SqlAsync(conexion); if (r.Ok) Consola.Ok(r.Mensaje); else { Consola.Error(r.Mensaje); fallas++; } }
    if (a.Https)
    {
        if (a.Pfx != "") { if (File.Exists(a.Pfx)) Consola.Ok("Certificado .pfx encontrado."); else { Consola.Error($"No existe el archivo {a.Pfx}."); fallas++; } }
        else if (a.Certificado != "") { if (Escucha.CertificadosDisponibles().Any(c => c.Asunto == a.Certificado)) Consola.Ok($"Certificado '{a.Certificado}' disponible."); else { Consola.Error($"No encuentro un certificado vigente con clave privada llamado '{a.Certificado}' en el almacen de la maquina."); fallas++; } }
        else { Consola.Error("HTTPS necesita un certificado (--certificado o --pfx). Si no tienes, instala con HTTP."); fallas++; }
    }
    servicio.Detener();   // si ya estaba instalado, libera el puerto antes de probarlo
    if (Escucha.PuertoLibre(a.Puerto)) Consola.Ok($"El puerto {a.Puerto} esta libre."); else { Consola.Error($"El puerto {a.Puerto} esta ocupado por otro programa."); fallas++; }
    if (Maquina(VarJwt) is null) Consola.Info("No hay secreto de firma (JWT): se generara uno nuevo y se guardara como variable de maquina.");
    if (a.Origenes.Count == 0) Consola.Aviso("No indicaste las direcciones de la web: el navegador bloqueara las llamadas (CORS) hasta que las agregues.");
    if (fallas > 0 && interactivo && !Consola.Confirmar($"Hay {fallas} comprobacion(es) que fallaron. Instalar de todos modos?", false)) return 1;
    if (fallas > 0 && !interactivo) { Consola.Error("Instalacion cancelada por las comprobaciones."); return 1; }

    Consola.Titulo("3. Instalando");
    try
    {
        Directory.CreateDirectory(a.Destino);
        CopiarCarpeta(origen, a.Destino);
        Consola.Ok($"Archivos copiados a {a.Destino}");
        var logs = Path.Combine(a.Destino, "logs"); Directory.CreateDirectory(logs);
        var origenes = a.Origenes.Count > 0 ? a.Origenes : JsonConfig.Lista(previo, "Cors:Origenes").ToList();
        Escucha.EscribirKestrel(previo, a.Https, a.Puerto, a.Certificado, a.Pfx, a.ClavePfx);
        JsonConfig.Poner(previo, "AllowedHosts", "*");   // el appsettings.json trae "localhost": con eso la API rechaza (400) a quien entra por el nombre o la IP del servidor
        JsonConfig.Poner(previo, "ErrorLog:LogDirectory", logs);
        JsonConfig.Poner(previo, "ErrorLog:SaveToFile", true);
        JsonConfig.Poner(previo, "Seguridad:RedirigirAHttps", a.Https);
        JsonConfig.Poner(previo, "Cors:Origenes", new System.Text.Json.Nodes.JsonArray(origenes.Select(o => (System.Text.Json.Nodes.JsonNode?)o).ToArray()));
        JsonConfig.Guardar(cfg, previo);
        Consola.Ok("appsettings.Production.json escrito (puerto, certificado, CORS, carpeta de logs).");
        if (a.Conexion != "") { Environment.SetEnvironmentVariable(VarDb, a.Conexion, EnvironmentVariableTarget.Machine); Consola.Ok($"Variable de maquina {VarDb} guardada."); }
        if (Maquina(VarJwt) is null)
        {
            Environment.SetEnvironmentVariable(VarJwt, Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), EnvironmentVariableTarget.Machine);
            Consola.Ok($"Variable de maquina {VarJwt} generada (secreto de firma de sesiones). No la cambies despues: cerraria todas las sesiones.");
        }
        if (Maquina(VarEmision) is null)
        {
            Environment.SetEnvironmentVariable(VarEmision, Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)), EnvironmentVariableTarget.Machine);
            Consola.Ok($"Variable de maquina {VarEmision} generada (cifra las credenciales de emision electronica de guias). ANOTALA/respaldala: si se pierde, esas credenciales hay que volver a escribirlas.");
        }
        var crear = servicio.Crear(Path.Combine(a.Destino, servicio.Exe), a.Cuenta, a.ClaveCuenta);
        if (!crear.Ok) { Consola.Error(crear.Mensaje); return 1; }
        Consola.Ok(crear.Mensaje);
        if (a.Firewall) { var f = Escucha.AbrirFirewall(Regla, a.Puerto); if (f.Ok) Consola.Ok(f.Mensaje); else Consola.Aviso("No se pudo abrir el firewall: " + f.Mensaje); }
    }
    catch (Exception ex) { Consola.Error("No se pudo instalar: " + ex.Message); return 1; }

    if (a.Iniciar)
    {
        Consola.Titulo("4. Iniciando y verificando");
        var r = servicio.Iniciar();
        if (!r.Ok) { Consola.Error(r.Mensaje); return 1; }
        Consola.Ok(r.Mensaje);
        var url = $"{(a.Https ? "https" : "http")}://localhost:{a.Puerto}/health";
        var h = await Escucha.EsperarAsync(url, 60);
        if (h.Ok) Consola.Ok(h.Mensaje); else { Consola.Error(h.Mensaje); Consola.Info("Mira el Registro de eventos de Windows (Aplicacion) y " + Path.Combine(a.Destino, "logs")); return 1; }
        foreach (var o in (a.Origenes.Count > 0 ? a.Origenes : JsonConfig.Lista(previo, "Cors:Origenes").ToList()))
        {
            var cors = await Escucha.GetAsync(url, o);
            if (cors.AllowOrigin == o.TrimEnd('/') || cors.AllowOrigin == "*") Consola.Ok($"CORS: la API acepta llamadas desde {o}");
            else Consola.Aviso($"CORS: la API NO devolvio permiso para {o}. Revisa que la direccion este escrita igual que en el navegador (con https y sin barra final).");
        }
    }
    Console.WriteLine(); Consola.Ok("Listo.");
    return 0;
}

int Desinstalar(bool borrar, string? destino, bool interactivo)
{
    Consola.Titulo("Desinstalando");
    if (!servicio.Existe()) Consola.Aviso("El servicio no estaba instalado.");
    else { var r = servicio.Eliminar(); if (r.Ok) Consola.Ok(r.Mensaje); else { Consola.Error(r.Mensaje); return 1; } }
    Escucha.QuitarFirewall(Regla);
    var dir = destino ?? @"C:\KonSolutions\Api";
    if (interactivo && borrar) dir = Consola.Preguntar("Carpeta a borrar", dir);
    if (borrar && Directory.Exists(dir)) { try { Directory.Delete(dir, true); Consola.Ok("Archivos borrados: " + dir); } catch (Exception ex) { Consola.Aviso("No se pudo borrar la carpeta: " + ex.Message); } }
    Consola.Info("Las variables de entorno de maquina no se tocan (por si reinstalas): la del JWT y la de emision no deben perderse.");
    return 0;
}

// copia los archivos de la API; NO pisa la configuracion de este servidor (appsettings.Production.json) ni los logs
static void CopiarCarpeta(string origen, string destino)
{
    foreach (var d in Directory.GetDirectories(origen, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(Path.Combine(destino, Path.GetRelativePath(origen, d)));
    foreach (var f in Directory.GetFiles(origen, "*", SearchOption.AllDirectories))
    {
        var rel = Path.GetRelativePath(origen, f);
        if (rel.Equals("appsettings.Production.json", StringComparison.OrdinalIgnoreCase) && File.Exists(Path.Combine(destino, rel))) continue;
        File.Copy(f, Path.Combine(destino, rel), true);
    }
}

public class Ajustes
{
    public string Destino { get; set; } = @"C:\KonSolutions\Api";
    public bool Https { get; set; } = true;
    public int Puerto { get; set; } = 5001;
    public string Certificado { get; set; } = "";
    public string Pfx { get; set; } = "";
    public string ClavePfx { get; set; } = "";
    public string Conexion { get; set; } = "";
    public List<string> Origenes { get; set; } = new();
    public string Cuenta { get; set; } = "";
    public string ClaveCuenta { get; set; } = "";
    public bool Firewall { get; set; } = true;
    public bool Iniciar { get; set; } = true;
}
