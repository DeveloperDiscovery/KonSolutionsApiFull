using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OpcionesProcesosEstadosFlujosDependenciaRepository : IOpcionesProcesosEstadosFlujosDependenciaRepository
{
    // NOTA: no está en Common/StoredProcedureNames.cs (SP.SCHEMAEstandar.*) como el resto de
    // los procedimientos por el tamaño del archivo; agregarlo ahí requeriría reescribirlo
    // entero para el push. Mover estas constantes a SP.SCHEMAEstandar.OpcionesProcesosEstadosFlujosDependencia.*
    // cuando se edite ese archivo desde un entorno con git push directo.
    private const string ProcShowall = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA_SHOWALL";
    private const string ProcInsert = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA_INSERT";
    private const string ProcDelete = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA_DELETE";
    private const string ProcValida = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_FLUJOS_DEPENDENCIA_VALIDA";

    private readonly IDbConnectionFactory _db;
    public OpcionesProcesosEstadosFlujosDependenciaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OpcionesProcesosEstadosFlujosDependencia>> ShowallAsync(
        string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OpcionesProcesosEstadosFlujosDependencia>(ProcShowall,
            new
            {
                COD_APLICACION = codAplicacion,
                COD_OPCION_APLICACION = codOpcionAplicacion,
                COD_TIPO_ESTADO = codTipoEstado,
                COD_BOTON_OPCION = codBotonOpcion
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(OpcionesProcesosEstadosFlujosDependenciaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_BOTON_OPCION", dto.COD_BOTON_OPCION);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
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

    public async Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado, string codBotonOpcion, short codEstado)
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
                COD_BOTON_OPCION = codBotonOpcion,
                COD_ESTADO = codEstado
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
                COD_BOTON_OPCION = codBotonOpcion,
                COD_ESTADO = codEstado
            })
        };
    }
}

    public async Task<OpcionesProcesosEstadosFlujosDependenciaValidaResult> ValidarAsync(
        string codAplicacion, string codOpcionAplicacion, string codBotonOpcion, short codTipoEstado, short codEstadoActual)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<OpcionesProcesosEstadosFlujosDependenciaValidaResult>(ProcValida,
            new
            {
                COD_APLICACION = codAplicacion,
                COD_OPCION_APLICACION = codOpcionAplicacion,
                COD_BOTON_OPCION = codBotonOpcion,
                COD_TIPO_ESTADO = codTipoEstado,
                COD_ESTADO_ACTUAL = codEstadoActual
            },
            commandType: CommandType.StoredProcedure);
        return r ?? new OpcionesProcesosEstadosFlujosDependenciaValidaResult { FLG_VALIDO = true };
    }
}
