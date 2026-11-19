using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosSucursalesCalendarioRepository : IUsuariosSucursalesCalendarioRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosSucursalesCalendarioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosSucursalesCalendario>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosSucursalesCalendario>(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosSucursalesCalendario?> ShowbyidAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosSucursalesCalendario>(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Showbyid, new { COD_USUARIO = COD_USUARIO, COD_SUCURSAL = COD_SUCURSAL, FEC_INICIAL = FEC_INICIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Showcombox);
    }

    public async Task<IEnumerable<UsuariosSucursalesCalendario>> SearchAsync(UsuariosSucursalesCalendarioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosSucursalesCalendario>(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosSucursalesCalendarioInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("FEC_INICIAL", dto.FEC_INICIAL);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UsuariosSucursalesCalendarioUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("FEC_INICIAL", dto.FEC_INICIAL);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_SUCURSAL, DateTime FEC_INICIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Delete, new { COD_USUARIO = COD_USUARIO, COD_SUCURSAL = COD_SUCURSAL, FEC_INICIAL = FEC_INICIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosSucursalesCalendario.Delete, new { COD_USUARIO = COD_USUARIO, COD_SUCURSAL = COD_SUCURSAL, FEC_INICIAL = FEC_INICIAL })
        };
    }
}

}