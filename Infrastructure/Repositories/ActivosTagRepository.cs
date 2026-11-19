using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Safety;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ActivosTagRepository : IActivosTagRepository
{
    private readonly IDbConnectionFactory _db;
    public ActivosTagRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ActivosTag>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosTag>(SP.SCHEMASafety.ActivosTag.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ActivosTag?> ShowbyidAsync(string COD_ACTIVO_TAG)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ActivosTag>(SP.SCHEMASafety.ActivosTag.Showbyid, new { COD_ACTIVO_TAG = COD_ACTIVO_TAG }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASafety.ActivosTag.Showcombox);
    }

    public async Task<IEnumerable<ActivosTag>> SearchAsync(ActivosTagSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ActivosTag>(SP.SCHEMASafety.ActivosTag.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ActivosTagInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO_TAG", dto.COD_ACTIVO_TAG);
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ActivosTag.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ActivosTag.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ActivosTag.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ActivosTagUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ACTIVO_TAG", dto.COD_ACTIVO_TAG);
        p.Add("COD_ACTIVO", dto.COD_ACTIVO);
        p.Add("COD_ENTIDAD_ASIGNADA", dto.COD_ENTIDAD_ASIGNADA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ActivosTag.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ActivosTag.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ActivosTag.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ACTIVO_TAG)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASafety.ActivosTag.Delete, new { COD_ACTIVO_TAG = COD_ACTIVO_TAG }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASafety.ActivosTag.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASafety.ActivosTag.Delete, new { COD_ACTIVO_TAG = COD_ACTIVO_TAG })
        };
    }
}

}