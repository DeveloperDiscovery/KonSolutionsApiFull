using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesTiposMedioContactoRepository : IEntidadesTiposMedioContactoRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesTiposMedioContactoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesTiposMedioContacto>> ShowallAsync(decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTiposMedioContacto>(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Showall, new { COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesTiposMedioContacto?> ShowbyidAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesTiposMedioContacto>(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.EntidadesTiposMedioContacto.Showcombox);
    }

    public async Task<IEnumerable<EntidadesTiposMedioContacto>> SearchAsync(EntidadesTiposMedioContactoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTiposMedioContacto>(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Search, new { COD_ENTIDAD = dto.COD_ENTIDAD, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>NUM_SECUENCIA se envía como 0 al crear — el SP INSERT recalcula el
    /// siguiente correlativo por COD_ENTIDAD dentro de la rama de alta (ROWCOUNT = 0)
    /// y lo ignora al actualizar (mismo patrón que NUM_VERSION en DisenosVersion).</summary>
    public async Task<WriteResult> InsertAsync(EntidadesTiposMedioContactoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA ?? 0);
        p.Add("COD_TIPO_MEDIO_CONTACTO", dto.COD_TIPO_MEDIO_CONTACTO);
        p.Add("DAT_TIPO_MEDIO_CONTACTO", dto.DAT_TIPO_MEDIO_CONTACTO);
        p.Add("COD_TIPO_USO", dto.COD_TIPO_USO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesTiposMedioContacto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, short NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesTiposMedioContacto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesTiposMedioContacto.Delete, new { COD_ENTIDAD = COD_ENTIDAD, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}
