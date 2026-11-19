using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventarosAlmacenesInventariadoresRepository : IInventarosAlmacenesInventariadoresRepository
{
    private readonly IDbConnectionFactory _db;
    public InventarosAlmacenesInventariadoresRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<InventarosAlmacenesInventariadores>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventarosAlmacenesInventariadores>(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<InventarosAlmacenesInventariadores?> ShowbyidAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventarosAlmacenesInventariadores>(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Showbyid, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR = COD_ENTIDAD_INVENTARIADOR }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Showcombox);
    }

    public async Task<IEnumerable<InventarosAlmacenesInventariadores>> SearchAsync(InventarosAlmacenesInventariadoresSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventarosAlmacenesInventariadores>(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(InventarosAlmacenesInventariadoresInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_ENTIDAD_INVENTARIADOR", dto.COD_ENTIDAD_INVENTARIADOR);
        p.Add("COD_TIPO_INVENTARIO", dto.COD_TIPO_INVENTARIO);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("FLG_MANUAL", dto.FLG_MANUAL ?? false);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(InventarosAlmacenesInventariadoresUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_ENTIDAD_INVENTARIADOR", dto.COD_ENTIDAD_INVENTARIADOR);
        p.Add("COD_TIPO_INVENTARIO", dto.COD_TIPO_INVENTARIO);
        p.Add("DES_OBSERVACIONES", dto.DES_OBSERVACIONES);
        p.Add("FEC_INICIO", dto.FEC_INICIO);
        p.Add("FEC_TERMINO", dto.FEC_TERMINO);
        p.Add("FLG_MANUAL", dto.FLG_MANUAL ?? false);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR = COD_ENTIDAD_INVENTARIADOR }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventarosAlmacenesInventariadores.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN, COD_ENTIDAD_INVENTARIADOR = COD_ENTIDAD_INVENTARIADOR })
        };
    }
}

}