using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ContenedoresAlmacenajeRepository : IContenedoresAlmacenajeRepository
{
    private readonly IDbConnectionFactory _db;
    public ContenedoresAlmacenajeRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ContenedoresAlmacenaje>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContenedoresAlmacenaje>(
            SP.SCHEMALogistica.ContenedoresAlmacenaje.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ContenedoresAlmacenaje?> ShowbyidAsync(string COD_CONTENEDOR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ContenedoresAlmacenaje>(
            SP.SCHEMALogistica.ContenedoresAlmacenaje.Showbyid,
            new { COD_CONTENEDOR = COD_CONTENEDOR }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los SP _SHOWCOMBOX devuelven COD_*/DES_*, no Id/Texto: la traducción por
    /// convención la hace ComboboxReader, igual que en el resto de los repositorios.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.ContenedoresAlmacenaje.Showcombox);
    }

    public async Task<IEnumerable<ContenedoresAlmacenaje>> SearchAsync(ContenedoresAlmacenajeSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContenedoresAlmacenaje>(
            SP.SCHEMALogistica.ContenedoresAlmacenaje.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ContenedoresAlmacenajeInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        // Vacío al crear: el SP genera el correlativo. Con valor, actualiza.
        p.Add("COD_CONTENEDOR", dto.COD_CONTENEDOR ?? string.Empty);
        p.Add("DES_CONTENDOR", dto.DES_CONTENDOR);   // sic: así se llama la columna en la BD
        p.Add("COD_TIPO_UBICACION", dto.COD_TIPO_UBICACION);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        // Polígono dibujado sobre el plano: viaja tal cual desde la UI y vuelve igual.
        // El SP lo recibe entre COD_ALMACEN y DES_POLIGONO; Dapper mapea por nombre, así
        // que el orden acá no importa, pero se deja junto al resto de la ubicación.
        p.Add("FLG_NODO_DISTRIBUCION", dto.FLG_NODO_DISTRIBUCION ?? false);
        p.Add("DES_POLIGONO", dto.DES_POLIGONO);
        p.Add("NUM_FILAS", dto.NUM_FILAS);
        p.Add("NUM_COLUMNAS", dto.NUM_COLUMNAS);
        p.Add("VAL_ALTO_NIVEL_CENTIMETROS", dto.VAL_ALTO_NIVEL_CENTIMETROS);
        p.Add("VAL_LARGO_NIVEL_CENTIMETROS", dto.VAL_LARGO_NIVEL_CENTIMETROS);
        p.Add("VAL_PROFUNDIDAD_NIVEL_CENTIMETROS", dto.VAL_PROFUNDIDAD_NIVEL_CENTIMETROS);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.ContenedoresAlmacenaje.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ContenedoresAlmacenaje.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ContenedoresAlmacenaje.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CONTENEDOR)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMALogistica.ContenedoresAlmacenaje.Delete,
            new { COD_CONTENEDOR = COD_CONTENEDOR }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.ContenedoresAlmacenaje.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.ContenedoresAlmacenaje.Delete, new { COD_CONTENEDOR = COD_CONTENEDOR })
        };
    }
}

    /// <summary>PROC_LOGISTICA_CONTENEDORES_ALMACENAJE_LOADCOMBOS: Tipos de ubicación +
    /// Sucursales + Almacenes (los tres combos independientes de
    /// ContenedoresAlmacenajeDialog.razor) en un solo viaje.</summary>
    public async Task<ContenedoresAlmacenajeLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.ContenedoresAlmacenaje.Loadcombos, commandType: CommandType.StoredProcedure);

        var tiposUbicacion = MapCombo(await multi.ReadAsync<dynamic>());
        var sucursales      = MapCombo(await multi.ReadAsync<dynamic>());
        var almacenes       = MapCombo(await multi.ReadAsync<dynamic>());

        return new ContenedoresAlmacenajeLoadCombosResult { TiposUbicacion = tiposUbicacion, Sucursales = sucursales, Almacenes = almacenes };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código (primera
    /// que empiece con "COD_") y la de descripción (primera "DES_"/"NOMBRE_") — nunca por
    /// columnas literales "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id    = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }
}
