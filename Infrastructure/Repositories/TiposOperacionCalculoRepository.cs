using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposOperacionCalculoRepository : ITiposOperacionCalculoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposOperacionCalculoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposOperacionCalculo>> ShowallAsync(string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionCalculo>(SP.SCHEMAComercial.TiposOperacionCalculo.Showall,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposOperacionCalculo?> ShowbyidAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposOperacionCalculo>(SP.SCHEMAComercial.TiposOperacionCalculo.Showbyid, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.TiposOperacionCalculo.Showcombox);
    }

    public async Task<IEnumerable<TiposOperacionCalculo>> SearchAsync(string codClaseOperacion, string codTipoOperacion, TiposOperacionCalculoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposOperacionCalculo>(SP.SCHEMAComercial.TiposOperacionCalculo.Search,
            new { COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposOperacionCalculoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_CALCULO_INVERSO", dto.FLG_CALCULO_INVERSO ?? false);
        p.Add("FLG_MOSTRAR_COMPROBANTE", dto.FLG_MOSTRAR_COMPROBANTE ?? false);
        p.Add("FLG_VALOR_MOSTRAR", dto.FLG_VALOR_MOSTRAR ?? false);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionCalculo.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionCalculo.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionCalculo.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposOperacionCalculoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("FLG_CALCULO_INVERSO", dto.FLG_CALCULO_INVERSO ?? false);
        p.Add("FLG_MOSTRAR_COMPROBANTE", dto.FLG_MOSTRAR_COMPROBANTE ?? false);
        p.Add("FLG_VALOR_MOSTRAR", dto.FLG_VALOR_MOSTRAR ?? false);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionCalculo.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionCalculo.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionCalculo.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_OPERACION, string COD_TIPO_OPERACION, string COD_TIPO_VALOR_CALCULO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.TiposOperacionCalculo.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.TiposOperacionCalculo.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.TiposOperacionCalculo.Delete, new { COD_CLASE_OPERACION = COD_CLASE_OPERACION, COD_TIPO_OPERACION = COD_TIPO_OPERACION, COD_TIPO_VALOR_CALCULO = COD_TIPO_VALOR_CALCULO })
        };
    }
}

    public async Task<WriteResult> CopyAsync(TiposOperacionCalculoCopyDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_CLASE_OPERACION_DESTINO", dto.COD_CLASE_OPERACION_DESTINO);
        p.Add("COD_TIPO_OPERACION_DESTINO", dto.COD_TIPO_OPERACION_DESTINO);
        p.Add("COD_USUARIO", codUsuario);
        p.Add("COD_ESTACION", codEstacion);
        await conn.ExecuteAsync(SP.SCHEMAComercial.TiposOperacionCalculo.Copy, p, commandType: CommandType.StoredProcedure);
        return new WriteResult { Estado = "OK", AffectedRows = 1 };
    }

}