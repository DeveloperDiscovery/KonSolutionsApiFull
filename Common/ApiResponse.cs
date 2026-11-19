namespace KonSolutionsApi.Common;

public class ApiResponse<T>
{
    public bool   Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T?     Data    { get; set; }
    public int    Total   { get; set; }
    public static ApiResponse<T> Ok(T data, string message = "OK", int total = 0) =>
        new() { Success = true, Message = message, Data = data, Total = total };
    public static ApiResponse<T> Fail(string message) =>
        new() { Success = false, Message = message };

    /// <summary>
    /// Usa este método en services que retornan WriteResult.
    /// Si el SP devolvió Estado="ERROR", retorna Fail automáticamente y dispara OnSpError para logging.
    /// </summary>
    public static ApiResponse<WriteResult> FromWriteResult(WriteResult r, string okMessage = "Operación realizada.")
    {
        if (r.IsError)
        {
            WriteResult.OnSpError?.Invoke(r.Mensaje);
            return new() { Success = false, Message = r.Mensaje, Data = r };
        }
        return new() { Success = true, Message = string.IsNullOrWhiteSpace(r.Mensaje) ? okMessage : r.Mensaje, Data = r };
    }
}

public class WriteResult
{
    public int    AffectedRows   { get; set; }
    public string Estado         { get; set; } = "OK";
    public string Mensaje        { get; set; } = string.Empty;
    public int    ErrorNumber    { get; set; }

    /// <summary>Clave que generó la base en el alta, cuando la genera ella.
    ///
    /// Los SP de las tablas con IDENTITY terminan con un SELECT del SCOPE_IDENTITY. Hasta ahora
    /// ese número se perdía —WriteResult no tenía dónde alojarlo y Dapper lo descartaba— y el
    /// llamador quedaba sin saber qué registro acababa de crear.
    ///
    /// Eso alcanzaba mientras el alta fuera de una sola fila. Deja de alcanzar en los documentos
    /// con detalle: para insertar los ítems hay que conocer el número de la cabecera recién
    /// creada, y la alternativa —releer el listado y quedarse con el último— se equivoca apenas
    /// dos usuarios graban a la vez.
    ///
    /// Es string y no int a propósito: unos SP devuelven un entero y otros un código armado con
    /// ceros a la izquierda, y el que llama sabe cuál de los dos espera.</summary>
    public string Codigo         { get; set; } = string.Empty;
    public string ErrorProcedure { get; set; } = string.Empty;
    public string SqlSentence    { get; set; } = string.Empty;

    /// <summary>Puente para el CATCH de los SP.
    ///
    /// Todos devuelven el detalle del error con el alias ErrorMessage, pero acá la propiedad
    /// se llama Mensaje, así que Dapper no tenía dónde ponerlo y el texto se perdía: llegaba
    /// un WriteResult con ErrorNumber cargado y Mensaje vacío, y la pantalla terminaba
    /// mostrando "OK" seguido del nombre del SP en vez del motivo real.
    ///
    /// Antes casi no se notaba porque los SP rara vez tomaban ese camino. Con los _INSERT
    /// estrictos —que rechazan el alta duplicada— pasó a ser el mensaje más frecuente del
    /// sistema, y era justo el que no se veía.</summary>
    public string ErrorMessage
    {
        get => Mensaje;
        set => Mensaje = value;
    }

    /// <summary>
    /// True si el SP retornó Estado distinto de OK, o si ErrorNumber viene distinto de 0 — red
    /// de seguridad para SPs cuyo CATCH no setea Estado='ERROR' explícitamente (ej.: un SELECT
    /// de error que solo trae ErrorNumber/ErrorMessage y deja Estado en su default "OK").
    /// No se usa AffectedRows como señal: varios SPs (Grupos/Familias/Sub Familias de
    /// Producto, etc.) no devuelven esa columna en su camino exitoso, así que quedaría en 0
    /// por defecto en C# aunque el guardado sí haya funcionado — eso generaba falsos errores.
    /// </summary>
    public bool IsError => (!string.IsNullOrWhiteSpace(Estado) && Estado.ToUpperInvariant() != "OK") || ErrorNumber != 0;

    /// <summary>ErrorLogService se suscribe a este evento al inicializarse para loguear errores de SP sin acoplar a DI.</summary>
    public static Action<string>? OnSpError { get; set; }
}

public class ComboboxItem
{
    public string Id    { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public string? Grupo { get; set; }
}

/// <summary>Item de estado con colores y flag default para EstadoCombo.</summary>
public class EstadoItem
{
    public int    CodEstado  { get; set; }
    public string DesEstado  { get; set; } = string.Empty;
    public string BackColor  { get; set; } = string.Empty;
    public string ForeColor  { get; set; } = string.Empty;
    public bool   EsDefault  { get; set; }
}
