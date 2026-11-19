using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

// ── Maestro ──────────────────────────────────────────────────────────────────
public class FlujosAprobacionRepository : IFlujosAprobacionRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosAprobacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosAprobacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosAprobacion>(
            SP.SCHEMALogistica.FlujosAprobacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosAprobacion?> ShowbyidAsync(short NUM_FLUJO_APROBACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosAprobacion>(
            SP.SCHEMALogistica.FlujosAprobacion.Showbyid,
            new { NUM_FLUJO_APROBACION }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.FlujosAprobacion.Showcombox);
    }

    public async Task<IEnumerable<FlujosAprobacion>> SearchAsync(FlujosAprobacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosAprobacion>(
            SP.SCHEMALogistica.FlujosAprobacion.Search,
            new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosAprobacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", dto.NUM_FLUJO_APROBACION);
        p.Add("DES_FLUJO_APROBACION", dto.DES_FLUJO_APROBACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacion.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacion.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacion.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(FlujosAprobacionUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", dto.NUM_FLUJO_APROBACION);
        p.Add("DES_FLUJO_APROBACION", dto.DES_FLUJO_APROBACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacion.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacion.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacion.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(short NUM_FLUJO_APROBACION)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", NUM_FLUJO_APROBACION);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacion.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacion.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacion.Delete, p)
            };
        }
    }
}


// ── Detalle: Área / Cargo ────────────────────────────────────────────────────
public class FlujosAprobacionAreaCargoRepository : IFlujosAprobacionAreaCargoRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosAprobacionAreaCargoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosAprobacionAreaCargo>> ShowallAsync(short numFlujo, int? codArea = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosAprobacionAreaCargo>(
            SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Showall,
            new { NUM_FLUJO_APROBACION = numFlujo, COD_AREA = codArea },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosAprobacionAreaCargo?> ShowbyidAsync(short numFlujo, int codArea, int codCargo)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosAprobacionAreaCargo>(
            SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Showbyid,
            new { NUM_FLUJO_APROBACION = numFlujo, COD_AREA = codArea, COD_CARGO = codCargo },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FlujosAprobacionAreaCargo>> SearchAsync(FlujosAprobacionAreaCargoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosAprobacionAreaCargo>(
            SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Search,
            new { NUM_FLUJO_APROBACION = dto.NUM_FLUJO_APROBACION, COD_AREA = dto.COD_AREA, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters Parametros(short? numFlujo, int? codArea, int? codCargo,
        int? numOrden, string? codMoneda, decimal? montoMin, decimal? montoMax,
        bool? flgUltimo, bool? flgHabilitado, string codUsuario, string codEstacion)
    {
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", numFlujo);
        p.Add("COD_AREA", codArea);
        p.Add("COD_CARGO", codCargo);
        p.Add("NUM_ORDEN_APROBACION", numOrden);
        p.Add("COD_MONEDA", codMoneda);
        p.Add("VAL_MONTO_MINIMO", montoMin);
        p.Add("VAL_MONTO_MAXIMO", montoMax);
        p.Add("FLG_ULTIMO", flgUltimo ?? false);
        p.Add("FLG_HABILITADO", flgHabilitado ?? false);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        return p;
    }

    public async Task<WriteResult> InsertAsync(FlujosAprobacionAreaCargoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto.NUM_FLUJO_APROBACION, dto.COD_AREA, dto.COD_CARGO, dto.NUM_ORDEN_APROBACION,
            dto.COD_MONEDA, dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO, dto.FLG_ULTIMO, dto.FLG_HABILITADO,
            codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Insert, p)
            };
        }
    }

    public async Task<WriteResult> UpdateAsync(FlujosAprobacionAreaCargoUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = Parametros(dto.NUM_FLUJO_APROBACION, dto.COD_AREA, dto.COD_CARGO, dto.NUM_ORDEN_APROBACION,
            dto.COD_MONEDA, dto.VAL_MONTO_MINIMO, dto.VAL_MONTO_MAXIMO, dto.FLG_ULTIMO, dto.FLG_HABILITADO,
            codUsuario, codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Update, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(short numFlujo, int codArea, int codCargo)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", numFlujo);
        p.Add("COD_AREA", codArea);
        p.Add("COD_CARGO", codCargo);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Delete, p)
            };
        }
    }

    /// <summary>La copia entera va en un solo SP, dentro de una transacción: hacerla fila por
    /// fila desde acá podría dejar el flujo destino a medio copiar si algo falla.</summary>
    public async Task<WriteResult> CopyAsync(FlujosAprobacionAreaCargoCopyDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", dto.NUM_FLUJO_APROBACION);
        p.Add("NUM_FLUJO_APROBACION_DESTINO", dto.NUM_FLUJO_APROBACION_DESTINO);
        p.Add("FLG_REEMPLAZAR", dto.FLG_REEMPLAZAR);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Copy, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 0 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Copy,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacionAreaCargo.Copy, p)
            };
        }
    }
}


// ── Detalle: Tipos de Operación ──────────────────────────────────────────────
public class FlujosAprobacionTiposOperacionRepository : IFlujosAprobacionTiposOperacionRepository
{
    private readonly IDbConnectionFactory _db;
    public FlujosAprobacionTiposOperacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<FlujosAprobacionTiposOperacion>> ShowallAsync(short numFlujo, string? codClaseOperacion = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosAprobacionTiposOperacion>(
            SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Showall,
            new { NUM_FLUJO_APROBACION = numFlujo, COD_CLASE_OPERACION = codClaseOperacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<FlujosAprobacionTiposOperacion?> ShowbyidAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FlujosAprobacionTiposOperacion>(
            SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Showbyid,
            new { NUM_FLUJO_APROBACION = numFlujo, COD_CLASE_OPERACION = codClaseOperacion, COD_TIPO_OPERACION = codTipoOperacion },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FlujosAprobacionTiposOperacion>> SearchAsync(FlujosAprobacionTiposOperacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<FlujosAprobacionTiposOperacion>(
            SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Search,
            new { NUM_FLUJO_APROBACION = dto.NUM_FLUJO_APROBACION, COD_CLASE_OPERACION = dto.COD_CLASE_OPERACION, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(FlujosAprobacionTiposOperacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", dto.NUM_FLUJO_APROBACION);
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Insert, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Insert,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Insert, p)
            };
        }
    }

    public async Task<WriteResult> DeleteAsync(short numFlujo, string codClaseOperacion, string codTipoOperacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_FLUJO_APROBACION", numFlujo);
        p.Add("COD_CLASE_OPERACION", codClaseOperacion);
        p.Add("COD_TIPO_OPERACION", codTipoOperacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Delete, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Delete,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.FlujosAprobacionTiposOperacion.Delete, p)
            };
        }
    }
}
