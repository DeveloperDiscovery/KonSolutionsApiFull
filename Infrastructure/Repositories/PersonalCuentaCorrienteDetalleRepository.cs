using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalCuentaCorrienteDetalleRepository : IPersonalCuentaCorrienteDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalCuentaCorrienteDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalCuentaCorrienteDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalCuentaCorrienteDetalle>(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalCuentaCorrienteDetalle?> ShowbyidAsync(int NUM_CUENTA_CORRIENTE, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalCuentaCorrienteDetalle>(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Showbyid, new { NUM_CUENTA_CORRIENTE = NUM_CUENTA_CORRIENTE, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Showcombox, "NUM_CUENTA_CORRIENTE", "DES_MONEDA");
    }

    public async Task<IEnumerable<PersonalCuentaCorrienteDetalle>> SearchAsync(PersonalCuentaCorrienteDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalCuentaCorrienteDetalle>(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalCuentaCorrienteDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CUENTA_CORRIENTE", dto.NUM_CUENTA_CORRIENTE);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CONCEPTO_REMUNERATIVO_AMORTIZACION", dto.COD_CONCEPTO_REMUNERATIVO_AMORTIZACION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_AMORTIZACION", dto.VAL_AMORTIZACION);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalCuentaCorrienteDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CUENTA_CORRIENTE", dto.NUM_CUENTA_CORRIENTE);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CONCEPTO_REMUNERATIVO_AMORTIZACION", dto.COD_CONCEPTO_REMUNERATIVO_AMORTIZACION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_AMORTIZACION", dto.VAL_AMORTIZACION);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CUENTA_CORRIENTE, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Delete, new { NUM_CUENTA_CORRIENTE = NUM_CUENTA_CORRIENTE, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCuentaCorrienteDetalle.Delete, new { NUM_CUENTA_CORRIENTE = NUM_CUENTA_CORRIENTE, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}