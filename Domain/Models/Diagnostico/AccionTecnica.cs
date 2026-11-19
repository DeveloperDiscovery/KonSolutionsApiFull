namespace KonSolutionsApi.Domain.Models.Diagnostico;

/// <summary>
/// Ficha técnica de un botón de acción: qué clase, qué método y qué stored procedure
/// hay detrás. La consume el menú contextual (clic derecho) de AccionesBotones.razor.
///
/// Los tres datos NO salen de la configuración en BD: DES_METODO no se está registrando,
/// así que el único origen confiable es el código mismo. Este modelo es el resultado de
/// resolverlo por reflexión sobre los controllers reales y sobre las constantes de
/// SP (Common/StoredProcedureNames.cs) — o sea que refleja lo que la aplicación hace hoy,
/// no lo que alguien anotó alguna vez.
/// </summary>
public class AccionTecnica
{
    /// <summary>Razor que dibuja la pantalla, tal como lo tiene la opción en DES_FORMULARIO.</summary>
    public string? Formulario { get; set; }

    /// <summary>Clase del controller que atiende la acción (ej. "InventariosController").</summary>
    public string? Clase { get; set; }

    /// <summary>Método del controller (ej. "Update").</summary>
    public string? Metodo { get; set; }

    /// <summary>Ruta HTTP completa, verbo incluido (ej. "PUT api/v1/logistica/inventarios").</summary>
    public string? Ruta { get; set; }

    /// <summary>Stored procedure que termina ejecutando (ej. "PROC_LOGISTICA_INVENTARIOS_UPDATE").</summary>
    public string? StoredProcedure { get; set; }

    /// <summary>Nombre del botón según la configuración, para que el diálogo diga de cuál habla.</summary>
    public string? Boton { get; set; }

    /// <summary>Verbo con el que se resolvió, deducido de los FLG_ACCION_* del botón.</summary>
    public string? Verbo { get; set; }

    /// <summary>
    /// Qué se pudo resolver y qué no. Es la parte importante de este modelo: un botón de
    /// proceso sin implementar (el caso de AccionProceso en Inventarios) no tiene método ni
    /// SP que mostrar, y decirlo explícito es más útil que devolver un nombre inventado por
    /// convención. Ejemplos: "Resuelto", "El botón no declara ningún verbo HTTP: la pantalla
    /// lo despacha por nombre y todavía no tiene acción implementada".
    /// </summary>
    public string? Observacion { get; set; }

    /// <summary>True sólo si Clase, Metodo y StoredProcedure quedaron los tres resueltos.</summary>
    public bool Resuelto { get; set; }
}
