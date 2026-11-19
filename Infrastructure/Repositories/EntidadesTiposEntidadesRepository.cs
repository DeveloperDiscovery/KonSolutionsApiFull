using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesTiposEntidadesRepository : IEntidadesTiposEntidadesRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesTiposEntidadesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesTiposEntidades>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTiposEntidades>(SP.SCHEMAEstandar.EntidadesTiposEntidades.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesTiposEntidades?> ShowbyidAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesTiposEntidades>(SP.SCHEMAEstandar.EntidadesTiposEntidades.Showbyid, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EntidadesTiposEntidades.Showcombox);
    }

    public async Task<IEnumerable<EntidadesTiposEntidades>> SearchAsync(EntidadesTiposEntidadesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTiposEntidades>(SP.SCHEMAEstandar.EntidadesTiposEntidades.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesTiposEntidadesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_ENTIDAD", dto.COD_TIPO_ENTIDAD);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EntidadesTiposEntidades.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EntidadesTiposEntidades.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EntidadesTiposEntidades.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, string COD_TIPO_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.EntidadesTiposEntidades.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.EntidadesTiposEntidades.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.EntidadesTiposEntidades.Delete, new { COD_ENTIDAD = COD_ENTIDAD, COD_TIPO_ENTIDAD = COD_TIPO_ENTIDAD })
        };
    }
}

    public async Task<IEnumerable<EntidadesTiposEntidadesShowselectRow>> ShowselectAsync(decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesTiposEntidadesShowselectRow>(
            SP.SCHEMAEstandar.EntidadesTiposEntidades.Showwselect, new { COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    // NOTA: se asume que el parámetro del SP se llama @COD_TIPO_ENTIDAD (mismo nombre que la
    // columna). Si el SP real usa otro nombre de parámetro, ajustar aquí.
    // El SP devuelve columnas crudas (COD_ENTIDAD, DES_NOMBRE_COMPLETO/DES_COMERCIAL, ...),
    // no Id/Texto — mismo patrón que EntidadesTemporadasRepository.ShowcomboxAsync.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxidAsync(string codTipoEntidad)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.EntidadesTiposEntidades.Showcomboxid, new { COD_TIPO_ENTIDAD = codTipoEntidad });
    }

}