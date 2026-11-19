using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Zonificacion;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposZonificacionRepository
{
    Task<IEnumerable<TiposZonificacion>> ShowallAsync();
    Task<TiposZonificacion?> ShowbyidAsync(string COD_TIPO_ZONIFICACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposZonificacion>> SearchAsync(TiposZonificacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposZonificacionInsertDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_ZONIFICACION);
}

public interface ITiposZonificacionService
{
    Task<ApiResponse<IEnumerable<TiposZonificacion>>> GetAllAsync();
    Task<ApiResponse<TiposZonificacion>> GetByIdAsync(string COD_TIPO_ZONIFICACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposZonificacion>>> SearchAsync(TiposZonificacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposZonificacionInsertDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_ZONIFICACION);
}