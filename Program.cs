using System.Text;
using System.Threading.RateLimiting;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using KonSolutionsApi.Common;
using KonSolutionsApi.Infrastructure.Data;
using KonSolutionsApi.API.Middleware;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Application.Services;
using KonSolutionsApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Si corre como servicio de Windows (instalador de la API) toma su carpeta como raiz y avisa su estado al administrador de servicios; en consola o en VS no cambia nada.
builder.Host.UseWindowsService();

// El pool de hilos de .NET solo agrega ~1-2 hilos nuevos por segundo bajo presión.
// SqlConnectionFactory.CreateConnection() abre la conexión de forma síncrona (conn.Open()),
// lo que bloquea un hilo por cada llamada concurrente a la BD. Al arrancar el menú, el
// frontend dispara varias peticiones en paralelo (roles, botones, config, notificaciones,
// límites) y sin hilos "calientes" de sobra, se encolan esperando turno — no porque la BD
// sea lenta (los SPs corren instantáneos en SSMS), sino porque la API se queda sin hilos.
ThreadPool.SetMinThreads(200, 200);

// Evitar que el handler JWT remapee los nombres de claims al escribir el token:
// así el claim "nameid" queda tal cual y la auditoría lo lee correctamente (punto 9).
System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// Prioridad: variable de entorno KonSolutions_JWT_SECRET > appsettings.json (solo para desarrollo local).
// En producción, definir la variable de entorno y NUNCA dejar el secreto real en este archivo.
var secretKeyRaw = Environment.GetEnvironmentVariable("KonSolutions_JWT_SECRET");
if (string.IsNullOrWhiteSpace(secretKeyRaw)) secretKeyRaw = jwtSettings["SecretKey"];
if (string.IsNullOrWhiteSpace(secretKeyRaw))
    throw new InvalidOperationException(
        "Falta configurar la variable de entorno KonSolutions_JWT_SECRET (JwtSettings:SecretKey en appsettings.json queda vacío a propósito, nunca debe llevar el secreto real).");
var secretKey = secretKeyRaw;

// El guardián de permisos va como filtro GLOBAL, no como atributo por controlador: con 477
// controladores, el que alguien se olvide de poner queda sin protección y nadie se entera.
// Arranca en modo Auditoría (appsettings: "Permisos:Modo") — registra lo que rechazaría pero
// deja pasar, hasta que se revise qué entidades faltan configurar.
builder.Services.AddControllers(opciones =>
{
    opciones.Filters.Add<KonSolutionsApi.API.Filters.PermisoAuthorizationFilter>();
    // Descarta el caché de permisos cuando se tocan las tablas que lo alimentan, para que
    // un cambio de permisos se sienta al instante y no al vencer el TTL de 5 minutos.
    opciones.Filters.Add<KonSolutionsApi.API.Filters.InvalidacionPermisosFilter>();
    // 67/68: guardián de FLUJO, hermano del de permisos. A diferencia de aquel, sólo actúa
    // en los controladores que declaren [ExigeFlujo] — no es automático sobre los 477 porque
    // exige además [ExigePermiso] (de ahí saca la tabla) y que esa tabla tenga
    // COD_TIPO_ESTADO+COD_ESTADO. Se declara adonde se sabe que hace falta y se extiende
    // deliberadamente. Arranca en "Flujos:Modo" = Auditoria (appsettings), igual que Permisos.
    opciones.Filters.Add<KonSolutionsApi.API.Filters.FlujoAuthorizationFilter>();
})
// Regla: los FLG_ sin marcar se guardan como false, nunca NULL (ver Common/FlgFalseJson.cs).
.AddJsonOptions(o => o.JsonSerializerOptions.TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver { Modifiers = { FlgFalseJson.Modificador } });
builder.Services.AddHttpClient();

builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

// Compresión de respuestas. Los íconos del menú y de los favoritos viajan como byte[] que el
// serializador convierte a base64 dentro del JSON, y ese base64 agrega ~33% de peso sobre el
// binario: comprimir lo recupera casi entero. Se habilita también sobre HTTPS
// (EnableForHttps) porque toda la app corre bajo TLS; el riesgo teórico de BREACH no aplica
// acá: el token viaja en la cabecera Authorization, no en el cuerpo comprimido, y no hay
// secretos reflejados en las respuestas junto con entrada del atacante.
builder.Services.AddResponseCompression(opciones =>
{
    opciones.EnableForHttps = true;
    opciones.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
    opciones.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
    opciones.MimeTypes = Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults
        .MimeTypes.Concat(new[] { "application/json" });
});

// Control de cambios (refresco automático de grillas)
builder.Services.AddSingleton<KonSolutionsApi.Common.FlagDownloadProgress>();
builder.Services.AddSingleton<KonSolutionsApi.Infrastructure.Realtime.ControlCambiosStore>();
builder.Services.AddSingleton<KonSolutionsApi.Infrastructure.Realtime.LimitesColumnasStore>();
builder.Services.AddSingleton<KonSolutionsApi.Infrastructure.Realtime.SpTraceBuffer>();
builder.Services.AddHostedService<KonSolutionsApi.Infrastructure.Realtime.ControlCambiosBackgroundService>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KonSolutions Costos API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", Type = SecuritySchemeType.Http,
        Scheme = "Bearer", BearerFormat = "JWT", In = ParameterLocation.Header,
        Description = "Ingrese el token JWT precedido por 'Bearer '"
    });
    var bearerScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { bearerScheme, Array.Empty<string>() } });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // No remapear los nombres de claims entrantes: así "nameid" llega tal cual
        // y la auditoría puede leer el código de usuario de forma confiable (punto 9).
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, ValidateAudience = true,
            ValidateLifetime = true, ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"], ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            // Indicar explícitamente qué claim representa el nombre/identificador
            NameClaimType = "nameid"
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy =>
        policy.RequireAuthenticatedUser()
              .RequireAssertion(ctx =>
              {
                  // Criterio por PERFIL del usuario (FLG_SEGURIDAD o FLG_ADMINISTRADOR),
                  // no por nombre de rol — un usuario puede tener distintos roles
                  // operativos (SEG, ADM, etc.) pero su perfil es lo que determina
                  // si puede administrar el sistema (Seguridad, Logs, Configuración).
                  var seguridad = ctx.User.FindFirst("flgSeguridad")?.Value == "1";
                  var administrador = ctx.User.FindFirst("flgAdministrador")?.Value == "1";
                  return seguridad || administrador;
              }));
});

