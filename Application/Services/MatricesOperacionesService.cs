using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

/// <summary>Matrices de operaciones y su asignacion a versiones de diseno (script SQL 130): valida lo basico y traduce los errores de
/// validacion de la base a mensajes para el usuario.</summary>
public class MatricesOperacionesService : IMatricesOperacionesService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MatricesOperacionesService);
    private readonly IMatricesOperacionesRepository _repo;
    private readonly ErrorLogService _log;
    public MatricesOperacionesService(IMatricesOperacionesRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    /// <summary>Errores que son del usuario: 52401 y 52440 a 52447 (version bloqueada, no existe, ya tiene ruta...) y 52620 a 52629 (matrices).</summary>
    private static bool EsValidacion(SqlException ex) => ex.Number is 52401 or (>= 52440 and <= 52447) or (>= 52620 and <= 52629);
    private sealed class ValidacionException : Exception { public ValidacionException(string m) : base(m) { } }

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (ValidacionException ex) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (ex.Number == 547) { return ApiResponse<T>.Fail("El registro esta en uso o el dato indicado no existe."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    private async Task<ApiResponse<bool>> EjecutarSin(Func<Task> accion, string metodo) => await Ejecutar(async () => { await accion(); return true; }, metodo);

    private static void Req(string? valor, string que) { if (string.IsNullOrWhiteSpace(valor)) throw new ValidacionException(que + " es obligatorio."); }

    /// <summary>La sub familia son los tres codigos juntos o ninguno.</summary>
    private static void SubFamilia(string? g, string? f, string? s)
    {
        var n = new[] { g, f, s }.Count(x => !string.IsNullOrWhiteSpace(x));
        if (n is not (0 or 3)) throw new ValidacionException("Indique el grupo, la familia y la sub familia completos, o ninguno.");
    }

    private static void Limpiar(MatrizGuardarDto d)
    {
        d.DES_RUTA = (d.DES_RUTA ?? "").Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(d.COD_TIPO_RUTA)) d.COD_TIPO_RUTA = "PRD";
        if (string.IsNullOrWhiteSpace(d.COD_GRUPO_PRODUCTO)) d.COD_GRUPO_PRODUCTO = null;
        if (string.IsNullOrWhiteSpace(d.COD_FAMILIA_PRODUCTO)) d.COD_FAMILIA_PRODUCTO = null;
        if (string.IsNullOrWhiteSpace(d.COD_SUB_FAMILIA_PRODUCTO)) d.COD_SUB_FAMILIA_PRODUCTO = null;
    }

    public Task<ApiResponse<List<MatrizResumen>>> MatricesAsync() => Ejecutar(_repo.MatricesAsync, nameof(MatricesAsync));

    public Task<ApiResponse<string>> MatrizCrearAsync(MatrizGuardarDto dto, string u, string e) =>
        Ejecutar(() =>
        {
            Limpiar(dto); Req(dto.DES_RUTA, "El nombre de la matriz");
            if (dto.DES_RUTA.Length > 50) throw new ValidacionException("El nombre de la matriz admite como maximo 50 caracteres.");
            SubFamilia(dto.COD_GRUPO_PRODUCTO, dto.COD_FAMILIA_PRODUCTO, dto.COD_SUB_FAMILIA_PRODUCTO);
            return _repo.MatrizCrearAsync(dto, u, e);
        }, nameof(MatrizCrearAsync));

    public Task<ApiResponse<bool>> MatrizActualizarAsync(MatrizGuardarDto dto, string u, string e) =>
        EjecutarSin(() =>
        {
            Limpiar(dto); Req(dto.COD_RUTA_PRODUCCION, "El codigo de la matriz"); Req(dto.DES_RUTA, "El nombre de la matriz");
            if (dto.DES_RUTA.Length > 50) throw new ValidacionException("El nombre de la matriz admite como maximo 50 caracteres.");
            SubFamilia(dto.COD_GRUPO_PRODUCTO, dto.COD_FAMILIA_PRODUCTO, dto.COD_SUB_FAMILIA_PRODUCTO);
            return _repo.MatrizActualizarAsync(dto, u, e);
        }, nameof(MatrizActualizarAsync));

    public Task<ApiResponse<bool>> MatrizEliminarAsync(string cod) => EjecutarSin(() => _repo.MatrizEliminarAsync(cod), nameof(MatrizEliminarAsync));

    public Task<ApiResponse<MatrizAsignarResultado>> MatrizDuplicarAsync(MatrizDuplicarDto dto, string u, string e) =>
        Ejecutar(() => { Req(dto.COD_RUTA_PRODUCCION, "La matriz"); return _repo.MatrizDuplicarAsync(dto, u, e); }, nameof(MatrizDuplicarAsync));

    public Task<ApiResponse<List<MatrizParaVersion>>> MatricesParaVersionAsync(string codDiseno, int numVersion) =>
        Ejecutar(() => _repo.MatricesParaVersionAsync(codDiseno, numVersion), nameof(MatricesParaVersionAsync));

    public Task<ApiResponse<MatrizAsignarResultado>> MatrizAsignarAsync(MatrizAsignarDto dto, string u, string e) =>
        Ejecutar(() => { Req(dto.COD_DISENO, "El diseno"); Req(dto.COD_MATRIZ, "La matriz"); return _repo.MatrizAsignarAsync(dto, u, e); }, nameof(MatrizAsignarAsync));

    public Task<ApiResponse<MatrizAsignarResultado>> GuardarComoMatrizAsync(GuardarComoMatrizDto dto, string u, string e) =>
        Ejecutar(() =>
        {
            Req(dto.DES_MATRIZ, "El nombre de la matriz"); dto.DES_MATRIZ = dto.DES_MATRIZ.Trim().ToUpperInvariant();
            SubFamilia(dto.COD_GRUPO_PRODUCTO, dto.COD_FAMILIA_PRODUCTO, dto.COD_SUB_FAMILIA_PRODUCTO);
            return _repo.GuardarComoMatrizAsync(dto, u, e);
        }, nameof(GuardarComoMatrizAsync));

    public Task<ApiResponse<RutaIndependizarResultado>> RutaIndependizarAsync(string codDiseno, int numVersion, string u, string e) =>
        Ejecutar(() => _repo.RutaIndependizarAsync(codDiseno, numVersion, u, e), nameof(RutaIndependizarAsync));

    public Task<ApiResponse<List<RutaCompartida>>> RutasCompartidasAsync() => Ejecutar(_repo.RutasCompartidasAsync, nameof(RutasCompartidasAsync));

    public Task<ApiResponse<DisenosRutaOperacionesInfo>> RutaOperacionesInfoAsync(string codDiseno, int numVersion) =>
        Ejecutar(() => _repo.RutaOperacionesInfoAsync(codDiseno, numVersion), nameof(RutaOperacionesInfoAsync));
}
