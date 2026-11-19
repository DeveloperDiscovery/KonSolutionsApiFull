using System.Data;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AlmacenesCierreRepository : IAlmacenesCierreRepository
{
    private readonly IDbConnectionFactory _db;
    public AlmacenesCierreRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AlmacenesCierre>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AlmacenesCierre>(
            SP.SCHEMALogistica.AlmacenesCierre.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AlmacenesCierre?> ShowbyidAsync(int NUM_CIERRE_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AlmacenesCierre>(
            SP.SCHEMALogistica.AlmacenesCierre.Showbyid,
            new { NUM_CIERRE_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AlmacenesCierre>> SearchAsync(AlmacenesCierreSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", string.IsNullOrWhiteSpace(dto.SEARCH) ? null : dto.SEARCH.Trim());
        p.Add("COD_ALMACEN", string.IsNullOrWhiteSpace(dto.COD_ALMACEN) ? null : dto.COD_ALMACEN.Trim());
        p.Add("FLG_CERRADO", dto.FLG_CERRADO);
        p.Add("FEC_REFERENCIA", dto.FEC_REFERENCIA?.Date, DbType.Date);
        return await conn.QueryAsync<AlmacenesCierre>(
            SP.SCHEMALogistica.AlmacenesCierre.Search, p, commandType: CommandType.StoredProcedure);
    }

    /// <summary>"¿Este almacén admite operar en esa fecha (y hoy)?" — la misma función que usan los SP de movimientos.</summary>
    public async Task<AlmacenesCierreConsulta?> ConsultarAsync(string COD_ALMACEN, DateTime? FEC_MOVIMENTO)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", COD_ALMACEN?.Trim());
        p.Add("FEC_MOVIMENTO", FEC_MOVIMENTO?.Date, DbType.Date);
        return await conn.QueryFirstOrDefaultAsync<AlmacenesCierreConsulta>(
            SP.SCHEMALogistica.AlmacenesCierre.Consultar, p, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Cierra uno o varios almacenes en una sola llamada: el SP lo hace en una transacción (todo o nada). Los
    /// almacenes viajan como XML —igual que las líneas de la confirmación de recepción— y no como JSON. El SP devuelve una
    /// fila por cierre creado; sus números quedan en Codigo, separados por coma.</summary>
    public async Task<WriteResult> InsertAsync(AlmacenesCierreInsertDto dto, string codUsuario, string codEstacion)
    {
        var xml = new XElement("almacenes",
            (dto.COD_ALMACENES ?? new()).Where(c => !string.IsNullOrWhiteSpace(c))
                .Select(c => new XElement("a", new XAttribute("cod", c.Trim()))));
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("XML_ALMACENES", xml.ToString(SaveOptions.DisableFormatting), DbType.Xml);
        p.Add("FEC_DESDE", dto.FEC_DESDE.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA.Date, DbType.Date);
        p.Add("DES_OBSERVACION", string.IsNullOrWhiteSpace(dto.DES_OBSERVACION) ? null : dto.DES_OBSERVACION.Trim());
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var creados = (await conn.QueryAsync<AlmacenesCierreCreado>(
                SP.SCHEMALogistica.AlmacenesCierre.Insert, p, commandType: CommandType.StoredProcedure)).ToList();
            return new WriteResult
            {
                Estado = "OK", AffectedRows = creados.Count,
                Codigo = string.Join(",", creados.Select(c => c.NUM_CIERRE_ALMACEN))
            };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMALogistica.AlmacenesCierre.Insert, p);
        }
    }

    public async Task<WriteResult> UpdateAsync(AlmacenesCierreUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CIERRE_ALMACEN", dto.NUM_CIERRE_ALMACEN);
        p.Add("FEC_DESDE", dto.FEC_DESDE.Date, DbType.Date);
        p.Add("FEC_HASTA", dto.FEC_HASTA.Date, DbType.Date);
        p.Add("DES_OBSERVACION", string.IsNullOrWhiteSpace(dto.DES_OBSERVACION) ? null : dto.DES_OBSERVACION.Trim());
        p.Add("COD_USUARIO_ACTUALIZACION", codUsuario);
        p.Add("COD_ESTACION_ACTUALIZACION", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMALogistica.AlmacenesCierre.Update, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMALogistica.AlmacenesCierre.Update, p);
        }
    }

    public async Task<WriteResult> ReabrirAsync(int NUM_CIERRE_ALMACEN, AlmacenesCierreReabrirDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CIERRE_ALMACEN", NUM_CIERRE_ALMACEN);
        p.Add("DES_MOTIVO_REAPERTURA", dto.DES_MOTIVO_REAPERTURA?.Trim());
        p.Add("COD_USUARIO_REAPERTURA", codUsuario);
        p.Add("COD_ESTACION_REAPERTURA", codEstacion);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMALogistica.AlmacenesCierre.Reabrir, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMALogistica.AlmacenesCierre.Reabrir, p);
        }
    }

    public async Task<WriteResult> DeleteAsync(int NUM_CIERRE_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_CIERRE_ALMACEN", NUM_CIERRE_ALMACEN);
        try
        {
            await conn.ExecuteAsync(SP.SCHEMALogistica.AlmacenesCierre.Delete, p, commandType: CommandType.StoredProcedure);
            return new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMALogistica.AlmacenesCierre.Delete, p);
        }
    }

    // Los SP lanzan con THROW (50592–50598): el texto del error ES el mensaje para el usuario.
    private static WriteResult Error(SqlException ex, string sp, DynamicParameters p) => new()
    {
        Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
        ErrorProcedure = sp, SqlSentence = SqlSentenceHelper.Build(sp, p)
    };
}
