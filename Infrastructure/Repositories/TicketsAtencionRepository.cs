using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Soporte;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TicketsAtencionRepository : ITicketsAtencionRepository
{
    private readonly IDbConnectionFactory _db;
    public TicketsAtencionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TicketsAtencion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketsAtencion>(SP.SCHEMASoporte.TicketsAtencion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TicketsAtencion?> ShowbyidAsync(int NUM_TICKET)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TicketsAtencion>(SP.SCHEMASoporte.TicketsAtencion.Showbyid, new { NUM_TICKET = NUM_TICKET }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASoporte.TicketsAtencion.Showcombox, "NUM_TICKET", "DES_TICKET");
    }

    public async Task<IEnumerable<TicketsAtencion>> SearchAsync(TicketsAtencionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketsAtencion>(SP.SCHEMASoporte.TicketsAtencion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TicketsAtencionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TICKET", dto.NUM_TICKET);
        p.Add("DES_TICKET", dto.DES_TICKET);
        p.Add("COD_AREA_SOLICITANTE", dto.COD_AREA_SOLICITANTE);
        p.Add("COD_CARGO_SOLICITANTE", dto.COD_CARGO_SOLICITANTE);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("DES_DETALLADA", dto.DES_DETALLADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_USUARIO_SOLICITANTE", dto.COD_USUARIO_SOLICITANTE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TicketsAtencionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TICKET", dto.NUM_TICKET);
        p.Add("DES_TICKET", dto.DES_TICKET);
        p.Add("COD_AREA_SOLICITANTE", dto.COD_AREA_SOLICITANTE);
        p.Add("COD_CARGO_SOLICITANTE", dto.COD_CARGO_SOLICITANTE);
        p.Add("COD_ENTIDAD_SOLICITANTE", dto.COD_ENTIDAD_SOLICITANTE);
        p.Add("FEC_SOLICITUD", dto.FEC_SOLICITUD);
        p.Add("DES_DETALLADA", dto.DES_DETALLADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_USUARIO_SOLICITANTE", dto.COD_USUARIO_SOLICITANTE);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_TICKET)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASoporte.TicketsAtencion.Delete, new { NUM_TICKET = NUM_TICKET }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASoporte.TicketsAtencion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASoporte.TicketsAtencion.Delete, new { NUM_TICKET = NUM_TICKET })
        };
    }
}

}