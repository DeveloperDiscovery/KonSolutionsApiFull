using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosProductoRepository : IDisenosProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosProducto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosProducto>(SP.SCHEMAIngenieria.DisenosProducto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosProducto?> ShowbyidAsync(string COD_DISENO, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosProducto>(SP.SCHEMAIngenieria.DisenosProducto.Showbyid, new { COD_DISENO = COD_DISENO, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.DisenosProducto.Showcombox);
    }

    public async Task<IEnumerable<DisenosProducto>> SearchAsync(DisenosProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosProducto>(SP.SCHEMAIngenieria.DisenosProducto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("DES_PRODUCTO", dto.DES_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("COD_TIPO_DISENO", dto.COD_TIPO_DISENO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosProducto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosProducto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosProducto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DisenosProductoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("DES_PRODUCTO", dto.DES_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("COD_TIPO_DISENO", dto.COD_TIPO_DISENO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosProducto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosProducto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosProducto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosProducto.Delete, new { COD_DISENO = COD_DISENO, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosProducto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosProducto.Delete, new { COD_DISENO = COD_DISENO, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}