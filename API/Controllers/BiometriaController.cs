using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.API.Controllers;

/// <summary>
/// Gestión de datos biométricos faciales.
///
/// AHORA EN LA BASE: SEGURIDAD.USUARIOS_BIOMETRIA_FACIAL, una fila por muestra.
///
/// Antes cada usuario era un archivo {ContentRoot}/face-data/{COD_USUARIO}.json. Eso
/// quedaba fuera del backup de la base, no se replicaba con ella, no registraba quién
/// dio de alta el rostro ni cuándo, y desaparecía al reinstalar el sitio en otro
/// servidor. El endpoint /migrar pasa lo que haya quedado en disco a la tabla.
///
/// El flag FLG_REGISTRO_FACIAL de USUARIOS se sigue actualizando acá: es lo que mira el
/// login para saber si corresponde pedir verificación.
///
/// LO QUE NUNCA SALE DE ACÁ: el descriptor guardado. La comparación se hace en el
/// servidor y al navegador solo le vuelve un booleano y una distancia. Devolver el
/// vector permitiría reproducirlo y entrar sin la cara.
/// </summary>
[Authorize]
[SinControlPermisos("Verificación de identidad, no administración: el usuario registra y valida su "
                    + "propia huella. Si más adelante se quiere restringir el alta de huellas ajenas, "
                    + "eso va como permiso en la acción registrar, no en el controlador.")]
[Route("api/v1/biometria")]
public class BiometriaController : BaseController
{
    /// <summary>
    /// Umbral de aceptación. Es el valor de referencia de face-api.js para su modelo de
    /// 128 dimensiones.
    ///
    /// NO SUBIRLO PARA QUE "RECONOZCA MEJOR": más alto significa aceptar caras más
    /// distintas, y quien entra de más en un login no es un usuario contento, es otra
    /// persona. Si a alguien no lo reconoce, lo que corresponde es volver a registrarlo
    /// con más muestras y mejor luz.
    /// </summary>
    private const double UmbralDistancia = 0.50;

    private readonly IUsuariosBiometriaFacialRepository _repo;
    private readonly IUsuariosRepository _usuariosRepo;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<BiometriaController> _log;

    public BiometriaController(IUsuariosBiometriaFacialRepository repo,
                               IUsuariosRepository usuariosRepo,
                               IWebHostEnvironment env,
                               ILogger<BiometriaController> log)
    { _repo = repo; _usuariosRepo = usuariosRepo; _env = env; _log = log; }

