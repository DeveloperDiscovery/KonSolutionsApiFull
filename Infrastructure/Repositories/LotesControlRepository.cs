using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class LotesControlRepository : ILotesControlRepository
{
    private readonly IDbConnectionFactory _db;
    public LotesControlRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<LotesControl>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LotesControl>(SP.SCHEMALogistica.LotesControl.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<LotesControl?> ShowbyidAsync(string NUM_LOTE_CONTROL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<LotesControl>(SP.SCHEMALogistica.LotesControl.Showbyid, new { NUM_LOTE_CONTROL = NUM_LOTE_CONTROL }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>NUM_LOTE_CONTROL como columna clave explícita: no empieza con "COD_", así que
    /// el lector genérico por convención tomaba la primera COD_* que encontrara —
    /// COD_TIPO_LOTE_CONTROL, en la práctica— y el combo terminaba mandando ese código como si
    /// fuera el lote. Se veía recién al grabar, como violación de FOREIGN KEY.</summary>
    /// <summary>codEntidad es opcional: el SP lo declara con DEFAULT NULL y, cuando llega,
    /// deja sólo los lotes de esa entidad. En null devuelve todos, como antes.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal? codEntidad = null)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.LotesControl.Showcombox,
            columnaId: "NUM_LOTE_CONTROL", columnaTexto: "DES_LOTE_CONTROL",
            parametros: new { COD_ENTIDAD = codEntidad });
    }

    public async Task<IEnumerable<LotesControl>> SearchAsync(LotesControlSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<LotesControl>(SP.SCHEMALogistica.LotesControl.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>El siguiente número de lote.
    ///
    /// La columna no es IDENTITY —es nvarchar y el procedimiento la recibe como parámetro—,
    /// así que el correlativo hay que calcularlo. Se toma el máximo de los lotes cuyo número
    /// es puramente numérico: si alguna vez se cargaron lotes con código alfanumérico a mano,
    /// no rompen la cuenta ni la contaminan.
    ///
    /// Va como consulta directa y no como procedimiento porque no existe uno para esto. Su
    /// lugar natural es un SP —ahí el cálculo y el alta entran en la misma transacción—; hasta
    /// entonces, dos altas simultáneas pueden calcular el mismo número y el INSERT rechaza la
    /// segunda por clave repetida, que es de lo que se ocupa el reintento en el servicio.</summary>
    public async Task<string> SiguienteNumeroAsync()
    {
        using var conn = _db.CreateConnection();
        var max = await conn.ExecuteScalarAsync<long?>(
            @"SELECT MAX(CONVERT(BIGINT, NUM_LOTE_CONTROL))
              FROM LOGISTICA.LOTES_CONTROL
              WHERE NUM_LOTE_CONTROL NOT LIKE '%[^0-9]%' AND LEN(NUM_LOTE_CONTROL) <= 18");
        return ((max ?? 0) + 1).ToString("D10");
    }

    public async Task<WriteResult> InsertAsync(LotesControlInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("DES_LOTE_CONTROL", dto.DES_LOTE_CONTROL);
        p.Add("FEC_LOTE_CONTROL", dto.FEC_LOTE_CONTROL);
        p.Add("COD_TIPO_LOTE_CONTROL", dto.COD_TIPO_LOTE_CONTROL);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_LOTE_CONTROL_ENTIDAD", dto.NUM_LOTE_CONTROL_ENTIDAD);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.LotesControl.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.LotesControl.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.LotesControl.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(LotesControlUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("DES_LOTE_CONTROL", dto.DES_LOTE_CONTROL);
        p.Add("FEC_LOTE_CONTROL", dto.FEC_LOTE_CONTROL);
        p.Add("COD_TIPO_LOTE_CONTROL", dto.COD_TIPO_LOTE_CONTROL);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("NUM_LOTE_CONTROL_ENTIDAD", dto.NUM_LOTE_CONTROL_ENTIDAD);
        p.Add("VAL_TOTAL", dto.VAL_TOTAL);
        p.Add("FEC_VENCIMIENTO", dto.FEC_VENCIMIENTO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.LotesControl.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.LotesControl.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.LotesControl.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string NUM_LOTE_CONTROL)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.LotesControl.Delete, new { NUM_LOTE_CONTROL = NUM_LOTE_CONTROL }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.LotesControl.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.LotesControl.Delete, new { NUM_LOTE_CONTROL = NUM_LOTE_CONTROL })
        };
    }
}

}
