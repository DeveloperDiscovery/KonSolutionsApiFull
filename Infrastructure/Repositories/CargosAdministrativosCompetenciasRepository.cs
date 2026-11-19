using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CargosAdministrativosCompetenciasRepository : ICargosAdministrativosCompetenciasRepository
{
    private readonly IDbConnectionFactory _db;
    public CargosAdministrativosCompetenciasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CargosAdministrativosCompetencias>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CargosAdministrativosCompetencias>(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CargosAdministrativosCompetencias?> ShowbyidAsync(int COD_CARGO, string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CargosAdministrativosCompetencias>(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Showbyid, new { COD_CARGO = COD_CARGO, COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.CargosAdministrativosCompetencias.Showcombox);
    }

    public async Task<IEnumerable<CargosAdministrativosCompetencias>> SearchAsync(CargosAdministrativosCompetenciasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CargosAdministrativosCompetencias>(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CargosAdministrativosCompetenciasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosAdministrativosCompetencias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CargosAdministrativosCompetenciasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_COMPETENCIA", dto.COD_COMPETENCIA);
        p.Add("COD_TIPO_COMPETENCIA", dto.COD_TIPO_COMPETENCIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosAdministrativosCompetencias.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CARGO, string COD_COMPETENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Delete, new { COD_CARGO = COD_CARGO, COD_COMPETENCIA = COD_COMPETENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosAdministrativosCompetencias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosAdministrativosCompetencias.Delete, new { COD_CARGO = COD_CARGO, COD_COMPETENCIA = COD_COMPETENCIA })
        };
    }
}

}