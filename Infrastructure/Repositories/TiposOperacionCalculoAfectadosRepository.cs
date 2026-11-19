using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOperacionCalculoAfectadosRepository : ITiposOperacionCalculoAfectadosRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOperacionCalculoAfectadosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOperacionCalculoAfectados>> ShowallAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionCalculoAfectados>(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, COD_TIPO_VALOR_CALCULO = codTipoValorCalculo },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOperacionCalculoAfectados?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOperacionCalculoAfectados>(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Showbyid,
            new { COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TiposOperacionCalculoAfectados>> SearchAsync(string codClaseOperacion, string codTipoOperacion, string codTipoValorCalculo, TiposOperacionCalculoAfectadosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionCalculoAfectados>(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Search,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, COD_TIPO_VALOR_CALCULO = codTipoValorCalculo, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOperacionCalculoAfectadosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_VALOR_CALCULO_AFECTADO", dto.COD_TIPO_VALOR_CALCULO_AFECTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO, string COD_TIPO_VALOR_CALCULO_AFECTADO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Delete,
            new { COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionCalculoAfectados.Delete, new { COD_CLASE_OPERACION, COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO, COD_TIPO_VALOR_CALCULO_AFECTADO })
        };
    }
}
}
