using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesOperacionProduccionRepository : IClasesOperacionProduccionRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesOperacionProduccionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesOperacionProduccion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesOperacionProduccion>(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesOperacionProduccion?> ShowbyidAsync(string COD_CLASE_OPERACION_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesOperacionProduccion>(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Showbyid, new { COD_CLASE_OPERACION_PRODUCCION = COD_CLASE_OPERACION_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Showcombox);
    }

    public async Task<IEnumerable<ClasesOperacionProduccion>> SearchAsync(ClasesOperacionProduccionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesOperacionProduccion>(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesOperacionProduccionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION_PRODUCCION", dto.COD_CLASE_OPERACION_PRODUCCION);
        p.Add("DES_CLASE_OPERACION_PRODUCCION", dto.DES_CLASE_OPERACION_PRODUCCION);
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesOperacionProduccionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION_PRODUCCION", dto.COD_CLASE_OPERACION_PRODUCCION);
        p.Add("DES_CLASE_OPERACION_PRODUCCION", dto.DES_CLASE_OPERACION_PRODUCCION);
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Delete, new { COD_CLASE_OPERACION_PRODUCCION = COD_CLASE_OPERACION_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ClasesOperacionProduccion.Delete, new { COD_CLASE_OPERACION_PRODUCCION = COD_CLASE_OPERACION_PRODUCCION })
        };
    }
}

}