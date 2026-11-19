using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Controlcalidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ClasesDefectosTecnicosRepository : IClasesDefectosTecnicosRepository
{
    private readonly IDbConnectionFactory _db;
    public ClasesDefectosTecnicosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ClasesDefectosTecnicos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesDefectosTecnicos>(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ClasesDefectosTecnicos?> ShowbyidAsync(string COD_CLASE_DEFECTO_TECNICO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ClasesDefectosTecnicos>(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Showbyid, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Showcombox);
    }

    public async Task<IEnumerable<ClasesDefectosTecnicos>> SearchAsync(ClasesDefectosTecnicosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ClasesDefectosTecnicos>(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ClasesDefectosTecnicosInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("DES_CLASE_DEFECTO_TECNICO", dto.DES_CLASE_DEFECTO_TECNICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ClasesDefectosTecnicosUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("DES_CLASE_DEFECTO_TECNICO", dto.DES_CLASE_DEFECTO_TECNICO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_DEFECTO_TECNICO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Delete, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAControlcalidad.ClasesDefectosTecnicos.Delete, new { COD_CLASE_DEFECTO_TECNICO = COD_CLASE_DEFECTO_TECNICO })
        };
    }
}

}