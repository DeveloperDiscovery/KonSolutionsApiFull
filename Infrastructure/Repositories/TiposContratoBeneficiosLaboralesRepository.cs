using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposContratoBeneficiosLaboralesRepository : ITiposContratoBeneficiosLaboralesRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposContratoBeneficiosLaboralesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposContratoBeneficiosLaborales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposContratoBeneficiosLaborales>(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposContratoBeneficiosLaborales?> ShowbyidAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposContratoBeneficiosLaborales>(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Showbyid, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL = COD_BENEFICIO_LABORAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Showcombox);
    }

    public async Task<IEnumerable<TiposContratoBeneficiosLaborales>> SearchAsync(TiposContratoBeneficiosLaboralesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposContratoBeneficiosLaborales>(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposContratoBeneficiosLaboralesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONTRATO", dto.COD_TIPO_CONTRATO);
        p.Add("COD_BENEFICIO_LABORAL", dto.COD_BENEFICIO_LABORAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CONTRATO, string COD_BENEFICIO_LABORAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Delete, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL = COD_BENEFICIO_LABORAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposContratoBeneficiosLaborales.Delete, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO, COD_BENEFICIO_LABORAL = COD_BENEFICIO_LABORAL })
        };
    }
}

}