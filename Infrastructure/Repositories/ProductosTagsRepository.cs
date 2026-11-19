using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProductosTagsRepository : IProductosTagsRepository
{
    private readonly IDbConnectionFactory _db;
    public ProductosTagsRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProductosTags>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosTags>(SP.SCHEMASafety.ProductosTags.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductosTags?> ShowbyidAsync(string COD_PRODUCTO_TAG)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProductosTags>(SP.SCHEMASafety.ProductosTags.Showbyid, new { COD_PRODUCTO_TAG = COD_PRODUCTO_TAG }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.ProductosTags.Showcombox);
    }

    public async Task<IEnumerable<ProductosTags>> SearchAsync(ProductosTagsSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosTags>(SP.SCHEMASafety.ProductosTags.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProductosTagsInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO_TAG", dto.COD_PRODUCTO_TAG);
        p.Add("COD_TIPO_TAG", dto.COD_TIPO_TAG);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ProductosTags.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ProductosTags.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ProductosTags.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProductosTagsUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO_TAG", dto.COD_PRODUCTO_TAG);
        p.Add("COD_TIPO_TAG", dto.COD_TIPO_TAG);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ProductosTags.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ProductosTags.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ProductosTags.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PRODUCTO_TAG)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ProductosTags.Delete, new { COD_PRODUCTO_TAG = COD_PRODUCTO_TAG }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ProductosTags.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ProductosTags.Delete, new { COD_PRODUCTO_TAG = COD_PRODUCTO_TAG })
        };
    }
}

}