using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Proyectos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProyectosRepository : IProyectosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProyectosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Proyectos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Proyectos>(SP.SCHEMAProyectos.Proyectos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Proyectos?> ShowbyidAsync(int NUM_PROYECTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Proyectos>(SP.SCHEMAProyectos.Proyectos.Showbyid, new { NUM_PROYECTO = NUM_PROYECTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProyectos.Proyectos.Showcombox);
    }

    public async Task<IEnumerable<Proyectos>> SearchAsync(ProyectosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Proyectos>(SP.SCHEMAProyectos.Proyectos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProyectosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("DES_PROYECTO", dto.DES_PROYECTO);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("COD_ENTIDAD_ESPONSOR", dto.COD_ENTIDAD_ESPONSOR);
        p.Add("COD_TIPO_PROYECTO", dto.COD_TIPO_PROYECTO);
        p.Add("COD_MONEDA_VALORIZACION", dto.COD_MONEDA_VALORIZACION);
        p.Add("VAL_PRESUPUESTO_PROYECTO", dto.VAL_PRESUPUESTO_PROYECTO);
        p.Add("VAL_COSTO_PROYECTO", dto.VAL_COSTO_PROYECTO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        // Se lee como fila suelta y no como WriteResult porque el SP devuelve la clave con su
        // propio nombre —NUM_PROYECTO— y Dapper no tiene cómo saber que eso es el Codigo.
        // El mismo resultset sirve para el camino del CATCH (ErrorNumber/ErrorMessage).
        var fila = await conn.QueryFirstOrDefaultAsync(SP.SCHEMAProyectos.Proyectos.Insert, p, commandType: CommandType.StoredProcedure);
        return LeerAlta(fila);
    }

    private static WriteResult LeerAlta(dynamic? fila)
    {
        if (fila is null) return new WriteResult { Estado = "OK", AffectedRows = 1 };
        var campos = (IDictionary<string, object?>)fila;

        if (campos.TryGetValue("ErrorNumber", out var num) && num is not null)
            return new WriteResult
            {
                Estado = "ERROR",
                AffectedRows = 0,
                ErrorNumber = Convert.ToInt32(num),
                Mensaje = campos.TryGetValue("ErrorMessage", out var msg) ? msg?.ToString() ?? "" : "",
                ErrorProcedure = campos.TryGetValue("ErrorProcedure", out var sp) ? sp?.ToString() ?? "" : ""
            };

        return new WriteResult
        {
            Estado = "OK",
            AffectedRows = 1,
            Codigo = campos.TryGetValue("NUM_PROYECTO", out var cod) ? cod?.ToString() ?? "" : ""
        };
    }

    public async Task<WriteResult> UpdateAsync(ProyectosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("DES_PROYECTO", dto.DES_PROYECTO);
        p.Add("FEC_INICIO_PROGRAMADO", dto.FEC_INICIO_PROGRAMADO);
        p.Add("FEC_FINAL_PROGRAMADO", dto.FEC_FINAL_PROGRAMADO);
        p.Add("FEC_INICIO_EJECUTADO", dto.FEC_INICIO_EJECUTADO);
        p.Add("FEC_FINAL_EJECUTADO", dto.FEC_FINAL_EJECUTADO);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("COD_ENTIDAD_ESPONSOR", dto.COD_ENTIDAD_ESPONSOR);
        p.Add("COD_TIPO_PROYECTO", dto.COD_TIPO_PROYECTO);
        p.Add("COD_MONEDA_VALORIZACION", dto.COD_MONEDA_VALORIZACION);
        p.Add("VAL_PRESUPUESTO_PROYECTO", dto.VAL_PRESUPUESTO_PROYECTO);
        p.Add("VAL_COSTO_PROYECTO", dto.VAL_COSTO_PROYECTO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.Proyectos.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAProyectos.Proyectos.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.Proyectos.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(int NUM_PROYECTO)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProyectos.Proyectos.Delete, new { NUM_PROYECTO = NUM_PROYECTO }, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAProyectos.Proyectos.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProyectos.Proyectos.Delete, new { NUM_PROYECTO = NUM_PROYECTO })
            };
        }
    }
}
