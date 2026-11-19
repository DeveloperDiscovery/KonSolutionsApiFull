using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposSucursalesRepository : ITiposSucursalesRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposSucursalesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposSucursales>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSucursales>(SP.SCHEMAEstandar.TiposSucursales.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposSucursales?> ShowbyidAsync(string COD_TIPO_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposSucursales>(SP.SCHEMAEstandar.TiposSucursales.Showbyid, new { COD_TIPO_SUCURSAL = COD_TIPO_SUCURSAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.TiposSucursales.Showcombox);
    }

    public async Task<IEnumerable<TiposSucursales>> SearchAsync(TiposSucursalesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposSucursales>(SP.SCHEMAEstandar.TiposSucursales.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposSucursalesInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SUCURSAL", dto.COD_TIPO_SUCURSAL);
        p.Add("DES_TIPO_SUCURSAL", dto.DES_TIPO_SUCURSAL);
        p.Add("FLG_SERVICIO", dto.FLG_SERVICIO ?? false);
        p.Add("FLG_PLANTA", dto.FLG_PLANTA ?? false);
        p.Add("FLG_ALMACEN", dto.FLG_ALMACEN ?? false);
        p.Add("FLG_ADMINISTRACION", dto.FLG_ADMINISTRACION ?? false);
        p.Add("FLG_TIENDA", dto.FLG_TIENDA ?? false);
        p.Add("FLG_CALLCENTER", dto.FLG_CALLCENTER ?? false);
        p.Add("FLG_DESARROLLO", dto.FLG_DESARROLLO ?? false);
        p.Add("FLG_EXPLOTACION", dto.FLG_EXPLOTACION ?? false);
        p.Add("FLG_PUERTO", dto.FLG_PUERTO ?? false);
        p.Add("FLG_AEROPUERTO", dto.FLG_AEROPUERTO ?? false);
        p.Add("FLG_TERMINAL_TERRESTRE", dto.FLG_TERMINAL_TERRESTRE ?? false);
        p.Add("FLG_PATIO_MANIOBRAS", dto.FLG_PATIO_MANIOBRAS ?? false);
        p.Add("FLG_CENTRO_MEDICO", dto.FLG_CENTRO_MEDICO ?? false);
        p.Add("FLG_HOTEL", dto.FLG_HOTEL ?? false);
        p.Add("FLG_CENTRO_DATOS", dto.FLG_CENTRO_DATOS ?? false);
        p.Add("FLG_CENTRO_DISTRIBUCION", dto.FLG_CENTRO_DISTRIBUCION ?? false);
        p.Add("FLG_MONITOREO", dto.FLG_MONITOREO ?? false);
        p.Add("FLG_FUNDO", dto.FLG_FUNDO ?? false);
        p.Add("FLG_CAMPAMENTO", dto.FLG_CAMPAMENTO ?? false);
        p.Add("FLG_ADUANA", dto.FLG_ADUANA ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposSucursales.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposSucursales.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposSucursales.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(TiposSucursalesUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_TIPO_SUCURSAL", dto.COD_TIPO_SUCURSAL);
        p.Add("DES_TIPO_SUCURSAL", dto.DES_TIPO_SUCURSAL);
        p.Add("FLG_SERVICIO", dto.FLG_SERVICIO ?? false);
        p.Add("FLG_PLANTA", dto.FLG_PLANTA ?? false);
        p.Add("FLG_ALMACEN", dto.FLG_ALMACEN ?? false);
        p.Add("FLG_ADMINISTRACION", dto.FLG_ADMINISTRACION ?? false);
        p.Add("FLG_TIENDA", dto.FLG_TIENDA ?? false);
        p.Add("FLG_CALLCENTER", dto.FLG_CALLCENTER ?? false);
        p.Add("FLG_DESARROLLO", dto.FLG_DESARROLLO ?? false);
        p.Add("FLG_EXPLOTACION", dto.FLG_EXPLOTACION ?? false);
        p.Add("FLG_PUERTO", dto.FLG_PUERTO ?? false);
        p.Add("FLG_AEROPUERTO", dto.FLG_AEROPUERTO ?? false);
        p.Add("FLG_TERMINAL_TERRESTRE", dto.FLG_TERMINAL_TERRESTRE ?? false);
        p.Add("FLG_PATIO_MANIOBRAS", dto.FLG_PATIO_MANIOBRAS ?? false);
        p.Add("FLG_CENTRO_MEDICO", dto.FLG_CENTRO_MEDICO ?? false);
        p.Add("FLG_HOTEL", dto.FLG_HOTEL ?? false);
        p.Add("FLG_CENTRO_DATOS", dto.FLG_CENTRO_DATOS ?? false);
        p.Add("FLG_CENTRO_DISTRIBUCION", dto.FLG_CENTRO_DISTRIBUCION ?? false);
        p.Add("FLG_MONITOREO", dto.FLG_MONITOREO ?? false);
        p.Add("FLG_FUNDO", dto.FLG_FUNDO ?? false);
        p.Add("FLG_CAMPAMENTO", dto.FLG_CAMPAMENTO ?? false);
        p.Add("FLG_ADUANA", dto.FLG_ADUANA ?? false);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposSucursales.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposSucursales.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposSucursales.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_TIPO_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.TiposSucursales.Delete, new { COD_TIPO_SUCURSAL = COD_TIPO_SUCURSAL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.TiposSucursales.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.TiposSucursales.Delete, new { COD_TIPO_SUCURSAL = COD_TIPO_SUCURSAL })
        };
    }
}

}