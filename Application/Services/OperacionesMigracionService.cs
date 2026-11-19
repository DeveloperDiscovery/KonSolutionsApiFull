using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Services;

/// <summary>Migracion asistida de las operaciones de las rutas existentes al catalogo (script SQL 128): valida la lista de decisiones y traduce los errores de validacion de la base.</summary>
public class OperacionesMigracionService : IOperacionesMigracionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(OperacionesMigracionService);
    private readonly IOperacionesMigracionRepository _repo;
    private readonly ErrorLogService _log;
    public OperacionesMigracionService(IOperacionesMigracionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private static bool EsValidacion(SqlException ex) => ex.Number is 52540 or (>= 52610 and <= 52619);
    private sealed class ValidacionException : Exception { public ValidacionException(string m) : base(m) { } }

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (ValidacionException ex) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (ex.Number == 547) { return ApiResponse<T>.Fail("Un dato indicado no existe en su catalogo o esta en uso."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    public Task<ApiResponse<MigracionResumen>> ResumenAsync() => Ejecutar(_repo.ResumenAsync, nameof(ResumenAsync));
    public Task<ApiResponse<List<MigracionGrupo>>> PropuestaAsync(string? ruta) => Ejecutar(() => _repo.PropuestaAsync(ruta), nameof(PropuestaAsync));

    public Task<ApiResponse<MigracionResultado>> AplicarAsync(MigracionAplicarDto dto, string codUsuario, string codEstacion) =>
        Ejecutar(async () =>
        {
            if (dto.Grupos == null || dto.Grupos.Count == 0) throw new ValidacionException("Elige al menos un grupo para migrar.");
            foreach (var g in dto.Grupos)
            {
                g.ACCION = (g.ACCION ?? "").Trim().ToUpperInvariant();
                if (g.ACCION is not ("EXISTENTE" or "NUEVA")) throw new ValidacionException("La accion de cada grupo debe ser EXISTENTE o NUEVA.");
                if (g.ACCION == "EXISTENTE" && string.IsNullOrWhiteSpace(g.COD_OPERACION)) throw new ValidacionException("Un grupo que usa una operacion existente necesita su codigo.");
            }
            return await _repo.AplicarAsync(dto, codUsuario, codEstacion);
        }, nameof(AplicarAsync));

    public Task<ApiResponse<MigracionDeshacerResultado>> DeshacerAsync(string? ruta, string codUsuario, string codEstacion) => Ejecutar(() => _repo.DeshacerAsync(ruta, codUsuario, codEstacion), nameof(DeshacerAsync));
}
