using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesTemporadasColeccionesRepository
{
    Task<IEnumerable<EntidadesTemporadasColecciones>> ShowallAsync(decimal codEntidad, int numTemporada);
    Task<EntidadesTemporadasColecciones?> ShowbyidAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal codEntidad, int numTemporada);
    Task<IEnumerable<EntidadesTemporadasColecciones>> SearchAsync(EntidadesTemporadasColeccionesSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesTemporadasColeccionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION);
}

public interface IEntidadesTemporadasColeccionesService
{
    Task<ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>> GetAllAsync(decimal codEntidad, int numTemporada);
    Task<ApiResponse<EntidadesTemporadasColecciones>> GetByIdAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal codEntidad, int numTemporada);
    Task<ApiResponse<IEnumerable<EntidadesTemporadasColecciones>>> SearchAsync(EntidadesTemporadasColeccionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTemporadasColeccionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA, int NUM_COLECCION);
}
