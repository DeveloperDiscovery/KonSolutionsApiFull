using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosAutorizadoresRepository : IUsuariosAutorizadoresRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosAutorizadoresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosAutorizadores>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAutorizadores>(SP.SCHEMAEntorno.UsuariosAutorizadores.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosAutorizadores?> ShowbyidAsync(string COD_TIPO_AUTORIZACION, string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosAutorizadores>(SP.SCHEMAEntorno.UsuariosAutorizadores.Showbyid, new { COD_TIPO_AUTORIZACION = COD_TIPO_AUTORIZACION, COD_USUARIO = COD_USUARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.UsuariosAutorizadores.Showcombox, "COD_TIPO_AUTORIZACION", "DES_TIPO_AUTORIZACION");
    }

    public async Task<IEnumerable<UsuariosAutorizadores>> SearchAsync(UsuariosAutorizadoresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAutorizadores>(SP.SCHEMAEntorno.UsuariosAutorizadores.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosAutorizadoresInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_AUTORIZACION", dto.COD_TIPO_AUTORIZACION);
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("NUM_ORDEN_PRIORIDAD", dto.NUM_ORDEN_PRIORIDAD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.UsuariosAutorizadores.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.UsuariosAutorizadores.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.UsuariosAutorizadores.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UsuariosAutorizadoresUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_AUTORIZACION", dto.COD_TIPO_AUTORIZACION);
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("NUM_ORDEN_PRIORIDAD", dto.NUM_ORDEN_PRIORIDAD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.UsuariosAutorizadores.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.UsuariosAutorizadores.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.UsuariosAutorizadores.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_AUTORIZACION, string COD_USUARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.UsuariosAutorizadores.Delete, new { COD_TIPO_AUTORIZACION = COD_TIPO_AUTORIZACION, COD_USUARIO = COD_USUARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.UsuariosAutorizadores.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.UsuariosAutorizadores.Delete, new { COD_TIPO_AUTORIZACION = COD_TIPO_AUTORIZACION, COD_USUARIO = COD_USUARIO })
        };
    }
}

}