using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ProductosRepository : IProductosRepository
{
    private readonly IDbConnectionFactory _db;
    public ProductosRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Un combo vacio manda "" y "" NO es lo mismo que NULL: el SP entiende NULL
    /// como "no filtres por esto", mientras que "" filtraria por un codigo que no existe y
    /// devolveria cero filas. Se normaliza aca, en un solo lugar.</summary>
    private static string? NullSiVacio(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

    /// <summary>Agrega el parametro SOLO si trae valor.
    ///
    /// No es lo mismo que mandarlo en NULL: mandarlo significa que el SP TIENE que declararlo, y
    /// PROC_ESTANDAR_PRODUCTOS_SHOWALL y _SEARCH no declaran ninguno de los tres filtros
    /// (verificado en sys.parameters y en el script de la base: SHOWALL no recibe nada y SEARCH
    /// solo @SEARCH). Cuando a un SP se le manda un parametro que no tiene, falla la llamada
    /// entera: por eso el listado volvia vacio y el combo de producto se veia sin opciones.
    ///
    /// Asi el repositorio funciona con el SP como esta hoy, y si manana le agregas los filtros
    /// empiezan a aplicarse solos, sin tocar este codigo.</summary>
    private static void AgregarSiTieneValor(DynamicParameters p, string nombre, string? valor)
    {
        if (!string.IsNullOrWhiteSpace(valor)) p.Add(nombre, valor);
    }

    public async Task<IEnumerable<Productos>> ShowallAsync(ProductosSearchDto? filtros = null)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        AgregarSiTieneValor(p, "COD_GRUPO_PRODUCTO",       filtros?.COD_GRUPO_PRODUCTO);
        AgregarSiTieneValor(p, "COD_FAMILIA_PRODUCTO",     filtros?.COD_FAMILIA_PRODUCTO);
        AgregarSiTieneValor(p, "COD_SUB_FAMILIA_PRODUCTO", filtros?.COD_SUB_FAMILIA_PRODUCTO);
        return await conn.QueryAsync<Productos>(SP.SCHEMAEstandar.Productos.Showall, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<Productos?> ShowbyidAsync(string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Productos>(SP.SCHEMAEstandar.Productos.Showbyid, new { COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.Productos.Showcombox);
    }

    public async Task<IEnumerable<Productos>> SearchAsync(ProductosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        // @SEARCH lo declara el SP, siempre va. Los tres filtros, solo si traen valor: mismo
        // motivo que en Showall. Este los mandaba SIEMPRE, asi que la busqueda de Productos
        // fallaba en toda llamada, no solo cuando se filtraba.
        p.Add("SEARCH",                   dto.Search);
        AgregarSiTieneValor(p, "COD_GRUPO_PRODUCTO",       dto.COD_GRUPO_PRODUCTO);
        AgregarSiTieneValor(p, "COD_FAMILIA_PRODUCTO",     dto.COD_FAMILIA_PRODUCTO);
        AgregarSiTieneValor(p, "COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        return await conn.QueryAsync<Productos>(SP.SCHEMAEstandar.Productos.Search, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(ProductosInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("DES_PRODUCTO", dto.DES_PRODUCTO);
        p.Add("DES_DETALLADA", dto.DES_DETALLADA);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        // NO es opcional: el parametro del SP no tiene valor por defecto, asi que sin
        // esta linea el alta falla con "expects parameter '@COD_UNIDAD_MEDIDA_DESPACHO_
        // ALMACEN', which was not supplied" desde el momento en que se aplico el SP.
        p.Add("COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN", dto.COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_PARTIDA_ARANCELARIA", dto.COD_PARTIDA_ARANCELARIA);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_PRODUCTO", dto.COD_TIPO_PRODUCTO);
        p.Add("COD_CUBSO", dto.COD_CUBSO);
        p.Add("FLG_INDIVISIBLE", dto.FLG_INDIVISIBLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Productos.Insert, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Productos.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Productos.Insert, p)
        };
    }
}

    /// <summary>Modificacion. Va contra PROC_ESTANDAR_PRODUCTOS_UPDATE y no contra el de
    /// alta: ese ultimo hace RAISERROR si la clave ya existe ("Ya existe un registro con esa
    /// clave"), asi que mientras el dialogo mando todo por POST, modificar un producto fue
    /// imposible. La lista de parametros es la misma del alta — el SP de UPDATE recibe los
    /// mismos veinte — con el COD_PRODUCTO real en vez de vacio.</summary>
    public async Task<WriteResult> UpdateAsync(ProductosUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_PRODUCTO", dto.COD_PRODUCTO);
        p.Add("DES_PRODUCTO", dto.DES_PRODUCTO);
        p.Add("DES_DETALLADA", dto.DES_DETALLADA);
        p.Add("COD_ENTIDAD_PROPIETARIA", dto.COD_ENTIDAD_PROPIETARIA);
        p.Add("COD_DISENO", dto.COD_DISENO);
        p.Add("NUM_VERSION", dto.NUM_VERSION);
        p.Add("COD_UNIDAD_MEDIDA", dto.COD_UNIDAD_MEDIDA);
        p.Add("COD_UNIDAD_MEDIDA_SECUNDARIA", dto.COD_UNIDAD_MEDIDA_SECUNDARIA);
        p.Add("COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN", dto.COD_UNIDAD_MEDIDA_DESPACHO_ALMACEN);
        p.Add("COD_GRUPO_PRODUCTO", dto.COD_GRUPO_PRODUCTO);
        p.Add("COD_FAMILIA_PRODUCTO", dto.COD_FAMILIA_PRODUCTO);
        p.Add("COD_SUB_FAMILIA_PRODUCTO", dto.COD_SUB_FAMILIA_PRODUCTO);
        p.Add("COD_PARTIDA_ARANCELARIA", dto.COD_PARTIDA_ARANCELARIA);
        p.Add("COD_TIPO_VALOR_CALCULO", dto.COD_TIPO_VALOR_CALCULO);
        p.Add("COD_TIPO_PRODUCTO", dto.COD_TIPO_PRODUCTO);
        p.Add("COD_CUBSO", dto.COD_CUBSO);
        p.Add("FLG_INDIVISIBLE", dto.FLG_INDIVISIBLE);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        try
        {
            var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Productos.Update, p, commandType: CommandType.StoredProcedure);
            return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
        }
        catch (SqlException ex)
        {
            return new WriteResult
            {
                Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
                ErrorProcedure = SP.SCHEMAEstandar.Productos.Update,
                SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Productos.Update, p)
            };
        }
    }

    public async Task<IEnumerable<ProductoEspecificacionArbol>> EspecificacionesArbolAsync(ProductosSearchDto? filtros)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("COD_GRUPO_PRODUCTO",       NullSiVacio(filtros?.COD_GRUPO_PRODUCTO));
        p.Add("COD_FAMILIA_PRODUCTO",     NullSiVacio(filtros?.COD_FAMILIA_PRODUCTO));
        p.Add("COD_SUB_FAMILIA_PRODUCTO", NullSiVacio(filtros?.COD_SUB_FAMILIA_PRODUCTO));
        // Consultas de Stock lo manda en true para que el arbol muestre solo lo que tiene
        // existencias. Productos no lo manda, y el SP lo toma como 0: el arbol completo.
        p.Add("SOLO_CON_EXISTENCIAS",     filtros?.SOLO_CON_EXISTENCIAS == true ? 1 : 0);
        return await conn.QueryAsync<ProductoEspecificacionArbol>(
            SP.SCHEMAEstandar.Productos.EspecificacionesArbol, p, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Este SP reemplaza al SHOWALL y al SEARCH cuando la pantalla tiene el panel de
    /// especificaciones: sin checks marcados se comporta igual que ellos, asi que la pantalla
    /// llama siempre al mismo endpoint en vez de elegir camino segun si hay filtros.</summary>
    public async Task<IEnumerable<Productos>> FiltrarPorEspecificacionesAsync(ProductosSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("FILTROS",                  NullSiVacio(dto.FILTRO_ESPECIFICACIONES));
        p.Add("SEARCH",                   dto.Search);
        p.Add("COD_GRUPO_PRODUCTO",       NullSiVacio(dto.COD_GRUPO_PRODUCTO));
        p.Add("COD_FAMILIA_PRODUCTO",     NullSiVacio(dto.COD_FAMILIA_PRODUCTO));
        p.Add("COD_SUB_FAMILIA_PRODUCTO", NullSiVacio(dto.COD_SUB_FAMILIA_PRODUCTO));
        return await conn.QueryAsync<Productos>(
            SP.SCHEMAEstandar.Productos.FiltrarEspecificaciones, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> DeleteAsync(string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMAEstandar.Productos.Delete, new { COD_PRODUCTO = COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.Productos.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.Productos.Delete, new { COD_PRODUCTO = COD_PRODUCTO })
        };
    }
}

    /// <summary>PROC_ESTANDAR_PRODUCTOS_DESCRIPCION_DETALLE_UPDATE recalcula DES_DETALLADA
    /// (dbo.FNC_PRODUCTO_MAKE_DESCRIPCION) a partir de las Especificaciones Técnicas del
    /// Producto — se llama después de guardar la ficha de Especificaciones Técnicas en
    /// ProductosDialog. No devuelve resultset (solo SELECT de error dentro del CATCH), así
    /// que se ejecuta con ExecuteAsync en vez de QueryFirstOrDefaultAsync.</summary>
    public async Task ActualizarDescripcionDetalleAsync(string COD_PRODUCTO)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(SP.SCHEMAEstandar.Productos.ActualizarDescripcionDetalle,
            new { COD_PRODUCTO }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// PROC_ESTANDAR_PRODUCTOS_LOAD_COMBOS invoca en cadena 7 SHOWCOMBOX (Grupos,
    /// Unidades de Medida, Tipos de Producto, Diseños, Entidades, Cubso, Estados).
    /// Tres de ellos (Grupos, Unidades de Medida, Tipos de Producto) NO alias sus
    /// columnas como Id/Texto — se mapean a mano igual que en FamiliasProductoRepository.
    /// Diseños y Entidades sí vienen aliasados (Id/Texto directo). Cubso viene con
    /// columnas propias (COD_CUBSO/DES_CUBSO/DES_CUBSO_ES) — se arma el texto como
    /// "código - descripción" priorizando la traducción en español. Estados viene con
    /// las mismas columnas que ya mapea MapEstados en los demás módulos.
    /// </summary>
    public async Task<ProductosLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstado>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "ESTANDAR.PRODUCTOS" },
            commandType: CommandType.StoredProcedure);
        var codTipoEstado = te?.COD_TIPO_ESTADO ?? 0;

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.Productos.Loadcombos,
            new { COD_TIPO_ESTADO_PRODUCTO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        return new ProductosLoadCombosResult
        {
            CodTipoEstado  = codTipoEstado,
            Grupos         = MapCombo(await multi.ReadAsync<dynamic>(), "COD_GRUPO_PRODUCTO", "DES_GRUPO_PRODUCTO"),
            UnidadesMedida = MapCombo(await multi.ReadAsync<dynamic>(), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA"),
            TiposProducto  = MapCombo(await multi.ReadAsync<dynamic>(), "COD_TIPO_PRODUCTO", "DES_TIPO_PRODUCTO"),
            Disenos        = (await multi.ReadAsync<ComboboxItem>()).ToList(),
            // La columna "Texto" que trae PROC_ESTANDAR_ENTIDADES_SHOWCOMBOX viene rota
            // (NULL o "...") — el nombre real está en DES_NOMBRE_COMPLETO/DES_COMERCIAL, así
            // que se mapea a mano igual que EntidadesRepository.ShowcomboxAsync, sin confiar
            // en el alias "Texto" del SP.
            Entidades      = MapEntidades(await multi.ReadAsync<dynamic>()),
            // PROC_SUNAT_CUBSO_SUNAT_SHOWCOMBOX ya alias sus columnas como Id/Texto
            // (fix aplicado en una sesión anterior) — se lee directo, sin mapeo manual.
            Cubso          = (await multi.ReadAsync<ComboboxItem>()).ToList(),
            Estados        = MapEstados(await multi.ReadAsync<dynamic>())
        };
    }

    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d  = (IDictionary<string, object>)f;
            var id = d.TryGetValue(colId,    out var v1) ? v1?.ToString() ?? "" : "";
            var tx = d.TryGetValue(colTexto, out var v2) ? v2?.ToString() ?? "" : "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    /// <summary>Mismo fallback que EntidadesRepository.ShowcomboxAsync: la columna "Texto"
    /// del SP viene rota, así que se prioriza DES_NOMBRE_COMPLETO/DES_COMERCIAL/DES_NOMBRE.</summary>
    private static List<ComboboxItem> MapEntidades(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d = (IDictionary<string, object>)f;
            var id = new[] { "Id", "COD_ENTIDAD" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
            var tx = new[] { "DES_NOMBRE_COMPLETO", "DES_COMERCIAL", "DES_NOMBRE", "DES_ENTIDAD", "Texto" }
                .Select(c => d.TryGetValue(c, out var v) ? v?.ToString() : null)
                .FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    private static List<EstadoItem> MapEstados(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d    = (IDictionary<string, object>)f;
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