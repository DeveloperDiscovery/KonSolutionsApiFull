using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosVersionSectorComponenteProductoFormulaRepository : IDisenosVersionSectorComponenteProductoFormulaRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosVersionSectorComponenteProductoFormulaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosVersionSectorComponenteProductoFormula>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionSectorComponenteProductoFormula>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosVersionSectorComponenteProductoFormula?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosVersionSectorComponenteProductoFormula>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Showbyid, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Showcombox);
    }

    public async Task<IEnumerable<DisenosVersionSectorComponenteProductoFormula>> SearchAsync(DisenosVersionSectorComponenteProductoFormulaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionSectorComponenteProductoFormula>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosVersionSectorComponenteProductoFormulaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO_COMPONENTE", dto.COD_PRODUCTO_COMPONENTE);
        p.Add("NUM_SECUENCIA_FORMULA", dto.NUM_SECUENCIA_FORMULA);
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("COD_TIPO_ELEMENTO_FORMULA", dto.COD_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_FORMULA", dto.DES_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_MASCARA_FORMULA", dto.DES_ELEMENTO_MASCARA_FORMULA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DisenosVersionSectorComponenteProductoFormulaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_PRODUCTO_COMPONENTE", dto.COD_PRODUCTO_COMPONENTE);
        p.Add("NUM_SECUENCIA_FORMULA", dto.NUM_SECUENCIA_FORMULA);
        p.Add("COD_CLASE_ELEMENTO_FORMULA", dto.COD_CLASE_ELEMENTO_FORMULA);
        p.Add("COD_TIPO_ELEMENTO_FORMULA", dto.COD_TIPO_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_FORMULA", dto.DES_ELEMENTO_FORMULA);
        p.Add("DES_ELEMENTO_MASCARA_FORMULA", dto.DES_ELEMENTO_MASCARA_FORMULA);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponenteProductoFormula.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO })
        };
    }
}

}