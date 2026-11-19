using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposDocumentoIdentidadRepository : ITiposDocumentoIdentidadRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposDocumentoIdentidadRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposDocumentoIdentidad>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoIdentidad>(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposDocumentoIdentidad?> ShowbyidAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposDocumentoIdentidad>(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Showbyid, new { COD_TIPO_DOCUMENTO_IDENTIDAD = COD_TIPO_DOCUMENTO_IDENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposDocumentoIdentidad.Showcombox);
    }

    public async Task<IEnumerable<TiposDocumentoIdentidad>> SearchAsync(TiposDocumentoIdentidadSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposDocumentoIdentidad>(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposDocumentoIdentidadInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("DES_TIPO_DOCUMENTO_IDENTIDAD", dto.DES_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT", CodigoSunat(dto.COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT));
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposDocumentoIdentidad.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.
    ///
    /// Los dos SP piden @VAL_LONGITUD y @COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT sin valor por omisión. Antes no se
    /// mandaba ninguno de los dos (la longitud llegaba de la web y se perdía en el camino), así que dar de alta
    /// o modificar un tipo terminaba en «expects parameter».</summary>
    public async Task<WriteResult> UpdateAsync(TiposDocumentoIdentidadUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("DES_TIPO_DOCUMENTO_IDENTIDAD", dto.DES_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT", CodigoSunat(dto.COD_TIPO_DOCUMENTO_IDENTIDAD_SUNAT));
        p.Add("VAL_LONGITUD", dto.VAL_LONGITUD);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposDocumentoIdentidad.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Update, p)
        };
    }
}

    /// <summary>El código SUNAT es optativo: vacío o en blanco viaja como NULL, no como cadena vacía; si
    /// viene, sin espacios y en mayúsculas, como lo escribe el catálogo 06 (A, B, C...).</summary>
    private static string? CodigoSunat(string? valor)
        => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim().ToUpperInvariant();

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_DOCUMENTO_IDENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Delete, new { COD_TIPO_DOCUMENTO_IDENTIDAD = COD_TIPO_DOCUMENTO_IDENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposDocumentoIdentidad.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposDocumentoIdentidad.Delete, new { COD_TIPO_DOCUMENTO_IDENTIDAD = COD_TIPO_DOCUMENTO_IDENTIDAD })
        };
    }
}

}