using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosTiposMovimientoDirectAccessRepository : IUsuariosTiposMovimientoDirectAccessRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosTiposMovimientoDirectAccessRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosTiposMovimientoDirectAccess>> ShowallAsync(string codUsuario, string codClaseMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosTiposMovimientoDirectAccess>(
            SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Showall,
            new { COD_USUARIO = codUsuario, COD_CLASE_MOVIMIENTO = codClaseMovimiento },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosTiposMovimientoDirectAccess?> ShowbyidAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosTiposMovimientoDirectAccess>(
            SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Showbyid,
            new { COD_USUARIO = codUsuario, COD_CLASE_MOVIMIENTO = codClaseMovimiento, COD_TIPO_MOVIMIENTO = codTipoMovimiento },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codUsuario, string codClaseMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Showcombox,
            "COD_TIPO_MOVIMIENTO", "DES_TIPO_MOVIMIENTO",
            new { COD_USUARIO = codUsuario, COD_CLASE_MOVIMIENTO = codClaseMovimiento });
    }

    public async Task<IEnumerable<UsuariosTiposMovimientoDirectAccess>> SearchAsync(string codUsuario, string codClaseMovimiento, UsuariosTiposMovimientoDirectAccessSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosTiposMovimientoDirectAccess>(
            SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Search,
            new { COD_USUARIO = codUsuario, COD_CLASE_MOVIMIENTO = codClaseMovimiento, SEARCH = dto.Search ?? string.Empty },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosTiposMovimientoDirectAccessInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new { dto.COD_USUARIO, dto.COD_CLASE_MOVIMIENTO, dto.COD_TIPO_MOVIMIENTO };
        try
        {
            await conn.ExecuteAsync(SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Insert, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codUsuario, string codClaseMovimiento, string codTipoMovimiento)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_USUARIO = codUsuario, COD_CLASE_MOVIMIENTO = codClaseMovimiento, COD_TIPO_MOVIMIENTO = codTipoMovimiento };
        try
        {
            await conn.ExecuteAsync(SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Delete, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.UsuariosTiposMovimientoDirectAccess.Delete, p)
            };
        }
    }
}
