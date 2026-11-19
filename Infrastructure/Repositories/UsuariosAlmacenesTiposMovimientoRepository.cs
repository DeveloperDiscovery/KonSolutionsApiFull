using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UsuariosAlmacenesTiposMovimientoRepository : IUsuariosAlmacenesTiposMovimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public UsuariosAlmacenesTiposMovimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UsuariosAlmacenesTiposMovimiento>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAlmacenesTiposMovimiento>(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UsuariosAlmacenesTiposMovimiento?> ShowbyidAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UsuariosAlmacenesTiposMovimiento>(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Showbyid, new { COD_USUARIO = COD_USUARIO, COD_ALMACEN = COD_ALMACEN, COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Showcombox);
    }

    public async Task<IEnumerable<UsuariosAlmacenesTiposMovimiento>> SearchAsync(UsuariosAlmacenesTiposMovimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UsuariosAlmacenesTiposMovimiento>(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UsuariosAlmacenesTiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_USUARIO", dto.COD_USUARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_USUARIO, string COD_ALMACEN, string COD_CLASE_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Delete, new { COD_USUARIO = COD_USUARIO, COD_ALMACEN = COD_ALMACEN, COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.UsuariosAlmacenesTiposMovimiento.Delete, new { COD_USUARIO = COD_USUARIO, COD_ALMACEN = COD_ALMACEN, COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO })
        };
    }
}

}