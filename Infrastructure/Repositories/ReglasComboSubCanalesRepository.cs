using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ReglasComboSubCanalesRepository : IReglasComboSubCanalesRepository
{
    private readonly IDbConnectionFactory _db;
    public ReglasComboSubCanalesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ReglasComboSubCanales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ReglasComboSubCanales>(SP.SCHEMAComercial.ReglasComboSubCanales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ReglasComboSubCanales?> ShowbyidAsync(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ReglasComboSubCanales>(SP.SCHEMAComercial.ReglasComboSubCanales.Showbyid, new { NUM_UNIDAD_COMERCIAL = NUM_UNIDAD_COMERCIAL, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.ReglasComboSubCanales.Showcombox, "NUM_UNIDAD_COMERCIAL", "DES_REGLA_COMBOS");
    }

    public async Task<IEnumerable<ReglasComboSubCanales>> SearchAsync(ReglasComboSubCanalesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ReglasComboSubCanales>(SP.SCHEMAComercial.ReglasComboSubCanales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ReglasComboSubCanalesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_REGLA_COMBOS", dto.COD_REGLA_COMBOS);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ReglasComboSubCanales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ReglasComboSubCanales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ReglasComboSubCanales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ReglasComboSubCanalesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("NUM_CANAL_VENTA", dto.NUM_CANAL_VENTA);
        p.Add("NUM_SUB_CANAL_VENTA", dto.NUM_SUB_CANAL_VENTA);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_REGLA_COMBOS", dto.COD_REGLA_COMBOS);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ReglasComboSubCanales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ReglasComboSubCanales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ReglasComboSubCanales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_UNIDAD_COMERCIAL, int NUM_CANAL_VENTA, int NUM_SUB_CANAL_VENTA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.ReglasComboSubCanales.Delete, new { NUM_UNIDAD_COMERCIAL = NUM_UNIDAD_COMERCIAL, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.ReglasComboSubCanales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.ReglasComboSubCanales.Delete, new { NUM_UNIDAD_COMERCIAL = NUM_UNIDAD_COMERCIAL, NUM_CANAL_VENTA = NUM_CANAL_VENTA, NUM_SUB_CANAL_VENTA = NUM_SUB_CANAL_VENTA })
        };
    }
}

}