    // ── GET api/v1/biometria/estado/{codUsuario} ──────────────────────────────
    [HttpGet("estado/{codUsuario}")]
    public async Task<IActionResult> GetEstado(string codUsuario)
    {
        try
        {
            var e = await _repo.EstadoAsync(Normaliza(codUsuario));
            // Los nombres se escriben tal cual los espera el Web (BiometriaEstadoDto).
            // La versión anterior devolvía NumMuestras/FecRegistro y el navegador, que
            // busca NUM_MUESTRAS y FEC_REGISTRO, los leía siempre en 0 y null.
            return Ok(ApiResponse<object>.Ok(new
            {
                e.COD_USUARIO,
                e.FLG_REGISTRADO,
                e.NUM_MUESTRAS,
                e.FEC_REGISTRO
            }));
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }

    // ── GET api/v1/biometria/estados ─────────────────────────────────────────
    [HttpGet("estados")]
    public async Task<IActionResult> GetEstados()
    {
        try
        {
            // Una consulta para todo el padrón. Antes era un acceso a disco por usuario.
            var r = await _repo.EstadosAsync();
            return Ok(ApiResponse<object>.Ok(r));
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }

    // ── POST api/v1/biometria/registrar ──────────────────────────────────────
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] BiometriaRegistrarDto dto)
    {
        if (dto.Descriptores == null || dto.Descriptores.Count < 1)
            return BadRequest(ApiResponse<object>.Fail("Se requiere al menos 1 muestra facial."));

        // Se valida el largo ANTES de tocar la tabla. Si una sola muestra viene mal, no
        // se borran las que el usuario ya tenía: quedarse sin rostro registrado por un
        // dato malformado es peor que no haber registrado el nuevo.
        var malas = dto.Descriptores.Count(d => d is null || d.Length != DescriptorFacial.Dimensiones);
        if (malas > 0)
            return BadRequest(ApiResponse<object>.Fail(
                $"{malas} de {dto.Descriptores.Count} muestras no tienen {DescriptorFacial.Dimensiones} valores. " +
                "No se modificó el registro existente."));

        try
        {
            var cod = Normaliza(dto.COD_USUARIO);
            var n   = await _repo.ReemplazarAsync(cod, dto.Descriptores, GetCodUsuario(), GetEstacion());
            await _usuariosRepo.ActualizarRegistroFacialAsync(cod, true);

            return Ok(ApiResponse<string>.Ok($"Rostro registrado con {n} muestra(s).",
                                             $"Rostro registrado con {n} muestra(s)."));
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Error registrando biometría facial de {Usuario}", dto.COD_USUARIO);
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    // ── POST api/v1/biometria/verificar ──────────────────────────────────────
    [AllowAnonymous]   // Login todavía no tiene token cuando verifica
    // Sin límite de intentos esto era fuerza-bruteable (probar muchos descriptores hasta
    // que Distancia <= 0.50). Reutiliza la misma política "login" (5/min/IP).
    [EnableRateLimiting("login")]
    [HttpPost("verificar")]
    public async Task<IActionResult> Verificar([FromBody] BiometriaVerificarDto dto)
    {
        // El mismo texto para "sin rostro registrado" y para "no coincide" sería lo
        // correcto si esto fuera público, pero acá el usuario ya se identificó y necesita
        // saber por qué no puede entrar. El rate limiting es lo que cubre el abuso.
        try
        {
            if (dto.Descriptor is null || dto.Descriptor.Length != DescriptorFacial.Dimensiones)
                return Ok(ApiResponse<object>.Ok(new
                {
                    Verificado = false, Distancia = 1.0,
                    Mensaje = "La muestra recibida no es válida."
                }));

            var muestras = (await _repo.ShowallAsync(Normaliza(dto.COD_USUARIO))).ToList();
            if (muestras.Count == 0)
                return Ok(ApiResponse<object>.Ok(new
                {
                    Verificado = false, Distancia = 1.0,
                    Mensaje = "Sin rostro registrado."
                }));

            double minDist = muestras
                .Select(m => DescriptorFacial.Distancia(DescriptorFacial.ADoubles(m.VAL_DESCRIPTOR), dto.Descriptor))
                .DefaultIfEmpty(1.0)
                .Min();

            bool ok = minDist <= UmbralDistancia;
            return Ok(ApiResponse<object>.Ok(new
            {
                Verificado = ok,
                Distancia  = Math.Round(minDist, 4),
                Mensaje    = ok ? "Verificación exitosa." : "Rostro no reconocido."
            }));
        }
        catch (Exception ex)
        {
            // Un error acá NO puede leerse como verificación exitosa.
            _log.LogError(ex, "Error verificando biometría facial de {Usuario}", dto.COD_USUARIO);
            return Ok(ApiResponse<object>.Ok(new
            {
                Verificado = false, Distancia = 1.0,
                Mensaje = "No se pudo verificar el rostro."
            }));
        }
    }

    // ── DELETE api/v1/biometria/{codUsuario} ─────────────────────────────────
    [HttpDelete("{codUsuario}")]
    public async Task<IActionResult> Eliminar(string codUsuario)
    {
        try
        {
            var cod = Normaliza(codUsuario);
            await _repo.DeleteAsync(cod);
            await _usuariosRepo.ActualizarRegistroFacialAsync(cod, false);
            return Ok(ApiResponse<string>.Ok("Registro facial eliminado.", "Registro facial eliminado."));
        }
        catch (Exception ex) { return BadRequest(ApiResponse<object>.Fail(ex.Message)); }
    }

    // ── POST api/v1/biometria/migrar ─────────────────────────────────────────
    /// <summary>
    /// Pasa a la tabla los rostros que quedaron en {ContentRoot}/face-data/*.json.
    ///
    /// Se corre UNA vez, a mano, después de aplicar el script de los SP. No está en el
    /// arranque a propósito: una migración que se dispara sola es una migración que
    /// nadie mira, y si falla a mitad de camino nadie se entera hasta que un usuario no
    /// puede entrar.
    ///
    /// Cada archivo migrado se renombra a .json.migrado. Así volver a llamar a este
    /// endpoint no duplica nada, y el original queda ahí por si hay que revisarlo.
    /// Cuando confirmes que todos entran por rostro, esa carpeta se puede borrar.
    /// </summary>
    [HttpPost("migrar")]
    public async Task<IActionResult> Migrar()
    {
        var dir = Path.Combine(_env.ContentRootPath, "face-data");
        if (!Directory.Exists(dir))
            return Ok(ApiResponse<object>.Ok(new { Migrados = 0, Omitidos = 0, Detalle = new List<string>() },
                                             "No hay carpeta face-data: no hay nada que migrar."));

        int migrados = 0, omitidos = 0;
        var detalle = new List<string>();

        foreach (var file in Directory.GetFiles(dir, "*.json"))
        {
            var cod = Path.GetFileNameWithoutExtension(file).Trim().ToUpperInvariant();
            try
            {
                var data = JsonSerializer.Deserialize<BiometriaFileData>(await System.IO.File.ReadAllTextAsync(file));
                var descs = data?.Descriptores?
                    .Where(d => d is not null && d.Length == DescriptorFacial.Dimensiones)
                    .ToList() ?? new List<double[]>();

                if (descs.Count == 0)
                {
                    omitidos++;
                    detalle.Add($"{cod}: sin muestras válidas, no se migró.");
                    continue;
                }

                await _repo.ReemplazarAsync(cod, descs, GetCodUsuario(), GetEstacion());
                await _usuariosRepo.ActualizarRegistroFacialAsync(cod, true);
                System.IO.File.Move(file, file + ".migrado", overwrite: true);

                migrados++;
                detalle.Add($"{cod}: {descs.Count} muestra(s).");
            }
            catch (Exception ex)
            {
                // Un archivo que falla no detiene a los demás: es preferible migrar 199 de
                // 200 y tener la lista del que faltó, que quedarse en el primero.
                omitidos++;
                detalle.Add($"{cod}: ERROR {ex.Message}");
                _log.LogError(ex, "Error migrando biometría facial de {Usuario}", cod);
            }
        }

        return Ok(ApiResponse<object>.Ok(new { Migrados = migrados, Omitidos = omitidos, Detalle = detalle },
                                         $"Migrados {migrados}, omitidos {omitidos}."));
    }

    // ── helpers ──────────────────────────────────────────────────────────────

    /// <summary>El código se guardaba como nombre de archivo en mayúsculas, y así quedó en
    /// los JSON que hay que migrar. Se mantiene el mismo criterio para que lo migrado y lo
    /// que se registre de ahora en más coincidan.</summary>
    private static string Normaliza(string cod) => (cod ?? "").Trim().ToUpperInvariant();
}

// ── DTOs locales (solo API) ───────────────────────────────────────────────────
public class BiometriaRegistrarDto
{
    public string COD_USUARIO { get; set; } = string.Empty;
    public List<double[]> Descriptores { get; set; } = new();
}

public class BiometriaVerificarDto
{
    public string   COD_USUARIO { get; set; } = string.Empty;
    public double[] Descriptor  { get; set; } = Array.Empty<double>();
}

/// <summary>El formato de los JSON viejos. Solo lo usa /migrar; cuando la carpeta
/// face-data se borre, esta clase se puede borrar con ella.</summary>
public class BiometriaFileData
{
    public string         CodUsuario   { get; set; } = string.Empty;
    public List<double[]> Descriptores { get; set; } = new();
    public int            NumMuestras  { get; set; }
    public DateTime       FecRegistro  { get; set; }
}
