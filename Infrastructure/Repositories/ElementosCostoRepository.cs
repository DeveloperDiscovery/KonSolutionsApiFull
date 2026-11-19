using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ElementosCostoRepository : IElementosCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public ElementosCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ElementosCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ElementosCosto>(SP.SCHEMACostos.ElementosCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ElementosCosto?> ShowbyidAsync(string COD_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ElementosCosto>(SP.SCHEMACostos.ElementosCosto.Showbyid, new { COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.ElementosCosto.Showcombox);
    }

    public async Task<IEnumerable<ElementosCosto>> SearchAsync(ElementosCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ElementosCosto>(SP.SCHEMACostos.ElementosCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ElementosCostoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("DES_ELEMENTO_COSTO", dto.DES_ELEMENTO_COSTO);
        p.Add("COD_CATEGORIA_FUNCION", dto.COD_CATEGORIA_FUNCION);
        p.Add("COD_TIPO_COSTO", dto.COD_TIPO_COSTO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ElementosCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ElementosCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ElementosCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ElementosCostoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ELEMENTO_COSTO", dto.COD_ELEMENTO_COSTO);
        p.Add("DES_ELEMENTO_COSTO", dto.DES_ELEMENTO_COSTO);
        p.Add("COD_CATEGORIA_FUNCION", dto.COD_CATEGORIA_FUNCION);
        p.Add("COD_TIPO_COSTO", dto.COD_TIPO_COSTO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ElementosCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ElementosCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ElementosCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ELEMENTO_COSTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.ElementosCosto.Delete, new { COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.ElementosCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.ElementosCosto.Delete, new { COD_ELEMENTO_COSTO = COD_ELEMENTO_COSTO })
        };
    }
}

}