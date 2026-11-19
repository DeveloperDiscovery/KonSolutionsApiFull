using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
namespace KonSolutionsApi.Domain.Models.Estandar;

/// <summary>Entidad ESTANDAR.CONFIGURACION_GENERAL — incluye todos los campos del SP.</summary>
public class ConfiguracionGeneral
{
    public string   DES_NOMBRE_EMPRESA                { get; set; } = "";
    public string   DES_NOMBRE_APLICACION             { get; set; } = "";
    public byte[]?  IMG_LOGO_EMPRESA                  { get; set; }
    public byte[]?  IMG_LOGO_APLICACION               { get; set; }

    /// <summary>Hay logo, aunque en esta respuesta no vengan los bytes.
    ///
    /// El endpoint /layout devuelve el registro con los dos IMG_LOGO en null —los logos se
    /// piden por URL aparte, para que el navegador los cachee— y usa estos flags para que
    /// la pantalla igual sepa si tiene que dibujar la imagen o su alternativa.</summary>
    public bool FLG_TIENE_LOGO_EMPRESA    { get; set; }
    public bool FLG_TIENE_LOGO_APLICACION { get; set; }

    /// <summary>Identifica el CONTENIDO de cada logo: cambia cuando se graba otra imagen y no cambia si se graba lo mismo.
    ///
    /// Va en la dirección del logo (…/logo-aplicacion?v=XXXX). La dirección era siempre la misma y el servidor
    /// la respondía con caché de un día, así que al grabar un logo nuevo el navegador seguía pintando el anterior
    /// sin ni siquiera preguntar al servidor: ni volviendo a iniciar sesión se veía el cambio. Con la versión en la
    /// dirección, un logo distinto es una dirección distinta y el navegador lo baja; uno que no cambió se sigue
    /// pintando desde el disco. Null si no hay logo.</summary>
    public string? DES_VERSION_LOGO_EMPRESA    { get; set; }
    public string? DES_VERSION_LOGO_APLICACION { get; set; }

