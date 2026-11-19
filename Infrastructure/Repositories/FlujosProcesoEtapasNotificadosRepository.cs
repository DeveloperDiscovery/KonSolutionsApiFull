using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class FlujosProcesoEtapasNotificadosRepository : IFlujosProcesoEtapasNotificadosRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosProcesoEtapasNotificadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosProcesoEtapasNotificados>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoEtapasNotificados>(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosProcesoEtapasNotificados?> ShowbyidAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosProcesoEtapasNotificados>(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Showbyid, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA, COD_ENTIDAD_NOTIFICAR = COD_ENTIDAD_NOTIFICAR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Showcombox, "NUM_FLUJO_PROCESO", "DES_COMERCIAL");
    }

    public async Task<IEnumerable<FlujosProcesoEtapasNotificados>> SearchAsync(FlujosProcesoEtapasNotificadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosProcesoEtapasNotificados>(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosProcesoEtapasNotificadosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_ETAPA", dto.NUM_ETAPA);
        p.Add("COD_ENTIDAD_NOTIFICAR", dto.COD_ENTIDAD_NOTIFICAR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(FlujosProcesoEtapasNotificadosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_PROCESO", dto.NUM_FLUJO_PROCESO);
        p.Add("NUM_ETAPA", dto.NUM_ETAPA);
        p.Add("COD_ENTIDAD_NOTIFICAR", dto.COD_ENTIDAD_NOTIFICAR);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_FLUJO_PROCESO, int NUM_ETAPA, decimal COD_ENTIDAD_NOTIFICAR)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA, COD_ENTIDAD_NOTIFICAR = COD_ENTIDAD_NOTIFICAR }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.FlujosProcesoEtapasNotificados.Delete, new { NUM_FLUJO_PROCESO = NUM_FLUJO_PROCESO, NUM_ETAPA = NUM_ETAPA, COD_ENTIDAD_NOTIFICAR = COD_ENTIDAD_NOTIFICAR })
        };
    }
}

}