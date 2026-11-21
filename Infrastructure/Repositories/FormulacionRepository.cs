using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Formulacion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Constructor de formulas (script SQL 145). Las validaciones salen como SqlException (52700 a 52729) y las atiende el servicio.</summary>
public class FormulacionRepository : IFormulacionRepository
{
    private const string P = "PROC_FORMULACION_";
    private static readonly CommandType SP = CommandType.StoredProcedure;
    private readonly IDbConnectionFactory _db;
    public FormulacionRepository(IDbConnectionFactory db) => _db = db;

    private async Task<WriteResult> Escribir(string proc, object parametros)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<WriteResult>(proc, parametros, commandType: SP);
    }

    // ---------------------------------------------------------------- componentes
    public async Task<List<FormulacionComponente>> ComponentesAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<FormulacionComponente>(P + "COMPONENTES_SHOWALL", commandType: SP)).ToList();
    }

    public async Task<FormulacionComponente?> ComponenteAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FormulacionComponente>(P + "COMPONENTES_SHOWBYID", new { COD_COMPONENTE_FORMULACION = cod }, commandType: SP);
    }

    private static object ParamsComponente(FormulacionComponenteGuardarDto d, string u, string e) => new
    {
        d.COD_COMPONENTE_FORMULACION, d.DES_COMPONENTE_FORMULACION, d.FLG_ELEMENTOS, d.FLG_HABILITADO, d.FLG_ESPECIFICACIONES_CONSUMO, d.FLG_ESPECIFICACIONES, d.FLG_CONSUMO, d.FLG_CONSUMO_SUPERIOR, d.FLG_CONSUMO_INFERIOR, d.FLG_ESPECIFICACIONES_PIVOT, d.DES_NAME_TABLE,
        d.DES_BACKCOLOR, d.DES_FORECOLOR, d.IMG_PICTURE, COD_USUARIO_REGISTRO = u, COD_ESTACION_REGISTRO = e
    };

    public Task<WriteResult> ComponenteInsertAsync(FormulacionComponenteGuardarDto d, string u, string e) => Escribir(P + "COMPONENTES_INSERT", ParamsComponente(d, u, e));
    public Task<WriteResult> ComponenteUpdateAsync(FormulacionComponenteGuardarDto d, string u, string e) => Escribir(P + "COMPONENTES_UPDATE", ParamsComponente(d, u, e));
    public Task<WriteResult> ComponenteDeleteAsync(string cod) => Escribir(P + "COMPONENTES_DELETE", new { COD_COMPONENTE_FORMULACION = cod });

    // ---------------------------------------------------------------- elementos
    public async Task<List<FormulacionElemento>> ElementosAsync(string? codComponente)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<FormulacionElemento>(P + "ELEMENTOS_SHOWALL", new { COD_COMPONENTE_FORMULACION = codComponente }, commandType: SP)).ToList();
    }

    public async Task<FormulacionElemento?> ElementoAsync(string codComponente, string codElemento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FormulacionElemento>(P + "ELEMENTOS_SHOWBYID",
            new { COD_COMPONENTE_FORMULACION = codComponente, COD_ELEMENTO_FORMULACION = codElemento }, commandType: SP);
    }

    public Task<WriteResult> ElementoInsertAsync(FormulacionElementoGuardarDto d, string u, string e) => Escribir(P + "ELEMENTOS_INSERT", new
    {
        d.COD_COMPONENTE_FORMULACION, d.COD_ELEMENTO_FORMULACION, d.DES_ELEMENTO_FORMULACION, d.COD_ALIAS_FORMULACION, d.COD_TIPO_ESPECIFICACION_TECNICA,
        d.FLG_NO_ES_RANGO, d.FLG_HABILITADO, d.IMG_PICTURE, COD_USUARIO_REGISTRO = u, COD_ESTACION_REGISTRO = e
    });

    public Task<WriteResult> ElementoUpdateAsync(FormulacionElementoGuardarDto d, string u, string e) => Escribir(P + "ELEMENTOS_UPDATE", new
    {
        d.COD_COMPONENTE_FORMULACION, d.COD_ELEMENTO_FORMULACION, d.DES_ELEMENTO_FORMULACION, d.COD_ALIAS_FORMULACION, d.COD_TIPO_ESPECIFICACION_TECNICA,
        d.FLG_NO_ES_RANGO, d.FLG_HABILITADO, d.IMG_PICTURE, COD_USUARIO_REGISTRO = u, COD_ESTACION_REGISTRO = e
    });

    public Task<WriteResult> ElementoDeleteAsync(string codComponente, string codElemento) => Escribir(P + "ELEMENTOS_DELETE",
        new { COD_COMPONENTE_FORMULACION = codComponente, COD_ELEMENTO_FORMULACION = codElemento });

    // ---------------------------------------------------------------- formulas
    public async Task<List<FormulaCabecera>> FormulasAsync(string? search)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<FormulaCabecera>(P + "DATABASE_SHOWALL", new { SEARCH = search }, commandType: SP)).ToList();
    }

    public async Task<FormulaCabecera?> FormulaAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FormulaCabecera>(P + "DATABASE_SHOWBYID", new { NUM_FORMULACION = num }, commandType: SP);
    }

    public async Task<List<FormulaFicha>> FormulaFichasAsync(int num)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<FormulaFicha>(P + "DATABASE_ELEMENTOS_SHOWALL", new { NUM_FORMULACION = num }, commandType: SP)).ToList();
    }

    public Task<WriteResult> FormulaSaveAsync(FormulaGuardarDto d, string fichasJson, string u, string e) => Escribir(P + "DATABASE_SAVE", new
    {
        NUM_FORMULACION = d.NUM_FORMULACION is > 0 ? d.NUM_FORMULACION : null, d.DES_FORMULACION,
        d.COD_GRUPO_PRODUCTO_CONSUMO, d.COD_FAMILIA_PRODUCTO_CONSUMO, d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO,
        d.COD_GRUPO_PRODUCTO, d.COD_FAMILIA_PRODUCTO, d.COD_SUB_FAMILIA_PRODUCTO, d.COD_ESTADO, ELEMENTOS = fichasJson,
        COD_USUARIO_REGISTRO = u, COD_ESTACION_REGISTRO = e
    });

    public Task<WriteResult> FormulaCopiarAsync(int num, string u, string e) => Escribir(P + "DATABASE_COPIAR",
        new { NUM_FORMULACION = num, COD_USUARIO_REGISTRO = u, COD_ESTACION_REGISTRO = e });

    public Task<WriteResult> FormulaDeleteAsync(int num) => Escribir(P + "DATABASE_DELETE", new { NUM_FORMULACION = num });

    public async Task<List<FormulaError>> FormulaValidarAsync(string fichasJson)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<FormulaError>(P + "DATABASE_VALIDAR", new { ELEMENTOS = fichasJson }, commandType: SP)).ToList();
    }

    public async Task<List<PaletaFicha>> PaletaAsync(PaletaConsultaDto d)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<PaletaFicha>(P + "PALETA",
            new { d.COD_GRUPO_PRODUCTO_CONSUMO, d.COD_FAMILIA_PRODUCTO_CONSUMO, d.COD_SUB_FAMILIA_PRODUCTO_CONSUMO, d.COD_GRUPO_PRODUCTO, d.COD_FAMILIA_PRODUCTO, d.COD_SUB_FAMILIA_PRODUCTO },
            commandType: SP)).ToList();
    }
}
