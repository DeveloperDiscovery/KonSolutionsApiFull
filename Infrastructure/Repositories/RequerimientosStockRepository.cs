using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class RequerimientosStockRepository : IRequerimientosStockRepository
{
    private readonly IDbConnectionFactory _db;
    public RequerimientosStockRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Un combo vacío manda "" y "" NO es lo mismo que NULL: el SP entiende NULL como
    /// "no filtres por esto", mientras que "" filtraría por un código que no existe y devolvería
    /// cero filas.
    ///
    /// Y el parámetro sólo se agrega si trae valor. Un parámetro que no se manda es un parámetro
    /// que no puede romper nada: si el script de SQL todavía no se corrió en esa base, el listado
    /// sin filtro sigue funcionando igual que siempre en vez de reventar con "has too many
    /// arguments specified" — que es exactamente lo que nos pasó con PRODUCTOS_SHOWALL.</summary>
    private static void AgregarSiTieneValor(DynamicParameters p, string nombre, string? valor)
    {
        if (!string.IsNullOrWhiteSpace(valor)) p.Add(nombre, valor);
    }

    public async Task<IEnumerable<RequerimientosStock>> ShowallAsync(RequerimientosStockSearchDto? filtros = null)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        AgregarSiTieneValor(p, "COD_TIPO_REQUERIMIENTO", filtros?.COD_TIPO_REQUERIMIENTO);
        return await conn.QueryAsync<RequerimientosStock>(SP.SCHEMALogistica.RequerimientosStock.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<RequerimientosStock?> ShowbyidAsync(int NUM_REQUERIMIENTO_STOCK)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<RequerimientosStock>(SP.SCHEMALogistica.RequerimientosStock.Showbyid, new { NUM_REQUERIMIENTO_STOCK = NUM_REQUERIMIENTO_STOCK }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        // Columnas explícitas: la clave es NUM_REQUERIMIENTO_STOCK y NO empieza con "COD_",
        // así que el lector por convención tomaba la primera COD_* de la fila
        // —COD_TIPO_REQUERIMIENTO— como si fuera el identificador. El combo quedaba con el
        // código del tipo en el Id: el valor guardado no coincidía con ningún ítem y el campo
        // se veía vacío (ver Reserva N° 1, que sí tenía NUM_REQUERIMIENTO_STOCK = 1).
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.RequerimientosStock.Showcombox,
            columnaId: "NUM_REQUERIMIENTO_STOCK", columnaTexto: "DES_REQUERIMIENTO_STOCK");
    }

    public async Task<IEnumerable<RequerimientosStock>> SearchAsync(RequerimientosStockSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("SEARCH", dto.Search);
        AgregarSiTieneValor(p, "COD_TIPO_REQUERIMIENTO", dto.COD_TIPO_REQUERIMIENTO);
        return await conn.QueryAsync<RequerimientosStock>(SP.SCHEMALogistica.RequerimientosStock.Search, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(RequerimientosStockInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        // El SP de alta también lo exige, sin default. No es un campo del formulario: si no
        // viene, cae al número recién asignado — mismo criterio que ya usa UpdateAsync.
        p.Add("NUM_REQUERIMIENTO_STOCK_ORIGINAL",
              dto.NUM_REQUERIMIENTO_STOCK_ORIGINAL ?? dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("DES_REQUERIMIENTO_STOCK", dto.DES_REQUERIMIENTO_STOCK);
        p.Add("FEC_REQUERIMIENTO_STOCK", dto.FEC_REQUERIMIENTO_STOCK);
        p.Add("COD_TIPO_REQUERIMIENTO", dto.COD_TIPO_REQUERIMIENTO);
        p.Add("COD_CENTRO_COSTO_REQUIRIENTE", dto.COD_CENTRO_COSTO_REQUIRIENTE);
        p.Add("COD_ENTIDAD_REQUIRIENTE", dto.COD_ENTIDAD_REQUIRIENTE);
        p.Add("COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL", dto.COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL);
        p.Add("COD_ENTIDAD_REQUIRIENTE_ORIGINAL", dto.COD_ENTIDAD_REQUIRIENTE_ORIGINAL);
        p.Add("FEC_ATENCION", dto.FEC_ATENCION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        // Se lee como fila suelta y no como WriteResult porque el SP devuelve la clave con su
        // propio nombre —NUM_REQUERIMIENTO_STOCK— y Dapper no tiene cómo saber que eso es el Codigo.
        // El mismo resultset sirve para el camino del CATCH, que trae ErrorNumber/ErrorMessage.
        var fila = await conn.QueryFirstOrDefaultAsync(SP.SCHEMALogistica.RequerimientosStock.Insert, p, commandType: CommandType.StoredProcedure);
        return LeerAlta(fila);
    }

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    /// <summary>Traduce el resultset del alta: o trae la clave que generó la base, o trae el
    /// error del CATCH. Sin esto el número se perdía y el llamador no sabía qué documento acababa
    /// de crear — que es justo lo que hace falta para insertarle los ítems.</summary>
    private static WriteResult LeerAlta(dynamic? fila)
    {
        if (fila is null) return new WriteResult { Estado = "OK", AffectedRows = 1 };
        var campos = (IDictionary<string, object?>)fila;

        if (campos.TryGetValue("ErrorNumber", out var num) && num is not null)
            return new WriteResult
            {
                Estado = "ERROR",
                AffectedRows = 0,
                ErrorNumber = Convert.ToInt32(num),
                Mensaje = campos.TryGetValue("ErrorMessage", out var msg) ? msg?.ToString() ?? "" : "",
                ErrorProcedure = campos.TryGetValue("ErrorProcedure", out var sp) ? sp?.ToString() ?? "" : ""
            };

        return new WriteResult
        {
            Estado = "OK",
            AffectedRows = 1,
            Codigo = campos.TryGetValue("NUM_REQUERIMIENTO_STOCK", out var cod) ? cod?.ToString() ?? "" : ""
        };
    }

    public async Task<WriteResult> UpdateAsync(RequerimientosStockUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", dto.NUM_REQUERIMIENTO_STOCK);
        // El SP lo exige: es la clave anterior, igual que los dos _ORIGINAL de abajo. El
        // correlativo no se edita en pantalla, así que si el cliente no lo manda, el número
        // original es el mismo que el actual.
        p.Add("NUM_REQUERIMIENTO_STOCK_ORIGINAL",
              dto.NUM_REQUERIMIENTO_STOCK_ORIGINAL ?? dto.NUM_REQUERIMIENTO_STOCK);
        p.Add("DES_REQUERIMIENTO_STOCK", dto.DES_REQUERIMIENTO_STOCK);
        p.Add("FEC_REQUERIMIENTO_STOCK", dto.FEC_REQUERIMIENTO_STOCK);
        p.Add("COD_TIPO_REQUERIMIENTO", dto.COD_TIPO_REQUERIMIENTO);
        p.Add("COD_CENTRO_COSTO_REQUIRIENTE", dto.COD_CENTRO_COSTO_REQUIRIENTE);
        p.Add("COD_ENTIDAD_REQUIRIENTE", dto.COD_ENTIDAD_REQUIRIENTE);
        p.Add("COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL", dto.COD_CENTRO_COSTO_REQUIRIENTE_ORIGINAL);
        p.Add("COD_ENTIDAD_REQUIRIENTE_ORIGINAL", dto.COD_ENTIDAD_REQUIRIENTE_ORIGINAL);
        p.Add("FEC_ATENCION", dto.FEC_ATENCION);
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.RequerimientosStock.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.RequerimientosStock.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStock.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(int NUM_REQUERIMIENTO_STOCK)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.RequerimientosStock.Delete, new { NUM_REQUERIMIENTO_STOCK = NUM_REQUERIMIENTO_STOCK }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.RequerimientosStock.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStock.Delete, new { NUM_REQUERIMIENTO_STOCK = NUM_REQUERIMIENTO_STOCK })
        };
    }
}

    /// <summary>Botón "Genera Reservas y Req. Compra" (COD_BOTON_OPCION "04"). Recorre el
    /// detalle y arma reservas contra stock disponible; lo que no alcanza a reservar queda
    /// como requerimiento de compra. Mismo contrato WriteResult que Update/Delete: éxito sin
    /// fila, o ErrorNumber/ErrorMessage del CATCH del procedimiento.</summary>
    public async Task<WriteResult> MakereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", numRequerimientoStock);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.RequerimientosStock.Makereserva, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.RequerimientosStock.Makereserva,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStock.Makereserva, p)
            };
        }
    }

    /// <summary>Botón "Revierte Reservas y Req. Compra" (COD_BOTON_OPCION "05"). Deshace lo
    /// que dejó MakereservaAsync sobre el mismo requerimiento.</summary>
    public async Task<WriteResult> ReversereservaAsync(int numRequerimientoStock, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_REQUERIMIENTO_STOCK", numRequerimientoStock);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
                SP.SCHEMALogistica.RequerimientosStock.Reversereserva, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMALogistica.RequerimientosStock.Reversereserva,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.RequerimientosStock.Reversereserva, p)
            };
        }
    }

    /// <summary>PROC_LOGISTICA_REQUERIMIENTOS_STOCK_LOADCOMBOS: Tipos de requerimiento +
    /// Centros de costo + Entidades + Lotes + Productos (los cinco combos independientes de
    /// RequerimientoDocumento.razor) en un solo viaje. Productos se lee tipado —no como combo—
    /// porque el detalle necesita más columnas del maestro (unidad de medida, etc.).</summary>
    public async Task<RequerimientosStockLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.RequerimientosStock.Loadcombos, commandType: CommandType.StoredProcedure);

        // Se lee como entidad y no con MapCombo: MapCombo se queda sólo con código y
        // descripción, y acá hace falta además el COD_TIPO_ESTADO de cada tipo.
        var tipos     = (await multi.ReadAsync<TiposRequerimiento>()).ToList();
        var centros   = MapCombo(await multi.ReadAsync<dynamic>());
        var entidades = MapEntidadCombo(await multi.ReadAsync<dynamic>());
        var lotes     = MapLoteCombo(await multi.ReadAsync<dynamic>());
        var productos = (await multi.ReadAsync<KonSolutionsApi.Domain.Models.Estandar.Productos>()).ToList();
        // Sexto EXEC, agregado al final del procedimiento: el orden es el contrato con el SP,
        // así que va último y no reordena los cinco que ya estaban.
        var almacenes = MapCombo(await multi.ReadAsync<dynamic>());
        // Séptimo EXEC, también agregado al final: PROC_PRODUCCION_PROCESOS_SHOWCOMBOX. El orden
        // de los resultsets ES el contrato con el SP — leerlos en otro orden no da error, da
        // combos cruzados.
        var procesos  = MapCombo(await multi.ReadAsync<dynamic>());

        return new RequerimientosStockLoadCombosResult
        {
            Tipos = tipos, Centros = centros, Entidades = entidades, Lotes = lotes, Productos = productos,
            Almacenes = almacenes, Procesos = procesos
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código (primera
    /// que empiece con "COD_") y la de descripción (primera "DES_"/"NOMBRE_") — nunca por
    /// columnas literales "Id"/"Texto".</summary>
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

    /// <summary>PROC_LOGISTICA_LOTES_CONTROL_SHOWCOMBOX devuelve NUM_LOTE_CONTROL, DES_LOTE_CONTROL,
    /// FEC_LOTE_CONTROL, COD_TIPO_LOTE_CONTROL, COD_ENTIDAD... El MapCombo genérico tomaría como
    /// código la primera "COD_*" (el TIPO de lote, p. ej. "LOC"): todos los lotes saldrían con el
    /// mismo Id. Acá el código es NUM_LOTE_CONTROL y el texto DES_LOTE_CONTROL, explícitos.</summary>
    private static List<ComboboxItem> MapLoteCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var id = d.TryGetValue("NUM_LOTE_CONTROL", out var n) ? n?.ToString() ?? "" : "";
            var tx = d.TryGetValue("DES_LOTE_CONTROL", out var t) ? t?.ToString() ?? "" : "";
            resultado.Add(new ComboboxItem { Id = id, Texto = tx });
        }
        return resultado;
    }

    /// <summary>PROC_ESTANDAR_ENTIDADES_SHOWCOMBOX trae varias columnas DES_* (DES_PATERNO,
    /// DES_MATERNO, DES_NOMBRE...) antes que DES_NOMBRE_COMPLETO: el MapCombo genérico (que toma
    /// la primera DES_*) elegiría la equivocada. Acá se prioriza explícitamente
    /// DES_NOMBRE_COMPLETO y se cae a las otras solo si esa viene vacía — sin tocar columnas
    /// literales "Id"/"Texto".</summary>
    private static List<ComboboxItem> MapEntidadCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var id = d.TryGetValue("COD_ENTIDAD", out var idv) ? idv?.ToString() ?? "" : "";
            var texto = new[] { "DES_NOMBRE_COMPLETO", "DES_COMERCIAL", "DES_NOMBRE" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
            resultado.Add(new ComboboxItem { Id = id, Texto = texto });
        }
        return resultado;
    }

}