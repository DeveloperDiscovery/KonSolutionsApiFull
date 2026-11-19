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
            d.COD_DISENO_ENLACE, d.NUM_VERSION_ENLACE
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

}
