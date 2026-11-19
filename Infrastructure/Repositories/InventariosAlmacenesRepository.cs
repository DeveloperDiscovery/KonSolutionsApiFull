using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class InventariosAlmacenesRepository : IInventariosAlmacenesRepository
{
    private readonly IDbConnectionFactory _db;
    public InventariosAlmacenesRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>El SP _SHOWALL exige @NUM_INVENTARIO — no es un listado global, filtra los
    /// almacenes de un inventario puntual. Sin filtro se pasa null (columna admite NULL en el SP).</summary>
    public async Task<IEnumerable<InventariosAlmacenes>> ShowallAsync(int? numInventario = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenes>(SP.SCHEMALogistica.InventariosAlmacenes.Showall, new { NUM_INVENTARIO = numInventario }, commandType: CommandType.StoredProcedure);
    }

    public async Task<InventariosAlmacenes?> ShowbyidAsync(int NUM_INVENTARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<InventariosAlmacenes>(SP.SCHEMALogistica.InventariosAlmacenes.Showbyid, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.InventariosAlmacenes.Showcombox);
    }

    public async Task<IEnumerable<InventariosAlmacenes>> SearchAsync(InventariosAlmacenesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<InventariosAlmacenes>(SP.SCHEMALogistica.InventariosAlmacenes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(InventariosAlmacenesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("FLG_TOTAL", dto.FLG_TOTAL ?? false);
        p.Add("FLG_INMOVILIZACION", dto.FLG_INMOVILIZACION ?? false);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("FEC_INICIO_PROGRAMADA", dto.FEC_INICIO_PROGRAMADA);
        p.Add("FEC_FINAL_PROGRAMADA", dto.FEC_FINAL_PROGRAMADA);
        p.Add("FEC_INICIO_EFECTUADA", dto.FEC_INICIO_EFECTUADA);
        p.Add("FEC_FINAL_EFECTUADA", dto.FEC_FINAL_EFECTUADA);
        p.Add("FEC_CORTE", dto.FEC_CORTE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(InventariosAlmacenesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_INVENTARIO", dto.NUM_INVENTARIO);
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("FLG_TOTAL", dto.FLG_TOTAL ?? false);
        p.Add("FLG_INMOVILIZACION", dto.FLG_INMOVILIZACION ?? false);
        p.Add("COD_ENTIDAD_RESPONSABLE", dto.COD_ENTIDAD_RESPONSABLE);
        p.Add("FEC_INICIO_PROGRAMADA", dto.FEC_INICIO_PROGRAMADA);
        p.Add("FEC_FINAL_PROGRAMADA", dto.FEC_FINAL_PROGRAMADA);
        p.Add("FEC_INICIO_EFECTUADA", dto.FEC_INICIO_EFECTUADA);
        p.Add("FEC_FINAL_EFECTUADA", dto.FEC_FINAL_EFECTUADA);
        p.Add("FEC_CORTE", dto.FEC_CORTE);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.InventariosAlmacenes.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.InventariosAlmacenes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.InventariosAlmacenes.Delete, new { NUM_INVENTARIO = NUM_INVENTARIO, COD_ALMACEN = COD_ALMACEN })
        };
    }
}

}