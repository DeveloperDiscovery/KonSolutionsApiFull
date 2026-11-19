using System.Reflection;

namespace KonSolutionsApi.Common;

/// <summary>
/// Extensión para recortar (TrimEnd) los espacios en blanco de la derecha
/// de todas las propiedades string de un objeto antes de persistirlo.
/// Uso: dto.TrimStrings(); antes de construir los DynamicParameters en el repositorio.
/// </summary>
public static class TrimExtensions
{
    /// <summary>
    /// Aplica RTRIM (TrimEnd) a todas las propiedades string públicas y escribibles del objeto.
    /// No afecta el contenido a la izquierda — solo elimina espacios sobrantes a la derecha,
    /// que es el patrón típico cuando los datos provienen de columnas CHAR de SQL Server
    /// o cuando el usuario deja espacios al final al escribir en un campo de texto.
    /// </summary>
    public static T TrimStrings<T>(this T obj) where T : class
    {
        if (obj is null) return obj!;
        foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (prop.PropertyType != typeof(string) || !prop.CanRead || !prop.CanWrite) continue;
            var value = (string?)prop.GetValue(obj);
            if (value is not null) prop.SetValue(obj, value.TrimEnd());
        }
        return obj;
    }
}
