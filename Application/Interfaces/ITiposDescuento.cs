using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDescuentoRepository
{
    Task<IEnumerable<TiposDescuento>> ShowallAsync();
    Task<TiposDescuento?> ShowbyidAsync(int COD_TIPO_DESCUENTO);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDescuento>> SearchAsync(TiposDescuentoSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDescuentoInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposDescuentoUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_DESCUENTO);
}

public interface ITiposDescuentoService
{
    Task<ApiResponse<IEnumerable<TiposDescuento>>> GetAllAsync();
    Task<ApiResponse<TiposDescuento>> GetByIdAsync(int COD_TIPO_DESCUENTO);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDescuento>>> SearchAsync(TiposDescuentoSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDescuentoInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposDescuentoUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_DESCUENTO);
}