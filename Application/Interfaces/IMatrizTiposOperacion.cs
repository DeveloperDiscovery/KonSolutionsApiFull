using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;

namespace KonSolutionsApi.Application.Interfaces;

public interface IMatrizTiposOperacionRepository
{
    Task<IEnumerable<MatrizCalculoCelda>> CalculoAsync(string codClaseOperacion);
    Task<MatrizSetResultado> CalculoSetAsync(MatrizCalculoSetDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<MatrizComprobanteCelda>> ComprobantesAsync(string codClaseOperacion);
    Task<MatrizSetResultado> ComprobanteSetAsync(MatrizComprobanteSetDto dto, string codUsuario, string codEstacion);
    Task<IEnumerable<FormulaCelda>> FormulasAsync(string codClaseOperacion);
    Task<FormulaSetResultado> FormulaSetAsync(FormulaSetDto dto, string codUsuario, string codEstacion);
}

public interface IMatrizTiposOperacionService
{
    Task<ApiResponse<IEnumerable<MatrizCalculoCelda>>> CalculoAsync(string codClaseOperacion);
    Task<ApiResponse<MatrizSetResultado>> CalculoSetAsync(MatrizCalculoSetDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<MatrizComprobanteCelda>>> ComprobantesAsync(string codClaseOperacion);
    Task<ApiResponse<MatrizSetResultado>> ComprobanteSetAsync(MatrizComprobanteSetDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<IEnumerable<FormulaCelda>>> FormulasAsync(string codClaseOperacion);
    Task<ApiResponse<FormulaSetResultado>> FormulaSetAsync(FormulaSetDto dto, string codUsuario, string codEstacion);
}
