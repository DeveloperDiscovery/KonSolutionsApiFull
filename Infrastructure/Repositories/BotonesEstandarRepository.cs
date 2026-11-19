using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class BotonesEstandarRepository : IBotonesEstandarRepository
{
    private readonly IDbConnectionFactory _db;
    public BotonesEstandarRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<BotonesEstandar>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BotonesEstandar>(SP.SCHEMASeguridad.BotonesEstandar.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<BotonesEstandar?> ShowbyidAsync(string COD_BOTON_OPCION_ESTANDAR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<BotonesEstandar>(SP.SCHEMASeguridad.BotonesEstandar.Showbyid, new { COD_BOTON_OPCION_ESTANDAR = COD_BOTON_OPCION_ESTANDAR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.BotonesEstandar.Showcombox);
    }

    public async Task<IEnumerable<BotonesEstandar>> SearchAsync(BotonesEstandarSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<BotonesEstandar>(SP.SCHEMASeguridad.BotonesEstandar.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(BotonesEstandarInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BOTON_OPCION_ESTANDAR", dto.COD_BOTON_OPCION_ESTANDAR);
        p.Add("DES_BOTON_OPCION_ESTANDAR", dto.DES_BOTON_OPCION_ESTANDAR);
        p.Add("FLG_GRILLA", dto.FLG_GRILLA ?? false);
        p.Add("FLG_HEADER", dto.FLG_HEADER ?? false);
        p.Add("FLG_ACCION_POST", dto.FLG_ACCION_POST ?? false);
        p.Add("FLG_ACCION_PUT", dto.FLG_ACCION_PUT ?? false);
        p.Add("FLG_ACCION_DELETE", dto.FLG_ACCION_DELETE ?? false);
        p.Add("FLG_ACCION_GET", dto.FLG_ACCION_GET ?? false);
        p.Add("DES_METODO", dto.DES_METODO, System.Data.DbType.String);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.BotonesEstandar.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.BotonesEstandar.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.BotonesEstandar.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(BotonesEstandarUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_BOTON_OPCION_ESTANDAR", dto.COD_BOTON_OPCION_ESTANDAR);
        p.Add("DES_BOTON_OPCION_ESTANDAR", dto.DES_BOTON_OPCION_ESTANDAR);
        p.Add("FLG_GRILLA", dto.FLG_GRILLA ?? false);
        p.Add("FLG_HEADER", dto.FLG_HEADER ?? false);
        p.Add("FLG_ACCION_POST", dto.FLG_ACCION_POST ?? false);
        p.Add("FLG_ACCION_PUT", dto.FLG_ACCION_PUT ?? false);
        p.Add("FLG_ACCION_DELETE", dto.FLG_ACCION_DELETE ?? false);
        p.Add("FLG_ACCION_GET", dto.FLG_ACCION_GET ?? false);
        p.Add("DES_METODO", dto.DES_METODO, System.Data.DbType.String);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.BotonesEstandar.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.BotonesEstandar.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.BotonesEstandar.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_BOTON_OPCION_ESTANDAR)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.BotonesEstandar.Delete, new { COD_BOTON_OPCION_ESTANDAR = COD_BOTON_OPCION_ESTANDAR }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.BotonesEstandar.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.BotonesEstandar.Delete, new { COD_BOTON_OPCION_ESTANDAR = COD_BOTON_OPCION_ESTANDAR })
        };
    }
}

}