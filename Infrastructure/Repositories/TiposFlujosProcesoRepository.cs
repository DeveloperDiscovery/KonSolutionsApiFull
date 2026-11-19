using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposFlujosProcesoRepository : ITiposFlujosProcesoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposFlujosProcesoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposFlujosProceso>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposFlujosProceso>(SP.SCHEMAEntorno.TiposFlujosProceso.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposFlujosProceso?> ShowbyidAsync(string COD_TIPO_FLUJO_PROCESO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposFlujosProceso>(SP.SCHEMAEntorno.TiposFlujosProceso.Showbyid, new { COD_TIPO_FLUJO_PROCESO = COD_TIPO_FLUJO_PROCESO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.TiposFlujosProceso.Showcombox);
    }

    public async Task<IEnumerable<TiposFlujosProceso>> SearchAsync(TiposFlujosProcesoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposFlujosProceso>(SP.SCHEMAEntorno.TiposFlujosProceso.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposFlujosProcesoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_FLUJO_PROCESO", dto.COD_TIPO_FLUJO_PROCESO);
        p.Add("DES_TIPO_FLUJO_PROCESO", dto.DES_TIPO_FLUJO_PROCESO);
        p.Add("FLG_AVISA_SOLO_RESPONSABLE", dto.FLG_AVISA_SOLO_RESPONSABLE ?? false);
        p.Add("NUM_MINUTOS_AVISO_PREVIO_INICIO", dto.NUM_MINUTOS_AVISO_PREVIO_INICIO);
        p.Add("NUM_MINUTOS_AVISO_PREVIO_FINAL", dto.NUM_MINUTOS_AVISO_PREVIO_FINAL);
        p.Add("FLG_ALARMA_PREVIA", dto.FLG_ALARMA_PREVIA ?? false);
        p.Add("NUN_REPETICIONES_PREVIA", dto.NUN_REPETICIONES_PREVIA);
        p.Add("NUM_SEGUNDOS_ALARMA_PREVIO", dto.NUM_SEGUNDOS_ALARMA_PREVIO);
        p.Add("NUM_MINUTOS_AVISO_POSTERIOR_INICIO", dto.NUM_MINUTOS_AVISO_POSTERIOR_INICIO);
        p.Add("NUM_MINUTOS_AVISO_POSTERIOR_FINAL", dto.NUM_MINUTOS_AVISO_POSTERIOR_FINAL);
        p.Add("FLG_ALARMA_POSTERIOR", dto.FLG_ALARMA_POSTERIOR ?? false);
        p.Add("NUM_SEGUNDOS_ALARMA_POSTERIOR", dto.NUM_SEGUNDOS_ALARMA_POSTERIOR);
        p.Add("NUN_REPETICIONES_POSTERIOR", dto.NUN_REPETICIONES_POSTERIOR);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposFlujosProceso.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposFlujosProceso.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposFlujosProceso.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposFlujosProcesoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_FLUJO_PROCESO", dto.COD_TIPO_FLUJO_PROCESO);
        p.Add("DES_TIPO_FLUJO_PROCESO", dto.DES_TIPO_FLUJO_PROCESO);
        p.Add("FLG_AVISA_SOLO_RESPONSABLE", dto.FLG_AVISA_SOLO_RESPONSABLE ?? false);
        p.Add("NUM_MINUTOS_AVISO_PREVIO_INICIO", dto.NUM_MINUTOS_AVISO_PREVIO_INICIO);
        p.Add("NUM_MINUTOS_AVISO_PREVIO_FINAL", dto.NUM_MINUTOS_AVISO_PREVIO_FINAL);
        p.Add("FLG_ALARMA_PREVIA", dto.FLG_ALARMA_PREVIA ?? false);
        p.Add("NUN_REPETICIONES_PREVIA", dto.NUN_REPETICIONES_PREVIA);
        p.Add("NUM_SEGUNDOS_ALARMA_PREVIO", dto.NUM_SEGUNDOS_ALARMA_PREVIO);
        p.Add("NUM_MINUTOS_AVISO_POSTERIOR_INICIO", dto.NUM_MINUTOS_AVISO_POSTERIOR_INICIO);
        p.Add("NUM_MINUTOS_AVISO_POSTERIOR_FINAL", dto.NUM_MINUTOS_AVISO_POSTERIOR_FINAL);
        p.Add("FLG_ALARMA_POSTERIOR", dto.FLG_ALARMA_POSTERIOR ?? false);
        p.Add("NUM_SEGUNDOS_ALARMA_POSTERIOR", dto.NUM_SEGUNDOS_ALARMA_POSTERIOR);
        p.Add("NUN_REPETICIONES_POSTERIOR", dto.NUN_REPETICIONES_POSTERIOR);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposFlujosProceso.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposFlujosProceso.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposFlujosProceso.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_FLUJO_PROCESO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.TiposFlujosProceso.Delete, new { COD_TIPO_FLUJO_PROCESO = COD_TIPO_FLUJO_PROCESO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.TiposFlujosProceso.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.TiposFlujosProceso.Delete, new { COD_TIPO_FLUJO_PROCESO = COD_TIPO_FLUJO_PROCESO })
        };
    }
}

}