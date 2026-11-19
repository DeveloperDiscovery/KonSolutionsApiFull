using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FlujosProcesoEtapasRepository : IFlujosProcesoEtapasRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosProcesoEtapasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosProcesoEtapas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoEtapas>(SP.SCHEMAEntorno.FlujosProcesoEtapas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosProcesoEtapas?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosProcesoEtapas>(SP.SCHEMAEntorno.FlujosProcesoEtapas.Showbyid, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.FlujosProcesoEtapas.Showcombox, "NUM_FLUJO_PROCESO", "DES_ETAPA");
    }

    public async Task<IEnumerable<FlujosProcesoEtapas>> SearchAsync(FlujosProcesoEtapasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoEtapas>(SP.SCHEMAEntorno.FlujosProcesoEtapas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosProcesoEtapasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_ETAPA", dto.NUM_ETAPA);
        p.Add("DES_ETAPA", dto.DES_ETAPA);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("NUM_ETAPA_PADRE", dto.NUM_ETAPA_PADRE);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FlujosProcesoEtapasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_ETAPA", dto.NUM_ETAPA);
        p.Add("DES_ETAPA", dto.DES_ETAPA);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_FINAL", dto.FEC_FINAL);
        p.Add("NUM_ETAPA_PADRE", dto.NUM_ETAPA_PADRE);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapas.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapas.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA })
        };
    }
}

}