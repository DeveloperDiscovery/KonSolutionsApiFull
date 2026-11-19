using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ActivosZonificacionRepository : IActivosZonificacionRepository
{
    private readonly IDbConnectionFactory _db;
    public ActivosZonificacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ActivosZonificacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosZonificacion>(SP.SCHEMAZonificacion.ActivosZonificacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ActivosZonificacion?> ShowbyidAsync(string COD_ACTIVO, string COD_SUCURSAL, string COD_ZONIFICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ActivosZonificacion>(SP.SCHEMAZonificacion.ActivosZonificacion.Showbyid, new { COD_ACTIVO = COD_ACTIVO, COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAZonificacion.ActivosZonificacion.Showcombox);
    }

    public async Task<IEnumerable<ActivosZonificacion>> SearchAsync(ActivosZonificacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosZonificacion>(SP.SCHEMAZonificacion.ActivosZonificacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ActivosZonificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("COD_CENTRO_COSTO_RESPONDABLE", dto.COD_CENTRO_COSTO_RESPONDABLE);
        p.Add("FLG_READER_RFID", dto.FLG_READER_RFID ?? false);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAZonificacion.ActivosZonificacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.ActivosZonificacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.ActivosZonificacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ActivosZonificacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("COD_CENTRO_COSTO_RESPONDABLE", dto.COD_CENTRO_COSTO_RESPONDABLE);
        p.Add("FLG_READER_RFID", dto.FLG_READER_RFID ?? false);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAZonificacion.ActivosZonificacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.ActivosZonificacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.ActivosZonificacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ACTIVO, string COD_SUCURSAL, string COD_ZONIFICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAZonificacion.ActivosZonificacion.Delete, new { COD_ACTIVO = COD_ACTIVO, COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.ActivosZonificacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.ActivosZonificacion.Delete, new { COD_ACTIVO = COD_ACTIVO, COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION })
        };
    }
}

}