using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;

namespace KonSolutionsApi.Application.Interfaces;

public interface IDisenosTizadoRepository
{
    Task<List<DisenosMolde>> MoldesAsync(string codDiseno, int numVersion);
    Task<List<int>> GuardarMoldesAsync(string codDiseno, int numVersion, IEnumerable<object> filas, string codUsuario, string codEstacion);
    Task<int> EliminarMoldeAsync(DisenosMoldeClaveDto clave);
    Task<List<DisenosTizadoResumen>> TizadosAsync(string codDiseno, int numVersion);
    Task<DisenosTizadoFila> TizadoAsync(string codDiseno, int numVersion, int numSecuencia);
    Task<int> GuardarTizadoAsync(string codDiseno, int numVersion, string nombre, DisenosTizadoCalcularDto p, string tallas, int prendas, int piezas, int noColocadas,
                                 double largo, double eficiencia, double? consumo, string resultadoJson, string codUsuario, string codEstacion);
    Task<int> EliminarTizadoAsync(DisenosMoldeClaveDto clave);
    Task<List<DisenosGradacionRegla>> ReglasGradacionAsync(string codDiseno, int numVersion);
    Task GuardarReglasGradacionAsync(string codDiseno, int numVersion, IEnumerable<object> filas, string codUsuario, string codEstacion);
    Task<int> EliminarReglaGradacionAsync(DisenosGradacionClaveDto clave);
}

public interface IDisenosTizadoService
{
    Task<ApiResponse<List<DisenosMolde>>> MoldesAsync(string codDiseno, int numVersion);
    Task<ApiResponse<DisenosMoldesGuardarResultado>> GuardarMoldesAsync(DisenosMoldesGuardarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<DisenosMoldeEliminarResultado>> EliminarMoldeAsync(DisenosMoldeClaveDto clave);
    Task<ApiResponse<DisenosMoldeImportarResultado>> ImportarDxfAsync(DisenosMoldeImportarDto dto);
    Task<ApiResponse<DisenosTizadoCalculo>> CalcularAsync(DisenosTizadoCalcularDto dto, CancellationToken ct);
    Task<ApiResponse<DisenosTizadoCalculo>> GuardarTizadoAsync(DisenosTizadoCalcularDto dto, string codUsuario, string codEstacion, CancellationToken ct);
    Task<ApiResponse<List<DisenosTizadoResumen>>> TizadosAsync(string codDiseno, int numVersion);
    Task<ApiResponse<DisenosTizadoCalculo>> TizadoAsync(string codDiseno, int numVersion, int numSecuencia);
    Task<ApiResponse<string>> TizadoDxfAsync(string codDiseno, int numVersion, int numSecuencia);
    Task<ApiResponse<DisenosTizadoEliminarResultado>> EliminarTizadoAsync(DisenosMoldeClaveDto clave);
    Task<ApiResponse<DisenosTizadoCalculo>> AjustarTizadoAsync(DisenosTizadoAjusteDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<List<DisenosGradacionRegla>>> ReglasGradacionAsync(string codDiseno, int numVersion);
    Task<ApiResponse<DisenosGradacionPrevia>> PrevisualizarGradacionAsync(DisenosGradacionDto dto);
    Task<ApiResponse<DisenosGradacionResultado>> GenerarGradacionAsync(DisenosGradacionDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<DisenosGradacionEliminarResultado>> EliminarReglaGradacionAsync(DisenosGradacionClaveDto clave);
}
