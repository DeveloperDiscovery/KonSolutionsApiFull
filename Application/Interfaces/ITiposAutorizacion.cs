using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Entorno;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposAutorizacionRepository
{
    Task<IEnumerable<TiposAutorizacion>> ShowallAsync();
    Task<TiposAutorizacion?> ShowbyidAsync(string COD_TIPO_AUTORIZACION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposAutorizacion>> SearchAsync(TiposAutorizacionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposAutorizacionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposAutorizacionUpdateDto dto);
    Task<WriteResult> DeleteAsync(string COD_TIPO_AUTORIZACION);
}

public interface ITiposAutorizacionService
{
    Task<ApiResponse<IEnumerable<TiposAutorizacion>>> GetAllAsync();
    Task<ApiResponse<TiposAutorizacion>> GetByIdAsync(string COD_TIPO_AUTORIZACION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposAutorizacion>>> SearchAsync(TiposAutorizacionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposAutorizacionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposAutorizacionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_TIPO_AUTORIZACION);
}