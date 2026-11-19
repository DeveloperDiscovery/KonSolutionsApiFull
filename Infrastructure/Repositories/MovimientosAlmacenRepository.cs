using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class MovimientosAlmacenRepository : IMovimientosAlmacenRepository
{
    /// <summary>"" y NULL no son lo mismo para una clave foranea: NULL nunca la viola, ""
    /// jamas existe en el catalogo referenciado (error 547). Los codigos opcionales de esta
    /// cabecera - comprobante, centro de costo, lote, almacen destino, ubigeo - llegan como
    /// cadena vacia cuando el tipo de movimiento no los usa y la pantalla no los muestra.</summary>
    private static string? Nulo(string? v) => string.IsNullOrWhiteSpace(v) ? null : v;

    private readonly IDbConnectionFactory _db;
    public MovimientosAlmacenRepository(IDbConnectionFactory db) => _db = db;

    /// <summary>Sólo los movimientos de los almacenes que los roles del usuario tienen
    /// habilitados (SEGURIDAD.ROLES_USUARIO_ALMACENES): el SP filtra por @COD_USUARIO.</summary>
    public async Task<IEnumerable<MovimientosAlmacen>> ShowallAsync(string codUsuario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacen>(SP.SCHEMALogistica.MovimientosAlmacen.Showall, new { COD_USUARIO = codUsuario }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacen?> ShowbyidAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<MovimientosAlmacen>(SP.SCHEMALogistica.MovimientosAlmacen.Showbyid, new { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMALogistica.MovimientosAlmacen.Showcombox, "NUM_MOVIMIENTO_ALMACEN", "DES_OBSERVACION");
    }

    /// <summary>Misma restricción por usuario que el SHOWALL.</summary>
    public async Task<IEnumerable<MovimientosAlmacen>> SearchAsync(MovimientosAlmacenSearchDto dto, string codUsuario)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientosAlmacen>(SP.SCHEMALogistica.MovimientosAlmacen.Search, new { COD_USUARIO = codUsuario, SEARCH = dto.Search }, commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> InsertAsync(MovimientosAlmacenInsertDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("FEC_MOVIMENTO_ALMACEN", dto.FEC_MOVIMENTO_ALMACEN);
        p.Add("FEC_RECEPCION", dto.FEC_RECEPCION);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", Nulo(dto.COD_TIPO_COMPROBANTE_PAGO));
        p.Add("NUM_COMPROBANTE_PAGO", Nulo(dto.NUM_COMPROBANTE_PAGO));
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_CENTRO_COSTO", Nulo(dto.COD_CENTRO_COSTO));
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("NUM_REQUERIMIENTO_STOCK_ATENDIDO", dto.NUM_REQUERIMIENTO_STOCK_ATENDIDO);
        p.Add("NUM_RESERVA_STOCK_ATENDIDO", dto.NUM_RESERVA_STOCK_ATENDIDO);
        p.Add("COD_ALMACEN_SINCRONIZADO", Nulo(dto.COD_ALMACEN_SINCRONIZADO));
        p.Add("NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO", dto.NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO);
        // Los cuatro que el SP pide y este repositorio no mandaba. Sin ellos, el alta y la
        // modificacion de la cabecera fallaban con "expects parameter"; y los dos que si
        // mandaba —los _SINCRONIZADO— ni siquiera son parametros del procedimiento, asi que
        // sumaban su propio error. El modelo venia de una version anterior de la tabla.
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("MUM_PEDIDO_COMERCIAL", dto.MUM_PEDIDO_COMERCIAL);
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_ENTIDAD_TRANSPORTISTA", dto.COD_ENTIDAD_TRANSPORTISTA);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("DES_DIRECCION_DESTINO", Nulo(dto.DES_DIRECCION_DESTINO));
        p.Add("COD_UBIGEO_DESTINO", Nulo(dto.COD_UBIGEO_DESTINO));
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
    try
        {
            // Se lee la fila como DICCIONARIO y no directo a WriteResult.
            //
            // El SP termina con "SELECT NUM_MOVIMIENTO_ALMACEN = SCOPE_IDENTITY()", y
            // WriteResult no tiene ninguna propiedad con ese nombre: Dapper no encontraba
            // dónde ponerlo y lo descartaba. El número llegaba a la base y se perdía acá, así
            // que la pantalla decía "el servidor no devolvió el número" cuando el servidor sí
            // lo había devuelto.
            //
            // Es el mismo patrón que ya usan RequerimientosStock, ReservasStock, Inventarios
            // y Proyectos: leer por nombre de columna y copiarlo a Codigo.
            var fila = await conn.QueryFirstOrDefaultAsync(SP.SCHEMALogistica.MovimientosAlmacen.Insert, p, commandType: CommandType.StoredProcedure);
            if (fila is null) return new WriteResult { Estado = "OK", AffectedRows = 1 };

            var campos = (IDictionary<string, object?>)fila;

            // El CATCH del SP devuelve las columnas de error en vez del número. Se distingue
            // por ErrorNumber: si está, esto no es un alta exitosa.
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
                Codigo = campos.TryGetValue("NUM_MOVIMIENTO_ALMACEN", out var cod) ? cod?.ToString() ?? "" : ""
            };
        }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacen.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacen.Insert, p)
        };
    }
}

    /// <summary>Modificación. Mismo contrato de parámetros que el alta.</summary>
    public async Task<WriteResult> UpdateAsync(MovimientosAlmacenUpdateDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", dto.NUM_MOVIMIENTO_ALMACEN);
        p.Add("COD_ALMACEN", Nulo(dto.COD_ALMACEN));
        p.Add("FEC_MOVIMENTO_ALMACEN", dto.FEC_MOVIMENTO_ALMACEN);
        p.Add("FEC_RECEPCION", dto.FEC_RECEPCION);
        p.Add("COD_CLASE_MOVIMIENTO", dto.COD_CLASE_MOVIMIENTO);
        p.Add("COD_TIPO_MOVIMIENTO", dto.COD_TIPO_MOVIMIENTO);
        p.Add("COD_ENTIDAD", dto.COD_ENTIDAD);
        p.Add("COD_TIPO_COMPROBANTE_PAGO", Nulo(dto.COD_TIPO_COMPROBANTE_PAGO));
        p.Add("NUM_COMPROBANTE_PAGO", Nulo(dto.NUM_COMPROBANTE_PAGO));
        p.Add("DES_OBSERVACION", dto.DES_OBSERVACION);
        p.Add("COD_CENTRO_COSTO", Nulo(dto.COD_CENTRO_COSTO));
        p.Add("NUM_PROYECTO", dto.NUM_PROYECTO);
        p.Add("NUM_LOTE_CONTROL", Nulo(dto.NUM_LOTE_CONTROL));
        p.Add("NUM_REQUERIMIENTO_STOCK_ATENDIDO", dto.NUM_REQUERIMIENTO_STOCK_ATENDIDO);
        p.Add("NUM_RESERVA_STOCK_ATENDIDO", dto.NUM_RESERVA_STOCK_ATENDIDO);
        p.Add("COD_ALMACEN_SINCRONIZADO", Nulo(dto.COD_ALMACEN_SINCRONIZADO));
        p.Add("NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO", dto.NUM_MOVIMIENTO_ALMACEN_SINCRONIZADO);
        // Los cuatro que el SP pide y este repositorio no mandaba. Sin ellos, el alta y la
        // modificacion de la cabecera fallaban con "expects parameter"; y los dos que si
        // mandaba —los _SINCRONIZADO— ni siquiera son parametros del procedimiento, asi que
        // sumaban su propio error. El modelo venia de una version anterior de la tabla.
        p.Add("COD_CLASE_OPERACION", dto.COD_CLASE_OPERACION);
        p.Add("COD_TIPO_OPERACION", dto.COD_TIPO_OPERACION);
        p.Add("MUM_PEDIDO_COMERCIAL", dto.MUM_PEDIDO_COMERCIAL);
        p.Add("NUM_REGISTRO", dto.NUM_REGISTRO);
        p.Add("COD_ENTIDAD_TRANSPORTISTA", dto.COD_ENTIDAD_TRANSPORTISTA);
        p.Add("NUM_SECUENCIA_DIRECCION", dto.NUM_SECUENCIA_DIRECCION);
        p.Add("DES_DIRECCION_DESTINO", Nulo(dto.DES_DIRECCION_DESTINO));
        p.Add("COD_UBIGEO_DESTINO", Nulo(dto.COD_UBIGEO_DESTINO));
        p.Add("COD_TIPO_MOTIVO", dto.COD_TIPO_MOTIVO);
        p.Add("COD_MOTIVO", dto.COD_MOTIVO);
        p.Add("COD_TIPO_ESTADO", dto.COD_TIPO_ESTADO);
        p.Add("COD_ESTADO", dto.COD_ESTADO);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacen.Update, p, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacen.Update,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacen.Update, p)
        };
    }
}

    public async Task<WriteResult> DeleteAsync(decimal NUM_MOVIMIENTO_ALMACEN)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(SP.SCHEMALogistica.MovimientosAlmacen.Delete, new { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN }, commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMALogistica.MovimientosAlmacen.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMALogistica.MovimientosAlmacen.Delete, new { NUM_MOVIMIENTO_ALMACEN = NUM_MOVIMIENTO_ALMACEN })
        };
    }
}


    /// <summary>Los ocho combos de la cabecera en un viaje.
    ///
    /// EL ORDEN EN QUE SE LEEN ES EL CONTRATO con el procedimiento: QueryMultiple devuelve
    /// los conjuntos en el orden en que el SP los declara. Si se cambia alla y no aca, la
    /// pantalla sale con los combos cruzados y sin ningun error que lo delate.</summary>
    public async Task<MovimientosAlmacenLoadCombosResult> LoadcombosAsync(string? codClaseMovimiento)
    {
        using var conn = _db.CreateConnection();

        // El tipo de estado sale de la tabla de configuracion, igual que en Productos: la
        // pantalla no tiene por que saberlo.
        var te = await conn.QueryFirstOrDefaultAsync<TablaTipoEstadoMov>(
            SP.SCHEMAEstandar.TablasTiposEstado.Search,
            new { SEARCH = "LOGISTICA.MOVIMIENTOS_ALMACEN" },
            commandType: CommandType.StoredProcedure);
        var codTipoEstado = te?.COD_TIPO_ESTADO ?? 0;

        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMALogistica.MovimientosAlmacen.Loadcombos,
            new { COD_CLASE_MOVIMIENTO = codClaseMovimiento, COD_TIPO_ESTADO = codTipoEstado },
            commandType: CommandType.StoredProcedure);

        // Se nombran las columnas de CADA conjunto. Los SHOWCOMBOX devuelven COD_x/DES_x, no
        // Id/Texto: leerlos como ComboboxItem no falla, mapea nada y el combo sale con una
        // fila en blanco por registro. Es lo mismo que resuelven a mano
        // StocksAlmacenUbicacionRepository y el MapCombo de ContenedoresAlmacenaje.
        // El primer conjunto (almacenes) se usa dos veces: para el combo y para saber la sucursal de
        // cada uno. Se materializa antes de leer los demás, que siguen en el mismo orden de siempre.
        var filasAlmacenes = (await multi.ReadAsync<dynamic>()).ToList();

        return new MovimientosAlmacenLoadCombosResult
        {
            CodTipoEstado        = codTipoEstado,
            Almacenes            = MapComboMov(filasAlmacenes, "COD_ALMACEN", "DES_ALMACEN"),
            TiposMovimiento      = MapComboMov(await multi.ReadAsync<dynamic>(), "COD_TIPO_MOVIMIENTO", "DES_TIPO_MOVIMIENTO"),
            // La columna "Texto" de PROC_ESTANDAR_ENTIDADES_SHOWCOMBOX viene rota, igual que
            // en Productos: el nombre real esta en DES_NOMBRE_COMPLETO / DES_COMERCIAL.
            Entidades            = MapEntidadesMov(await multi.ReadAsync<dynamic>()),
            TiposComprobantePago = MapComboMov(await multi.ReadAsync<dynamic>(), "COD_TIPO_COMPROBANTE_PAGO", "DES_TIPO_COMPROBANTE_PAGO"),
            CentrosCosto         = MapComboMov(await multi.ReadAsync<dynamic>(), "COD_CENTRO_COSTO", "DES_CENTRO_COSTO"),
            LotesControl         = MapComboMov(await multi.ReadAsync<dynamic>(), "NUM_LOTE_CONTROL", "DES_LOTE_CONTROL"),
            UnidadesMedida       = MapComboMov(await multi.ReadAsync<dynamic>(), "COD_UNIDAD_MEDIDA", "DES_UNIDAD_MEDIDA"),
            Estados              = MapEstadosMov(await multi.ReadAsync<dynamic>()),
            AlmacenesSucursal    = MapAlmacenesSucursalMov(filasAlmacenes)
        };
    }

    /// <summary>Número de comprobante al imprimir. Todo lo decide el SP —a qué movimientos
    /// aplica, si ya tenía número, cómo se arma— porque consumir un correlativo tiene que ser
    /// una sola operación de base: entre leer el último y grabarlo, dos usuarios se llevarían
    /// el mismo número.</summary>
    public async Task<MovimientosAlmacenNumeroImpresionResult> NumeroImpresionAsync(
        decimal numMovimiento, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        var r = await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenNumeroImpresionResult>(
            SP.SCHEMALogistica.MovimientosAlmacen.NumeroImpresion, p, commandType: CommandType.StoredProcedure);
        return r ?? new MovimientosAlmacenNumeroImpresionResult();
    }

    public async Task TransferirAsync(decimal numMovimiento, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        await conn.ExecuteAsync(SP.SCHEMALogistica.MovimientosAlmacen.Transferir, p, commandType: CommandType.StoredProcedure);
    }

    /// <summary>Los tres "Agregar al detalle" comparten firma, asi que comparten metodo.
    /// El procedimiento devuelve una fila con cuantas lineas agrego y un mensaje.</summary>
    public async Task<MovimientosAlmacenAgregarResult> AgregarDesdeAsync(
        string sp, decimal numMovimiento, string nombreClave, int valorClave,
        string? secuencias, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("NUM_MOVIMIENTO_ALMACEN", numMovimiento);
        p.Add(nombreClave, valorClave);
        p.Add("DES_SECUENCIAS", secuencias);
        p.Add("COD_USUARIO_REGISTRO", codUsuario);
        p.Add("COD_ESTACION_REGISTRO", codEstacion);
        var r = await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenAgregarResult>(
            sp, p, commandType: CommandType.StoredProcedure);
        return r ?? new MovimientosAlmacenAgregarResult { CAN_LINEAS_AGREGADAS = 0, DES_MENSAJE = "Sin respuesta del procedimiento." };
    }

    public async Task<IEnumerable<MovimientoDevolucionGuia>> DevolucionGuiasDisponiblesAsync(decimal numMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoDevolucionGuia>(SP.SCHEMALogistica.MovimientosAlmacen.DevolucionCompraGuiasDisponibles,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MovimientoDevolucionGuia>> DevolucionGuiasAsync(decimal numMovimiento)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoDevolucionGuia>(SP.SCHEMALogistica.MovimientosAlmacen.DevolucionCompraGuias,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MovimientoDevolucionLinea>> DevolucionLineasAsync(decimal numMovimiento, string? origenes)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<MovimientoDevolucionLinea>(SP.SCHEMALogistica.MovimientosAlmacen.DevolucionCompraLineas,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento, DES_ORIGENES = origenes }, commandType: CommandType.StoredProcedure);
    }

    public async Task<MovimientosAlmacenAgregarResult> DevolucionAdicionarAsync(decimal numMovimiento, string xml, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var r = await conn.QueryFirstOrDefaultAsync<MovimientosAlmacenAgregarResult>(SP.SCHEMALogistica.MovimientosAlmacen.DevolucionCompraAdicionar,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento, DES_XML = xml, COD_USUARIO_REGISTRO = codUsuario, COD_ESTACION_REGISTRO = codEstacion },
            commandType: CommandType.StoredProcedure);
        return r ?? new MovimientosAlmacenAgregarResult { CAN_LINEAS_AGREGADAS = 0, DES_MENSAJE = "Sin respuesta del procedimiento." };
    }

    public async Task<int> DevolucionQuitarGuiaAsync(decimal numMovimiento, int numMovimientoOrigen)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<int>(SP.SCHEMALogistica.MovimientosAlmacen.DevolucionCompraQuitarGuia,
            new { NUM_MOVIMIENTO_ALMACEN = numMovimiento, NUM_MOVIMIENTO_ALMACEN_ORIGEN = numMovimientoOrigen }, commandType: CommandType.StoredProcedure);
    }

    private static List<ComboboxItem> MapComboMov(IEnumerable<dynamic> filas, string colId, string colTexto) =>
        filas.Select(f => {
            var d  = (IDictionary<string, object>)f;
            var id = d.TryGetValue(colId,    out var v1) ? v1?.ToString() ?? "" : "";
            var tx = d.TryGetValue(colTexto, out var v2) ? v2?.ToString() ?? "" : "";
            return new ComboboxItem { Id = id, Texto = tx };
        }).ToList();

    /// <summary>Sucursal de cada almacén, de las mismas filas de PROC_LOGISTICA_ALMACENES_SHOWCOMBOX.
    /// Un almacén sin sucursal (COD_SUCURSAL null) queda con CodSucursal en null.</summary>
    private static List<AlmacenSucursalItem> MapAlmacenesSucursalMov(IEnumerable<dynamic> filas) =>
        filas.Select(f => {
            var d   = (IDictionary<string, object>)f;
            var alm = d.TryGetValue("COD_ALMACEN",  out var v1) ? v1?.ToString() ?? "" : "";
            var suc = d.TryGetValue("COD_SUCURSAL", out var v2) ? v2?.ToString() : null;
            var des = d.TryGetValue("DES_SUCURSAL", out var v3) ? v3?.ToString() : null;
            return new AlmacenSucursalItem
            {
                CodAlmacen  = alm,
                CodSucursal = string.IsNullOrWhiteSpace(suc) ? null : suc,
                DesSucursal = string.IsNullOrWhiteSpace(des) ? null : des
            };
        }).ToList();

    private static List<ComboboxItem> MapEntidadesMov(IEnumerable<dynamic> filas) =>
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

    private static List<EstadoItem> MapEstadosMov(IEnumerable<dynamic> filas) =>
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

    private class TablaTipoEstadoMov { public int COD_TIPO_ESTADO { get; set; } }
}