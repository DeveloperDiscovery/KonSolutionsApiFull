using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RegistroProduccionTiposOperacionRepository : IRegistroProduccionTiposOperacionRepository
{
    private readonly IDbConnectionFactory _db;
    public RegistroProduccionTiposOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RegistroProduccionTiposOperacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroProduccionTiposOperacion>(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<RegistroProduccionTiposOperacion?> ShowbyidAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RegistroProduccionTiposOperacion>(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Showbyid, new { NUM_REGISTRO_PRODUCCION = NUM_REGISTRO_PRODUCCION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Showcombox, "NUM_REGISTRO_PRODUCCION", "DES_TIPO_OPERACION_PRODUCCION");
    }

    public async Task<IEnumerable<RegistroProduccionTiposOperacion>> SearchAsync(RegistroProduccionTiposOperacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RegistroProduccionTiposOperacion>(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RegistroProduccionTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO_PRODUCCION", dto.NUM_REGISTRO_PRODUCCION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("CAN_LEAD_TIME", dto.CAN_LEAD_TIME);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("CAN_PRODUCIDA_SECUNDARIA", dto.CAN_PRODUCIDA_SECUNDARIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(RegistroProduccionTiposOperacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REGISTRO_PRODUCCION", dto.NUM_REGISTRO_PRODUCCION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_OPERACION_PRODUCCION", dto.COD_TIPO_OPERACION_PRODUCCION);
        p.Add("CAN_LEAD_TIME", dto.CAN_LEAD_TIME);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("CAN_PRODUCIDA_SECUNDARIA", dto.CAN_PRODUCIDA_SECUNDARIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_REGISTRO_PRODUCCION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Delete, new { NUM_REGISTRO_PRODUCCION = NUM_REGISTRO_PRODUCCION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.RegistroProduccionTiposOperacion.Delete, new { NUM_REGISTRO_PRODUCCION = NUM_REGISTRO_PRODUCCION, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}