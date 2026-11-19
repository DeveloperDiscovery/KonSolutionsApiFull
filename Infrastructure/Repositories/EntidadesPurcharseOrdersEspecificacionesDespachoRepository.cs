using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesPurcharseOrdersEspecificacionesDespachoRepository : IEntidadesPurcharseOrdersEspecificacionesDespachoRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesPurcharseOrdersEspecificacionesDespachoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespacho>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurcharseOrdersEspecificacionesDespacho>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesPurcharseOrdersEspecificacionesDespacho?> ShowbyidAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesPurcharseOrdersEspecificacionesDespacho>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Showcombox);
    }

    public async Task<IEnumerable<EntidadesPurcharseOrdersEspecificacionesDespacho>> SearchAsync(EntidadesPurcharseOrdersEspecificacionesDespachoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesPurcharseOrdersEspecificacionesDespacho>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesPurcharseOrdersEspecificacionesDespachoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("CAN_EMBALAJE", dto.CAN_EMBALAJE);
        p.Add("COD_PRODUCTO_EMBALAJE_INNERPACK", dto.COD_PRODUCTO_EMBALAJE_INNERPACK);
        p.Add("COD_PRODUCTO_EMBALAJE_MASTERPACK", dto.COD_PRODUCTO_EMBALAJE_MASTERPACK);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(EntidadesPurcharseOrdersEspecificacionesDespachoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_PURCHASE_ORDER", dto.NUM_PURCHASE_ORDER);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("CAN_EMBALAJE", dto.CAN_EMBALAJE);
        p.Add("COD_PRODUCTO_EMBALAJE_INNERPACK", dto.COD_PRODUCTO_EMBALAJE_INNERPACK);
        p.Add("COD_PRODUCTO_EMBALAJE_MASTERPACK", dto.COD_PRODUCTO_EMBALAJE_MASTERPACK);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string NUM_PURCHASE_ORDER, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesPurcharseOrdersEspecificacionesDespacho.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_PURCHASE_ORDER = NUM_PURCHASE_ORDER, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}