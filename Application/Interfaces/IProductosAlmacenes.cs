using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

// Los SP de esta tabla NO listan "todos los productos": SHOWALL, SEARCH y SHOWCOMBOX
// exigen @COD_PRODUCTO (sin default), porque la pantalla siempre entra desde un producto.
// Por eso las tres firmas lo llevan — el scaffold original no lo mandaba y el motor
// respondía error 201 ("expects parameter @COD_PRODUCTO, which was not supplied").
public interface IProductosAlmacenesRepository
{
    Task<IEnumerable<ProductosAlmacenes>> ShowallAsync(string codProducto);
    Task<IEnumerable<ProductosAlmacenes>> ShowallPorAlmacenAsync(string codAlmacen);
    Task<IEnumerable<ProductosAlmacenes>> ShowallRequerirAsync();
    Task<ProductosAlmacenes?> ShowbyidAsync(string COD_PRODUCTO, string COD_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(string codProducto);
    Task<IEnumerable<ProductosAlmacenes>> SearchAsync(string codProducto, ProductosAlmacenesSearchDto dto);
    Task<WriteResult> InsertAsync(ProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(ProductosAlmacenesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(string COD_PRODUCTO, string COD_ALMACEN);
    Task<WriteResult> ResetAsync(string? codAlmacen, string? codProducto);
    Task<WriteResult> RequerimientoAsync(string codUsuario, string codEstacion);
    Task<WriteResult> CantidadesAsync(string lstAlmacenes, string lstProductos);
    Task<WriteResult> ReversionAsync(int numRequerimientoStock, string codUsuario, string codEstacion);
}

public interface IProductosAlmacenesService
{
    Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> GetAllAsync(string codProducto);
    Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> GetAllPorAlmacenAsync(string codAlmacen);
    Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> GetAllRequerirAsync();
    Task<ApiResponse<ProductosAlmacenes>> GetByIdAsync(string COD_PRODUCTO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(string codProducto);
    Task<ApiResponse<IEnumerable<ProductosAlmacenes>>> SearchAsync(string codProducto, ProductosAlmacenesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(ProductosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(ProductosAlmacenesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(string COD_PRODUCTO, string COD_ALMACEN);
    Task<ApiResponse<WriteResult>> ResetAsync(string? codAlmacen, string? codProducto);
    Task<ApiResponse<WriteResult>> RequerimientoAsync(string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> CantidadesAsync(string lstAlmacenes, string lstProductos);
    Task<ApiResponse<WriteResult>> ReversionAsync(int numRequerimientoStock, string codUsuario, string codEstacion);
}