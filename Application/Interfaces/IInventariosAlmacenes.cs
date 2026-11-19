using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosAlmacenesRepository
{
    Task<IEnumerable<InventariosAlmacenes>> ShowallAsync(int? numInventario = null);
    Task<InventariosAlmacenes?> ShowbyidAsync(int NUM_INVENTARIO, string COD_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<InventariosAlmacenes>> SearchAsync(InventariosAlmacenesSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> UpdateAsync(InventariosAlmacenesUpdateDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN);
}

public interface IInventariosAlmacenesService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenes>>> GetAllAsync(int? numInventario = null);
    Task<ApiResponse<InventariosAlmacenes>> GetByIdAsync(int NUM_INVENTARIO, string COD_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<InventariosAlmacenes>>> SearchAsync(InventariosAlmacenesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosAlmacenesUpdateDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN);
}