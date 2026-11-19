using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository : IEntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas?> ShowbyidAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Showcombox);
    }

    public async Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FLG_HEADER", dto.FLG_HEADER ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicasUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_ESPECIFICACION_TECNICA", dto.COD_ESPECIFICACION_TECNICA);
        p.Add("COD_ITEM_TECNICO", dto.COD_ITEM_TECNICO);
        p.Add("FLG_HEADER", dto.FLG_HEADER ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespachoEspecificacionesTecnicas.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}