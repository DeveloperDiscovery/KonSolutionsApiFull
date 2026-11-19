using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Planeamiento;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProgramacionVersionDetalleRepository : IProgramacionVersionDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public ProgramacionVersionDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProgramacionVersionDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionVersionDetalle>(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProgramacionVersionDetalle?> ShowbyidAsync(int NUM_PROGRAMA, int NUM_VERSION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProgramacionVersionDetalle>(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Showbyid, new { NUM_PROGRAMA = NUM_PROGRAMA, NUM_VERSION = NUM_VERSION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Showcombox);
    }

    public async Task<IEnumerable<ProgramacionVersionDetalle>> SearchAsync(ProgramacionVersionDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionVersionDetalle>(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProgramacionVersionDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROGRAMA", dto.NUM_PROGRAMA);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_PROGRAMACION", dto.COD_TIPO_PROGRAMACION);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_ACTIVO_MAQUINA", dto.COD_ACTIVO_MAQUINA);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PROGRAMADA", dto.CAN_PROGRAMADA);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("FEC_PROGRAMADA_INICIO", dto.FEC_PROGRAMADA_INICIO);
        p.Add("FEC_PROGRAMADA_FINAL", dto.FEC_PROGRAMADA_FINAL);
        p.Add("FEC_REAL_INICIO", dto.FEC_REAL_INICIO);
        p.Add("FEC_REAL_FINAL", dto.FEC_REAL_FINAL);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProgramacionVersionDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PROGRAMA", dto.NUM_PROGRAMA);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_TIPO_PROGRAMACION", dto.COD_TIPO_PROGRAMACION);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_SUB_PROCESO", dto.COD_SUB_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_ACTIVO_MAQUINA", dto.COD_ACTIVO_MAQUINA);
        p.Add("NUM_ORDEN_PRODUCCION", dto.NUM_ORDEN_PRODUCCION);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM", dto.NUM_ORDEN_PRODUCCION_ITEM);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD", dto.NUM_ORDEN_PRODUCCION_UNIDAD);
        p.Add("CAN_PROGRAMADA", dto.CAN_PROGRAMADA);
        p.Add("CAN_PRODUCIDA", dto.CAN_PRODUCIDA);
        p.Add("FEC_PROGRAMADA_INICIO", dto.FEC_PROGRAMADA_INICIO);
        p.Add("FEC_PROGRAMADA_FINAL", dto.FEC_PROGRAMADA_FINAL);
        p.Add("FEC_REAL_INICIO", dto.FEC_REAL_INICIO);
        p.Add("FEC_REAL_FINAL", dto.FEC_REAL_FINAL);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PROGRAMA, int NUM_VERSION, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Delete, new { NUM_PROGRAMA = NUM_PROGRAMA, NUM_VERSION = NUM_VERSION, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAPlaneamiento.ProgramacionVersionDetalle.Delete, new { NUM_PROGRAMA = NUM_PROGRAMA, NUM_VERSION = NUM_VERSION, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}