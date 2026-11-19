using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosVersionSectorComponenteRepository : IDisenosVersionSectorComponenteRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosVersionSectorComponenteRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosVersionSectorComponente>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionSectorComponente>(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosVersionSectorComponente?> ShowbyidAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosVersionSectorComponente>(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Showbyid, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Showcombox, "COD_DISENO", "DES_COMPONENTE_PRODUCTO");
    }

    public async Task<IEnumerable<DisenosVersionSectorComponente>> SearchAsync(DisenosVersionSectorComponenteSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionSectorComponente>(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosVersionSectorComponenteInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DisenosVersionSectorComponenteUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_SECTOR_PRODUCTO", dto.COD_SECTOR_PRODUCTO);
        p.Add("COD_COMPONENTE_PRODUCTO", dto.COD_COMPONENTE_PRODUCTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_DISENO, int NUM_VERSION, string COD_SECTOR_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionSectorComponente.Delete, new { COD_DISENO = COD_DISENO, NUM_VERSION = NUM_VERSION, COD_SECTOR_PRODUCTO = COD_SECTOR_PRODUCTO })
        };
    }
}

}