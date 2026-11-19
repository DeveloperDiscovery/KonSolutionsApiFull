using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class HorariosDetalleRepository : IHorariosDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public HorariosDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<HorariosDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<HorariosDetalle>(SP.SCHEMANomina.HorariosDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<HorariosDetalle?> ShowbyidAsync(string COD_HORARIO, int NUM_DIA_SEMANA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<HorariosDetalle>(SP.SCHEMANomina.HorariosDetalle.Showbyid, new { COD_HORARIO = COD_HORARIO, NUM_DIA_SEMANA = NUM_DIA_SEMANA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.HorariosDetalle.Showcombox);
    }

    public async Task<IEnumerable<HorariosDetalle>> SearchAsync(HorariosDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<HorariosDetalle>(SP.SCHEMANomina.HorariosDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(HorariosDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_HORARIO", dto.COD_HORARIO);
        p.Add("NUM_DIA_SEMANA", dto.NUM_DIA_SEMANA);
        p.Add("HOR_INGRESO", dto.HOR_INGRESO);
        p.Add("HOR_SALIDA", dto.HOR_SALIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.HorariosDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.HorariosDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.HorariosDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(HorariosDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_HORARIO", dto.COD_HORARIO);
        p.Add("NUM_DIA_SEMANA", dto.NUM_DIA_SEMANA);
        p.Add("HOR_INGRESO", dto.HOR_INGRESO);
        p.Add("HOR_SALIDA", dto.HOR_SALIDA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.HorariosDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.HorariosDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.HorariosDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_HORARIO, int NUM_DIA_SEMANA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.HorariosDetalle.Delete, new { COD_HORARIO = COD_HORARIO, NUM_DIA_SEMANA = NUM_DIA_SEMANA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.HorariosDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.HorariosDetalle.Delete, new { COD_HORARIO = COD_HORARIO, NUM_DIA_SEMANA = NUM_DIA_SEMANA })
        };
    }
}

}