    /// <summary>La versión de un logo: los primeros 16 caracteres hexadecimales de su SHA-256. Null si no hay imagen.</summary>
    public static string? VersionDeLogo(byte[]? bytes)
        => bytes is { Length: > 0 } ? Convert.ToHexString(SHA256.HashData(bytes))[..16].ToLowerInvariant() : null;
    public string   NUM_DOCUMENTO_IDENTIDAD           { get; set; } = "";
    public string   DES_DOMICILIO_FISCAL              { get; set; } = "";
    public string   DES_REFERENCIA_FISCAL             { get; set; } = "";
    public string   COD_PAIS                          { get; set; } = "";
    public string   COD_UBIGEO                        { get; set; } = "";
    public string   COD_TIPO_EMPRESA                  { get; set; } = "";
    public string   COD_TIPO_AUTORIZACION_COMERCIAL   { get; set; } = "";
    public string   DES_COLOR_PRIMARY                 { get; set; } = "";
    public string   DES_BACKCOLOR                     { get; set; } = "";
    public string   DES_FORECOLOR                     { get; set; } = "";
    public string   DES_COLOR_SECONDARY               { get; set; } = "";
    public string   DES_TYPOGRAPHY_TITLE_FONT         { get; set; } = "";
    public int      DES_TYPOGRAPHY_TITLE_SIZE         { get; set; }
    public string   DES_FORECOLOR_TITLE               { get; set; } = "";
    public string   DES_TYPOGRAPHY_SUBTITLE_FONT      { get; set; } = "";
    public int      NUM_TYPOGRAPHY_SUBTITLE_SIZE      { get; set; }
    public string   DES_FORECOLOR_SUBTITLE            { get; set; } = "";
    public string   DES_TYPOGRAPHY_CONTROLS_FONT      { get; set; } = "";
    public int      NUM_TYPOGRAPHY_CONTROLS_SIZE      { get; set; }
    public bool     FLG_ACTIVATE_REFRESH_AUTOMATIC    { get; set; }
    public int      NUM_SECONDS_REFRESH_GRID          { get; set; }
    public bool     FLG_TECLADO_VIRTUAL               { get; set; }
    public string   DES_BACKCOLOR_CONTEXTUALMENU      { get; set; } = "";
    public string   DES_FORECOLOR_CONTEXTUALMENU      { get; set; } = "";
    public string   DES_BACKCOLOR_LOGIN               { get; set; } = "";
    public string   DES_FORECOLOR_LOGIN               { get; set; } = "";
    public string   DES_BACKCOLOR_DIALOG              { get; set; } = "";
    public string   DES_FORECOLOR_DIALOG              { get; set; } = "";
    public string   DES_ICONCOLOR_ACTIONS             { get; set; } = "";
    public bool     FLG_CONTROL_CAMBIOS_ACTIVO        { get; set; }
    public int?     NUM_HIGH_ROW_GRID                 { get; set; }
    public int?     NUM_FONT_ROW_SIZE                 { get; set; }
    public int?     NUM_HIGH_HEAD_GRID                { get; set; }
    public int?     NUM_FONT_HEAD_SIZE                { get; set; }
    // Apariencia de los inputs (línea, fondo y tamaño del label) — configurables desde
    // Configuración General en vez de estar fijos en app.css.
    public string?  DES_INPUTLINECOLOR                { get; set; }
    public string?  DES_INPUTBACKCOLOR                { get; set; }
    public string?  DES_INPUTFORECOLOR                { get; set; }
    public string?  DES_INPUTLABELFORECOLOR           { get; set; }
    public int?     NUM_INPUTFORESIZE                 { get; set; }
    public int?     NUM_INPUTLABELSIZE                { get; set; }
    public int?     NUM_INPUTSPACEHORIZONTAL          { get; set; }
    public int?     NUM_INPUTSPACEVERTICAL            { get; set; }
    public bool     FLG_ACTIVAR_LOCKOUT_SCREEN        { get; set; }
    public int      NUM_MINUTOS_LOCKOUT_SCREEN        { get; set; }
    public int      NUM_SECONDS_OUT_FACIAL_RECOGNITION{ get; set; }
    public string   DES_BACKCOLOR_CONTROLS_REQUIRED   { get; set; } = "";
    public int      NUM_ICONS_GRID_WIDTH_SIZE         { get; set; }
    public int      NUM_ICONS_GRID_HEIGHT_SIZE        { get; set; }
    public int      NUM_IMAGES_GRID_WIDTH_SIZE        { get; set; }
    public int      NUM_IMAGES_GRID_HEIGHT_SIZE       { get; set; }
    public int      NUM_IMAGENES_MEGAS_UPLOAD         { get; set; }
    public int      NUM_ROWS_SHOW_GRID_UNIQUE         { get; set; }
    public int      NUM_ROWS_SHOW_GRID_FATHER         { get; set; }
    public int      NUM_ROWS_SHOW_GRID_CHILDREN       { get; set; }
    public string   DES_BACKCOLOR_ROWS_FOCUS          { get; set; } = "";
    public string   DES_FORECOLOR_ROWS_FOCUS          { get; set; } = "";
    public string   DES_TIPOGRAFIA_ROWS_FOCUS         { get; set; } = "";
    public string   DES_TIPOGRAFIA_GRID_ROWS          { get; set; } = "";
    public string   DES_TIPOGRAFIA_GRID_ROWS_EVEN     { get; set; } = "";
    public string   DES_TIPOGRAFIA_GRID_ROWS_ODD      { get; set; } = "";
    public string   DES_SKIN_COLOR                    { get; set; } = "";
    public string   DES_MODE_TEXT                     { get; set; } = "";
    public string   DES_BACKCOLOR_GRID_HEADER         { get; set; } = "";
    public string   DES_FORECOLOR_GRID_HEADER         { get; set; } = "";
    public string   DES_FORECOLOR_GRID_ROWS           { get; set; } = "";
    public string   DES_BACKCOLOR_GRID_ROWS_EVEN      { get; set; } = "";
    public string   DES_BACKCOLOR_GRID_ROWS_ODD       { get; set; } = "";
    public string   DES_LINECOLOR_GRID_ROWS           { get; set; } = "";
    public string   DES_TYPOGRAPHY_GRID_ROWS          { get; set; } = "";
    public string   DES_BACKCOLOR_ENVIRONMENT         { get; set; } = "";
    public string   DES_FORECOLOR_ENVIRONMENT         { get; set; } = "";
    public string   DES_TYPOGRAPHY_ENVIRONMENT_FONT   { get; set; } = "";
    public bool     FLG_DEGRADE_ENVIRONMENT           { get; set; }
    public bool     FLG_DEGRADE_BACKGROUND_COLOR      { get; set; }
    public bool     FLG_DEGRADE_SECUNDARY_COLOR       { get; set; }
    public string   DES_BUTTON_BACKCOLOR              { get; set; } = "";
    public string   DES_BUTTON_FORECOLOR              { get; set; } = "";
    public string   DES_CULTURE                       { get; set; } = "es-PE";
    public string   COD_MONEDA_OFICIAL                { get; set; } = "";
    public string   COD_MONEDA_EXTRANJERA_REFERENCIA  { get; set; } = "";
    public string   COD_MONEDA_PIVOT_TIPO_CAMBIO      { get; set; } = "";
    public int      NUM_CHARACTERS_MIO_SEARCH         { get; set; }
    public string?  COD_TIPO_VALOR_CALCULO_IMPORTE_TOTAL { get; set; }
    public string?  COD_TIPO_VALOR_CALCULO_DESCUENTO     { get; set; }
    /// <summary>Tipo de valor de cálculo de los comprobantes SIN impuestos (subtotal). Lo usa Proyectos para el costo de las facturas.</summary>
    public string?  COD_TIPO_VALOR_CALCULO_SUBTOTAL      { get; set; }

