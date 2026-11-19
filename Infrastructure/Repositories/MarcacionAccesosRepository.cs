using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MarcacionAccesosRepository : IMarcacionAccesosRepository
{
    private readonly IDbConnectionFactory _db;
    public MarcacionAccesosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MarcacionAccesos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MarcacionAccesos>(SP.SCHEMASafety.MarcacionAccesos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MarcacionAccesos?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION, string COD_ACTIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MarcacionAccesos>(SP.SCHEMASafety.MarcacionAccesos.Showbyid, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ACTIVO = COD_ACTIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.MarcacionAccesos.Showcombox);
    }

    public async Task<IEnumerable<MarcacionAccesos>> SearchAsync(MarcacionAccesosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MarcacionAccesos>(SP.SCHEMASafety.MarcacionAccesos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MarcacionAccesosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FEC_ACCESO", dto.FEC_ACCESO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.MarcacionAccesos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.MarcacionAccesos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.MarcacionAccesos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MarcacionAccesosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FEC_ACCESO", dto.FEC_ACCESO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.MarcacionAccesos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.MarcacionAccesos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.MarcacionAccesos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, string COD_ACTIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.MarcacionAccesos.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ACTIVO = COD_ACTIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.MarcacionAccesos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.MarcacionAccesos.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ACTIVO = COD_ACTIVO })
        };
    }
}

}