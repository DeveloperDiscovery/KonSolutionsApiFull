using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposInventarioRepository : ITiposInventarioRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposInventarioRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposInventario>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposInventario>(SP.SCHEMALogistica.TiposInventario.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposInventario?> ShowbyidAsync(string COD_TIPO_INVENTARIO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposInventario>(SP.SCHEMALogistica.TiposInventario.Showbyid, new { COD_TIPO_INVENTARIO = COD_TIPO_INVENTARIO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposInventario.Showcombox);
    }

    public async Task<IEnumerable<TiposInventario>> SearchAsync(TiposInventarioSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposInventario>(SP.SCHEMALogistica.TiposInventario.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposInventarioInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_INVENTARIO", dto.COD_TIPO_INVENTARIO);
        p.Add("DES_TIPO_INVENTARIO", dto.DES_TIPO_INVENTARIO);
        p.Add("FLG_MUESTREO", dto.FLG_MUESTREO);
        p.Add("FLG_ABC_ALEATORIO", dto.FLG_ABC_ALEATORIO);
        p.Add("COD_AREA_RESPONSABLE", dto.COD_AREA_RESPONSABLE);
        p.Add("COD_CARGO_RESPONSABLE", dto.COD_CARGO_RESPONSABLE);
        p.Add("COD_AREA_ALMACEN", dto.COD_AREA_ALMACEN);
        p.Add("COD_CARGO_ALMACEN", dto.COD_CARGO_ALMACEN);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposInventario.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposInventario.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposInventario.Insert, p)
        };
    }
}

    /// <summary>Modificación. Esta tabla no lleva columnas de auditoría — mismo criterio que
    /// LogisticaTiposTransporteRepository.</summary>
    public async Task<WriteResult> UpdateAsync(TiposInventarioUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_INVENTARIO", dto.COD_TIPO_INVENTARIO);
        p.Add("DES_TIPO_INVENTARIO", dto.DES_TIPO_INVENTARIO);
        p.Add("FLG_MUESTREO", dto.FLG_MUESTREO);
        p.Add("FLG_ABC_ALEATORIO", dto.FLG_ABC_ALEATORIO);
        p.Add("COD_AREA_RESPONSABLE", dto.COD_AREA_RESPONSABLE);
        p.Add("COD_CARGO_RESPONSABLE", dto.COD_CARGO_RESPONSABLE);
        p.Add("COD_AREA_ALMACEN", dto.COD_AREA_ALMACEN);
        p.Add("COD_CARGO_ALMACEN", dto.COD_CARGO_ALMACEN);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposInventario.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposInventario.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposInventario.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_INVENTARIO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposInventario.Delete, new { COD_TIPO_INVENTARIO = COD_TIPO_INVENTARIO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposInventario.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposInventario.Delete, new { COD_TIPO_INVENTARIO = COD_TIPO_INVENTARIO })
        };
    }
}

}
