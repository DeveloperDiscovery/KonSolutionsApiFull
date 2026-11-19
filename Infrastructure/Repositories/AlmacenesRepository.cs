using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AlmacenesRepository : IAlmacenesRepository
{
    private readonly IDbConnectionFactory _db;
    public AlmacenesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Almacenes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Almacenes>(SP.SCHEMALogistica.Almacenes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Almacenes?> ShowbyidAsync(string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Almacenes>(SP.SCHEMALogistica.Almacenes.Showbyid, new { COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá. Mapearlo directo devolvía items en blanco.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.Almacenes.Showcombox);
    }

    public async Task<IEnumerable<Almacenes>> SearchAsync(AlmacenesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Almacenes>(SP.SCHEMALogistica.Almacenes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("DES_ALMACEN", dto.DES_ALMACEN);
        p.Add("DES_ABREVIADA", dto.DES_ABREVIADA);
        p.Add("COD_TIPO_ALMACEN", dto.COD_TIPO_ALMACEN);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("FLG_STOCK_NEGATIVO", dto.FLG_STOCK_NEGATIVO ?? false);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_AREA", dto.COD_AREA);
        // Polígono dibujado sobre el plano: viaja tal cual desde la UI y vuelve igual.
        p.Add("DES_POLIGONO", dto.DES_POLIGONO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Almacenes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Almacenes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Almacenes.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Almacenes.Delete, new { COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Almacenes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Almacenes.Delete, new { COD_ALMACEN = COD_ALMACEN })
        };
    }
}

    /// <summary>PROC_LOGISTICA_ALMACENES_LOADCOMBOS: Tipos de almacén + Sucursales + Procesos
    /// (los tres combos independientes de AlmacenesDialog.razor) en un solo viaje.</summary>
    public async Task<AlmacenesLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.Almacenes.Loadcombos, commandType: CommandType.StoredProcedure);

        var tiposAlmacen = MapCombo(await multi.ReadAsync<dynamic>());
        var sucursales   = MapCombo(await multi.ReadAsync<dynamic>());
        var procesos     = MapCombo(await multi.ReadAsync<dynamic>());

        return new AlmacenesLoadCombosResult { TiposAlmacen = tiposAlmacen, Sucursales = sucursales, Procesos = procesos };
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