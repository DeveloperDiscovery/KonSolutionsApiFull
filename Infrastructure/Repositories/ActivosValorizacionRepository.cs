using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ActivosValorizacionRepository : IActivosValorizacionRepository
{
    private readonly IDbConnectionFactory _db;
    public ActivosValorizacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ActivosValorizacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosValorizacion>(SP.SCHEMAActivos.ActivosValorizacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ActivosValorizacion?> ShowbyidAsync(string COD_ACTIVO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ActivosValorizacion>(SP.SCHEMAActivos.ActivosValorizacion.Showbyid, new { COD_ACTIVO = COD_ACTIVO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAActivos.ActivosValorizacion.Showcombox);
    }

    public async Task<IEnumerable<ActivosValorizacion>> SearchAsync(ActivosValorizacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosValorizacion>(SP.SCHEMAActivos.ActivosValorizacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ActivosValorizacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ENTIDAD_PROVEEDOR", dto.COD_ENTIDAD_PROVEEDOR);
        p.Add("FEC_ADQUISICION", dto.FEC_ADQUISICION);
        p.Add("FEC_VALORIZACION", dto.FEC_VALORIZACION);
        p.Add("VAL_VALORIZACION", dto.VAL_VALORIZACION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_DEPRECIACION_ACUMULADA", dto.VAL_DEPRECIACION_ACUMULADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ActivosValorizacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ActivosValorizacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ActivosValorizacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ActivosValorizacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ENTIDAD_PROVEEDOR", dto.COD_ENTIDAD_PROVEEDOR);
        p.Add("FEC_ADQUISICION", dto.FEC_ADQUISICION);
        p.Add("FEC_VALORIZACION", dto.FEC_VALORIZACION);
        p.Add("VAL_VALORIZACION", dto.VAL_VALORIZACION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("VAL_DEPRECIACION_ACUMULADA", dto.VAL_DEPRECIACION_ACUMULADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ActivosValorizacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ActivosValorizacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ActivosValorizacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ACTIVO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ActivosValorizacion.Delete, new { COD_ACTIVO = COD_ACTIVO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ActivosValorizacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ActivosValorizacion.Delete, new { COD_ACTIVO = COD_ACTIVO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}