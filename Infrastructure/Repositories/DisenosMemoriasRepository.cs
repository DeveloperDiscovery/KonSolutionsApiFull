using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Ingenieria;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DisenosMemoriasRepository : IDisenosMemoriasRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosMemoriasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosMemorias>> ShowallAsync(string codDiseno)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosMemorias>(
            SP.SCHEMAIngenieria.DisenosMemorias.Showall,
            new { COD_DISENO = codDiseno }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosMemorias?> ShowbyidAsync(string codDiseno, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosMemorias>(
            SP.SCHEMAIngenieria.DisenosMemorias.Showbyid,
            new { COD_DISENO = codDiseno, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosMemorias>> SearchAsync(DisenosMemoriasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosMemorias>(
            SP.SCHEMAIngenieria.DisenosMemorias.Search,
            new { COD_DISENO = dto.COD_DISENO, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosMemoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAIngenieria.DisenosMemorias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosMemorias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosMemorias.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codDiseno, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAIngenieria.DisenosMemorias.Delete,
            new { COD_DISENO = codDiseno, NUM_SECUENCIA = numSecuencia }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosMemorias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosMemorias.Delete, new { COD_DISENO = codDiseno, NUM_SECUENCIA = numSecuencia })
        };
    }
}
}

public class DisenosVersionesMemoriasRepository : IDisenosVersionesMemoriasRepository
{
    private readonly IDbConnectionFactory _db;
    public DisenosVersionesMemoriasRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DisenosVersionesMemorias>> ShowallAsync(string codDiseno, short numVersion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionesMemorias>(
            SP.SCHEMAIngenieria.DisenosVersionesMemorias.Showall,
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<DisenosVersionesMemorias?> ShowbyidAsync(string codDiseno, short numVersion, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DisenosVersionesMemorias>(
            SP.SCHEMAIngenieria.DisenosVersionesMemorias.Showbyid,
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<DisenosVersionesMemorias>> SearchAsync(DisenosVersionesMemoriasSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisenosVersionesMemorias>(
            SP.SCHEMAIngenieria.DisenosVersionesMemorias.Search,
            new { COD_DISENO = dto.COD_DISENO, NUM_VERSION = dto.NUM_VERSION, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DisenosVersionesMemoriasInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("DES_MEMORIA", dto.DES_MEMORIA);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAIngenieria.DisenosVersionesMemorias.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionesMemorias.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionesMemorias.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string codDiseno, short numVersion, short numSecuencia)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAIngenieria.DisenosVersionesMemorias.Delete,
            new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAIngenieria.DisenosVersionesMemorias.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAIngenieria.DisenosVersionesMemorias.Delete, new { COD_DISENO = codDiseno, NUM_VERSION = numVersion, NUM_SECUENCIA = numSecuencia })
        };
    }
}
}
