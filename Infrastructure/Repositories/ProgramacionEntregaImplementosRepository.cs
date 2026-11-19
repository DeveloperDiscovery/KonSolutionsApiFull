using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProgramacionEntregaImplementosRepository : IProgramacionEntregaImplementosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProgramacionEntregaImplementosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProgramacionEntregaImplementos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionEntregaImplementos>(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProgramacionEntregaImplementos?> ShowbyidAsync(int NUM_ENTREGA_IMPLEMENTOS)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProgramacionEntregaImplementos>(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Showbyid, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.ProgramacionEntregaImplementos.Showcombox, "NUM_ENTREGA_IMPLEMENTOS", "DES_ENTREGA_IMPLEMENTOS");
    }

    public async Task<IEnumerable<ProgramacionEntregaImplementos>> SearchAsync(ProgramacionEntregaImplementosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionEntregaImplementos>(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProgramacionEntregaImplementosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENTREGA_IMPLEMENTOS", dto.NUM_ENTREGA_IMPLEMENTOS);
        p.Add("DES_ENTREGA_IMPLEMENTOS", dto.DES_ENTREGA_IMPLEMENTOS);
        p.Add("FEC_ENTREGA_PROGRAMADA", dto.FEC_ENTREGA_PROGRAMADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProgramacionEntregaImplementosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENTREGA_IMPLEMENTOS", dto.NUM_ENTREGA_IMPLEMENTOS);
        p.Add("DES_ENTREGA_IMPLEMENTOS", dto.DES_ENTREGA_IMPLEMENTOS);
        p.Add("FEC_ENTREGA_PROGRAMADA", dto.FEC_ENTREGA_PROGRAMADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Delete, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementos.Delete, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS })
        };
    }
}

}