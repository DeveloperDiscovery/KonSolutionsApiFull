using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rfid;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

/// <summary>La cola de trabajos de impresión (RFID.TRABAJOS_IMPRESION): la web los pide y el servicio del agente los toma.</summary>
public class RfidImpresionRepository : IRfidImpresionRepository
{
    private readonly IDbConnectionFactory _db;
    public RfidImpresionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<RfidTrabajoImpresion>> SearchAsync(RfidTrabajoImpresionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", dto.NUM_EMISION);
        p.Add("FLG_SOLO_ACTIVOS", dto.FLG_SOLO_ACTIVOS ?? false);
        p.Add("TOP", dto.TOP);
        return await conn.QueryAsync<RfidTrabajoImpresion>(SP.SCHEMARfid.TrabajosImpresion.Search, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<RfidTrabajoImpresion?> ShowbyidAsync(int NUM_TRABAJO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RfidTrabajoImpresion>(
            SP.SCHEMARfid.TrabajosImpresion.Showbyid, new { NUM_TRABAJO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<(WriteResult Resultado, RfidTrabajoCreado? Creado)> SolicitarAsync(RfidImprimirDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_EMISION", dto.NUM_EMISION);
        p.Add("COD_PRODUCTO_EQUIPO", Nulo(dto.COD_PRODUCTO_EQUIPO));
        p.Add("CAN_SOLICITADAS", dto.CAN_SOLICITADAS);
        p.Add("COD_USUARIO_SOLICITUD", codUsuario);
        p.Add("COD_ESTACION_SOLICITUD", codEstacion);
        try
        {
            var c = await conn.QueryFirstOrDefaultAsync<RfidTrabajoCreado>(SP.SCHEMARfid.TrabajosImpresion.Solicitar, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1, Codigo = (c?.NUM_TRABAJO ?? 0).ToString() }, c);
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMARfid.TrabajosImpresion.Solicitar, p), null); }
    }

    public async Task<(WriteResult Resultado, RfidTrabajoEstado? Estado)> CancelarAsync(int NUM_TRABAJO, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TRABAJO", NUM_TRABAJO);
        p.Add("COD_USUARIO", codUsuario);
        try
        {
            var e = await conn.QueryFirstOrDefaultAsync<RfidTrabajoEstado>(SP.SCHEMARfid.TrabajosImpresion.Cancelar, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1 }, e);
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMARfid.TrabajosImpresion.Cancelar, p), null); }
    }

    public async Task<(WriteResult Resultado, RfidTrabajoImpresion? Trabajo)> ReclamarAsync(RfidTrabajoReclamarDto dto, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO_EQUIPO", dto.COD_PRODUCTO_EQUIPO?.Trim());
        p.Add("COD_USUARIO_AGENTE", codUsuario);
        p.Add("MINUTOS_SIN_LATIDO", dto.MINUTOS_SIN_LATIDO ?? 3);
        try
        {
            var t = await conn.QueryFirstOrDefaultAsync<RfidTrabajoImpresion>(SP.SCHEMARfid.TrabajosImpresion.Reclamar, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = t is null ? 0 : 1 }, t);
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMARfid.TrabajosImpresion.Reclamar, p), null); }
    }

    public async Task<(WriteResult Resultado, RfidTrabajoEstado? Estado)> ProgresoAsync(int NUM_TRABAJO, RfidTrabajoProgresoDto dto, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TRABAJO", NUM_TRABAJO);
        p.Add("CAN_IMPRESAS", dto.CAN_IMPRESAS);
        p.Add("CAN_FALLIDAS", dto.CAN_FALLIDAS);
        p.Add("DES_MENSAJE", Nulo(dto.DES_MENSAJE));
        p.Add("COD_USUARIO_AGENTE", codUsuario);
        try
        {
            var e = await conn.QueryFirstOrDefaultAsync<RfidTrabajoEstado>(SP.SCHEMARfid.TrabajosImpresion.Progreso, p, commandType: CommandType.StoredProcedure);
            return (new WriteResult { Estado = "OK", AffectedRows = 1 }, e);
        }
        catch (SqlException ex) { return (Error(ex, SP.SCHEMARfid.TrabajosImpresion.Progreso, p), null); }
    }

    public async Task<WriteResult> TerminarAsync(int NUM_TRABAJO, RfidTrabajoTerminarDto dto, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_TRABAJO", NUM_TRABAJO);
        p.Add("COD_ESTADO_TRABAJO", dto.COD_ESTADO_TRABAJO?.Trim().ToUpperInvariant());
        p.Add("CAN_IMPRESAS", dto.CAN_IMPRESAS);
        p.Add("CAN_FALLIDAS", dto.CAN_FALLIDAS);
        p.Add("DES_MENSAJE", Nulo(dto.DES_MENSAJE));
        p.Add("COD_USUARIO_AGENTE", codUsuario);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMARfid.TrabajosImpresion.Terminar, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex) { return Error(ex, SP.SCHEMARfid.TrabajosImpresion.Terminar, p); }
    }

    private static string? Nulo(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    // Los SP lanzan con THROW (50624-50656): el texto del error ES el mensaje para el usuario.
    private static WriteResult Error(SqlException ex, string sp, DynamicParameters p) => new()
    {
        Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
        ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p)
    };
}
