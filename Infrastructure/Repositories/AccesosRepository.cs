using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AccesosRepository : IAccesosRepository
{
    private readonly IDbConnectionFactory _db;
    public AccesosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Accesos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Accesos>(SP.SCHEMASafety.Accesos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Accesos?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Accesos>(SP.SCHEMASafety.Accesos.Showbyid, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.Accesos.Showcombox, "COD_SUCURSAL", "COD_SUCURSAL");
    }

    public async Task<IEnumerable<Accesos>> SearchAsync(AccesosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Accesos>(SP.SCHEMASafety.Accesos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AccesosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.Accesos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.Accesos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.Accesos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AccesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.Accesos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.Accesos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.Accesos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.Accesos.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.Accesos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.Accesos.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ENTIDAD = COD_ENTIDAD })
        };
    }
}

}