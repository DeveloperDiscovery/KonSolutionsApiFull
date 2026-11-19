using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

// Tabla hija: los SP SHOWALL/SEARCH/SHOWCOMBOX reciben @COD_CONTENEDOR obligatorio.
public interface IContenedoresAlmacenajeCasillerosRepository
{
    Task<IEnumerable<ContenedoresAlmacenajeCasilleros>> ShowallAsync(string COD_CONTENEDOR);
    Task<ContenedoresAlmacenajeCasilleros?> ShowbyidAsync(string COD_CONTENEDOR, string COD_CASILLERO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string COD_CONTENEDOR);
    Task<IEnumerable<ContenedoresAlmacenajeCasilleros>> SearchAsync(ContenedoresAlmacenajeCasillerosSearchDto dto);
    /// <summary>El único caso en que se busca sin conocer el contenedor: alguien tiene una
    /// ubicación (de una reserva, de un stock) y quiere saber en qué mueble físico vive.</summary>
    Task<ContenedoresAlmacenajeCasilleros?> BuscarPorUbicacionAsync(string codAlmacen, string codUbicacionAlmacen);
    Task<WriteResult> InsertAsync(ContenedoresAlmacenajeCasillerosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_CONTENEDOR, string COD_CASILLERO);
    Task<WriteResult> GenerarAsync(ContenedoresAlmacenajeCasillerosGenerarDto dto, string codUsuario, string codEstacion);
}

public interface IContenedoresAlmacenajeCasillerosService
{
    Task<ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>> GetAllAsync(string COD_CONTENEDOR);
    Task<ApiResponse<ContenedoresAlmacenajeCasilleros>> GetByIdAsync(string COD_CONTENEDOR, string COD_CASILLERO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string COD_CONTENEDOR);
    Task<ApiResponse<IEnumerable<ContenedoresAlmacenajeCasilleros>>> SearchAsync(ContenedoresAlmacenajeCasillerosSearchDto dto);
    Task<ApiResponse<ContenedoresAlmacenajeCasilleros>> BuscarPorUbicacionAsync(string codAlmacen, string codUbicacionAlmacen);
    Task<ApiResponse<WriteResult>> InsertAsync(ContenedoresAlmacenajeCasillerosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_CONTENEDOR, string COD_CASILLERO);
    Task<ApiResponse<WriteResult>> GenerarAsync(ContenedoresAlmacenajeCasillerosGenerarDto dto, string codUsuario, string codEstacion);
}
