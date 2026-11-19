using Microsoft.AspNetCore.Authorization;
using KonSolutionsApi.API.Filters;
using Microsoft.AspNetCore.Mvc;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.API.Controllers;

[Authorize]
[Route("api/v1/estandar/configuracion-general")]
[ExigePermiso("ESTANDAR.CONFIGURACION_GENERAL")]
public class ConfiguracionGeneralController : BaseController
{
    private readonly IConfiguracionGeneralService _service;
    public ConfiguracionGeneralController(IConfiguracionGeneralService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { var r = await _service.GetAllAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // Antes GetAll() era [AllowAnonymous] completo — exponía datos fiscales/legales
    // (NUM_DOCUMENTO_IDENTIDAD, DES_DOMICILIO_FISCAL, DES_REFERENCIA_FISCAL) y de auditoría
    // sin token, solo porque la pantalla de Login necesita pintarse (logo/colores/tipografía)
    // antes de autenticar. Se separa: este endpoint queda anónimo pero devuelve solo el
    // subconjunto cosmético (ConfiguracionGeneralPublicDto); GetAll() ahora exige [Authorize]
    // (heredado de la clase) para el registro completo.
    [AllowAnonymous]
    [HttpGet("public")]
    public async Task<IActionResult> GetPublic()
    { var r = await _service.GetPublicAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // ── LOS LOGOS, COMO ARCHIVO Y NO DENTRO DEL JSON ────────────────────────────
    //
    // Antes viajaban como byte[] dentro de la respuesta de configuración: bajaban enteros
    // en cada arranque y el navegador no podía cachearlos por separado, porque son parte
    // de un JSON que cambia. En la medición eso daba 22 752 ms.
    //
    // Servidos como archivo con su propia URL, el navegador los guarda un día y no los
    // vuelve a pedir. Mismo patrón que la foto de usuario en UsuariosController.
    //
    // [AllowAnonymous] porque la pantalla de Login y el splash los necesitan ANTES de que
    // haya sesión. Es el mismo criterio que /public, y no expone nada sensible: son el
    // logo de la empresa y el de la aplicación, que cualquiera ve al entrar.

    /// <summary>El mismo registro completo que GetAll, pero SIN los dos logos.
    ///
    /// Lo usa el layout al arrancar: necesita todo —colores, tipografías, cultura,
    /// parámetros de grilla— pero los logos ya no, porque los pide por URL. Traerlos en
    /// esta respuesta significaba bajarlos otra vez en cada arranque; medido, esta llamada
    /// costaba 9 932 ms.
    ///
    /// Se anulan sobre la instancia recién traída del repositorio, que no está cacheada:
    /// cada llamada consulta la base, así que no hay ningún estado compartido que ensuciar.</summary>
    [HttpGet("layout")]
    public async Task<IActionResult> GetParaLayout()
    {
        var r = await _service.GetAllAsync();
        var cfg = r.Data?.FirstOrDefault();
        if (cfg is null) return Ok(r);

        cfg.FLG_TIENE_LOGO_EMPRESA    = cfg.IMG_LOGO_EMPRESA    is { Length: > 0 };
        cfg.FLG_TIENE_LOGO_APLICACION = cfg.IMG_LOGO_APLICACION is { Length: > 0 };
        cfg.DES_VERSION_LOGO_EMPRESA    = ConfiguracionGeneral.VersionDeLogo(cfg.IMG_LOGO_EMPRESA);
        cfg.DES_VERSION_LOGO_APLICACION = ConfiguracionGeneral.VersionDeLogo(cfg.IMG_LOGO_APLICACION);
        cfg.IMG_LOGO_EMPRESA    = null;
        cfg.IMG_LOGO_APLICACION = null;

        return Ok(ApiResponse<IEnumerable<ConfiguracionGeneral>>.Ok(new[] { cfg }, total: 1));
    }

    [AllowAnonymous]
    [HttpGet("logo-empresa")]
    public Task<IActionResult> GetLogoEmpresa([FromQuery] string? v) => ServirLogo(c => c.IMG_LOGO_EMPRESA, v);

    [AllowAnonymous]
    [HttpGet("logo-aplicacion")]
    public Task<IActionResult> GetLogoAplicacion([FromQuery] string? v) => ServirLogo(c => c.IMG_LOGO_APLICACION, v);

    /// <param name="v">La versión del logo (ConfiguracionGeneral.VersionDeLogo), que la pantalla pone en la dirección. Solo decide
    /// cuánto se guarda en el navegador: el contenido que se devuelve es siempre el logo que hay grabado.</param>
    private async Task<IActionResult> ServirLogo(Func<ConfiguracionGeneral, byte[]?> cual, string? v)
    {
        var r = await _service.GetAllAsync();
        var bytes = r.Data?.FirstOrDefault() is { } cfg ? cual(cfg) : null;

        // 204 y no 404: "no hay logo" no es un error. El cliente lo cachea como "sin
        // imagen" y no lo reintenta en cada arranque. Mismo criterio que la foto de usuario.
        if (bytes is null || bytes.Length == 0) return NoContent();

        // ANTES: "public, max-age=86400" sobre una dirección que siempre era la misma. Hacía DOS daños al grabar un
        // logo nuevo, y por eso ni cerrando sesión y volviendo a entrar se veía el cambio:
        //  1. El middleware UseResponseCaching de esta API guarda en memoria las respuestas "public" y las sirve sin
        //     llegar a la base. Su clave es la ruta (ignora el ?v=), así que TODOS los navegadores recibían el logo
        //     anterior hasta un día después, o hasta reiniciar la API.
        //  2. El navegador lo guardaba un día y ni preguntaba al servidor.
        //
        // AHORA es "private": el navegador lo guarda, pero la memoria de la API no (igual que la foto de usuario). Y,
        // según cómo se pida:
        //  · Con ?v=<versión> (como lo piden las pantallas): la dirección cambia cuando cambia el logo, así que lo
        //    que el navegador tenga guardado de esa dirección nunca puede ser un logo viejo. Se guarda un año.
        //  · Sin versión (quien no la conozca): el navegador pregunta cada vez si cambió, con el ETag, y recibe un
        //    304 sin bytes si es el mismo. Nunca muestra un logo viejo.
        var version = ConfiguracionGeneral.VersionDeLogo(bytes);
        var cambioPorVersion = !string.IsNullOrWhiteSpace(v);
        Response.Headers.CacheControl = cambioPorVersion ? "private, max-age=31536000, immutable" : "private, no-cache";
        return File(bytes, "image/png", null, new Microsoft.Net.Http.Headers.EntityTagHeaderValue($"\"{version}\""));
    }

    [HttpGet("{DES_NOMBRE_EMPRESA}")]
    public async Task<IActionResult> GetById(string DES_NOMBRE_EMPRESA)
    { var r = await _service.GetByIdAsync(DES_NOMBRE_EMPRESA); return r.Success ? Ok(r) : NotFound(r); }

    [HttpGet("combobox")]
    public async Task<IActionResult> GetCombobox()
    { var r = await _service.GetComboboxAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    // Consolida en un solo viaje los combos independientes que la pantalla necesita al abrir
    // (Monedas + Tipos de Configuración + Grupos de Producto) — antes 3 llamadas separadas.
    [HttpGet("loadcombos")]
    public async Task<IActionResult> GetLoadcombos()
    { var r = await _service.GetLoadcombosAsync(); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] ConfiguracionGeneralSearchDto dto)
    { var r = await _service.SearchAsync(dto); return r.Success ? Ok(r) : BadRequest(r); }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] ConfiguracionGeneralSaveDto dto)
    { if (!ModelState.IsValid) return BadRequest(ModelState);
      var r = await _service.SaveAsync(dto, GetCodUsuario(), GetEstacion());
      return r.Success ? Ok(r) : BadRequest(r); }

    [HttpDelete("{DES_NOMBRE_EMPRESA}")]
    public async Task<IActionResult> Delete(string DES_NOMBRE_EMPRESA)
    { var r = await _service.DeleteAsync(DES_NOMBRE_EMPRESA); return r.Success ? Ok(r) : BadRequest(r); }
}
