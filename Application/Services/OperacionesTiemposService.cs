using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

/// <summary>Operaciones, tiempos estandar y rutas con tiempos (scripts SQL 119 a 122): valida lo que la base no puede decir con claridad y
/// traduce sus errores de validacion a mensajes para el usuario.</summary>
public class OperacionesTiemposService : IOperacionesTiemposService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OperacionesTiemposService);
    private readonly IOperacionesTiemposRepository _repo;
    private readonly ErrorLogService _log;
    public OperacionesTiemposService(IOperacionesTiemposRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    /// <summary>Errores que son del usuario y no de la aplicacion: 50000 (catalogos: ya existe / no existe), 52442 (ruta bloqueada), 52500 a 52559 (scripts 120 a 122).</summary>
    private static bool EsValidacion(SqlException ex) => ex.Number is 50000 or 52442 or (>= 52500 and <= 52559);
    private sealed class ValidacionException : Exception { public ValidacionException(string m) : base(m) { } }

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (ValidacionException ex) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (ex.Number == 547) { return ApiResponse<T>.Fail("El dato indicado no existe en su catalogo, o el registro esta en uso y no se puede quitar."); }
        catch (SqlException ex) when (ex.Number is 2627 or 2601) { return ApiResponse<T>.Fail("Ya existe un registro con ese codigo."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    private async Task<ApiResponse<bool>> EjecutarSin(Func<Task> accion, string metodo) => await Ejecutar(async () => { await accion(); return true; }, metodo);

    private static void Req(string? valor, string que) { if (string.IsNullOrWhiteSpace(valor)) throw new ValidacionException(que + " es obligatorio."); }

    // ===== O1 =====
    public Task<ApiResponse<List<CatalogoOperacionItem>>> CatalogoListarAsync(string catalogo, bool soloActivos) =>
        Ejecutar(() => _repo.CatalogoListarAsync(catalogo, soloActivos), nameof(CatalogoListarAsync));

    public Task<ApiResponse<bool>> CatalogoGuardarAsync(string catalogo, CatalogoOperacionItem item, bool nuevo, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            Req(item.COD, "El codigo"); Req(item.DES, "La descripcion");
            item.COD = item.COD.Trim().ToUpperInvariant(); item.DES = item.DES.Trim().ToUpperInvariant();
            if (catalogo == "materiales" && string.IsNullOrWhiteSpace(item.COD_REGLA)) throw new ValidacionException("La abreviatura es obligatoria.");
            if (catalogo == "distribuciones" && item.COD_REGLA is not ("MAX_PUESTO" or "SUMA_OPERARIOS")) throw new ValidacionException("La regla de ciclo debe ser MAX_PUESTO o SUMA_OPERARIOS.");
            await _repo.CatalogoGuardarAsync(catalogo, item, nuevo, codUsuario, codEstacion);
        }, nameof(CatalogoGuardarAsync));

    public Task<ApiResponse<bool>> CatalogoEliminarAsync(string catalogo, string cod) => EjecutarSin(() => _repo.CatalogoEliminarAsync(catalogo, cod), nameof(CatalogoEliminarAsync));

    public Task<ApiResponse<Dictionary<string, List<CatalogoOperacionItem>>>> CombosAsync() => Ejecutar(_repo.CombosAsync, nameof(CombosAsync));

    // ===== O2 =====
    public Task<ApiResponse<List<Operacion>>> OperacionesAsync() => Ejecutar(_repo.OperacionesAsync, nameof(OperacionesAsync));

    public Task<ApiResponse<Operacion>> OperacionAsync(string cod) =>
        Ejecutar(async () => await _repo.OperacionAsync(cod) ?? throw new ValidacionException("La operacion no existe."), nameof(OperacionAsync));

    public Task<ApiResponse<bool>> OperacionGuardarAsync(OperacionGuardarDto dto, bool nuevo, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            Req(dto.COD_OPERACION, "El codigo"); Req(dto.DES_OPERACION, "La descripcion"); Req(dto.COD_TIPO_OPERACION_PRODUCCION, "El tipo de operacion"); Req(dto.COD_PROCESO, "El proceso");
            dto.COD_OPERACION = dto.COD_OPERACION.Trim().ToUpperInvariant(); dto.DES_OPERACION = dto.DES_OPERACION.Trim().ToUpperInvariant();
            await _repo.OperacionGuardarAsync(dto, nuevo, codUsuario, codEstacion);
        }, nameof(OperacionGuardarAsync));

    public Task<ApiResponse<bool>> OperacionEliminarAsync(string cod) => EjecutarSin(() => _repo.OperacionEliminarAsync(cod), nameof(OperacionEliminarAsync));
    public Task<ApiResponse<string>> SiguienteCodigoAsync() => Ejecutar(_repo.SiguienteCodigoAsync, nameof(SiguienteCodigoAsync));
    public Task<ApiResponse<List<OperacionComponente>>> ComponentesAsync(string codPadre) => Ejecutar(() => _repo.ComponentesAsync(codPadre), nameof(ComponentesAsync));

    public Task<ApiResponse<bool>> ComponentesGuardarAsync(OperacionComponentesGuardarDto dto, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            Req(dto.COD_OPERACION_PADRE, "La operacion");
            if (dto.Componentes.Count > 200) throw new ValidacionException("Una operacion admite hasta 200 componentes.");
            await _repo.ComponentesGuardarAsync(dto, codUsuario, codEstacion);
        }, nameof(ComponentesGuardarAsync));

    public Task<ApiResponse<List<OperacionArbolNodo>>> ArbolAsync(string cod, DateTime? fecha) => Ejecutar(() => _repo.ArbolAsync(cod, fecha), nameof(ArbolAsync));
    public Task<ApiResponse<List<OperacionUsadaEn>>> UsadaEnAsync(string cod) => Ejecutar(() => _repo.UsadaEnAsync(cod), nameof(UsadaEnAsync));
    public Task<ApiResponse<List<OperacionUsadaEnRuta>>> UsadaEnRutasAsync(string cod) => Ejecutar(() => _repo.UsadaEnRutasAsync(cod), nameof(UsadaEnRutasAsync));

    // ===== O3 =====
    public Task<ApiResponse<List<OperacionTiempo>>> TiemposAsync(string cod) => Ejecutar(() => _repo.TiemposAsync(cod), nameof(TiemposAsync));

    public Task<ApiResponse<OperacionTiempoResultado>> TiempoGuardarAsync(OperacionTiempoGuardarDto dto, string codUsuario, string codEstacion) =>
        Ejecutar(() =>
        {
            Req(dto.COD_OPERACION, "La operacion"); Req(dto.COD_METODO_TIEMPO, "El metodo de tiempo");
            return _repo.TiempoGuardarAsync(dto, codUsuario, codEstacion);
        }, nameof(TiempoGuardarAsync));

    public Task<ApiResponse<bool>> TiempoAprobarAsync(OperacionTiempoClaveDto dto, string codUsuario, string codEstacion) => EjecutarSin(() => _repo.TiempoAprobarAsync(dto, codUsuario, codEstacion), nameof(TiempoAprobarAsync));
    public Task<ApiResponse<bool>> TiempoEliminarAsync(OperacionTiempoClaveDto dto) => EjecutarSin(() => _repo.TiempoEliminarAsync(dto), nameof(TiempoEliminarAsync));
    public Task<ApiResponse<List<EstudioTiempo>>> EstudiosAsync(string cod) => Ejecutar(() => _repo.EstudiosAsync(cod), nameof(EstudiosAsync));
    public Task<ApiResponse<List<EstudioTiempoElemento>>> EstudioElementosAsync(int num) => Ejecutar(() => _repo.EstudioElementosAsync(num), nameof(EstudioElementosAsync));

    public Task<ApiResponse<EstudioTiempoResultado>> EstudioGuardarAsync(EstudioTiempoGuardarDto dto, string codUsuario, string codEstacion) =>
        Ejecutar(() =>
        {
            Req(dto.COD_OPERACION, "La operacion"); Req(dto.COD_METODO_TIEMPO, "El metodo");
            if (dto.Elementos.Count == 0) throw new ValidacionException("El estudio debe tener al menos un elemento.");
            if (dto.Elementos.Count > 200) throw new ValidacionException("Un estudio admite hasta 200 elementos.");
            return _repo.EstudioGuardarAsync(dto, codUsuario, codEstacion);
        }, nameof(EstudioGuardarAsync));

    public Task<ApiResponse<bool>> EstudioEliminarAsync(int num) => EjecutarSin(() => _repo.EstudioEliminarAsync(num), nameof(EstudioEliminarAsync));
    public Task<ApiResponse<List<SamPorCategoria>>> SamPorCategoriaOperacionAsync(string cod, DateTime? fecha) => Ejecutar(() => _repo.SamPorCategoriaOperacionAsync(cod, fecha), nameof(SamPorCategoriaOperacionAsync));
    public Task<ApiResponse<List<SamPorClase>>> SamPorClaseOperacionAsync(string cod, DateTime? fecha) => Ejecutar(() => _repo.SamPorClaseOperacionAsync(cod, fecha), nameof(SamPorClaseOperacionAsync));

    // ===== O4 =====
    public Task<ApiResponse<List<RutaTiempoResumen>>> RutasAsync() => Ejecutar(_repo.RutasAsync, nameof(RutasAsync));
    public Task<ApiResponse<List<RutaNodo>>> RutaArbolAsync(string ruta, DateTime? fecha) => Ejecutar(() => _repo.RutaArbolAsync(ruta, fecha), nameof(RutaArbolAsync));
    public Task<ApiResponse<int>> RutaProcesoAgregarAsync(RutaProcesoAgregarDto dto, string codUsuario, string codEstacion) => Ejecutar(() => _repo.RutaProcesoAgregarAsync(dto, codUsuario, codEstacion), nameof(RutaProcesoAgregarAsync));
    public Task<ApiResponse<int>> RutaSubprocesoAgregarAsync(RutaSubprocesoAgregarDto dto, string codUsuario, string codEstacion) => Ejecutar(() => _repo.RutaSubprocesoAgregarAsync(dto, codUsuario, codEstacion), nameof(RutaSubprocesoAgregarAsync));

    public Task<ApiResponse<RutaOperacionAgregarResultado>> RutaOperacionAgregarAsync(RutaOperacionAgregarDto dto, string codUsuario, string codEstacion) =>
        Ejecutar(() =>
        {
            Req(dto.COD_OPERACION, "La operacion");
            if (dto.VAL_FRECUENCIA <= 0) throw new ValidacionException("La frecuencia debe ser mayor que cero.");
            return _repo.RutaOperacionAgregarAsync(dto, codUsuario, codEstacion);
        }, nameof(RutaOperacionAgregarAsync));

    public Task<ApiResponse<int>> RutaQuitarAsync(RutaFilaClaveDto dto) => Ejecutar(() => _repo.RutaQuitarAsync(dto), nameof(RutaQuitarAsync));
    public Task<ApiResponse<bool>> RutaTiempoPropioAsync(RutaFilaClaveDto dto, string codUsuario, string codEstacion) => EjecutarSin(() => _repo.RutaTiempoPropioAsync(dto, codUsuario, codEstacion), nameof(RutaTiempoPropioAsync));
    public Task<ApiResponse<bool>> RutaOperacionEditarAsync(RutaOperacionEditarDto dto, string codUsuario, string codEstacion) =>
        EjecutarSin(() =>
        {
            Req(dto.COD_RUTA_PRODUCCION, "La ruta");
            if (dto.VAL_FRECUENCIA <= 0 || dto.VAL_FRECUENCIA > 9999) throw new ValidacionException("Las veces por unidad deben ser mayores que cero y como maximo 9999.");
            dto.COD_CATEGORIA_OCUPACIONAL = string.IsNullOrWhiteSpace(dto.COD_CATEGORIA_OCUPACIONAL) ? null : dto.COD_CATEGORIA_OCUPACIONAL.Trim().ToUpperInvariant();
            return _repo.RutaOperacionEditarAsync(dto, codUsuario, codEstacion);
        }, nameof(RutaOperacionEditarAsync));
    public Task<ApiResponse<bool>> RutaMoverAsync(RutaMoverDto dto) =>
        Ejecutar(() => { if (dto.DIRECCION is not (-1 or 1)) throw new ValidacionException("La direccion debe ser -1 (subir) o 1 (bajar)."); return _repo.RutaMoverAsync(dto); }, nameof(RutaMoverAsync));
    public Task<ApiResponse<int>> RutaRecalcularAsync(RutaRecalcularDto dto, string codUsuario, string codEstacion) => Ejecutar(() => _repo.RutaRecalcularAsync(dto, codUsuario, codEstacion), nameof(RutaRecalcularAsync));
    public Task<ApiResponse<List<SamPorCategoria>>> RutaSamPorCategoriaAsync(string ruta) => Ejecutar(() => _repo.RutaSamPorCategoriaAsync(ruta), nameof(RutaSamPorCategoriaAsync));
    public Task<ApiResponse<List<SamPorClase>>> RutaSamPorClaseAsync(string ruta) => Ejecutar(() => _repo.RutaSamPorClaseAsync(ruta), nameof(RutaSamPorClaseAsync));
    public Task<ApiResponse<List<RutaPrecedencia>>> PrecedenciasAsync(string ruta) => Ejecutar(() => _repo.PrecedenciasAsync(ruta), nameof(PrecedenciasAsync));
    public Task<ApiResponse<int>> PrecedenciasGenerarAsync(RutaPrecedenciasGenerarDto dto, string codUsuario, string codEstacion) => Ejecutar(() => _repo.PrecedenciasGenerarAsync(dto, codUsuario, codEstacion), nameof(PrecedenciasGenerarAsync));

    public Task<ApiResponse<bool>> PrecedenciasGuardarAsync(RutaPrecedenciasGuardarDto dto, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            if (dto.Precedencias.Count > 5000) throw new ValidacionException("La ruta admite hasta 5000 precedencias.");
            await _repo.PrecedenciasGuardarAsync(dto, codUsuario, codEstacion);
        }, nameof(PrecedenciasGuardarAsync));
}
