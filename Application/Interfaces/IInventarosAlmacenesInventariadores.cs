using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventarosAlmacenesInventariadoresRepository
{
    Task<IEnumerable<InventarosAlmacenesInventariadores>> ShowallAsync();
    Task<InventarosAlmacenesInventariadores?> ShowbyidAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<InventarosAlmacenesInventariadores>> SearchAsync(InventarosAlmacenesInventariadoresSearchDto dto);
    Task<WriteResult> InsertAsync(InventarosAlmacenesInventariadoresInsertDto dto);
    Task<WriteResult> UpdateAsync(InventarosAlmacenesInventariadoresUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR);
}

public interface IInventarosAlmacenesInventariadoresService
{
    Task<ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>> GetAllAsync();
    Task<ApiResponse<InventarosAlmacenesInventariadores>> GetByIdAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<InventarosAlmacenesInventariadores>>> SearchAsync(InventarosAlmacenesInventariadoresSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventarosAlmacenesInventariadoresInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventarosAlmacenesInventariadoresUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, decimal COD_ENTIDAD_INVENTARIADOR);
}