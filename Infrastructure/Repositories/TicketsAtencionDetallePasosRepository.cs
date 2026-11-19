using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TicketsAtencionDetallePasosRepository : ITicketsAtencionDetallePasosRepository
{
    private readonly IDbConnectionFactory _db;
    public TicketsAtencionDetallePasosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TicketsAtencionDetallePasos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketsAtencionDetallePasos>(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TicketsAtencionDetallePasos?> ShowbyidAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TicketsAtencionDetallePasos>(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Showbyid, new { NUM_TICKET = NUM_TICKET, NUM_SECUENCIA = NUM_SECUENCIA, NUM_PASO = NUM_PASO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASoporte.TicketsAtencionDetallePasos.Showcombox, "NUM_TICKET", "DES_PASO");
    }

    public async Task<IEnumerable<TicketsAtencionDetallePasos>> SearchAsync(TicketsAtencionDetallePasosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketsAtencionDetallePasos>(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TicketsAtencionDetallePasosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TICKET", dto.NUM_TICKET);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_PASO", dto.NUM_PASO);
        p.Add("DES_PASO", dto.DES_PASO);
        p.Add("DES_ANOTACIONES", dto.DES_ANOTACIONES);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencionDetallePasos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo juego de parámetros que el alta: el SP _UPDATE se
    /// genera con el mismo contrato que el _INSERT.</summary>
    public async Task<WriteResult> UpdateAsync(TicketsAtencionDetallePasosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TICKET", dto.NUM_TICKET);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_PASO", dto.NUM_PASO);
        p.Add("DES_PASO", dto.DES_PASO);
        p.Add("DES_ANOTACIONES", dto.DES_ANOTACIONES);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencionDetallePasos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA, int NUM_PASO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Delete, new { NUM_TICKET = NUM_TICKET, NUM_SECUENCIA = NUM_SECUENCIA, NUM_PASO = NUM_PASO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencionDetallePasos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencionDetallePasos.Delete, new { NUM_TICKET = NUM_TICKET, NUM_SECUENCIA = NUM_SECUENCIA, NUM_PASO = NUM_PASO })
        };
    }
}

}