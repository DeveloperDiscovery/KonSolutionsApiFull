using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Activos;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ActivosRepository : IActivosRepository
{
    private readonly IDbConnectionFactory _db;
    public ActivosRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Activos>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Activos>(SP.SCHEMAActivos.Activos.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<Activos?> ShowbyidAsync(string COD_ACTIVO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Activos>(SP.SCHEMAActivos.Activos.Showbyid, new { COD_ACTIVO = COD_ACTIVO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAActivos.Activos.Showcombox);
    }

    public async Task<IEnumerable<Activos>> SearchAsync(ActivosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Activos>(SP.SCHEMAActivos.Activos.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ActivosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("DES_ACTIVO", dto.DES_ACTIVO);
        p.Add("COD_GRUPO_ACTIVO", dto.COD_GRUPO_ACTIVO);
        p.Add("COD_FAMILIA_ACTIVO", dto.COD_FAMILIA_ACTIVO);
        p.Add("COD_SUB_FAMILIA_ACTIVO", dto.COD_SUB_FAMILIA_ACTIVO);
        p.Add("COD_TIPO_ACTIVO", dto.COD_TIPO_ACTIVO);
        p.Add("COD_TAG", dto.COD_TAG);
        p.Add("DES_MARCA", dto.DES_MARCA);
        p.Add("DES_MODELO", dto.DES_MODELO);
        p.Add("DES_SERIE", dto.DES_SERIE);
        p.Add("CAN_ANOS_VIDA_UTIL", dto.CAN_ANOS_VIDA_UTIL);
        p.Add("FLG_ASIGNACION_ENTIDAD", dto.FLG_ASIGNACION_ENTIDAD ?? false);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.Activos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.Activos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.Activos.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ActivosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("DES_ACTIVO", dto.DES_ACTIVO);
        p.Add("COD_GRUPO_ACTIVO", dto.COD_GRUPO_ACTIVO);
        p.Add("COD_FAMILIA_ACTIVO", dto.COD_FAMILIA_ACTIVO);
        p.Add("COD_SUB_FAMILIA_ACTIVO", dto.COD_SUB_FAMILIA_ACTIVO);
        p.Add("COD_TIPO_ACTIVO", dto.COD_TIPO_ACTIVO);
        p.Add("COD_TAG", dto.COD_TAG);
        p.Add("DES_MARCA", dto.DES_MARCA);
        p.Add("DES_MODELO", dto.DES_MODELO);
        p.Add("DES_SERIE", dto.DES_SERIE);
        p.Add("CAN_ANOS_VIDA_UTIL", dto.CAN_ANOS_VIDA_UTIL);
        p.Add("FLG_ASIGNACION_ENTIDAD", dto.FLG_ASIGNACION_ENTIDAD ?? false);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.Activos.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.Activos.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.Activos.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ACTIVO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAActivos.Activos.Delete, new { COD_ACTIVO = COD_ACTIVO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAActivos.Activos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAActivos.Activos.Delete, new { COD_ACTIVO = COD_ACTIVO })
        };
    }
}

}