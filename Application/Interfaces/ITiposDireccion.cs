using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface ITiposDireccionRepository
{
    Task<IEnumerable<TiposDireccion>> ShowallAsync();
    Task<TiposDireccion?> ShowbyidAsync(int COD_TIPO_DIRECCION);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<TiposDireccion>> SearchAsync(TiposDireccionSearchDto dto);
    Task<WriteResult> InsertAsync(TiposDireccionInsertDto dto);
    Task<WriteResult> UpdateAsync(TiposDireccionUpdateDto dto);
    Task<WriteResult> DeleteAsync(int COD_TIPO_DIRECCION);
}

public interface ITiposDireccionService
{
    Task<ApiResponse<IEnumerable<TiposDireccion>>> GetAllAsync();
    Task<ApiResponse<TiposDireccion>> GetByIdAsync(int COD_TIPO_DIRECCION);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<TiposDireccion>>> SearchAsync(TiposDireccionSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(TiposDireccionInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(TiposDireccionUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int COD_TIPO_DIRECCION);
}