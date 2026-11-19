namespace KonSolutionsApi.API.Filters;

/// <summary>Declara sobre qué objeto trabaja un controlador, para que el guardián de permisos
/// sepa contra qué fila de SEGURIDAD.APLICACIONES_OPCIONES.DES_OBJETO contrastar.
///
/// Va con el nombre de la TABLA (`LOGISTICA.ALMACENES`) y no con el de la opción de menú: así
/// el permiso no se rompe cuando una opción se mueve de aplicación — que fue exactamente la
/// preocupación que dio origen a este diseño.
///
/// Se pone en el controlador. Sólo hace falta repetirlo en una acción cuando esa acción toca
/// otro objeto distinto del que declara el controlador.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class ExigePermisoAttribute : Attribute
{
    public string Objeto { get; }
    public ExigePermisoAttribute(string objeto) => Objeto = objeto;
}

/// <summary>Deja un endpoint fuera del control de permisos, sin sacarle el [Authorize].
///
/// Es para lo que no administra ninguna tabla: login, refresh de token, combos de apoyo,
/// catálogos que consulta todo el mundo. Marcarlo explícito obliga a que sea una decisión y
/// no un olvido — el guardián registra en auditoría todo lo que llega sin objeto declarado.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class SinControlPermisosAttribute : Attribute
{
    public string? Motivo { get; }
    public SinControlPermisosAttribute(string? motivo = null) => Motivo = motivo;
}
