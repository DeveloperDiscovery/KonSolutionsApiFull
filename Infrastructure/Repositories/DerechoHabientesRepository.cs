using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DerechoHabientesRepository : IDerechoHabientesRepository
{
    private readonly IDbConnectionFactory _db;
    public DerechoHabientesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DerechoHabientes>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DerechoHabientes>(SP.SCHEMARrhh.DerechoHabientes.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DerechoHabientes?> ShowbyidAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_DERECHO_HABIENTE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DerechoHabientes>(SP.SCHEMARrhh.DerechoHabientes.Showbyid, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_DERECHO_HABIENTE = COD_DERECHO_HABIENTE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.DerechoHabientes.Showcombox);
    }

    public async Task<IEnumerable<DerechoHabientes>> SearchAsync(DerechoHabientesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DerechoHabientes>(SP.SCHEMARrhh.DerechoHabientes.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DerechoHabientesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_DERECHO_HABIENTE", dto.COD_DERECHO_HABIENTE);
        p.Add("COD_TIPO_PARENTESCO", dto.COD_TIPO_PARENTESCO);
        p.Add("DES_PATERNO", dto.DES_PATERNO);
        p.Add("DES_MATERNO", dto.DES_MATERNO);
        p.Add("DES_NOMBRE", dto.DES_NOMBRE);
        p.Add("DES_NOMBRE2", dto.DES_NOMBRE2);
        p.Add("FEC_NACIMIENTO", dto.FEC_NACIMIENTO);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("NUM_DOCUMENTO_IDENTIDAD", dto.NUM_DOCUMENTO_IDENTIDAD);
        p.Add("COD_SEXO", dto.COD_SEXO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DerechoHabientes.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DerechoHabientes.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DerechoHabientes.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DerechoHabientesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_DERECHO_HABIENTE", dto.COD_DERECHO_HABIENTE);
        p.Add("COD_TIPO_PARENTESCO", dto.COD_TIPO_PARENTESCO);
        p.Add("DES_PATERNO", dto.DES_PATERNO);
        p.Add("DES_MATERNO", dto.DES_MATERNO);
        p.Add("DES_NOMBRE", dto.DES_NOMBRE);
        p.Add("DES_NOMBRE2", dto.DES_NOMBRE2);
        p.Add("FEC_NACIMIENTO", dto.FEC_NACIMIENTO);
        p.Add("COD_TIPO_DOCUMENTO_IDENTIDAD", dto.COD_TIPO_DOCUMENTO_IDENTIDAD);
        p.Add("NUM_DOCUMENTO_IDENTIDAD", dto.NUM_DOCUMENTO_IDENTIDAD);
        p.Add("COD_SEXO", dto.COD_SEXO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DerechoHabientes.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DerechoHabientes.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DerechoHabientes.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD_PERSONAL, decimal COD_DERECHO_HABIENTE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.DerechoHabientes.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_DERECHO_HABIENTE = COD_DERECHO_HABIENTE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.DerechoHabientes.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.DerechoHabientes.Delete, new { COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_DERECHO_HABIENTE = COD_DERECHO_HABIENTE })
        };
    }
}

}