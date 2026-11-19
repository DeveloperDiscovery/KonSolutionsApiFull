using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Aprobaciones;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposAprobacionRepository
{
    Task<IEnumerable<TiposAprobacion>> ShowallAsync();
    Task<TiposAprobacion?> ShowbyidAsync(string COD_TIPO_APROBACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposAprobacion>> SearchAsync(TiposAprobacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposAprobacionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposAprobacionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_APROBACION);
}

public interface ITiposAprobacionService
{
    Task<ApiResponse<IEnumerable<TiposAprobacion>>> GetAllAsync();
    Task<ApiResponse<TiposAprobacion>> GetByIdAsync(string COD_TIPO_APROBACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposAprobacion>>> SearchAsync(TiposAprobacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposAprobacionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposAprobacionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_APROBACION);
}