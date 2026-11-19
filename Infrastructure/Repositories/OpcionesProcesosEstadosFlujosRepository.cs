using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OpcionesProcesosEstadosFlujosRepository : IOpcionesProcesosEstadosFlujosRepository
{
    // NOTA: no está en Common/StoredProcedureNames.cs (SP.SCHEMAEstandar.*) como el resto de
    // los procedimientos por el tamaño del archivo; agregarlo ahí requeriría reescribirlo
    // entero para el push. Mover estas constantes a SP.SCHEMAEstandar.OpcionesProcesosEstadosFlujos.*
    // cuando se edite ese archivo desde un entorno con git push directo.
    private const string ProcTreelist = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_TREELIST";
    private const string ProcShowall = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_SHOWALL";
    private const string ProcInsert = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_INSERT";
    private const string ProcUpdate = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_UPDATE";
    private const string ProcDelete = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DELETE";

    private readonly IDbConnectionFactory _db;
    public OpcionesProcesosEstadosFlujosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OpcionesProcesosEstadosFlujosTreeList>> TreelistAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OpcionesProcesosEstadosFlujosTreeList>(
            ProcTreelist,
            new
            {
                COD_APLICACION = codAplicacion,
                COD_OPCION_APLICACION = codOpcionAplicacion,
                COD_TIPO_ESTADO = codTipoEstado
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<OpcionesProcesosEstadosFlujos>> ShowallAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OpcionesProcesosEstadosFlujos>(ProcShowall,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion, COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OpcionesProcesosEstadosFlujosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_BOTON_OPCION", dto.COD_BOTON_OPCION);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("NUM_ORDEN_FLUJO", dto.NUM_ORDEN_FLUJO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(ProcInsert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = ProcInsert,
            SqlSentence = SqlSentenceHelper.Build(ProcInsert, p)
        };
    }
}

    public async Task<WriteResult> UpdateAsync(OpcionesProcesosEstadosFlujosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_BOTON_OPCION", dto.COD_BOTON_OPCION);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("NUM_ORDEN_FLUJO", dto.NUM_ORDEN_FLUJO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(ProcUpdate, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = ProcUpdate,
            SqlSentence = SqlSentenceHelper.Build(ProcUpdate, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(ProcDelete,
            new
            {
                COD_APLICACION = codAplicacion,
                COD_OPCION_APLICACION = codOpcionAplicacion,
                COD_TIPO_ESTADO = codTipoEstado,
                COD_BOTON_OPCION = codBotonOpcion
            },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = ProcDelete,
            SqlSentence = SqlSentenceHelper.Build(ProcDelete, new
            {
                COD_APLICACION = codAplicacion,
                COD_OPCION_APLICACION = codOpcionAplicacion,
                COD_TIPO_ESTADO = codTipoEstado,
                COD_BOTON_OPCION = codBotonOpcion
            })
        };
    }
}
}
