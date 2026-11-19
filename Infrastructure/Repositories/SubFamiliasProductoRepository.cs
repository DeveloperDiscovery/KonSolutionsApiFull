using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class SubFamiliasProductoRepository : ISubFamiliasProductoRepository
{
    private readonly IDbConnectionFactory _db;
    public SubFamiliasProductoRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<SubFamiliasProducto>> ShowallAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubFamiliasProducto>(SP.SCHEMAEstandar.SubFamiliasProducto.Showall, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<SubFamiliasProducto?> ShowbyidAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<SubFamiliasProducto>(SP.SCHEMAEstandar.SubFamiliasProducto.Showbyid, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO = COD_SUB_FAMILIA_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.SubFamiliasProducto.Showcombox);
    }

    /// <summary>El mismo procedimiento, acotado a su padre. Es lo que permite encadenar los
    /// combos sin traerse el catálogo entero y recortarlo en el navegador.</summary>
    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.SubFamiliasProducto.Showcombox, new { COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO });
    }

    public async Task<IEnumerable<SubFamiliasProducto>> SearchAsync(SubFamiliasProductoSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<SubFamiliasProducto>(SP.SCHEMAEstandar.SubFamiliasProducto.Search, new { SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(SubFamiliasProductoInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("DES_SUB_FAMILIA_PRODUCTO", dto.DES_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_CUBSO", NuloSiVacio(dto.COD_CUBSO));
        p.Add("COD_SUB_FAMILIA_PRODUCTO_ALIAS", dto.COD_SUB_FAMILIA_PRODUCTO_ALIAS);
        p.Add("COD_TIPO_PRODUCTO", NuloSiVacio(dto.COD_TIPO_PRODUCTO));
        p.Add("COD_PARTIDA_ARANCELARIA", NuloSiVacio(dto.COD_PARTIDA_ARANCELARIA));
        p.Add("COD_TIPO_VALOR_CALCULO", NuloSiVacio(dto.COD_TIPO_VALOR_CALCULO));
        p.Add("COD_UNIDAD_MEDIDA", NuloSiVacio(dto.COD_UNIDAD_MEDIDA));
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", NuloSiVacio(dto.COD_UNIDAD_MEDIDA_SECUNDARIA));
        p.Add("COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN", NuloSiVacio(dto.COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN));
        // tinyint: sin tipo de lectura va NULL, no 0 (un 0 no existe en ENTORNO.TIPOS_LECTURA).
        p.Add("COD_TIPO_LECTURA", dto.COD_TIPO_LECTURA, DbType.Byte);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SubFamiliasProducto.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SubFamiliasProducto.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SubFamiliasProducto.Insert, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(string COD_GRUPO_PRODUCTO, string COD_FAMILIA_PRODUCTO, string COD_SUB_FAMILIA_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.SubFamiliasProducto.Delete, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO = COD_SUB_FAMILIA_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.SubFamiliasProducto.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.SubFamiliasProducto.Delete, new { COD_GRUPO_PRODUCTO = COD_GRUPO_PRODUCTO, COD_FAMILIA_PRODUCTO = COD_FAMILIA_PRODUCTO, COD_SUB_FAMILIA_PRODUCTO = COD_SUB_FAMILIA_PRODUCTO })
        };
    }
}

    public async Task<SubFamiliasProductoLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "ESTANDAR.SUB_FAMILIAS_PRODUCTO" },
            commandType: CommandType.StoredProcedure);
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.SubFamiliasProducto.Loadcombos,
            new { COD_TIPO_ESTADO = te?.COD_TIPO_ESTADO ?? 0 },
            commandType: CommandType.StoredProcedure);
        var tiposProducto = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_PRODUCTO", "DES_TIPO_PRODUCTO");
        var estados       = MapEstados(await multi.ReadAsync<dynamic>());
        // Cubso (SUNAT) — 3er resultset, agregado al final del SP (EXEC
        // PROC_SUNAT_CUBSO_SUNAT_SHOWCOMBOX). Antes se traía con una llamada HTTP aparte a
        // ApiRoutes.Sunat.CubsoSunatCombobox. A diferencia del resto de SHOWCOMBOX de este
        // repo, este resultset se lee por convención de nombre (COD_*/DES_*) y no por los
        // alias Id/Texto: el Id venía vacío y el combo terminaba mandando '' al grabar.
        var cubso         = MapIdTexto(await multi.ReadAsync<dynamic>());

        // Igual que en Grupos: Unidad de Medida y Tipo de Valor de Cálculo no vienen en el
        // LOADCOMBOS de este módulo — se cargan aparte con los mismos SHOWCOMBOX ya usados
        // en el resto del sistema, mapeados a mano porque no alias sus columnas Id/Texto.
        var unidadesMedida    = MapCombo(await conn.QueryAsync<dynamic>(SP.SCHEMAEstandar.UnidadesMedida.Showcombox, commandType: CommandType.StoredProcedure), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA");
        var tiposValorCalculo = MapCombo(await conn.QueryAsync<dynamic>(SP.SCHEMAComercial.TiposValorCalculo.Showcombox, commandType: CommandType.StoredProcedure), "COD_TIPO_VALOR_CALCULO", "DES_TIPO_VALOR_CALCULO");
        // Tipos de lectura: su SHOWCOMBOX devuelve (COD_TIPO_LECTURA, DES_TIPO_LECTURA, FLG_MANUAL, FLG_CODEBAR, FLG_RFID), sin alias Id/Texto.
        var tiposLectura      = MapCombo(await conn.QueryAsync<dynamic>(SP.SCHEMAEntorno.TiposLectura.Showcombox, commandType: CommandType.StoredProcedure), "COD_TIPO_LECTURA", "DES_TIPO_LECTURA");

        return new SubFamiliasProductoLoadCombosResult
        {
            CodTipoEstado     = te?.COD_TIPO_ESTADO ?? 0,
            TiposProducto     = tiposProducto,
            Estados           = estados,
            UnidadesMedida    = unidadesMedida,
            TiposValorCalculo = tiposValorCalculo,
            TiposLectura      = tiposLectura,
            Cubso             = cubso
        };
    }

    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var id  = d.TryGetValue(colId,    out var v1) ? v1?.ToString() ?? "" : "";
            var tx  = d.TryGetValue(colTexto, out var v2) ? v2?.ToString() ?? "" : "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    /// <summary>Lee un resultset de combo por CONVENCIÓN de nombre de columna, igual que
    /// ComboboxReader: la primera columna COD_* es el código y la primera DES_* la
    /// descripción. Los alias genéricos Id/Texto quedan sólo como último recurso.
    ///
    /// POR QUÉ CAMBIÓ. Antes leía únicamente "Id"/"Texto", confiando en que
    /// PROC_SUNAT_CUBSO_SUNAT_SHOWCOMBOX los aliasea. El Texto llegaba bien pero el Id venía
    /// VACÍO, así que el combo de CUBSO se veía correcto —el código estaba dentro del texto—
    /// y al grabar mandaba cadena vacía. Eso reventaba contra FK_..._CUBSO_SUNAT con error
    /// 547 aunque el código existiera en SUNAT.CUBSO_SUNAT: '' no es ningún CUBSO.
    ///
    /// Leer por convención es además lo que ComboboxReader documenta como política de la
    /// casa: los SP pierden los alias cada vez que se regeneran, y el nombre de la columna
    /// es lo único estable.</summary>
    private static List<ComboboxItem> MapIdTexto(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var id = PrimeraPorPrefijo(d, "COD_") ?? Valor(d, "Id");
            var tx = PrimeraPorPrefijo(d, "DES_") ?? Valor(d, "Texto");
            return new ComboboxItem { Id = id ?? "", Texto = string.IsNullOrWhiteSpace(tx) ? (id ?? "") : tx };
        }).ToList();

    /// <summary>El valor de la primera columna cuyo nombre empiece con el prefijo y traiga
    /// contenido. null si no hay ninguna, para poder encadenar con ?? el respaldo.</summary>
    private static string? PrimeraPorPrefijo(IDictionary<string, object> fila, string prefijo)
    {
        foreach (var kv in fila)
        {
            if (!kv.Key.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase)) continue;
            var texto = kv.Value?.ToString()?.Trim();
            if (!string.IsNullOrEmpty(texto)) return texto;
        }
        return null;
    }

    private static string? Valor(IDictionary<string, object> fila, string columna)
    {
        if (!fila.TryGetValue(columna, out var v)) return null;
        var texto = v?.ToString()?.Trim();
        return string.IsNullOrEmpty(texto) ? null : texto;
    }

    /// <summary>Cadena vacía a NULL para las columnas que son clave foránea.
    ///
    /// Una FK opcional acepta NULL —"no tiene"— pero nunca acepta '': la cadena vacía sí es
    /// un valor y tendría que existir en la tabla referenciada. Un combo sin elegir manda ""
    /// y el motor responde 547, que se lee como si el código estuviera mal cuando en realidad
    /// no se eligió ninguno.</summary>
    private static string? NuloSiVacio(string? valor)
        => string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();

    private static List<EstadoItem> MapEstados(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var cod  = d.TryGetValue("COD_ESTADO",    out var v1) ? v1?.ToString() ?? "" : "";
            var des  = d.TryGetValue("DES_ESTADO",    out var v2) ? v2?.ToString() ?? "" : "";
            var back = d.TryGetValue("DES_BACKCOLOR", out var v3) ? v3?.ToString() ?? "" : "";
            var fore = d.TryGetValue("DES_FORECOLOR", out var v4) ? v4?.ToString() ?? "" : "";
            var def  = d.TryGetValue("FLG_DEFAULT",   out var v5) && v5 is not null && Convert.ToBoolean(v5);
            return new EstadoItem { CodEstado = int.TryParse(cod, out var n) ? n : 0,
                DesEstado = des, BackColor = back, ForeColor = fore, EsDefault = def };
        }).ToList();

    private class TablaTipoEstado { public int COD_TIPO_ESTADO { get; set; } }
}