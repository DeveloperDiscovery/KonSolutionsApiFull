using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposNotificacionRepository
{
    Task<IEnumerable<TiposNotificacion>> ShowallAsync();
    Task<TiposNotificacion?> ShowbyidAsync(string COD_TIPO_NOTIFICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposNotificacion>> SearchAsync(TiposNotificacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposNotificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_TIPO_NOTIFICACION);
}

public interface ITiposNotificacionService
{
    Task<ApiResponse<IEnumerable<TiposNotificacion>>> GetAllAsync();
    Task<ApiResponse<TiposNotificacion>> GetByIdAsync(string COD_TIPO_NOTIFICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposNotificacion>>> SearchAsync(TiposNotificacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposNotificacionInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_NOTIFICACION);
}
