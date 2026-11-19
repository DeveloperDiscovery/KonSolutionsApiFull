using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposContratoRepository : ITiposContratoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposContratoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposContrato>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposContrato>(SP.SCHEMARrhh.TiposContrato.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposContrato?> ShowbyidAsync(string COD_TIPO_CONTRATO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposContrato>(SP.SCHEMARrhh.TiposContrato.Showbyid, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposContrato.Showcombox);
    }

    public async Task<IEnumerable<TiposContrato>> SearchAsync(TiposContratoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposContrato>(SP.SCHEMARrhh.TiposContrato.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposContratoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONTRATO", dto.COD_TIPO_CONTRATO);
        p.Add("DES_TIPO_CONTRATO", dto.DES_TIPO_CONTRATO);
        p.Add("FLG_INDEFINIDO", dto.FLG_INDEFINIDO ?? false);
        p.Add("DES_SUB_TIPO", dto.DES_SUB_TIPO);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_DURACION_MAXIMA", dto.CAN_DURACION_MAXIMA);
        p.Add("FLG_CAUSA_OBJETIVA", dto.FLG_CAUSA_OBJETIVA ?? false);
        p.Add("DES_USO_CONTRACTUAL", dto.DES_USO_CONTRACTUAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposContrato.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposContrato.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposContrato.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposContratoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONTRATO", dto.COD_TIPO_CONTRATO);
        p.Add("DES_TIPO_CONTRATO", dto.DES_TIPO_CONTRATO);
        p.Add("FLG_INDEFINIDO", dto.FLG_INDEFINIDO ?? false);
        p.Add("DES_SUB_TIPO", dto.DES_SUB_TIPO);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO", dto.COD_UNIDAD_MEDIDA_TIEMPO);
        p.Add("CAN_DURACION_MAXIMA", dto.CAN_DURACION_MAXIMA);
        p.Add("FLG_CAUSA_OBJETIVA", dto.FLG_CAUSA_OBJETIVA ?? false);
        p.Add("DES_USO_CONTRACTUAL", dto.DES_USO_CONTRACTUAL);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposContrato.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposContrato.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposContrato.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CONTRATO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposContrato.Delete, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposContrato.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposContrato.Delete, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO })
        };
    }
}

}