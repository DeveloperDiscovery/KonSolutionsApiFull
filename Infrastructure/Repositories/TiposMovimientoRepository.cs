using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class TiposMovimientoRepository : ITiposMovimientoRepository
{
    private readonly IDbConnectionFactory _db;
    public TiposMovimientoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TiposMovimiento>> ShowallAsync(string codClaseMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMovimiento>(SP.SCHEMALogistica.TiposMovimiento.Showall, new { COD_CLASE_MOVIMIENTO = codClaseMovimiento }, commandType: CommandType.StoredProcedure);
    }

    public async Task<TiposMovimiento?> ShowbyidAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TiposMovimiento>(SP.SCHEMALogistica.TiposMovimiento.Showbyid, new { COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO = COD_TIPO_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Fila cruda de PROC_LOGISTICA_TIPOS_MOVIMIENTO_SHOWCOMBOX — el SP alias "Id" =
    /// COD_CLASE_MOVIMIENTO (no COD_TIPO_MOVIMIENTO, que es la clave real del combo; probable
    /// inconsistencia del SP). Se lee la columna sin aliasear COD_TIPO_MOVIMIENTO en su lugar,
    /// y "Texto" (alias real de DES_TIPO_MOVIMIENTO) para la descripción.</summary>
    private class TipoMovimientoComboRow
    {
        public string COD_TIPO_MOVIMIENTO { get; set; } = string.Empty;
        public string? Texto { get; set; }
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codClaseMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.TiposMovimiento.Showcombox,
            new { COD_CLASE_MOVIMIENTO = codClaseMovimiento });
    }

    public async Task<IEnumerable<TiposMovimiento>> SearchAsync(TiposMovimientoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TiposMovimiento>(SP.SCHEMALogistica.TiposMovimiento.Search, new { dto.COD_CLASE_MOVIMIENTO, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("DES_TIPO_MOVIMIENTO", dto.DES_TIPO_MOVIMIENTO);
        p.Add("COD_TIPO_ENTIDAD_ASOCIADA", dto.COD_TIPO_ENTIDAD_ASOCIADA);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_TRANSFERENCIA", dto.FLG_TRANSFERENCIA ?? false);
        p.Add("FLG_COMERCIAL", dto.FLG_COMERCIAL ?? false);
        p.Add("FLG_DEVOLUCION", dto.FLG_DEVOLUCION ?? false);
        p.Add("FLG_ANULACION", dto.FLG_ANULACION ?? false);
        p.Add("FLG_CONSIGNACION", dto.FLG_CONSIGNACION ?? false);
        p.Add("FLG_AJUSTE_INVENTARIO", dto.FLG_AJUSTE_INVENTARIO ?? false);
        p.Add("FLG_COMPROBANTE_RELACIONADO", dto.FLG_COMPROBANTE_RELACIONADO ?? false);
        p.Add("FLG_VALOR_INVENTARIO", dto.FLG_VALOR_INVENTARIO ?? false);
        p.Add("FLG_CONSIGNA_PROYECTO", dto.FLG_CONSIGNA_PROYECTO ?? false);
        p.Add("FLG_CONSIGNA_CENTRO_COSTO", dto.FLG_CONSIGNA_CENTRO_COSTO ?? false);
        p.Add("FLG_CONSIGNA_LOTE_CONTROL", dto.FLG_CONSIGNA_LOTE_CONTROL ?? false);
        p.Add("FLG_DESTINO_AUTOMATICO", dto.FLG_DESTINO_AUTOMATICO ?? false);
        p.Add("FLG_REQUIERE_CONFIRMACION_RECEPCION", dto.FLG_REQUIERE_CONFIRMACION_RECEPCION ?? false);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("FLG_REQUERIMIENTO", dto.FLG_REQUERIMIENTO ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        // Los dos del alcance de la transferencia. Con ?? false, igual que el resto: la
        // columna admite NULL pero un flag en NULL es un "no se sabe" que despues obliga a
        // preguntar por tres estados en cada lugar que lo lea.
        p.Add("FLG_TRANSFERENCIA_SUCURSALES_EMPRESA", dto.FLG_TRANSFERENCIA_SUCURSALES_EMPRESA ?? false);
        p.Add("FLG_TRANSFERENCIA_ALMACENES_SUCURSAL", dto.FLG_TRANSFERENCIA_ALMACENES_SUCURSAL ?? false);
        p.Add("FLG_TRANSFERENCIA_UBICACIONES_ALMACEN", dto.FLG_TRANSFERENCIA_UBICACIONES_ALMACEN ?? false);
        p.Add("COD_TIPO_MOVIMIENTO_SINCRONIZADO", dto.COD_TIPO_MOVIMIENTO_SINCRONIZADO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO_RELACIONADO", dto.COD_TIPO_ESTADO_RELACIONADO);
        p.Add("COD_TIPO_MOTIVO_DESTINO", dto.COD_TIPO_MOTIVO_DESTINO);
        p.Add("COD_MOTIVO_DEFECTO", dto.COD_MOTIVO_DEFECTO);
        // DbType.Binary explicito: sin el, Dapper manda el byte[] como varbinary(8000) y una
        // imagen mas grande se corta sin avisar.
        p.Add("IMG_ICONO", dto.IMG_ICONO, DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        // Requisito de lectura (N / O / R). Vacio viaja como NULL: el SP de alta lo toma como N y el de
        // modificacion deja el que estaba, asi un llamado que no lo manda no pisa lo configurado.
        p.Add("COD_REQUISITO_LECTURA", string.IsNullOrWhiteSpace(dto.COD_REQUISITO_LECTURA) ? null : dto.COD_REQUISITO_LECTURA.Trim().ToUpperInvariant());
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposMovimiento.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposMovimiento.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposMovimiento.Insert, p)
        };
    }
}

    /// <summary>Modificacion. Usa el MISMO DTO que el alta: el SP de update recibe los mismos
    /// parametros, y tener dos clases identicas solo garantiza que algun dia una quede sin un
    /// campo que la otra si tiene.</summary>
    public async Task<WriteResult> UpdateAsync(TiposMovimientoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("DES_TIPO_MOVIMIENTO", dto.DES_TIPO_MOVIMIENTO);
        p.Add("COD_TIPO_ENTIDAD_ASOCIADA", dto.COD_TIPO_ENTIDAD_ASOCIADA);
        p.Add("FLG_POSITIVO", dto.FLG_POSITIVO ?? false);
        p.Add("FLG_TRANSFERENCIA", dto.FLG_TRANSFERENCIA ?? false);
        p.Add("FLG_COMERCIAL", dto.FLG_COMERCIAL ?? false);
        p.Add("FLG_DEVOLUCION", dto.FLG_DEVOLUCION ?? false);
        p.Add("FLG_ANULACION", dto.FLG_ANULACION ?? false);
        p.Add("FLG_CONSIGNACION", dto.FLG_CONSIGNACION ?? false);
        p.Add("FLG_AJUSTE_INVENTARIO", dto.FLG_AJUSTE_INVENTARIO ?? false);
        p.Add("FLG_COMPROBANTE_RELACIONADO", dto.FLG_COMPROBANTE_RELACIONADO ?? false);
        p.Add("FLG_VALOR_INVENTARIO", dto.FLG_VALOR_INVENTARIO ?? false);
        p.Add("FLG_CONSIGNA_PROYECTO", dto.FLG_CONSIGNA_PROYECTO ?? false);
        p.Add("FLG_CONSIGNA_CENTRO_COSTO", dto.FLG_CONSIGNA_CENTRO_COSTO ?? false);
        p.Add("FLG_CONSIGNA_LOTE_CONTROL", dto.FLG_CONSIGNA_LOTE_CONTROL ?? false);
        p.Add("FLG_DESTINO_AUTOMATICO", dto.FLG_DESTINO_AUTOMATICO ?? false);
        p.Add("FLG_REQUIERE_CONFIRMACION_RECEPCION", dto.FLG_REQUIERE_CONFIRMACION_RECEPCION ?? false);
        p.Add("FLG_CONTROL_CALIDAD", dto.FLG_CONTROL_CALIDAD ?? false);
        p.Add("FLG_REQUERIMIENTO", dto.FLG_REQUERIMIENTO ?? false);
        p.Add("FLG_PRODUCCION", dto.FLG_PRODUCCION ?? false);
        // Los dos del alcance de la transferencia. Con ?? false, igual que el resto: la
        // columna admite NULL pero un flag en NULL es un "no se sabe" que despues obliga a
        // preguntar por tres estados en cada lugar que lo lea.
        p.Add("FLG_TRANSFERENCIA_SUCURSALES_EMPRESA", dto.FLG_TRANSFERENCIA_SUCURSALES_EMPRESA ?? false);
        p.Add("FLG_TRANSFERENCIA_ALMACENES_SUCURSAL", dto.FLG_TRANSFERENCIA_ALMACENES_SUCURSAL ?? false);
        p.Add("FLG_TRANSFERENCIA_UBICACIONES_ALMACEN", dto.FLG_TRANSFERENCIA_UBICACIONES_ALMACEN ?? false);
        p.Add("COD_TIPO_MOVIMIENTO_SINCRONIZADO", dto.COD_TIPO_MOVIMIENTO_SINCRONIZADO);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO_RELACIONADO", dto.COD_TIPO_ESTADO_RELACIONADO);
        p.Add("COD_TIPO_MOTIVO_DESTINO", dto.COD_TIPO_MOTIVO_DESTINO);
        p.Add("COD_MOTIVO_DEFECTO", dto.COD_MOTIVO_DEFECTO);
        // DbType.Binary explicito: sin el, Dapper manda el byte[] como varbinary(8000) y una
        // imagen mas grande se corta sin avisar.
        p.Add("IMG_ICONO", dto.IMG_ICONO, DbType.Binary);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        // Requisito de lectura (N / O / R). Vacio viaja como NULL: el SP de alta lo toma como N y el de
        // modificacion deja el que estaba, asi un llamado que no lo manda no pisa lo configurado.
        p.Add("COD_REQUISITO_LECTURA", string.IsNullOrWhiteSpace(dto.COD_REQUISITO_LECTURA) ? null : dto.COD_REQUISITO_LECTURA.Trim().ToUpperInvariant());
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposMovimiento.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposMovimiento.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposMovimiento.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_CLASE_MOVIMIENTO, string COD_TIPO_MOVIMIENTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.TiposMovimiento.Delete, new { COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO = COD_TIPO_MOVIMIENTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.TiposMovimiento.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.TiposMovimiento.Delete, new { COD_CLASE_MOVIMIENTO = COD_CLASE_MOVIMIENTO, COD_TIPO_MOVIMIENTO = COD_TIPO_MOVIMIENTO })
        };
    }
}

    /// <summary>PROC_LOGISTICA_TIPOS_MOVIMIENTO_LOADCOMBOS: TiposEntidades + TiposMotivos +
    /// TiposEstados (ya venían con 3 llamadas HTTP secuenciales) + Estado (COD_ESTADO/
    /// DES_ESTADO/colores) en un solo viaje. Si codTipoEstado &lt;= 0, se resuelve el de
    /// LOGISTICA.TIPOS_MOVIMIENTO automáticamente (mismo mecanismo que
    /// EstadosPorTablaController), para que el cliente no tenga que saberlo.
    /// CargarMotivos() (cascada real por COD_TIPO_MOTIVO_DESTINO) NO forma parte de este
    /// bundle: sigue siendo una llamada aparte, disparada al elegir Tipo de Motivo Destino.</summary>
    public async Task<TiposMovimientoLoadCombosResult> LoadcombosAsync(int codTipoEstado)
    {
        using var conn = _db.CreateConnection();

        if (codTipoEstado <= 0)
        {
            var tabla = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
                "PROC_ESTANDAR_TABLAS_TIPOS_ESTADO_SHOWBYID",
                new { DES_TABLA = "LOGISTICA.TIPOS_MOVIMIENTO" },
                commandType: CommandType.StoredProcedure);
            codTipoEstado = tabla?.COD_TIPO_ESTADO ?? 0;
        }

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.TiposMovimiento.Loadcombos,
            new { COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        var tiposEntidades = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposMotivos    = MapCombo(await multi.ReadAsync<dynamic>());
        var tiposEstados    = MapCombo(await multi.ReadAsync<dynamic>());
        var estadosFilas    = await multi.ReadAsync<dynamic>();

        var estados = MapEstados(estadosFilas);

        return new TiposMovimientoLoadCombosResult
        {
            CodTipoEstado = codTipoEstado,
            TiposEntidades = tiposEntidades,
            TiposMotivos = tiposMotivos,
            TiposEstados = tiposEstados,
            Estados = estados
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código
    /// (primera COD_*) y de descripción (primera DES_*/NOMBRE_*) — nunca busca columnas
    /// literales "Id"/"Texto".</summary>
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
                Id = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    private static List<EstadoItem> MapEstados(IEnumerable<dynamic> filas) => filas.Select(f =>
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

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }

}