using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class UnidadesComercialesRepository : IUnidadesComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public UnidadesComercialesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<UnidadesComerciales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesComerciales>(SP.SCHEMAComercial.UnidadesComerciales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<UnidadesComerciales?> ShowbyidAsync(int NUM_UNIDAD_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<UnidadesComerciales>(SP.SCHEMAComercial.UnidadesComerciales.Showbyid, new { NUM_UNIDAD_COMERCIAL = NUM_UNIDAD_COMERCIAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAComercial.UnidadesComerciales.Showcombox, "NUM_UNIDAD_COMERCIAL", "DES_UNIDAD_COMERCIAL");
    }

    public async Task<IEnumerable<UnidadesComerciales>> SearchAsync(UnidadesComercialesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<UnidadesComerciales>(SP.SCHEMAComercial.UnidadesComerciales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(UnidadesComercialesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("DES_UNIDAD_COMERCIAL", dto.DES_UNIDAD_COMERCIAL);
        p.Add("COD_TIPO_UNIDAD_COMERCIAL", dto.COD_TIPO_UNIDAD_COMERCIAL);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("NUM_POSICION_SUPERIOR", dto.NUM_POSICION_SUPERIOR);
        p.Add("NUM_POSICION_IZQUIERDA", dto.NUM_POSICION_IZQUIERDA);
        p.Add("NUM_LONGITUD_LARGO", dto.NUM_LONGITUD_LARGO);
        p.Add("NUM_LONGITUD_ANCHO", dto.NUM_LONGITUD_ANCHO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.UnidadesComerciales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.UnidadesComerciales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.UnidadesComerciales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(UnidadesComercialesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_UNIDAD_COMERCIAL", dto.NUM_UNIDAD_COMERCIAL);
        p.Add("DES_UNIDAD_COMERCIAL", dto.DES_UNIDAD_COMERCIAL);
        p.Add("COD_TIPO_UNIDAD_COMERCIAL", dto.COD_TIPO_UNIDAD_COMERCIAL);
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION);
        p.Add("NUM_POSICION_SUPERIOR", dto.NUM_POSICION_SUPERIOR);
        p.Add("NUM_POSICION_IZQUIERDA", dto.NUM_POSICION_IZQUIERDA);
        p.Add("NUM_LONGITUD_LARGO", dto.NUM_LONGITUD_LARGO);
        p.Add("NUM_LONGITUD_ANCHO", dto.NUM_LONGITUD_ANCHO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.UnidadesComerciales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.UnidadesComerciales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.UnidadesComerciales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_UNIDAD_COMERCIAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.UnidadesComerciales.Delete, new { NUM_UNIDAD_COMERCIAL = NUM_UNIDAD_COMERCIAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAComercial.UnidadesComerciales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.UnidadesComerciales.Delete, new { NUM_UNIDAD_COMERCIAL = NUM_UNIDAD_COMERCIAL })
        };
    }
}

}