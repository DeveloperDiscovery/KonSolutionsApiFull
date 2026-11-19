using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using KonSolutionsApi.Application.Interfaces;
using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Estandar;
using KonSolutionsApi.Infrastructure.Data;

namespace KonSolutionsApi.Infrastructure.Repositories;

public class ConfiguracionGeneralRepository : IConfiguracionGeneralRepository
{
    private readonly IDbConnectionFactory _db;
    public ConfiguracionGeneralRepository(IDbConnectionFactory db) => _db = db;

    public async Task<IEnumerable<ConfiguracionGeneral>> ShowallAsync(string? desNombreEmpresa = null)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionGeneral>(
            SP.SCHEMAEstandar.ConfiguracionGeneral.Showall,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<ConfiguracionGeneral?> ShowbyidAsync(string DES_NOMBRE_EMPRESA)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<ConfiguracionGeneral>(
            SP.SCHEMAEstandar.ConfiguracionGeneral.Showbyid,
            new { DES_NOMBRE_EMPRESA },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ComboboxItem>> ShowcomboxAsync()
    {
        using var conn = _db.CreateConnection();
        return await ComboboxReader.LeerAsync(conn, SP.SCHEMAEstandar.ConfiguracionGeneral.Showcombox);
    }

    /// <summary>PROC_ESTANDAR_CONFIGURACION_GENERAL_LOADCOMBOS: Monedas + Tipos de
    /// Configuración + Grupos de Producto + Tipos de Valor de Cálculo en un solo viaje (antes
    /// llamadas independientes desde ConfiguracionGeneral.razor). El SP solo hace EXEC de los
    /// 4 _SHOWCOMBOX ya existentes, en ese orden — se leen como 4 result sets con
    /// QueryMultipleAsync.</summary>
    public async Task<ConfiguracionGeneralLoadCombosResult> LoadcombosAsync()
    {
        using var conn = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            SP.SCHEMAEstandar.ConfiguracionGeneral.Loadcombos, commandType: CommandType.StoredProcedure);

        return new ConfiguracionGeneralLoadCombosResult
        {
            Monedas            = MapCombo(await multi.ReadAsync<dynamic>()),
            TiposConfiguracion = MapCombo(await multi.ReadAsync<dynamic>()),
            GruposProducto     = MapCombo(await multi.ReadAsync<dynamic>()),
            TiposValorCalculo  = MapCombo(await multi.ReadAsync<dynamic>()),
            // Dos EXEC agregados al final del procedimiento: el mismo combo de clases de
            // movimiento sirve para Almacén y para Producción, igual que TiposValorCalculo
            // ya se reutiliza para Importe Total y Descuento.
            ClasesOperacion    = MapCombo(await multi.ReadAsync<dynamic>()),
            ClasesMovimiento   = MapCombo(await multi.ReadAsync<dynamic>())
        };
    }

    /// <summary>Mapea filas dinámicas a ComboboxItem detectando la columna de código (primera
    /// "COD_*") y la de descripción (primera "DES_*") — mismo criterio que ComboboxReader,
    /// pero sobre un result set ya leído por QueryMultipleAsync en vez de ejecutar el SP.</summary>
    private static List<ComboboxItem> MapCombo(IEnumerable<dynamic> filas)
    {
        var resultado = new List<ComboboxItem>();
        foreach (var fila in filas)
        {
            var d = (IDictionary<string, object>)fila;
            var colId = d.Keys.FirstOrDefault(k => k.StartsWith("COD_", StringComparison.OrdinalIgnoreCase));
            var colTexto = d.Keys.FirstOrDefault(k => k.StartsWith("DES_", StringComparison.OrdinalIgnoreCase));

            resultado.Add(new ComboboxItem
            {
                Id    = colId is not null ? d[colId]?.ToString() ?? "" : "",
                Texto = colTexto is not null ? d[colTexto]?.ToString() ?? "" : ""
            });
        }
        return resultado;
    }

    public async Task<IEnumerable<ConfiguracionGeneral>> SearchAsync(ConfiguracionGeneralSearchDto dto)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<ConfiguracionGeneral>(
            SP.SCHEMAEstandar.ConfiguracionGeneral.Search,
            new { SEARCH = dto.Search },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<WriteResult> SaveAsync(ConfiguracionGeneralSaveDto dto, string codUsuario, string codEstacion)
    {
        using var conn = _db.CreateConnection();
        var p = new DynamicParameters();
        p.Add("DES_NOMBRE_EMPRESA",               dto.DES_NOMBRE_EMPRESA);
        p.Add("DES_NOMBRE_APLICACION",             dto.DES_NOMBRE_APLICACION);
        p.Add("IMG_LOGO_EMPRESA",                  dto.IMG_LOGO_EMPRESA,    DbType.Binary, size: -1);
        p.Add("IMG_LOGO_APLICACION",               dto.IMG_LOGO_APLICACION, DbType.Binary, size: -1);
        p.Add("NUM_DOCUMENTO_IDENTIDAD",           dto.NUM_DOCUMENTO_IDENTIDAD);
        p.Add("DES_DOMICILIO_FISCAL",              dto.DES_DOMICILIO_FISCAL);
        p.Add("DES_REFERENCIA_FISCAL",             dto.DES_REFERENCIA_FISCAL);
        p.Add("COD_PAIS",                          dto.COD_PAIS);
        p.Add("COD_UBIGEO",                        dto.COD_UBIGEO);
        p.Add("COD_TIPO_EMPRESA",                  dto.COD_TIPO_EMPRESA);
        p.Add("COD_TIPO_AUTORIZACION_COMERCIAL",   dto.COD_TIPO_AUTORIZACION_COMERCIAL);
        p.Add("DES_COLOR_PRIMARY",                 dto.DES_COLOR_PRIMARY);
        p.Add("DES_BACKCOLOR",                     dto.DES_BACKCOLOR);
        p.Add("DES_FORECOLOR",                     dto.DES_FORECOLOR);
        p.Add("DES_COLOR_SECONDARY",               dto.DES_COLOR_SECONDARY);
        p.Add("DES_TYPOGRAPHY_TITLE_FONT",         dto.DES_TYPOGRAPHY_TITLE_FONT);
        p.Add("DES_TYPOGRAPHY_TITLE_SIZE",         dto.DES_TYPOGRAPHY_TITLE_SIZE);
        p.Add("DES_FORECOLOR_TITLE",               dto.DES_FORECOLOR_TITLE);
        p.Add("DES_TYPOGRAPHY_SUBTITLE_FONT",      dto.DES_TYPOGRAPHY_SUBTITLE_FONT);
        p.Add("NUM_TYPOGRAPHY_SUBTITLE_SIZE",      dto.NUM_TYPOGRAPHY_SUBTITLE_SIZE);
        p.Add("DES_FORECOLOR_SUBTITLE",            dto.DES_FORECOLOR_SUBTITLE);
        p.Add("DES_TYPOGRAPHY_CONTROLS_FONT",      dto.DES_TYPOGRAPHY_CONTROLS_FONT);
        p.Add("NUM_TYPOGRAPHY_CONTROLS_SIZE",      dto.NUM_TYPOGRAPHY_CONTROLS_SIZE);
        p.Add("FLG_ACTIVATE_REFRESH_AUTOMATIC",    dto.FLG_ACTIVATE_REFRESH_AUTOMATIC);
        p.Add("NUM_SECONDS_REFRESH_GRID",          dto.NUM_SECONDS_REFRESH_GRID);
        p.Add("FLG_TECLADO_VIRTUAL",               dto.FLG_TECLADO_VIRTUAL);
        p.Add("DES_BACKCOLOR_CONTEXTUALMENU",      dto.DES_BACKCOLOR_CONTEXTUALMENU);
        p.Add("DES_FORECOLOR_CONTEXTUALMENU",      dto.DES_FORECOLOR_CONTEXTUALMENU);
        p.Add("DES_BACKCOLOR_LOGIN",               dto.DES_BACKCOLOR_LOGIN);
        p.Add("DES_FORECOLOR_LOGIN",               dto.DES_FORECOLOR_LOGIN);
        p.Add("DES_BACKCOLOR_DIALOG",              dto.DES_BACKCOLOR_DIALOG);
        p.Add("DES_FORECOLOR_DIALOG",              dto.DES_FORECOLOR_DIALOG);
        p.Add("DES_ICONCOLOR_ACTIONS",             dto.DES_ICONCOLOR_ACTIONS);
        p.Add("FLG_CONTROL_CAMBIOS_ACTIVO",        dto.FLG_CONTROL_CAMBIOS_ACTIVO);
        p.Add("NUM_HIGH_ROW_GRID",                 dto.NUM_HIGH_ROW_GRID);
        p.Add("NUM_FONT_ROW_SIZE",                 dto.NUM_FONT_ROW_SIZE);
        p.Add("NUM_HIGH_HEAD_GRID",                dto.NUM_HIGH_HEAD_GRID);
        p.Add("NUM_FONT_HEAD_SIZE",                dto.NUM_FONT_HEAD_SIZE);
        p.Add("DES_INPUTLINECOLOR",                dto.DES_INPUTLINECOLOR);
        p.Add("DES_INPUTBACKCOLOR",                dto.DES_INPUTBACKCOLOR);
        p.Add("DES_INPUTFORECOLOR",                dto.DES_INPUTFORECOLOR);
        p.Add("DES_INPUTLABELFORECOLOR",           dto.DES_INPUTLABELFORECOLOR);
        p.Add("NUM_INPUTFORESIZE",                 dto.NUM_INPUTFORESIZE);
        p.Add("NUM_INPUTLABELSIZE",                dto.NUM_INPUTLABELSIZE);
        p.Add("NUM_INPUTSPACEHORIZONTAL",          dto.NUM_INPUTSPACEHORIZONTAL);
        p.Add("NUM_INPUTSPACEVERTICAL",            dto.NUM_INPUTSPACEVERTICAL);
        p.Add("FLG_ACTIVAR_LOCKOUT_SCREEN",        dto.FLG_ACTIVAR_LOCKOUT_SCREEN);
        p.Add("NUM_MINUTOS_LOCKOUT_SCREEN",        dto.NUM_MINUTOS_LOCKOUT_SCREEN);
        p.Add("NUM_SECONDS_OUT_FACIAL_RECOGNITION",dto.NUM_SECONDS_OUT_FACIAL_RECOGNITION);
        p.Add("DES_BACKCOLOR_CONTROLS_REQUIRED",   dto.DES_BACKCOLOR_CONTROLS_REQUIRED);
        p.Add("NUM_ICONS_GRID_WIDTH_SIZE",         dto.NUM_ICONS_GRID_WIDTH_SIZE);
        p.Add("NUM_ICONS_GRID_HEIGHT_SIZE",        dto.NUM_ICONS_GRID_HEIGHT_SIZE);
        p.Add("NUM_IMAGES_GRID_WIDTH_SIZE",        dto.NUM_IMAGES_GRID_WIDTH_SIZE);
        p.Add("NUM_IMAGES_GRID_HEIGHT_SIZE",       dto.NUM_IMAGES_GRID_HEIGHT_SIZE);
        p.Add("NUM_IMAGENES_MEGAS_UPLOAD",         dto.NUM_IMAGENES_MEGAS_UPLOAD);
        p.Add("NUM_ROWS_SHOW_GRID_UNIQUE",         dto.NUM_ROWS_SHOW_GRID_UNIQUE);
        p.Add("NUM_ROWS_SHOW_GRID_FATHER",         dto.NUM_ROWS_SHOW_GRID_FATHER);
        p.Add("NUM_ROWS_SHOW_GRID_CHILDREN",       dto.NUM_ROWS_SHOW_GRID_CHILDREN);
        p.Add("DES_BACKCOLOR_ROWS_FOCUS",          dto.DES_BACKCOLOR_ROWS_FOCUS);
        p.Add("DES_FORECOLOR_ROWS_FOCUS",          dto.DES_FORECOLOR_ROWS_FOCUS);
        p.Add("DES_TIPOGRAFIA_ROWS_FOCUS",         dto.DES_TIPOGRAFIA_ROWS_FOCUS);
        p.Add("DES_TIPOGRAFIA_GRID_ROWS",          dto.DES_TIPOGRAFIA_GRID_ROWS);
        p.Add("DES_TIPOGRAFIA_GRID_ROWS_EVEN",     dto.DES_TIPOGRAFIA_GRID_ROWS_EVEN);
        p.Add("DES_TIPOGRAFIA_GRID_ROWS_ODD",      dto.DES_TIPOGRAFIA_GRID_ROWS_ODD);
        p.Add("DES_SKIN_COLOR",                    dto.DES_SKIN_COLOR);
        p.Add("DES_MODE_TEXT",                     dto.DES_MODE_TEXT);
        p.Add("DES_BACKCOLOR_GRID_HEADER",         dto.DES_BACKCOLOR_GRID_HEADER);
        p.Add("DES_FORECOLOR_GRID_HEADER",         dto.DES_FORECOLOR_GRID_HEADER);
        p.Add("DES_FORECOLOR_GRID_ROWS",           dto.DES_FORECOLOR_GRID_ROWS);
        p.Add("DES_BACKCOLOR_GRID_ROWS_EVEN",      dto.DES_BACKCOLOR_GRID_ROWS_EVEN);
        p.Add("DES_BACKCOLOR_GRID_ROWS_ODD",       dto.DES_BACKCOLOR_GRID_ROWS_ODD);
        p.Add("DES_LINECOLOR_GRID_ROWS",           dto.DES_LINECOLOR_GRID_ROWS);
        p.Add("DES_TYPOGRAPHY_GRID_ROWS",          dto.DES_TYPOGRAPHY_GRID_ROWS);
        p.Add("DES_BACKCOLOR_ENVIRONMENT",         dto.DES_BACKCOLOR_ENVIRONMENT);
        p.Add("DES_FORECOLOR_ENVIRONMENT",         dto.DES_FORECOLOR_ENVIRONMENT);
        p.Add("DES_TYPOGRAPHY_ENVIRONMENT_FONT",   dto.DES_TYPOGRAPHY_ENVIRONMENT_FONT);
        p.Add("FLG_DEGRADE_ENVIRONMENT",           dto.FLG_DEGRADE_ENVIRONMENT);
        p.Add("FLG_DEGRADE_BACKGROUND_COLOR",      dto.FLG_DEGRADE_BACKGROUND_COLOR);
        p.Add("FLG_DEGRADE_SECUNDARY_COLOR",       dto.FLG_DEGRADE_SECUNDARY_COLOR);
        p.Add("DES_BUTTON_BACKCOLOR",              dto.DES_BUTTON_BACKCOLOR);
        p.Add("DES_BUTTON_FORECOLOR",              dto.DES_BUTTON_FORECOLOR);
        p.Add("DES_CULTURE",                       dto.DES_CULTURE);
        p.Add("COD_MONEDA_OFICIAL",                dto.COD_MONEDA_OFICIAL);
        p.Add("COD_MONEDA_EXTRANJERA_REFERENCIA",  dto.COD_MONEDA_EXTRANJERA_REFERENCIA);
        p.Add("COD_MONEDA_PIVOT_TIPO_CAMBIO",      dto.COD_MONEDA_PIVOT_TIPO_CAMBIO);
        p.Add("NUM_CHARACTERS_MIO_SEARCH",         dto.NUM_CHARACTERS_MIO_SEARCH);
        // Obligatorios en el SP (sin ISNULL/default): si no se mandan, el motor tira 201
        // "expects parameter", igual que pasó antes con CAN_PRODUCTO_ENTIDAD en otro módulo.
        p.Add("COD_TIPO_VALOR_CALCULO_IMPORTE_TOTAL", dto.COD_TIPO_VALOR_CALCULO_IMPORTE_TOTAL);
        p.Add("COD_TIPO_VALOR_CALCULO_DESCUENTO",     dto.COD_TIPO_VALOR_CALCULO_DESCUENTO);
        p.Add("COD_TIPO_VALOR_CALCULO_SUBTOTAL",      dto.COD_TIPO_VALOR_CALCULO_SUBTOTAL);
        p.Add("COD_CLASE_OPERACION_ORDEN_COMPRA",  dto.COD_CLASE_OPERACION_ORDEN_COMPRA);
        p.Add("COD_CLASE_MOVIMIENTO_ALMACEN",      dto.COD_CLASE_MOVIMIENTO_ALMACEN);
        p.Add("COD_CLASE_MOVIMIENTO_PRODUCCION",   dto.COD_CLASE_MOVIMIENTO_PRODUCCION);
        // Los cinco por defecto de almacén: el SP los declara SIN default, así que van
        // siempre, aunque vengan en null. Omitir uno da el error 201 "expects parameter".
        p.Add("NUM_LOTE_CONTROL_DEFAULT",            dto.NUM_LOTE_CONTROL_DEFAULT);
        p.Add("COD_PROCESO_DEFAULT",                 dto.COD_PROCESO_DEFAULT);
        p.Add("NUM_ORDEN_PRODUCCION_DEFAULT",        dto.NUM_ORDEN_PRODUCCION_DEFAULT);
        p.Add("NUM_ORDEN_PRODUCCION_ITEM_DEFAULT",   dto.NUM_ORDEN_PRODUCCION_ITEM_DEFAULT);
        p.Add("NUM_ORDEN_PRODUCCION_UNIDAD_DEFAULT", dto.NUM_ORDEN_PRODUCCION_UNIDAD_DEFAULT);
        // Las cinco especificaciones técnicas del transporte: el SP también las declara SIN default.
        p.Add("COD_ESPECIFICACION_TECNICA_PLACA", dto.COD_ESPECIFICACION_TECNICA_PLACA);
        p.Add("COD_ESPECIFICACION_TECNICA_CONSTANCIA_INSCRIPCION", dto.COD_ESPECIFICACION_TECNICA_CONSTANCIA_INSCRIPCION);
        p.Add("COD_ESPECIFICACION_TECNICA_CERTIFICADO_VEHICULAR", dto.COD_ESPECIFICACION_TECNICA_CERTIFICADO_VEHICULAR);
        p.Add("COD_ESPECIFICACION_TECNICA_CERTIFICADO_HABILITACION_MTC", dto.COD_ESPECIFICACION_TECNICA_CERTIFICADO_HABILITACION_MTC);
        p.Add("COD_ESPECIFICACION_TECNICA_VEHICULO_ACARREO", dto.COD_ESPECIFICACION_TECNICA_VEHICULO_ACARREO);
        p.Add("COD_GRUPO_PRODUCTO_EQUIPO_RFID", dto.COD_GRUPO_PRODUCTO_EQUIPO_RFID);
        p.Add("COD_FAMILIA_PRODUCTO_EQUIPO_RFID", dto.COD_FAMILIA_PRODUCTO_EQUIPO_RFID);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_MARCA", dto.COD_ESPECIFICACION_TECNICA_RFID_MARCA);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_MODELO", dto.COD_ESPECIFICACION_TECNICA_RFID_MODELO);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_PROTOCOLO", dto.COD_ESPECIFICACION_TECNICA_RFID_PROTOCOLO);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_DIRECCION", dto.COD_ESPECIFICACION_TECNICA_RFID_DIRECCION);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_PUERTO", dto.COD_ESPECIFICACION_TECNICA_RFID_PUERTO);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_PUERTOS_ANTENA", dto.COD_ESPECIFICACION_TECNICA_RFID_PUERTOS_ANTENA);
        p.Add("COD_ESPECIFICACION_TECNICA_RFID_POTENCIA", dto.COD_ESPECIFICACION_TECNICA_RFID_POTENCIA);
        p.Add("COD_GRUPO_PRODUCTO_ESTACION_RFID", dto.COD_GRUPO_PRODUCTO_ESTACION_RFID);
        p.Add("COD_FAMILIA_PRODUCTO_ESTACION_RFID", dto.COD_FAMILIA_PRODUCTO_ESTACION_RFID);
        p.Add("COD_SUB_FAMILIA_PRODUCTO_ESTACION_RFID", dto.COD_SUB_FAMILIA_PRODUCTO_ESTACION_RFID);
        p.Add("COD_PRODUCTO_ESTACION_RFID", dto.COD_PRODUCTO_ESTACION_RFID);
        p.Add("COD_KEY_PATHFILE_PLANTILLA_RFID", dto.COD_KEY_PATHFILE_PLANTILLA_RFID);
        p.Add("DES_ARCHIVO_PLANTILLA_RFID", dto.DES_ARCHIVO_PLANTILLA_RFID);
        p.Add("COD_KEY_PATHFILE_COMPRAS_XML", dto.COD_KEY_PATHFILE_COMPRAS_XML);
        p.Add("DES_CARPETA_COMPRAS_PROCESADOS", dto.DES_CARPETA_COMPRAS_PROCESADOS);
        p.Add("DES_CARPETA_COMPRAS_ERRORES", dto.DES_CARPETA_COMPRAS_ERRORES);
        p.Add("COD_TIPO_OPERACION_COMPRAS_XML", dto.COD_TIPO_OPERACION_COMPRAS_XML);
        p.Add("TAS_TOLERANCIA_PRECIO_COMPRAS", dto.TAS_TOLERANCIA_PRECIO_COMPRAS);
        p.Add("VAL_TOLERANCIA_IMPORTE_COMPRAS", dto.VAL_TOLERANCIA_IMPORTE_COMPRAS);
        p.Add("FLG_XML_RECIBIDOS_ACTIVO", dto.FLG_XML_RECIBIDOS_ACTIVO);
        p.Add("CAN_MINUTOS_XML_RECIBIDOS", dto.CAN_MINUTOS_XML_RECIBIDOS);
        p.Add("DES_CARPETA_CORREO_RECEPCION", dto.DES_CARPETA_CORREO_RECEPCION);
        p.Add("DES_CARPETA_CORREO_PROCESADOS", dto.DES_CARPETA_CORREO_PROCESADOS);
        p.Add("DES_CARPETA_CORREO_ERRORES", dto.DES_CARPETA_CORREO_ERRORES);
        p.Add("COD_USUARIO_VCDP", dto.COD_USUARIO_VCDP);
        // la clave SOL va en el mismo SP: nueva = se cifra y guarda en SQL; vacia = no se toca; FLG_QUITAR = se borra
        p.Add("DES_PASSWORD_VCDP", string.IsNullOrWhiteSpace(dto.DES_PASSWORD_VCDP_NUEVO) ? null : dto.DES_PASSWORD_VCDP_NUEVO.Trim(), DbType.String, size: 100);
        p.Add("FLG_QUITAR_PASSWORD_VCDP", dto.FLG_QUITAR_PASSWORD_VCDP);
        p.Add("COD_USUARIO_REGISTRO",              codUsuario);
        p.Add("COD_ESTACION_REGISTRO",             codEstacion);

try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.ConfiguracionGeneral.Insert, p,
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionGeneral.Insert,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionGeneral.Insert, p)
        };
    }
}

    // Kept for backwards-compat
    public Task<WriteResult> InsertAsync(ConfiguracionGeneralInsertDto dto, string u, string e)
        => SaveAsync(dto, u, e);

    public async Task<WriteResult> DeleteAsync(string DES_NOMBRE_EMPRESA)
    {
        using var conn = _db.CreateConnection();
try
    {
                var r = await conn.QueryFirstOrDefaultAsync<WriteResult>(
            SP.SCHEMAEstandar.ConfiguracionGeneral.Delete,
            new { DES_NOMBRE_EMPRESA },
            commandType: CommandType.StoredProcedure);
        return r ?? new WriteResult { Estado = "OK", AffectedRows = 1 };
    }
    catch (SqlException ex)
    {
        return new WriteResult
        {
            Estado = "ERROR", Mensaje = ex.Message, ErrorNumber = ex.Number,
            ErrorProcedure = SP.SCHEMAEstandar.ConfiguracionGeneral.Delete,
            SqlSentence = SqlSentenceHelper.Build(SP.SCHEMAEstandar.ConfiguracionGeneral.Delete, new { DES_NOMBRE_EMPRESA })
        };
    }
}

    private sealed class FilaVcdp { public string? DES_NOMBRE_EMPRESA { get; set; } public string? NUM_DOCUMENTO_IDENTIDAD { get; set; } public string? COD_USUARIO_VCDP { get; set; } public string? DES_PASSWORD_VCDP { get; set; } }

    public async Task<(string? Usuario, string? Clave, string? Ruc)> CredencialesVcdpAsync()
    {
        using var conn = _db.CreateConnection();
        var f = await conn.QueryFirstOrDefaultAsync<FilaVcdp>("PROC_ESTANDAR_CONFIGURACION_GENERAL_PASSWORD_VCDP_LEER", new { DES_NOMBRE_EMPRESA = (string?)null }, commandType: CommandType.StoredProcedure);
        return (f?.COD_USUARIO_VCDP, f?.DES_PASSWORD_VCDP, f?.NUM_DOCUMENTO_IDENTIDAD);
    }
}
