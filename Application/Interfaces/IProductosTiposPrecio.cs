using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Comercial;

namespace KonSolutionsApi.Application.Interfaces;

public interface IProductosTiposPrecioRepository
{
    Task<IEnumerable<ProductosTiposPrecio>> ShowallAsync();
    Task<ProductosTiposPrecio?> ShowbyidAsync(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<ProductosTiposPrecio>> SearchAsync(ProductosTiposPrecioSearchDto dto);
    Task<WriteResult> InsertAsync(ProductosTiposPrecioInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProductosTiposPrecioUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
}

public interface IProductosTiposPrecioService
{
    Task<ApiResponse<IEnumerable<ProductosTiposPrecio>>> GetAllAsync();
    Task<ApiResponse<ProductosTiposPrecio>> GetByIdAsync(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<ProductosTiposPrecio>>> SearchAsync(ProductosTiposPrecioSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProductosTiposPrecioInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProductosTiposPrecioUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO, int COD_TIPO_PRECIO, string COD_MONEDA);
}