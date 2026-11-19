using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FeriadosRepository : IFeriadosRepository
{
    private readonly IDbConnectionFactory _db;
    public FeriadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Feriados>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Feriados>(SP.SCHEMAEstandar.Feriados.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Feriados?> ShowbyidAsync(DateTime FEC_FERIADO)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters(); p.Add("FEC_FERIADO", FEC_FERIADO.Date, DbType.Date);
        return await conn.QueryFirstOrDefaultAsync<Feriados>(SP.SCHEMAEstandar.Feriados.Showbyid, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Feriados>> SearchAsync(FeriadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Feriados>(SP.SCHEMAEstandar.Feriados.Search, new { SEARCH = dto.Search ?? "" }, commandType: CommandType.StoredProcedure);
    }

    private async Task<WriteResult> EscribirAsync(string sp, DynamicParameters p)
    {
        using var conn = _db.CreateConnection();
        try
        {
            await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult { Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number, ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p) };
        }
    }

    public Task<WriteResult> InsertAsync(FeriadosInsertDto dto, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("FEC_FERIADO", dto.FEC_FERIADO.Date, DbType.Date); p.Add("DES_FERIADO", dto.DES_FERIADO.Trim()); p.Add("FLG_ANUAL", dto.FLG_ANUAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario); p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return EscribirAsync(SP.SCHEMAEstandar.Feriados.Insert, p);
    }

    public Task<WriteResult> UpdateAsync(FeriadosUpdateDto dto, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("FEC_FERIADO", dto.FEC_FERIADO.Date, DbType.Date); p.Add("DES_FERIADO", dto.DES_FERIADO.Trim()); p.Add("FLG_ANUAL", dto.FLG_ANUAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario); p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return EscribirAsync(SP.SCHEMAEstandar.Feriados.Update, p);
    }

    public Task<WriteResult> DeleteAsync(DateTime FEC_FERIADO)
    {
        var p = new DynamicParameters(); p.Add("FEC_FERIADO", FEC_FERIADO.Date, DbType.Date);
        return EscribirAsync(SP.SCHEMAEstandar.Feriados.Delete, p);
    }
}
