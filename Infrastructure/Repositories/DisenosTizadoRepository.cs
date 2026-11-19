using System.Data;
using System.Text.Json;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Moldes y tizados de una version de diseno (script SQL 115). Las validaciones salen como SqlException 52401 y 52480 a 52485
/// y las atiende el servicio.</summary>
public class DisenosTizadoRepository : IDisenosTizadoRepository
{
    private const string P = "PROC_INGENIERIA_DISENOS_";
    private readonly IDbConnectionFactory _db;
    public DisenosTizadoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<List<DisenosMolde>> MoldesAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<DisenosMolde>(P + "MOLDES_SHOWALL", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task<List<int>> GuardarMoldesAsync(string codDiseno, int numVersion, IEnumerable<object> filas, string codUsuario, string codEstacion)
    {
        // las filas van como JSON y el procedimiento las graba juntas (todo o nada); los numeros viajan como numeros JSON, sin depender de la cultura
        var json = JsonSerializer.Serialize(filas);
        using var conn = _db.CreateConnection();
        var r = await conn.QueryAsync<(int ORDEN, int NUM_SECUENCIA)>(P + "MOLDES_GUARDAR",
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, FILAS = json, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
        return r.OrderBy(x => x.ORDEN).Select(x => x.NUM_SECUENCIA).ToList();
    }

    public async Task<int> EliminarMoldeAsync(DisenosMoldeClaveDto c)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "MOLDES_ELIMINAR", new { c.COD_DISENO, c.NUM_VERSION, c.NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<List<DisenosTizadoResumen>> TizadosAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<DisenosTizadoResumen>(P + "TIZADOS_SHOWALL", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task<DisenosTizadoFila> TizadoAsync(string codDiseno, int numVersion, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<DisenosTizadoFila>(P + "TIZADOS_SHOWBYID", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> GuardarTizadoAsync(string codDiseno, int numVersion, string nombre, DisenosTizadoCalcularDto p, string tallas, int prendas, int piezas, int noColocadas,
                                              double largo, double eficiencia, double? consumo, string resultadoJson, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "TIZADOS_GUARDAR", new
        {
            COD_DISENO = codDiseno, NUM_VERSION = numVersion, DES_TIZADO = nombre,
            VAL_ANCHO_TELA = p.VAL_ANCHO_TELA, VAL_SEPARACION = p.VAL_SEPARACION, VAL_MARGEN = p.VAL_MARGEN, VAL_CELDA = p.VAL_CELDA, NUM_INTENTOS = p.NUM_INTENTOS,
            DES_TALLAS = tallas, NUM_PRENDAS = prendas, NUM_PIEZAS = piezas, NUM_NO_COLOCADAS = noColocadas, VAL_LARGO = largo, VAL_EFICIENCIA = eficiencia,
            VAL_CONSUMO_PRENDA = consumo, DES_RESULTADO = resultadoJson, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> EliminarTizadoAsync(DisenosMoldeClaveDto c)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "TIZADOS_ELIMINAR", new { c.COD_DISENO, c.NUM_VERSION, c.NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<List<DisenosGradacionRegla>> ReglasGradacionAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return (await conn.QueryAsync<DisenosGradacionRegla>(P + "MOLDES_GRADACION_SHOWALL", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task GuardarReglasGradacionAsync(string codDiseno, int numVersion, IEnumerable<object> filas, string codUsuario, string codEstacion)
    {
        var json = JsonSerializer.Serialize(filas);
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "MOLDES_GRADACION_GUARDAR", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, FILAS = json, COD_USUARIO = codUsuario, COD_ESTACION = codEstacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> EliminarReglaGradacionAsync(DisenosGradacionClaveDto c)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<int>(P + "MOLDES_GRADACION_ELIMINAR", new { c.COD_DISENO, c.NUM_VERSION, c.NUM_SECUENCIA, c.DES_TALLA }, commandType: CommandType.StoredProcedure);
    }
}
