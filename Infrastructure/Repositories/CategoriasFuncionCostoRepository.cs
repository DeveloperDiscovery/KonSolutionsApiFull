using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Costos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CategoriasFuncionCostoRepository : ICategoriasFuncionCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public CategoriasFuncionCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CategoriasFuncionCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasFuncionCosto>(SP.SCHEMACostos.CategoriasFuncionCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CategoriasFuncionCosto?> ShowbyidAsync(string COD_CATEGORIA_FUNCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CategoriasFuncionCosto>(SP.SCHEMACostos.CategoriasFuncionCosto.Showbyid, new { COD_CATEGORIA_FUNCION = COD_CATEGORIA_FUNCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMACostos.CategoriasFuncionCosto.Showcombox);
    }

    public async Task<IEnumerable<CategoriasFuncionCosto>> SearchAsync(CategoriasFuncionCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CategoriasFuncionCosto>(SP.SCHEMACostos.CategoriasFuncionCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CategoriasFuncionCostoInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_FUNCION", dto.COD_CATEGORIA_FUNCION);
        p.Add("DES_CATEGORIA_FUNCION", dto.DES_CATEGORIA_FUNCION);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CategoriasFuncionCosto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CategoriasFuncionCosto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CategoriasFuncionCosto.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CategoriasFuncionCostoUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CATEGORIA_FUNCION", dto.COD_CATEGORIA_FUNCION);
        p.Add("DES_CATEGORIA_FUNCION", dto.DES_CATEGORIA_FUNCION);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CategoriasFuncionCosto.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CategoriasFuncionCosto.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CategoriasFuncionCosto.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CATEGORIA_FUNCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMACostos.CategoriasFuncionCosto.Delete, new { COD_CATEGORIA_FUNCION = COD_CATEGORIA_FUNCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMACostos.CategoriasFuncionCosto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMACostos.CategoriasFuncionCosto.Delete, new { COD_CATEGORIA_FUNCION = COD_CATEGORIA_FUNCION })
        };
    }
}

}