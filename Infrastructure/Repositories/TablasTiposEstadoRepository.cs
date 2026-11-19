using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;
namespace KonSolutionsApi.Infrastructure.Repositories;

public class TablasTiposEstadoRepository : ITablasTiposEstadoRepository
{
    private readonly IDbConnectionFactory _db;
    public TablasTiposEstadoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TablasTiposEstado>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablasTiposEstado>(SP.SCHEMAEstandar.TablasTiposEstado.Showall, commandType: CommandType.StoredProcedure);
    }
    public async Task<TablasTiposEstado?> ShowbyidAsync(string DES_TABLA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TablasTiposEstado>(SP.SCHEMAEstandar.TablasTiposEstado.Showbyid, new { DES_TABLA }, commandType: CommandType.StoredProcedure);
    }
    public async Task<IEnumerable<TablasTiposEstado>> SearchAsync(TablasTiposEstadoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablasTiposEstado>(SP.SCHEMAEstandar.TablasTiposEstado.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }
    public async Task<WriteResult> InsertAsync(TablasTiposEstadoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("DES_TABLA", dto.DES_TABLA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TablasTiposEstado.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TablasTiposEstado.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TablasTiposEstado.Insert, p)
        };
    }
}
    public async Task<WriteResult> DeleteAsync(string DES_TABLA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TablasTiposEstado.Delete, new { DES_TABLA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TablasTiposEstado.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TablasTiposEstado.Delete, new { DES_TABLA })
        };
    }
}

    public async Task<IEnumerable<TablaDisponible>> TablasDisponiblesAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TablaDisponible>(
            "PROC_ENTORNO_TABLAS_TIPOS_ESTADO_TABLASDISPONIBLES", commandType: CommandType.StoredProcedure);
    }
}
