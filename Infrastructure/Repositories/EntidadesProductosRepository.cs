using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesProductosRepository : IEntidadesProductosRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesProductosRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Los dos sentidos con la misma consulta: pasando la entidad devuelve SUS
    /// productos, pasando el producto devuelve SUS proveedores. El SP ignora el parámetro
    /// que venga en null, así que no hacen falta dos procedimientos ni dos métodos.</summary>
    public async Task<IEnumerable<EntidadesProductos>> ShowallAsync(decimal? codEntidad = null, string? codProducto = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesProductos>(
            SP.SCHEMAComercial.EntidadesProductos.Showall,
            new { COD_ENTIDAD = codEntidad, COD_PRODUCTO = string.IsNullOrWhiteSpace(codProducto) ? null : codProducto },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesProductos?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesProductos>(SP.SCHEMAComercial.EntidadesProductos.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesProductos.Showcombox);
    }

    public async Task<IEnumerable<EntidadesProductos>> SearchAsync(EntidadesProductosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesProductos>(SP.SCHEMAComercial.EntidadesProductos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesProductosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PRODUCTO_ENTIDAD", dto.COD_PRODUCTO_ENTIDAD);
        p.Add("DES_PRODUCTO_ENTIDAD", dto.DES_PRODUCTO_ENTIDAD);
        p.Add("COD_UNIDAD_MEDIDA_ATENCION", dto.COD_UNIDAD_MEDIDA_ATENCION);
        p.Add("FAC_CONVERSION", dto.FAC_CONVERSION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_PRECIO_ENTIDAD", dto.VAL_PRECIO_ENTIDAD);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("CAN_DIAS_ATENCION", dto.CAN_DIAS_ATENCION);
        p.Add("FLG_DIAS_HABILES", dto.FLG_DIAS_HABILES ?? false);
        p.Add("CAN_TOLERANCIA_EXCESO", dto.CAN_TOLERANCIA_EXCESO);
        p.Add("FLG_TOLERANCIA_EMBALAJE", dto.FLG_TOLERANCIA_EMBALAJE ?? false);
        p.Add("CAN_TOLERANCIA_DEFICIT", dto.CAN_TOLERANCIA_DEFICIT);
        p.Add("TAS_TOLERANCIA_EXCESO", dto.TAS_TOLERANCIA_EXCESO);
        p.Add("TAS_TOLERANCIA_DEFICIT", dto.TAS_TOLERANCIA_DEFICIT);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesProductos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesProductos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesProductos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesProductosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("COD_PRODUCTO_ENTIDAD", dto.COD_PRODUCTO_ENTIDAD);
        p.Add("DES_PRODUCTO_ENTIDAD", dto.DES_PRODUCTO_ENTIDAD);
        p.Add("COD_UNIDAD_MEDIDA_ATENCION", dto.COD_UNIDAD_MEDIDA_ATENCION);
        p.Add("FAC_CONVERSION", dto.FAC_CONVERSION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("VAL_PRECIO_ENTIDAD", dto.VAL_PRECIO_ENTIDAD);
        p.Add("TAS_DESCUENTO", dto.TAS_DESCUENTO);
        p.Add("CAN_DIAS_ATENCION", dto.CAN_DIAS_ATENCION);
        p.Add("FLG_DIAS_HABILES", dto.FLG_DIAS_HABILES ?? false);
        p.Add("CAN_TOLERANCIA_EXCESO", dto.CAN_TOLERANCIA_EXCESO);
        p.Add("FLG_TOLERANCIA_EMBALAJE", dto.FLG_TOLERANCIA_EMBALAJE ?? false);
        p.Add("CAN_TOLERANCIA_DEFICIT", dto.CAN_TOLERANCIA_DEFICIT);
        p.Add("TAS_TOLERANCIA_EXCESO", dto.TAS_TOLERANCIA_EXCESO);
        p.Add("TAS_TOLERANCIA_DEFICIT", dto.TAS_TOLERANCIA_DEFICIT);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesProductos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesProductos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesProductos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesProductos.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesProductos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesProductos.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}