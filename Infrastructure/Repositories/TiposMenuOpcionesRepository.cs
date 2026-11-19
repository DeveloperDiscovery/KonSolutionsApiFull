using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposMenuOpcionesRepository : ITiposMenuOpcionesRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposMenuOpcionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposMenuOpciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMenuOpciones>(
            SP.SCHEMAEstandar.TiposMenuOpciones.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposMenuOpciones?> ShowbyidAsync(string COD_TIPO_MENU_OPCIONES)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposMenuOpciones>(
            SP.SCHEMAEstandar.TiposMenuOpciones.Showbyid,
            new { COD_TIPO_MENU_OPCIONES },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposMenuOpciones.Showcombox);
    }

    public async Task<IEnumerable<TiposMenuOpciones>> SearchAsync(TiposMenuOpcionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMenuOpciones>(
            SP.SCHEMAEstandar.TiposMenuOpciones.Search,
            new { SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposMenuOpcionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_MENU_OPCIONES", dto.COD_TIPO_MENU_OPCIONES);
        p.Add("DES_TIPO_MENU_OPCIONES", dto.DES_TIPO_MENU_OPCIONES);
        p.Add("IMG_ICONO", dto.IMG_ICONO, DbType.Binary, size: -1);

try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.TiposMenuOpciones.Insert, p,
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposMenuOpciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposMenuOpciones.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_MENU_OPCIONES)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.TiposMenuOpciones.Delete,
            new { COD_TIPO_MENU_OPCIONES },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposMenuOpciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposMenuOpciones.Delete, new { COD_TIPO_MENU_OPCIONES })
        };
    }
}
}
