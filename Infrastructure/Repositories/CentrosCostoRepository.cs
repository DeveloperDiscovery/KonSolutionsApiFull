using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CentrosCostoRepository : ICentrosCostoRepository
{
    private readonly IDbConnectionFactory _db;
    public CentrosCostoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CentrosCosto>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CentrosCosto>(SP.SCHEMAContabilidad.CentrosCosto.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CentrosCosto?> ShowbyidAsync(string COD_CENTRO_COSTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CentrosCosto>(SP.SCHEMAContabilidad.CentrosCosto.Showbyid, new { COD_CENTRO_COSTO = COD_CENTRO_COSTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.CentrosCosto.Showcombox);
    }

    public async Task<IEnumerable<CentrosCosto>> SearchAsync(CentrosCostoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CentrosCosto>(SP.SCHEMAContabilidad.CentrosCosto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CentrosCostoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("DES_CENTRO_COSTO", dto.DES_CENTRO_COSTO);
        p.Add("COD_TIPO_CENTRO_COSTO", dto.COD_TIPO_CENTRO_COSTO);
        p.Add("NUM_NIVEL", dto.NUM_NIVEL);
        p.Add("COD_CENTRO_COSTO_PADRE", dto.COD_CENTRO_COSTO_PADRE);
        p.Add("FLG_GENERA_ASIENTO", dto.FLG_GENERA_ASIENTO ?? false);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CentrosCosto.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAContabilidad.CentrosCosto.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CentrosCosto.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string COD_CENTRO_COSTO)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CENTRO_COSTO", COD_CENTRO_COSTO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.CentrosCosto.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAContabilidad.CentrosCosto.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.CentrosCosto.Delete, p)
            };
        }
    }

}