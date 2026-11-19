using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

/// <summary>Personal por categoria ocupacional y armado de lineas (scripts SQL 124 y 125): valida lo que la base no puede decir con claridad
/// y traduce sus errores de validacion a mensajes para el usuario.</summary>
public class PersonalLineasService : IPersonalLineasService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(PersonalLineasService);
    private readonly IPersonalLineasRepository _repo;
    private readonly ErrorLogService _log;
    public PersonalLineasService(IPersonalLineasRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    /// <summary>Errores que son del usuario y no de la aplicacion: 50000 (ya existe / no existe) y 52560 a 52609 (scripts 124, 125 y 127).</summary>
    private static bool EsValidacion(SqlException ex) => ex.Number is 50000 or (>= 52560 and <= 52609);
    private sealed class ValidacionException : Exception { public ValidacionException(string m) : base(m) { } }

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (ValidacionException ex) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (ex.Number == 547) { return ApiResponse<T>.Fail("El dato indicado no existe en su catalogo, no cumple un limite permitido o el registro esta en uso y no se puede quitar."); }
        catch (SqlException ex) when (ex.Number is 2627 or 2601) { return ApiResponse<T>.Fail("Ya existe un registro con ese codigo."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    private async Task<ApiResponse<bool>> EjecutarSin(Func<Task> accion, string metodo) => await Ejecutar(async () => { await accion(); return true; }, metodo);

    private static void Req(string? valor, string que) { if (string.IsNullOrWhiteSpace(valor)) throw new ValidacionException(que + " es obligatorio."); }

    // ===== O5 =====
    public Task<ApiResponse<List<PersonalOperacionItem>>> PersonalListarAsync(string? buscar) => Ejecutar(() => _repo.PersonalListarAsync(buscar), nameof(PersonalListarAsync));

    public Task<ApiResponse<PersonalOperacionItem>> PersonalAsync(int codEntidad) =>
        Ejecutar(async () => await _repo.PersonalAsync(codEntidad) ?? throw new ValidacionException("La persona no tiene categoria asignada."), nameof(PersonalAsync));

    public Task<ApiResponse<bool>> PersonalGuardarAsync(PersonalOperacionGuardarDto dto, bool nuevo, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            if (dto.COD_ENTIDAD_PERSONAL <= 0) throw new ValidacionException("La persona es obligatoria.");
            Req(dto.COD_CATEGORIA_OCUPACIONAL, "La categoria"); Req(dto.COD_PROCESO, "El proceso");
            dto.COD_CATEGORIA_OCUPACIONAL = dto.COD_CATEGORIA_OCUPACIONAL.Trim().ToUpperInvariant();
            await _repo.PersonalGuardarAsync(dto, nuevo, codUsuario, codEstacion);
        }, nameof(PersonalGuardarAsync));

    public Task<ApiResponse<bool>> PersonalEliminarAsync(int codEntidad) => EjecutarSin(() => _repo.PersonalEliminarAsync(codEntidad), nameof(PersonalEliminarAsync));
    public Task<ApiResponse<List<PersonaDisponible>>> PersonalDisponiblesAsync() => Ejecutar(_repo.PersonalDisponiblesAsync, nameof(PersonalDisponiblesAsync));
    public Task<ApiResponse<List<PersonalResumenItem>>> PersonalResumenAsync() => Ejecutar(_repo.PersonalResumenAsync, nameof(PersonalResumenAsync));
    public Task<ApiResponse<Dictionary<string, List<CatalogoOperacionItem>>>> PersonalCombosAsync() => Ejecutar(_repo.PersonalCombosAsync, nameof(PersonalCombosAsync));

    // ===== O6 =====
    public Task<ApiResponse<List<Armado>>> ArmadosAsync(string? buscar) => Ejecutar(() => _repo.ArmadosAsync(buscar), nameof(ArmadosAsync));

    public Task<ApiResponse<Armado>> ArmadoAsync(string cod) =>
        Ejecutar(async () => await _repo.ArmadoAsync(cod) ?? throw new ValidacionException("El armado no existe."), nameof(ArmadoAsync));

    public Task<ApiResponse<bool>> ArmadoGuardarAsync(ArmadoGuardarDto dto, bool nuevo, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            Req(dto.COD_ARMADO, "El codigo"); Req(dto.DES_ARMADO, "El nombre"); Req(dto.COD_RUTA_PRODUCCION, "La ruta"); Req(dto.COD_PROCESO, "El proceso"); Req(dto.COD_TIPO_DISTRIBUCION, "La disposicion");
            dto.COD_ARMADO = dto.COD_ARMADO.Trim().ToUpperInvariant(); dto.DES_ARMADO = dto.DES_ARMADO.Trim().ToUpperInvariant();
            if (dto.VAL_MINUTOS_TURNO is <= 0) throw new ValidacionException("Los minutos del turno deben ser mayores que cero.");
            if (dto.TAS_EFICIENCIA is <= 0 or > 100) throw new ValidacionException("La eficiencia debe estar entre 0 y 100 por ciento.");
            if (dto.VAL_META_UNIDADES is <= 0) throw new ValidacionException("La meta de unidades debe ser mayor que cero (o dejarla vacia).");
            await _repo.ArmadoGuardarAsync(dto, nuevo, codUsuario, codEstacion);
        }, nameof(ArmadoGuardarAsync));

    public Task<ApiResponse<bool>> ArmadoEliminarAsync(string cod) => EjecutarSin(() => _repo.ArmadoEliminarAsync(cod), nameof(ArmadoEliminarAsync));
    public Task<ApiResponse<string>> ArmadoSiguienteCodigoAsync() => Ejecutar(_repo.ArmadoSiguienteCodigoAsync, nameof(ArmadoSiguienteCodigoAsync));
    public Task<ApiResponse<List<ArmadoPuesto>>> ArmadoPuestosAsync(string cod) => Ejecutar(() => _repo.ArmadoPuestosAsync(cod), nameof(ArmadoPuestosAsync));
    public Task<ApiResponse<List<ArmadoHoja>>> ArmadoHojasAsync(string cod) => Ejecutar(() => _repo.ArmadoHojasAsync(cod), nameof(ArmadoHojasAsync));
    public Task<ApiResponse<List<ArmadoAviso>>> ArmadoAvisosAsync(string cod) => Ejecutar(() => _repo.ArmadoAvisosAsync(cod), nameof(ArmadoAvisosAsync));
    public Task<ApiResponse<List<ArmadoOperariosCategoria>>> ArmadoOperariosPorCategoriaAsync(string cod) => Ejecutar(() => _repo.ArmadoOperariosPorCategoriaAsync(cod), nameof(ArmadoOperariosPorCategoriaAsync));

    public Task<ApiResponse<bool>> ArmadoPuestosGuardarAsync(ArmadoPuestosGuardarDto dto, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            Req(dto.COD_ARMADO, "El armado");
            if (dto.Puestos == null) throw new ValidacionException("La lista de puestos es obligatoria.");
            foreach (var p in dto.Puestos) { p.DES_PUESTO = (p.DES_PUESTO ?? "").Trim(); p.HOJAS ??= new(); }
            await _repo.ArmadoPuestosGuardarAsync(dto, codUsuario, codEstacion);
        }, nameof(ArmadoPuestosGuardarAsync));

    public Task<ApiResponse<ArmadoSugerido>> ArmadoSugerirAsync(string cod, string codUsuario, string codEstacion) => Ejecutar(() => _repo.ArmadoSugerirAsync(cod, codUsuario, codEstacion), nameof(ArmadoSugerirAsync));
    public Task<ApiResponse<Dictionary<string, List<CatalogoOperacionItem>>>> ArmadoCombosAsync() => Ejecutar(_repo.ArmadoCombosAsync, nameof(ArmadoCombosAsync));

    // ===== O7 =====
    public Task<ApiResponse<List<ArmadoPersonaAsignada>>> ArmadoPersonalAsignadoAsync(string cod) => Ejecutar(() => _repo.ArmadoPersonalAsignadoAsync(cod), nameof(ArmadoPersonalAsignadoAsync));
    public Task<ApiResponse<List<ArmadoPersonaDisponible>>> ArmadoPersonalDisponibleAsync(string cod) => Ejecutar(() => _repo.ArmadoPersonalDisponibleAsync(cod), nameof(ArmadoPersonalDisponibleAsync));
    public Task<ApiResponse<List<ArmadoCobertura>>> ArmadoCoberturaAsync(string cod) => Ejecutar(() => _repo.ArmadoCoberturaAsync(cod), nameof(ArmadoCoberturaAsync));

    public Task<ApiResponse<bool>> ArmadoPersonalGuardarAsync(ArmadoPersonalGuardarDto dto, string codUsuario, string codEstacion) =>
        EjecutarSin(async () =>
        {
            Req(dto.COD_ARMADO, "El armado");
            if (dto.Puestos == null) throw new ValidacionException("La lista de personal es obligatoria.");
            foreach (var p in dto.Puestos) p.PERSONAS ??= new();
            await _repo.ArmadoPersonalGuardarAsync(dto, codUsuario, codEstacion);
        }, nameof(ArmadoPersonalGuardarAsync));

    public Task<ApiResponse<ArmadoPersonalSugerido>> ArmadoPersonalSugerirAsync(string cod, string codUsuario, string codEstacion) => Ejecutar(() => _repo.ArmadoPersonalSugerirAsync(cod, codUsuario, codEstacion), nameof(ArmadoPersonalSugerirAsync));
    public Task<ApiResponse<ArmadoComparacionResultado>> ArmadoCompararAsync(string cod) => Ejecutar(() => _repo.ArmadoCompararAsync(cod), nameof(ArmadoCompararAsync));
}
