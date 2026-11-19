using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposNotificacionAccionesRepository
{
    Task<IEnumerable<TiposNotificacionAcciones>> ShowallAsync(string codTipoNotificacion);
    Task<TiposNotificacionAcciones?> ShowbyidAsync(string codTipoNotificacion, int numSecuencia);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codTipoNotificacion);
    Task<IEnumerable<TiposNotificacionAcciones>> SearchAsync(TiposNotificacionAccionesSearchDto dto);
    Task<WriteResult> InsertAsync(TiposNotificacionAccionesInsertDto dto);
    Task<WriteResult> DeleteAsync(string codTipoNotificacion, int numSecuencia);
}

public interface ITiposNotificacionAccionesService
{
    Task<ApiResponse<IEnumerable<TiposNotificacionAcciones>>> GetAllAsync(string codTipoNotificacion);
    Task<ApiResponse<TiposNotificacionAcciones>> GetByIdAsync(string codTipoNotificacion, int numSecuencia);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codTipoNotificacion);
    Task<ApiResponse<IEnumerable<TiposNotificacionAcciones>>> SearchAsync(TiposNotificacionAccionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposNotificacionAccionesInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string codTipoNotificacion, int numSecuencia);
}
