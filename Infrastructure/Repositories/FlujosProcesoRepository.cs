using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FlujosProcesoRepository : IFlujosProcesoRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosProcesoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosProceso>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProceso>(SP.SCHEMAEntorno.FlujosProceso.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosProceso?> ShowbyidAsync(int NUM_FLUJO_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosProceso>(SP.SCHEMAEntorno.FlujosProceso.Showbyid, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.FlujosProceso.Showcombox, "NUM_FLUJO_PROCESO", "DES_FLUJO_PROCESO");
    }

    public async Task<IEnumerable<FlujosProceso>> SearchAsync(FlujosProcesoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProceso>(SP.SCHEMAEntorno.FlujosProceso.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosProcesoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("DES_FLUJO_PROCESO", dto.DES_FLUJO_PROCESO);
        p.Add("COD_ENTIDAD_DEFINIDORA", dto.COD_ENTIDAD_DEFINIDORA);
        p.Add("COD_TIPO_FLUJO_PROCESO", dto.COD_TIPO_FLUJO_PROCESO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProceso.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProceso.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProceso.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FlujosProcesoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("DES_FLUJO_PROCESO", dto.DES_FLUJO_PROCESO);
        p.Add("COD_ENTIDAD_DEFINIDORA", dto.COD_ENTIDAD_DEFINIDORA);
        p.Add("COD_TIPO_FLUJO_PROCESO", dto.COD_TIPO_FLUJO_PROCESO);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProceso.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProceso.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProceso.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProceso.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProceso.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProceso.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO })
        };
    }
}

}