using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Formulacion;

namespace KonSolutionsApi.Application.Services;

/// <summary>Constructor de formulas (script SQL 145): valida lo basico (largos que la base cortaria sin avisar, colores, sub familias completas, fichas)
/// y traduce los errores de validacion de la base (52700 a 52729) a mensajes para el usuario. No calcula formulas: eso es una etapa posterior.</summary>
public class FormulacionService : IFormulacionService
{
    private const string Programa = "KonSolutionsApi";
    private const string Clase = nameof(FormulacionService);
    /// <summary>Tope de fichas de una formula (la base guarda el orden de presentacion en un tinyint).</summary>
    public const int MaxFichas = 250;
    private static readonly Regex Color = new(@"^(#[0-9A-Fa-f]{3}|#[0-9A-Fa-f]{6}|#[0-9A-Fa-f]{8}|[A-Za-z]{3,30}|rgba?\([0-9., %]+\))$", RegexOptions.Compiled);
    private static readonly JsonSerializerOptions JsonOpt = new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };

    private readonly IFormulacionRepository _repo;
    private readonly ErrorLogService _log;
    public FormulacionService(IFormulacionRepository repo, ErrorLogService log) { _repo = repo; _log = log; }

    private static bool EsValidacion(SqlException ex) => ex.Number is >= 52700 and <= 52729;
    private sealed class ValidacionException : Exception { public ValidacionException(string m) : base(m) { } }

    private async Task<ApiResponse<T>> Ejecutar<T>(Func<Task<T>> accion, string metodo)
    {
        try { return ApiResponse<T>.Ok(await accion()); }
        catch (ValidacionException ex) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (EsValidacion(ex)) { return ApiResponse<T>.Fail(ex.Message); }
        catch (SqlException ex) when (ex.Number == 547) { return ApiResponse<T>.Fail("El registro esta en uso o el dato indicado no existe."); }
        catch (Exception ex) { _log.LogError(ex, Programa, Clase, metodo); return ApiResponse<T>.Fail(ex.Message); }
    }

    /// <summary>Las escrituras devuelven un WriteResult: si la base lo marca como error, sale como error.</summary>
    private async Task<ApiResponse<WriteResult>> Escribir(Func<Task<WriteResult>> accion, string metodo, string ok)
    {
        var r = await Ejecutar(accion, metodo);
        return r.Success && r.Data is not null ? ApiResponse<WriteResult>.FromWriteResult(r.Data, ok) : ApiResponse<WriteResult>.Fail(r.Message ?? "No se pudo completar la operacion.");
    }

    private static void Req(string? valor, string que) { if (string.IsNullOrWhiteSpace(valor)) throw new ValidacionException(que + " es obligatorio."); }
    private static void Largo(string? valor, int max, string que) { if (valor is not null && valor.Trim().Length > max) throw new ValidacionException($"{que} admite como maximo {max} caracteres."); }
    private static void Hex(string? valor, string que) { if (!string.IsNullOrWhiteSpace(valor) && !Color.IsMatch(valor.Trim())) throw new ValidacionException($"{que} debe ser un color: #RRGGBB, un nombre como Green o rgb(...)."); }
    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    /// <summary>La sub familia son los tres codigos juntos o ninguno.</summary>
    private static void SubFamilia(string? g, string? f, string? s, string que)
    {
        var n = new[] { g, f, s }.Count(x => !string.IsNullOrWhiteSpace(x));
        if (n is not (0 or 3)) throw new ValidacionException($"Indique el grupo, la familia y la sub familia {que} completos, o ninguno.");
    }

    // ------------------------------------------------------------------ componentes
    private static void LimpiarComponente(FormulacionComponenteGuardarDto d)
    {
        Req(d.COD_COMPONENTE_FORMULACION, "El codigo del componente");
        Req(d.DES_COMPONENTE_FORMULACION, "La descripcion del componente");
        d.COD_COMPONENTE_FORMULACION = d.COD_COMPONENTE_FORMULACION.Trim().ToUpperInvariant();
        d.DES_COMPONENTE_FORMULACION = d.DES_COMPONENTE_FORMULACION.Trim();
        Largo(d.COD_COMPONENTE_FORMULACION, 5, "El codigo del componente");
        Largo(d.DES_COMPONENTE_FORMULACION, 50, "La descripcion del componente");
        Largo(d.DES_NAME_TABLE, 100, "El nombre de tabla"); d.DES_NAME_TABLE = Nulo(d.DES_NAME_TABLE);
        Hex(d.DES_BACKCOLOR, "El color de fondo"); Hex(d.DES_FORECOLOR, "El color del texto");
        d.DES_BACKCOLOR = Nulo(d.DES_BACKCOLOR); d.DES_FORECOLOR = Nulo(d.DES_FORECOLOR);
        if (d.FLG_ESPECIFICACIONES_CONSUMO == true && d.FLG_ESPECIFICACIONES == true)
            throw new ValidacionException("Un componente usa las especificaciones del consumo o las de la estructura, no las dos.");
    }

    public Task<ApiResponse<List<FormulacionComponente>>> ComponentesAsync() => Ejecutar(() => _repo.ComponentesAsync(), nameof(ComponentesAsync));

    public Task<ApiResponse<FormulacionComponente>> ComponenteAsync(string cod) => Ejecutar(async () =>
        await _repo.ComponenteAsync((cod ?? "").Trim().ToUpperInvariant()) ?? throw new ValidacionException("El componente no existe."), nameof(ComponenteAsync));

    public Task<ApiResponse<WriteResult>> ComponenteCrearAsync(FormulacionComponenteGuardarDto d, string u, string e) =>
        Escribir(() => { LimpiarComponente(d); return _repo.ComponenteInsertAsync(d, u, e); }, nameof(ComponenteCrearAsync), "Componente guardado.");

    public Task<ApiResponse<WriteResult>> ComponenteActualizarAsync(FormulacionComponenteGuardarDto d, string u, string e) =>
        Escribir(() => { LimpiarComponente(d); return _repo.ComponenteUpdateAsync(d, u, e); }, nameof(ComponenteActualizarAsync), "Componente modificado.");

    public Task<ApiResponse<WriteResult>> ComponenteEliminarAsync(string cod) =>
        Escribir(() => { Req(cod, "El codigo del componente"); return _repo.ComponenteDeleteAsync(cod.Trim().ToUpperInvariant()); }, nameof(ComponenteEliminarAsync), "Componente eliminado.");

    // ------------------------------------------------------------------ elementos
    private static void LimpiarElemento(FormulacionElementoGuardarDto d, bool alta)
    {
        Req(d.COD_COMPONENTE_FORMULACION, "El componente");
        Req(d.DES_ELEMENTO_FORMULACION, "El elemento (fragmento tecnico)");
        Req(d.COD_ALIAS_FORMULACION, "El alias");
        d.COD_COMPONENTE_FORMULACION = d.COD_COMPONENTE_FORMULACION.Trim().ToUpperInvariant();
        d.DES_ELEMENTO_FORMULACION = d.DES_ELEMENTO_FORMULACION.Trim();
        d.COD_ALIAS_FORMULACION = d.COD_ALIAS_FORMULACION.Trim();
        d.COD_ELEMENTO_FORMULACION = Nulo(d.COD_ELEMENTO_FORMULACION);
        d.COD_TIPO_ESPECIFICACION_TECNICA = Nulo(d.COD_TIPO_ESPECIFICACION_TECNICA)?.ToUpperInvariant();
        Largo(d.COD_COMPONENTE_FORMULACION, 5, "El componente");
        Largo(d.COD_ELEMENTO_FORMULACION, 5, "El codigo del elemento");
        Largo(d.DES_ELEMENTO_FORMULACION, 100, "El elemento");
        Largo(d.COD_ALIAS_FORMULACION, 100, "El alias");
        Largo(d.COD_TIPO_ESPECIFICACION_TECNICA, 3, "El tipo de especificacion tecnica");
        if (!alta && d.COD_ELEMENTO_FORMULACION is null) throw new ValidacionException("El codigo del elemento es obligatorio.");
    }

    public Task<ApiResponse<List<FormulacionElemento>>> ElementosAsync(string? codComponente) =>
        Ejecutar(() => _repo.ElementosAsync(Nulo(codComponente)?.ToUpperInvariant()), nameof(ElementosAsync));

    public Task<ApiResponse<FormulacionElemento>> ElementoAsync(string codComponente, string codElemento) => Ejecutar(async () =>
        await _repo.ElementoAsync((codComponente ?? "").Trim().ToUpperInvariant(), (codElemento ?? "").Trim()) ?? throw new ValidacionException("El elemento no existe."), nameof(ElementoAsync));

    public Task<ApiResponse<WriteResult>> ElementoCrearAsync(FormulacionElementoGuardarDto d, string u, string e) =>
        Escribir(() => { LimpiarElemento(d, true); return _repo.ElementoInsertAsync(d, u, e); }, nameof(ElementoCrearAsync), "Elemento guardado.");

    public Task<ApiResponse<WriteResult>> ElementoActualizarAsync(FormulacionElementoGuardarDto d, string u, string e) =>
        Escribir(() => { LimpiarElemento(d, false); return _repo.ElementoUpdateAsync(d, u, e); }, nameof(ElementoActualizarAsync), "Elemento modificado.");

    public Task<ApiResponse<WriteResult>> ElementoEliminarAsync(string codComponente, string codElemento) =>
        Escribir(() => { Req(codComponente, "El componente"); Req(codElemento, "El codigo del elemento"); return _repo.ElementoDeleteAsync(codComponente.Trim().ToUpperInvariant(), codElemento.Trim()); }, nameof(ElementoEliminarAsync), "Elemento eliminado.");

    // ------------------------------------------------------------------ formulas
    /// <summary>Las fichas como JSON para la base. Llevan texto propio (DES_FORMULA) las constantes (su valor) y las especificaciones tecnicas (su descripcion en singular); la base ignora el texto en las demas.</summary>
    private static string FichasJson(List<FormulaFichaDto>? fichas)
    {
        fichas ??= new();
        if (fichas.Count > MaxFichas) throw new ValidacionException($"Una formula admite como maximo {MaxFichas} fichas.");
        var lista = new List<object>();
        foreach (var f in fichas)
        {
            Req(f.COD_COMPONENTE_FORMULACION, "El componente de la ficha");
            var esp = Nulo(f.COD_ESPECIFICACION_TECNICA);
            if (esp is null) Req(f.COD_ELEMENTO_FORMULACION, "El elemento de la ficha");
            var comp = f.COD_COMPONENTE_FORMULACION.Trim().ToUpperInvariant();
            var elem = (f.COD_ELEMENTO_FORMULACION ?? "").Trim();
            Largo(comp, 5, "El componente de la ficha"); Largo(elem, 5, "El elemento de la ficha");
            if (esp is not null) Largo(esp, 50, "La especificacion de la ficha");
            if (esp is not null) lista.Add(new { COD_COMPONENTE_FORMULACION = comp, COD_ELEMENTO_FORMULACION = elem.Length == 0 ? null : elem, DES_FORMULA = Nulo(f.DES_FORMULA), COD_ESPECIFICACION_TECNICA = esp });
            else if (comp == "CONST" || Nulo(f.DES_FORMULA) is not null) lista.Add(new { COD_COMPONENTE_FORMULACION = comp, COD_ELEMENTO_FORMULACION = elem, DES_FORMULA = Nulo(f.DES_FORMULA) });
            else lista.Add(new { COD_COMPONENTE_FORMULACION = comp, COD_ELEMENTO_FORMULACION = elem });
        }
        return JsonSerializer.Serialize(lista, JsonOpt);
    }

    private static void LimpiarFormula(FormulaGuardarDto d)
    {
        Req(d.DES_FORMULACION, "La descripcion de la formula");
        d.DES_FORMULACION = d.DES_FORMULACION.Trim();
        Largo(d.DES_FORMULACION, 50, "La descripcion de la formula");
        d.COD_GRUPO_PRODUCTO_CONSUMO = Nulo(d.COD_GRUPO_PRODUCTO_CONSUMO); d.COD_FAMILIA_PRODUCTO_CONSUMO = Nulo(d.COD_FAMILIA_PRODUCTO_CONSUMO); d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO = Nulo(d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO);
        d.COD_GRUPO_PRODUCTO = Nulo(d.COD_GRUPO_PRODUCTO); d.COD_FAMILIA_PRODUCTO = Nulo(d.COD_FAMILIA_PRODUCTO); d.COD_SUB_FAMILIA_PRODUCTO = Nulo(d.COD_SUB_FAMILIA_PRODUCTO);
        SubFamilia(d.COD_GRUPO_PRODUCTO_CONSUMO, d.COD_FAMILIA_PRODUCTO_CONSUMO, d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO, "del consumo");
        SubFamilia(d.COD_GRUPO_PRODUCTO, d.COD_FAMILIA_PRODUCTO, d.COD_SUB_FAMILIA_PRODUCTO, "del producto");
        if (d.COD_ESTADO is < 0 or > 2) throw new ValidacionException("El estado debe ser 0 (borrador), 1 (activa) o 2 (inactiva).");
    }

    public Task<ApiResponse<List<FormulaCabecera>>> FormulasAsync(string? search) => Ejecutar(() => _repo.FormulasAsync(Nulo(search)), nameof(FormulasAsync));

    public Task<ApiResponse<FormulaDetalle>> FormulaAsync(int num) => Ejecutar(async () =>
    {
        var cab = await _repo.FormulaAsync(num) ?? throw new ValidacionException("La formula no existe.");
        return new FormulaDetalle { Cabecera = cab, Fichas = await _repo.FormulaFichasAsync(num) };
    }, nameof(FormulaAsync));

    public Task<ApiResponse<WriteResult>> FormulaGuardarAsync(FormulaGuardarDto d, string u, string e) =>
        Escribir(() => { LimpiarFormula(d); return _repo.FormulaSaveAsync(d, FichasJson(d.Fichas), u, e); }, nameof(FormulaGuardarAsync), "Formula guardada.");

    public Task<ApiResponse<WriteResult>> FormulaCopiarAsync(int num, string u, string e) => Escribir(() => _repo.FormulaCopiarAsync(num, u, e), nameof(FormulaCopiarAsync), "Formula copiada.");

    public Task<ApiResponse<WriteResult>> FormulaEliminarAsync(int num) => Escribir(() => _repo.FormulaDeleteAsync(num), nameof(FormulaEliminarAsync), "Formula eliminada.");

    /// <summary>Valida la FORMA de la formula (no la calcula). Un problema de la formula no es un error de la peticion: se devuelve en la lista.</summary>
    public Task<ApiResponse<FormulaValidacion>> FormulaValidarAsync(FormulaValidarDto d) => Ejecutar(async () =>
        new FormulaValidacion { Errores = await _repo.FormulaValidarAsync(FichasJson(d.Fichas)) }, nameof(FormulaValidarAsync));

    public Task<ApiResponse<List<PaletaFicha>>> PaletaAsync(PaletaConsultaDto d) => Ejecutar(() =>
    {
        d.COD_GRUPO_PRODUCTO_CONSUMO = Nulo(d.COD_GRUPO_PRODUCTO_CONSUMO); d.COD_FAMILIA_PRODUCTO_CONSUMO = Nulo(d.COD_FAMILIA_PRODUCTO_CONSUMO); d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO = Nulo(d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO);
        d.COD_GRUPO_PRODUCTO = Nulo(d.COD_GRUPO_PRODUCTO); d.COD_FAMILIA_PRODUCTO = Nulo(d.COD_FAMILIA_PRODUCTO); d.COD_SUB_FAMILIA_PRODUCTO = Nulo(d.COD_SUB_FAMILIA_PRODUCTO);
        return _repo.PaletaAsync(d);
    }, nameof(PaletaAsync));
}
