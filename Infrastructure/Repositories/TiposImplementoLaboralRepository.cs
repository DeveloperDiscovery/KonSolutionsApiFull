using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposImplementoLaboralRepository : ITiposImplementoLaboralRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposImplementoLaboralRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposImplementoLaboral>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposImplementoLaboral>(SP.SCHEMARrhh.TiposImplementoLaboral.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposImplementoLaboral?> ShowbyidAsync(string COD_TIPO_IMPLEMENTO_LABORAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposImplementoLaboral>(SP.SCHEMARrhh.TiposImplementoLaboral.Showbyid, new { COD_TIPO_IMPLEMENTO_LABORAL = COD_TIPO_IMPLEMENTO_LABORAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.TiposImplementoLaboral.Showcombox);
    }

    public async Task<IEnumerable<TiposImplementoLaboral>> SearchAsync(TiposImplementoLaboralSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposImplementoLaboral>(SP.SCHEMARrhh.TiposImplementoLaboral.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposImplementoLaboralInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_IMPLEMENTO_LABORAL", dto.COD_TIPO_IMPLEMENTO_LABORAL);
        p.Add("DES_TIPO_IMPLEMENTO_LABORAL", dto.DES_TIPO_IMPLEMENTO_LABORAL);
        p.Add("NUM_MESES_REPOSICION", dto.NUM_MESES_REPOSICION);
        p.Add("FLG_CONSIDERAR_FECHA_ENTREGA", dto.FLG_CONSIDERAR_FECHA_ENTREGA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposImplementoLaboral.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposImplementoLaboral.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposImplementoLaboral.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposImplementoLaboralUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_IMPLEMENTO_LABORAL", dto.COD_TIPO_IMPLEMENTO_LABORAL);
        p.Add("DES_TIPO_IMPLEMENTO_LABORAL", dto.DES_TIPO_IMPLEMENTO_LABORAL);
        p.Add("NUM_MESES_REPOSICION", dto.NUM_MESES_REPOSICION);
        p.Add("FLG_CONSIDERAR_FECHA_ENTREGA", dto.FLG_CONSIDERAR_FECHA_ENTREGA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposImplementoLaboral.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposImplementoLaboral.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposImplementoLaboral.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_IMPLEMENTO_LABORAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.TiposImplementoLaboral.Delete, new { COD_TIPO_IMPLEMENTO_LABORAL = COD_TIPO_IMPLEMENTO_LABORAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.TiposImplementoLaboral.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.TiposImplementoLaboral.Delete, new { COD_TIPO_IMPLEMENTO_LABORAL = COD_TIPO_IMPLEMENTO_LABORAL })
        };
    }
}

}