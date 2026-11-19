using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PathFilesRepository : IPathFilesRepository
{
    private readonly IDbConnectionFactory _db;
    public PathFilesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PathFiles>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PathFiles>(SP.SCHEMAEntorno.PathFiles.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PathFiles?> ShowbyidAsync(string COD_KEY_PATHFILE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PathFiles>(SP.SCHEMAEntorno.PathFiles.Showbyid, new { COD_KEY_PATHFILE = COD_KEY_PATHFILE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.PathFiles.Showcombox);
    }

    public async Task<IEnumerable<PathFiles>> SearchAsync(PathFilesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PathFiles>(SP.SCHEMAEntorno.PathFiles.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PathFilesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_KEY_PATHFILE", dto.COD_KEY_PATHFILE);
        p.Add("DES_KEY_PATHFILE", dto.DES_KEY_PATHFILE);
        p.Add("COD_KEY_SERVIDOR", dto.COD_KEY_SERVIDOR);
        p.Add("FLG_YEAR", dto.FLG_YEAR ?? false);
        p.Add("FLG_MONTH", dto.FLG_MONTH ?? false);
        p.Add("FLG_WEEK", dto.FLG_WEEK ?? false);
        p.Add("FLG_DAY", dto.FLG_DAY ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.PathFiles.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.PathFiles.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.PathFiles.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_KEY_PATHFILE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.PathFiles.Delete, new { COD_KEY_PATHFILE = COD_KEY_PATHFILE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.PathFiles.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.PathFiles.Delete, new { COD_KEY_PATHFILE = COD_KEY_PATHFILE })
        };
    }
}

}
