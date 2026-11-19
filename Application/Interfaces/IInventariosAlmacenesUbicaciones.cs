using KonSolutionsApi.Common;
using KonSolutionsApi.Domain.Models.Logistica;

namespace KonSolutionsApi.Application.Interfaces;

public interface IInventariosAlmacenesUbicacionesRepository
{
    Task<IEnumerable<InventariosAlmacenesUbicaciones>> ShowallAsync(int numInventario, string codAlmacen);
    Task<InventariosAlmacenesUbicaciones?> ShowbyidAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen);
    Task<IEnumerable<ComboboxItem>> ShowcomboxAsync(int numInventario, string codAlmacen);
    Task<IEnumerable<InventariosAlmacenesUbicaciones>> SearchAsync(InventariosAlmacenesUbicacionesSearchDto dto);
    Task<WriteResult> InsertAsync(InventariosAlmacenesUbicacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<WriteResult> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen);
}

public interface IInventariosAlmacenesUbicacionesService
{
    Task<ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>> GetAllAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<InventariosAlmacenesUbicaciones>> GetByIdAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen);
    Task<ApiResponse<IEnumerable<ComboboxItem>>> GetComboboxAsync(int numInventario, string codAlmacen);
    Task<ApiResponse<IEnumerable<InventariosAlmacenesUbicaciones>>> SearchAsync(InventariosAlmacenesUbicacionesSearchDto dto);
    Task<ApiResponse<WriteResult>> InsertAsync(InventariosAlmacenesUbicacionesInsertDto dto, string codUsuario, string codEstacion);
    Task<ApiResponse<WriteResult>> DeleteAsync(int numInventario, string codAlmacen, string codUbicacionAlmacen);
}
