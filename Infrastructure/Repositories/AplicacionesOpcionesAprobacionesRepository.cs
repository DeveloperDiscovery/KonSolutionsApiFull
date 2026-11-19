using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class AplicacionesOpcionesAprobacionesRepository : IAplicacionesOpcionesAprobacionesRepository
{
    private readonly IDbConnectionFactory _db;
    public AplicacionesOpcionesAprobacionesRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<AplicacionesOpcionesAprobaciones>> ShowallAsync(string codAplicacion, string codOpcion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpcionesAprobaciones>(
            SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Showall,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<AplicacionesOpcionesAprobaciones?> ShowbyidAsync(string codAplicacion, string codOpcion, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<AplicacionesOpcionesAprobaciones>(
            SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Showbyid,
            new { COD_APLICACION = codAplicacion, COD_OPCION_APLICACION = codOpcion, NUM_SECUENCIA = numSecuencia },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AplicacionesOpcionesAprobaciones>> SearchAsync(AplicacionesOpcionesAprobacionesSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpcionesAprobaciones>(
            SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Search,
            new { COD_APLICACION = dto.COD_APLICACION, COD_OPCION_APLICACION = dto.COD_OPCION_APLICACION, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AplicacionesOpcionesAprobacionesResumen>> ResumenAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<AplicacionesOpcionesAprobacionesResumen>(
            SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.ShowallResumen,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los campos editables van juntos porque INSERT y UPDATE reciben exactamente los
    /// mismos parámetros.
    ///
    /// Las cuatro banderas se mandan siempre con valor: la columna admite NULL, y un NULL en
    /// FLG_APROBADOR no es "no aprueba" sino "no se sabe", que después hay que interpretar en
    /// cada consulta.
    ///
    /// Los montos y la moneda sí viajan como NULL cuando no se indican: ahí el NULL significa
    /// algo distinto —"este nivel no tiene tope"— y aplanarlo a cero diría lo contrario, que
    /// aprueba hasta cero.</summary>
    private static DynamicParameters Parametros(
        string codAplicacion, string codOpcion, int? numSecuencia,
        int? codArea, int? codCargo, string? codMoneda, decimal? montoMin, decimal? montoMax,
        bool? habilitado, bool? aprobador, bool? notificado, bool? ultimo, int? orden,
        string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", codAplicacion);
        p.Add("COD_OPCION_APLICACION", codOpcion);
        p.Add("NUM_SECUENCIA", numSecuencia ?? 0);
        p.Add("COD_AREA", codArea);
        p.Add("COD_CARGO", codCargo);
        p.Add("COD_MONEDA", string.IsNullOrWhiteSpace(codMoneda) ? null : codMoneda);
        p.Add("VAL_MONTO_MINIMO", montoMin);
        p.Add("VAL_MONTO_MAXIMO", montoMax);
        p.Add("FLG_HABILITADO", habilitado ?? false);
        p.Add("FLG_APROBADOR", aprobador ?? false);
        p.Add("FLG_NOTIFICADO", notificado ?? false);
        p.Add("FLG_APROBADOR_ULTIMO", ultimo ?? false);
        p.Add("NUM_ORDEN_APROBACION", orden);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    public async Task<WriteResult> InsertAsync(AplicacionesOpcionesAprobacionesInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        // NUM_SECUENCIA va en 0: la numeración la resuelve el SP. Además 0 nunca coincide con
        // una fila existente, así que el UPDATE previo del SP (que ahora es un upsert) no
        // engancha nada y siempre entra por la rama de alta, que es lo que se quiere acá.
        var p = Parametros(dto.COD_APLICACION, dto.COD_OPCION_APLICACION, 0,
            dto.COD_AREA, dto.COD_CARGO, dto.COD_MONEDA, dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO,
            dto.FLG_HABILITADO, dto.FLG_APROBADOR, dto.FLG_NOTIFICADO,
            dto.FLG_APROBADOR_ULTIMO, dto.NUM_ORDEN_APROBACION, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(AplicacionesOpcionesAprobacionesUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto.COD_APLICACION, dto.COD_OPCION_APLICACION, dto.NUM_SECUENCIA,
            dto.COD_AREA, dto.COD_CARGO, dto.COD_MONEDA, dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO,
            dto.FLG_HABILITADO, dto.FLG_APROBADOR, dto.FLG_NOTIFICADO,
            dto.FLG_APROBADOR_ULTIMO, dto.NUM_ORDEN_APROBACION, codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(string codAplicacion, string codOpcion, int numSecuencia)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_APLICACION", codAplicacion);
        p.Add("COD_OPCION_APLICACION", codOpcion);
        p.Add("NUM_SECUENCIA", numSecuencia);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEntorno.AplicacionesOpcionesAprobaciones.Delete, p)
            };
        }
    }
}
