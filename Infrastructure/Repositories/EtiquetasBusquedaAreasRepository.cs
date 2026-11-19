using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EtiquetasBusquedaAreasRepository : IEtiquetasBusquedaAreasRepository
{
    private readonly IDbConnectionFactory _db;
    public EtiquetasBusquedaAreasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EtiquetasBusquedaAreas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EtiquetasBusquedaAreas>(
            SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EtiquetasBusquedaAreas?> ShowbyidAsync(string codEtiquetaBusqueda)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EtiquetasBusquedaAreas>(
            SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Showbyid,
            new { COD_ETIQUETA_BUSQUEDA = codEtiquetaBusqueda }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EtiquetasBusquedaAreasInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ETIQUETA_BUSQUEDA", dto.COD_ETIQUETA_BUSQUEDA);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("FLG_GERENCIA", dto.FLG_GERENCIA);
        p.Add("FLG_SUB_GERENCIA", dto.FLG_SUB_GERENCIA);
        p.Add("FLG_JEFATURA", dto.FLG_JEFATURA);
        p.Add("FLG_SUBJEFATURA", dto.FLG_SUBJEFATURA);
        p.Add("FLG_SUPERVISOR", dto.FLG_SUPERVISOR);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codEtiquetaBusqueda)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Delete,
            new { COD_ETIQUETA_BUSQUEDA = codEtiquetaBusqueda }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EtiquetasBusquedaAreas.Delete, new { COD_ETIQUETA_BUSQUEDA = codEtiquetaBusqueda })
        };
    }
}
}
