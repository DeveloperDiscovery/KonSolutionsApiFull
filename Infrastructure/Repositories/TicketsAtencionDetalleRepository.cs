using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TicketsAtencionDetalleRepository : ITicketsAtencionDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public TicketsAtencionDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TicketsAtencionDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketsAtencionDetalle>(SP.SCHEMASoporte.TicketsAtencionDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TicketsAtencionDetalle?> ShowbyidAsync(int NUM_TICKET, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TicketsAtencionDetalle>(SP.SCHEMASoporte.TicketsAtencionDetalle.Showbyid, new { NUM_TICKET = NUM_TICKET, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASoporte.TicketsAtencionDetalle.Showcombox, "NUM_TICKET", "DES_DETALLADA");
    }

    public async Task<IEnumerable<TicketsAtencionDetalle>> SearchAsync(TicketsAtencionDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketsAtencionDetalle>(SP.SCHEMASoporte.TicketsAtencionDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TicketsAtencionDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TICKET", dto.NUM_TICKET);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_AREA_EJECUTORA", dto.COD_AREA_EJECUTORA);
        p.Add("COD_CARGO_EJECUTORA", dto.COD_CARGO_EJECUTORA);
        p.Add("COD_ENTIDAD_EJECUTORA", dto.COD_ENTIDAD_EJECUTORA);
        p.Add("FEC_ATENCION_PROGRAMADA", dto.FEC_ATENCION_PROGRAMADA);
        p.Add("FEC_ATENCION_EJECUTADA", dto.FEC_ATENCION_EJECUTADA);
        p.Add("DES_DETALLADA", dto.DES_DETALLADA);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_USUARIO_EJECUTORA", dto.COD_USUARIO_EJECUTORA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencionDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencionDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencionDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TicketsAtencionDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TICKET", dto.NUM_TICKET);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TAREA", dto.COD_TAREA);
        p.Add("COD_AREA_EJECUTORA", dto.COD_AREA_EJECUTORA);
        p.Add("COD_CARGO_EJECUTORA", dto.COD_CARGO_EJECUTORA);
        p.Add("COD_ENTIDAD_EJECUTORA", dto.COD_ENTIDAD_EJECUTORA);
        p.Add("FEC_ATENCION_PROGRAMADA", dto.FEC_ATENCION_PROGRAMADA);
        p.Add("FEC_ATENCION_EJECUTADA", dto.FEC_ATENCION_EJECUTADA);
        p.Add("DES_DETALLADA", dto.DES_DETALLADA);
        p.Add("NUM_AUTORIZACION", dto.NUM_AUTORIZACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_USUARIO_EJECUTORA", dto.COD_USUARIO_EJECUTORA);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencionDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencionDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencionDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_TICKET, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencionDetalle.Delete, new { NUM_TICKET = NUM_TICKET, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencionDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencionDetalle.Delete, new { NUM_TICKET = NUM_TICKET, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}