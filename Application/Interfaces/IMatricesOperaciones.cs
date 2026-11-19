using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Matrices de operaciones y su asignacion a versiones de diseno (script SQL 130).</summary>
public interface IMatricesOperacionesRepository
{
    Task<List<MatrizResumen>> MatricesAsync();
    Task<string> MatrizCrearAsync(MatrizGuardarDto dto, string codUsuario, string codEstacion);
    Task MatrizActualizarAsync(MatrizGuardarDto dto, string codUsuario, string codEstacion);
    Task MatrizEliminarAsync(string cod);
    Task<MatrizAsignarResultado> MatrizDuplicarAsync(MatrizDuplicarDto dto, string codUsuario, string codEstacion);
    Task<List<MatrizParaVersion>> MatricesParaVersionAsync(string codDiseno, int numVersion);
    Task<MatrizAsignarResultado> MatrizAsignarAsync(MatrizAsignarDto dto, string codUsuario, string codEstacion);
    Task<MatrizAsignarResultado> GuardarComoMatrizAsync(GuardarComoMatrizDto dto, string codUsuario, string codEstacion);
    Task<RutaIndependizarResultado> RutaIndependizarAsync(string codDiseno, int numVersion, string codUsuario, string codEstacion);
    Task<List<RutaCompartida>> RutasCompartidasAsync();
    Task<DisenosRutaOperacionesInfo> RutaOperacionesInfoAsync(string codDiseno, int numVersion);
}

public interface IMatricesOperacionesService
{
    Task<ApiResponse<List<MatrizResumen>>> MatricesAsync();
    Task<ApiResponse<string>> MatrizCrearAsync(MatrizGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> MatrizActualizarAsync(MatrizGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> MatrizEliminarAsync(string cod);
    Task<ApiResponse<MatrizAsignarResultado>> MatrizDuplicarAsync(MatrizDuplicarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<List<MatrizParaVersion>>> MatricesParaVersionAsync(string codDiseno, int numVersion);
    Task<ApiResponse<MatrizAsignarResultado>> MatrizAsignarAsync(MatrizAsignarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<MatrizAsignarResultado>> GuardarComoMatrizAsync(GuardarComoMatrizDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<RutaIndependizarResultado>> RutaIndependizarAsync(string codDiseno, int numVersion, string codUsuario, string codEstacion);
    Task<ApiResponse<List<RutaCompartida>>> RutasCompartidasAsync();
    Task<ApiResponse<DisenosRutaOperacionesInfo>> RutaOperacionesInfoAsync(string codDiseno, int numVersion);
}
