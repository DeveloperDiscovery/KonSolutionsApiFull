using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProgramacionVersionRepository : IProgramacionVersionRepository
{
    private readonly IDbConnectionFactory _db;
    public ProgramacionVersionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProgramacionVersion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionVersion>(SP.SCHEMAPlaneamiento.ProgramacionVersion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProgramacionVersion?> ShowbyidAsync(int NUM_PROGRAMA, int NUM_VERSION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProgramacionVersion>(SP.SCHEMAPlaneamiento.ProgramacionVersion.Showbyid, new { NUM_PROGRAMA = NUM_PROGRAMA, NUM_VERSION = NUM_VERSION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.ProgramacionVersion.Showcombox, "NUM_PROGRAMA", "DES_VERSION");
    }

    public async Task<IEnumerable<ProgramacionVersion>> SearchAsync(ProgramacionVersionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionVersion>(SP.SCHEMAPlaneamiento.ProgramacionVersion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProgramacionVersionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROGRAMA", dto.NUM_PROGRAMA);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("DES_VERSION", dto.DES_VERSION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("TAS_EFICIENCIA_TIEMPO", dto.TAS_EFICIENCIA_TIEMPO);
        p.Add("TAS_EFICIENCIA_CANTIDAD", dto.TAS_EFICIENCIA_CANTIDAD);
        p.Add("FEC_PROGRAMADA_INICIO", dto.FEC_PROGRAMADA_INICIO);
        p.Add("FEC_PROGRAMADA_FINAL", dto.FEC_PROGRAMADA_FINAL);
        p.Add("FEC_REAL_INICIO", dto.FEC_REAL_INICIO);
        p.Add("FEC_REAL_FINAL", dto.FEC_REAL_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionVersion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionVersion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionVersion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProgramacionVersionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROGRAMA", dto.NUM_PROGRAMA);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("DES_VERSION", dto.DES_VERSION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("TAS_EFICIENCIA_TIEMPO", dto.TAS_EFICIENCIA_TIEMPO);
        p.Add("TAS_EFICIENCIA_CANTIDAD", dto.TAS_EFICIENCIA_CANTIDAD);
        p.Add("FEC_PROGRAMADA_INICIO", dto.FEC_PROGRAMADA_INICIO);
        p.Add("FEC_PROGRAMADA_FINAL", dto.FEC_PROGRAMADA_FINAL);
        p.Add("FEC_REAL_INICIO", dto.FEC_REAL_INICIO);
        p.Add("FEC_REAL_FINAL", dto.FEC_REAL_FINAL);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionVersion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionVersion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionVersion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROGRAMA, int NUM_VERSION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionVersion.Delete, new { NUM_PROGRAMA = NUM_PROGRAMA, NUM_VERSION = NUM_VERSION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionVersion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionVersion.Delete, new { NUM_PROGRAMA = NUM_PROGRAMA, NUM_VERSION = NUM_VERSION })
        };
    }
}

}