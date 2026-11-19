using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class IncotermsTiposTransporteRepository : IIncotermsTiposTransporteRepository
{
    private readonly IDbConnectionFactory _db;
    public IncotermsTiposTransporteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<IncotermsTiposTransporte>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<IncotermsTiposTransporte>(SP.SCHEMAComercial.IncotermsTiposTransporte.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<IncotermsTiposTransporte?> ShowbyidAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<IncotermsTiposTransporte>(SP.SCHEMAComercial.IncotermsTiposTransporte.Showbyid, new { COD_INCOTERM = COD_INCOTERM, COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.IncotermsTiposTransporte.Showcombox);
    }

    public async Task<IEnumerable<IncotermsTiposTransporte>> SearchAsync(IncotermsTiposTransporteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<IncotermsTiposTransporte>(SP.SCHEMAComercial.IncotermsTiposTransporte.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(IncotermsTiposTransporteInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_INCOTERM", dto.COD_INCOTERM);
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.IncotermsTiposTransporte.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.IncotermsTiposTransporte.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.IncotermsTiposTransporte.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_INCOTERM, string COD_TIPO_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.IncotermsTiposTransporte.Delete, new { COD_INCOTERM = COD_INCOTERM, COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.IncotermsTiposTransporte.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.IncotermsTiposTransporte.Delete, new { COD_INCOTERM = COD_INCOTERM, COD_TIPO_TRANSPORTE = COD_TIPO_TRANSPORTE })
        };
    }
}

}