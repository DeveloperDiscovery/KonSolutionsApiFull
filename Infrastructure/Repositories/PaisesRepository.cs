using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PaisesRepository : IPaisesRepository
{
    private readonly IDbConnectionFactory _db;
    public PaisesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Paises>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Paises>(SP.SCHEMAEstandar.Paises.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Paises?> ShowbyidAsync(string COD_PAIS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Paises>(SP.SCHEMAEstandar.Paises.Showbyid, new { COD_PAIS = COD_PAIS }, commandType: CommandType.StoredProcedure);
    }

    // El SHOWCOMBOX no filtra por búsqueda y su mapeo dinámico a Id/Texto no traía la
    // descripción de forma confiable (mismo problema visto en Motivos/TiposMovimiento) —
    // se usa el SHOWALL tipado (que sí trae DES_PAIS real) y se filtra/mapea acá.
    // No usa ComboboxReader.LeerAsync: no existe un SP _SHOWCOMBOX dedicado para Países;
    // el combo se deriva de la lista completa de países ya obtenida por ShowallAsync.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string? search = null)
    {
        var paises = await ShowallAsync();
        if (!string.IsNullOrWhiteSpace(search))
            paises = paises.Where(p =>
                p.COD_PAIS.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.DES_PAIS.Contains(search, StringComparison.OrdinalIgnoreCase));
        return paises.Select(p => new ComboboxItem { Id = p.COD_PAIS, Texto = p.DES_PAIS });
    }

    public async Task<IEnumerable<Paises>> SearchAsync(PaisesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Paises>(SP.SCHEMAEstandar.Paises.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PaisesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PAIS",          dto.COD_PAIS);
        p.Add("DES_PAIS",          dto.DES_PAIS);
        p.Add("COD_PAIS_3",        dto.COD_PAIS_3,        DbType.String);
        p.Add("DES_PAIS_INGLES",   dto.DES_PAIS_INGLES,   DbType.String);
        p.Add("DES_PAIS_NATIVO",   dto.DES_PAIS_NATIVO,   DbType.String);
        p.Add("DES_REGION",        dto.DES_REGION,        DbType.String);
        p.Add("DES_SUBREGION",     dto.DES_SUBREGION,     DbType.String);
        p.Add("DES_CAPITAL",       dto.DES_CAPITAL,       DbType.String);
        p.Add("DES_BANDERA_EMOJI", dto.DES_BANDERA_EMOJI, DbType.String);
        p.Add("URL_BANDERA_PNG",   dto.URL_BANDERA_PNG,   DbType.String);
        p.Add("URL_BANDERA_SVG",   dto.URL_BANDERA_SVG,   DbType.String);
        p.Add("IMG_ICONO",         dto.IMG_ICONO,         DbType.Binary);
        p.Add("IMG_ICONO_SVG",     dto.IMG_ICONO_SVG,     DbType.Binary);
        p.Add("NUM_POBLACION",     dto.NUM_POBLACION,     DbType.Int64);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Paises.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Paises.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Paises.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PaisesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PAIS",          dto.COD_PAIS);
        p.Add("DES_PAIS",          dto.DES_PAIS);
        p.Add("COD_PAIS_3",        dto.COD_PAIS_3,        DbType.String);
        p.Add("DES_PAIS_INGLES",   dto.DES_PAIS_INGLES,   DbType.String);
        p.Add("DES_PAIS_NATIVO",   dto.DES_PAIS_NATIVO,   DbType.String);
        p.Add("DES_REGION",        dto.DES_REGION,        DbType.String);
        p.Add("DES_SUBREGION",     dto.DES_SUBREGION,     DbType.String);
        p.Add("DES_CAPITAL",       dto.DES_CAPITAL,       DbType.String);
        p.Add("DES_BANDERA_EMOJI", dto.DES_BANDERA_EMOJI, DbType.String);
        p.Add("URL_BANDERA_PNG",   dto.URL_BANDERA_PNG,   DbType.String);
        p.Add("URL_BANDERA_SVG",   dto.URL_BANDERA_SVG,   DbType.String);
        p.Add("IMG_ICONO",         dto.IMG_ICONO,         DbType.Binary);
        p.Add("IMG_ICONO_SVG",     dto.IMG_ICONO_SVG,     DbType.Binary);
        p.Add("NUM_POBLACION",     dto.NUM_POBLACION,     DbType.Int64);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Paises.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Paises.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Paises.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PAIS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Paises.Delete, new { COD_PAIS = COD_PAIS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Paises.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Paises.Delete, new { COD_PAIS = COD_PAIS })
        };
    }
}

    public async Task<WriteResult> UploadJsonAsync(string jsonArray)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.Paises.UploadJson,
            new { JSON_ARRAY = jsonArray },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Paises.UploadJson,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Paises.UploadJson, new { JSON_ARRAY = jsonArray })
        };
    }
}

}