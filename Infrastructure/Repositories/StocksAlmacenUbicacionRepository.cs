using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class StocksAlmacenUbicacionRepository : IStocksAlmacenUbicacionRepository
{
    private readonly IDbConnectionFactory _db;
    public StocksAlmacenUbicacionRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<StocksAlmacenUbicacion>> ShowallAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<StocksAlmacenUbicacion>(SP.SCHEMALogistica.StocksAlmacenUbicacion.Showall, commandType: CommandType.StoredProcedure);
    }

    public async Task<StocksAlmacenUbicacion?> ShowbyidAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<StocksAlmacenUbicacion>(SP.SCHEMALogistica.StocksAlmacenUbicacion.Showbyid, new { COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.StocksAlmacenUbicacion.Showcombox);
    }

    /// <summary>Los cinco combos independientes, en una sola ida a la base.
    ///
    /// Cada resultset se reconoce por su columna clave (COD_ALMACEN, COD_GRUPO_PRODUCTO, …) y
    /// no por la posición en que llega. Antes se leían por orden, y el orden es justamente lo
    /// que no se puede dar por sentado: alcanza con que uno de los SHOWCOMBOX del procedimiento
    /// devuelva un resultset de más —o que se reordene una línea— para que todos los combos
    /// queden corridos y el de almacenes muestre proveedores, sin ningún error visible.</summary>
    public async Task<StocksAlmacenUbicacionCombos> LoadCombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var lector = await conn.ExecuteReaderAsync(
            SP.SCHEMALogistica.StocksAlmacenUbicacion.LoadCombos, commandType: CommandType.StoredProcedure);

        var combos = new StocksAlmacenUbicacionCombos();
        do
        {
            if (lector.FieldCount == 0) continue;

            // Se mira la PRIMERA columna, no si la clave aparece en algún lugar del resultset.
            // Buscarla en cualquier posición no alcanzaba: el resultset de productos también
            // trae COD_GRUPO_PRODUCTO y COD_TIPO_PRODUCTO —son atributos del producto—, así
            // que se lo llevaba la rama de grupos y el combo de productos quedaba vacío. Los
            // SHOWCOMBOX ponen su clave primero, y esa posición sí distingue de qué son las
            // filas. Se resuelve antes de leerlas, así que identifica también al resultset
            // que viene vacío.
            switch (lector.GetName(0).ToUpperInvariant())
            {
                case "COD_ALMACEN":        combos.Almacenes     = LeerCombo(lector, "COD_ALMACEN", "DES_ALMACEN"); break;
                case "COD_GRUPO_PRODUCTO": combos.Grupos        = LeerCombo(lector, "COD_GRUPO_PRODUCTO", "DES_GRUPO_PRODUCTO"); break;
                case "COD_TIPO_PRODUCTO":  combos.TiposProducto = LeerCombo(lector, "COD_TIPO_PRODUCTO", "DES_TIPO_PRODUCTO"); break;
                case "COD_ENTIDAD":        combos.Proveedores   = LeerCombo(lector, "COD_ENTIDAD", "DES_NOMBRE_COMPLETO", "DES_RAZON_SOCIAL", "DES_ENTIDAD"); break;
                case "COD_PRODUCTO":       combos.Productos     = LeerCombo(lector, "COD_PRODUCTO", "DES_PRODUCTO"); break;

                // Último recurso, sólo para productos: es el resultset más frágil de
                // identificar, porque comparte columnas con los otros cuatro. Si ninguno de
                // los casos anteriores lo reclamó y todavía no hay productos, se lo toma por
                // tener COD_PRODUCTO en cualquier posición. El resto se ignora: es preferible
                // un combo vacío a uno lleno con los datos de otro.
                default:
                    if (combos.Productos.Count == 0 && Tiene(lector, "COD_PRODUCTO"))
                        combos.Productos = LeerCombo(lector, "COD_PRODUCTO", "DES_PRODUCTO");
                    break;
            }
        }
        while (lector.NextResult());

        return combos;
    }

    private static bool Tiene(IDataReader lector, string columna)
    {
        for (var i = 0; i < lector.FieldCount; i++)
            if (string.Equals(lector.GetName(i), columna, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    /// <summary>Las columnas se nombran, no se adivinan.
    ///
    /// <paramref name="colId"/> porque "la primera COD_" no siempre identifica a la fila: en
    /// almacenes conviven COD_ALMACEN, COD_TIPO_ALMACEN, COD_SUCURSAL y COD_ZONIFICACION.
    /// <paramref name="colsTexto"/> por lo mismo del otro lado: el resultset de almacenes trae
    /// además la razón social de la empresa, y tomar "la primera DES_" con la preferencia por
    /// nombres que necesitan las entidades hacía que el combo de almacenes mostrara el nombre
    /// del proveedor en las dos filas. Se pasan varias en orden de preferencia porque en
    /// entidades el nombre está en una columna distinta según sea persona o empresa; el
    /// prefijo DES_ queda sólo como último recurso.</summary>
    private static List<ComboboxItem> LeerCombo(IDataReader lector, string colId, params string[] colsTexto)
    {
        var ordsId = Ordinales(lector, colId);
        var ordsTexto = Ordinales(lector, colsTexto);

        var lista = new List<ComboboxItem>();
        while (lector.Read())
            lista.Add(new ComboboxItem
            {
                Id = PrimeroConValor(lector, ordsId),
                Texto = PrimeroConValor(lector, ordsTexto)
            });
        return lista;
    }

    /// <summary>Los ordinales de las columnas pedidas, en el orden en que se pidieron.
    ///
    /// Devuelve varias y no una sola porque en entidades el nombre no está siempre en la misma
    /// columna: la descripción comercial viene vacía para las personas, y quedarse con ella
    /// dejaba el combo con filas en blanco. La que sirve se elige después, fila por fila.</summary>
    private static List<int> Ordinales(IDataReader lector, params string[] nombres)
    {
        var ords = new List<int>();
        foreach (var nombre in nombres)
            for (var i = 0; i < lector.FieldCount; i++)
                if (string.Equals(lector.GetName(i), nombre, StringComparison.OrdinalIgnoreCase) && !ords.Contains(i))
                    ords.Add(i);
        return ords;
    }

    /// <summary>El primer valor no vacío entre las candidatas. Se decide por fila y no por
    /// columna: en un mismo resultset puede haber filas donde una columna trae dato y otras
    /// donde no.</summary>
    private static string PrimeroConValor(IDataReader lector, List<int> ordinales)
    {
        foreach (var o in ordinales)
        {
            if (o < 0 || o >= lector.FieldCount || lector.IsDBNull(o)) continue;
            var v = lector.GetValue(o)?.ToString()?.Trim();
            if (!string.IsNullOrEmpty(v)) return v;
        }
        return string.Empty;
    }

    /// <summary>La consulta de existencias. Los filtros van tal cual llegan: el procedimiento
    /// descarta solo los vacíos y los ceros, así que no hace falta armar el parámetro variable
    /// acá ni construir SQL a mano.</summary>
    public async Task<IEnumerable<StocksAlmacenUbicacionDetalle>> SearchDetailAsync(StocksAlmacenUbicacionSearchDetailDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<StocksAlmacenUbicacionDetalle>(
            SP.SCHEMALogistica.StocksAlmacenUbicacion.SearchDetail, dto,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Lo que dice una etiqueta (RFID o código de barras) y cómo está su stock. No graba nada.
    /// Si el procedimiento rechaza la etiqueta (50710, 50711, 50714) la SqlException sube tal cual: el servicio
    /// la convierte en el mensaje para el usuario.</summary>
    public async Task<StocksAlmacenUbicacionEtiqueta?> EtiquetaAsync(string codStickerRfid)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<StocksAlmacenUbicacionEtiqueta>(
            SP.SCHEMALogistica.StocksAlmacenUbicacion.Etiqueta,
            new { COD_STICKER_RFID = (codStickerRfid ?? "").Trim() },
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>Kardex de una línea de stock. Va con la clave completa de
    /// STOCKS_ALMACEN_UBICACION (almacén, ubicación, producto, lote, proceso y orden de
    /// producción): con menos campos mezclaría los movimientos de otro lote u otra OP y el
    /// saldo no cuadraría con la fila de la consulta.</summary>
    public async Task<IEnumerable<StocksAlmacenUbicacionKardex>> KardexAsync(StocksAlmacenUbicacionKardexDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<StocksAlmacenUbicacionKardex>(
            SP.SCHEMALogistica.StocksAlmacenUbicacion.Kardex, dto,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<StocksAlmacenUbicacion>> SearchAsync(StocksAlmacenUbicacionSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<StocksAlmacenUbicacion>(SP.SCHEMALogistica.StocksAlmacenUbicacion.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(StocksAlmacenUbicacionInsertDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_SECUNDARIO", dto.CAN_PRODUCTO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("CAN_RESERVADA", dto.CAN_RESERVADA);
        p.Add("CAN_RESERVADA_SECUNDARIO", dto.CAN_RESERVADA_SECUNDARIO);
        p.Add("CAN_DISPONIBLE", dto.CAN_DISPONIBLE);
        p.Add("CAN_DISPONIBLE_SECUNDARIO", dto.CAN_DISPONIBLE_SECUNDARIO);
        p.Add("CAN_INMOBILIZADO", dto.CAN_INMOBILIZADO);
        p.Add("CAN_INMOBILIZADO_SECUNDARIO", dto.CAN_INMOBILIZADO_SECUNDARIO);
        p.Add("CAN_TRANSITO", dto.CAN_TRANSITO);
        p.Add("CAN_TRANSITO_SECUNDARIO", dto.CAN_TRANSITO_SECUNDARIO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.StocksAlmacenUbicacion.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.StocksAlmacenUbicacion.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.StocksAlmacenUbicacion.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(StocksAlmacenUbicacionUpdateDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_ALMACEN", dto.COD_ALMACEN);
        p.Add("COD_UBICACION_ALMACEN", dto.COD_UBICACION_ALMACEN);
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("NUM_LOTE_CONTROL", dto.NUM_LOTE_CONTROL);
        p.Add("CAN_PRODUCTO", dto.CAN_PRODUCTO);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("CAN_PRODUCTO_SECUNDARIO", dto.CAN_PRODUCTO_SECUNDARIO);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIO", dto.COD_UNIDAD_MEDIDA_SECUNDARIO);
        p.Add("CAN_RESERVADA", dto.CAN_RESERVADA);
        p.Add("CAN_RESERVADA_SECUNDARIO", dto.CAN_RESERVADA_SECUNDARIO);
        p.Add("CAN_DISPONIBLE", dto.CAN_DISPONIBLE);
        p.Add("CAN_DISPONIBLE_SECUNDARIO", dto.CAN_DISPONIBLE_SECUNDARIO);
        p.Add("CAN_INMOBILIZADO", dto.CAN_INMOBILIZADO);
        p.Add("CAN_INMOBILIZADO_SECUNDARIO", dto.CAN_INMOBILIZADO_SECUNDARIO);
        p.Add("CAN_TRANSITO", dto.CAN_TRANSITO);
        p.Add("CAN_TRANSITO_SECUNDARIO", dto.CAN_TRANSITO_SECUNDARIO);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.StocksAlmacenUbicacion.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.StocksAlmacenUbicacion.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.StocksAlmacenUbicacion.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_ALMACEN, string COD_UBICACION_ALMACEN, string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.StocksAlmacenUbicacion.Delete, new { COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN, COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.StocksAlmacenUbicacion.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.StocksAlmacenUbicacion.Delete, new { COD_ALMACEN = COD_ALMACEN, COD_UBICACION_ALMACEN = COD_UBICACION_ALMACEN, COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

}