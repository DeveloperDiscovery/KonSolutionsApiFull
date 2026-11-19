using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposLecturaRepository : ITiposLecturaRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposLecturaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposLectura>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposLectura>(SP.SCHEMAEntorno.TiposLectura.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposLectura?> ShowbyidAsync(byte COD_TIPO_LECTURA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposLectura>(SP.SCHEMAEntorno.TiposLectura.Showbyid, new { COD_TIPO_LECTURA }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SHOWCOMBOX devuelve (COD_TIPO_LECTURA, DES_TIPO_LECTURA, FLG_*): ComboboxReader lo lee por convención de nombre.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.TiposLectura.Showcombox);
    }

    /// <summary>Alta. El código lo asigna la base (identity): el SP no lo recibe como dato, aunque lo declare.</summary>
    public async Task<WriteResult> InsertAsync(TiposLecturaInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto.COD_TIPO_LECTURA, dto.DES_TIPO_LECTURA, dto.FLG_MANUAL, dto.FLG_CODEBAR, dto.FLG_RFID);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposLectura.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMAEntorno.TiposLectura.Insert, p);
        }
    }

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposLecturaUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto.COD_TIPO_LECTURA, dto.DES_TIPO_LECTURA, dto.FLG_MANUAL, dto.FLG_CODEBAR, dto.FLG_RFID);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposLectura.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMAEntorno.TiposLectura.Update, p);
        }
    }

    public async Task<WriteResult> DeleteAsync(byte COD_TIPO_LECTURA)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_TIPO_LECTURA };
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposLectura.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return Error(ex, SP.SCHEMAEntorno.TiposLectura.Delete, p);
        }
    }

    private static DynamicParameters Parametros(byte? cod, string des, bool? manual, bool? codebar, bool? rfid)
    {
        var p = new DynamicParameters();
        p.Add("COD_TIPO_LECTURA", cod, DbType.Byte);
        p.Add("DES_TIPO_LECTURA", des?.Trim());
        // Una bandera sin marcar es "no", no "sin dato": la columna es bit y el SP no la recibe nula.
        p.Add("FLG_MANUAL", manual ?? false);
        p.Add("FLG_CODEBAR", codebar ?? false);
        p.Add("FLG_RFID", rfid ?? false);
        return p;
    }

    private static WriteResult Error(SqlException ex, string procedimiento, object parametros) => new()
    {
        Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
        ErrorProcedure = procedimiento,
        SqlSentence = SqlSentenceHelper.Build(procedimiento, parametros)
    };
}
