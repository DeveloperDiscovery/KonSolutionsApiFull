using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Produccion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProcesosLineasActivosRepository : IProcesosLineasActivosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProcesosLineasActivosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProcesosLineasActivos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosLineasActivos>(SP.SCHEMAProduccion.ProcesosLineasActivos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProcesosLineasActivos?> ShowbyidAsync(string COD_PROCESO, string COD_ACTIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProcesosLineasActivos>(SP.SCHEMAProduccion.ProcesosLineasActivos.Showbyid, new { COD_PROCESO = COD_PROCESO, COD_ACTIVO = COD_ACTIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAProduccion.ProcesosLineasActivos.Showcombox);
    }

    public async Task<IEnumerable<ProcesosLineasActivos>> SearchAsync(ProcesosLineasActivosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosLineasActivos>(SP.SCHEMAProduccion.ProcesosLineasActivos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProcesosLineasActivosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("NUM_ORDEN_LINEA", dto.NUM_ORDEN_LINEA);
        p.Add("COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION", dto.COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION", dto.COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION);
        p.Add("CAN_CAPACIDAD_PRODUCCION", dto.CAN_CAPACIDAD_PRODUCCION);
        p.Add("TAS_EFICIENCIA_ESTIMADA", dto.TAS_EFICIENCIA_ESTIMADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ProcesosLineasActivos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ProcesosLineasActivos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ProcesosLineasActivos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProcesosLineasActivosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("NUM_ORDEN_LINEA", dto.NUM_ORDEN_LINEA);
        p.Add("COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION", dto.COD_UNIDAD_MEDIDA_CAPACIDAD_PRODUCCION);
        p.Add("COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION", dto.COD_UNIDAD_MEDIDA_TIEMPO_ESTANDAR_PRODUCCION);
        p.Add("CAN_CAPACIDAD_PRODUCCION", dto.CAN_CAPACIDAD_PRODUCCION);
        p.Add("TAS_EFICIENCIA_ESTIMADA", dto.TAS_EFICIENCIA_ESTIMADA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ProcesosLineasActivos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ProcesosLineasActivos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ProcesosLineasActivos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_ACTIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAProduccion.ProcesosLineasActivos.Delete, new { COD_PROCESO = COD_PROCESO, COD_ACTIVO = COD_ACTIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAProduccion.ProcesosLineasActivos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAProduccion.ProcesosLineasActivos.Delete, new { COD_PROCESO = COD_PROCESO, COD_ACTIVO = COD_ACTIVO })
        };
    }
}

}