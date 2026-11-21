using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace KonSolutionsApi.Common;

/// <summary>Regla de la solución: un campo FLG_ que llega sin marcar se guarda como false, nunca como NULL.
/// Los DTO traen los FLG_ como bool? y la pantalla manda null (o no manda nada) cuando el check no se tocó; al leer el cuerpo de la petición
/// todo FLG_ bool? que quede en null pasa a false. Se aplica una sola vez, a todos los DTO, en vez de repetirlo en cada controlador.
/// No toca los DTO de búsqueda / filtro / consulta: ahí null significa "sin filtrar", no "desmarcado". Un valor explícito (true/false) nunca se cambia.</summary>
public static class FlgFalseJson
{
    private static readonly string[] SinRegla = { "Search", "Filtro", "Filter", "Consulta", "Query", "Busqueda" };

    /// <summary>Se registra como modificador del contrato JSON de MVC (ver Program.cs).</summary>
    public static void Modificador(JsonTypeInfo tipo)
    {
        if (tipo.Kind != JsonTypeInfoKind.Object) return;
        if (SinRegla.Any(s => tipo.Type.Name.Contains(s, StringComparison.OrdinalIgnoreCase))) return;
        var flags = tipo.Properties
            .Where(p => p.PropertyType == typeof(bool?) && p.Get is not null && p.Set is not null
                        && p.AttributeProvider is PropertyInfo pi && pi.Name.StartsWith("FLG_", StringComparison.Ordinal))
            .ToList();
        if (flags.Count == 0) return;
        var anterior = tipo.OnDeserialized;
        tipo.OnDeserialized = o =>
        {
            anterior?.Invoke(o);
            foreach (var p in flags) if (p.Get!(o) is null) p.Set!(o, false);
        };
    }

    /// <summary>Opciones con la regla aplicada (las usa Program.cs y las pruebas).</summary>
    public static JsonSerializerOptions Opciones() => new(JsonSerializerDefaults.Web)
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver { Modifiers = { Modificador } }
    };
}
