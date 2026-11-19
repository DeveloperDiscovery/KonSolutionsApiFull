using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosVersionSectorComponenteProductoRepository : IDisenosVersionSectorComponenteProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosVersionSectorComponenteProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosVersionSectorComponenteProducto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionSectorComponenteProducto>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosVersionSectorComponenteProducto?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosVersionSectorComponenteProducto>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Showbyid, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Showcombox);
    }

    public async Task<IEnumerable<DisenosVersionSectorComponenteProducto>> SearchAsync(DisenosVersionSectorComponenteProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionSectorComponenteProducto>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosVersionSectorComponenteProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO_COMPONENTE", dto.COD_PRODUCTO_COMPONENTE);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_CONSUMO", dto.CAN_CONSUMO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("CAN_CONSUMO_SECUNDARIO", dto.CAN_CONSUMO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_CAPACIDAD", dto.COD_UNIDAD_MEDIDA_CAPACIDAD);
        p.Add("CAN_CAPACIDAD", dto.CAN_CAPACIDAD);
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("TAS_MERMA", dto.TAS_MERMA);
        p.Add("CAN_MERMA", dto.CAN_MERMA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("PRE_TOTAL", dto.PRE_TOTAL);
        p.Add("DES_FORMULA", dto.DES_FORMULA);
        p.Add("DES_MASCARA_FORMULA", dto.DES_MASCARA_FORMULA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DisenosVersionSectorComponenteProductoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO_COMPONENTE", dto.COD_PRODUCTO_COMPONENTE);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_CONSUMO", dto.CAN_CONSUMO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("CAN_CONSUMO_SECUNDARIO", dto.CAN_CONSUMO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_CAPACIDAD", dto.COD_UNIDAD_MEDIDA_CAPACIDAD);
        p.Add("CAN_CAPACIDAD", dto.CAN_CAPACIDAD);
        p.Add("COD_TIPO_MERMA", dto.COD_TIPO_MERMA);
        p.Add("TAS_MERMA", dto.TAS_MERMA);
        p.Add("CAN_MERMA", dto.CAN_MERMA);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("PRE_UNITARIO", dto.PRE_UNITARIO);
        p.Add("PRE_TOTAL", dto.PRE_TOTAL);
        p.Add("DES_FORMULA", dto.DES_FORMULA);
        p.Add("DES_MASCARA_FORMULA", dto.DES_MASCARA_FORMULA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProducto.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO })
        };
    }
}

}