using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TransportesRepository : ITransportesRepository
{
    private readonly IDbConnectionFactory _db;
    public TransportesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Transportes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Transportes>(SP.SCHEMALogistica.Transportes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Transportes?> ShowbyidAsync(string COD_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Transportes>(SP.SCHEMALogistica.Transportes.Showbyid, new { COD_TRANSPORTE = COD_TRANSPORTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.Transportes.Showcombox);
    }

    public async Task<IEnumerable<Transportes>> SearchAsync(TransportesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Transportes>(SP.SCHEMALogistica.Transportes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TransportesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("DES_TRANSPORTE", dto.DES_TRANSPORTE);
        p.Add("FLG_PROPIO", dto.FLG_PROPIO);
        p.Add("FLG_ACARREO", dto.FLG_ACARREO);
        p.Add("COD_PRODUCTO_TRANSPORTE", dto.COD_PRODUCTO_TRANSPORTE);
        p.Add("COD_ENTIDAD_PROPIETARIO", dto.COD_ENTIDAD_PROPIETARIO);
        p.Add("COD_ENTIDAD_CONDUCTOR", dto.COD_ENTIDAD_CONDUCTOR);
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Transportes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Transportes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Transportes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TransportesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TRANSPORTE", dto.COD_TRANSPORTE);
        p.Add("DES_TRANSPORTE", dto.DES_TRANSPORTE);
        p.Add("FLG_PROPIO", dto.FLG_PROPIO);
        p.Add("FLG_ACARREO", dto.FLG_ACARREO);
        p.Add("COD_PRODUCTO_TRANSPORTE", dto.COD_PRODUCTO_TRANSPORTE);
        p.Add("COD_ENTIDAD_PROPIETARIO", dto.COD_ENTIDAD_PROPIETARIO);
        p.Add("COD_ENTIDAD_CONDUCTOR", dto.COD_ENTIDAD_CONDUCTOR);
        p.Add("COD_TIPO_TRANSPORTE", dto.COD_TIPO_TRANSPORTE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Transportes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Transportes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Transportes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TRANSPORTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.Transportes.Delete, new { COD_TRANSPORTE = COD_TRANSPORTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.Transportes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.Transportes.Delete, new { COD_TRANSPORTE = COD_TRANSPORTE })
        };
    }
}

}