    // Clase por defecto que cada módulo usa cuando no se le indica otra.
    public string?  COD_CLASE_OPERACION_ORDEN_COMPRA  { get; set; }
    public string?  COD_CLASE_MOVIMIENTO_ALMACEN      { get; set; }
    public string?  COD_CLASE_MOVIMIENTO_PRODUCCION   { get; set; }

    // ── Valores por defecto de almacén ──────────────────────────────────────────
    // Lo que Reservas y Requerimientos graban cuando la línea no trae lote, proceso u
    // orden de producción. Evita que esas columnas queden en NULL, que es lo que rompe
    // las comparaciones contra el stock (NULL = NULL no es verdadero con ANSI_NULLS ON).
    public string?  NUM_LOTE_CONTROL_DEFAULT            { get; set; }
    public string?  COD_PROCESO_DEFAULT                 { get; set; }
    public int?     NUM_ORDEN_PRODUCCION_DEFAULT        { get; set; }
    public short?   NUM_ORDEN_PRODUCCION_ITEM_DEFAULT   { get; set; }
    public byte?    NUM_ORDEN_PRODUCCION_UNIDAD_DEFAULT { get; set; }

    // ── Especificaciones técnicas del transporte (pestaña Almacenes) ─────────────
    // Qué especificación técnica de INGENIERIA.ESPECIFICACIONES_TECNICAS guarda, en LOGISTICA.TRANSPORTES_ESPECIFICACIONES_TECNICAS,
    // cada dato del vehículo que pide la guía de remisión.
    public string?  COD_ESPECIFICACION_TECNICA_PLACA { get; set; }
    public string?  COD_ESPECIFICACION_TECNICA_CONSTANCIA_INSCRIPCION { get; set; }
    public string?  COD_ESPECIFICACION_TECNICA_CERTIFICADO_VEHICULAR { get; set; }
    public string?  COD_ESPECIFICACION_TECNICA_CERTIFICADO_HABILITACION_MTC { get; set; }
    public string?  COD_ESPECIFICACION_TECNICA_VEHICULO_ACARREO { get; set; }
    // ── RFID (pestaña RFID) ──────────────────────────────────────────────────────
    // Qué productos son equipos RFID (el grupo y, si se indica, la familia) y qué especificación técnica de INGENIERIA.ESPECIFICACIONES_TECNICAS guarda,
    // en ESTANDAR.PRODUCTOS_ESPECIFICACIONES_TECNICAS, cada dato del equipo que necesitan la emisión y la impresión de etiquetas.
    // El tipo de equipo no se configura: sale del grupo, la familia y la sub familia del producto. La estación donde corre el agente es UN producto (COD_PRODUCTO_ESTACION_RFID)
    // del grupo, la familia y la sub familia _ESTACION_RFID; la plantilla de impresión es UN archivo (DES_ARCHIVO_PLANTILLA_RFID) dentro de la ruta COD_KEY_PATHFILE_PLANTILLA_RFID
    // (ENTORNO.PATHFILES, Rutas de archivos). Los dos valen para todo el sistema.
    public string? COD_GRUPO_PRODUCTO_EQUIPO_RFID { get; set; }
    public string? COD_FAMILIA_PRODUCTO_EQUIPO_RFID { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_MARCA { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_MODELO { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_PROTOCOLO { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_DIRECCION { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_PUERTO { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_PUERTOS_ANTENA { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_POTENCIA { get; set; }
    public string? COD_GRUPO_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_FAMILIA_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_KEY_PATHFILE_PLANTILLA_RFID { get; set; }
    public string? DES_ARCHIVO_PLANTILLA_RFID { get; set; }
    // ── Compras (pestaña Compras) ───────────────────────────────────────────────
    // La ruta (ENTORNO.PATHFILES, Rutas de archivos) de donde se importan los XML de los proveedores y las subcarpetas a donde pasan los importados y los que fallaron;
    // el tipo de operación con el que entran si no se elige otro; y las tolerancias de la validación (precio en %, importe por redondeo).
    public string? COD_KEY_PATHFILE_COMPRAS_XML { get; set; }
    public string? DES_CARPETA_COMPRAS_PROCESADOS { get; set; }
    public string? DES_CARPETA_COMPRAS_ERRORES { get; set; }
    public string? COD_TIPO_OPERACION_COMPRAS_XML { get; set; }
    public decimal? TAS_TOLERANCIA_PRECIO_COMPRAS { get; set; }
    public decimal? VAL_TOLERANCIA_IMPORTE_COMPRAS { get; set; }
    // ── Carga de XML recibidos (pestaña Compras > XML recibidos) ──────────────────────────
    // Las tres carpetas de Outlook (donde llegan los correos, a donde pasan los ya bajados y a donde pasan los que no se pudieron bajar o procesar), cada cuántos minutos se revisa el correo (vacío: 15)
    // y si la carga automática está activa. Los archivos bajados se guardan en la ruta de la pestaña Compras > Registro de compras, con sus mismas subcarpetas de procesados y de errores.
    public bool?   FLG_XML_RECIBIDOS_ACTIVO { get; set; }
    public int?    CAN_MINUTOS_XML_RECIBIDOS { get; set; }
    public string? DES_CARPETA_CORREO_RECEPCION { get; set; }
    public string? DES_CARPETA_CORREO_PROCESADOS { get; set; }
    public string? DES_CARPETA_CORREO_ERRORES { get; set; }
    // ── Validación de comprobantes de pago electrónicos en SUNAT (pestaña Compras) ──────────
    // El usuario SOL (con el RUC delante: 20123456789USUARIO1) va tal cual. La clave SOL NO vuelve nunca a la pantalla: se guarda cifrada y aquí solo se dice si hay una guardada.
    public string? COD_USUARIO_VCDP { get; set; }
    public bool    TIENE_PASSWORD_VCDP { get; set; }
    public string   COD_USUARIO_REGISTRO              { get; set; } = "";
    public string   COD_ESTACION_REGISTRO             { get; set; } = "";
    public DateTime? FEC_REGISTRO                     { get; set; }
    public string   COD_USUARIO_ACTUALIZACION         { get; set; } = "";
    public string   COD_ESTACION_ACTUALIZACION        { get; set; } = "";
    public DateTime? FEC_ACTUALIZACION                { get; set; }
    // joins
    public string   DES_TIPO_EMPRESA                  { get; set; } = "";
    public string   DES_UBIGEO                        { get; set; } = "";
    public string   DES_MONEDA                        { get; set; } = "";
    public string   DES_PAIS                          { get; set; } = "";
    public string?  DES_BANDERA_EMOJI                 { get; set; }
    public string?  DES_CAPITAL                       { get; set; }
    public string?  DES_PAIS_INGLES                   { get; set; }
    public string?  DES_PAIS_NATIVO                   { get; set; }
    public string?  DES_REGION                        { get; set; }
    public string?  DES_SUBREGION                     { get; set; }
    public string?  DES_TIPO_VALOR_CALCULO_IMPORTE_TOTAL { get; set; }
    public string?  DES_TIPO_VALOR_CALCULO_DESCUENTO     { get; set; }
    public string?  DES_ESPECIFICACION_TECNICA_PLACA { get; set; }
    public string?  DES_ESPECIFICACION_TECNICA_CONSTANCIA_INSCRIPCION { get; set; }
    public string?  DES_ESPECIFICACION_TECNICA_CERTIFICADO_VEHICULAR { get; set; }
    public string?  DES_ESPECIFICACION_TECNICA_CERTIFICADO_HABILITACION_MTC { get; set; }
    public string?  DES_ESPECIFICACION_TECNICA_VEHICULO_ACARREO { get; set; }
}

/// <summary>DTO upsert — incluye todos los campos editables (sin auditoría).</summary>
public class ConfiguracionGeneralSaveDto
{
    [Required] public string  DES_NOMBRE_EMPRESA                { get; set; } = "";
    public string  DES_NOMBRE_APLICACION             { get; set; } = "";
    public byte[]?            IMG_LOGO_EMPRESA                  { get; set; }
    public byte[]?            IMG_LOGO_APLICACION               { get; set; }
    public string  NUM_DOCUMENTO_IDENTIDAD           { get; set; } = "";
    public string  DES_DOMICILIO_FISCAL              { get; set; } = "";
    public string  DES_REFERENCIA_FISCAL             { get; set; } = "";
    public string  COD_PAIS                          { get; set; } = "";
    public string  COD_UBIGEO                        { get; set; } = "";
    public string  COD_TIPO_EMPRESA                  { get; set; } = "";
    public string  COD_TIPO_AUTORIZACION_COMERCIAL   { get; set; } = "";
    public string  DES_COLOR_PRIMARY                 { get; set; } = "";
    public string  DES_BACKCOLOR                     { get; set; } = "";
    public string  DES_FORECOLOR                     { get; set; } = "";
    public string  DES_COLOR_SECONDARY               { get; set; } = "";
    public string  DES_TYPOGRAPHY_TITLE_FONT         { get; set; } = "";
    public int     DES_TYPOGRAPHY_TITLE_SIZE         { get; set; }
    public string  DES_FORECOLOR_TITLE               { get; set; } = "";
    public string  DES_TYPOGRAPHY_SUBTITLE_FONT      { get; set; } = "";
    public int     NUM_TYPOGRAPHY_SUBTITLE_SIZE      { get; set; }
    public string  DES_FORECOLOR_SUBTITLE            { get; set; } = "";
    public string  DES_TYPOGRAPHY_CONTROLS_FONT      { get; set; } = "";
    public int     NUM_TYPOGRAPHY_CONTROLS_SIZE      { get; set; }
    public bool    FLG_ACTIVATE_REFRESH_AUTOMATIC    { get; set; }
    public int     NUM_SECONDS_REFRESH_GRID          { get; set; }
    public bool    FLG_TECLADO_VIRTUAL               { get; set; }
    public string  DES_BACKCOLOR_CONTEXTUALMENU      { get; set; } = "";
    public string  DES_FORECOLOR_CONTEXTUALMENU      { get; set; } = "";
    public string  DES_BACKCOLOR_LOGIN               { get; set; } = "";
    public string  DES_FORECOLOR_LOGIN               { get; set; } = "";
    public string  DES_BACKCOLOR_DIALOG              { get; set; } = "";
    public string  DES_FORECOLOR_DIALOG              { get; set; } = "";
    public string  DES_ICONCOLOR_ACTIONS             { get; set; } = "";
    public bool    FLG_CONTROL_CAMBIOS_ACTIVO        { get; set; }
    public int?    NUM_HIGH_ROW_GRID                 { get; set; }
    public int?    NUM_FONT_ROW_SIZE                 { get; set; }
    public int?    NUM_HIGH_HEAD_GRID                { get; set; }
    public int?    NUM_FONT_HEAD_SIZE                { get; set; }
    // Apariencia de los inputs (línea, fondo y tamaño del label) — configurables desde
    // Configuración General en vez de estar fijos en app.css.
    public string? DES_INPUTLINECOLOR                { get; set; }
    public string? DES_INPUTBACKCOLOR                { get; set; }
    public string? DES_INPUTFORECOLOR                { get; set; }
    public string? DES_INPUTLABELFORECOLOR           { get; set; }
    public int?    NUM_INPUTFORESIZE                 { get; set; }
    public int?    NUM_INPUTLABELSIZE                { get; set; }
    public int?    NUM_INPUTSPACEHORIZONTAL          { get; set; }
    public int?    NUM_INPUTSPACEVERTICAL            { get; set; }
    public bool    FLG_ACTIVAR_LOCKOUT_SCREEN        { get; set; }
    public int     NUM_MINUTOS_LOCKOUT_SCREEN        { get; set; }
    public int     NUM_SECONDS_OUT_FACIAL_RECOGNITION{ get; set; }
    public string  DES_BACKCOLOR_CONTROLS_REQUIRED   { get; set; } = "";
    public int     NUM_ICONS_GRID_WIDTH_SIZE         { get; set; }
    public int     NUM_ICONS_GRID_HEIGHT_SIZE        { get; set; }
    public int     NUM_IMAGES_GRID_WIDTH_SIZE        { get; set; }
    public int     NUM_IMAGES_GRID_HEIGHT_SIZE       { get; set; }
    public int     NUM_IMAGENES_MEGAS_UPLOAD         { get; set; }
    public int     NUM_ROWS_SHOW_GRID_UNIQUE         { get; set; }
    public int     NUM_ROWS_SHOW_GRID_FATHER         { get; set; }
    public int     NUM_ROWS_SHOW_GRID_CHILDREN       { get; set; }
    public string  DES_BACKCOLOR_ROWS_FOCUS          { get; set; } = "";
    public string  DES_FORECOLOR_ROWS_FOCUS          { get; set; } = "";
    public string  DES_TIPOGRAFIA_ROWS_FOCUS         { get; set; } = "";
    public string  DES_TIPOGRAFIA_GRID_ROWS          { get; set; } = "";
    public string  DES_TIPOGRAFIA_GRID_ROWS_EVEN     { get; set; } = "";
    public string  DES_TIPOGRAFIA_GRID_ROWS_ODD      { get; set; } = "";
    public string  DES_SKIN_COLOR                    { get; set; } = "";
    public string  DES_MODE_TEXT                     { get; set; } = "";
    public string  DES_BACKCOLOR_GRID_HEADER         { get; set; } = "";
    public string  DES_FORECOLOR_GRID_HEADER         { get; set; } = "";
    public string  DES_FORECOLOR_GRID_ROWS           { get; set; } = "";
    public string  DES_BACKCOLOR_GRID_ROWS_EVEN      { get; set; } = "";
    public string  DES_BACKCOLOR_GRID_ROWS_ODD       { get; set; } = "";
    public string  DES_LINECOLOR_GRID_ROWS           { get; set; } = "";
    public string  DES_TYPOGRAPHY_GRID_ROWS          { get; set; } = "";
    public string  DES_BACKCOLOR_ENVIRONMENT         { get; set; } = "";
    public string  DES_FORECOLOR_ENVIRONMENT         { get; set; } = "";
    public string  DES_TYPOGRAPHY_ENVIRONMENT_FONT   { get; set; } = "";
    public bool    FLG_DEGRADE_ENVIRONMENT           { get; set; }
    public bool    FLG_DEGRADE_BACKGROUND_COLOR      { get; set; }
    public bool    FLG_DEGRADE_SECUNDARY_COLOR       { get; set; }
    public string  DES_BUTTON_BACKCOLOR              { get; set; } = "";
    public string  DES_BUTTON_FORECOLOR              { get; set; } = "";
    public string  DES_CULTURE                       { get; set; } = "es-PE";
    public string  COD_MONEDA_OFICIAL                { get; set; } = "";
    public string  COD_MONEDA_EXTRANJERA_REFERENCIA  { get; set; } = "";
    public string  COD_MONEDA_PIVOT_TIPO_CAMBIO      { get; set; } = "";
    public int     NUM_CHARACTERS_MIO_SEARCH         { get; set; } = 3;
    /// <summary>Tipo de valor de cálculo (COMERCIAL.TIPOS_VALOR_CALCULO) que representa el
    /// importe total de un pedido comercial. Lo usa el módulo Financiero para saber qué fila
    /// de PEDIDOS_COMERCIALES_CALCULO mostrar como "el total".</summary>
    public string? COD_TIPO_VALOR_CALCULO_IMPORTE_TOTAL { get; set; }
    /// <summary>Igual que arriba, para el descuento.</summary>
    public string? COD_TIPO_VALOR_CALCULO_DESCUENTO     { get; set; }
    public string? COD_TIPO_VALOR_CALCULO_SUBTOTAL      { get; set; }

    // Clase por defecto que cada módulo usa cuando no se le indica otra.
    public string? COD_CLASE_OPERACION_ORDEN_COMPRA  { get; set; }
    public string? COD_CLASE_MOVIMIENTO_ALMACEN      { get; set; }
    public string? COD_CLASE_MOVIMIENTO_PRODUCCION   { get; set; }

    // ── Valores por defecto de almacén ──────────────────────────────────────────
    // Lo que Reservas y Requerimientos graban cuando la línea no trae lote, proceso u
    // orden de producción. Evita que esas columnas queden en NULL, que es lo que rompe
    // las comparaciones contra el stock (NULL = NULL no es verdadero con ANSI_NULLS ON).
    public string? NUM_LOTE_CONTROL_DEFAULT            { get; set; }
    public string? COD_PROCESO_DEFAULT                 { get; set; }
    public int?    NUM_ORDEN_PRODUCCION_DEFAULT        { get; set; }
    public short?  NUM_ORDEN_PRODUCCION_ITEM_DEFAULT   { get; set; }
    public byte?   NUM_ORDEN_PRODUCCION_UNIDAD_DEFAULT { get; set; }

    // ── Especificaciones técnicas del transporte (pestaña Almacenes) ─────────────
    // Qué especificación técnica de INGENIERIA.ESPECIFICACIONES_TECNICAS guarda, en LOGISTICA.TRANSPORTES_ESPECIFICACIONES_TECNICAS,
    // cada dato del vehículo que pide la guía de remisión.
    public string? COD_ESPECIFICACION_TECNICA_PLACA { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_CONSTANCIA_INSCRIPCION { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_CERTIFICADO_VEHICULAR { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_CERTIFICADO_HABILITACION_MTC { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_VEHICULO_ACARREO { get; set; }
    // ── RFID (pestaña RFID) ──────────────────────────────────────────────────────
    // Qué productos son equipos RFID (el grupo y, si se indica, la familia) y qué especificación técnica de INGENIERIA.ESPECIFICACIONES_TECNICAS guarda,
    // en ESTANDAR.PRODUCTOS_ESPECIFICACIONES_TECNICAS, cada dato del equipo que necesitan la emisión y la impresión de etiquetas.
    // El tipo de equipo no se configura: sale del grupo, la familia y la sub familia del producto. La estación donde corre el agente es UN producto (COD_PRODUCTO_ESTACION_RFID)
    // del grupo, la familia y la sub familia _ESTACION_RFID; la plantilla de impresión es UN archivo (DES_ARCHIVO_PLANTILLA_RFID) dentro de la ruta COD_KEY_PATHFILE_PLANTILLA_RFID
    // (ENTORNO.PATHFILES, Rutas de archivos). Los dos valen para todo el sistema.
    public string? COD_GRUPO_PRODUCTO_EQUIPO_RFID { get; set; }
    public string? COD_FAMILIA_PRODUCTO_EQUIPO_RFID { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_MARCA { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_MODELO { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_PROTOCOLO { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_DIRECCION { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_PUERTO { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_PUERTOS_ANTENA { get; set; }
    public string? COD_ESPECIFICACION_TECNICA_RFID_POTENCIA { get; set; }
    public string? COD_GRUPO_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_FAMILIA_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_SUB_FAMILIA_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_PRODUCTO_ESTACION_RFID { get; set; }
    public string? COD_KEY_PATHFILE_PLANTILLA_RFID { get; set; }
    public string? DES_ARCHIVO_PLANTILLA_RFID { get; set; }
    // ── Compras (pestaña Compras) ───────────────────────────────────────────────
    // La ruta (ENTORNO.PATHFILES, Rutas de archivos) de donde se importan los XML de los proveedores y las subcarpetas a donde pasan los importados y los que fallaron;
    // el tipo de operación con el que entran si no se elige otro; y las tolerancias de la validación (precio en %, importe por redondeo).
    public string? COD_KEY_PATHFILE_COMPRAS_XML { get; set; }
    public string? DES_CARPETA_COMPRAS_PROCESADOS { get; set; }
    public string? DES_CARPETA_COMPRAS_ERRORES { get; set; }
    public string? COD_TIPO_OPERACION_COMPRAS_XML { get; set; }
    public decimal? TAS_TOLERANCIA_PRECIO_COMPRAS { get; set; }
    public decimal? VAL_TOLERANCIA_IMPORTE_COMPRAS { get; set; }
    // ── Carga de XML recibidos (pestaña Compras > XML recibidos) ──────────────────────────
    // Las tres carpetas de Outlook (donde llegan los correos, a donde pasan los ya bajados y a donde pasan los que no se pudieron bajar o procesar), cada cuántos minutos se revisa el correo (vacío: 15)
    // y si la carga automática está activa. Los archivos bajados se guardan en la ruta de la pestaña Compras > Registro de compras, con sus mismas subcarpetas de procesados y de errores.
    public bool?   FLG_XML_RECIBIDOS_ACTIVO { get; set; }
    public int?    CAN_MINUTOS_XML_RECIBIDOS { get; set; }
    public string? DES_CARPETA_CORREO_RECEPCION { get; set; }
    public string? DES_CARPETA_CORREO_PROCESADOS { get; set; }
    public string? DES_CARPETA_CORREO_ERRORES { get; set; }
    // ── Validación de comprobantes de pago electrónicos en SUNAT (pestaña Compras) ──────────
    // El usuario SOL (con el RUC delante: 20123456789USUARIO1) va tal cual. La clave SOL NO vuelve nunca a la pantalla: se guarda cifrada y aquí solo se dice si hay una guardada.
    public string? COD_USUARIO_VCDP { get; set; }
    /// <summary>La clave SOL nueva. Vacía no toca la que ya está guardada.</summary>
    public string? DES_PASSWORD_VCDP_NUEVO { get; set; }
    /// <summary>true quita la clave SOL guardada (si no se manda una nueva).</summary>
    public bool    FLG_QUITAR_PASSWORD_VCDP { get; set; }
}

// Kept for backwards-compat with existing code that references these types.
public class ConfiguracionGeneralInsertDto : ConfiguracionGeneralSaveDto { }
public class ConfiguracionGeneralUpdateDto : ConfiguracionGeneralSaveDto { }
public class ConfiguracionGeneralSearchDto { public string? Search { get; set; } }

/// <summary>Subconjunto SIN datos fiscales/legales (NUM_DOCUMENTO_IDENTIDAD,
/// DES_DOMICILIO_FISCAL, DES_REFERENCIA_FISCAL) ni de auditoría — es lo único que la
/// pantalla de Login necesita para pintarse (nombre/logo/colores/tipografía) ANTES de
/// autenticar. El registro completo (GetAll) ahora requiere [Authorize]; este DTO es el
/// que queda expuesto en un endpoint [AllowAnonymous] aparte.</summary>
public class ConfiguracionGeneralPublicDto
{
    public string DES_NOMBRE_EMPRESA              { get; set; } = "";
    public string DES_NOMBRE_APLICACION           { get; set; } = "";

    // LOS LOGOS YA NO VIAJAN ACÁ. Iban como byte[] dentro del JSON, así que bajaban
    // enteros en cada arranque y el navegador no podía cachearlos por separado: en la
    // medición, esta llamada daba 22 752 ms.
    //
    // Ahora se sirven como archivo, con su propia URL y cabecera de caché:
    //     GET api/v1/estandar/configuracion-general/logo-empresa
    //     GET api/v1/estandar/configuracion-general/logo-aplicacion
    //
    // Los dos flags dicen si hay logo, para que la pantalla decida si dibuja la imagen
    // o su alternativa, sin tener que bajar los bytes para averiguarlo.
    public bool FLG_TIENE_LOGO_EMPRESA            { get; set; }
    public bool FLG_TIENE_LOGO_APLICACION         { get; set; }

    /// <summary>Va en la dirección del logo (…/logo-aplicacion?v=XXXX) para que un logo nuevo no se confunda con el que el
    /// navegador ya tiene guardado. Ver ConfiguracionGeneral.DES_VERSION_LOGO_APLICACION.</summary>
    public string? DES_VERSION_LOGO_EMPRESA       { get; set; }
    public string? DES_VERSION_LOGO_APLICACION    { get; set; }

    public string DES_COLOR_PRIMARY               { get; set; } = "";
    public string DES_BACKCOLOR                   { get; set; } = "";
    public string DES_FORECOLOR                   { get; set; } = "";
    public string DES_COLOR_SECONDARY             { get; set; } = "";
    public string DES_TYPOGRAPHY_TITLE_FONT       { get; set; } = "";
    public int    DES_TYPOGRAPHY_TITLE_SIZE       { get; set; }
    public string DES_FORECOLOR_TITLE             { get; set; } = "";
    public string DES_TYPOGRAPHY_SUBTITLE_FONT    { get; set; } = "";
    public int    NUM_TYPOGRAPHY_SUBTITLE_SIZE    { get; set; }
    public string DES_FORECOLOR_SUBTITLE          { get; set; } = "";
    public string DES_TYPOGRAPHY_CONTROLS_FONT    { get; set; } = "";
    public int    NUM_TYPOGRAPHY_CONTROLS_SIZE    { get; set; }
    public bool   FLG_TECLADO_VIRTUAL             { get; set; }
    public string DES_BACKCOLOR_LOGIN             { get; set; } = "";
    public string DES_FORECOLOR_LOGIN             { get; set; } = "";
    public string DES_BACKCOLOR_DIALOG            { get; set; } = "";
    public string DES_FORECOLOR_DIALOG            { get; set; } = "";
    public bool   FLG_ACTIVAR_LOCKOUT_SCREEN      { get; set; }
    public int    NUM_MINUTOS_LOCKOUT_SCREEN      { get; set; }
    public string DES_SKIN_COLOR                  { get; set; } = "";
    public string DES_MODE_TEXT                   { get; set; } = "";
    public string DES_BUTTON_BACKCOLOR            { get; set; } = "";
    public string DES_BUTTON_FORECOLOR            { get; set; } = "";
    public string DES_CULTURE                     { get; set; } = "es-PE";

    public static ConfiguracionGeneralPublicDto De(ConfiguracionGeneral c) => new()
    {
        DES_NOMBRE_EMPRESA = c.DES_NOMBRE_EMPRESA, DES_NOMBRE_APLICACION = c.DES_NOMBRE_APLICACION,
        FLG_TIENE_LOGO_EMPRESA    = c.IMG_LOGO_EMPRESA    is { Length: > 0 },
        FLG_TIENE_LOGO_APLICACION = c.IMG_LOGO_APLICACION is { Length: > 0 },
        DES_VERSION_LOGO_EMPRESA    = ConfiguracionGeneral.VersionDeLogo(c.IMG_LOGO_EMPRESA),
        DES_VERSION_LOGO_APLICACION = ConfiguracionGeneral.VersionDeLogo(c.IMG_LOGO_APLICACION),
        DES_COLOR_PRIMARY = c.DES_COLOR_PRIMARY, DES_BACKCOLOR = c.DES_BACKCOLOR, DES_FORECOLOR = c.DES_FORECOLOR,
        DES_COLOR_SECONDARY = c.DES_COLOR_SECONDARY,
        DES_TYPOGRAPHY_TITLE_FONT = c.DES_TYPOGRAPHY_TITLE_FONT, DES_TYPOGRAPHY_TITLE_SIZE = c.DES_TYPOGRAPHY_TITLE_SIZE,
        DES_FORECOLOR_TITLE = c.DES_FORECOLOR_TITLE,
        DES_TYPOGRAPHY_SUBTITLE_FONT = c.DES_TYPOGRAPHY_SUBTITLE_FONT, NUM_TYPOGRAPHY_SUBTITLE_SIZE = c.NUM_TYPOGRAPHY_SUBTITLE_SIZE,
        DES_FORECOLOR_SUBTITLE = c.DES_FORECOLOR_SUBTITLE,
        DES_TYPOGRAPHY_CONTROLS_FONT = c.DES_TYPOGRAPHY_CONTROLS_FONT, NUM_TYPOGRAPHY_CONTROLS_SIZE = c.NUM_TYPOGRAPHY_CONTROLS_SIZE,
        FLG_TECLADO_VIRTUAL = c.FLG_TECLADO_VIRTUAL,
        DES_BACKCOLOR_LOGIN = c.DES_BACKCOLOR_LOGIN, DES_FORECOLOR_LOGIN = c.DES_FORECOLOR_LOGIN,
        DES_BACKCOLOR_DIALOG = c.DES_BACKCOLOR_DIALOG, DES_FORECOLOR_DIALOG = c.DES_FORECOLOR_DIALOG,
        FLG_ACTIVAR_LOCKOUT_SCREEN = c.FLG_ACTIVAR_LOCKOUT_SCREEN, NUM_MINUTOS_LOCKOUT_SCREEN = c.NUM_MINUTOS_LOCKOUT_SCREEN,
        DES_SKIN_COLOR = c.DES_SKIN_COLOR, DES_MODE_TEXT = c.DES_MODE_TEXT,
        DES_BUTTON_BACKCOLOR = c.DES_BUTTON_BACKCOLOR, DES_BUTTON_FORECOLOR = c.DES_BUTTON_FORECOLOR,
        DES_CULTURE = c.DES_CULTURE
    };
}
