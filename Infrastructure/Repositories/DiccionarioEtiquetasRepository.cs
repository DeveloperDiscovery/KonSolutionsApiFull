using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DiccionarioEtiquetasRepository : IDiccionarioEtiquetasRepository
{
    private readonly IDbConnectionFactory _db;
    public DiccionarioEtiquetasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DiccionarioEtiquetas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DiccionarioEtiquetas>(SP.SCHEMAEntorno.DiccionarioEtiquetas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DiccionarioEtiquetas?> ShowbyidAsync(string codEtiqueta)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DiccionarioEtiquetas>(SP.SCHEMAEntorno.DiccionarioEtiquetas.Showbyid, new { COD_ETIQUETA = codEtiqueta }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.DiccionarioEtiquetas.Showcombox);
    }

    public async Task<WriteResult> InsertAsync(DiccionarioEtiquetasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ETIQUETA", dto.COD_ETIQUETA);
        p.Add("DES_ETIQUETA", dto.DES_ETIQUETA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.DiccionarioEtiquetas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.DiccionarioEtiquetas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.DiccionarioEtiquetas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codEtiqueta)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.DiccionarioEtiquetas.Delete, new { COD_ETIQUETA = codEtiqueta }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.DiccionarioEtiquetas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.DiccionarioEtiquetas.Delete, new { COD_ETIQUETA = codEtiqueta })
        };
    }
}
}
