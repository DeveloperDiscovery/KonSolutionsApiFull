using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposPrecioRepository
{
    Task<IEnumerable<TiposPrecio>> ShowallAsync();
    Task<TiposPrecio?> ShowbyidAsync(int COD_TIPO_PRECIO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposPrecio>> SearchAsync(TiposPrecioSearchDto dto);
    Task<WriteResult> InsertAsync(TiposPrecioInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposPrecioUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_PRECIO);
}

public interface ITiposPrecioService
{
    Task<ApiResponse<IEnumerable<TiposPrecio>>> GetAllAsync();
    Task<ApiResponse<TiposPrecio>> GetByIdAsync(int COD_TIPO_PRECIO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposPrecio>>> SearchAsync(TiposPrecioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposPrecioInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposPrecioUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_PRECIO);
}