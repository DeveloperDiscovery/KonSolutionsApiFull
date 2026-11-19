using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CargosImplementosRepository : ICargosImplementosRepository
{
    private readonly IDbConnectionFactory _db;
    public CargosImplementosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CargosImplementos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CargosImplementos>(SP.SCHEMARrhh.CargosImplementos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CargosImplementos?> ShowbyidAsync(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CargosImplementos>(SP.SCHEMARrhh.CargosImplementos.Showbyid, new { COD_CARGO = COD_CARGO, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.CargosImplementos.Showcombox);
    }

    public async Task<IEnumerable<CargosImplementos>> SearchAsync(CargosImplementosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CargosImplementos>(SP.SCHEMARrhh.CargosImplementos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CargosImplementosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_PRODUCTO_IMPLEMENTO", dto.COD_PRODUCTO_IMPLEMENTO);
        p.Add("COD_TIPO_IMPLEMENTO_LABORAL", dto.COD_TIPO_IMPLEMENTO_LABORAL);
        p.Add("NUM_MESES_REPOSICION", dto.NUM_MESES_REPOSICION);
        p.Add("FLG_CONSIDERAR_FECHA_ENTREGA", dto.FLG_CONSIDERAR_FECHA_ENTREGA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosImplementos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosImplementos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosImplementos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CargosImplementosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_PRODUCTO_IMPLEMENTO", dto.COD_PRODUCTO_IMPLEMENTO);
        p.Add("COD_TIPO_IMPLEMENTO_LABORAL", dto.COD_TIPO_IMPLEMENTO_LABORAL);
        p.Add("NUM_MESES_REPOSICION", dto.NUM_MESES_REPOSICION);
        p.Add("FLG_CONSIDERAR_FECHA_ENTREGA", dto.FLG_CONSIDERAR_FECHA_ENTREGA ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosImplementos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosImplementos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosImplementos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CARGO, string COD_PRODUCTO_IMPLEMENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosImplementos.Delete, new { COD_CARGO = COD_CARGO, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosImplementos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosImplementos.Delete, new { COD_CARGO = COD_CARGO, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO })
        };
    }
}

}