using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class CargosFuncionesRepository : ICargosFuncionesRepository
{
    private readonly IDbConnectionFactory _db;
    public CargosFuncionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<CargosFunciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CargosFunciones>(SP.SCHEMARrhh.CargosFunciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<CargosFunciones?> ShowbyidAsync(int COD_CARGO, string COD_FUNCION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<CargosFunciones>(SP.SCHEMARrhh.CargosFunciones.Showbyid, new { COD_CARGO = COD_CARGO, COD_FUNCION = COD_FUNCION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.CargosFunciones.Showcombox);
    }

    public async Task<IEnumerable<CargosFunciones>> SearchAsync(CargosFuncionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<CargosFunciones>(SP.SCHEMARrhh.CargosFunciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(CargosFuncionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_FUNCION", dto.COD_FUNCION);
        p.Add("DES_FUNCION", dto.DES_FUNCION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosFunciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosFunciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosFunciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(CargosFuncionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_FUNCION", dto.COD_FUNCION);
        p.Add("DES_FUNCION", dto.DES_FUNCION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosFunciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosFunciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosFunciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int COD_CARGO, string COD_FUNCION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.CargosFunciones.Delete, new { COD_CARGO = COD_CARGO, COD_FUNCION = COD_FUNCION }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.CargosFunciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.CargosFunciones.Delete, new { COD_CARGO = COD_CARGO, COD_FUNCION = COD_FUNCION })
        };
    }
}

}