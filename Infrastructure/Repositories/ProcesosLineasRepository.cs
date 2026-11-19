using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProcesosLineasRepository : IProcesosLineasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProcesosLineasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProcesosLineas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosLineas>(SP.SCHEMAProduccion.ProcesosLineas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProcesosLineas?> ShowbyidAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProcesosLineas>(SP.SCHEMAProduccion.ProcesosLineas.Showbyid, new { COD_PROCESO = COD_PROCESO, COD_LINEA_PRODUCCION = COD_LINEA_PRODUCCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.ProcesosLineas.Showcombox);
    }

    public async Task<IEnumerable<ProcesosLineas>> SearchAsync(ProcesosLineasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosLineas>(SP.SCHEMAProduccion.ProcesosLineas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProcesosLineasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("DES_LINEA_PRODUCCION", dto.DES_LINEA_PRODUCCION);
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("FLG_MANUAL", dto.FLG_MANUAL ?? false);
        p.Add("FLG_MAQUINA", dto.FLG_MAQUINA ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ProcesosLineas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ProcesosLineas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ProcesosLineas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProcesosLineasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("DES_LINEA_PRODUCCION", dto.DES_LINEA_PRODUCCION);
        p.Add("COD_TIPO_LINEA_PRODUCCION", dto.COD_TIPO_LINEA_PRODUCCION);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("FLG_MANUAL", dto.FLG_MANUAL ?? false);
        p.Add("FLG_MAQUINA", dto.FLG_MAQUINA ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ProcesosLineas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ProcesosLineas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ProcesosLineas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ProcesosLineas.Delete, new { COD_PROCESO = COD_PROCESO, COD_LINEA_PRODUCCION = COD_LINEA_PRODUCCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ProcesosLineas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ProcesosLineas.Delete, new { COD_PROCESO = COD_PROCESO, COD_LINEA_PRODUCCION = COD_LINEA_PRODUCCION })
        };
    }
}

}