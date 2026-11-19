using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Contabilidad;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MovimientosContablesDetalleRepository : IMovimientosContablesDetalleRepository
{
    private readonly IDbConnectionFactory _db;
    public MovimientosContablesDetalleRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<MovimientosContablesDetalle>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosContablesDetalle>(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosContablesDetalle?> ShowbyidAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosContablesDetalle>(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Showbyid, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE = NUM_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAContabilidad.MovimientosContablesDetalle.Showcombox);
    }

    public async Task<IEnumerable<MovimientosContablesDetalle>> SearchAsync(MovimientosContablesDetalleSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosContablesDetalle>(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosContablesDetalleInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO);
        p.Add("FEC_VENCMIENTO", dto.FEC_VENCMIENTO);
        p.Add("COD_ENTIDAD_CONTABLE", dto.COD_ENTIDAD_CONTABLE);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("VAL_IMPORTE_CONTABLE", dto.VAL_IMPORTE_CONTABLE);
        p.Add("VAL_IMPORTE_CONTABLE_ME", dto.VAL_IMPORTE_CONTABLE_ME);
        p.Add("DES_GLOSA_CONTABLE_DETALLE", dto.DES_GLOSA_CONTABLE_DETALLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.MovimientosContablesDetalle.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MovimientosContablesDetalleUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_PERIODO_CONTABLE", dto.NUM_PERIODO_CONTABLE);
        p.Add("COD_ASIENTO_CONTABLE", dto.COD_ASIENTO_CONTABLE);
        p.Add("NUM_ASIENTO_CONTABLE", dto.NUM_ASIENTO_CONTABLE);
        p.Add("NUM_SECUENCIA", dto.NUM_SECUENCIA);
        p.Add("COD_CUENTA_CONTABLE", dto.COD_CUENTA_CONTABLE);
        p.Add("COD_CENTRO_COSTO", dto.COD_CENTRO_COSTO);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", dto.COD_TIPO_COMPROBANTE_PAGO);
        p.Add("NUM_COMPROBANTE_PAGO", dto.NUM_COMPROBANTE_PAGO);
        p.Add("FEC_COMPROBANTE_PAGO", dto.FEC_COMPROBANTE_PAGO);
        p.Add("FEC_VENCMIENTO", dto.FEC_VENCMIENTO);
        p.Add("COD_ENTIDAD_CONTABLE", dto.COD_ENTIDAD_CONTABLE);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("COD_MONEDA", dto.COD_MONEDA);
        p.Add("FAC_TIPO_CAMBIO", dto.FAC_TIPO_CAMBIO);
        p.Add("COD_CUENTA_NATURALEZA", dto.COD_CUENTA_NATURALEZA);
        p.Add("VAL_IMPORTE_CONTABLE", dto.VAL_IMPORTE_CONTABLE);
        p.Add("VAL_IMPORTE_CONTABLE_ME", dto.VAL_IMPORTE_CONTABLE_ME);
        p.Add("DES_GLOSA_CONTABLE_DETALLE", dto.DES_GLOSA_CONTABLE_DETALLE);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.MovimientosContablesDetalle.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_PERIODO_CONTABLE, string COD_ASIENTO_CONTABLE, int NUM_ASIENTO_CONTABLE)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE = NUM_ASIENTO_CONTABLE }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAContabilidad.MovimientosContablesDetalle.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAContabilidad.MovimientosContablesDetalle.Delete, new { NUM_PERIODO_CONTABLE = NUM_PERIODO_CONTABLE, COD_ASIENTO_CONTABLE = COD_ASIENTO_CONTABLE, NUM_ASIENTO_CONTABLE = NUM_ASIENTO_CONTABLE })
        };
    }
}

}