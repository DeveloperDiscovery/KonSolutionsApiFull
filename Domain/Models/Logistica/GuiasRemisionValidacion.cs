namespace KonSolutionsApi.Domain.Models.Logistica;

/// <summary>Una observación del validador previo a la emisión.</summary>
public class GuiaValidacionItem
{
    /// <summary>Identifica la regla (GRE-0xx): sirve para buscarla en el código y para las pruebas.</summary>
    public string Codigo { get; set; } = string.Empty;
    /// <summary>ERROR impide emitir. AVISO se puede emitir, pero conviene revisarlo.</summary>
    public string Gravedad { get; set; } = GuiaValidacionGravedad.Error;
    /// <summary>Dónde se corrige: Configuración, Movimiento, Destinatario, Transporte, Puntos, Mercadería...</summary>
    public string Seccion { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
}

public static class GuiaValidacionGravedad
{
    public const string Error = "ERROR";
    public const string Aviso = "AVISO";
}

/// <summary>El resultado de validar un movimiento antes de emitir su guía.</summary>
public class GuiaValidacionResultado
{
    public decimal NUM_MOVIMIENTO_ALMACEN { get; set; }
    public bool PuedeEmitir { get; set; }
    public int NumErrores { get; set; }
    public int NumAvisos { get; set; }
    public List<GuiaValidacionItem> Items { get; set; } = new();
    /// <summary>La guía vigente del movimiento, si ya tiene una (para mostrar su estado).</summary>
    public GuiaRemision? GuiaVigente { get; set; }
}
