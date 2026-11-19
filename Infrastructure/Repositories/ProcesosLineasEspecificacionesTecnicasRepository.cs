using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProcesosLineasEspecificacionesTecnicasRepository : IProcesosLineasEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProcesosLineasEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProcesosLineasEspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosLineasEspecificacionesTecnicas>(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProcesosLineasEspecificacionesTecnicas?> ShowbyidAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProcesosLineasEspecificacionesTecnicas>(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Showbyid, new { COD_PROCESO = COD_PROCESO, COD_LINEA_PRODUCCION = COD_LINEA_PRODUCCION, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<ProcesosLineasEspecificacionesTecnicas>> SearchAsync(ProcesosLineasEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProcesosLineasEspecificacionesTecnicas>(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProcesosLineasEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPACIFICACION_TENICA", dto.VAL_ESPACIFICACION_TENICA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProcesosLineasEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PROCESO", dto.COD_PROCESO);
        p.Add("COD_LINEA_PRODUCCION", dto.COD_LINEA_PRODUCCION);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("VAL_ESPACIFICACION_TENICA", dto.VAL_ESPACIFICACION_TENICA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("COD_CLASE_DEFECTO_TECNICO", dto.COD_CLASE_DEFECTO_TECNICO);
        p.Add("COD_TIPO_DEFECTO_TECNICO", dto.COD_TIPO_DEFECTO_TECNICO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PROCESO, string COD_LINEA_PRODUCCION, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Delete, new { COD_PROCESO = COD_PROCESO, COD_LINEA_PRODUCCION = COD_LINEA_PRODUCCION, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.ProcesosLineasEspecificacionesTecnicas.Delete, new { COD_PROCESO = COD_PROCESO, COD_LINEA_PRODUCCION = COD_LINEA_PRODUCCION, COD_ESPECIFICACION_TECNICA = COD_ESPECIFICACION_TECNICA })
        };
    }
}

}