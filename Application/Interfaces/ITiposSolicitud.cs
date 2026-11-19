using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Solicitudes;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposSolicitudRepository
{
    Task<IEnumerable<TiposSolicitud>> ShowallAsync();
    Task<TiposSolicitud?> ShowbyidAsync(string COD_TIPO_SOLICITUD);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposSolicitud>> SearchAsync(TiposSolicitudSearchDto dto);
    Task<WriteResult> InsertAsync(TiposSolicitudInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposSolicitudUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_SOLICITUD);
}

public interface ITiposSolicitudService
{
    Task<ApiResponse<IEnumerable<TiposSolicitud>>> GetAllAsync();
    Task<ApiResponse<TiposSolicitud>> GetByIdAsync(string COD_TIPO_SOLICITUD);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposSolicitud>>> SearchAsync(TiposSolicitudSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposSolicitudInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposSolicitudUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_SOLICITUD);
}