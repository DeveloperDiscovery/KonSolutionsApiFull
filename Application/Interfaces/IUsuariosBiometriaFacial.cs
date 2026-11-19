using KonSolutionsApi.Domain.Models.Seguridad;

namespace KonSolutionsApi.Application.Interfaces;

public interface IUsuariosBiometriaFacialRepository
{
    /// <summary>Las muestras de UN usuario. No existe un "traer todo": el padrón
    /// biométrico completo no tiene por qué estar nunca en memoria.</summary>
    Task<IEnumerable<UsuariosBiometriaFacial>> ShowallAsync(string codUsuario);

    Task<UsuariosBiometriaFacialEstado> EstadoAsync(string codUsuario);
    Task<IEnumerable<UsuariosBiometriaFacialEstado>> EstadosAsync();

    /// <summary>Reemplaza el juego completo de muestras del usuario, en UNA transacción.
    ///
    /// Es borrar y volver a grabar, no agregar: mezclar muestras viejas con nuevas es lo
    /// que hace que la verificación empiece a aceptar caras parecidas. Y va en una
    /// transacción porque el estado intermedio —las viejas borradas y las nuevas todavía
    /// no— es un usuario que no puede entrar.</summary>
    Task<int> ReemplazarAsync(string codUsuario, IReadOnlyList<double[]> descriptores,
                              string codUsuarioRegistro, string codEstacion);

    /// <summary>numMuestra en null borra todas las del usuario.</summary>
    Task<int> DeleteAsync(string codUsuario, int? numMuestra = null);
}
