using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SucursalesRepository : ISucursalesRepository
{
    private readonly IDbConnectionFactory _db;
    public SucursalesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Sucursales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Sucursales>(SP.SCHEMAEstandar.Sucursales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Sucursales?> ShowbyidAsync(string COD_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Sucursales>(SP.SCHEMAEstandar.Sucursales.Showbyid, new { COD_SUCURSAL = COD_SUCURSAL }, commandType: CommandType.StoredProcedure);
    }

    // Los SP _SHOWCOMBOX devuelven las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá, en el repositorio. Mapear el SP directo a
    // ComboboxItem devolvía una lista de items en blanco.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Sucursales.Showcombox);
    }

    public async Task<IEnumerable<Sucursales>> SearchAsync(SucursalesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Sucursales>(SP.SCHEMAEstandar.Sucursales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SucursalesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("DES_SUCURSAL", dto.DES_SUCURSAL);
        p.Add("DES_DIRECCION", dto.DES_DIRECCION);
        p.Add("COD_UBIGEO", dto.COD_UBIGEO);
        p.Add("COD_TIPO_SUCURSAL", dto.COD_TIPO_SUCURSAL);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("DES_LINK_PATH_FACTURACION", dto.DES_LINK_PATH_FACTURACION);
        p.Add("DES_TOKEN_FACTURACION", dto.DES_TOKEN_FACTURACION);
        p.Add("COD_TIPO_POS", dto.COD_TIPO_POS);
        p.Add("COD_TIPO_DIRECCION", dto.COD_TIPO_DIRECCION);
        p.Add("VAL_LATITUD", dto.VAL_LATITUD);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("COD_SUCURSAL_SUNAT", CodigoSunat(dto.COD_SUCURSAL_SUNAT));
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Sucursales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Sucursales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Sucursales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(SucursalesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("DES_SUCURSAL", dto.DES_SUCURSAL);
        p.Add("DES_DIRECCION", dto.DES_DIRECCION);
        p.Add("COD_UBIGEO", dto.COD_UBIGEO);
        p.Add("COD_TIPO_SUCURSAL", dto.COD_TIPO_SUCURSAL);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("DES_LINK_PATH_FACTURACION", dto.DES_LINK_PATH_FACTURACION);
        p.Add("DES_TOKEN_FACTURACION", dto.DES_TOKEN_FACTURACION);
        p.Add("COD_TIPO_POS", dto.COD_TIPO_POS);
        p.Add("COD_TIPO_DIRECCION", dto.COD_TIPO_DIRECCION);
        p.Add("VAL_LATITUD", dto.VAL_LATITUD);
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("COD_SUCURSAL_SUNAT", CodigoSunat(dto.COD_SUCURSAL_SUNAT));
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Sucursales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Sucursales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Sucursales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Sucursales.Delete, new { COD_SUCURSAL = COD_SUCURSAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Sucursales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Sucursales.Delete, new { COD_SUCURSAL = COD_SUCURSAL })
        };
    }
}

    /// <summary>El código SUNAT del establecimiento es optativo: vacío o en blanco viaja como NULL,
    /// no como cadena vacía, para que "sin código" sea uno solo en la tabla. El INSERT y el UPDATE
    /// lo piden sin valor por omisión.</summary>
    private static string? CodigoSunat(string? valor)
        => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();

    public async Task<SucursalesLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "ESTANDAR.ENTIDADES" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.Sucursales.Loadcombos,
            new { COD_TIPO_ESTADO_ENTIDAD = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);
        // La SP no alía las columnas a Id/Texto (usa COD_UBIGEO/DES_UBIGEO, etc.),
        // así que se mapea manualmente en vez de con QueryAsync<ComboboxItem> directo.
        return new SucursalesLoadCombosResult
        {
            Ubigeos        = MapCombo(await multi.ReadAsync<dynamic>(), "COD_UBIGEO",        "DES_UBIGEO"),
            Entidades      = MapCombo(await multi.ReadAsync<dynamic>(), "COD_ENTIDAD",       "DES_NOMBRE_COMPLETO"),
            TiposPos       = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_POS",      "DES_TIPO_POS"),
            TiposDireccion = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_DIRECCION", "DES_TIPO_DIRECCION")
        };
    }

    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var id = d.TryGetValue(colId,    out var v1) ? v1?.ToString() ?? "" : "";
            var tx = d.TryGetValue(colTexto, out var v2) ? v2?.ToString() ?? "" : "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}