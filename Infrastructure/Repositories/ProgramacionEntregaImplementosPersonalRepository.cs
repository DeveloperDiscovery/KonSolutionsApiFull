using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProgramacionEntregaImplementosPersonalRepository : IProgramacionEntregaImplementosPersonalRepository
{
    private readonly IDbConnectionFactory _db;
    public ProgramacionEntregaImplementosPersonalRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProgramacionEntregaImplementosPersonal>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionEntregaImplementosPersonal>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProgramacionEntregaImplementosPersonal?> ShowbyidAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProgramacionEntregaImplementosPersonal>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Showbyid, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Showcombox, "NUM_ENTREGA_IMPLEMENTOS", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<ProgramacionEntregaImplementosPersonal>> SearchAsync(ProgramacionEntregaImplementosPersonalSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionEntregaImplementosPersonal>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProgramacionEntregaImplementosPersonalInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENTREGA_IMPLEMENTOS", dto.NUM_ENTREGA_IMPLEMENTOS);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProgramacionEntregaImplementosPersonalUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENTREGA_IMPLEMENTOS", dto.NUM_ENTREGA_IMPLEMENTOS);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Delete, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonal.Delete, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL })
        };
    }
}

}