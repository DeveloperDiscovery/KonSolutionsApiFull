using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesUnidadesComercialesRepository : IEntidadesUnidadesComercialesRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesUnidadesComercialesRepository(IDbConnectionFactory db) => _db = db;

    private static string? Nulo(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

    public async Task<IEnumerable<EntidadesUnidadesComerciales>> ShowallAsync(decimal codEntidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesUnidadesComerciales>(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Showall,
            new { COD_ENTIDAD = codEntidad }, commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesUnidadesComerciales?> ShowbyidAsync(decimal codEntidad, byte codUnidadComercial)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesUnidadesComerciales>(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Showbyid,
            new { COD_ENTIDAD = codEntidad, COD_UNIDAD_COMERCIAL = codUnidadComercial }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesUnidadesComercialesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_UNIDAD_COMERCIAL", dto.COD_UNIDAD_COMERCIAL);
        p.Add("DES_UNIDAD_COMERCIAL", Nulo(dto.DES_UNIDAD_COMERCIAL));
        p.Add("COD_PAIS", Nulo(dto.COD_PAIS));
        p.Add("DES_DIRECCION", Nulo(dto.DES_DIRECCION));
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesUnidadesComerciales.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EntidadesUnidadesComercialesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_UNIDAD_COMERCIAL", dto.COD_UNIDAD_COMERCIAL);
        p.Add("DES_UNIDAD_COMERCIAL", Nulo(dto.DES_UNIDAD_COMERCIAL));
        p.Add("COD_PAIS", Nulo(dto.COD_PAIS));
        p.Add("DES_DIRECCION", Nulo(dto.DES_DIRECCION));
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", dto.COD_USUARIO_REGISTRO);
        p.Add("COD_ESTACION_REGISTRO", dto.COD_ESTACION_REGISTRO);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesUnidadesComerciales.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal codEntidad, byte codUnidadComercial)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_ENTIDAD = codEntidad, COD_UNIDAD_COMERCIAL = codUnidadComercial };
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesUnidadesComerciales.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesUnidadesComerciales.Delete, p)
            };
        }
    }
}
