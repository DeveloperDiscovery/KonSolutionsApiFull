using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IEntidadesTemporadasRepository
{
    Task<IEnumerable<EntidadesTemporadas>> ShowallAsync(decimal codEntidad);
    Task<EntidadesTemporadas?> ShowbyidAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(decimal codEntidad);
    Task<IEnumerable<EntidadesTemporadas>> SearchAsync(EntidadesTemporadasSearchDto dto);
    Task<WriteResult> InsertAsync(EntidadesTemporadasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA);
    Task<EntidadesTemporadasLoadCombosResult> LoadcombosAsync();
}

public interface IEntidadesTemporadasService
{
    Task<ApiResponse<IEnumerable<EntidadesTemporadas>>> GetAllAsync(decimal codEntidad);
    Task<ApiResponse<EntidadesTemporadas>> GetByIdAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(decimal codEntidad);
    Task<ApiResponse<IEnumerable<EntidadesTemporadas>>> SearchAsync(EntidadesTemporadasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(EntidadesTemporadasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(decimal COD_ENTIDAD, int NUM_TEMPORADA);
    Task<ApiResponse<EntidadesTemporadasLoadCombosResult>> LoadcombosAsync();
}