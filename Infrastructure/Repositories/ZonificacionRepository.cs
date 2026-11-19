using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ZonificacionRepository : IZonificacionRepository
{
    private readonly IDbConnectionFactory _db;
    public ZonificacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<Zonificacion>> ShowallAsync(string COD_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Zonificacion>(
            SP.SCHEMAZonificacion.Zonificacion.Showall,
            new { COD_SUCURSAL = COD_SUCURSAL }, commandType: CommandType.StoredProcedure);
    }

    public async Task<Zonificacion?> ShowbyidAsync(string COD_SUCURSAL, string COD_ZONIFICACION)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Zonificacion>(
            SP.SCHEMAZonificacion.Zonificacion.Showbyid,
            new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION },
            commandType: CommandType.StoredProcedure);
    }

    // El SP _SHOWCOMBOX devuelve las columnas de la tabla (COD_*/DES_*), no "Id"/"Texto":
    // la traducción a ComboboxItem se hace acá, en el repositorio.
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_SUCURSAL)
    {
        using var conn = _db.CreateConnection();
        // COLUMNAS EXPLÍCITAS, NO POR CONVENCIÓN.
        // El SHOWCOMBOX devuelve COD_SUCURSAL antes que COD_ZONIFICACION, así que el lector
        // genérico —que toma la PRIMERA COD_*— estaba usando la sucursal como identificador:
        // los cinco niveles salían con el mismo id "00001" y distinta descripción.
        // No se nota al abrir el combo, porque la sucursal también tiene datos; se nota al
        // grabar, como una violación de FK contra una zonificación que no existe. Es el
        // caso que el propio ComboboxReader documenta.
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAZonificacion.Zonificacion.Showcombox,
            "COD_ZONIFICACION", "DES_ZONIFICACION",
            new { COD_SUCURSAL = COD_SUCURSAL });
    }

    public async Task<IEnumerable<Zonificacion>> SearchAsync(ZonificacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<Zonificacion>(
            SP.SCHEMAZonificacion.Zonificacion.Search,
            new { COD_SUCURSAL = dto.COD_SUCURSAL, SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ZonificacionInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_SUCURSAL", dto.COD_SUCURSAL);
        // Vacío al crear: el SP arma el código como PADRE + correlativo(3). Con valor, actualiza.
        p.Add("COD_ZONIFICACION", dto.COD_ZONIFICACION ?? string.Empty);
        p.Add("DES_ZONIFICACION", dto.DES_ZONIFICACION);
        p.Add("COD_ZONIFICACION_PADRE", dto.COD_ZONIFICACION_PADRE ?? string.Empty);
        p.Add("COD_TIPO_ZONIFICACION", dto.COD_TIPO_ZONIFICACION);
        p.Add("NUM_NIVEL", dto.NUM_NIVEL);
        p.Add("NUM_NIVEL_ORDEN", dto.NUM_NIVEL_ORDEN);
        p.Add("VAL_ANCHO_METROS", dto.VAL_ANCHO_METROS);
        p.Add("VAL_LARGO_METROS", dto.VAL_LARGO_METROS);
        p.Add("FLG_MOSTRAR_DESCENDENTE", dto.FLG_MOSTRAR_DESCENDENTE ?? false);
        // Explícito como binario: sin el DbType, Dapper manda byte[] nulo como string y el SP falla.
        p.Add("IMG_PLANO", dto.IMG_PLANO, DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAZonificacion.Zonificacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.Zonificacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.Zonificacion.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_SUCURSAL, string COD_ZONIFICACION)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAZonificacion.Zonificacion.Delete,
            new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAZonificacion.Zonificacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAZonificacion.Zonificacion.Delete, new { COD_SUCURSAL = COD_SUCURSAL, COD_ZONIFICACION = COD_ZONIFICACION })
        };
    }
}

    /// <summary>PROC_ZONIFICACION_ZONIFICACION_LOADCOMBOS: tipos de zonificación + zonas de la
    /// sucursal (combo "Zona padre") + estados en un solo viaje. Si codTipoEstado &lt;= 0, se
    /// resuelve el de ZONIFICACION.ZONIFICACION automáticamente, igual que
    /// PROC_ADMINISTRACION_AREAS_LOADCOMBOS.</summary>
    public async Task<ZonificacionLoadCombosResult> LoadcombosAsync(string codSucursal, int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "ZONIFICACION.ZONIFICACION" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAZonificacion.Zonificacion.Loadcombos,
            new { COD_SUCURSAL = codSucursal, COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tipos = MapCombo(await multi.ReadAsync<dynamic>());
        var zonificaciones = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas = await multi.ReadAsync<dynamic>();

        var estados = estadosFilas.Select(f =>
        {
            var d = (IDictionary<string, object>)f;
            var codStr = d.TryGetValue("COD_ESTADO", out var id) ? id?.ToString() ?? "" : "";
            return new EstadoItem
            {
                CodEstado = int.TryParse(codStr, out var n) ? n : 0,
                DesEstado = d.TryGetValue("DES_ESTADO", out var tx) ? tx?.ToString() ?? "" : "",
                BackColor = d.TryGetValue("DES_BACKCOLOR", out var bc) ? bc?.ToString() ?? "" : "",
                ForeColor = d.TryGetValue("DES_FORECOLOR", out var fc) ? fc?.ToString() ?? "" : "",
                EsDefault = d.TryGetValue("FLG_DEFAULT", out var df) && df is not null && Convert.ToBoolean(df)
            };
        }).ToList();

        return new ZonificacionLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposZonificacion = tipos,
            Zonificaciones = zonificaciones,
            Estados = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem por convención COD_*/DES_*/NOMBRE_* — sin
    /// reconocer alias literales "Id"/"Texto" (mismo criterio que ComboboxReader).</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase) || k.StartsWith("NOMBRE_", StringComparison.OrdinalIgnoreCase));
            resultado.Add(new ComboboxItem
            {
                Id    = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }
}
