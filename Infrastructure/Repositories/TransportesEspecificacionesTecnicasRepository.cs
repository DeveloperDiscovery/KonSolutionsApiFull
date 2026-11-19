using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TransportesEspecificacionesTecnicasRepository : ITransportesEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public TransportesEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TransportesEspecificacionesTecnicas>> ShowallAsync(string COD_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TransportesEspecificacionesTecnicas>(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Showall, new { COD_TRANSPORTE = COD_TRANSPORTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TransportesEspecificacionesTecnicas?> ShowbyidAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TransportesEspecificacionesTecnicas>(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Showbyid, new { COD_TRANSPORTE = COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Showcombox, new { COD_TRANSPORTE = COD_TRANSPORTE });
    }

    public async Task<IEnumerable<TransportesEspecificacionesTecnicas>> SearchAsync(TransportesEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TransportesEspecificacionesTecnicas>(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Search, new { COD_TRANSPORTE = dto.COD_TRANSPORTE, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TransportesEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPECIFICACION_TENICA", dto.VAL_ESPECIFICACION_TENICA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TransportesEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPECIFICACION_TENICA", dto.VAL_ESPECIFICACION_TENICA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TRANSPORTE, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Delete, new { COD_TRANSPORTE = COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TransportesEspecificacionesTecnicas.Delete, new { COD_TRANSPORTE = COD_TRANSPORTE, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA })
        };
    }
}

}