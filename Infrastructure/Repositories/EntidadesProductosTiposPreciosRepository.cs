using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesProductosTiposPreciosRepository : IEntidadesProductosTiposPreciosRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesProductosTiposPreciosRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Los tipos de precio de un par (entidad, producto). El tipo de precio es
    /// opcional: sin él devuelve todos, que es lo que necesita la subgrilla.</summary>
    public async Task<IEnumerable<EntidadesProductosTiposPrecios>> ShowallAsync(decimal codEntidad, string codProducto, int? codTipoPrecio = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesProductosTiposPrecios>(
            SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Showall,
            new { COD_ENTIDAD = codEntidad, COD_PRODUCTO = codProducto, COD_TIPO_PRECIO = codTipoPrecio },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesProductosTiposPrecios?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesProductosTiposPrecios>(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, COD_PRODUCTO = COD_PRODUCTO, COD_TIPO_PRECIO = COD_TIPO_PRECIO, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Showcombox);
    }

    public async Task<IEnumerable<EntidadesProductosTiposPrecios>> SearchAsync(EntidadesProductosTiposPreciosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesProductosTiposPrecios>(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesProductosTiposPreciosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_PRECIO", dto.VAL_PRECIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesProductosTiposPreciosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_TIPO_PRECIO", dto.COD_TIPO_PRECIO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_PRECIO", dto.VAL_PRECIO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Update, p)
        };
    }
}

    // La PK son CUATRO columnas: sin la moneda el DELETE no encuentra la fila, porque un
    // mismo tipo de precio puede tener un precio por moneda.
    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_PRODUCTO = COD_PRODUCTO, COD_TIPO_PRECIO = COD_TIPO_PRECIO, COD_MONEDA = COD_MONEDA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesProductosTiposPrecios.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_PRODUCTO = COD_PRODUCTO, COD_TIPO_PRECIO = COD_TIPO_PRECIO, COD_MONEDA = COD_MONEDA })
        };
    }
}

}