using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class OpcionesProcesosEstadosRepository : IOpcionesProcesosEstadosRepository
{
    // NOTA: no está en Common/StoredProcedureNames.cs (SP.SCHEMAEstandar.*) como el resto de
    // los procedimientos por el tamaño del archivo; agregarlo ahí requeriría reescribirlo
    // entero para el push. Mover estas constantes a SP.SCHEMAEstandar.OpcionesProcesosEstados.*
    // cuando se edite ese archivo desde un entorno con git push directo.
    private const string ProcShowall = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_SHOWALL";
    private const string ProcShowbyid = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_SHOWBYID";
    private const string ProcShowcombox = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_SHOWCOMBOX";
    private const string ProcSearch = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_SEARCH";
    private const string ProcInsert = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_INSERT";
    private const string ProcDelete = "PROC_ESTANDAR_OPCIONES_PROCESOS_ESTADOS_DELETE";

    private readonly IDbConnectionFactory _db;
    public OpcionesProcesosEstadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<OpcionesProcesosEstados>> ShowallAsync(string codAplicacion, string codOpcionAplicacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OpcionesProcesosEstados>(ProcShowall,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<OpcionesProcesosEstados?> ShowbyidAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<OpcionesProcesosEstados>(ProcShowbyid,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion, COD_TIPO_ESTADO = codTipoEstado }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codAplicacion, string codOpcionAplicacion)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, ProcShowcombox,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion });
    }

    public async Task<IEnumerable<OpcionesProcesosEstados>> SearchAsync(OpcionesProcesosEstadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<OpcionesProcesosEstados>(ProcSearch,
            new { COD_APLICACION = dto.COD_APLICACION, COD_OPCION_APLICACION = dto.COD_OPCION_APLICACION, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>El SP _INSERT es upsert (actualiza si ya existe, inserta si no) — no hay SP _UPDATE
    /// para esta tabla de asignación pura.</summary>
    public async Task<WriteResult> InsertAsync(OpcionesProcesosEstadosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", dto.COD_APLICACION);
        p.Add("COD_OPCION_APLICACION", dto.COD_OPCION_APLICACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
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

    public async Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcionAplicacion, short codTipoEstado)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(ProcDelete,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion, COD_TIPO_ESTADO = codTipoEstado }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = ProcDelete,
            SqlSentence = SqlSentenceHelper.Build(ProcDelete, new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcionAplicacion, COD_TIPO_ESTADO = codTipoEstado })
        };
    }
}
}
