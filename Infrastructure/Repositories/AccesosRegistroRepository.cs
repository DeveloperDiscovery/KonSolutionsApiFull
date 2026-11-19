using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AccesosRegistroRepository : IAccesosRegistroRepository
{
    private readonly IDbConnectionFactory _db;
    public AccesosRegistroRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AccesosRegistro>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AccesosRegistro>(SP.SCHEMASafety.AccesosRegistro.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<AccesosRegistro?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AccesosRegistro>(SP.SCHEMASafety.AccesosRegistro.Showbyid, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.AccesosRegistro.Showcombox);
    }

    public async Task<IEnumerable<AccesosRegistro>> SearchAsync(AccesosRegistroSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AccesosRegistro>(SP.SCHEMASafety.AccesosRegistro.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(AccesosRegistroInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FEC_ACCESO", dto.FEC_ACCESO);
        p.Add("DES_INCIDENCIA", dto.DES_INCIDENCIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.AccesosRegistro.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.AccesosRegistro.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.AccesosRegistro.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(AccesosRegistroUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("FEC_ACCESO", dto.FEC_ACCESO);
        p.Add("DES_INCIDENCIA", dto.DES_INCIDENCIA);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.AccesosRegistro.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.AccesosRegistro.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.AccesosRegistro.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION, decimal COD_ENTIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.AccesosRegistro.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ENTIDAD = COD_ENTIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.AccesosRegistro.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.AccesosRegistro.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION, COD_ENTIDAD = COD_ENTIDAD })
        };
    }
}

}