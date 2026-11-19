using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Matrices de operaciones y su asignacion a versiones de diseno (script SQL 130). Las validaciones salen como SqlException
/// (52440 a 52447 y 52620 a 52629) y las atiende el servicio.</summary>
public class MatricesOperacionesRepository : IMatricesOperacionesRepository
{
    private const string P = "PROC_INGENIERIA_";
    private static readonly CommandType SP = CommandType.StoredProcedure;
    private readonly IDbConnectionFactory _db;
    public MatricesOperacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<List<MatrizResumen>> MatricesAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<MatrizResumen>(P + "MATRICES_SHOWALL", commandType: SP)).ToList();
    }

    public async Task<string> MatrizCrearAsync(MatrizGuardarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<string>(P + "MATRICES_INSERT",
            new { d.DES_RUTA, d.COD_TIPO_RUTA, d.COD_GRUPO_PRODUCTO, d.COD_FAMILIA_PRODUCTO, d.COD_SUB_FAMILIA_PRODUCTO, COD_USUARIO = u, COD_ESTACION = e }, commandType: SP);
    }

    public async Task MatrizActualizarAsync(MatrizGuardarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "MATRICES_UPDATE",
            new { d.COD_RUTA_PRODUCCION, d.DES_RUTA, d.COD_TIPO_RUTA, d.COD_GRUPO_PRODUCTO, d.COD_FAMILIA_PRODUCTO, d.COD_SUB_FAMILIA_PRODUCTO, COD_USUARIO = u, COD_ESTACION = e }, commandType: SP);
    }

    public async Task MatrizEliminarAsync(string cod)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "MATRICES_DELETE", new { COD_RUTA_PRODUCCION = cod }, commandType: SP);
    }

    public async Task<MatrizAsignarResultado> MatrizDuplicarAsync(MatrizDuplicarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<MatrizAsignarResultado>(P + "MATRICES_DUPLICAR",
            new { d.COD_RUTA_PRODUCCION, d.DES_RUTA, COD_USUARIO = u, COD_ESTACION = e }, commandType: SP);
    }

    public async Task<List<MatrizParaVersion>> MatricesParaVersionAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<MatrizParaVersion>(P + "MATRICES_PARA_VERSION", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: SP)).ToList();
    }

    public async Task<MatrizAsignarResultado> MatrizAsignarAsync(MatrizAsignarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<MatrizAsignarResultado>(P + "DISENOS_MATRIZ_ASIGNAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_MATRIZ, FEC_TIEMPOS = d.FEC_TIEMPOS?.Date, d.REEMPLAZAR, COD_USUARIO = u, COD_ESTACION = e }, commandType: SP);
    }

    public async Task<MatrizAsignarResultado> GuardarComoMatrizAsync(GuardarComoMatrizDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<MatrizAsignarResultado>(P + "DISENOS_RUTA_GUARDAR_COMO_MATRIZ",
            new { d.COD_DISENO, d.NUM_VERSION, d.DES_MATRIZ, d.COD_GRUPO_PRODUCTO, d.COD_FAMILIA_PRODUCTO, d.COD_SUB_FAMILIA_PRODUCTO, d.SIN_SUB_FAMILIA, COD_USUARIO = u, COD_ESTACION = e }, commandType: SP);
    }

    public async Task<RutaIndependizarResultado> RutaIndependizarAsync(string codDiseno, int numVersion, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<RutaIndependizarResultado>(P + "DISENOS_RUTA_INDEPENDIZAR",
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, COD_USUARIO = u, COD_ESTACION = e }, commandType: SP);
    }

    public async Task<List<RutaCompartida>> RutasCompartidasAsync()
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<RutaCompartida>(P + "RUTAS_COMPARTIDAS_SHOWALL", commandType: SP)).ToList();
    }

    public async Task<DisenosRutaOperacionesInfo> RutaOperacionesInfoAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<DisenosRutaOperacionesInfo>(P + "DISENOS_RUTA_OPERACIONES_INFO", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: SP);
    }
}
