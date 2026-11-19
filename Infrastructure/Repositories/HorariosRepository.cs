using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class HorariosRepository : IHorariosRepository
{
    private readonly IDbConnectionFactory _db;
    public HorariosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Horarios>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Horarios>(SP.SCHEMANomina.Horarios.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Horarios?> ShowbyidAsync(string COD_HORARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Horarios>(SP.SCHEMANomina.Horarios.Showbyid, new { COD_HORARIO = COD_HORARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.Horarios.Showcombox);
    }

    public async Task<IEnumerable<Horarios>> SearchAsync(HorariosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Horarios>(SP.SCHEMANomina.Horarios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(HorariosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_HORARIO", dto.COD_HORARIO);
        p.Add("DES_HORARIO", dto.DES_HORARIO);
        p.Add("COD_TIPO_HORARIO", dto.COD_TIPO_HORARIO);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("FLG_CALCULO_HORAS_EXTRAS_ANTERIOR", dto.FLG_CALCULO_HORAS_EXTRAS_ANTERIOR ?? false);
        p.Add("FLG_CALCULO_HORAS_EXTRAS_POSTERIOR", dto.FLG_CALCULO_HORAS_EXTRAS_POSTERIOR ?? false);
        p.Add("FLG_HORARIO_PUESTO_LABORAL", dto.FLG_HORARIO_PUESTO_LABORAL ?? false);
        p.Add("NUM_MINUTOS_TOLERANCIA_INGRESO", dto.NUM_MINUTOS_TOLERANCIA_INGRESO);
        p.Add("NUM_MINUTOS_ALMUERZO", dto.NUM_MINUTOS_ALMUERZO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.Horarios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.Horarios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.Horarios.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(HorariosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_HORARIO", dto.COD_HORARIO);
        p.Add("DES_HORARIO", dto.DES_HORARIO);
        p.Add("COD_TIPO_HORARIO", dto.COD_TIPO_HORARIO);
        p.Add("COD_AREA", dto.COD_AREA);
        p.Add("FLG_CALCULO_HORAS_EXTRAS_ANTERIOR", dto.FLG_CALCULO_HORAS_EXTRAS_ANTERIOR ?? false);
        p.Add("FLG_CALCULO_HORAS_EXTRAS_POSTERIOR", dto.FLG_CALCULO_HORAS_EXTRAS_POSTERIOR ?? false);
        p.Add("FLG_HORARIO_PUESTO_LABORAL", dto.FLG_HORARIO_PUESTO_LABORAL ?? false);
        p.Add("NUM_MINUTOS_TOLERANCIA_INGRESO", dto.NUM_MINUTOS_TOLERANCIA_INGRESO);
        p.Add("NUM_MINUTOS_ALMUERZO", dto.NUM_MINUTOS_ALMUERZO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.Horarios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.Horarios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.Horarios.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_HORARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.Horarios.Delete, new { COD_HORARIO = COD_HORARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.Horarios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.Horarios.Delete, new { COD_HORARIO = COD_HORARIO })
        };
    }
}

}