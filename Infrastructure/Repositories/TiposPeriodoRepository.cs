using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposPeriodoRepository : ITiposPeriodoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposPeriodoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposPeriodo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPeriodo>(SP.SCHEMAEstandar.TiposPeriodo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposPeriodo?> ShowbyidAsync(string COD_TIPO_PERIODO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposPeriodo>(SP.SCHEMAEstandar.TiposPeriodo.Showbyid, new { COD_TIPO_PERIODO = COD_TIPO_PERIODO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComboboxItem>(SP.SCHEMAEstandar.TiposPeriodo.Showcombox, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposPeriodo>> SearchAsync(TiposPeriodoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposPeriodo>(SP.SCHEMAEstandar.TiposPeriodo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposPeriodoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("DES_TIPO_PERIODO", dto.DES_TIPO_PERIODO);
        p.Add("DES_PREFIJO", dto.DES_PREFIJO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("FLG_QUINQUENAL", dto.FLG_QUINQUENAL ?? false);
        p.Add("FLG_BIANUAL", dto.FLG_BIANUAL ?? false);
        p.Add("FLG_ANUAL", dto.FLG_ANUAL ?? false);
        p.Add("FLG_SEMESTRAL", dto.FLG_SEMESTRAL ?? false);
        p.Add("FLG_TRIMESTRAL", dto.FLG_TRIMESTRAL ?? false);
        p.Add("FLG_MENSUAL", dto.FLG_MENSUAL ?? false);
        p.Add("FLG_QUINCENAL", dto.FLG_QUINCENAL ?? false);
        p.Add("FLG_SEMANAL", dto.FLG_SEMANAL ?? false);
        p.Add("FLG_DIARIO", dto.FLG_DIARIO ?? false);
        p.Add("FLG_MODULO_CONTABLE", dto.FLG_MODULO_CONTABLE ?? false);
        p.Add("FLG_MODULO_COSTOS", dto.FLG_MODULO_COSTOS ?? false);
        p.Add("FLG_MODULO_RRHH_NOMINA", dto.FLG_MODULO_RRHH_NOMINA ?? false);
        p.Add("FLG_MODULO_COMERCIAL", dto.FLG_MODULO_COMERCIAL ?? false);
        p.Add("FLG_MODULO_NETWORKING", dto.FLG_MODULO_NETWORKING ?? false);
        p.Add("FLG_MODULO_PLANEAMIENTO", dto.FLG_MODULO_PLANEAMIENTO ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposPeriodo.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.TiposPeriodo.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposPeriodo.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(TiposPeriodoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERIODO", dto.COD_TIPO_PERIODO);
        p.Add("DES_TIPO_PERIODO", dto.DES_TIPO_PERIODO);
        p.Add("DES_PREFIJO", dto.DES_PREFIJO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("FLG_QUINQUENAL", dto.FLG_QUINQUENAL ?? false);
        p.Add("FLG_BIANUAL", dto.FLG_BIANUAL ?? false);
        p.Add("FLG_ANUAL", dto.FLG_ANUAL ?? false);
        p.Add("FLG_SEMESTRAL", dto.FLG_SEMESTRAL ?? false);
        p.Add("FLG_TRIMESTRAL", dto.FLG_TRIMESTRAL ?? false);
        p.Add("FLG_MENSUAL", dto.FLG_MENSUAL ?? false);
        p.Add("FLG_QUINCENAL", dto.FLG_QUINCENAL ?? false);
        p.Add("FLG_SEMANAL", dto.FLG_SEMANAL ?? false);
        p.Add("FLG_DIARIO", dto.FLG_DIARIO ?? false);
        p.Add("FLG_MODULO_CONTABLE", dto.FLG_MODULO_CONTABLE ?? false);
        p.Add("FLG_MODULO_COSTOS", dto.FLG_MODULO_COSTOS ?? false);
        p.Add("FLG_MODULO_RRHH_NOMINA", dto.FLG_MODULO_RRHH_NOMINA ?? false);
        p.Add("FLG_MODULO_COMERCIAL", dto.FLG_MODULO_COMERCIAL ?? false);
        p.Add("FLG_MODULO_NETWORKING", dto.FLG_MODULO_NETWORKING ?? false);
        p.Add("FLG_MODULO_PLANEAMIENTO", dto.FLG_MODULO_PLANEAMIENTO ?? false);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposPeriodo.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.TiposPeriodo.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposPeriodo.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_PERIODO)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_PERIODO", COD_TIPO_PERIODO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposPeriodo.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.TiposPeriodo.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposPeriodo.Delete, p)
            };
        }
    }

}
