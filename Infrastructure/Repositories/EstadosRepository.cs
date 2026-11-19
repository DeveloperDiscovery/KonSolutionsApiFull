using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EstadosRepository : IEstadosRepository
{
    private readonly IDbConnectionFactory _db;
    public EstadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Estados>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Estados>(SP.SCHEMAEstandar.Estados.Showall, new { COD_TIPO_ESTADO = (int?)null }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Estados>> ShowByTipoAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Estados>(SP.SCHEMAEstandar.Estados.Showall, new { COD_TIPO_ESTADO = codTipoEstado }, commandType: CommandType.StoredProcedure);
    }

    public async Task<Estados?> ShowbyidAsync(int COD_TIPO_ESTADO, int COD_ESTADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Estados>(SP.SCHEMAEstandar.Estados.Showbyid, new { COD_TIPO_ESTADO = COD_TIPO_ESTADO, COD_ESTADO = COD_ESTADO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Estados.Showcombox);
    }

    public async Task<IEnumerable<Estados>> SearchAsync(EstadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Estados>(SP.SCHEMAEstandar.Estados.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EstadosInsertDto dto, string codUsuario, string codEstacion)
    {
        dto.TrimStrings(); // RTRIM de todos los campos string antes de grabar
        using var conn = _db.CreateConnection();

        // El SP calcula el siguiente COD_ESTADO internamente cuando recibe NULL (insert nuevo).
        // En edición se pasa el código existente tal cual.
        int? codEstado = dto.EsNuevo ? null : dto.COD_ESTADO;
        if (!dto.EsNuevo && !dto.COD_ESTADO.HasValue)
            return new WriteResult { Estado = "ERROR", AffectedRows = 0,
                Mensaje = "No se recibió COD_ESTADO para la edición. Operación cancelada para evitar duplicar el registro." };

        var p = new DynamicParameters();
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", codEstado);
        p.Add("DES_ESTADO", dto.DES_ESTADO);
        p.Add("COD_ESTADO_ALIAS", dto.COD_ESTADO_ALIAS);
        p.Add("FLG_DEFAULT", dto.FLG_DEFAULT ?? false);
        p.Add("FLG_DISPARADOR_MAIL", dto.FLG_DISPARADOR_MAIL ?? false);
        p.Add("FLG_DISPARADOR_NOTIFICACION", dto.FLG_DISPARADOR_NOTIFICACION ?? false);
        p.Add("FLG_DISPARADOR_AUTORIZACION", dto.FLG_DISPARADOR_AUTORIZACION ?? false);
        p.Add("FLG_DISPARADOR_AUTORIZACION_COMERCIAL", dto.FLG_DISPARADOR_AUTORIZACION_COMERCIAL ?? false);
        p.Add("FLG_PROCESO_REVERSA", dto.FLG_PROCESO_REVERSA ?? false);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("DES_BACKCOLOR", dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR", dto.DES_FORECOLOR);
        p.Add("IMG_PICTURE", dto.IMG_PICTURE, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Estados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1, Mensaje = $"Estado {codEstado} guardado." };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Estados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Estados.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_TIPO_ESTADO, int COD_ESTADO)
    {
        using var conn = _db.CreateConnection();
        // El SP de borrado retorna el resultado directamente.
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Estados.Delete,
            new { COD_TIPO_ESTADO, COD_ESTADO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1, Mensaje = "Estado eliminado." };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Estados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Estados.Delete, new { COD_TIPO_ESTADO, COD_ESTADO })
        };
    }
}


    public async Task<WriteResult> ImportFromEstandarAsync(int codTipoEstado, int codEstadoEstandar, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_ESTADO", codTipoEstado);
        p.Add("COD_ESTADO_ESTANDAR", codEstadoEstandar);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Estados.FromEstadosEstandar, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Estados.FromEstadosEstandar,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Estados.FromEstadosEstandar, p)
        };
    }
}
}
