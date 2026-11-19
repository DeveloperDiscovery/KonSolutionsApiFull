using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Rrhh;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProgramacionEntregaImplementosPersonalDetalleRepository : IProgramacionEntregaImplementosPersonalDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public ProgramacionEntregaImplementosPersonalDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ProgramacionEntregaImplementosPersonalDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionEntregaImplementosPersonalDetalle>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<ProgramacionEntregaImplementosPersonalDetalle?> ShowbyidAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ProgramacionEntregaImplementosPersonalDetalle>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Showbyid, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Showcombox, "NUM_ENTREGA_IMPLEMENTOS", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<ProgramacionEntregaImplementosPersonalDetalle>> SearchAsync(ProgramacionEntregaImplementosPersonalDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ProgramacionEntregaImplementosPersonalDetalle>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProgramacionEntregaImplementosPersonalDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENTREGA_IMPLEMENTOS", dto.NUM_ENTREGA_IMPLEMENTOS);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_PRODUCTO_IMPLEMENTO", dto.COD_PRODUCTO_IMPLEMENTO);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(ProgramacionEntregaImplementosPersonalDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_ENTREGA_IMPLEMENTOS", dto.NUM_ENTREGA_IMPLEMENTOS);
        p.Add("COD_ENTIDAD_PERSONAL", dto.COD_ENTIDAD_PERSONAL);
        p.Add("COD_PRODUCTO_IMPLEMENTO", dto.COD_PRODUCTO_IMPLEMENTO);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_ENTREGA_IMPLEMENTOS, decimal COD_ENTIDAD_PERSONAL, string COD_PRODUCTO_IMPLEMENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Delete, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMARrhh.ProgramacionEntregaImplementosPersonalDetalle.Delete, new { NUM_ENTREGA_IMPLEMENTOS = NUM_ENTREGA_IMPLEMENTOS, COD_ENTIDAD_PERSONAL = COD_ENTIDAD_PERSONAL, COD_PRODUCTO_IMPLEMENTO = COD_PRODUCTO_IMPLEMENTO })
        };
    }
}

}