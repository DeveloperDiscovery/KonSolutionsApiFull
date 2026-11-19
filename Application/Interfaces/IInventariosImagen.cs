using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosImagenRepository
{
    Task<IEnumerable<InventariosImagen>> ShowallAsync();
    Task<InventariosImagen?> ShowbyidAsync(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync();
    Task<IEnumerable<InventariosImagen>> SearchAsync(InventariosImagenSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosImagenInsertDto dto);
    Task<WriteResult> UpdateAsync(InventariosImagenUpdateDto dto);
    Task<WriteResult> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN);
}

public interface IInventariosImagenService
{
    Task<ApiResponse<IEnumerable<InventariosImagen>>> GetAllAsync();
    Task<ApiResponse<InventariosImagen>> GetByIdAsync(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync();
    Task<ApiResponse<IEnumerable<InventariosImagen>>> SearchAsync(InventariosImagenSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosImagenInsertDto dto);
    Task<ApiResponse<WriteResult>> UpdateAsync(InventariosImagenUpdateDto dto);
    Task<ApiResponse<WriteResult>> DeleteAsync(int NUM_INVENTARIO, string COD_ALMACEN, string COD_UBICACION_ALMACEN);
}