namespace KonSolutionsApi.Domain.Models.Entorno;

public class NotificacionAprobacionFicha
{
    public int      NUM_NOTIFICACION           { get; set; }
    public string?  DES_NOTIFICACION           { get; set; }
    public string?  DES_INFORMACION            { get; set; }
    public DateTime FEC_NOTIFICACION           { get; set; }
    public DateTime? FEC_VENCIMIENTO           { get; set; }
    public string?  DES_NOTIFICADOR            { get; set; }
    public string?  DES_CARGO                  { get; set; }
    public string?  DES_AREA                   { get; set; }
    public string?  DES_CORREO_INSTITUCIONAL   { get; set; }
    public string?  DES_ESTADO                 { get; set; }
    public byte[]?  IMG_FOTO_NOTIFICADOR       { get; set; }
    public byte[]?  IMG_TIPO_NOTIFICACION      { get; set; }
    public byte[]?  IMG_ESTADO                 { get; set; }
    public string?  DES_BACKCOLOR              { get; set; }
    public string?  DES_FORECOLOR              { get; set; }
}

public class NotificacionAprobacionDocumento
{
    public int      NUM_SECUENCIA               { get; set; }
    public string?  DES_TIPO_COMPROBANTE_PAGO    { get; set; }
    public string?  NUM_COMPROBANTE_PAGO         { get; set; }
    public string?  DES_PATHFILE                 { get; set; }
}

public class NotificacionAprobacionHistorial
{
    public byte[]?  IMG_FOTO                     { get; set; }
    public string?  COD_USUARIO                  { get; set; }
    public string?  DES_USUARIO                  { get; set; }
    public string?  DES_ACCION_CORRESPONDIENTE   { get; set; }
    public string?  DES_APROBADOR_FINAL          { get; set; }
    public string?  DES_CARGO                    { get; set; }
    public string?  DES_AREA                     { get; set; }
    public int?     COD_ESTADO                   { get; set; }
    public string?  DES_ESTADO                   { get; set; }
    public string?  DES_BACKCOLOR                { get; set; }
    public string?  DES_FORECOLOR                { get; set; }
    public byte[]?  IMG_ESTADO                   { get; set; }
    public string?  DES_CORREO_INSTITUCIONAL     { get; set; }
    public DateTime? FEC_ACTUALIZACION           { get; set; }
}

public class NotificacionAprobacionEjecucion
{
    public string?  DES_ACCION       { get; set; }
    public int?     COD_TIPO_ESTADO  { get; set; }
    /// <summary>Siempre vacío cuando la ficha se pide con usuario: el cliente no recibe ni manda SQL.</summary>
    public string?  DES_COMMANDSQL   { get; set; }
    /// <summary>APROBAR, RECHAZAR u OBSERVAR: lo que el botón le pide al servidor.</summary>
    public string?  COD_ACCION       { get; set; }
    public int?     COD_ESTADO       { get; set; }
    public string?  DES_ESTADO       { get; set; }
    public byte[]?  IMG_ESTADO       { get; set; }
    public string?  DES_BACKCOLOR    { get; set; }
    public string?  DES_FORECOLOR    { get; set; }
}

public class NotificacionAprobacionResult
{
    public NotificacionAprobacionFicha?             Ficha       { get; set; }
    public List<NotificacionAprobacionDocumento>    Documentos  { get; set; } = new();
    public List<NotificacionAprobacionHistorial>    Historial   { get; set; } = new();
    public List<NotificacionAprobacionEjecucion>    Ejecucion   { get; set; } = new();
}

/// <summary>Cuerpo de la acción sobre una notificación. No lleva SQL: la acción va en la ruta y lo demás lo decide el servidor.</summary>
public class NotificacionAprobacionAccionDto
{
    [System.ComponentModel.DataAnnotations.StringLength(500)]
    public string? DES_OBSERVACION { get; set; }
}

/// <summary>Resultado de aprobar, rechazar u observar.</summary>
public class NotificacionAprobacionAccionResult
{
    public long   NUM_NOTIFICACION  { get; set; }
    public string COD_ACCION        { get; set; } = string.Empty;
    /// <summary>true si la acción cerró el flujo (rechazo, observación o aprobación del último nivel); false si solo pasó el turno al siguiente aprobador.</summary>
    public bool   FLG_FLUJO_CERRADO { get; set; }
}
