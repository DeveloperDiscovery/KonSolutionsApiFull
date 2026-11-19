using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComercialTiposTransporteRepository : IComercialTiposTransporteRepository
{
    private readonly IDbConnectionFactory _db;
    public ComercialTiposTransporteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComercialTiposTransporte>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComercialTiposTransporte>(SP.SCHEMAComercial.TiposTransporte.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComercialTiposTransporte?> ShowbyidAsync(string COD_TIPO_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComercialTiposTransporte>(SP.SCHEMAComercial.TiposTransporte.Showbyid, new { COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposTransporte.Showcombox);
    }

    public async Task<IEnumerable<ComercialTiposTransporte>> SearchAsync(ComercialTiposTransporteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComercialTiposTransporte>(SP.SCHEMAComercial.TiposTransporte.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComercialTiposTransporteInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("DES_TIPO_TRANSPORTE", dto.DES_TIPO_TRANSPORTE);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposTransporte.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposTransporte.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposTransporte.Insert, p)
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
    public async Task<WriteResult> UpdateAsync(ComercialTiposTransporteUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("DES_TIPO_TRANSPORTE", dto.DES_TIPO_TRANSPORTE);
        p.Add("IMG_ICONO", dto.IMG_ICONO, System.Data.DbType.Binary);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposTransporte.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposTransporte.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposTransporte.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposTransporte.Delete, new { COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposTransporte.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposTransporte.Delete, new { COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE })
        };
    }
}

}