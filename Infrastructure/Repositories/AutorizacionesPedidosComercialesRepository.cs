using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AutorizacionesPedidosComercialesRepository : IAutorizacionesPedidosComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public AutorizacionesPedidosComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AutorizacionesPedidosComerciales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AutorizacionesPedidosComerciales>(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AutorizacionesPedidosComerciales?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AutorizacionesPedidosComerciales>(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Showbyid, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA = COD_ENTIDAD_AUTORIZADA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Showcombox);
    }

    public async Task<IEnumerable<AutorizacionesPedidosComerciales>> SearchAsync(AutorizacionesPedidosComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AutorizacionesPedidosComerciales>(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AutorizacionesPedidosComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_ENTIDAD_AUTORIZADA", dto.COD_ENTIDAD_AUTORIZADA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AutorizacionesPedidosComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_ENTIDAD_AUTORIZADA", dto.COD_ENTIDAD_AUTORIZADA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, decimal COD_ENTIDAD_AUTORIZADA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA = COD_ENTIDAD_AUTORIZADA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.AutorizacionesPedidosComerciales.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_ENTIDAD_AUTORIZADA = COD_ENTIDAD_AUTORIZADA })
        };
    }
}

}