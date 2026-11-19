using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosAlmacenesProductosRepository
{
    Task<IEnumerable<InventariosAlmacenesProductos>> ShowallAsync(int numInventario, string codAlmacen);
    Task<InventariosAlmacenesProductos?> ShowbyidAsync(int numInventario, string codAlmacen, string codProducto);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(int numInventario, string codAlmacen);
    Task<IEnumerable<InventariosAlmacenesProductos>> SearchAsync(InventariosAlmacenesProductosSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, string codProducto);
}

public interface IInventariosAlmacenesProductosService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesProductos>>> GetAllAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<InventariosAlmacenesProductos>> GetByIdAsync(int numInventario, string codAlmacen, string codProducto);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesProductos>>> SearchAsync(InventariosAlmacenesProductosSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesProductosInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, string codProducto);
}
