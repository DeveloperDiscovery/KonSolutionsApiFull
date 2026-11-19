using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ContratosModeloRepository : IContratosModeloRepository
{
    private readonly IDbConnectionFactory _db;
    public ContratosModeloRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ContratosModelo>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContratosModelo>(SP.SCHEMARrhh.ContratosModelo.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ContratosModelo?> ShowbyidAsync(string COD_TIPO_CONTRATO, int NUM_VERSION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ContratosModelo>(SP.SCHEMARrhh.ContratosModelo.Showbyid, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO, NUM_VERSION = NUM_VERSION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.ContratosModelo.Showcombox);
    }

    public async Task<IEnumerable<ContratosModelo>> SearchAsync(ContratosModeloSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ContratosModelo>(SP.SCHEMARrhh.ContratosModelo.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ContratosModeloInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONTRATO", dto.COD_TIPO_CONTRATO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("DES_CONTRATO_MODELO", dto.DES_CONTRATO_MODELO);
        p.Add("COD_REGIMEN_LABORAL", dto.COD_REGIMEN_LABORAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ContratosModelo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ContratosModelo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ContratosModelo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ContratosModeloUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CONTRATO", dto.COD_TIPO_CONTRATO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("DES_CONTRATO_MODELO", dto.DES_CONTRATO_MODELO);
        p.Add("COD_REGIMEN_LABORAL", dto.COD_REGIMEN_LABORAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ContratosModelo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ContratosModelo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ContratosModelo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CONTRATO, int NUM_VERSION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ContratosModelo.Delete, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO, NUM_VERSION = NUM_VERSION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ContratosModelo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ContratosModelo.Delete, new { COD_TIPO_CONTRATO = COD_TIPO_CONTRATO, NUM_VERSION = NUM_VERSION })
        };
    }
}

}