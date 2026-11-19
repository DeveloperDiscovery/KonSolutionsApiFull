using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProductosEspecificacionesTecnicasRepository : IProductosEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public ProductosEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProductosEspecificacionesTecnicas>> ShowallAsync(string codProducto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosEspecificacionesTecnicas>(
            SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Showall,
            new { COD_PRODUCTO = codProducto }, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProductosEspecificacionesTecnicas?> ShowbyidAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProductosEspecificacionesTecnicas>(
            SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Showbyid,
            new { COD_PRODUCTO, COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ProductosEspecificacionesTecnicas>> SearchAsync(ProductosEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProductosEspecificacionesTecnicas>(
            SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Search,
            new { dto.COD_PRODUCTO, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProductosEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FEC_ESPECIFICACION_TENICA", dto.FEC_ESPECIFICACION_TENICA);
        p.Add("HOR_ESPECIFICACION_TENICA", dto.HOR_ESPECIFICACION_TENICA);
        p.Add("LOG_ESPECIFICACION_TENICA", dto.LOG_ESPECIFICACION_TENICA);
        p.Add("STR_ESPECIFICACION_TENICA", dto.STR_ESPECIFICACION_TENICA);
        p.Add("VAL_ESPECIFICACION_TENICA", dto.VAL_ESPECIFICACION_TENICA);
        p.Add("VAL_MINIMO", dto.VAL_MINIMO);
        p.Add("VAL_MAXIMO", dto.VAL_MAXIMO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_INCLUIR_DESCRIPCION", dto.FLG_INCLUIR_DESCRIPCION ?? false);
        p.Add("COD_GRUPO_ESPECIFICACION_TECNICA", dto.COD_GRUPO_ESPECIFICACION_TECNICA);
        p.Add("FLG_REGISTRO_COMERCIAL", dto.FLG_REGISTRO_COMERCIAL ?? false);
        p.Add("FLG_FICHA_TECNICA", dto.FLG_FICHA_TECNICA ?? false);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PRODUCTO, string COD_ESPECIFICACION_TECNICA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Delete,
            new { COD_PRODUCTO, COD_ESPECIFICACION_TECNICA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ProductosEspecificacionesTecnicas.Delete, new { COD_PRODUCTO, COD_ESPECIFICACION_TECNICA })
        };
    }
}
}
