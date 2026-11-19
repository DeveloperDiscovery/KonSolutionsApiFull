using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class PersonalCuentaCorrienteRepository : IPersonalCuentaCorrienteRepository
{
    private readonly IDbConnectionFactory _db;
    public PersonalCuentaCorrienteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<PersonalCuentaCorriente>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalCuentaCorriente>(SP.SCHEMARrhh.PersonalCuentaCorriente.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<PersonalCuentaCorriente?> ShowbyidAsync(int NUM_CUENTA_CORRIENTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<PersonalCuentaCorriente>(SP.SCHEMARrhh.PersonalCuentaCorriente.Showbyid, new { NUM_CUENTA_CORRIENTE = NUM_CUENTA_CORRIENTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.PersonalCuentaCorriente.Showcombox, "NUM_CUENTA_CORRIENTE", "DES_DETALLE");
    }

    public async Task<IEnumerable<PersonalCuentaCorriente>> SearchAsync(PersonalCuentaCorrienteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PersonalCuentaCorriente>(SP.SCHEMARrhh.PersonalCuentaCorriente.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(PersonalCuentaCorrienteInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CUENTA_CORRIENTE", dto.NUM_CUENTA_CORRIENTE);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_TIPO_CUENTA_CORRIENTE", dto.COD_TIPO_CUENTA_CORRIENTE);
        p.Add("COD_CONCEPTO_REMUNERATIVO_CARGO", dto.COD_CONCEPTO_REMUNERATIVO_CARGO);
        p.Add("FEC_CUENTA_CORRIENTE", dto.FEC_CUENTA_CORRIENTE);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_IMPORTE", dto.VAL_IMPORTE);
        p.Add("VAL_AMORTIZACION", dto.VAL_AMORTIZACION);
        p.Add("VAL_SALDO", dto.VAL_SALDO);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCuentaCorriente.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCuentaCorriente.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCuentaCorriente.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(PersonalCuentaCorrienteUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CUENTA_CORRIENTE", dto.NUM_CUENTA_CORRIENTE);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_TIPO_CUENTA_CORRIENTE", dto.COD_TIPO_CUENTA_CORRIENTE);
        p.Add("COD_CONCEPTO_REMUNERATIVO_CARGO", dto.COD_CONCEPTO_REMUNERATIVO_CARGO);
        p.Add("FEC_CUENTA_CORRIENTE", dto.FEC_CUENTA_CORRIENTE);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_IMPORTE", dto.VAL_IMPORTE);
        p.Add("VAL_AMORTIZACION", dto.VAL_AMORTIZACION);
        p.Add("VAL_SALDO", dto.VAL_SALDO);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("DES_DETALLE", dto.DES_DETALLE);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCuentaCorriente.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCuentaCorriente.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCuentaCorriente.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_CUENTA_CORRIENTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.PersonalCuentaCorriente.Delete, new { NUM_CUENTA_CORRIENTE = NUM_CUENTA_CORRIENTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.PersonalCuentaCorriente.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.PersonalCuentaCorriente.Delete, new { NUM_CUENTA_CORRIENTE = NUM_CUENTA_CORRIENTE })
        };
    }
}

}