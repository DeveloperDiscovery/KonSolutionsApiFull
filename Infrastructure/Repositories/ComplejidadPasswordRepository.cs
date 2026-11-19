using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Seguridad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ComplejidadPasswordRepository : IComplejidadPasswordRepository
{
    private readonly IDbConnectionFactory _db;
    public ComplejidadPasswordRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ComplejidadPassword>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComplejidadPassword>(SP.SCHEMASeguridad.ComplejidadPassword.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ComplejidadPassword?> ShowbyidAsync(string COD_PASSWORD_COMPLEJIDAD)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ComplejidadPassword>(SP.SCHEMASeguridad.ComplejidadPassword.Showbyid, new { COD_PASSWORD_COMPLEJIDAD = COD_PASSWORD_COMPLEJIDAD }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMASeguridad.ComplejidadPassword.Showcombox);
    }

    public async Task<IEnumerable<ComplejidadPassword>> SearchAsync(ComplejidadPasswordSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ComplejidadPassword>(SP.SCHEMASeguridad.ComplejidadPassword.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ComplejidadPasswordInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PASSWORD_COMPLEJIDAD", dto.COD_PASSWORD_COMPLEJIDAD);
        p.Add("DES_PASSWORD_COMPLEJIDAD", dto.DES_PASSWORD_COMPLEJIDAD);
        // Campos de caracteres opcionales: vacío → NULL (la tabla lo acepta).
        p.Add("DES_CARACTERES_LETRAS_MAYUSCULAS", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_LETRAS_MAYUSCULAS) ? null : dto.DES_CARACTERES_LETRAS_MAYUSCULAS, System.Data.DbType.String);
        p.Add("DES_CARACTERES_LETRAS_MINUSCULAS", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_LETRAS_MINUSCULAS) ? null : dto.DES_CARACTERES_LETRAS_MINUSCULAS, System.Data.DbType.String);
        p.Add("DES_CARACTERES_NUMERICOS", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_NUMERICOS) ? null : dto.DES_CARACTERES_NUMERICOS, System.Data.DbType.String);
        p.Add("DES_CARACTERES_ESPECIALES", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_ESPECIALES) ? null : dto.DES_CARACTERES_ESPECIALES, System.Data.DbType.String);
        p.Add("CAN_CARACTERES_MINIMO", dto.CAN_CARACTERES_MINIMO);
        p.Add("CAN_CARACTERES_MAXIMO", dto.CAN_CARACTERES_MAXIMO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.ComplejidadPassword.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.ComplejidadPassword.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.ComplejidadPassword.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ComplejidadPasswordUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PASSWORD_COMPLEJIDAD", dto.COD_PASSWORD_COMPLEJIDAD);
        p.Add("DES_PASSWORD_COMPLEJIDAD", dto.DES_PASSWORD_COMPLEJIDAD);
        // Campos de caracteres opcionales: vacío → NULL (la tabla lo acepta).
        p.Add("DES_CARACTERES_LETRAS_MAYUSCULAS", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_LETRAS_MAYUSCULAS) ? null : dto.DES_CARACTERES_LETRAS_MAYUSCULAS, System.Data.DbType.String);
        p.Add("DES_CARACTERES_LETRAS_MINUSCULAS", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_LETRAS_MINUSCULAS) ? null : dto.DES_CARACTERES_LETRAS_MINUSCULAS, System.Data.DbType.String);
        p.Add("DES_CARACTERES_NUMERICOS", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_NUMERICOS) ? null : dto.DES_CARACTERES_NUMERICOS, System.Data.DbType.String);
        p.Add("DES_CARACTERES_ESPECIALES", string.IsNullOrWhiteSpace(dto.DES_CARACTERES_ESPECIALES) ? null : dto.DES_CARACTERES_ESPECIALES, System.Data.DbType.String);
        p.Add("CAN_CARACTERES_MINIMO", dto.CAN_CARACTERES_MINIMO);
        p.Add("CAN_CARACTERES_MAXIMO", dto.CAN_CARACTERES_MAXIMO);
        p.Add("NUM_ORDEN_PRESENTACION", dto.NUM_ORDEN_PRESENTACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.ComplejidadPassword.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.ComplejidadPassword.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.ComplejidadPassword.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_PASSWORD_COMPLEJIDAD)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMASeguridad.ComplejidadPassword.Delete, new { COD_PASSWORD_COMPLEJIDAD = COD_PASSWORD_COMPLEJIDAD }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMASeguridad.ComplejidadPassword.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMASeguridad.ComplejidadPassword.Delete, new { COD_PASSWORD_COMPLEJIDAD = COD_PASSWORD_COMPLEJIDAD })
        };
    }
}

}