using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposSolicitudComercialAreasRepository
{
    Task<IEnumerable<TiposSolicitudComercialAreas>> ShowallAsync(string codTipoSolicitudComercial);
    Task<TiposSolicitudComercialAreas?> ShowbyidAsync(string codTipoSolicitudComercial, int codArea);
    // El SP SHOWCOMBOX de este puente devuelve las mismas columnas que SHOWALL (no
    // Id/Texto) — se conserva el nombre por paridad con el resto del sistema, pero
    // el combo real para "agregar área" lo arma el diálogo con AreasController.
    Task<IEnumerable<TiposSolicitudComercialAreas>> ShowcomboxAsync(string codTipoSolicitudComercial);
    Task<IEnumerable<TiposSolicitudComercialAreas>> SearchAsync(TiposSolicitudComercialAreasSearchDto dto);
    Task<WriteResult> InsertAsync(TiposSolicitudComercialAreasInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string codTipoSolicitudComercial, int codArea);
}

public interface ITiposSolicitudComercialAreasService
{
    Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>> GetAllAsync(string codTipoSolicitudComercial);
    Task<ApiResponse<TiposSolicitudComercialAreas>> GetByIdAsync(string codTipoSolicitudComercial, int codArea);
    Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>> GetComboboxAsync(string codTipoSolicitudComercial);
    Task<ApiResponse<IEnumerable<TiposSolicitudComercialAreas>>> SearchAsync(TiposSolicitudComercialAreasSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudComercialAreasInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoSolicitudComercial, int codArea);
}
