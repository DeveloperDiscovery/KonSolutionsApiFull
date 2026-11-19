using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class LogisticaTiposTransporteRepository : ILogisticaTiposTransporteRepository
{
    private readonly IDbConnectionFactory _db;
    public LogisticaTiposTransporteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<LogisticaTiposTransporte>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LogisticaTiposTransporte>(SP.SCHEMALogistica.TiposTransporte.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<LogisticaTiposTransporte?> ShowbyidAsync(string COD_TIPO_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<LogisticaTiposTransporte>(SP.SCHEMALogistica.TiposTransporte.Showbyid, new { COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposTransporte.Showcombox);
    }

    public async Task<IEnumerable<LogisticaTiposTransporte>> SearchAsync(LogisticaTiposTransporteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LogisticaTiposTransporte>(SP.SCHEMALogistica.TiposTransporte.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(LogisticaTiposTransporteInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("DES_TIPO_TRANSPORTE", dto.DES_TIPO_TRANSPORTE);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("FLG_TERRESTRE", dto.FLG_TERRESTRE);
        p.Add("FLG_MARITIMO", dto.FLG_MARITIMO);
        p.Add("FLG_FLUVIAL", dto.FLG_FLUVIAL);
        p.Add("FLG_AEREO", dto.FLG_AEREO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposTransporte.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposTransporte.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposTransporte.Insert, p)
        };
    }
}

    /// <summary>Modificación. Va contra el SP _UPDATE, que actualiza la fila existente.
    ///
    /// Antes la pantalla hacía POST también al editar, y desde que el SP de alta dejó de ser un
    /// upsert eso terminaba en violación de clave primaria: acá el código lo escribe el usuario,
    /// no se autogenera, así que no duplicaba — fallaba.
    ///
    /// Esta tabla no lleva columnas de auditoría, por eso el método no recibe usuario ni
    /// estación: el SP tampoco los declara (verificado contra sys.parameters).</summary>
    public async Task<WriteResult> UpdateAsync(LogisticaTiposTransporteUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("DES_TIPO_TRANSPORTE", dto.DES_TIPO_TRANSPORTE);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("FLG_TERRESTRE", dto.FLG_TERRESTRE);
        p.Add("FLG_MARITIMO", dto.FLG_MARITIMO);
        p.Add("FLG_FLUVIAL", dto.FLG_FLUVIAL);
        p.Add("FLG_AEREO", dto.FLG_AEREO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposTransporte.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposTransporte.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposTransporte.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposTransporte.Delete, new { COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposTransporte.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposTransporte.Delete, new { COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE })
        };
    }
}

}