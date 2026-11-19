using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ManualesFuncionesRepository : IManualesFuncionesRepository
{
    private readonly IDbConnectionFactory _db;
    public ManualesFuncionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ManualesFunciones>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ManualesFunciones>(SP.SCHEMARrhh.ManualesFunciones.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ManualesFunciones?> ShowbyidAsync(int NUM_MANUAL_FUNCIONES)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ManualesFunciones>(SP.SCHEMARrhh.ManualesFunciones.Showbyid, new { NUM_MANUAL_FUNCIONES = NUM_MANUAL_FUNCIONES }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.ManualesFunciones.Showcombox, "NUM_MANUAL_FUNCIONES", "DES_MANUAL_FUNCIONES");
    }

    public async Task<IEnumerable<ManualesFunciones>> SearchAsync(ManualesFuncionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ManualesFunciones>(SP.SCHEMARrhh.ManualesFunciones.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ManualesFuncionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANUAL_FUNCIONES", dto.NUM_MANUAL_FUNCIONES);
        p.Add("DES_MANUAL_FUNCIONES", dto.DES_MANUAL_FUNCIONES);
        p.Add("DES_TEXTO", dto.DES_TEXTO);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ManualesFunciones.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ManualesFunciones.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ManualesFunciones.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ManualesFuncionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MANUAL_FUNCIONES", dto.NUM_MANUAL_FUNCIONES);
        p.Add("DES_MANUAL_FUNCIONES", dto.DES_MANUAL_FUNCIONES);
        p.Add("DES_TEXTO", dto.DES_TEXTO);
        p.Add("COD_CARGO", dto.COD_CARGO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ManualesFunciones.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ManualesFunciones.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ManualesFunciones.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_MANUAL_FUNCIONES)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ManualesFunciones.Delete, new { NUM_MANUAL_FUNCIONES = NUM_MANUAL_FUNCIONES }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ManualesFunciones.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ManualesFunciones.Delete, new { NUM_MANUAL_FUNCIONES = NUM_MANUAL_FUNCIONES })
        };
    }
}

}