using System.Data;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>Estructura de una version de diseno: sectores, componentes e insumos con subensambles de hasta
/// 10 niveles, bloqueo y copias (script SQL 107). Las validaciones salen como SqlException 52400 a 52462 y las atiende el servicio.</summary>
public class DisenosEstructuraRepository : IDisenosEstructuraRepository
{
    private const string P = "PROC_INGENIERIA_DISENOS_";
    private readonly IDbConnectionFactory _db;
    public DisenosEstructuraRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosEstructuraNodo>> EstructuraAsync(string codDiseno, int numVersion, int maxNivel)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosEstructuraNodo>(P + "VERSION_ESTRUCTURA",
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, MAX_NIVEL = maxNivel }, commandType: CommandType.StoredProcedure);
    }

    public async Task SectorGuardarAsync(DisenosEstructuraSectorDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTRUCTURA_SECTOR_GUARDAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.DES_SECTOR_PRODUCTO, d.FLG_TOTAL_PRODUCTO, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task SectorEliminarAsync(string codDiseno, int numVersion, string codSector)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTRUCTURA_SECTOR_ELIMINAR",
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, COD_SECTOR_PRODUCTO = codSector }, commandType: CommandType.StoredProcedure);
    }

    public async Task ComponenteAgregarAsync(DisenosEstructuraComponenteDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTRUCTURA_COMPONENTE_AGREGAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_PRODUCTO, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task ComponenteQuitarAsync(DisenosEstructuraComponenteDto d)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTRUCTURA_COMPONENTE_QUITAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task ComponenteCambiarAsync(DisenosEstructuraComponenteCambiarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTRUCTURA_COMPONENTE_CAMBIAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_ORIGEN, d.COD_COMPONENTE_DESTINO, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> InsumoGuardarAsync(DisenosEstructuraInsumoDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstAsync<DisenosEstructuraInsumoResultado>(P + "ESTRUCTURA_INSUMO_GUARDAR", new
        {
            d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_PRODUCTO, d.NUM_SECUENCIA, d.COD_PRODUCTO_COMPONENTE,
            d.CAN_CONSUMO, d.COD_TIPO_MERMA, d.TAS_MERMA, d.COD_MONEDA, d.PRE_UNITARIO, COD_USUARIO = u, COD_ESTACION = e,
            d.COD_DISENO_ENLACE, d.NUM_VERSION_ENLACE, d.FLG_PRODUCTO_COMPONENTE_PIVOT, d.COD_TIPO_MATERIAL
        }, commandType: CommandType.StoredProcedure);
        return r.NUM_SECUENCIA;
    }

    public async Task InsumoEliminarAsync(DisenosEstructuraInsumoClaveDto d)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "ESTRUCTURA_INSUMO_ELIMINAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_PRODUCTO, d.NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task BloquearAsync(string codDiseno, int numVersion, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "VERSION_BLOQUEAR", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, COD_USUARIO = codUsuario }, commandType: CommandType.StoredProcedure);
    }

    public async Task DesbloquearAsync(string codDiseno, int numVersion, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "VERSION_DESBLOQUEAR", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, COD_USUARIO = codUsuario }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosVersionCopiarResultado> CopiarVersionAsync(DisenosVersionCopiarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<DisenosVersionCopiarResultado>(P + "VERSION_COPIAR", new
        {
            d.COD_DISENO_ORIGEN, d.NUM_VERSION_ORIGEN, d.COD_DISENO_DESTINO, d.NUM_VERSION_DESTINO, d.DES_VERSION, COD_USUARIO = u, COD_ESTACION = e
        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosSegmentoCopiarResultado> CopiarSegmentoAsync(DisenosSegmentoCopiarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstAsync<DisenosSegmentoCopiarResultado>(P + "VERSION_COPIAR_SEGMENTO", new
        {
            d.COD_DISENO_ORIGEN, d.NUM_VERSION_ORIGEN, d.COD_SECTOR_ORIGEN, d.COD_COMPONENTE_ORIGEN, d.COD_DISENO_DESTINO, d.NUM_VERSION_DESTINO,
            d.COD_SECTOR_DESTINO, d.MODO, COD_USUARIO = u, COD_ESTACION = e
        }, commandType: CommandType.StoredProcedure);
    }

    // ---------- ruta de produccion de la version (script 112) ----------
    public async Task<DisenosRutaCatalogos> RutaCatalogosAsync()
    {
        using var conn = _db.CreateConnection();
        using var g = await conn.QueryMultipleAsync(P + "RUTA_CATALOGOS", commandType: CommandType.StoredProcedure);
        return new DisenosRutaCatalogos
        {
            TiposRuta = (await g.ReadAsync<DisenosRutaCombo>()).ToList(),
            Rutas = (await g.ReadAsync<DisenosRutaCombo>()).ToList(),
        };
    }

    public async Task<string> RutaCrearAsync(DisenosRutaCrearDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<string>(P + "RUTA_CREAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.DES_RUTA, d.COD_TIPO_RUTA, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task RutaAsignarAsync(DisenosRutaAsignarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(P + "RUTA_ASIGNAR", new { d.COD_DISENO, d.NUM_VERSION, d.COD_RUTA, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosRutaResultado> RutaCopiarAsync(DisenosRutaCopiarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<DisenosRutaResultado>(P + "RUTA_COPIAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_RUTA_ORIGEN, d.DES_RUTA, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    // ---------- formulas de consumo en las lineas (script 147) ----------
    private const string PF = "PROC_INGENIERIA_ESTRUCTURA_FORMULA_";
    private static object Clave(DisenosEstructuraLineaClaveDto l) =>
        new { l.COD_DISENO, l.NUM_VERSION, l.COD_SECTOR_PRODUCTO, l.COD_COMPONENTE_PRODUCTO, l.NUM_SECUENCIA, l.COD_PRODUCTO_COMPONENTE };

    public async Task<IEnumerable<DisenosEstructuraFormulaDisponible>> FormulaDisponiblesAsync(DisenosEstructuraLineaClaveDto l)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosEstructuraFormulaDisponible>(PF + "DISPONIBLES", Clave(l), commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosEstructuraFormulaLinea> FormulaLineaAsync(DisenosEstructuraLineaClaveDto l)
    {
        using var conn = _db.CreateConnection();
        using var g = await conn.QueryMultipleAsync(PF + "SHOW", Clave(l), commandType: CommandType.StoredProcedure);
        var r = await g.ReadFirstAsync<DisenosEstructuraFormulaLinea>();
        r.Fichas = (await g.ReadAsync<DisenosEstructuraFormulaFicha>()).ToList();
        return r;
    }

    public async Task<DisenosEstructuraFormulaResultado> FormulaAsignarAsync(DisenosEstructuraFormulaAsignarDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<DisenosEstructuraFormulaResultado>(PF + "ASIGNAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_PRODUCTO, d.NUM_SECUENCIA, d.COD_PRODUCTO_COMPONENTE, d.NUM_FORMULACION, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task FormulaQuitarAsync(DisenosEstructuraLineaClaveDto l, string u, string e)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(PF + "QUITAR",
            new { l.COD_DISENO, l.NUM_VERSION, l.COD_SECTOR_PRODUCTO, l.COD_COMPONENTE_PRODUCTO, l.NUM_SECUENCIA, l.COD_PRODUCTO_COMPONENTE, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosEstructuraFormulaResultado> FormulaCalcularAsync(DisenosEstructuraFormulaCalcularDto d, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<DisenosEstructuraFormulaResultado>(PF + "CALCULAR",
            new { d.COD_DISENO, d.NUM_VERSION, d.COD_SECTOR_PRODUCTO, d.COD_COMPONENTE_PRODUCTO, d.NUM_SECUENCIA, d.COD_PRODUCTO_COMPONENTE, d.GUARDAR, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosEstructuraFormulaVersionFila>> FormulaCalcularVersionAsync(string codDiseno, int numVersion, bool guardar, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosEstructuraFormulaVersionFila>(PF + "CALCULAR_VERSION",
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, GUARDAR = guardar, COD_USUARIO = u, COD_ESTACION = e }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosEstructuraFormulaResultado> FormulaGuardarAsync(DisenosEstructuraLineaClaveDto l, string fichasJson, string u, string e)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleAsync<DisenosEstructuraFormulaResultado>(PF + "GUARDAR",
            new { l.COD_DISENO, l.NUM_VERSION, l.COD_SECTOR_PRODUCTO, l.COD_COMPONENTE_PRODUCTO, l.NUM_SECUENCIA, l.COD_PRODUCTO_COMPONENTE, ELEMENTOS = fichasJson, COD_USUARIO = u, COD_ESTACION = e },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosEstructuraFormulaDiagrama>> FormulaDiagramaAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosEstructuraFormulaDiagrama>(PF + "DIAGRAMA", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosEstructuraIndicador>> IndicadoresAsync(string codDiseno, int numVersion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosEstructuraIndicador>("PROC_INGENIERIA_ESTRUCTURA_INDICADORES", new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosEstructuraTipoMaterial>> TiposMaterialAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosEstructuraTipoMaterial>("PROC_INGENIERIA_TIPOS_MATERIAL_ESTRUCTURA_COMBOBOX", commandType: CommandType.StoredProcedure);
    }
}
