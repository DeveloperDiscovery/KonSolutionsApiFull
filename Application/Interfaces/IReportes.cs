using KonSolutionsApi.Application.Reportes;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Reportes;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Un parámetro listo para ejecutar el SP de la fuente: nombre con @, tipo de SQL y valor ya convertido.</summary>
public sealed record ReporteParametroEjecucion(string Nombre, string? TipoSql, object Valor, int? Longitud, int? Precision, int? Escala);

public interface IReportesRepository
{
    // Catálogo
    Task<List<ReporteProcedimiento>> ProcedimientosAsync(string? buscar);
    Task<List<ReporteParametroSp>> ParametrosSpAsync(string esquema, string procedimiento);
    Task<List<ReporteEtiqueta>> EtiquetasAsync(IEnumerable<string> columnas);
    // Fuentes
    Task<List<ReporteFuente>> FuentesAsync();
    Task<ReporteFuenteDetalle?> FuenteAsync(int numFuente);
    Task<int> FuenteGrabarAsync(ReporteFuenteDetalle d, string codUsuario, string codEstacion);
    Task FuenteEliminarAsync(int numFuente);
    Task<int> FuenteColumnasAgregarAsync(int numFuente, IEnumerable<ReporteFuenteColumna> columnas);
    // Diseños
    Task<List<ReporteDiseno>> DisenosAsync(int? numFuente);
    Task<ReporteDisenoDetalle?> DisenoAsync(int numDiseno);
    Task<int> DisenoGrabarAsync(ReporteDisenoDetalle d, string codUsuario, string codEstacion);
    Task DisenoEliminarAsync(int numDiseno);
    Task<ReporteLienzo> LienzoAsync(int numDiseno);
    Task LienzoGrabarAsync(int numDiseno, ReporteLienzo lienzo, string codUsuario, string codEstacion);
    Task PlantillaGrabarAsync(int numDiseno, string formato, byte[]? contenido, string? nombre, string codUsuario, string codEstacion);
    Task<(byte[]? Contenido, string? Nombre)> PlantillaAsync(int numDiseno, string formato);
    // Botones
    Task<List<ReporteBoton>> BotonesAsync(string? codAplicacion, string? buscar, bool soloAsignados);
    Task<ReporteBotonDetalle?> BotonAsync(string codAplicacion, string codOpcion, string codBoton, int? numDiseno);
    Task BotonGrabarAsync(ReporteBotonGrabarDto dto, string codUsuario, string codEstacion);
    Task BotonEliminarAsync(string codAplicacion, string codOpcion, string codBoton);
    Task<List<ReporteRuta>> RutasAsync();
    // Ejecución
    Task<List<ReporteOpcionBoton>> OpcionBotonesAsync(string codAplicacion, string codOpcion, string codRol);
    Task<ReporteDefinicion> DefinicionAsync(int? numDiseno, string? codAplicacion, string? codOpcion, string? codBoton, string? codRol, string? codUsuario);
    Task<ReporteEmpresa?> EmpresaAsync(string codUsuario);
    Task<ReporteDatos> EjecutarAsync(string esquema, string procedimiento, IEnumerable<ReporteParametroEjecucion> parametros, int segundos, int maxFilas);
    Task<List<ComboboxItem>> ComboAsync(string procedimiento, IReadOnlyDictionary<string, object?> valores, string? columnaId, string? columnaTexto);
}

public interface IReportesService
{
    // Catálogo
    Task<ApiResponse<List<ReporteProcedimiento>>> ProcedimientosAsync(string? buscar);
    Task<ApiResponse<List<ReporteParametroSp>>> ParametrosSpAsync(string? esquema, string? procedimiento);
    Task<ApiResponse<List<ReporteEtiqueta>>> EtiquetasAsync(List<string>? columnas);
    Task<ApiResponse<ReportePrueba>> ProbarAsync(ReportePruebaDto dto, string codUsuario, string? codRol, string codEstacion);
    // Fuentes
    Task<ApiResponse<List<ReporteFuente>>> FuentesAsync();
    Task<ApiResponse<ReporteFuenteDetalle>> FuenteAsync(int numFuente);
    Task<ApiResponse<int>> FuenteGrabarAsync(ReporteFuenteDetalle d, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> FuenteEliminarAsync(int numFuente);
    // Diseños
    Task<ApiResponse<List<ReporteDiseno>>> DisenosAsync(int? numFuente);
    Task<ApiResponse<ReporteDisenoDetalle>> DisenoAsync(int numDiseno);
    Task<ApiResponse<int>> DisenoGrabarAsync(ReporteDisenoDetalle d, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> DisenoEliminarAsync(int numDiseno);
    /// <summary>Crea un diseño libre (bandas) a partir de un listado o una ficha. Devuelve su número.</summary>
    Task<ApiResponse<int>> DisenoConvertirAsync(int numDiseno, string codUsuario, string codEstacion);
    Task<ApiResponse<ReporteFormulaResultado>> FormulaComprobarAsync(ReporteFormulaComprobarDto dto, string codUsuario, string? codRol, string codEstacion);
    ApiResponse<List<ReporteFormulaFuncion>> FormulaFunciones();
    Task<ApiResponse<ReporteArchivo>> PlantillaBaseAsync(int numDiseno, string formato, string codUsuario);
    Task<ApiResponse<ReporteArchivo>> PlantillaDescargarAsync(int numDiseno, string formato);
    Task<ApiResponse<ReportePlantillaResultado>> PlantillaSubirAsync(int numDiseno, string formato, ReportePlantillaDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> PlantillaQuitarAsync(int numDiseno, string formato, string codUsuario, string codEstacion);
    // Botones
    Task<ApiResponse<List<ReporteBoton>>> BotonesAsync(string? codAplicacion, string? buscar, bool soloAsignados);
    Task<ApiResponse<ReporteBotonDetalle>> BotonAsync(string? codAplicacion, string? codOpcion, string? codBoton, int? numDiseno);
    Task<ApiResponse<bool>> BotonGrabarAsync(ReporteBotonGrabarDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<bool>> BotonEliminarAsync(string? codAplicacion, string? codOpcion, string? codBoton);
    Task<ApiResponse<List<ReporteRuta>>> RutasAsync();
    // Ejecución (por botón o, en la vista previa del diseñador, por diseño)
    Task<ApiResponse<List<ReporteOpcionBoton>>> OpcionBotonesAsync(string? codAplicacion, string? codOpcion, string? codRol);
    Task<ApiResponse<ReporteDialogo>> DialogoAsync(int? numDiseno, string? codAplicacion, string? codOpcion, string? codBoton, string? codRol, string codUsuario);
    Task<ApiResponse<List<ComboboxItem>>> ComboAsync(ReporteComboDto dto, string codUsuario, string? codRolToken, string codEstacion);
    Task<ApiResponse<ReporteArchivo>> GenerarAsync(ReporteGenerarDto dto, string codUsuario, string? codRolToken, string codEstacion);
}