builder.Services.AddCors(opts =>
{
    // Desarrollo: permite cualquier origen para facilitar el trabajo local
    opts.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    // Producción: solo orígenes conocidos. Ajustar las URLs al desplegar.
    // Los origenes de la web (por donde se abre el sistema) se agregan por configuracion, sin recompilar: Cors:Origenes en appsettings.Production.json (lista)
    // o la variable de entorno KonSolutions_CORS_ORIGINS (separados por coma). Lo escribe el instalador de la API.
    var origenesProd = new List<string> { "https://localhost", "http://localhost", "https://localhost:7001", "http://localhost:5000" };
    origenesProd.AddRange(builder.Configuration.GetSection("Cors:Origenes").Get<string[]>() ?? Array.Empty<string>());
    origenesProd.AddRange((Environment.GetEnvironmentVariable("KonSolutions_CORS_ORIGINS") ?? "").Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    opts.AddPolicy("Production", p => p
        .WithOrigins(origenesProd.Select(o => o.TrimEnd('/')).Distinct(StringComparer.OrdinalIgnoreCase).ToArray())
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddSingleton<ErrorLogService>();
builder.Services.AddScoped<KonSolutionsApi.Application.Services.ActiveDirectoryService>();

// ── Registro de los 410 módulos (818 servicios) ──
builder.Services.AddScoped<IAccesosRegistroRepository, AccesosRegistroRepository>();
builder.Services.AddScoped<IAccesosRegistroService, AccesosRegistroService>();
builder.Services.AddScoped<IAccesosRepository, AccesosRepository>();
builder.Services.AddScoped<IAccesosService, AccesosService>();
builder.Services.AddScoped<IActivosEntidadesRepository, ActivosEntidadesRepository>();
builder.Services.AddScoped<IActivosEntidadesService, ActivosEntidadesService>();
builder.Services.AddScoped<IActivosEspecificacionesTecnicasRepository, ActivosEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IActivosEspecificacionesTecnicasService, ActivosEspecificacionesTecnicasService>();
builder.Services.AddScoped<IActivosRepository, ActivosRepository>();
builder.Services.AddScoped<IActivosService, ActivosService>();
builder.Services.AddScoped<IActivosTagRepository, ActivosTagRepository>();
builder.Services.AddScoped<IActivosTagService, ActivosTagService>();
builder.Services.AddScoped<IActivosValorizacionRepository, ActivosValorizacionRepository>();
builder.Services.AddScoped<IActivosValorizacionService, ActivosValorizacionService>();
builder.Services.AddScoped<IActivosZonificacionRepository, ActivosZonificacionRepository>();
builder.Services.AddScoped<IActivosZonificacionService, ActivosZonificacionService>();
builder.Services.AddScoped<IAfpsRepository, AfpsRepository>();
builder.Services.AddScoped<IAfpsService, AfpsService>();
builder.Services.AddScoped<IAlmacenesRepository, AlmacenesRepository>();
builder.Services.AddScoped<IAlmacenesService, AlmacenesService>();
builder.Services.AddScoped<IAlmacenesTiposMovimientoRepository, AlmacenesTiposMovimientoRepository>();
builder.Services.AddScoped<IAlmacenesTiposMovimientoService, AlmacenesTiposMovimientoService>();
builder.Services.AddScoped<IAlmacenesUbicacionesAlmacenRepository, AlmacenesUbicacionesAlmacenRepository>();
builder.Services.AddScoped<IAlmacenesUbicacionesAlmacenService, AlmacenesUbicacionesAlmacenService>();
builder.Services.AddScoped<IAprobacionesDetalleRepository, AprobacionesDetalleRepository>();
builder.Services.AddScoped<IAprobacionesDetalleService, AprobacionesDetalleService>();
builder.Services.AddScoped<IAprobacionesRepository, AprobacionesRepository>();
builder.Services.AddScoped<IAprobacionesService, AprobacionesService>();
builder.Services.AddScoped<IAreasRepository, AreasRepository>();
builder.Services.AddScoped<IAreasService, AreasService>();
builder.Services.AddScoped<IAsientosContablesRepository, AsientosContablesRepository>();
builder.Services.AddScoped<IAsientosContablesService, AsientosContablesService>();
builder.Services.AddScoped<IAutorizacionesEventosRepository, AutorizacionesEventosRepository>();
builder.Services.AddScoped<IAutorizacionesEventosService, AutorizacionesEventosService>();
builder.Services.AddScoped<IAutorizacionesPedidosComercialesRepository, AutorizacionesPedidosComercialesRepository>();
builder.Services.AddScoped<IAutorizacionesPedidosComercialesService, AutorizacionesPedidosComercialesService>();
builder.Services.AddScoped<IAutorizacionesRepository, AutorizacionesRepository>();
builder.Services.AddScoped<IAutorizacionesService, AutorizacionesService>();
builder.Services.AddScoped<IBancosCuentasBancariasRepository, BancosCuentasBancariasRepository>();
builder.Services.AddScoped<IBancosCuentasBancariasService, BancosCuentasBancariasService>();
builder.Services.AddScoped<IBancosRepository, BancosRepository>();
builder.Services.AddScoped<IBancosService, BancosService>();
builder.Services.AddScoped<IBeneficiosLaboralesRepository, BeneficiosLaboralesRepository>();
builder.Services.AddScoped<IBeneficiosLaboralesService, BeneficiosLaboralesService>();
builder.Services.AddScoped<ICajasRepository, CajasRepository>();
builder.Services.AddScoped<ICajasSaldosRepository, CajasSaldosRepository>();
builder.Services.AddScoped<ICajasSaldosService, CajasSaldosService>();
builder.Services.AddScoped<ICajasService, CajasService>();
builder.Services.AddScoped<ICanalesVentaRepository, CanalesVentaRepository>();
builder.Services.AddScoped<ICanalesVentaService, CanalesVentaService>();
builder.Services.AddScoped<ICargosAdministrativosCompetenciasRepository, CargosAdministrativosCompetenciasRepository>();
builder.Services.AddScoped<ICargosAdministrativosCompetenciasService, CargosAdministrativosCompetenciasService>();
builder.Services.AddScoped<ICargosFuncionesRepository, CargosFuncionesRepository>();
builder.Services.AddScoped<ICargosFuncionesService, CargosFuncionesService>();
builder.Services.AddScoped<ICargosImplementosRepository, CargosImplementosRepository>();
builder.Services.AddScoped<ICargosImplementosService, CargosImplementosService>();
builder.Services.AddScoped<ICargosRepository, CargosRepository>();
builder.Services.AddScoped<ICargosService, CargosService>();
builder.Services.AddScoped<ICarrerasAcademicasRepository, CarrerasAcademicasRepository>();
builder.Services.AddScoped<ICarrerasAcademicasService, CarrerasAcademicasService>();
builder.Services.AddScoped<ICatalogosPreciosDetalleComboRepository, CatalogosPreciosDetalleComboRepository>();
builder.Services.AddScoped<ICatalogosPreciosDetalleComboService, CatalogosPreciosDetalleComboService>();
builder.Services.AddScoped<ICatalogosPreciosDetalleRepository, CatalogosPreciosDetalleRepository>();
builder.Services.AddScoped<ICatalogosPreciosDetalleService, CatalogosPreciosDetalleService>();
builder.Services.AddScoped<ICatalogosPreciosRepository, CatalogosPreciosRepository>();
builder.Services.AddScoped<ICatalogosPreciosService, CatalogosPreciosService>();
builder.Services.AddScoped<ICategoriasComisionHistoricoRepository, CategoriasComisionHistoricoRepository>();
builder.Services.AddScoped<ICategoriasComisionHistoricoService, CategoriasComisionHistoricoService>();
builder.Services.AddScoped<ICategoriasComisionRepository, CategoriasComisionRepository>();
builder.Services.AddScoped<ICategoriasComisionService, CategoriasComisionService>();
builder.Services.AddScoped<ICategoriasComportamientoCostoRepository, CategoriasComportamientoCostoRepository>();
builder.Services.AddScoped<ICategoriasComportamientoCostoService, CategoriasComportamientoCostoService>();
builder.Services.AddScoped<ICategoriasFuncionCostoRepository, CategoriasFuncionCostoRepository>();
builder.Services.AddScoped<ICategoriasFuncionCostoService, CategoriasFuncionCostoService>();
builder.Services.AddScoped<ICategoriasOperacionProduccionRepository, CategoriasOperacionProduccionRepository>();
builder.Services.AddScoped<ICategoriasOperacionProduccionService, CategoriasOperacionProduccionService>();
builder.Services.AddScoped<ICentrosCostoRepository, CentrosCostoRepository>();
builder.Services.AddScoped<ICentrosCostoService, CentrosCostoService>();
builder.Services.AddScoped<IClasesCostoRepository, ClasesCostoRepository>();
builder.Services.AddScoped<IClasesCostoService, ClasesCostoService>();
builder.Services.AddScoped<IClasesDefectosTecnicosRepository, ClasesDefectosTecnicosRepository>();
builder.Services.AddScoped<IClasesDefectosTecnicosService, ClasesDefectosTecnicosService>();
builder.Services.AddScoped<IClasesElementoFormulaRepository, ClasesElementoFormulaRepository>();
builder.Services.AddScoped<IClasesElementoFormulaService, ClasesElementoFormulaService>();
builder.Services.AddScoped<IClasesElementosCostosRepository, ClasesElementosCostosRepository>();
builder.Services.AddScoped<IClasesElementosCostosService, ClasesElementosCostosService>();
builder.Services.AddScoped<IClasesMovimientoRepository, ClasesMovimientoRepository>();
builder.Services.AddScoped<IClasesMovimientoService, ClasesMovimientoService>();
builder.Services.AddScoped<IClasesOperacionProduccionRepository, ClasesOperacionProduccionRepository>();
builder.Services.AddScoped<IClasesOperacionProduccionService, ClasesOperacionProduccionService>();
builder.Services.AddScoped<IClasesDesarrolloRepository, ClasesDesarrolloRepository>();
builder.Services.AddScoped<IClasesDesarrolloService, ClasesDesarrolloService>();
builder.Services.AddScoped<ITiposDesarrolloRepository, TiposDesarrolloRepository>();
builder.Services.AddScoped<ITiposDesarrolloService, TiposDesarrolloService>();
builder.Services.AddScoped<ITiposUsoRepository, TiposUsoRepository>();
builder.Services.AddScoped<ITiposUsoService, TiposUsoService>();
builder.Services.AddScoped<IClasesOperacionRepository, ClasesOperacionRepository>();
builder.Services.AddScoped<IClasesOperacionService, ClasesOperacionService>();
builder.Services.AddScoped<IClasesProcesoRepository, ClasesProcesoRepository>();
builder.Services.AddScoped<IClasesProcesoService, ClasesProcesoService>();
builder.Services.AddScoped<IClasesProductosAlmacenesRepository, ClasesProductosAlmacenesRepository>();
builder.Services.AddScoped<IClasesProductosAlmacenesService, ClasesProductosAlmacenesService>();
builder.Services.AddScoped<IClasesProductosRepository, ClasesProductosRepository>();
builder.Services.AddScoped<IClasesProductosService, ClasesProductosService>();
builder.Services.AddScoped<ICompetenciasRepository, CompetenciasRepository>();
builder.Services.AddScoped<ICompetenciasService, CompetenciasService>();
builder.Services.AddScoped<IComponentesProductoRepository, ComponentesProductoRepository>();
builder.Services.AddScoped<IComponentesProductoService, ComponentesProductoService>();
builder.Services.AddScoped<IComprobantesPagoAplicadosRepository, ComprobantesPagoAplicadosRepository>();
builder.Services.AddScoped<IComprobantesPagoAplicadosService, ComprobantesPagoAplicadosService>();
builder.Services.AddScoped<IComprobantesPagoAsociadosRepository, ComprobantesPagoAsociadosRepository>();
builder.Services.AddScoped<IComprobantesPagoAsociadosService, ComprobantesPagoAsociadosService>();
builder.Services.AddScoped<IComprobantesPagoCalculoRepository, ComprobantesPagoCalculoRepository>();
builder.Services.AddScoped<IComprobantesPagoCalculoService, ComprobantesPagoCalculoService>();
builder.Services.AddScoped<IComprobantesPagoCronogramaCancelacionRepository, ComprobantesPagoCronogramaCancelacionRepository>();
builder.Services.AddScoped<IComprobantesPagoCronogramaCancelacionService, ComprobantesPagoCronogramaCancelacionService>();
builder.Services.AddScoped<IComprobantesPagoCronogramaRepository, ComprobantesPagoCronogramaRepository>();
builder.Services.AddScoped<IComprobantesPagoCronogramaService, ComprobantesPagoCronogramaService>();
builder.Services.AddScoped<IComprobantesPagoDetalleRepository, ComprobantesPagoDetalleRepository>();
builder.Services.AddScoped<IComprobantesPagoDetalleService, ComprobantesPagoDetalleService>();
builder.Services.AddScoped<IComprobantesPagoMovimientosAlmacenRepository, ComprobantesPagoMovimientosAlmacenRepository>();
builder.Services.AddScoped<IComprobantesPagoMovimientosAlmacenService, ComprobantesPagoMovimientosAlmacenService>();
builder.Services.AddScoped<IComprobantesPagoRepository, ComprobantesPagoRepository>();
builder.Services.AddScoped<IComprobantesPagoService, ComprobantesPagoService>();
builder.Services.AddScoped<IConceptosFinancierosRepository, ConceptosFinancierosRepository>();
builder.Services.AddScoped<IConceptosFinancierosService, ConceptosFinancierosService>();
builder.Services.AddScoped<IConceptosRemunerativosRepository, ConceptosRemunerativosRepository>();
builder.Services.AddScoped<IConceptosRemunerativosService, ConceptosRemunerativosService>();
builder.Services.AddScoped<ICondicionesPagoRepository, CondicionesPagoRepository>();
builder.Services.AddScoped<ICondicionesPagoService, CondicionesPagoService>();
builder.Services.AddScoped<IConfiguracionContableRepository, ConfiguracionContableRepository>();
builder.Services.AddScoped<IConfiguracionContableService, ConfiguracionContableService>();
builder.Services.AddScoped<IConfiguracionGeneralRepository, ConfiguracionGeneralRepository>();
builder.Services.AddScoped<IConfiguracionGeneralService, ConfiguracionGeneralService>();
builder.Services.AddScoped<IConfiguracionUsuarioRepository, ConfiguracionUsuarioRepository>();
builder.Services.AddScoped<IConfiguracionUsuarioService, ConfiguracionUsuarioService>();
builder.Services.AddScoped<IConfiguracionPedidosComercialesRepository, ConfiguracionPedidosComercialesRepository>();
builder.Services.AddScoped<IConfiguracionPedidosComercialesService, ConfiguracionPedidosComercialesService>();
builder.Services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
builder.Services.AddScoped<IConfiguracionRrhhNominaRepository, ConfiguracionRrhhNominaRepository>();
builder.Services.AddScoped<IConfiguracionRrhhNominaService, ConfiguracionRrhhNominaService>();
builder.Services.AddScoped<IConfiguracionSafetyRepository, ConfiguracionSafetyRepository>();
builder.Services.AddScoped<IConfiguracionSafetyService, ConfiguracionSafetyService>();
builder.Services.AddScoped<IConfiguracionService, ConfiguracionService>();
builder.Services.AddScoped<IContenedoresProduccionRepository, ContenedoresProduccionRepository>();
builder.Services.AddScoped<IContenedoresProduccionService, ContenedoresProduccionService>();
builder.Services.AddScoped<IContratosModeloRepository, ContratosModeloRepository>();
builder.Services.AddScoped<IContratosModeloService, ContratosModeloService>();
builder.Services.AddScoped<IControlCalidadMovimientosAlmacenEspecificacionesRepository, ControlCalidadMovimientosAlmacenEspecificacionesRepository>();
builder.Services.AddScoped<IControlCalidadMovimientosAlmacenEspecificacionesService, ControlCalidadMovimientosAlmacenEspecificacionesService>();
builder.Services.AddScoped<IControlCalidadMovimientosAlmacenRepository, ControlCalidadMovimientosAlmacenRepository>();
builder.Services.AddScoped<IControlCalidadMovimientosAlmacenService, ControlCalidadMovimientosAlmacenService>();
builder.Services.AddScoped<IControlCalidadProduccionEspecificacionesRepository, ControlCalidadProduccionEspecificacionesRepository>();
builder.Services.AddScoped<IControlCalidadProduccionEspecificacionesService, ControlCalidadProduccionEspecificacionesService>();
builder.Services.AddScoped<IControlCalidadProduccionRepository, ControlCalidadProduccionRepository>();
builder.Services.AddScoped<IControlCalidadProduccionService, ControlCalidadProduccionService>();
builder.Services.AddScoped<ICostoSubElementosCentroCostoRepository, CostoSubElementosCentroCostoRepository>();
builder.Services.AddScoped<ICostoSubElementosCentroCostoService, CostoSubElementosCentroCostoService>();
builder.Services.AddScoped<ICostosCentroCostosProcesosRepository, CostosCentroCostosProcesosRepository>();
builder.Services.AddScoped<ICostosCentroCostosProcesosService, CostosCentroCostosProcesosService>();
builder.Services.AddScoped<ICostosEntidadesTarifadoRepository, CostosEntidadesTarifadoRepository>();
builder.Services.AddScoped<ICostosEntidadesTarifadoService, CostosEntidadesTarifadoService>();
builder.Services.AddScoped<ICostosProcesoRepository, CostosProcesoRepository>();
builder.Services.AddScoped<ICostosProcesoService, CostosProcesoService>();
builder.Services.AddScoped<ICostosProduccionRepository, CostosProduccionRepository>();
builder.Services.AddScoped<ICostosProduccionService, CostosProduccionService>();
builder.Services.AddScoped<ICostosProductosRepository, CostosProductosRepository>();
builder.Services.AddScoped<ICostosProductosService, CostosProductosService>();
builder.Services.AddScoped<ICubsoSunatRepository, CubsoSunatRepository>();
builder.Services.AddScoped<ICubsoSunatService, CubsoSunatService>();
builder.Services.AddScoped<ICuentaCorrienteRepository, CuentaCorrienteRepository>();
builder.Services.AddScoped<ICuentaCorrienteService, CuentaCorrienteService>();
builder.Services.AddScoped<ICuentasContablesRepository, CuentasContablesRepository>();
builder.Services.AddScoped<ICuentasContablesService, CuentasContablesService>();
builder.Services.AddScoped<ICuentasNaturalezaRepository, CuentasNaturalezaRepository>();
builder.Services.AddScoped<ICuentasNaturalezaService, CuentasNaturalezaService>();
builder.Services.AddScoped<IDerechoHabientesRepository, DerechoHabientesRepository>();
builder.Services.AddScoped<IDerechoHabientesService, DerechoHabientesService>();
builder.Services.AddScoped<IDespachosRepository, DespachosRepository>();
builder.Services.AddScoped<IDespachosRutaRepository, DespachosRutaRepository>();
builder.Services.AddScoped<IDespachosRutaService, DespachosRutaService>();
builder.Services.AddScoped<IDespachosService, DespachosService>();
builder.Services.AddScoped<IDisenosProductoRepository, DisenosProductoRepository>();
builder.Services.AddScoped<IDisenosProductoService, DisenosProductoService>();
builder.Services.AddScoped<IDisenosRepository, DisenosRepository>();
builder.Services.AddScoped<IDisenosMemoriasRepository, DisenosMemoriasRepository>();
builder.Services.AddScoped<IDisenosMemoriasService, DisenosMemoriasService>();
builder.Services.AddScoped<IDisenosVersionesMemoriasRepository, DisenosVersionesMemoriasRepository>();
builder.Services.AddScoped<IDisenosVersionesMemoriasService, DisenosVersionesMemoriasService>();
builder.Services.AddScoped<IDisenosSectoresRepository, DisenosSectoresRepository>();
builder.Services.AddScoped<IDisenosSectoresService, DisenosSectoresService>();
builder.Services.AddScoped<IDisenosService, DisenosService>();
builder.Services.AddScoped<IDisenosVersionEspecificacionesTecnicasRepository, DisenosVersionEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IDisenosVersionEspecificacionesTecnicasService, DisenosVersionEspecificacionesTecnicasService>();
builder.Services.AddScoped<IDisenosVersionRepository, DisenosVersionRepository>();
builder.Services.AddScoped<IDisenosVersionSectorComponenteProductoFormulaRepository, DisenosVersionSectorComponenteProductoFormulaRepository>();
builder.Services.AddScoped<IDisenosVersionSectorComponenteProductoFormulaService, DisenosVersionSectorComponenteProductoFormulaService>();
builder.Services.AddScoped<IDisenosVersionSectorComponenteProductoRepository, DisenosVersionSectorComponenteProductoRepository>();
builder.Services.AddScoped<IDisenosVersionSectorComponenteProductoService, DisenosVersionSectorComponenteProductoService>();
builder.Services.AddScoped<IDisenosVersionSectorComponenteRepository, DisenosVersionSectorComponenteRepository>();
builder.Services.AddScoped<IDisenosVersionSectorComponenteService, DisenosVersionSectorComponenteService>();
builder.Services.AddScoped<IDisenosVersionService, DisenosVersionService>();
builder.Services.AddScoped<IDocumentosOficialesPersonalRepository, DocumentosOficialesPersonalRepository>();
builder.Services.AddScoped<IDocumentosOficialesPersonalService, DocumentosOficialesPersonalService>();
builder.Services.AddScoped<IDocumentosOficialesRepository, DocumentosOficialesRepository>();
builder.Services.AddScoped<IDocumentosOficialesService, DocumentosOficialesService>();
builder.Services.AddScoped<IElementosCostoRepository, ElementosCostoRepository>();
builder.Services.AddScoped<IElementosCostoService, ElementosCostoService>();
builder.Services.AddScoped<IEmbaladoEmbalajeDetalleRepository, EmbaladoEmbalajeDetalleRepository>();
builder.Services.AddScoped<IEmbaladoEmbalajeDetalleService, EmbaladoEmbalajeDetalleService>();
builder.Services.AddScoped<IEmbaladoEmbalajesRepository, EmbaladoEmbalajesRepository>();
builder.Services.AddScoped<IEmbaladoEmbalajesService, EmbaladoEmbalajesService>();
builder.Services.AddScoped<IEmbaladoRepository, EmbaladoRepository>();
builder.Services.AddScoped<IEmbaladoService, EmbaladoService>();
builder.Services.AddScoped<IEncuestasAreasRepository, EncuestasAreasRepository>();
builder.Services.AddScoped<IEncuestasAreasService, EncuestasAreasService>();
builder.Services.AddScoped<IEncuestasPersonalRepository, EncuestasPersonalRepository>();
builder.Services.AddScoped<IEncuestasPersonalRespuestasRepository, EncuestasPersonalRespuestasRepository>();
builder.Services.AddScoped<IEncuestasPersonalRespuestasService, EncuestasPersonalRespuestasService>();
builder.Services.AddScoped<IEncuestasPersonalService, EncuestasPersonalService>();
builder.Services.AddScoped<IEncuestasPreguntasRepository, EncuestasPreguntasRepository>();
builder.Services.AddScoped<IEncuestasPreguntasRespuestasRepository, EncuestasPreguntasRespuestasRepository>();
builder.Services.AddScoped<IEncuestasPreguntasRespuestasService, EncuestasPreguntasRespuestasService>();
builder.Services.AddScoped<IEncuestasPreguntasService, EncuestasPreguntasService>();
builder.Services.AddScoped<IEncuestasRepository, EncuestasRepository>();
builder.Services.AddScoped<IEncuestasService, EncuestasService>();
builder.Services.AddScoped<IEntidadesCuentasBancariasRepository, EntidadesCuentasBancariasRepository>();
builder.Services.AddScoped<IEntidadesCuentasBancariasService, EntidadesCuentasBancariasService>();
builder.Services.AddScoped<IEntidadesDireccionesRepository, EntidadesDireccionesRepository>();
builder.Services.AddScoped<IEntidadesDireccionesService, EntidadesDireccionesService>();
builder.Services.AddScoped<IEntidadesDocumentosIdentidadRepository, EntidadesDocumentosIdentidadRepository>();
builder.Services.AddScoped<IEntidadesDocumentosIdentidadService, EntidadesDocumentosIdentidadService>();
builder.Services.AddScoped<IEntidadesProductosRepository, EntidadesProductosRepository>();
builder.Services.AddScoped<IEntidadesProductosService, EntidadesProductosService>();
builder.Services.AddScoped<IEntidadesProductosTiposPreciosRepository, EntidadesProductosTiposPreciosRepository>();
builder.Services.AddScoped<IEntidadesProductosTiposPreciosService, EntidadesProductosTiposPreciosService>();
// Registro de Compras (Comercial/RegistroCompras): comprobantes de pago de proveedores, órdenes asociadas, validación e importación de XML
builder.Services.AddScoped<IRegistroComprasRepository, RegistroComprasRepository>();
builder.Services.AddScoped<IRegistroComprasService, RegistroComprasService>();

// Bandeja de XML recibidos (Finanzas/Logistica): baja de Microsoft 365 los XML y PDF de los comprobantes y guias de los proveedores y los carga (scripts 41 a 43).
// Corre solo, cada tantos minutos, si la carga automatica esta activa en Configuracion general > Compras > XML recibidos. La cuenta (TenantId, ClientId y Buzon) va en la seccion
// XmlRecibidos de appsettings.json; el secreto, en la variable de entorno KonSolutions_GRAPH_SECRET.
builder.Services.Configure<KonSolutionsApi.Domain.Models.Finanzas.XmlRecibidosOpciones>(builder.Configuration.GetSection("XmlRecibidos"));
builder.Services.AddSingleton<KonSolutionsApi.Infrastructure.XmlRecibidos.GraphTokenCache>();
builder.Services.AddHttpClient<IGraphCorreo, KonSolutionsApi.Infrastructure.XmlRecibidos.GraphCorreo>();
builder.Services.AddScoped<IXmlRecibidosRepository, XmlRecibidosRepository>();
builder.Services.AddScoped<IXmlRecibidosService, XmlRecibidosService>();
builder.Services.AddSingleton<XmlRecibidosEstado>();
// La lectura del correo ahora la hace el servicio de Windows KonSolutions.XmlRecibidos.Servicio (proyecto aparte). La API solo conserva los endpoints manuales. Descomenta para volver a leerlo desde la API (nunca con los dos a la vez):
// builder.Services.AddHostedService<KonSolutionsApi.Infrastructure.XmlRecibidos.XmlRecibidosBackgroundService>();
// ... y las pantallas de la bandeja (Traer de XML recibidos / Traer guia recibida)
builder.Services.AddScoped<XmlRecibidosArchivos>();
builder.Services.Configure<KonSolutionsApi.Infrastructure.Sunat.SunatOpciones>(builder.Configuration.GetSection("Sunat"));
builder.Services.AddHttpClient<KonSolutionsApi.Infrastructure.Sunat.ISunatValidador, KonSolutionsApi.Infrastructure.Sunat.SunatValidador>();
builder.Services.AddScoped<ICpeRecibidosRepository, CpeRecibidosRepository>();
builder.Services.AddScoped<ICpeRecibidosService, CpeRecibidosService>();
builder.Services.AddScoped<IGreRecibidasRepository, GreRecibidasRepository>();
builder.Services.AddScoped<IGreRecibidasService, GreRecibidasService>();
builder.Services.AddScoped<IPedidosComercialesRequerimientosRepository, PedidosComercialesRequerimientosRepository>();
builder.Services.AddScoped<IPedidosComercialesRequerimientosService, PedidosComercialesRequerimientosService>();
// FALTABAN. Sin estas dos, DI no puede construir
// PedidosComercialesRequerimientosStockLinkController —le pide el servicio y no existe—, y
// la excepción se dispara ANTES de entrar al controlador: por eso salía un 500 generico y no
// el 400 con mensaje que devuelve el servicio cuando algo falla adentro.
builder.Services.AddScoped<IPedidosComercialesRequerimientosStockLinkRepository, PedidosComercialesRequerimientosStockLinkRepository>();
builder.Services.AddScoped<IPedidosComercialesRequerimientosStockLinkService, PedidosComercialesRequerimientosStockLinkService>();
builder.Services.AddScoped<IPedidosComercialesComparativoRepository, PedidosComercialesComparativoRepository>();
builder.Services.AddScoped<IPedidosComercialesComparativoService, PedidosComercialesComparativoService>();
builder.Services.AddScoped<IPedidosComercialesEstadosRepository, PedidosComercialesEstadosRepository>();
builder.Services.AddScoped<IPedidosComercialesEstadosService, PedidosComercialesEstadosService>();
builder.Services.AddScoped<IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository, EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService, EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasService>();
builder.Services.AddScoped<IEntidadesPurcharseOrdersEspecificacionesDespachoRepository, EntidadesPurcharseOrdersEspecificacionesDespachoRepository>();
builder.Services.AddScoped<IEntidadesPurcharseOrdersEspecificacionesDespachoService, EntidadesPurcharseOrdersEspecificacionesDespachoService>();
builder.Services.AddScoped<IEntidadesPurcharseOrdersRepository, EntidadesPurcharseOrdersRepository>();
builder.Services.AddScoped<IEntidadesPurcharseOrdersService, EntidadesPurcharseOrdersService>();
builder.Services.AddScoped<IEntidadesRepository, EntidadesRepository>();
builder.Services.AddScoped<IEntidadesService, EntidadesService>();
builder.Services.AddScoped<IEntidadesUnidadesComercialesRepository, EntidadesUnidadesComercialesRepository>();
builder.Services.AddScoped<IEntidadesUnidadesComercialesService, EntidadesUnidadesComercialesService>();
builder.Services.AddScoped<IEntidadesTemporadasRepository, EntidadesTemporadasRepository>();
builder.Services.AddScoped<IEntidadesTemporadasService, EntidadesTemporadasService>();
builder.Services.AddScoped<IEntidadesTemporadasColeccionesRepository, EntidadesTemporadasColeccionesRepository>();
builder.Services.AddScoped<IEntidadesTemporadasColeccionesService, EntidadesTemporadasColeccionesService>();
builder.Services.AddScoped<ISolicitudesComercialesRepository, SolicitudesComercialesRepository>();
builder.Services.AddScoped<ISolicitudesComercialesService, SolicitudesComercialesService>();
builder.Services.AddScoped<ISolicitudesComercialesMemoriasRepository, SolicitudesComercialesMemoriasRepository>();
builder.Services.AddScoped<ISolicitudesComercialesMemoriasService, SolicitudesComercialesMemoriasService>();
builder.Services.AddScoped<ISolicitudesComercialesNotificacionesRepository, SolicitudesComercialesNotificacionesRepository>();
builder.Services.AddScoped<ISolicitudesComercialesNotificacionesService, SolicitudesComercialesNotificacionesService>();
builder.Services.AddScoped<ISolicitudesComercialesClienteRepository, SolicitudesComercialesClienteRepository>();
builder.Services.AddScoped<ISolicitudesComercialesClienteService, SolicitudesComercialesClienteService>();
builder.Services.AddScoped<ITiposSolicitudComercialRepository, TiposSolicitudComercialRepository>();
builder.Services.AddScoped<ITiposSolicitudComercialService, TiposSolicitudComercialService>();
builder.Services.AddScoped<ITiposSolicitudComercialAreasRepository, TiposSolicitudComercialAreasRepository>();
builder.Services.AddScoped<ITiposSolicitudComercialAreasService, TiposSolicitudComercialAreasService>();
builder.Services.AddScoped<ITiposSolicitudComercialAreasCargosRepository, TiposSolicitudComercialAreasCargosRepository>();
builder.Services.AddScoped<ITiposSolicitudComercialAreasCargosService, TiposSolicitudComercialAreasCargosService>();
builder.Services.AddScoped<IAreasCargosEntidadesRepository, AreasCargosEntidadesRepository>();
builder.Services.AddScoped<IAreasCargosEntidadesService, AreasCargosEntidadesService>();
builder.Services.AddScoped<IEntidadesTiposEntidadesRepository, EntidadesTiposEntidadesRepository>();
builder.Services.AddScoped<IEntidadesTiposEntidadesService, EntidadesTiposEntidadesService>();
builder.Services.AddScoped<IEntidadesInformacionComercialRepository, EntidadesInformacionComercialRepository>();
builder.Services.AddScoped<IEntidadesInformacionComercialService, EntidadesInformacionComercialService>();
builder.Services.AddScoped<IEntidadesAsesoresComercialesRepository, EntidadesAsesoresComercialesRepository>();
builder.Services.AddScoped<IEntidadesAsesoresComercialesService, EntidadesAsesoresComercialesService>();
builder.Services.AddScoped<IEntidadesTiposMedioContactoRepository, EntidadesTiposMedioContactoRepository>();
builder.Services.AddScoped<IEntidadesTiposMedioContactoService, EntidadesTiposMedioContactoService>();
builder.Services.AddScoped<IEspecificacionesTecnicasItemsRepository, EspecificacionesTecnicasItemsRepository>();
builder.Services.AddScoped<IEspecificacionesTecnicasItemsService, EspecificacionesTecnicasItemsService>();
builder.Services.AddScoped<IEspecificacionesTecnicasRepository, EspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IEspecificacionesTecnicasService, EspecificacionesTecnicasService>();
builder.Services.AddScoped<IEspecificacionesItemsTecnicosRepository, EspecificacionesItemsTecnicosRepository>();
builder.Services.AddScoped<IEspecificacionesItemsTecnicosService, EspecificacionesItemsTecnicosService>();
builder.Services.AddScoped<IOpcionesProcesosEstadosRepository, OpcionesProcesosEstadosRepository>();
builder.Services.AddScoped<IOpcionesProcesosEstadosService, OpcionesProcesosEstadosService>();
builder.Services.AddScoped<IOpcionesProcesosEstadosFlujosRepository, OpcionesProcesosEstadosFlujosRepository>();
builder.Services.AddScoped<IOpcionesProcesosEstadosFlujosService, OpcionesProcesosEstadosFlujosService>();
builder.Services.AddScoped<IOpcionesProcesosEstadosFlujosDependenciaRepository, OpcionesProcesosEstadosFlujosDependenciaRepository>();
builder.Services.AddScoped<IOpcionesProcesosEstadosFlujosDependenciaService, OpcionesProcesosEstadosFlujosDependenciaService>();
builder.Services.AddScoped<ICopiaEstadosRepository, CopiaEstadosRepository>();
builder.Services.AddScoped<ICopiaEstadosService, CopiaEstadosService>();
builder.Services.AddScoped<IMatrizTiposOperacionRepository, MatrizTiposOperacionRepository>();
builder.Services.AddScoped<IMatrizTiposOperacionService, MatrizTiposOperacionService>();
builder.Services.AddScoped<IDisenosEstructuraRepository, DisenosEstructuraRepository>();
builder.Services.AddScoped<IDisenosEstructuraService, DisenosEstructuraService>();
builder.Services.AddScoped<IDisenosEspecificacionesRepository, DisenosEspecificacionesRepository>();
builder.Services.AddScoped<IDisenosEspecificacionesService, DisenosEspecificacionesService>();
builder.Services.AddScoped<IDisenosTizadoRepository, DisenosTizadoRepository>();
builder.Services.AddScoped<IDisenosTizadoService, DisenosTizadoService>();
builder.Services.AddScoped<IOperacionesTiemposRepository, OperacionesTiemposRepository>();
builder.Services.AddScoped<IOperacionesTiemposService, OperacionesTiemposService>();
builder.Services.AddScoped<IMatricesOperacionesRepository, MatricesOperacionesRepository>();
builder.Services.AddScoped<IMatricesOperacionesService, MatricesOperacionesService>();
builder.Services.AddScoped<IFormulacionRepository, FormulacionRepository>();
builder.Services.AddScoped<IFormulacionService, FormulacionService>();
builder.Services.AddScoped<IPersonalLineasRepository, PersonalLineasRepository>();
builder.Services.AddScoped<IPersonalLineasService, PersonalLineasService>();
builder.Services.AddScoped<IOperacionesMigracionRepository, OperacionesMigracionRepository>();
builder.Services.AddScoped<IOperacionesMigracionService, OperacionesMigracionService>();
builder.Services.AddScoped<IEstadosCivilesRepository, EstadosCivilesRepository>();
builder.Services.AddScoped<IEstadosCivilesService, EstadosCivilesService>();
builder.Services.AddScoped<IEstadosEstandarRepository, EstadosEstandarRepository>();
builder.Services.AddScoped<IEstadosEstandarService, EstadosEstandarService>();
builder.Services.AddScoped<IEstadosRepository, EstadosRepository>();
builder.Services.AddScoped<IEstadosService, EstadosService>();
builder.Services.AddScoped<IEstructuraEspecificacionTecnicaRepository, EstructuraEspecificacionTecnicaRepository>();
builder.Services.AddScoped<IEstructuraEspecificacionTecnicaService, EstructuraEspecificacionTecnicaService>();
builder.Services.AddScoped<IEvaluacionesDesempenoCompetenciasRepository, EvaluacionesDesempenoCompetenciasRepository>();
builder.Services.AddScoped<IEvaluacionesDesempenoCompetenciasService, EvaluacionesDesempenoCompetenciasService>();
builder.Services.AddScoped<IEvaluacionesDesempenoMetricasRepository, EvaluacionesDesempenoMetricasRepository>();
builder.Services.AddScoped<IEvaluacionesDesempenoMetricasService, EvaluacionesDesempenoMetricasService>();
builder.Services.AddScoped<IEvaluacionesDesempenoPersonalEvaluacionRepository, EvaluacionesDesempenoPersonalEvaluacionRepository>();
builder.Services.AddScoped<IEvaluacionesDesempenoPersonalEvaluacionService, EvaluacionesDesempenoPersonalEvaluacionService>();
builder.Services.AddScoped<IEvaluacionesDesempenoPersonalRepository, EvaluacionesDesempenoPersonalRepository>();
builder.Services.AddScoped<IEvaluacionesDesempenoPersonalService, EvaluacionesDesempenoPersonalService>();
builder.Services.AddScoped<IEvaluacionesDesempenoRepository, EvaluacionesDesempenoRepository>();
builder.Services.AddScoped<IEvaluacionesDesempenoService, EvaluacionesDesempenoService>();
builder.Services.AddScoped<IEvaluacionesDesempenoTiposCompetenciasRepository, EvaluacionesDesempenoTiposCompetenciasRepository>();
builder.Services.AddScoped<IEvaluacionesDesempenoTiposCompetenciasService, EvaluacionesDesempenoTiposCompetenciasService>();
builder.Services.AddScoped<IFamiliasProductoRepository, FamiliasProductoRepository>();
builder.Services.AddScoped<IFamiliasProductoService, FamiliasProductoService>();
builder.Services.AddScoped<IFasesIncotermRepository, FasesIncotermRepository>();
builder.Services.AddScoped<IFasesIncotermService, FasesIncotermService>();
builder.Services.AddScoped<IFlujosProcesoDocumentacionRepository, FlujosProcesoDocumentacionRepository>();
builder.Services.AddScoped<IFlujosProcesoDocumentacionService, FlujosProcesoDocumentacionService>();
builder.Services.AddScoped<IFlujosProcesoEtapasDocumentacionRepository, FlujosProcesoEtapasDocumentacionRepository>();
builder.Services.AddScoped<IFlujosProcesoEtapasDocumentacionService, FlujosProcesoEtapasDocumentacionService>();
builder.Services.AddScoped<IFlujosProcesoEtapasNotificadosRepository, FlujosProcesoEtapasNotificadosRepository>();
builder.Services.AddScoped<IFlujosProcesoEtapasNotificadosService, FlujosProcesoEtapasNotificadosService>();
builder.Services.AddScoped<IFlujosProcesoEtapasRepository, FlujosProcesoEtapasRepository>();
builder.Services.AddScoped<IFlujosProcesoEtapasService, FlujosProcesoEtapasService>();
builder.Services.AddScoped<IFlujosProcesoMemoriaRepository, FlujosProcesoMemoriaRepository>();
builder.Services.AddScoped<IFlujosProcesoMemoriaService, FlujosProcesoMemoriaService>();
builder.Services.AddScoped<IFlujosProcesoRepository, FlujosProcesoRepository>();
builder.Services.AddScoped<IFlujosProcesoService, FlujosProcesoService>();
builder.Services.AddScoped<IFormasPagoRepository, FormasPagoRepository>();
builder.Services.AddScoped<IFormasPagoService, FormasPagoService>();
builder.Services.AddScoped<IGradosConfidenciabilidadRepository, GradosConfidenciabilidadRepository>();
builder.Services.AddScoped<IGradosConfidenciabilidadService, GradosConfidenciabilidadService>();
builder.Services.AddScoped<IGruposFamiliasSubFamiliasEspecificacionTecnicaRepository, GruposFamiliasSubFamiliasEspecificacionTecnicaRepository>();
builder.Services.AddScoped<IGruposFamiliasSubFamiliasEspecificacionTecnicaService, GruposFamiliasSubFamiliasEspecificacionTecnicaService>();
builder.Services.AddScoped<IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository, GruposFamiliasSubFamiliasGruposEspecificacionTecnicaRepository>();
builder.Services.AddScoped<IGruposFamiliasSubFamiliasGruposEspecificacionTecnicaService, GruposFamiliasSubFamiliasGruposEspecificacionTecnicaService>();
builder.Services.AddScoped<IGruposFamiliasSubFamiliasReglasCodificacionRepository, GruposFamiliasSubFamiliasReglasCodificacionRepository>();
builder.Services.AddScoped<IGruposFamiliasSubFamiliasReglasCodificacionService, GruposFamiliasSubFamiliasReglasCodificacionService>();
builder.Services.AddScoped<IGruposNetworkingAsesoresComerialesRepository, GruposNetworkingAsesoresComerialesRepository>();
builder.Services.AddScoped<IGruposNetworkingAsesoresComerialesService, GruposNetworkingAsesoresComerialesService>();
builder.Services.AddScoped<IGruposNetworkingRepository, GruposNetworkingRepository>();
builder.Services.AddScoped<IGruposNetworkingService, GruposNetworkingService>();
builder.Services.AddScoped<IGruposPlaneamientoRepository, GruposPlaneamientoRepository>();
builder.Services.AddScoped<IGruposPlaneamientoService, GruposPlaneamientoService>();
builder.Services.AddScoped<IGruposPlaneamientosPedidosDetalleRepository, GruposPlaneamientosPedidosDetalleRepository>();
builder.Services.AddScoped<IGruposPlaneamientosPedidosDetalleService, GruposPlaneamientosPedidosDetalleService>();
builder.Services.AddScoped<IGruposPlaneamientosPedidosRepository, GruposPlaneamientosPedidosRepository>();
builder.Services.AddScoped<IGruposPlaneamientosPedidosService, GruposPlaneamientosPedidosService>();
builder.Services.AddScoped<IGruposProductoRepository, GruposProductoRepository>();
builder.Services.AddScoped<IGruposProductoService, GruposProductoService>();
builder.Services.AddScoped<IGruposRequerimientoPedidosRepository, GruposRequerimientoPedidosRepository>();
builder.Services.AddScoped<IGruposRequerimientoPedidosService, GruposRequerimientoPedidosService>();
builder.Services.AddScoped<IGruposRequerimientoRepository, GruposRequerimientoRepository>();
builder.Services.AddScoped<IGruposRequerimientoService, GruposRequerimientoService>();
builder.Services.AddScoped<IHorariosDetalleRepository, HorariosDetalleRepository>();
builder.Services.AddScoped<IHorariosDetalleService, HorariosDetalleService>();
builder.Services.AddScoped<IHorariosRepository, HorariosRepository>();
builder.Services.AddScoped<IHorariosService, HorariosService>();
builder.Services.AddScoped<IIncotermsFasesRepository, IncotermsFasesRepository>();
builder.Services.AddScoped<IIncotermsFasesService, IncotermsFasesService>();
builder.Services.AddScoped<IIncotermsRepository, IncotermsRepository>();
builder.Services.AddScoped<IIncotermsService, IncotermsService>();
builder.Services.AddScoped<IIncotermsTiposTransporteRepository, IncotermsTiposTransporteRepository>();
builder.Services.AddScoped<IIncotermsTiposTransporteService, IncotermsTiposTransporteService>();
builder.Services.AddScoped<IInventariosAlmacenesProductosRepository, InventariosAlmacenesProductosRepository>();
builder.Services.AddScoped<IInventariosAlmacenesProductosService, InventariosAlmacenesProductosService>();
builder.Services.AddScoped<IInventariosAlmacenesRepository, InventariosAlmacenesRepository>();
builder.Services.AddScoped<IInventariosAlmacenesService, InventariosAlmacenesService>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaRepository, InventariosAlmacenesPuntoTomaRepository>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaService, InventariosAlmacenesPuntoTomaService>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaConteoRepository, InventariosAlmacenesPuntoTomaConteoRepository>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaConteoService, InventariosAlmacenesPuntoTomaConteoService>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaEquiposAsignadosRepository, InventariosAlmacenesPuntoTomaEquiposAsignadosRepository>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaEquiposAsignadosService, InventariosAlmacenesPuntoTomaEquiposAsignadosService>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaPersonalRepository, InventariosAlmacenesPuntoTomaPersonalRepository>();
builder.Services.AddScoped<IInventariosAlmacenesPuntoTomaPersonalService, InventariosAlmacenesPuntoTomaPersonalService>();
builder.Services.AddScoped<IInventariosAlmacenesImagenRepository, InventariosAlmacenesImagenRepository>();
builder.Services.AddScoped<IInventariosAlmacenesImagenService, InventariosAlmacenesImagenService>();
builder.Services.AddScoped<IInventariosAlmacenesUbicacionesRepository, InventariosAlmacenesUbicacionesRepository>();
builder.Services.AddScoped<IInventariosAlmacenesUbicacionesService, InventariosAlmacenesUbicacionesService>();
builder.Services.AddScoped<IInventariosImagenRepository, InventariosImagenRepository>();
builder.Services.AddScoped<IInventariosImagenService, InventariosImagenService>();
builder.Services.AddScoped<IInventariosRepository, InventariosRepository>();
builder.Services.AddScoped<IInventariosService, InventariosService>();
builder.Services.AddScoped<IInventarosAlmacenesInventariadoresRepository, InventarosAlmacenesInventariadoresRepository>();
builder.Services.AddScoped<IInventarosAlmacenesInventariadoresService, InventarosAlmacenesInventariadoresService>();
builder.Services.AddScoped<ILiquidacionesComisionVentaDetalleRepository, LiquidacionesComisionVentaDetalleRepository>();
builder.Services.AddScoped<ILiquidacionesComisionVentaDetalleService, LiquidacionesComisionVentaDetalleService>();
builder.Services.AddScoped<ILiquidacionesComisionVentaRepository, LiquidacionesComisionVentaRepository>();
builder.Services.AddScoped<ILiquidacionesComisionVentaService, LiquidacionesComisionVentaService>();
builder.Services.AddScoped<ILotesControlDetalleRepository, LotesControlDetalleRepository>();
builder.Services.AddScoped<ILotesControlDetalleService, LotesControlDetalleService>();
builder.Services.AddScoped<ILotesControlRepository, LotesControlRepository>();
builder.Services.AddScoped<ILotesControlService, LotesControlService>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleEntidadesRepository, MantenimientoIndustrialDetalleEntidadesRepository>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleEntidadesService, MantenimientoIndustrialDetalleEntidadesService>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleEspecificacionesTecnicasRepository, MantenimientoIndustrialDetalleEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleEspecificacionesTecnicasService, MantenimientoIndustrialDetalleEspecificacionesTecnicasService>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleRepository, MantenimientoIndustrialDetalleRepository>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleRequerimientosRepository, MantenimientoIndustrialDetalleRequerimientosRepository>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleRequerimientosService, MantenimientoIndustrialDetalleRequerimientosService>();
builder.Services.AddScoped<IMantenimientoIndustrialDetalleService, MantenimientoIndustrialDetalleService>();
builder.Services.AddScoped<IMantenimientoIndustrialRepository, MantenimientoIndustrialRepository>();
builder.Services.AddScoped<IMantenimientoIndustrialService, MantenimientoIndustrialService>();
builder.Services.AddScoped<IManualesFuncionesRepository, ManualesFuncionesRepository>();
builder.Services.AddScoped<IManualesFuncionesService, ManualesFuncionesService>();
builder.Services.AddScoped<IMarcacionAccesosRepository, MarcacionAccesosRepository>();
builder.Services.AddScoped<IMarcacionAccesosService, MarcacionAccesosService>();
builder.Services.AddScoped<IMarcasAccesosRepository, MarcasAccesosRepository>();
builder.Services.AddScoped<IMarcasAccesosService, MarcasAccesosService>();
builder.Services.AddScoped<IMensajesRepository, MensajesRepository>();
builder.Services.AddScoped<IMensajesService, MensajesService>();
builder.Services.AddScoped<IMonedasRepository, MonedasRepository>();
builder.Services.AddScoped<IMonedasService, MonedasService>();
builder.Services.AddScoped<IMonedasTipoCambioRepository, MonedasTipoCambioRepository>();
builder.Services.AddScoped<IMonedasTipoCambioService, MonedasTipoCambioService>();
builder.Services.AddScoped<IMotivosRepository, MotivosRepository>();
builder.Services.AddScoped<IMotivosService, MotivosService>();
builder.Services.AddScoped<IMovimientosAlmacenDetalleRepository, MovimientosAlmacenDetalleRepository>();
builder.Services.AddScoped<IMovimientosAlmacenDetalleService, MovimientosAlmacenDetalleService>();
builder.Services.AddScoped<IMovimientosAlmacenRepository, MovimientosAlmacenRepository>();
builder.Services.AddScoped<IMovimientosAlmacenService, MovimientosAlmacenService>();
builder.Services.AddScoped<IMovimientosAlmacenTransporteRepository, MovimientosAlmacenTransporteRepository>();
builder.Services.AddScoped<IMovimientosAlmacenTransporteService, MovimientosAlmacenTransporteService>();
builder.Services.AddScoped<IMovimientosAlmacenRequerimientosStockRepository, MovimientosAlmacenRequerimientosStockRepository>();
builder.Services.AddScoped<IMovimientosAlmacenRequerimientosStockService, MovimientosAlmacenRequerimientosStockService>();
builder.Services.AddScoped<IMovimientosAlmacenReservasStockRepository, MovimientosAlmacenReservasStockRepository>();
builder.Services.AddScoped<IMovimientosAlmacenReservasStockService, MovimientosAlmacenReservasStockService>();
builder.Services.AddScoped<IMovimientosAlmacenReservasStockDetalleRepository, MovimientosAlmacenReservasStockDetalleRepository>();
builder.Services.AddScoped<IMovimientosAlmacenReservasStockDetalleService, MovimientosAlmacenReservasStockDetalleService>();
builder.Services.AddScoped<IMovimientosAlmacenRequerimientosVistaRepository, MovimientosAlmacenRequerimientosVistaRepository>();
builder.Services.AddScoped<IMovimientosAlmacenRequerimientosVistaService, MovimientosAlmacenRequerimientosVistaService>();
builder.Services.AddScoped<IMovimientosAlmacenOrdenesCompraRepository, MovimientosAlmacenOrdenesCompraRepository>();
builder.Services.AddScoped<IMovimientosAlmacenOrdenesCompraService, MovimientosAlmacenOrdenesCompraService>();
builder.Services.AddScoped<IMovimientosAlmacenPedidosComercialesRepository, MovimientosAlmacenPedidosComercialesRepository>();
builder.Services.AddScoped<IMovimientosAlmacenPedidosComercialesService, MovimientosAlmacenPedidosComercialesService>();
builder.Services.AddScoped<IMovimientosAlmacenPedidosComercialesDetalleRepository, MovimientosAlmacenPedidosComercialesDetalleRepository>();
builder.Services.AddScoped<IMovimientosAlmacenPedidosComercialesDetalleService, MovimientosAlmacenPedidosComercialesDetalleService>();
builder.Services.AddScoped<IMovimientosContablesDetalleRepository, MovimientosContablesDetalleRepository>();
builder.Services.AddScoped<IMovimientosContablesDetalleService, MovimientosContablesDetalleService>();
builder.Services.AddScoped<IMovimientosContablesRepository, MovimientosContablesRepository>();
builder.Services.AddScoped<IMovimientosContablesService, MovimientosContablesService>();
builder.Services.AddScoped<INivelesEvaluacionRepository, NivelesEvaluacionRepository>();
builder.Services.AddScoped<INivelesEvaluacionService, NivelesEvaluacionService>();
builder.Services.AddScoped<INueveBoxDesempenoPotencialRepository, NueveBoxDesempenoPotencialRepository>();
builder.Services.AddScoped<INueveBoxDesempenoPotencialService, NueveBoxDesempenoPotencialService>();
builder.Services.AddScoped<IOperacionesCajaBancosDetalleCalculoRepository, OperacionesCajaBancosDetalleCalculoRepository>();
builder.Services.AddScoped<IOperacionesCajaBancosDetalleCalculoService, OperacionesCajaBancosDetalleCalculoService>();
builder.Services.AddScoped<IOperacionesCajaBancosDetalleRepository, OperacionesCajaBancosDetalleRepository>();
builder.Services.AddScoped<IOperacionesCajaBancosDetalleService, OperacionesCajaBancosDetalleService>();
builder.Services.AddScoped<IOperacionesCajaBancosRepository, OperacionesCajaBancosRepository>();
builder.Services.AddScoped<IOperacionesCajaBancosService, OperacionesCajaBancosService>();
builder.Services.AddScoped<IOrdenProduccionItemsRepository, OrdenProduccionItemsRepository>();
builder.Services.AddScoped<IOrdenProduccionItemsService, OrdenProduccionItemsService>();
builder.Services.AddScoped<IOrdenProduccionRepository, OrdenProduccionRepository>();
builder.Services.AddScoped<IOrdenProduccionService, OrdenProduccionService>();
builder.Services.AddScoped<IOrdenProduccionUnidadEspecificacionesRepository, OrdenProduccionUnidadEspecificacionesRepository>();
builder.Services.AddScoped<IOrdenProduccionUnidadEspecificacionesService, OrdenProduccionUnidadEspecificacionesService>();
builder.Services.AddScoped<IOrdenProduccionUnidadRepository, OrdenProduccionUnidadRepository>();
builder.Services.AddScoped<IOrdenProduccionUnidadService, OrdenProduccionUnidadService>();
builder.Services.AddScoped<IOrdenesProduccionUnidadesPedidosComercialesRepository, OrdenesProduccionUnidadesPedidosComercialesRepository>();
builder.Services.AddScoped<IOrdenesProduccionUnidadesPedidosComercialesService, OrdenesProduccionUnidadesPedidosComercialesService>();
builder.Services.AddScoped<IPaisesRepository, PaisesRepository>();
builder.Services.AddScoped<IPaisesService, PaisesService>();
builder.Services.AddScoped<IPedidosComercialesCalculoRepository, PedidosComercialesCalculoRepository>();
builder.Services.AddScoped<IPedidosComercialesCalculoService, PedidosComercialesCalculoService>();
builder.Services.AddScoped<IPedidosComercialesCronogramaEntregasRepository, PedidosComercialesCronogramaEntregasRepository>();
builder.Services.AddScoped<IPedidosComercialesCronogramaEntregasService, PedidosComercialesCronogramaEntregasService>();
builder.Services.AddScoped<IPedidosComercialesDetalleEspecificacionesTecnicasRepository, PedidosComercialesDetalleEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IPedidosComercialesDetalleEspecificacionesTecnicasService, PedidosComercialesDetalleEspecificacionesTecnicasService>();
builder.Services.AddScoped<IPedidosComercialesDetalleRepository, PedidosComercialesDetalleRepository>();
builder.Services.AddScoped<IPedidosComercialesDetalleService, PedidosComercialesDetalleService>();
builder.Services.AddScoped<IPedidosComercialesEspecificacionesTecnicasBaseRepository, PedidosComercialesEspecificacionesTecnicasBaseRepository>();
builder.Services.AddScoped<IPedidosComercialesEspecificacionesTecnicasBaseService, PedidosComercialesEspecificacionesTecnicasBaseService>();
builder.Services.AddScoped<IPedidosComercialesMovimientosAlmacenRepository, PedidosComercialesMovimientosAlmacenRepository>();
builder.Services.AddScoped<IPedidosComercialesMovimientosAlmacenService, PedidosComercialesMovimientosAlmacenService>();
builder.Services.AddScoped<IPedidosComercialesRepository, PedidosComercialesRepository>();
builder.Services.AddScoped<IPedidosComercialesSecuenciasEspecificacionesTecnicasRepository, PedidosComercialesSecuenciasEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IPedidosComercialesSecuenciasEspecificacionesTecnicasService, PedidosComercialesSecuenciasEspecificacionesTecnicasService>();
builder.Services.AddScoped<IPedidosComercialesSecuenciasRepository, PedidosComercialesSecuenciasRepository>();
builder.Services.AddScoped<IPedidosComercialesSecuenciasService, PedidosComercialesSecuenciasService>();
builder.Services.AddScoped<IPedidosComercialesService, PedidosComercialesService>();
builder.Services.AddScoped<IPedidosEspecificacionesDespachoEspecificacionesTecnicasRepository, PedidosEspecificacionesDespachoEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IPedidosEspecificacionesDespachoEspecificacionesTecnicasService, PedidosEspecificacionesDespachoEspecificacionesTecnicasService>();
builder.Services.AddScoped<IPedidosEspecificacionesDespachoRepository, PedidosEspecificacionesDespachoRepository>();
builder.Services.AddScoped<IPedidosEspecificacionesDespachoService, PedidosEspecificacionesDespachoService>();
builder.Services.AddScoped<IPeriodosRepository, PeriodosRepository>();
builder.Services.AddScoped<IPeriodosService, PeriodosService>();
builder.Services.AddScoped<IPersonalAfpConceptosRemunerativosRepository, PersonalAfpConceptosRemunerativosRepository>();
builder.Services.AddScoped<IPersonalAfpConceptosRemunerativosService, PersonalAfpConceptosRemunerativosService>();
builder.Services.AddScoped<IPersonalAfpRepository, PersonalAfpRepository>();
builder.Services.AddScoped<IPersonalAfpService, PersonalAfpService>();
builder.Services.AddScoped<IPersonalAsistenciaRepository, PersonalAsistenciaRepository>();
builder.Services.AddScoped<IPersonalAsistenciaService, PersonalAsistenciaService>();
builder.Services.AddScoped<IPersonalBeneficiariosRepository, PersonalBeneficiariosRepository>();
builder.Services.AddScoped<IPersonalBeneficiariosService, PersonalBeneficiariosService>();
builder.Services.AddScoped<IPersonalCargoRepository, PersonalCargoRepository>();
builder.Services.AddScoped<IPersonalCargoService, PersonalCargoService>();
builder.Services.AddScoped<IPersonalCompetenciasRepository, PersonalCompetenciasRepository>();
builder.Services.AddScoped<IPersonalCompetenciasService, PersonalCompetenciasService>();
builder.Services.AddScoped<IPersonalConceptosRemunerativosHistoricoRepository, PersonalConceptosRemunerativosHistoricoRepository>();
builder.Services.AddScoped<IPersonalConceptosRemunerativosHistoricoService, PersonalConceptosRemunerativosHistoricoService>();
builder.Services.AddScoped<IPersonalConceptosRemunerativosRepository, PersonalConceptosRemunerativosRepository>();
builder.Services.AddScoped<IPersonalConceptosRemunerativosService, PersonalConceptosRemunerativosService>();
builder.Services.AddScoped<IPersonalContactosRepository, PersonalContactosRepository>();
builder.Services.AddScoped<IPersonalContactosService, PersonalContactosService>();
builder.Services.AddScoped<IPersonalContratosRepository, PersonalContratosRepository>();
builder.Services.AddScoped<IPersonalContratosService, PersonalContratosService>();
builder.Services.AddScoped<IPersonalCuentaCorrienteDetalleRepository, PersonalCuentaCorrienteDetalleRepository>();
builder.Services.AddScoped<IPersonalCuentaCorrienteDetalleService, PersonalCuentaCorrienteDetalleService>();
builder.Services.AddScoped<IPersonalCuentaCorrienteRepository, PersonalCuentaCorrienteRepository>();
builder.Services.AddScoped<IPersonalCuentaCorrienteService, PersonalCuentaCorrienteService>();
builder.Services.AddScoped<IPersonalExperienciaLaboralRepository, PersonalExperienciaLaboralRepository>();
builder.Services.AddScoped<IPersonalExperienciaLaboralService, PersonalExperienciaLaboralService>();
builder.Services.AddScoped<IPersonalFormacionAcademicaRepository, PersonalFormacionAcademicaRepository>();
builder.Services.AddScoped<IPersonalFormacionAcademicaService, PersonalFormacionAcademicaService>();
builder.Services.AddScoped<IPersonalHorariosRepository, PersonalHorariosRepository>();
builder.Services.AddScoped<IPersonalHorariosService, PersonalHorariosService>();
builder.Services.AddScoped<IPersonalImplementosRepository, PersonalImplementosRepository>();
builder.Services.AddScoped<IPersonalImplementosService, PersonalImplementosService>();
builder.Services.AddScoped<IPersonalMarcacionesRepository, PersonalMarcacionesRepository>();
builder.Services.AddScoped<IPersonalMarcacionesService, PersonalMarcacionesService>();
builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();
builder.Services.AddScoped<IPersonalService, PersonalService>();
builder.Services.AddScoped<IPersonalTiposPersonalRepository, PersonalTiposPersonalRepository>();
builder.Services.AddScoped<IPersonalTiposPersonalService, PersonalTiposPersonalService>();
builder.Services.AddScoped<IPlaneamientoConfiguracionRepository, PlaneamientoConfiguracionRepository>();
builder.Services.AddScoped<IPlaneamientoConfiguracionService, PlaneamientoConfiguracionService>();
builder.Services.AddScoped<IPlanillaRemuneracionesRepository, PlanillaRemuneracionesRepository>();
builder.Services.AddScoped<IPlanillaRemuneracionesService, PlanillaRemuneracionesService>();
builder.Services.AddScoped<IPlanillasRemuneracionesDetalleRepository, PlanillasRemuneracionesDetalleRepository>();
builder.Services.AddScoped<IPlanillasRemuneracionesDetalleService, PlanillasRemuneracionesDetalleService>();
builder.Services.AddScoped<IPlanillasRemuneracionesFormulasRepository, PlanillasRemuneracionesFormulasRepository>();
builder.Services.AddScoped<IPlanillasRemuneracionesFormulasService, PlanillasRemuneracionesFormulasService>();
builder.Services.AddScoped<IPlanillasRemuneracionesPersonalRepository, PlanillasRemuneracionesPersonalRepository>();
builder.Services.AddScoped<IPlanillasRemuneracionesPersonalService, PlanillasRemuneracionesPersonalService>();
builder.Services.AddScoped<IPrioridadesRepository, PrioridadesRepository>();
builder.Services.AddScoped<IPrioridadesService, PrioridadesService>();
builder.Services.AddScoped<IProcesosContablesRepository, ProcesosContablesRepository>();
builder.Services.AddScoped<IProcesosContablesService, ProcesosContablesService>();
builder.Services.AddScoped<IProcesosLineasActivosRepository, ProcesosLineasActivosRepository>();
builder.Services.AddScoped<IProcesosLineasActivosService, ProcesosLineasActivosService>();
builder.Services.AddScoped<IProcesosLineasEspecificacionesTecnicasRepository, ProcesosLineasEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IProcesosLineasEspecificacionesTecnicasService, ProcesosLineasEspecificacionesTecnicasService>();
builder.Services.AddScoped<IProcesosLineasRepository, ProcesosLineasRepository>();
builder.Services.AddScoped<IProcesosLineasService, ProcesosLineasService>();
builder.Services.AddScoped<IProcesosRepository, ProcesosRepository>();
builder.Services.AddScoped<IProcesosService, ProcesosService>();
builder.Services.AddScoped<IProduccionRutasOperacionesEstructuraRepository, ProduccionRutasOperacionesEstructuraRepository>();
builder.Services.AddScoped<IProduccionRutasOperacionesEstructuraService, ProduccionRutasOperacionesEstructuraService>();
builder.Services.AddScoped<IProduccionRutasRepository, ProduccionRutasRepository>();
builder.Services.AddScoped<IProduccionRutasService, ProduccionRutasService>();
builder.Services.AddScoped<IProductosAlmacenesRepository, ProductosAlmacenesRepository>();
builder.Services.AddScoped<IProductosAlmacenesService, ProductosAlmacenesService>();
builder.Services.AddScoped<IProductosEspecificacionesTecnicasRepository, ProductosEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<IProductosEspecificacionesTecnicasService, ProductosEspecificacionesTecnicasService>();
builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<IProductosService, ProductosService>();
builder.Services.AddScoped<IProductosTagsRepository, ProductosTagsRepository>();
builder.Services.AddScoped<IProductosTagsService, ProductosTagsService>();
builder.Services.AddScoped<IProductosTiposPrecioRepository, ProductosTiposPrecioRepository>();
builder.Services.AddScoped<IProductosTiposPrecioService, ProductosTiposPrecioService>();
builder.Services.AddScoped<IProgramacionEntregaImplementosPersonalDetalleRepository, ProgramacionEntregaImplementosPersonalDetalleRepository>();
builder.Services.AddScoped<IProgramacionEntregaImplementosPersonalDetalleService, ProgramacionEntregaImplementosPersonalDetalleService>();
builder.Services.AddScoped<IProgramacionEntregaImplementosPersonalRepository, ProgramacionEntregaImplementosPersonalRepository>();
builder.Services.AddScoped<IProgramacionEntregaImplementosPersonalService, ProgramacionEntregaImplementosPersonalService>();
builder.Services.AddScoped<IProgramacionEntregaImplementosRepository, ProgramacionEntregaImplementosRepository>();
builder.Services.AddScoped<IProgramacionEntregaImplementosService, ProgramacionEntregaImplementosService>();
builder.Services.AddScoped<IProgramacionLineasProduccionRepository, ProgramacionLineasProduccionRepository>();
builder.Services.AddScoped<IProgramacionLineasProduccionService, ProgramacionLineasProduccionService>();
builder.Services.AddScoped<IProgramacionRepository, ProgramacionRepository>();
builder.Services.AddScoped<IProgramacionService, ProgramacionService>();
builder.Services.AddScoped<IProgramacionVacacionesRepository, ProgramacionVacacionesRepository>();
builder.Services.AddScoped<IProgramacionVacacionesService, ProgramacionVacacionesService>();
builder.Services.AddScoped<IProgramacionVersionDetalleRepository, ProgramacionVersionDetalleRepository>();
builder.Services.AddScoped<IProgramacionVersionDetalleService, ProgramacionVersionDetalleService>();
builder.Services.AddScoped<IProgramacionVersionRepository, ProgramacionVersionRepository>();
builder.Services.AddScoped<IProgramacionVersionService, ProgramacionVersionService>();
builder.Services.AddScoped<IProyectosArchivosRepository, ProyectosArchivosRepository>();
builder.Services.AddScoped<IProyectosArchivosService, ProyectosArchivosService>();
builder.Services.AddScoped<IProyectosFasesRepository, ProyectosFasesRepository>();
builder.Services.AddScoped<IProyectosFasesService, ProyectosFasesService>();
builder.Services.AddScoped<IProyectosGastosRepository, ProyectosGastosRepository>();
builder.Services.AddScoped<IProyectosGastosService, ProyectosGastosService>();
builder.Services.AddScoped<IProyectosHitosRepository, ProyectosHitosRepository>();
builder.Services.AddScoped<IProyectosHitosService, ProyectosHitosService>();
builder.Services.AddScoped<IProyectosImpactoRiesgoRepository, ProyectosImpactoRiesgoRepository>();
builder.Services.AddScoped<IProyectosImpactoRiesgoService, ProyectosImpactoRiesgoService>();
builder.Services.AddScoped<IProyectosMemoriaRepository, ProyectosMemoriaRepository>();
builder.Services.AddScoped<IProyectosMemoriaService, ProyectosMemoriaService>();
builder.Services.AddScoped<IProyectosPlanesMitigacionRiesgosRepository, ProyectosPlanesMitigacionRiesgosRepository>();
builder.Services.AddScoped<IProyectosPlanesMitigacionRiesgosService, ProyectosPlanesMitigacionRiesgosService>();
builder.Services.AddScoped<IProyectosProbabilidadRiesgoRepository, ProyectosProbabilidadRiesgoRepository>();
builder.Services.AddScoped<IProyectosProbabilidadRiesgoService, ProyectosProbabilidadRiesgoService>();
builder.Services.AddScoped<IProyectosRecursoHumanoAsignadoRepository, ProyectosRecursoHumanoAsignadoRepository>();
builder.Services.AddScoped<IProyectosRecursoHumanoAsignadoService, ProyectosRecursoHumanoAsignadoService>();
builder.Services.AddScoped<IProyectosRepository, ProyectosRepository>();
builder.Services.AddScoped<IProyectosRiesgosRepository, ProyectosRiesgosRepository>();
builder.Services.AddScoped<IProyectosRiesgosService, ProyectosRiesgosService>();
builder.Services.AddScoped<IProyectosRolesRepository, ProyectosRolesRepository>();
builder.Services.AddScoped<IProyectosRolesService, ProyectosRolesService>();
builder.Services.AddScoped<IProyectosRolesCategoriasRepository, ProyectosRolesCategoriasRepository>();
builder.Services.AddScoped<IProyectosRolesCategoriasService, ProyectosRolesCategoriasService>();
builder.Services.AddScoped<ITiposDocumentoCompromisoRepository, TiposDocumentoCompromisoRepository>();
builder.Services.AddScoped<ITiposDocumentoCompromisoService, TiposDocumentoCompromisoService>();
builder.Services.AddScoped<IPlantillasDocumentoRepository, PlantillasDocumentoRepository>();
builder.Services.AddScoped<IPlantillasDocumentoService, PlantillasDocumentoService>();
builder.Services.AddScoped<IProyectosDocumentosEmitidosRepository, ProyectosDocumentosEmitidosRepository>();
builder.Services.AddScoped<IProyectosDocumentosEmitidosService, ProyectosDocumentosEmitidosService>();
builder.Services.AddScoped<IProyectosParametrosRepository, ProyectosParametrosRepository>();
builder.Services.AddScoped<IProyectosParametrosService, ProyectosParametrosService>();
builder.Services.AddScoped<IFeriadosRepository, FeriadosRepository>();
builder.Services.AddScoped<IFeriadosService, FeriadosService>();
builder.Services.AddScoped<IProyectosService, ProyectosService>();
builder.Services.AddScoped<IProyectosSubTareasRepository, ProyectosSubTareasRepository>();
builder.Services.AddScoped<IProyectosMaterialesRepository, ProyectosMaterialesRepository>();
builder.Services.AddScoped<IProyectosMaterialesService, ProyectosMaterialesService>();
builder.Services.AddScoped<IProyectosGastosAdicionalesRepository, ProyectosGastosAdicionalesRepository>();
builder.Services.AddScoped<IProyectosGastosAdicionalesService, ProyectosGastosAdicionalesService>();
builder.Services.AddScoped<IProyectosExpedienteRepository, ProyectosExpedienteRepository>();
builder.Services.AddScoped<IProyectosExpedienteService, ProyectosExpedienteService>();
builder.Services.AddScoped<IProyectosSubTareasService, ProyectosSubTareasService>();
builder.Services.AddScoped<IProyectosTareasRepository, ProyectosTareasRepository>();
builder.Services.AddScoped<IProyectosTareasService, ProyectosTareasService>();
builder.Services.AddScoped<IProyectosTiposEntidadRepository, ProyectosTiposEntidadRepository>();
builder.Services.AddScoped<IProyectosTiposEntidadService, ProyectosTiposEntidadService>();
builder.Services.AddScoped<IProyectosUnidadesMedidaTareasRepository, ProyectosUnidadesMedidaTareasRepository>();
builder.Services.AddScoped<IProyectosUnidadesMedidaTareasService, ProyectosUnidadesMedidaTareasService>();
builder.Services.AddScoped<IRankingAcademicoRepository, RankingAcademicoRepository>();
builder.Services.AddScoped<IRankingAcademicoService, RankingAcademicoService>();
builder.Services.AddScoped<IRegimenesLaboralesRepository, RegimenesLaboralesRepository>();
builder.Services.AddScoped<IRegimenesLaboralesService, RegimenesLaboralesService>();
builder.Services.AddScoped<IRegimenesTributariosRepository, RegimenesTributariosRepository>();
builder.Services.AddScoped<IRegimenesTributariosService, RegimenesTributariosService>();
builder.Services.AddScoped<IRegistroProduccionRepository, RegistroProduccionRepository>();
builder.Services.AddScoped<IRegistroProduccionService, RegistroProduccionService>();
builder.Services.AddScoped<IRegistroProduccionTiposOperacionRepository, RegistroProduccionTiposOperacionRepository>();
builder.Services.AddScoped<IRegistroProduccionTiposOperacionService, RegistroProduccionTiposOperacionService>();
builder.Services.AddScoped<IReglasComboRepository, ReglasComboRepository>();
builder.Services.AddScoped<IReglasComboService, ReglasComboService>();
builder.Services.AddScoped<IReglasComboSubCanalesRepository, ReglasComboSubCanalesRepository>();
builder.Services.AddScoped<IReglasComboSubCanalesService, ReglasComboSubCanalesService>();
builder.Services.AddScoped<IRendicionCuentasDetalleRepository, RendicionCuentasDetalleRepository>();
builder.Services.AddScoped<IRendicionCuentasDetalleService, RendicionCuentasDetalleService>();
builder.Services.AddScoped<IRendicionCuentasRepository, RendicionCuentasRepository>();
builder.Services.AddScoped<IRendicionCuentasService, RendicionCuentasService>();
builder.Services.AddScoped<IRequerimientosStockDetalleRepository, RequerimientosStockDetalleRepository>();
builder.Services.AddScoped<IRequerimientosStockDetalleService, RequerimientosStockDetalleService>();
builder.Services.AddScoped<IRequerimientosStockRepository, RequerimientosStockRepository>();
builder.Services.AddScoped<IRequerimientosStockService, RequerimientosStockService>();
builder.Services.AddScoped<IReservasStockDetalleRepository, ReservasStockDetalleRepository>();
builder.Services.AddScoped<IReservasStockDetalleService, ReservasStockDetalleService>();
builder.Services.AddScoped<IReservasStockRepository, ReservasStockRepository>();
builder.Services.AddScoped<IReservasStockService, ReservasStockService>();
builder.Services.AddScoped<IRolesUsuarioUsuariosRepository, RolesUsuarioUsuariosRepository>();
builder.Services.AddScoped<IRolesUsuarioUsuariosService, RolesUsuarioUsuariosService>();
builder.Services.AddScoped<IRolesUsuariosAplicacionesOpcionesBotonesRepository, RolesUsuariosAplicacionesOpcionesBotonesRepository>();
builder.Services.AddScoped<IRolesUsuariosAplicacionesOpcionesBotonesService, RolesUsuariosAplicacionesOpcionesBotonesService>();
builder.Services.AddScoped<IRolesUsuariosAplicacionesOpcionesRepository, RolesUsuariosAplicacionesOpcionesRepository>();
builder.Services.AddScoped<IRolesUsuariosAplicacionesOpcionesService, RolesUsuariosAplicacionesOpcionesService>();
builder.Services.AddScoped<IRutasArchivosRepository, RutasArchivosRepository>();
builder.Services.AddScoped<IRutasArchivosService, RutasArchivosService>();
builder.Services.AddScoped<ISaldosContablesRepository, SaldosContablesRepository>();
builder.Services.AddScoped<ISaldosContablesService, SaldosContablesService>();
builder.Services.AddScoped<ISaldosCuentaCorrienteRepository, SaldosCuentaCorrienteRepository>();
builder.Services.AddScoped<ISaldosCuentaCorrienteService, SaldosCuentaCorrienteService>();
builder.Services.AddScoped<ISexosRepository, SexosRepository>();
builder.Services.AddScoped<ISexosService, SexosService>();
builder.Services.AddScoped<ITiposPersonasRepository, TiposPersonasRepository>();
builder.Services.AddScoped<ITiposPersonasService, TiposPersonasService>();
builder.Services.AddScoped<IAnosRepository, AnosRepository>();
builder.Services.AddScoped<IAnosService, AnosService>();
builder.Services.AddScoped<IMesesRepository, MesesRepository>();
builder.Services.AddScoped<IMesesService, MesesService>();
builder.Services.AddScoped<ISolicitudesPersonalRepository, SolicitudesPersonalRepository>();
builder.Services.AddScoped<ISolicitudesPersonalService, SolicitudesPersonalService>();
builder.Services.AddScoped<ISolicitudesReclutamientoCompetenciasRepository, SolicitudesReclutamientoCompetenciasRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoCompetenciasService, SolicitudesReclutamientoCompetenciasService>();
builder.Services.AddScoped<ISolicitudesReclutamientoFuncionesRepository, SolicitudesReclutamientoFuncionesRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoFuncionesService, SolicitudesReclutamientoFuncionesService>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesEvaluacionCompetenciasRepository, SolicitudesReclutamientoPostulantesEvaluacionCompetenciasRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesEvaluacionCompetenciasService, SolicitudesReclutamientoPostulantesEvaluacionCompetenciasService>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesFormacionAcademicaRepository, SolicitudesReclutamientoPostulantesFormacionAcademicaRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesFormacionAcademicaService, SolicitudesReclutamientoPostulantesFormacionAcademicaService>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesRepository, SolicitudesReclutamientoPostulantesRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesService, SolicitudesReclutamientoPostulantesService>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesTiposEvaluacionRepository, SolicitudesReclutamientoPostulantesTiposEvaluacionRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoPostulantesTiposEvaluacionService, SolicitudesReclutamientoPostulantesTiposEvaluacionService>();
builder.Services.AddScoped<ISolicitudesReclutamientoRepository, SolicitudesReclutamientoRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoService, SolicitudesReclutamientoService>();
builder.Services.AddScoped<ISolicitudesReclutamientoTiposEvaluacionRepository, SolicitudesReclutamientoTiposEvaluacionRepository>();
builder.Services.AddScoped<ISolicitudesReclutamientoTiposEvaluacionService, SolicitudesReclutamientoTiposEvaluacionService>();
builder.Services.AddScoped<ISolicitudesRepository, SolicitudesRepository>();
builder.Services.AddScoped<ISolicitudesService, SolicitudesService>();
builder.Services.AddScoped<IStocksAlmacenUbicacionRepository, StocksAlmacenUbicacionRepository>();
builder.Services.AddScoped<IStocksAlmacenUbicacionService, StocksAlmacenUbicacionService>();
builder.Services.AddScoped<ITrazabilidadRepository, TrazabilidadRepository>();
builder.Services.AddScoped<ITrazabilidadService, TrazabilidadService>();
builder.Services.AddScoped<ITrasladosUbicacionesRepository, TrasladosUbicacionesRepository>();
builder.Services.AddScoped<ITrasladosUbicacionesService, TrasladosUbicacionesService>();
builder.Services.AddScoped<IConfirmacionRecepcionRepository, ConfirmacionRecepcionRepository>();
builder.Services.AddScoped<IConfirmacionRecepcionService, ConfirmacionRecepcionService>();
builder.Services.AddScoped<IReportesRepository, ReportesRepository>();
builder.Services.AddScoped<IReportesService, ReportesService>();
builder.Services.AddScoped<IStoresProceduresRepository, StoresProceduresRepository>();
builder.Services.AddScoped<IStoresProceduresService, StoresProceduresService>();
builder.Services.AddScoped<ISubCanalesVentaProductosFijosRepository, SubCanalesVentaProductosFijosRepository>();
builder.Services.AddScoped<ISubCanalesVentaProductosFijosService, SubCanalesVentaProductosFijosService>();
builder.Services.AddScoped<ISubCanalesVentaRepository, SubCanalesVentaRepository>();
builder.Services.AddScoped<ISubCanalesVentaService, SubCanalesVentaService>();
builder.Services.AddScoped<ISubElementosCostoRepository, SubElementosCostoRepository>();
builder.Services.AddScoped<ISubElementosCostoService, SubElementosCostoService>();
builder.Services.AddScoped<ISubFamiliasProductoRepository, SubFamiliasProductoRepository>();
builder.Services.AddScoped<ISubFamiliasProductoService, SubFamiliasProductoService>();
builder.Services.AddScoped<ISubProcesosRepository, SubProcesosRepository>();
builder.Services.AddScoped<ISubProcesosService, SubProcesosService>();
builder.Services.AddScoped<ISucursalesRepository, SucursalesRepository>();
builder.Services.AddScoped<ISucursalesSerieComprobanteRepository, SucursalesSerieComprobanteRepository>();
builder.Services.AddScoped<ISucursalesSerieComprobanteService, SucursalesSerieComprobanteService>();
builder.Services.AddScoped<ISucursalesSerieComprobantesAplicadasRepository, SucursalesSerieComprobantesAplicadasRepository>();
builder.Services.AddScoped<ISucursalesSerieComprobantesAplicadasService, SucursalesSerieComprobantesAplicadasService>();
builder.Services.AddScoped<ISucursalesService, SucursalesService>();
builder.Services.AddScoped<ISucursalesTiposComprobantePagoRepository, SucursalesTiposComprobantePagoRepository>();
builder.Services.AddScoped<ISucursalesTiposComprobantePagoService, SucursalesTiposComprobantePagoService>();
builder.Services.AddScoped<ITablasCodigosRepository, TablasCodigosRepository>();
builder.Services.AddScoped<ITablasCodigosService, TablasCodigosService>();
builder.Services.AddScoped<ITablasEspecificacionesTecnicasRepository, TablasEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<ITablasEspecificacionesTecnicasService, TablasEspecificacionesTecnicasService>();
builder.Services.AddScoped<ITareasRepository, TareasRepository>();
builder.Services.AddScoped<ITareasService, TareasService>();
builder.Services.AddScoped<ITicketsAtencionDetallePasosRepository, TicketsAtencionDetallePasosRepository>();
builder.Services.AddScoped<ITicketsAtencionDetallePasosService, TicketsAtencionDetallePasosService>();
builder.Services.AddScoped<ITicketsAtencionDetalleRepository, TicketsAtencionDetalleRepository>();
builder.Services.AddScoped<ITicketsAtencionDetalleService, TicketsAtencionDetalleService>();
builder.Services.AddScoped<ITicketsAtencionRepository, TicketsAtencionRepository>();
builder.Services.AddScoped<ITicketsAtencionService, TicketsAtencionService>();
builder.Services.AddScoped<ITiposActivoRepository, TiposActivoRepository>();
builder.Services.AddScoped<ITiposActivoService, TiposActivoService>();
builder.Services.AddScoped<IContenedoresAlmacenajeRepository, ContenedoresAlmacenajeRepository>();
builder.Services.AddScoped<IContenedoresAlmacenajeService, ContenedoresAlmacenajeService>();
builder.Services.AddScoped<IContenedoresAlmacenajeCasillerosRepository, ContenedoresAlmacenajeCasillerosRepository>();
builder.Services.AddScoped<IContenedoresAlmacenajeCasillerosService, ContenedoresAlmacenajeCasillerosService>();
builder.Services.AddScoped<IAlmacenesCierreRepository, AlmacenesCierreRepository>();
builder.Services.AddScoped<IAlmacenesCierreService, AlmacenesCierreService>();
// Módulo RFID (emisión de códigos de etiquetas, equipos y consulta de etiquetas)
builder.Services.AddScoped<IRfidEquiposRepository, RfidEquiposRepository>();
builder.Services.AddScoped<IRfidEquiposService, RfidEquiposService>();
builder.Services.AddScoped<IRfidConfiguracionService, RfidConfiguracionService>();
builder.Services.AddScoped<IRfidEmisionesRepository, RfidEmisionesRepository>();
builder.Services.AddScoped<IRfidEmisionesService, RfidEmisionesService>();
builder.Services.AddScoped<IRfidImpresionRepository, RfidImpresionRepository>();
builder.Services.AddScoped<IRfidImpresionService, RfidImpresionService>();
builder.Services.AddScoped<ITiposUbicacionRepository, TiposUbicacionRepository>();
builder.Services.AddScoped<ITiposUbicacionService, TiposUbicacionService>();
builder.Services.AddScoped<ITiposAlmacenRepository, TiposAlmacenRepository>();
builder.Services.AddScoped<ITiposAlmacenService, TiposAlmacenService>();
builder.Services.AddScoped<ITiposAprobacionRepository, TiposAprobacionRepository>();
builder.Services.AddScoped<ITiposAprobacionService, TiposAprobacionService>();
builder.Services.AddScoped<ITiposAutorizacionRepository, TiposAutorizacionRepository>();
builder.Services.AddScoped<ITiposAutorizacionService, TiposAutorizacionService>();
builder.Services.AddScoped<ITiposCargoAdministrativoRepository, TiposCargoAdministrativoRepository>();
builder.Services.AddScoped<ITiposCargoAdministrativoService, TiposCargoAdministrativoService>();
builder.Services.AddScoped<ITiposCarreraAcademicaRepository, TiposCarreraAcademicaRepository>();
builder.Services.AddScoped<ITiposCarreraAcademicaService, TiposCarreraAcademicaService>();
builder.Services.AddScoped<ITiposCatalogoPreciosRepository, TiposCatalogoPreciosRepository>();
builder.Services.AddScoped<ITiposCatalogoPreciosService, TiposCatalogoPreciosService>();
builder.Services.AddScoped<ITiposCategoriaComisionRepository, TiposCategoriaComisionRepository>();
builder.Services.AddScoped<ITiposCategoriaComisionService, TiposCategoriaComisionService>();
builder.Services.AddScoped<ITiposCentroCostoRepository, TiposCentroCostoRepository>();
builder.Services.AddScoped<ITiposCentroCostoService, TiposCentroCostoService>();
builder.Services.AddScoped<ITiposCompetenciaRepository, TiposCompetenciaRepository>();
builder.Services.AddScoped<ITiposCompetenciaService, TiposCompetenciaService>();
builder.Services.AddScoped<ITiposComprobantesPagoRepository, TiposComprobantesPagoRepository>();
builder.Services.AddScoped<ITiposComprobantesPagoService, TiposComprobantesPagoService>();
builder.Services.AddScoped<ITiposConceptoRemunerativoRepository, TiposConceptoRemunerativoRepository>();
builder.Services.AddScoped<ITiposConceptoRemunerativoService, TiposConceptoRemunerativoService>();
builder.Services.AddScoped<ITiposConceptosFinancierosRepository, TiposConceptosFinancierosRepository>();
builder.Services.AddScoped<ITiposConceptosFinancierosService, TiposConceptosFinancierosService>();
builder.Services.AddScoped<ITiposContratoBeneficiosLaboralesRepository, TiposContratoBeneficiosLaboralesRepository>();
builder.Services.AddScoped<ITiposContratoBeneficiosLaboralesService, TiposContratoBeneficiosLaboralesService>();
builder.Services.AddScoped<ITiposContratoRepository, TiposContratoRepository>();
builder.Services.AddScoped<ITiposContratoService, TiposContratoService>();
builder.Services.AddScoped<ITiposCostoRepository, TiposCostoRepository>();
builder.Services.AddScoped<ITiposCostoService, TiposCostoService>();
builder.Services.AddScoped<ITiposCuentaContableRepository, TiposCuentaContableRepository>();
builder.Services.AddScoped<ITiposCuentaContableService, TiposCuentaContableService>();
builder.Services.AddScoped<ITiposCuentaCorrienteRepository, TiposCuentaCorrienteRepository>();
builder.Services.AddScoped<ITiposCuentaCorrienteService, TiposCuentaCorrienteService>();
builder.Services.AddScoped<ITiposCuentasBancariasRepository, TiposCuentasBancariasRepository>();
builder.Services.AddScoped<ITiposCuentasBancariasService, TiposCuentasBancariasService>();
builder.Services.AddScoped<ITiposDefectosTecnicosRepository, TiposDefectosTecnicosRepository>();
builder.Services.AddScoped<ITiposDefectosTecnicosService, TiposDefectosTecnicosService>();
builder.Services.AddScoped<ITiposDescuentoRepository, TiposDescuentoRepository>();
builder.Services.AddScoped<ITiposDescuentoService, TiposDescuentoService>();
builder.Services.AddScoped<ITiposDireccionRepository, TiposDireccionRepository>();
builder.Services.AddScoped<ITiposDireccionService, TiposDireccionService>();
builder.Services.AddScoped<ITiposDisenoRepository, TiposDisenoRepository>();
builder.Services.AddScoped<ITiposDisenoService, TiposDisenoService>();
builder.Services.AddScoped<ITiposDocumentoIdentidadRepository, TiposDocumentoIdentidadRepository>();
builder.Services.AddScoped<ITiposDocumentoIdentidadService, TiposDocumentoIdentidadService>();
builder.Services.AddScoped<ITiposDocumentoOficialFormatosRepository, TiposDocumentoOficialFormatosRepository>();
builder.Services.AddScoped<ITiposDocumentoOficialFormatosService, TiposDocumentoOficialFormatosService>();
builder.Services.AddScoped<ITiposDocumentoOficialRepository, TiposDocumentoOficialRepository>();
builder.Services.AddScoped<ITiposDocumentoOficialService, TiposDocumentoOficialService>();
builder.Services.AddScoped<ITiposElementoFormulaRepository, TiposElementoFormulaRepository>();
builder.Services.AddScoped<ITiposElementoFormulaService, TiposElementoFormulaService>();
builder.Services.AddScoped<ITiposEmbaladoRepository, TiposEmbaladoRepository>();
builder.Services.AddScoped<ITiposEmbaladoService, TiposEmbaladoService>();
builder.Services.AddScoped<ITiposEmbalajeRepository, TiposEmbalajeRepository>();
builder.Services.AddScoped<ITiposEmbalajeService, TiposEmbalajeService>();
builder.Services.AddScoped<ITiposEmpresaRepository, TiposEmpresaRepository>();
builder.Services.AddScoped<ITiposEmpresaService, TiposEmpresaService>();
builder.Services.AddScoped<ITiposEntidadesRepository, TiposEntidadesRepository>();
builder.Services.AddScoped<ITiposEntidadesService, TiposEntidadesService>();
builder.Services.AddScoped<ITiposEspecificacionTecnicaRepository, TiposEspecificacionTecnicaRepository>();
builder.Services.AddScoped<ITiposEspecificacionTecnicaService, TiposEspecificacionTecnicaService>();
builder.Services.AddScoped<ITiposEstadosRepository, TiposEstadosRepository>();
builder.Services.AddScoped<ITiposEstadosService, TiposEstadosService>();
builder.Services.AddScoped<ITiposEvaluacionRepository, TiposEvaluacionRepository>();
builder.Services.AddScoped<ITiposEvaluacionService, TiposEvaluacionService>();
builder.Services.AddScoped<ITiposFlujosProcesoRepository, TiposFlujosProcesoRepository>();
builder.Services.AddScoped<ITiposFlujosProcesoService, TiposFlujosProcesoService>();
builder.Services.AddScoped<ITiposHorarioRepository, TiposHorarioRepository>();
builder.Services.AddScoped<ITiposHorarioService, TiposHorarioService>();
builder.Services.AddScoped<ITiposImplementoLaboralRepository, TiposImplementoLaboralRepository>();
builder.Services.AddScoped<ITiposImplementoLaboralService, TiposImplementoLaboralService>();
builder.Services.AddScoped<ITiposImpuestoRepository, TiposImpuestoRepository>();
builder.Services.AddScoped<ITiposImpuestoService, TiposImpuestoService>();
builder.Services.AddScoped<ITiposImpuestoValoresRepository, TiposImpuestoValoresRepository>();
builder.Services.AddScoped<ITiposImpuestoValoresService, TiposImpuestoValoresService>();
builder.Services.AddScoped<ITiposInventarioRepository, TiposInventarioRepository>();
builder.Services.AddScoped<ITiposInventarioService, TiposInventarioService>();
// Flujos de aprobación: maestro + sus dos detalles (Área/Cargo y Tipos de Operación).
builder.Services.AddScoped<IFlujosAprobacionRepository, FlujosAprobacionRepository>();
builder.Services.AddScoped<IFlujosAprobacionService, FlujosAprobacionService>();
builder.Services.AddScoped<IFlujosAprobacionAreaCargoRepository, FlujosAprobacionAreaCargoRepository>();
builder.Services.AddScoped<IFlujosAprobacionAreaCargoService, FlujosAprobacionAreaCargoService>();
builder.Services.AddScoped<IFlujosAprobacionTiposOperacionRepository, FlujosAprobacionTiposOperacionRepository>();
builder.Services.AddScoped<IFlujosAprobacionTiposOperacionService, FlujosAprobacionTiposOperacionService>();
builder.Services.AddScoped<IAplicacionesOpcionesAprobacionesRepository, AplicacionesOpcionesAprobacionesRepository>();
builder.Services.AddScoped<IAplicacionesOpcionesAprobacionesService, AplicacionesOpcionesAprobacionesService>();
builder.Services.AddScoped<IEntidadesPurchaseOrdersRepository, EntidadesPurchaseOrdersRepository>();
builder.Services.AddScoped<IEntidadesPurchaseOrdersService, EntidadesPurchaseOrdersService>();
builder.Services.AddScoped<IEntidadesPurchaseOrdersDespachoRepository, EntidadesPurchaseOrdersDespachoRepository>();
builder.Services.AddScoped<IEntidadesPurchaseOrdersDespachoService, EntidadesPurchaseOrdersDespachoService>();
builder.Services.AddScoped<IEntidadesPurchaseOrdersEspecificacionTecnicaRepository, EntidadesPurchaseOrdersEspecificacionTecnicaRepository>();
builder.Services.AddScoped<IEntidadesPurchaseOrdersEspecificacionTecnicaService, EntidadesPurchaseOrdersEspecificacionTecnicaService>();
builder.Services.AddScoped<ITiposLineaProduccionRepository, TiposLineaProduccionRepository>();
builder.Services.AddScoped<ITiposLineaProduccionService, TiposLineaProduccionService>();
builder.Services.AddScoped<ITiposLoteControlRepository, TiposLoteControlRepository>();
builder.Services.AddScoped<ITiposLoteControlService, TiposLoteControlService>();
builder.Services.AddScoped<ITiposMantenimientoIndustrialRepository, TiposMantenimientoIndustrialRepository>();
builder.Services.AddScoped<ITiposMantenimientoIndustrialService, TiposMantenimientoIndustrialService>();
builder.Services.AddScoped<ITiposMedioContactoRepository, TiposMedioContactoRepository>();
builder.Services.AddScoped<ITiposMedioContactoService, TiposMedioContactoService>();
builder.Services.AddScoped<ITiposLecturaRepository, TiposLecturaRepository>();
builder.Services.AddScoped<ITiposLecturaService, TiposLecturaService>();
builder.Services.AddScoped<ITiposMermaRepository, TiposMermaRepository>();
builder.Services.AddScoped<ITiposMermaService, TiposMermaService>();
builder.Services.AddScoped<ITiposMotivosRepository, TiposMotivosRepository>();
builder.Services.AddScoped<ITiposMotivosService, TiposMotivosService>();
builder.Services.AddScoped<ITiposNotificacionRepository, TiposNotificacionRepository>();
builder.Services.AddScoped<ITiposNotificacionService, TiposNotificacionService>();
builder.Services.AddScoped<ITiposNotificacionAccionesRepository, TiposNotificacionAccionesRepository>();
builder.Services.AddScoped<ITiposNotificacionAccionesService, TiposNotificacionAccionesService>();
builder.Services.AddScoped<IEjecutarSentenciasRepository, EjecutarSentenciasRepository>();
builder.Services.AddScoped<IEjecutarSentenciasService, EjecutarSentenciasService>();
builder.Services.AddScoped<IBancoColoresRepository, BancoColoresRepository>();
builder.Services.AddScoped<IBancoColoresService, BancoColoresService>();
builder.Services.AddScoped<IDiccionarioEtiquetasRepository, DiccionarioEtiquetasRepository>();
builder.Services.AddScoped<IDiccionarioEtiquetasService, DiccionarioEtiquetasService>();
builder.Services.AddScoped<IDominiosEmpresaRepository, DominiosEmpresaRepository>();
builder.Services.AddScoped<IDominiosEmpresaService, DominiosEmpresaService>();
builder.Services.AddScoped<INotificacionesFlujoRepository, NotificacionesFlujoRepository>();
builder.Services.AddScoped<INotificacionesFlujoService, NotificacionesFlujoService>();
builder.Services.AddScoped<INotificacionesAprobacionesRepository, NotificacionesAprobacionesRepository>();
builder.Services.AddScoped<INotificacionesAprobacionesService, NotificacionesAprobacionesService>();
builder.Services.AddScoped<ITiposMenuOpcionesRepository, TiposMenuOpcionesRepository>();
builder.Services.AddScoped<ITiposMenuOpcionesService, TiposMenuOpcionesService>();
builder.Services.AddScoped<ITiposMovimientoRepository, TiposMovimientoRepository>();
builder.Services.AddScoped<ITiposMovimientoService, TiposMovimientoService>();
builder.Services.AddScoped<ITiposMovimientoTiposComprobantePagoRepository, TiposMovimientoTiposComprobantePagoRepository>();
builder.Services.AddScoped<ITiposMovimientoTiposComprobantePagoService, TiposMovimientoTiposComprobantePagoService>();
builder.Services.AddScoped<IUsuariosTiposMovimientoDirectAccessRepository, UsuariosTiposMovimientoDirectAccessRepository>();
builder.Services.AddScoped<IUsuariosTiposMovimientoDirectAccessService, UsuariosTiposMovimientoDirectAccessService>();
builder.Services.AddScoped<ITiposConfiguracionRepository, TiposConfiguracionRepository>();
builder.Services.AddScoped<ITiposConfiguracionService, TiposConfiguracionService>();
builder.Services.AddScoped<IConfiguracionesProductosRepository, ConfiguracionesProductosRepository>();
builder.Services.AddScoped<IConfiguracionesProductosService, ConfiguracionesProductosService>();
builder.Services.AddScoped<IEstructuraRfidRepository, EstructuraRfidRepository>();
builder.Services.AddScoped<IEstructuraRfidService, EstructuraRfidService>();
builder.Services.AddScoped<IEstructuraCodebarRepository, EstructuraCodebarRepository>();
builder.Services.AddScoped<IEstructuraCodebarService, EstructuraCodebarService>();
builder.Services.AddScoped<IEstructuraProductoRepository, EstructuraProductoRepository>();
builder.Services.AddScoped<IEstructuraProductoService, EstructuraProductoService>();
builder.Services.AddScoped<IEntidadesConfigurarRepository, EntidadesConfigurarRepository>();
builder.Services.AddScoped<IEntidadesConfigurarService, EntidadesConfigurarService>();
builder.Services.AddScoped<IEntidadesProceduresRepository, EntidadesProceduresRepository>();
builder.Services.AddScoped<IEntidadesProceduresService, EntidadesProceduresService>();
builder.Services.AddScoped<IAplicacionesOpcionesAreasRepository, AplicacionesOpcionesAreasRepository>();
builder.Services.AddScoped<ITiposOperacionCalculoRepository, TiposOperacionCalculoRepository>();
builder.Services.AddScoped<ITiposOperacionCalculoService, TiposOperacionCalculoService>();
builder.Services.AddScoped<ITiposOperacionCalculoAfectadosRepository, TiposOperacionCalculoAfectadosRepository>();
builder.Services.AddScoped<ITiposOperacionCalculoAfectadosService, TiposOperacionCalculoAfectadosService>();
builder.Services.AddScoped<ITiposOperacionProduccionRepository, TiposOperacionProduccionRepository>();
builder.Services.AddScoped<ITiposOperacionProduccionService, TiposOperacionProduccionService>();
builder.Services.AddScoped<ITiposOperacionRepository, TiposOperacionRepository>();
builder.Services.AddScoped<ITiposOperacionService, TiposOperacionService>();
builder.Services.AddScoped<ITiposOperacionTiposComprobantePagoRepository, TiposOperacionTiposComprobantePagoRepository>();
builder.Services.AddScoped<ITiposOperacionTiposComprobantePagoService, TiposOperacionTiposComprobantePagoService>();
builder.Services.AddScoped<ITiposOrdenProduccionRepository, TiposOrdenProduccionRepository>();
builder.Services.AddScoped<ITiposOrdenProduccionService, TiposOrdenProduccionService>();
builder.Services.AddScoped<ITiposParentescoRepository, TiposParentescoRepository>();
builder.Services.AddScoped<ITiposParentescoService, TiposParentescoService>();
builder.Services.AddScoped<ITiposPedidoComercialRepository, TiposPedidoComercialRepository>();
builder.Services.AddScoped<ITiposPedidoComercialService, TiposPedidoComercialService>();
builder.Services.AddScoped<ITiposPeriodoRepository, TiposPeriodoRepository>();
builder.Services.AddScoped<ITiposPeriodoService, TiposPeriodoService>();
builder.Services.AddScoped<ITiposPersonalRepository, TiposPersonalRepository>();
builder.Services.AddScoped<ITiposPersonalService, TiposPersonalService>();
builder.Services.AddScoped<ITiposPlanillaConceptosRemunerativosRepository, TiposPlanillaConceptosRemunerativosRepository>();
builder.Services.AddScoped<ITiposPlanillaConceptosRemunerativosService, TiposPlanillaConceptosRemunerativosService>();
builder.Services.AddScoped<ITiposPlanillaPeriodosRepository, TiposPlanillaPeriodosRepository>();
builder.Services.AddScoped<ITiposPlanillaPeriodosService, TiposPlanillaPeriodosService>();
builder.Services.AddScoped<ITiposPlanillaRepository, TiposPlanillaRepository>();
builder.Services.AddScoped<ITiposPlanillaService, TiposPlanillaService>();
builder.Services.AddScoped<ITiposPosRepository, TiposPosRepository>();
builder.Services.AddScoped<ITiposPosService, TiposPosService>();
builder.Services.AddScoped<ITiposPrecioRepository, TiposPrecioRepository>();
builder.Services.AddScoped<ITiposPrecioService, TiposPrecioService>();
builder.Services.AddScoped<ITiposProductoRepository, TiposProductoRepository>();
builder.Services.AddScoped<ITiposProductoService, TiposProductoService>();
builder.Services.AddScoped<ITiposProgramacionRepository, TiposProgramacionRepository>();
builder.Services.AddScoped<ITiposProgramacionService, TiposProgramacionService>();
builder.Services.AddScoped<ITiposProyectoRepository, TiposProyectoRepository>();
builder.Services.AddScoped<ITiposProyectoService, TiposProyectoService>();
builder.Services.AddScoped<ITiposRegistroDatosRepository, TiposRegistroDatosRepository>();
builder.Services.AddScoped<ITiposRegistroDatosService, TiposRegistroDatosService>();
builder.Services.AddScoped<ITiposRequerimientoRepository, TiposRequerimientoRepository>();
builder.Services.AddScoped<ITiposRequerimientoService, TiposRequerimientoService>();
builder.Services.AddScoped<ITiposReservaStockRepository, TiposReservaStockRepository>();
builder.Services.AddScoped<ITiposReservaStockService, TiposReservaStockService>();
builder.Services.AddScoped<ITiposRutaProduccionRepository, TiposRutaProduccionRepository>();
builder.Services.AddScoped<ITiposRutaProduccionService, TiposRutaProduccionService>();
builder.Services.AddScoped<ITiposSolicitudRepository, TiposSolicitudRepository>();
builder.Services.AddScoped<ITiposSolicitudService, TiposSolicitudService>();
builder.Services.AddScoped<ITiposSucursalesRepository, TiposSucursalesRepository>();
builder.Services.AddScoped<ITiposSucursalesService, TiposSucursalesService>();
builder.Services.AddScoped<ITiposTagRepository, TiposTagRepository>();
builder.Services.AddScoped<ITiposTagService, TiposTagService>();
builder.Services.AddScoped<ITiposUnidadComercialRepository, TiposUnidadComercialRepository>();
builder.Services.AddScoped<ITiposUnidadComercialService, TiposUnidadComercialService>();
builder.Services.AddScoped<ITiposUnidadMedidaRepository, TiposUnidadMedidaRepository>();
builder.Services.AddScoped<ITiposUnidadMedidaService, TiposUnidadMedidaService>();
builder.Services.AddScoped<ITiposUnidadNegocioRepository, TiposUnidadNegocioRepository>();
builder.Services.AddScoped<ITiposUnidadNegocioService, TiposUnidadNegocioService>();
builder.Services.AddScoped<ITiposValorCalculoRepository, TiposValorCalculoRepository>();
builder.Services.AddScoped<ITiposValorCalculoService, TiposValorCalculoService>();
builder.Services.AddScoped<ITiposViaRepository, TiposViaRepository>();
builder.Services.AddScoped<ITiposViaService, TiposViaService>();
builder.Services.AddScoped<IEtiquetasBusquedaAreasRepository, EtiquetasBusquedaAreasRepository>();
builder.Services.AddScoped<IEtiquetasBusquedaAreasService, EtiquetasBusquedaAreasService>();
builder.Services.AddScoped<ITiposZonaRepository, TiposZonaRepository>();
builder.Services.AddScoped<ITiposZonaService, TiposZonaService>();
builder.Services.AddScoped<ITiposZonificacionRepository, TiposZonificacionRepository>();
builder.Services.AddScoped<ITiposZonificacionService, TiposZonificacionService>();
builder.Services.AddScoped<ITitulosAcademicosRepository, TitulosAcademicosRepository>();
builder.Services.AddScoped<ITitulosAcademicosService, TitulosAcademicosService>();
builder.Services.AddScoped<ITransportesEspecificacionesTecnicasRepository, TransportesEspecificacionesTecnicasRepository>();
builder.Services.AddScoped<ITransportesEspecificacionesTecnicasService, TransportesEspecificacionesTecnicasService>();
builder.Services.AddScoped<ITransportesRepository, TransportesRepository>();
builder.Services.AddScoped<ITransportesService, TransportesService>();
builder.Services.AddScoped<IGuiasRemisionSeriesRepository, GuiasRemisionSeriesRepository>();
builder.Services.AddScoped<IGuiasRemisionSeriesService, GuiasRemisionSeriesService>();
builder.Services.AddScoped<IGuiasRemisionRepository, GuiasRemisionRepository>();
builder.Services.AddScoped<IGuiasRemisionService, GuiasRemisionService>();
// El cliente de SUNAT es único: guarda el token (vale una hora) en memoria y lo comparte. Su conexión se renueva cada pocos minutos
// (para no quedarse con una dirección vieja de SUNAT) y espera como máximo 60 segundos por respuesta.
builder.Services.AddSingleton(_ => new GuiaRemisionSunatCliente(new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(2) }) { Timeout = TimeSpan.FromSeconds(60) }));
builder.Services.AddScoped<IGuiasRemisionEmisionService, GuiasRemisionEmisionService>();
builder.Services.AddScoped<IConfiguracionEmisionElectronicaRepository, ConfiguracionEmisionElectronicaRepository>();
builder.Services.AddScoped<IConfiguracionEmisionElectronicaService, ConfiguracionEmisionElectronicaService>();
// La clave que cifra las credenciales de emision (variable de entorno KonSolutions_EMISION_CLAVE). Un solo objeto: la lee una vez.
builder.Services.AddSingleton<ICifradoSecretos, CifradoSecretos>();
builder.Services.AddScoped<IPostItsRepository, PostItsRepository>();
builder.Services.AddScoped<IPostItsService, PostItsService>();
builder.Services.AddScoped<ITurnosTrabajoRepository, TurnosTrabajoRepository>();
builder.Services.AddScoped<ITurnosTrabajoService, TurnosTrabajoService>();
builder.Services.AddScoped<IServidoresRepository, ServidoresRepository>();
builder.Services.AddScoped<IServidoresService, ServidoresService>();
builder.Services.AddScoped<IPathFilesRepository, PathFilesRepository>();
builder.Services.AddScoped<IPathFilesService, PathFilesService>();
builder.Services.AddScoped<IDocumentosCarpetaService, DocumentosCarpetaService>();
builder.Services.AddScoped<ISistemaHerramientasRepository, SistemaHerramientasRepository>();
builder.Services.AddScoped<ISistemaHerramientasService, SistemaHerramientasService>();
builder.Services.AddScoped<IUbigeosRepository, UbigeosRepository>();
builder.Services.AddScoped<IUbigeosService, UbigeosService>();
builder.Services.AddScoped<IUnidadesComercialesRepository, UnidadesComercialesRepository>();
builder.Services.AddScoped<IUnidadesComercialesService, UnidadesComercialesService>();
builder.Services.AddScoped<IUnidadesMedidaEquivalenciasRepository, UnidadesMedidaEquivalenciasRepository>();
builder.Services.AddScoped<IUnidadesMedidaEquivalenciasService, UnidadesMedidaEquivalenciasService>();
builder.Services.AddScoped<IUnidadesMedidaRepository, UnidadesMedidaRepository>();
builder.Services.AddScoped<IUnidadesMedidaService, UnidadesMedidaService>();
builder.Services.AddScoped<IUnidadesNegocioRepository, UnidadesNegocioRepository>();
builder.Services.AddScoped<IUnidadesNegocioService, UnidadesNegocioService>();
builder.Services.AddScoped<IUsuariosBiometriaFacialRepository, UsuariosBiometriaFacialRepository>();
builder.Services.AddScoped<IUsuariosAlmacenesRepository, UsuariosAlmacenesRepository>();
builder.Services.AddScoped<IUsuariosAlmacenesService, UsuariosAlmacenesService>();
builder.Services.AddScoped<IUsuariosAlmacenesTiposMovimientoRepository, UsuariosAlmacenesTiposMovimientoRepository>();
builder.Services.AddScoped<IUsuariosAlmacenesTiposMovimientoService, UsuariosAlmacenesTiposMovimientoService>();
builder.Services.AddScoped<IUsuariosAsientosContablesRepository, UsuariosAsientosContablesRepository>();
builder.Services.AddScoped<IUsuariosAsientosContablesService, UsuariosAsientosContablesService>();
builder.Services.AddScoped<IUsuariosAutorizadoresRepository, UsuariosAutorizadoresRepository>();
builder.Services.AddScoped<IUsuariosAutorizadoresService, UsuariosAutorizadoresService>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IEntidadesUsuariosSugeridosRepository, EntidadesUsuariosSugeridosRepository>();
builder.Services.AddScoped<IEntidadesUsuariosSugeridosService, EntidadesUsuariosSugeridosService>();
builder.Services.AddScoped<IUsuariosSucursalesCalendarioRepository, UsuariosSucursalesCalendarioRepository>();
builder.Services.AddScoped<IUsuariosSucursalesCalendarioService, UsuariosSucursalesCalendarioService>();
builder.Services.AddScoped<IUsuariosTiposOperacionRepository, UsuariosTiposOperacionRepository>();
builder.Services.AddScoped<IUsuariosTiposOperacionService, UsuariosTiposOperacionService>();
builder.Services.AddScoped<IUtilidadesAnualesRepository, UtilidadesAnualesRepository>();
builder.Services.AddScoped<IUtilidadesAnualesService, UtilidadesAnualesService>();
builder.Services.AddScoped<IZonificacionRepository, ZonificacionRepository>();
builder.Services.AddScoped<IZonificacionService, ZonificacionService>();

builder.Services.AddScoped<IComercialTiposTransporteRepository, ComercialTiposTransporteRepository>();
builder.Services.AddScoped<IComercialTiposTransporteService, ComercialTiposTransporteService>();
builder.Services.AddScoped<ILogisticaTiposTransporteRepository, LogisticaTiposTransporteRepository>();
builder.Services.AddScoped<ILogisticaTiposTransporteService, LogisticaTiposTransporteService>();

builder.Services.AddScoped<IAplicacionesOpcionesTreeviewRepository, AplicacionesOpcionesTreeviewRepository>();
builder.Services.AddScoped<IAplicacionesOpcionesTreeviewService, AplicacionesOpcionesTreeviewService>();
builder.Services.AddScoped<IAplicacionesRepository, AplicacionesRepository>();
builder.Services.AddScoped<IAplicacionesService, AplicacionesService>();
builder.Services.AddScoped<IAplicacionesOpcionesBotonesRepository, AplicacionesOpcionesBotonesRepository>();
builder.Services.AddScoped<IAplicacionesOpcionesBotonesService, AplicacionesOpcionesBotonesService>();
builder.Services.AddScoped<IAplicacionesOpcionesRepository, AplicacionesOpcionesRepository>();
builder.Services.AddScoped<IAplicacionesOpcionesService, AplicacionesOpcionesService>();
builder.Services.AddScoped<IBotonesEstandarRepository, BotonesEstandarRepository>();
builder.Services.AddScoped<IBotonesEstandarService, BotonesEstandarService>();
builder.Services.AddScoped<IComplejidadPasswordRepository, ComplejidadPasswordRepository>();
builder.Services.AddScoped<IComplejidadPasswordService, ComplejidadPasswordService>();
builder.Services.AddScoped<IPerfilesUsuarioRepository, PerfilesUsuarioRepository>();
builder.Services.AddScoped<IPerfilesUsuarioService, PerfilesUsuarioService>();
// Ficha técnica de los botones (Clase/Método/SP) para el clic derecho de AccionesBotones.
// Singleton a propósito: resuelve por reflexión sobre el ensamblado, que no cambia mientras
// el proceso viva, y cachea los resultados — no tiene estado por request ni toca la BD.
builder.Services.AddSingleton<IAccionTecnicaService, AccionTecnicaService>();
builder.Services.AddScoped<IRolesUsuarioAplicacionesRepository, RolesUsuarioAplicacionesRepository>();
builder.Services.AddScoped<IRolesUsuarioAplicacionesService, RolesUsuarioAplicacionesService>();
builder.Services.AddScoped<IRolesUsuarioRepository, RolesUsuarioRepository>();
builder.Services.AddScoped<IRolesUsuarioService, RolesUsuarioService>();
// Autorización de la API (guardián de permisos + desempate del upsert).
builder.Services.AddScoped<IPermisosApiRepository, PermisosApiRepository>();
builder.Services.AddScoped<IPermisosApiService, PermisosApiService>();
// Hijos del rol: almacenes, tipos de movimiento por almacén, tipos de operación y asientos.
builder.Services.AddScoped<IRolesUsuarioAlmacenesRepository, RolesUsuarioAlmacenesRepository>();
builder.Services.AddScoped<IRolesUsuarioAlmacenesService, RolesUsuarioAlmacenesService>();
builder.Services.AddScoped<IRolesUsuarioAlmacenesTiposMovimientoRepository, RolesUsuarioAlmacenesTiposMovimientoRepository>();
builder.Services.AddScoped<IRolesUsuarioAlmacenesTiposMovimientoService, RolesUsuarioAlmacenesTiposMovimientoService>();
builder.Services.AddScoped<IRolesUsuarioTiposOperacionRepository, RolesUsuarioTiposOperacionRepository>();
builder.Services.AddScoped<IRolesUsuarioTiposOperacionService, RolesUsuarioTiposOperacionService>();
builder.Services.AddScoped<IRolesUsuarioAsientosContablesRepository, RolesUsuarioAsientosContablesRepository>();
builder.Services.AddScoped<IRolesUsuarioAsientosContablesService, RolesUsuarioAsientosContablesService>();
builder.Services.AddScoped<ITiposOpcionesRepository, TiposOpcionesRepository>();
builder.Services.AddScoped<ITiposOpcionesService, TiposOpcionesService>();

builder.Services.AddScoped<ITablasTiposEstadoRepository, TablasTiposEstadoRepository>();
builder.Services.AddScoped<ITablasTiposEstadoService, TablasTiposEstadoService>();

// RateLimitMiddleware.cs quedó vacío hace tiempo con un comentario diciendo que su lógica
// "se migró" al RateLimiter nativo — pero ese AddRateLimiter()/UseRateLimiter() nunca se
// llegó a agregar acá, así que hoy no hay ningún límite real de intentos (login vulnerable
// a fuerza bruta/credential stuffing). Se agrega ahora: partición por IP, ventana fija de
// 1 minuto, máximo 5 intentos — solo sobre /api/v1/auth/login (política "login" aplicada
// con [EnableRateLimiting("login")] en AuthController.Login).
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("login", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "sin-ip",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

var app = builder.Build();

// Antes que cualquier middleware que escriba en la respuesta, para que alcance a todo.
app.UseResponseCompression();

app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseRateLimiter();
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KonSolutions Costos API v1"));
}

// Si la API escucha solo por HTTP (detras de otro servidor o en una red interna) se apaga con Seguridad:RedirigirAHttps = false; por omision redirige como siempre.
if (app.Configuration.GetValue("Seguridad:RedirigirAHttps", true)) app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
    app.UseCors("AllowAll");
else
    app.UseCors("Production");

// ── Timing-Allow-Origin ─────────────────────────────────────────────────────
// SOLO EN DESARROLLO, y sirve para poder medir.
//
// El navegador esconde el desglose de tiempos (cuánto esperó turno, cuánto tardó el
// servidor, cuánto pesó la respuesta) de toda petición a OTRO ORIGEN. Y acá lo son: la
// aplicación corre en un puerto y la API en otro. Sin esta cabecera, medir desde la
// consola devuelve ceros y números negativos — que es justo lo que nos pasó.
//
// Con ella, Resource Timing entrega los valores reales y se puede separar "está
// haciendo cola" de "el servidor tarda", que piden arreglos opuestos.
//
// NO va en producción: expone tiempos internos a cualquier sitio que los pida. Si
// aplicación y API terminan bajo el mismo dominio, tampoco hace falta.
if (app.Environment.IsDevelopment())
{
    app.Use(async (ctx, next) =>
    {
        ctx.Response.Headers["Timing-Allow-Origin"] = "*";
        await next();
    });
}
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }))
   .AllowAnonymous();

// ── Warmup: pre-calienta el connection pool al arrancar ──────────────────────
// Evita el cold start de ~100s en el primer login después de iniciar la API.
_ = Task.Run(async () =>
{
    await Task.Delay(2000); // Esperar que el servidor esté listo
    try
    {
        var db = app.Services.GetRequiredService<IDbConnectionFactory>();
        using var conn = db.CreateConnection();
        // Abrir la conexión es lo único que hace falta para pre-calentar el pool: el
        // handshake y la autenticación ya ocurrieron. No hace falta ninguna sentencia,
        // así que no queda SQL suelto en el código.
        if (conn.State != System.Data.ConnectionState.Open) conn.Open();
        app.Logger.LogInformation("[WARMUP] Conexión a BD pre-calentada correctamente.");
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning("[WARMUP] No se pudo pre-calentar la conexión: {msg}", ex.Message);
    }
});

app.Run();
