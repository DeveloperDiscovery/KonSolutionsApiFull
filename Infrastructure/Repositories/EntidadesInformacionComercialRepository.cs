using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class EntidadesInformacionComercialRepository : IEntidadesInformacionComercialRepository
{
    private readonly IDbConnectionFactory _db;
    public EntidadesInformacionComercialRepository(IDbConnectionFactory db) => _db = db;

    // El SHOWALL pide entidad Y clase de operación: no existe una lectura de "toda la
    // información comercial de la entidad" de una sola vez. Por eso la ficha muestra una
    // clase por vez, con un combo arriba para cambiarla.
    public async Task<IEnumerable<EntidadesInformacionComercial>> ShowallAsync(decimal codEntidad, string codClaseOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesInformacionComercial>(
            SP.SCHEMAComercial.EntidadesInformacionComercial.Showall,
            new { COD_ENTIDAD = codEntidad, COD_CLASE_OPERACION = codClaseOperacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<EntidadesInformacionComercial?> ShowbyidAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<EntidadesInformacionComercial>(
            SP.SCHEMAComercial.EntidadesInformacionComercial.Showbyid,
            new { COD_ENTIDAD = codEntidad, COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<EntidadesInformacionComercial>> SearchAsync(EntidadesInformacionComercialSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<EntidadesInformacionComercial>(
            SP.SCHEMAComercial.EntidadesInformacionComercial.Search,
            new { COD_ENTIDAD = dto.COD_ENTIDAD, COD_CLASE_OPERACION = dto.COD_CLASE_OPERACION, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(EntidadesInformacionComercialInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesInformacionComercial.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesInformacionComercial.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesInformacionComercial.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(EntidadesInformacionComercialUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("COD_CONDICION_PAGO", dto.COD_CONDICION_PAGO);
        p.Add("COD_FORMA_PAGO", dto.COD_FORMA_PAGO);
        // El UPDATE recibe el usuario como @COD_USUARIO_REGISTRO y por dentro lo graba en
        // COD_USUARIO_ACTUALIZACION — mismo criterio que el resto de los SP del sistema.
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesInformacionComercial.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesInformacionComercial.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesInformacionComercial.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(decimal codEntidad, string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        var p = new { COD_ENTIDAD = codEntidad, COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion };
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAComercial.EntidadesInformacionComercial.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAComercial.EntidadesInformacionComercial.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAComercial.EntidadesInformacionComercial.Delete, p)
            };
        }
    }
}
