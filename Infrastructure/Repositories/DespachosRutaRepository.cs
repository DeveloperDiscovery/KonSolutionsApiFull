using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class DespachosRutaRepository : IDespachosRutaRepository
{
    private readonly IDbConnectionFactory _db;
    public DespachosRutaRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<DespachosRuta>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DespachosRuta>(SP.SCHEMALogistica.DespachosRuta.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<DespachosRuta?> ShowbyidAsync(int NUM_DESPACHO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DespachosRuta>(SP.SCHEMALogistica.DespachosRuta.Showbyid, new { NUM_DESPACHO = NUM_DESPACHO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.DespachosRuta.Showcombox, "NUM_DESPACHO", "DES_OBSERVACION");
    }

    public async Task<IEnumerable<DespachosRuta>> SearchAsync(DespachosRutaSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DespachosRuta>(SP.SCHEMALogistica.DespachosRuta.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(DespachosRutaInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DESPACHO", dto.NUM_DESPACHO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("VAL_LATITUD_ORIGEN", dto.VAL_LATITUD_ORIGEN);
        p.Add("VAL_LONGITUD_ORIGEN", dto.VAL_LONGITUD_ORIGEN);
        p.Add("VAL_LATITUD_DESTINO", dto.VAL_LATITUD_DESTINO);
        p.Add("VAL_LONGITUD_DESTINO", dto.VAL_LONGITUD_DESTINO);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.DespachosRuta.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.DespachosRuta.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.DespachosRuta.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(DespachosRutaUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_DESPACHO", dto.NUM_DESPACHO);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("VAL_LATITUD_ORIGEN", dto.VAL_LATITUD_ORIGEN);
        p.Add("VAL_LONGITUD_ORIGEN", dto.VAL_LONGITUD_ORIGEN);
        p.Add("VAL_LATITUD_DESTINO", dto.VAL_LATITUD_DESTINO);
        p.Add("VAL_LONGITUD_DESTINO", dto.VAL_LONGITUD_DESTINO);
        p.Add("FEC_ENTREGA", dto.FEC_ENTREGA);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.DespachosRuta.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.DespachosRuta.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.DespachosRuta.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_DESPACHO, int NUM_SECUENCIA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.DespachosRuta.Delete, new { NUM_DESPACHO = NUM_DESPACHO, NUM_SECUENCIA = NUM_SECUENCIA }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.DespachosRuta.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.DespachosRuta.Delete, new { NUM_DESPACHO = NUM_DESPACHO, NUM_SECUENCIA = NUM_SECUENCIA })
        };
    }
}

}