using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesAsesoresComercialesRepository : IEntidadesAsesoresComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesAsesoresComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<EntidadesAsesoresComerciales>> ShowallAsync(decimal codEntidadCliente)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesAsesoresComerciales>(
            SP.SCHEMAComercial.EntidadesAsesoresComerciales.Showall,
            new { COD_ENTIDAD_CLIENTE = codEntidadCliente }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesAsesoresComerciales?> ShowbyidAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesAsesoresComerciales>(
            SP.SCHEMAComercial.EntidadesAsesoresComerciales.Showbyid,
            new { COD_ENTIDAD_CLIENTE = codEntidadCliente, COD_ENTIDAD_ASESOR_COMERCIAL = codEntidadAsesorComercial },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EntidadesAsesoresComerciales>> SearchAsync(EntidadesAsesoresComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesAsesoresComerciales>(
            SP.SCHEMAComercial.EntidadesAsesoresComerciales.Search,
            new { COD_ENTIDAD_CLIENTE = dto.COD_ENTIDAD_CLIENTE, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesAsesoresComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_CLIENTE", dto.COD_ENTIDAD_CLIENTE);
        p.Add("COD_ENTIDAD_ASESOR_COMERCIAL", dto.COD_ENTIDAD_ASESOR_COMERCIAL);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAComercial.EntidadesAsesoresComerciales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesAsesoresComerciales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesAsesoresComerciales.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal codEntidadCliente, decimal codEntidadAsesorComercial)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAComercial.EntidadesAsesoresComerciales.Delete,
            new { COD_ENTIDAD_CLIENTE = codEntidadCliente, COD_ENTIDAD_ASESOR_COMERCIAL = codEntidadAsesorComercial },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.EntidadesAsesoresComerciales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesAsesoresComerciales.Delete, new { COD_ENTIDAD_CLIENTE = codEntidadCliente, COD_ENTIDAD_ASESOR_COMERCIAL = codEntidadAsesorComercial })
        };
    }
}
}
