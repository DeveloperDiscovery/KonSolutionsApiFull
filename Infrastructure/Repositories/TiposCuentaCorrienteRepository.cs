using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Nomina;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposCuentaCorrienteRepository : ITiposCuentaCorrienteRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposCuentaCorrienteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposCuentaCorriente>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCuentaCorriente>(SP.SCHEMANomina.TiposCuentaCorriente.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposCuentaCorriente?> ShowbyidAsync(string COD_TIPO_CUENTA_CORRIENTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposCuentaCorriente>(SP.SCHEMANomina.TiposCuentaCorriente.Showbyid, new { COD_TIPO_CUENTA_CORRIENTE = COD_TIPO_CUENTA_CORRIENTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMANomina.TiposCuentaCorriente.Showcombox);
    }

    public async Task<IEnumerable<TiposCuentaCorriente>> SearchAsync(TiposCuentaCorrienteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposCuentaCorriente>(SP.SCHEMANomina.TiposCuentaCorriente.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposCuentaCorrienteInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CUENTA_CORRIENTE", dto.COD_TIPO_CUENTA_CORRIENTE);
        p.Add("DES_TIPO_CUENTA_CORRIENTE", dto.DES_TIPO_CUENTA_CORRIENTE);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposCuentaCorriente.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposCuentaCorriente.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposCuentaCorriente.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposCuentaCorrienteUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_CUENTA_CORRIENTE", dto.COD_TIPO_CUENTA_CORRIENTE);
        p.Add("DES_TIPO_CUENTA_CORRIENTE", dto.DES_TIPO_CUENTA_CORRIENTE);
        p.Add("COD_CONCEPTO_REMUNERATIVO", dto.COD_CONCEPTO_REMUNERATIVO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposCuentaCorriente.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposCuentaCorriente.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposCuentaCorriente.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_CUENTA_CORRIENTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMANomina.TiposCuentaCorriente.Delete, new { COD_TIPO_CUENTA_CORRIENTE = COD_TIPO_CUENTA_CORRIENTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMANomina.TiposCuentaCorriente.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMANomina.TiposCuentaCorriente.Delete, new { COD_TIPO_CUENTA_CORRIENTE = COD_TIPO_CUENTA_CORRIENTE })
        };
    }
}

}