using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Formulacion;

namespace KonSolutionsApi.Application.Interfaces;

/// <summary>Constructor de formulas (script SQL 145): componentes, elementos y formulas de consumo. Las validaciones salen como SqlException (52700 a 52729).</summary>
public interface IFormulacionRepository
{
    // componentes
    Task<List<FormulacionComponente>> ComponentesAsync();
    Task<FormulacionComponente?> ComponenteAsync(string cod);
    Task<WriteResult> ComponenteInsertAsync(FormulacionComponenteGuardarDto d, string u, string e);
    Task<WriteResult> ComponenteUpdateAsync(FormulacionComponenteGuardarDto d, string u, string e);
    Task<WriteResult> ComponenteDeleteAsync(string cod);
    // elementos
    Task<List<FormulacionElemento>> ElementosAsync(string? codComponente);
    Task<FormulacionElemento?> ElementoAsync(string codComponente, string codElemento);
    Task<WriteResult> ElementoInsertAsync(FormulacionElementoGuardarDto d, string u, string e);
    Task<WriteResult> ElementoUpdateAsync(FormulacionElementoGuardarDto d, string u, string e);
    Task<WriteResult> ElementoDeleteAsync(string codComponente, string codElemento);
    // formulas
    Task<List<FormulaCabecera>> FormulasAsync(string? search);
    Task<FormulaCabecera?> FormulaAsync(int num);
    Task<List<FormulaFicha>> FormulaFichasAsync(int num);
    Task<WriteResult> FormulaSaveAsync(FormulaGuardarDto d, string fichasJson, string u, string e);
    Task<WriteResult> FormulaCopiarAsync(int num, string u, string e);
    Task<WriteResult> FormulaDeleteAsync(int num);
    Task<List<FormulaError>> FormulaValidarAsync(string fichasJson);
    Task<List<PaletaFicha>> PaletaAsync(PaletaConsultaDto d);
}

public interface IFormulacionService
{
    Task<ApiResponse<List<FormulacionComponente>>> ComponentesAsync();
    Task<ApiResponse<FormulacionComponente>> ComponenteAsync(string cod);
    Task<ApiResponse<WriteResult>> ComponenteCrearAsync(FormulacionComponenteGuardarDto d, string u, string e);
    Task<ApiResponse<WriteResult>> ComponenteActualizarAsync(FormulacionComponenteGuardarDto d, string u, string e);
    Task<ApiResponse<WriteResult>> ComponenteEliminarAsync(string cod);

    Task<ApiResponse<List<FormulacionElemento>>> ElementosAsync(string? codComponente);
    Task<ApiResponse<FormulacionElemento>> ElementoAsync(string codComponente, string codElemento);
    Task<ApiResponse<WriteResult>> ElementoCrearAsync(FormulacionElementoGuardarDto d, string u, string e);
    Task<ApiResponse<WriteResult>> ElementoActualizarAsync(FormulacionElementoGuardarDto d, string u, string e);
    Task<ApiResponse<WriteResult>> ElementoEliminarAsync(string codComponente, string codElemento);

    Task<ApiResponse<List<FormulaCabecera>>> FormulasAsync(string? search);
    Task<ApiResponse<FormulaDetalle>> FormulaAsync(int num);
    Task<ApiResponse<WriteResult>> FormulaGuardarAsync(FormulaGuardarDto d, string u, string e);
    Task<ApiResponse<WriteResult>> FormulaCopiarAsync(int num, string u, string e);
    Task<ApiResponse<WriteResult>> FormulaEliminarAsync(int num);
    Task<ApiResponse<FormulaValidacion>> FormulaValidarAsync(FormulaValidarDto d);
    Task<ApiResponse<List<PaletaFicha>>> PaletaAsync(PaletaConsultaDto d);
}
