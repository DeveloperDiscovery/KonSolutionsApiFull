using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Services;

public class MatrizTiposOperacionService : IMatrizTiposOperacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(MatrizTiposOperacionService);
    private readonly IMatrizTiposOperacionRepository _repo;
    private readonly ErrorLogService _log;
    public MatrizTiposOperacionService(IMatrizTiposOperacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    // 523xx (52301 a 52307) son validaciones del procedimiento (tipo o valor inexistente): se devuelven como mensaje, no como falla del sistema.
    private static bool EsValidacion(SqlException ex) => ex.Number is >= 52301 and <= 52307;

    public async Task<ApiResponse<IEnumerable<MatrizCalculoCelda>>> CalculoAsync(string codClaseOperacion)
    {
        try { var d = (await _repo.CalculoAsync(codClaseOperacion)).ToList(); return ApiResponse<IEnumerable<MatrizCalculoCelda>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CalculoAsync)); return ApiResponse<IEnumerable<MatrizCalculoCelda>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MatrizSetResultado>> CalculoSetAsync(MatrizCalculoSetDto dto, string codUsuario, string codEstacion)
    {
        try { return ApiResponse<MatrizSetResultado>.Ok(await _repo.CalculoSetAsync(dto, codUsuario, codEstacion)); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<MatrizSetResultado>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(CalculoSetAsync)); return ApiResponse<MatrizSetResultado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<MatrizComprobanteCelda>>> ComprobantesAsync(string codClaseOperacion)
    {
        try { var d = (await _repo.ComprobantesAsync(codClaseOperacion)).ToList(); return ApiResponse<IEnumerable<MatrizComprobanteCelda>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ComprobantesAsync)); return ApiResponse<IEnumerable<MatrizComprobanteCelda>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<MatrizSetResultado>> ComprobanteSetAsync(MatrizComprobanteSetDto dto, string codUsuario, string codEstacion)
    {
        try { return ApiResponse<MatrizSetResultado>.Ok(await _repo.ComprobanteSetAsync(dto, codUsuario, codEstacion)); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<MatrizSetResultado>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(ComprobanteSetAsync)); return ApiResponse<MatrizSetResultado>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<IEnumerable<FormulaCelda>>> FormulasAsync(string codClaseOperacion)
    {
        try { var d = (await _repo.FormulasAsync(codClaseOperacion)).ToList(); return ApiResponse<IEnumerable<FormulaCelda>>.Ok(d, total: d.Count); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(FormulasAsync)); return ApiResponse<IEnumerable<FormulaCelda>>.Fail(ex.Message); }
    }

    public async Task<ApiResponse<FormulaSetResultado>> FormulaSetAsync(FormulaSetDto dto, string codUsuario, string codEstacion)
    {
        try { return ApiResponse<FormulaSetResultado>.Ok(await _repo.FormulaSetAsync(dto, codUsuario, codEstacion)); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<FormulaSetResultado>.Fail(ex.Message); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, nameof(FormulaSetAsync)); return ApiResponse<FormulaSetResultado>.Fail(ex.Message); }
    }
}
